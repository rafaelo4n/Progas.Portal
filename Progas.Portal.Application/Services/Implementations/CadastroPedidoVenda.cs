using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using Progas.Portal.Application.Services.Contracts;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.ViewModel;
using SAP.Middleware.Connector;

namespace Progas.Portal.Application.Services.Implementations
{
    public class CadastroPedidoVenda : ICadastroPedidoVenda
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPedidosVenda _pedidosVenda;
        private readonly IUsuarios _usuarios;
        private readonly IClienteVendas _clienteVendas;
        private readonly IClientes _clientes ;
        private readonly IMateriais _materiais;
        private readonly IFornecedores _fornecedores;

        public CadastroPedidoVenda(IUnitOfWork unitOfWork, IPedidosVenda pedidosVenda, 
            IUsuarios usuarios, IClienteVendas clienteVendas, IMateriais materiais, IClientes clientes, IFornecedores fornecedores)
        {
            _unitOfWork = unitOfWork;
            _pedidosVenda = pedidosVenda;
            _usuarios = usuarios;
            _clienteVendas = clienteVendas;
            _materiais = materiais;
            _clientes = clientes;
            _fornecedores = fornecedores;
        }               
        // Parametro de Repositorio utilizado na conexao SAP
        public static string RepositorydestinationPar = ConfigurationSettings.AppSettings["RepositoryDestination"];

        private class TransportadorasDoPedido
        {
            public Fornecedor Transportadora { get; set; }
            public Fornecedor TransportadoraDeRedespacho { get; set; }
            public Fornecedor TransportadoraDeRedespachoCif { get; set; }
            
        }

        private TransportadorasDoPedido ConsultarTransportadoras(PedidoVendaSalvarVm pedido)
        {
            var idsDasTransportadoras = new List<int>();

            if (pedido.CodigoDaTransportadora.HasValue)
            {
                idsDasTransportadoras.Add(pedido.CodigoDaTransportadora.Value);
            }

            if (pedido.CodigoDaTransportadoraDeRedespacho.HasValue)
            {
                idsDasTransportadoras.Add(pedido.CodigoDaTransportadoraDeRedespacho.Value);
            }

            if (pedido.CodigoDaTransportadoraDeRedespachoCif.HasValue)
            {
                idsDasTransportadoras.Add(pedido.CodigoDaTransportadoraDeRedespachoCif.Value);
            }

            IList<Fornecedor> transportadoras = _fornecedores.BuscaListaPorIds(idsDasTransportadoras).List();

            var retorno = new TransportadorasDoPedido
            {
                Transportadora = pedido.CodigoDaTransportadora.HasValue
                    ? transportadoras.Single(x => x.Id == pedido.CodigoDaTransportadora.Value)
                    : null,
                TransportadoraDeRedespacho = pedido.CodigoDaTransportadoraDeRedespacho.HasValue
                    ? transportadoras.Single(x => x.Id == pedido.CodigoDaTransportadoraDeRedespacho.Value)
                    : null,
                TransportadoraDeRedespachoCif = pedido.CodigoDaTransportadoraDeRedespachoCif.HasValue
                    ? transportadoras.Single(x => x.Id == pedido.CodigoDaTransportadoraDeRedespachoCif.Value)
                    : null
            };

            return retorno;

        }

