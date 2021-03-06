﻿using System; 
using System.Collections.Generic;
using System.Linq;
using Progas.Portal.Application.Services.Contracts;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Domain.Services.Contracts;
using Progas.Portal.DTO;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.ViewModel;

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
        private readonly IIncotermsCabs _incotermsCabs;
        private readonly IIncotermsLinhas _incotermsLinhas;
        private readonly ICondicoesDePagamento _condicoesDePagamento;
        private readonly IListasPreco _listasPreco;
        private readonly IMotivosDeRecusa _motivosDeRecusa;
        private readonly IComunicacaoSap _comunicacaoSap;
        private readonly IAtualizadorDeItensDoPedidoDeVenda _atualizadorDeItens;

        public CadastroPedidoVenda(IUnitOfWork unitOfWork, IPedidosVenda pedidosVenda, 
            IUsuarios usuarios, IClienteVendas clienteVendas, IMateriais materiais, IClientes clientes,
            IFornecedores fornecedores, IIncotermsCabs incotermsCabs, IIncotermsLinhas incotermsLinhas, 
            IListasPreco listasPreco, IComunicacaoSap comunicacaoSap, IMotivosDeRecusa motivosDeRecusa, 
            IAtualizadorDeItensDoPedidoDeVenda atualizadorDeItens, ICondicoesDePagamento condicoesDePagamento)
        {
            _unitOfWork = unitOfWork;
            _pedidosVenda = pedidosVenda;
            _usuarios = usuarios;
            _clienteVendas = clienteVendas;
            _materiais = materiais;
            _clientes = clientes;
            _fornecedores = fornecedores;
            _incotermsCabs = incotermsCabs;
            _incotermsLinhas = incotermsLinhas;
            _listasPreco = listasPreco;
            _comunicacaoSap = comunicacaoSap;
            _motivosDeRecusa = motivosDeRecusa;
            _atualizadorDeItens = atualizadorDeItens;
            _condicoesDePagamento = condicoesDePagamento;
        }               

        private class TransportadorasDoPedido
        {
            public Fornecedor Transportadora { get; set; }
            public Fornecedor TransportadoraDeRedespacho { get; set; }
            public Fornecedor TransportadoraDeRedespachoCif { get; set; }
            
        }

        private TransportadorasDoPedido ConsultarTransportadoras(PedidoVendaSalvarVm pedido)
        {
            var codigosDasTransportadoras = new List<string>();

            if (!string.IsNullOrEmpty(pedido.CodigoDaTransportadora))
            {
                codigosDasTransportadoras.Add(pedido.CodigoDaTransportadora);
            }

            if (!string.IsNullOrEmpty(pedido.CodigoDaTransportadoraDeRedespacho))
            {
                codigosDasTransportadoras.Add(pedido.CodigoDaTransportadoraDeRedespacho);
            }

            if (!string.IsNullOrEmpty(pedido.CodigoDaTransportadoraDeRedespachoCif))
            {
                codigosDasTransportadoras.Add(pedido.CodigoDaTransportadoraDeRedespachoCif);
            }

            IList<Fornecedor> transportadoras = _fornecedores.BuscaListaPorCodigo(codigosDasTransportadoras.ToArray()).List();

            var retorno = new TransportadorasDoPedido
            {
                Transportadora = !string.IsNullOrEmpty(pedido.CodigoDaTransportadora) 
                    ? transportadoras.Single(x => x.Codigo == pedido.CodigoDaTransportadora)
                    : null,
                TransportadoraDeRedespacho = !string.IsNullOrEmpty(pedido.CodigoDaTransportadoraDeRedespacho)
                    ? transportadoras.Single(x => x.Codigo == pedido.CodigoDaTransportadoraDeRedespacho)
                    : null,
                TransportadoraDeRedespachoCif = !string.IsNullOrEmpty(pedido.CodigoDaTransportadoraDeRedespachoCif)
                    ? transportadoras.Single(x => x.Codigo == pedido.CodigoDaTransportadoraDeRedespachoCif)
                    : null
            };

            return retorno;

        }

        public PedidoSapRetornoDTO Salvar(PedidoVendaSalvarVm pedido)
        {

            try
            {

                var usuarioConectado = _usuarios.UsuarioConectado();

                ClienteVenda clienteVenda = _clienteVendas.ObterPorId(pedido.IdDaAreaDeVenda);

                TransportadorasDoPedido transportadorasDoPedido = ConsultarTransportadoras(pedido);

                IncotermCab incoterm1 = _incotermsCabs.FiltraPorId(pedido.IdDoIncoterm1).Single();

                IncotermLinha incoterm2 = _incotermsLinhas.FiltraPorId(pedido.IdDoIncoterm2).Single();

                Cliente cliente = _clientes.BuscaPeloCodigo(pedido.CodigoDoCliente).Single();

                CondicaoDePagamento condicaoDePagamento = _condicoesDePagamento.BuscaPeloCodigo(pedido.CodigoDaCondicaoDePagamento);

                PedidoVenda pedidoVenda;

                if (!string.IsNullOrEmpty(pedido.IdDaCotacao))
                {
                    pedidoVenda = _pedidosVenda.FiltraPorId(pedido.IdDaCotacao).Single();

                    pedidoVenda
                        .AlterarTipo(pedido.Tipo)
                        .AlterarCliente(clienteVenda, cliente)
                        .AlterarIncoterm(incoterm1, incoterm2)
                        .AlterarTransportadora(transportadorasDoPedido.Transportadora, transportadorasDoPedido.TransportadoraDeRedespacho, transportadorasDoPedido.TransportadoraDeRedespachoCif)
                        .AlterarDados(pedido.NumeroPedidoDoRepresentante, pedido.NumeroPedidoDoCliente, pedido.DataDoPedido, condicaoDePagamento, pedido.Observacao);

                }
                else
                {

                    pedidoVenda = new PedidoVenda(pedido.Tipo,
                        pedido.CodigoTipoPedido,
                        clienteVenda.Org_vendas,
                        cliente,
                        clienteVenda,
                        DateTime.Now,
                        pedido.NumeroPedidoDoRepresentante,
                        pedido.NumeroPedidoDoCliente,
                        pedido.DataDoPedido,
                        condicaoDePagamento, 
                        incoterm1,
                        incoterm2,
                        transportadorasDoPedido.Transportadora,
                        transportadorasDoPedido.TransportadoraDeRedespacho,
                        transportadorasDoPedido.TransportadoraDeRedespachoCif,
                        usuarioConectado.Fornecedor,
                        pedido.Observacao
                        );
                }

                int[] idDosMateriais = pedido.Itens.Select(x => x.IdMaterial).Distinct().ToArray();

                IList<Material> materiaisDosItens = _materiais.BuscarLista(idDosMateriais).List();

                string[] codigoDasListasDePreco = pedido.Itens.Select(item => item.CodigoDaListaDePreco).Distinct().ToArray();

                IList<ListaPreco> listasDePreco = _listasPreco.FiltraPorListaDeCodigos(codigoDasListasDePreco).List();

                string[] codigoDosMotivosDeRecusa = pedido.Itens
                    .Where(item => !string.IsNullOrEmpty(item.CodigoDoMotivoDeRecusa))
                    .Select(item => item.CodigoDoMotivoDeRecusa)
                    .ToArray();
                
                IList<MotivoDeRecusa> motivosDeRecusaDosItens = _motivosDeRecusa.BuscarLista(codigoDosMotivosDeRecusa).List();

                if (!string.IsNullOrEmpty(pedido.IdDaCotacao))
                {
                    _atualizadorDeItens.Atualizar(pedidoVenda, pedido);
                }
                else
                {
                    for (int i = 0; i < pedido.Itens.Count; i++)
                    {
                        PedidoVendaSalvarItemVm item = pedido.Itens[i];

                        Material material = materiaisDosItens.Single(x => x.pro_id_material == item.IdMaterial);

                        ListaPreco listaDePreco = listasDePreco.Single(lista => lista.Codigo == item.CodigoDaListaDePreco);

                        MotivoDeRecusa motivoDeRecusa = string.IsNullOrEmpty(item.CodigoDoMotivoDeRecusa)
                            ? null
                            : motivosDeRecusaDosItens.Single(m => m.Codigo == item.CodigoDoMotivoDeRecusa);

                        var linhasPedido = new PedidoVendaLinha(item.Numero, material, item.Quantidade, listaDePreco, item.Desconto, motivoDeRecusa);

                        pedidoVenda.AdicionarItem(linhasPedido);
                    }
                    
                }


                _comunicacaoSap.EnviarPedido(pedidoVenda);

                if (pedido.Tipo == "G")
                {
                    using (_unitOfWork)
                    {
                        _unitOfWork.BeginTransaction();

                        _pedidosVenda.Save(pedidoVenda);

                        _unitOfWork.Commit();
                        
                    }
                }

                var dtoDeRetorno = new PedidoSapRetornoDTO
                {
                    IdDoPedido = pedidoVenda.Id_cotacao,
                    Status = pedidoVenda.Status.Descricao,
                    ValorTotal = pedidoVenda.ValorTotal,
                    Itens = pedidoVenda.Itens.Select(item => new PedidoSapItemRetornoDTO
                    {
                        Id = item.Id,
                        NumeroDoItem = item.Numero,
                        ValorDeTabela = item.ValorTabela,
                        ValorPolitica = item.ValorPolitica,
                        CodigoDoMotivoDeRecusa = item.MotivoDeRecusa != null ? item.MotivoDeRecusa.Codigo : null,
                        DescricaoDoMotivoDeRecusa = item.MotivoDeRecusa != null ? item.MotivoDeRecusa.Descricao: null,
                        CondicoesDePreco = item.CondicoesDePreco.Select(condicaoDePreco => new CondicaoDePrecoDTO
                        {
                            Nivel = condicaoDePreco.Nivel,
                            Descricao = condicaoDePreco.Descricao,
                            Tipo = condicaoDePreco.Tipo,
                            Valor = condicaoDePreco.Valor
                        }).ToList()
                    }).ToList()
                };

                return dtoDeRetorno;

            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }

        }

    }
}
