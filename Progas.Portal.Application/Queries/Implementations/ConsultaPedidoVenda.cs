using System;
using System.Linq;
using System.Collections.Generic;
using Progas.Portal.Application.Queries.Builders;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.ViewModel;
using NHibernate.Criterion;
//using StructureMap;

namespace Progas.Portal.Application.Queries.Implementations
{
    public class ConsultaPedidoVenda : IConsultaPedidoVenda
    {
        private readonly IPedidosVenda      _pedidosVenda;
        private readonly IPedidosVendaLinha _pedidosVendaLinha; 
        private readonly IBuilder<PedidoVenda, PedidoVendaCadastroVm> _builderPedidoVenda;
        private readonly IBuilder<PedidoVendaLinha, PedidoVendaLinhaCadastroVm> _builderPedidoVendaLinha;
        private readonly IUsuarios _usuarios;

        public ConsultaPedidoVenda( IPedidosVenda pedidosVenda,
                                    IPedidosVendaLinha pedidosVendaLinha,
                                    IBuilder<PedidoVenda, PedidoVendaCadastroVm> builderPedidoVenda,
                                    IBuilder<PedidoVendaLinha, PedidoVendaLinhaCadastroVm> builderPedidoVendaLinha,
                                    IUsuarios usuarios)
        {
            _pedidosVenda             = pedidosVenda;
            _pedidosVendaLinha        = pedidosVendaLinha;
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
                .CotacaoRepresentante(usuarioConectado.CodigoFornecedor.ToString());
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
            _pedidosVendaLinha.CotacaoPedidoContendo(cotacao); 

            var kendoGridVm = new KendoGridVm
            {
                QuantidadeDeRegistros = _pedidosVendaLinha.Count(),
                Registros = 
                            _builderPedidoVendaLinha.BuildList
                            (
                                _pedidosVendaLinha                                    
                                    .Skip(paginacaoVm.Skip)
                                   .Take(paginacaoVm.Take)                                   
                                   .List()
                            )
                            .Cast<ListagemVm>().ToList()
            };
            return kendoGridVm;
        }

        // Pesquisa do Botão Copiar( Cabecalho )
        public PedidoVendaCadastroVm ListarLinhasPedido(string id_cotacao)
        {
            var usuarioConectado = _usuarios.UsuarioConectado();

            return _builderPedidoVenda.BuildSingle(_pedidosVenda.CotacaoPedidoContendo(id_cotacao, usuarioConectado.CodigoFornecedor.ToString()));
        }

        // Pesquisa do Botão Copiar( Linhas )
        public IList<PedidoVendaLinhaCadastroVm> ListarLinhasCotacao(string cotacao)
        {
            //_pedidosVendaLinha.CotacaoPedidoContendo(cotacao);
           //return _builderPedidoVendaLinha.BuildList(_pedidosVendaLinha.List());
            //return _builderPedidoVendaLinha.BuildList(_pedidosVendaLinha.CotacaoPedidoContendo(cotacao).List());        
            var dados = _builderPedidoVendaLinha.BuildList(_pedidosVendaLinha.CotacaoPedidoContendo(cotacao).List());
            return dados.ToList();

        }
    }
}
