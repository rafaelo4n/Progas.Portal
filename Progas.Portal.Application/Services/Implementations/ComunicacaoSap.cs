using System;
using System.Collections.Generic;
using System.Linq;
using Progas.Portal.Application.Services.Contracts;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.DataAccess;
using Progas.Portal.Infra.Repositories.Contracts;
using SAP.Middleware.Connector;

namespace Progas.Portal.Application.Services.Implementations
{
    public class ComunicacaoSap : IComunicacaoSap
    {

        private readonly IMotivosDeRecusa _motivosDeRecusa;
        private readonly IRepositorioDeStatusDoPedidoDeVenda _statusDoPedidoDeVenda;

        public ComunicacaoSap(IMotivosDeRecusa motivosDeRecusa, IRepositorioDeStatusDoPedidoDeVenda statusDoPedidoDeVenda)
        {
            _motivosDeRecusa = motivosDeRecusa;
            _statusDoPedidoDeVenda = statusDoPedidoDeVenda;
        }

        public void EnviarPedido(PedidoVenda pedidoVenda)
        {
            var conexaoSap = new SapConnect();
            try
            {

                RfcDestination rfcDestination = conexaoSap.Conectar();
                RfcRepository rfcRepository = rfcDestination.Repository;

                IRfcFunction fReadTable = rfcRepository.CreateFunction("ZFXI_SD06");
                RfcStructureMetadata t_item = rfcRepository.GetStructureMetadata("ZSTSD007");
                IRfcTable envio_item = fReadTable.GetTable("TI_ITEM");
                RfcStructureMetadata t_condicoes = rfcRepository.GetStructureMetadata("ZSTSD008");
                IRfcTable envio_condicao = fReadTable.GetTable("TI_CONDICOES");

                IRfcStructure i_cabecalho = fReadTable.GetStructure("I_CABECALHO");

                fReadTable.SetValue("I_TIPO_ENVIO", pedidoVenda.Tipo);
                i_cabecalho.SetValue("COTACAO", pedidoVenda.Id_cotacao);
                i_cabecalho.SetValue("BSTKD", pedidoVenda.NumeroDoPedidoDoRepresentante);
                i_cabecalho.SetValue("AUART", pedidoVenda.TipoPedido);
                i_cabecalho.SetValue("WERKS", pedidoVenda.Id_centro);
                i_cabecalho.SetValue("VKORG", pedidoVenda.Id_centro);
                i_cabecalho.SetValue("VTWEG", pedidoVenda.AreaDeVenda.Can_dist);
                i_cabecalho.SetValue("SPART", pedidoVenda.AreaDeVenda.Set_ativ);
                i_cabecalho.SetValue("KUNNR", pedidoVenda.Cliente.Id_cliente);
                i_cabecalho.SetValue("ZTERM", pedidoVenda.Condpgto);
                i_cabecalho.SetValue("INCO1", pedidoVenda.Incoterm1.CodigoIncotermCab);
                i_cabecalho.SetValue("INCO2", pedidoVenda.Incoterm2.IncotermLinha);
                i_cabecalho.SetValue("TRANS",
                    pedidoVenda.Transportadora != null ? pedidoVenda.Transportadora.Codigo : null);
                i_cabecalho.SetValue("TRANSRED",
                    pedidoVenda.TransportadoraDeRedespacho != null
                        ? pedidoVenda.TransportadoraDeRedespacho.Codigo
                        : null);
                i_cabecalho.SetValue("TRANSREDCIF",
                    pedidoVenda.TransportadoraDeRedespachoCif != null
                        ? pedidoVenda.TransportadoraDeRedespachoCif.Codigo
                        : null);
                i_cabecalho.SetValue("REPRE", pedidoVenda.Id_repre);
                i_cabecalho.SetValue("OBSERVACAO", pedidoVenda.Observacao);


                for (int i = pedidoVenda.Itens.Count - 1; i >= 0; i--)
                {

                    var item = pedidoVenda.Itens[i];
                    IRfcStructure linha_envio_item = t_item.CreateStructure();

                    linha_envio_item.SetValue("POSNR", item.Numero);
                    linha_envio_item.SetValue("MATNR", item.Material.Id_material);
                    linha_envio_item.SetValue("MENGE", item.Quantidade);
                    linha_envio_item.SetValue("MEINS", item.Material.UnidadeDeMedida.Id_unidademedida);
                    linha_envio_item.SetValue("PLTYP", item.ListaDePreco.Codigo);

                    if (item.MotivoDeRecusa != null)
                    {
                        linha_envio_item.SetValue("ABGRU", item.MotivoDeRecusa.Codigo);
                    }


                    envio_item.Insert(linha_envio_item);

                    if (item.DescontoManual > 0)
                    {
                        IRfcStructure linha_envio_cond = t_condicoes.CreateStructure();

                        linha_envio_cond.SetValue("POSNR", item.Numero);
                        linha_envio_cond.SetValue("KBETR", item.DescontoManual);
                        linha_envio_cond.SetValue("KSCHL", "ZDEM");
                        envio_condicao.Insert(linha_envio_cond);
                    }
                }

                fReadTable.Invoke(rfcDestination);

                string statusDaComunicacao = fReadTable.GetString("E_STATUS");

                IRfcTable mensagensDeRetorno = fReadTable.GetTable("TE_MENSAGENS");

                if (statusDaComunicacao == "E")
                {
                    throw new Exception(string.Join(". ", mensagensDeRetorno.Select(m => m.GetString("MESSAGE"))));
                }

                string statusDaCotacao = fReadTable.GetString("E_STATUS_COTACAO");

                IRfcTable retornoItens = fReadTable.GetTable("TE_ITEM");

                IRfcStructure primeiroItem = retornoItens.First();

                StatusDoPedidoDeVenda statusDoPedidoDeVenda = _statusDoPedidoDeVenda.BuscaPorCodigo(statusDaCotacao).Single();

                if (statusDoPedidoDeVenda == null)
                {
                    throw new Exception("SAP retornou um status de Pedido inválido: " + statusDaComunicacao);
                }

                pedidoVenda.Alterar(primeiroItem.GetString("COTACAO"), statusDoPedidoDeVenda);

                IRfcTable retornoCondicoes = fReadTable.GetTable("TE_CONDICOES");

                string[] codigoDosMotivosDeRecusa = retornoItens
                    .Where(x => !string.IsNullOrEmpty(x.GetString("ABGRU")))
                    .Select(x => x.GetString("ABGRU")).Distinct().ToArray();

                IList<MotivoDeRecusa> motivosDeRecusa = _motivosDeRecusa.BuscarLista(codigoDosMotivosDeRecusa).List();


                for (int i = 0; i < retornoItens.Count; i++)
                {
                    IRfcStructure retornoItem = retornoItens[i];
                    PedidoVendaLinha itemDoPedido = pedidoVenda.Itens[i];

                    string codigoDoMotivoDeRecusa = retornoItem.GetString("ABGRU");
                    MotivoDeRecusa motivoDeRecusa = string.IsNullOrEmpty(codigoDoMotivoDeRecusa)
                        ? null
                        : motivosDeRecusa.Single(x => x.Codigo == codigoDoMotivoDeRecusa);

                    itemDoPedido.Alterar(retornoItem.GetString("POSNR"),
                        Convert.ToDecimal(retornoItem.GetString("VLRPOL")),
                        Convert.ToDecimal(retornoItem.GetString("VLRTAB")), motivoDeRecusa);

                    itemDoPedido.CondicoesDePreco.Clear();

                    foreach (var condicaoRetornada in
                        retornoCondicoes.Where(
                            condicao => condicao.GetString("POSNR") == retornoItem.GetString("POSNR")))
                    {
                        var condicaoDePreco = new CondicaoDePreco(condicaoRetornada.GetString("STUNR"),
                            condicaoRetornada.GetString("KSCHL"),
                            condicaoRetornada.GetDecimal("KAWRT"), condicaoRetornada.GetDecimal("KBETR"),
                            condicaoRetornada.GetDecimal("KWERT"));

                        itemDoPedido.AdicionarCondicao(condicaoDePreco);

                    }
                }

                // retornar apenas a lista de itens
                pedidoVenda.CalcularTotal();

            }
            finally
            {
                if (conexaoSap != null)
                {
                    RfcDestinationManager.UnregisterDestinationConfiguration(conexaoSap);
                }

            }

        }
    }
}