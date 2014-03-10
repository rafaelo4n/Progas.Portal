using System.Linq;
using System.Collections.Generic;
using Progas.Portal.Application.Queries.Builders;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Implementations
{
    public class ConsultaPedidoVenda : IConsultaPedidoVenda
    {
        private readonly IPedidosVenda      _pedidosVenda;
        private readonly IBuilder<PedidoVenda, PedidoVendaCadastroVm> _builderPedidoVenda;
        private readonly IBuilder<PedidoVendaLinha, PedidoVendaLinhaCadastroVm> _builderPedidoVendaLinha;
        private readonly IUsuarios _usuarios;

        public ConsultaPedidoVenda( IPedidosVenda pedidosVenda,
                                    IBuilder<PedidoVenda, PedidoVendaCadastroVm> builderPedidoVenda,
                                    IBuilder<PedidoVendaLinha, PedidoVendaLinhaCadastroVm> builderPedidoVendaLinha,
                                    IUsuarios usuarios)
        {
            _pedidosVenda             = pedidosVenda;
            _builderPedidoVenda       = builderPedidoVenda;
            _builderPedidoVendaLinha  = builderPedidoVendaLinha;
            _usuarios                 = usuarios;
        }

        // Pesquisa da tela de Listar Pedidos ( Cabecalho)
        public KendoGridVm Listar(PaginacaoVm paginacaoVm, PedidoVendaFiltroVm filtro)
        {
            var usuarioConectado = _usuarios.UsuarioConectado();

            _pedidosVenda
                .ClienteCodigoContendo(filtro.id_cliente)
                .DataCriacaoContendo(filtro.datacp)
                .PedidoCodigoContendo(filtro.id_pedido)
                .DataPedidoContendo(filtro.datap)
                .CotacaoRepresentante(usuarioConectado.CodigoDoFornecedor);
                //.NomeContendo(filtro.Nome);
            var kendoGridVmn = new KendoGridVm()
            {
                QuantidadeDeRegistros = _pedidosVenda.Count(),
                Registros =
                    _builderPedidoVenda.BuildList(_pedidosVenda.Skip(paginacaoVm.Skip).Take(paginacaoVm.Take).List())
                            .Cast<ListagemVm>()
                            .ToList()
            };
            return kendoGridVmn;
        }

        // Pesquisa da tela de Listar Pedidos ( Linhas) 
        public KendoGridVm ListarLinhasPedido(PaginacaoVm paginacaoVm, string cotacao)
        {            
            //_pedidosVendaLinha.CotacaoPedidoContendo(cotacao); 

            //var kendoGridVm = new KendoGridVm
            //{
            //    QuantidadeDeRegistros = _pedidosVendaLinha.Count(),
            //    Registros = 
            //                _builderPedidoVendaLinha.BuildList
            //                (
            //                    _pedidosVendaLinha                                    
            //                        .Skip(paginacaoVm.Skip)
            //                       .Take(paginacaoVm.Take)                                   
            //                       .List()
            //                )
            //                .Cast<ListagemVm>().ToList()
            //};
            //return kendoGridVm;

            return new KendoGridVm();
        }

        // Pesquisa do Botão Copiar( Cabecalho )
        public PedidoVendaCadastroVm ListarLinhasPedido(string id_cotacao)
        {
            var usuarioConectado = _usuarios.UsuarioConectado();

            return _builderPedidoVenda.BuildSingle(_pedidosVenda.CotacaoPedidoContendo(id_cotacao, usuarioConectado.CodigoDoFornecedor));
        }

        // Pesquisa do Botão Copiar( Linhas )
        public IList<PedidoVendaLinhaCadastroVm> ListarLinhasCotacao(string cotacao)
        {
            //_pedidosVendaLinha.CotacaoPedidoContendo(cotacao);
           //return _builderPedidoVendaLinha.BuildList(_pedidosVendaLinha.List());
            //return _builderPedidoVendaLinha.BuildList(_pedidosVendaLinha.CotacaoPedidoContendo(cotacao).List());        
            //var dados = _builderPedidoVendaLinha.BuildList(_pedidosVendaLinha.CotacaoPedidoContendo(cotacao).List());
            //return dados.ToList();

            return new List<PedidoVendaLinhaCadastroVm>();

        }

        public PedidoVendaCadastroVm Consultar(string idDoPedido)
        {
            IQueryable<PedidoVenda> queryable = _pedidosVenda
                .FiltraPorId(idDoPedido)
                .GetQuery();

            PedidoVendaCadastroVm pedidoVendaCadastroVm = queryable.Select(pedido => new PedidoVendaCadastroVm
            {
                id_cotacao = pedido.Id_cotacao,
                IdDaAreaDeVenda = pedido.AreaDeVenda.Id,
                Cliente = new ClienteDoPedidoDeVendaVm
                {
                  Id  = pedido.Cliente.Id ,
                  Codigo = pedido.Cliente.Id_cliente,
                  Nome = pedido.Cliente.Nome,
                  Cnpj =  pedido.Cliente.Cnpj ?? pedido.Cliente.Cpf,
                  Telefone = pedido.Cliente.Tel_res
                },
                CodigoTipoPedido = pedido.TipoPedido ,
                Transportadora = pedido.Transportadora != null 
                ? new TransportadoraDoPedidoDeVenda
                {
                    Id = pedido.Transportadora.Id,
                    Codigo = pedido.Transportadora.Codigo,
                    Nome = pedido.Transportadora.Nome
                }
                : null,

                TransportadoraDeRedespacho = pedido.TransportadoraDeRedespacho != null
                ?new TransportadoraDoPedidoDeVenda
                {
                    Id = pedido.TransportadoraDeRedespacho.Id,
                    Codigo = pedido.TransportadoraDeRedespacho.Codigo,
                    Nome =  pedido.TransportadoraDeRedespacho.Nome
                }
                : null,

                TransportadoraDeRedespachoCif = pedido.TransportadoraDeRedespachoCif != null 
                ?  new TransportadoraDoPedidoDeVenda
                {
                    Id = pedido.TransportadoraDeRedespachoCif.Id,
                    Codigo = pedido.TransportadoraDeRedespachoCif.Codigo,
                    Nome = pedido.TransportadoraDeRedespachoCif.Nome
                }
                : null,
                condpgto = pedido.Condpgto,
                datacp = pedido.Datacp.ToShortDateString(),
                datap = pedido.Datap.ToShortDateString(),
                id_centro = pedido.Id_centro ,
                id_pedido = pedido.Id_pedido ,
                id_repre =pedido.Id_repre ,
                inco1 = pedido.Inco1,
                inco2 = pedido.Inco2 ,
                vlrtot = pedido.Vlrtot,
                obs = pedido.Obs

            }).Single();

            return pedidoVendaCadastroVm;

        }
    }
}