        public void Salvar(PedidoVendaSalvarVm pedido)
        {

            SAPConnect con = null;
            try
            {
                con = new SAPConnect();
                con.GetParameters("DEV");

                RfcDestinationManager.RegisterDestinationConfiguration(con);
                RfcDestination dest = RfcDestinationManager.GetDestination(RepositorydestinationPar);
                RfcRepository repo = dest.Repository;
                IRfcFunction fReadTable = repo.CreateFunction("ZFXI_SD06");
                IRfcStructure i_cabecalho = fReadTable.GetStructure("I_CABECALHO");
                RfcStructureMetadata t_item = repo.GetStructureMetadata("ZSTSD007");
                IRfcStructure linha_envio_item = t_item.CreateStructure();
                IRfcTable envio_item = fReadTable.GetTable("TI_ITEM");
                RfcStructureMetadata t_condicoes = repo.GetStructureMetadata("ZSTSD008");
                IRfcStructure linha_envio_cond = t_condicoes.CreateStructure();
                IRfcTable envio_condicao = fReadTable.GetTable("TI_CONDICOES");

                var usuarioConectado = _usuarios.UsuarioConectado();

                //ClienteVenda clienteVendas = _clienteVendas.ConsultaAtivDistribuicao(pedido.Cliente, pedido.Centro);
                ClienteVenda clienteVendas = _clienteVendas.ObterPorId(pedido.IdDaAreaDeVenda);

                fReadTable.SetValue("I_TIPO_ENVIO", pedido.Tipo);
                i_cabecalho.SetValue("BSTKD", pedido.NumeroPedido);
                i_cabecalho.SetValue("AUART", pedido.CodigoTipoPedido);
                i_cabecalho.SetValue("WERKS", pedido.Centro);
                i_cabecalho.SetValue("VKORG", pedido.Centro);
                i_cabecalho.SetValue("VTWEG", Convert.ToString(clienteVendas.Can_dist));
                i_cabecalho.SetValue("SPART", Convert.ToString(clienteVendas.Set_ativ));
                i_cabecalho.SetValue("KUNNR", pedido.IdDoCliente);
                i_cabecalho.SetValue("ZTERM", pedido.CodigoDaCondicaoDePagamento);
                i_cabecalho.SetValue("INCO1", pedido.Inco1);
                i_cabecalho.SetValue("INCO2", pedido.Inco2);
                i_cabecalho.SetValue("TRANS", pedido.CodigoDaTransportadora);
                i_cabecalho.SetValue("TRANSRED", pedido.CodigoDaTransportadoraDeRedespacho);
                i_cabecalho.SetValue("TRANSREDCIF", pedido.CodigoDaTransportadoraDeRedespachoCif);
                i_cabecalho.SetValue("REPRE", Convert.ToString(usuarioConectado.CodigoDoFornecedor));
                i_cabecalho.SetValue("OBSERVACAO", pedido.Observacao);

                int[] idDosMateriais = pedido.Itens.Select(x => x.IdMaterial).Distinct().ToArray();

                IList<Material> materiaisDosItens = _materiais.BuscarLista(idDosMateriais).List();

                int contadorDeItens = 1;

                foreach (var dados in pedido.Itens)
                {
                    Material material = materiaisDosItens.Single(x => x.pro_id_material == dados.IdMaterial);

                    // LINHAS (Estrutura tipo tabela)
                    linha_envio_item.SetValue("POSNR", contadorDeItens);
                    linha_envio_item.SetValue("MATNR", dados.IdMaterial);
                    linha_envio_item.SetValue("MENGE", dados.Quantidade);
                    linha_envio_item.SetValue("MEINS", material.Uni_med);
                    linha_envio_item.SetValue("PLTYP", dados.CodigoDaListaDePreco);
                    envio_item.Insert(linha_envio_item);
                    
                    // CONDICAO (Estrutura tipo tabela) 
                    if (dados.Desconto > 0)
                    {
                        linha_envio_cond.SetValue("POSNR", contadorDeItens);
                        linha_envio_cond.SetValue("KWERT", dados.Desconto);
                        linha_envio_cond.SetValue("KSCHL", "ZDEM");
                        envio_condicao.Insert(linha_envio_cond);
                    }
                    // Apos inserir o ultimo item na estrutura do SAP, realiza a chamada da Função e salva para o tipo Gravação e retornar as linhas.
                    contadorDeItens++;
                }

                fReadTable.Invoke(dest);

                string status = fReadTable.GetString("E_STATUS");

                if (status == "E")
                {
                    throw new Exception("Ocorreu um erro ao enviar um Pedido para o SAP.");
                }

                IRfcTable retornoItens = fReadTable.GetTable("TE_ITEM");
                //IRfcStructure primeiroItemDoRetorno = retornoItens.FirstOrDefault();

                IRfcTable retornoCondicoes = fReadTable.GetTable("TE_CONDICOES");

                Cliente cliente = _clientes.BuscaPeloId(pedido.IdDoCliente).Single();

                TransportadorasDoPedido transportadorasDoPedido = ConsultarTransportadoras(pedido);


                var pedidoVenda = new PedidoVenda(pedido.Tipo,
                    //primeiroItemDoRetorno.GetString("COTACAO"),
                    fReadTable.GetString("COTACAO"),
                    pedido.CodigoTipoPedido,
                    pedido.Centro,
                    cliente,
                    clienteVendas, 
                    DateTime.Now,
                    pedido.NumeroPedido,
                    pedido.DataDoPedido,
                    pedido.CodigoDaCondicaoDePagamento,
                    pedido.Inco1,
                    pedido.Inco2,
                    transportadorasDoPedido.Transportadora,
                    transportadorasDoPedido.TransportadoraDeRedespacho,
                    transportadorasDoPedido.TransportadoraDeRedespachoCif,
                    Convert.ToString(usuarioConectado.CodigoDoFornecedor),
                    pedido.Observacao,
                    status);


                for (int i = 0; i < retornoItens.Count; i++)
                {
                    IRfcStructure retornoItem = retornoItens[i];
                    PedidoVendaSalvarItemVm item = pedido.Itens[i];

                    Material material = materiaisDosItens.Single(x => x.pro_id_material == item.IdMaterial);

                    var linhasPedido = new PedidoVendaLinha(//retornoItem.GetString("COTACAO"),
                        retornoItem.GetString("POSNR"),
                        pedido.NumeroPedido,
                        material, 
                        item.Quantidade,
                        item.CodigoDaListaDePreco,
                        Convert.ToDecimal(retornoItem.GetString("VLRTAB")), // valtab
                        Convert.ToDecimal(retornoItem.GetString("VLRPOL")), // valpol
                        item.Desconto,
                        retornoItem.GetString("ABGRU") // motivo de recusa
                        );

                    foreach (var condicaoRetornada in
                            retornoCondicoes.Where(
                                condicao => condicao.GetString("POSNR") == retornoItem.GetString("POSNR")))
                    {
                        var condicaoDePreco = new CondicaoDePreco(condicaoRetornada.GetString("STUNR"), condicaoRetornada.GetString("KSCHL"),
                            condicaoRetornada.GetDecimal("KAWRT"), condicaoRetornada.GetDecimal("KBETR"), condicaoRetornada.GetDecimal("KWERT"));

                        linhasPedido.AdicionarCondicao(condicaoDePreco);

                    }

                    pedidoVenda.AdicionarItem(linhasPedido);
                }
                // retornar apenas a lista de itens                                

                if (pedido.Tipo == "G")
                {
                    using (_unitOfWork)
                    {
                        _unitOfWork.BeginTransaction();

                        _pedidosVenda.Save(pedidoVenda);

                        _unitOfWork.Commit();
                        
                    }
                }


            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }
            finally
            {
                if (con != null)
                {
                    RfcDestinationManager.UnregisterDestinationConfiguration(con);
                }
            }

        }

