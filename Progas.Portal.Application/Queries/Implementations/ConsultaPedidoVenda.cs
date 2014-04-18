using System;
using System.Linq;
using System.Collections.Generic;
using Progas.Portal.Application.Queries.Builders;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.Domain.Entities;
using Progas.Portal.DTO;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Implementations
{
    public class ConsultaPedidoVenda : IConsultaPedidoVenda
    {
        private readonly IPedidosVenda _pedidosVenda;
        private readonly IUsuarios _usuarios;

        public ConsultaPedidoVenda(IPedidosVenda pedidosVenda, IUsuarios usuarios)
        {
            _pedidosVenda = pedidosVenda;
            _usuarios = usuarios;
        }

        // Pesquisa da tela de Listar Pedidos ( Cabecalho)
        public KendoGridVm Listar(PaginacaoVm paginacaoVm, PedidoVendaFiltroVm filtro)
        {
            var usuarioConectado = _usuarios.UsuarioConectado();

            if (!string.IsNullOrEmpty(filtro.id_cliente))
            {
                _pedidosVenda.DoCliente(filtro.id_cliente);

            }

            if (filtro.IdDoMaterial.HasValue)
            {
                _pedidosVenda.ContendoMaterial(filtro.IdDoMaterial.Value);
            }

            _pedidosVenda
                .DataCriacaoContendo(filtro.datacp)
                .PedidoCodigoContendo(filtro.id_pedido)
                .DataPedidoContendo(filtro.datap)
                .CotacaoRepresentante(usuarioConectado.CodigoDoFornecedor);


            var kendoGridVmn = new KendoGridVm()
            {
                QuantidadeDeRegistros = _pedidosVenda.Count(),
                Registros = _pedidosVenda.GetQuery().Select(pedido =>
                    new PedidoVendaListagemVm
                    {
                        IdDaCotacao = pedido.Id_cotacao,
                        NumeroDoPedido = pedido.NumeroDoPedidoDoRepresentante,
                        DataDeCriacao =  pedido.Datacp.ToString("dd/MM/yyyy HH:mm:ss"),
                        DataDoPedido =  pedido.Datap.ToShortDateString(),
                        NomeDoCliente =  pedido.Cliente.Nome,
                        ValorTotal = pedido.ValorTotal
                    })
                    .Cast<ListagemVm>()
                    .ToList()
            };
            
            return kendoGridVmn;
        }

        public IList<PedidoVendaLinhaCadastroVm> ListarItensDoPedido(string idDoPedido)
        {
            IQueryable<PedidoVenda> queryable = _pedidosVenda.FiltraPorId(idDoPedido).GetQuery();

            List<PedidoVendaLinha> itens = (from pedido in queryable
                from item in pedido.Itens
                select item).ToList();

            List<PedidoVendaLinhaCadastroVm> linhas = itens.Select(item => new PedidoVendaLinhaCadastroVm()
            {
                Id = item.Id,
                NumeroDoItem = item.Numero,
                IdMaterial = item.Material.pro_id_material,
                CodigoMaterial = item.Material.Id_material,
                DescricaoMaterial = item.Material.Descricao,
                Quantidade = item.Quantidade,
                CodigoUnidadeMedida = item.Material.UnidadeDeMedida.Id_unidademedida + " - " + item.Material.UnidadeDeMedida.Descricao,
                CodigoListaPreco = item.ListaDePreco.Codigo,
                DescricaoListaPreco = item.ListaDePreco.Descricao,
                Desconto = item.DescontoManual,
                ValorPolitica = item.ValorPolitica,
                ValorTabela =  item.ValorTabela,
                CodigoDoMotivoDeRecusa = item.MotivoDeRecusa != null ? item.MotivoDeRecusa.Codigo : null,
                DescricaoDoMotivoDeRecusa = item.MotivoDeRecusa != null ? item.MotivoDeRecusa.Descricao : null,
                CondicoesDePreco = item.CondicoesDePreco.Select(condicaoDePreco => new CondicaoDePrecoDTO
                {
                    Nivel = condicaoDePreco.Nivel,
                    Tipo = condicaoDePreco.Tipo,
                    Base = condicaoDePreco.Base,
                    Montante = condicaoDePreco.Montante,
                    Valor = condicaoDePreco.Valor

                })

            }).ToList();

            return linhas;

        }

        public PedidoVendaCadastroVm Consultar(string idDoPedido)
        {
            IQueryable<PedidoVenda> queryable = _pedidosVenda
                .FiltraPorId(idDoPedido)
                .GetQuery();

            PedidoVendaCadastroVm pedidoVendaCadastroVm = queryable.Select(pedido => new PedidoVendaCadastroVm
            {
                id_cotacao = pedido.Id_cotacao,
                status = pedido.Status.Descricao,
                IdDaAreaDeVenda = pedido.AreaDeVenda.Id,
                Cliente = new ClienteDoPedidoDeVendaVm
                {
                  Codigo = pedido.Cliente.Id_cliente,
                  Nome = pedido.Cliente.Nome,
                  Cnpj =  pedido.Cliente.Cnpj ?? pedido.Cliente.Cpf,
                  Telefone = pedido.Cliente.Tel_res
                },
                CodigoTipoPedido = pedido.TipoPedido ,
                Transportadora = pedido.Transportadora != null 
                ? new TransportadoraDoPedidoDeVenda
                {
                    Codigo = pedido.Transportadora.Codigo,
                    Nome = pedido.Transportadora.Nome
                }
                : null,

                TransportadoraDeRedespacho = pedido.TransportadoraDeRedespacho != null
                ?new TransportadoraDoPedidoDeVenda
                {
                    Codigo = pedido.TransportadoraDeRedespacho.Codigo,
                    Nome =  pedido.TransportadoraDeRedespacho.Nome
                }
                : null,

                TransportadoraDeRedespachoCif = pedido.TransportadoraDeRedespachoCif != null 
                ?  new TransportadoraDoPedidoDeVenda
                {
                    Codigo = pedido.TransportadoraDeRedespachoCif.Codigo,
                    Nome = pedido.TransportadoraDeRedespachoCif.Nome
                }
                : null,
                condpgto = pedido.Condpgto,
                datacp = pedido.Datacp.ToShortDateString(),
                datap = pedido.Datap.ToShortDateString(),
                id_centro = pedido.Id_centro ,
                id_pedido = pedido.NumeroDoPedidoDoRepresentante ,
                id_repre =pedido.Id_repre ,
                IdDoIncoterm1 = Convert.ToString(pedido.Incoterm1.pro_id_incotermCab),
                IdDoIncoterm2 = Convert.ToString(pedido.Incoterm2.pro_id_incotermLinha),
                vlrtot = pedido.ValorTotal,
                obs = pedido.Observacao

            }).Single();

            return pedidoVendaCadastroVm;

        }

        public bool PedidoExiste(string idDoPedido)
        {
            int count = _pedidosVenda.FiltraPorId(idDoPedido).Count();

            return count == 1;
        }

        public KendoGridVm ListarCondicoesDePreco(PedidoVendaLinhaChaveVm item)
        {
            IQueryable<PedidoVenda> queryable = _pedidosVenda.FiltraPorId(item.IdDoPedido).GetQuery();

            var condicoesDePreco = (from pedido in queryable
                from itemDoPedido in pedido.Itens
                from condicaoDePreco in itemDoPedido.CondicoesDePreco
                where itemDoPedido.Id == item.IdDoItem
                select new CondicaoDePrecoDTO
                {
                    Nivel = condicaoDePreco.Nivel,
                    Tipo = condicaoDePreco.Tipo,
                    Base = condicaoDePreco.Base,
                    Montante = condicaoDePreco.Montante,
                    Valor = condicaoDePreco.Valor
                }).Cast<ListagemVm>().ToList();

            return new KendoGridVm
            {
                QuantidadeDeRegistros = condicoesDePreco.Count,
                Registros = condicoesDePreco
            };


        }
    }
}