        // Realiza a Conexão no SAP conforme os dados do arquivo Web.config.
        public class SAPConnect : IDestinationConfiguration
        {
            // Dados de Parametros do Web.config
            public static string appserverhost = ConfigurationSettings.AppSettings["AppServerHost"];
            public static string saprouter = ConfigurationSettings.AppSettings["SAPRouter"];
            public static string systemnumber = ConfigurationSettings.AppSettings["SystemNumber"];
            public static string systemid = ConfigurationSettings.AppSettings["SystemID"];
            public static string user = ConfigurationSettings.AppSettings["User"];
            public static string password = ConfigurationSettings.AppSettings["Password"];
            public static string client = ConfigurationSettings.AppSettings["Client"];
            public static string poolsize = ConfigurationSettings.AppSettings["PoolSize"];
            public static string repositorydestination = ConfigurationSettings.AppSettings["RepositoryDestination"];

            public RfcConfigParameters GetParameters(String destinationName)
            {
                if (repositorydestination == destinationName)
                {
                    RfcConfigParameters parms = new RfcConfigParameters();
                    parms.Add(RfcConfigParameters.AppServerHost, appserverhost);
                    parms.Add(RfcConfigParameters.SAPRouter, saprouter);
                    parms.Add(RfcConfigParameters.SystemNumber, systemnumber);
                    parms.Add(RfcConfigParameters.SystemID, systemid);
                    parms.Add(RfcConfigParameters.User, user);
                    parms.Add(RfcConfigParameters.Password, password);
                    parms.Add(RfcConfigParameters.Client, client);
                    parms.Add(RfcConfigParameters.PoolSize, poolsize);
                    return parms;
                }
                else return null;
            }

            // The following two are not used in this example:
            public bool ChangeEventsSupported()
            {
                return false;
            }

            public event RfcDestinationManager.ConfigurationChangeHandler
                ConfigurationChanged;
        }
    }
}
