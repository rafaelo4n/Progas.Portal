using System; 
using System.Collections.Generic;
using System.Linq;
using NHibernate.Hql;
using Progas.Portal.Application.Services.Contracts;
using Progas.Portal.Domain.Entities;
using Progas.Portal.DTO;
using Progas.Portal.Infra.DataAccess;
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
        private readonly IIncotermsCabs _incotermsCabs;
        private readonly IIncotermsLinhas _incotermsLinhas;
        private readonly IListasPreco _listasPreco;
        private readonly IMotivosDeRecusa _motivosDeRecusa;
        private readonly IComunicacaoSap _comunicacaoSap;

        public CadastroPedidoVenda(IUnitOfWork unitOfWork, IPedidosVenda pedidosVenda, 
            IUsuarios usuarios, IClienteVendas clienteVendas, IMateriais materiais, IClientes clientes,
            IFornecedores fornecedores, IIncotermsCabs incotermsCabs, IIncotermsLinhas incotermsLinhas, 
            IListasPreco listasPreco, IComunicacaoSap comunicacaoSap, IMotivosDeRecusa motivosDeRecusa)
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
        }               

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

        public PedidoSapRetornoDTO Salvar(PedidoVendaSalvarVm pedido)
        {

            try
            {

                var usuarioConectado = _usuarios.UsuarioConectado();

                ClienteVenda clienteVendas = _clienteVendas.ObterPorId(pedido.IdDaAreaDeVenda);

                TransportadorasDoPedido transportadorasDoPedido = ConsultarTransportadoras(pedido);

                IncotermCab incoterm1 = _incotermsCabs.FiltraPorId(pedido.IdDoIncoterm1).Single();

                IncotermLinhas incoterm2 = _incotermsLinhas.FiltraPorId(pedido.IdDoIncoterm2).Single();

                Cliente cliente = _clientes.BuscaPeloId(pedido.IdDoCliente).Single();

                PedidoVenda pedidoVenda;

                if (string.IsNullOrEmpty(pedido.IdDaCotacao))
                {
                    pedidoVenda = _pedidosVenda.FiltraPorId(pedido.IdDaCotacao).Single();

                }
                else
                {

                    pedidoVenda = new PedidoVenda(pedido.Tipo,
                        //fReadTable.GetString("COTACAO"),
                        pedido.CodigoTipoPedido,
                        pedido.Centro,
                        cliente,
                        clienteVendas,
                        DateTime.Now,
                        pedido.NumeroPedido,
                        pedido.DataDoPedido,
                        pedido.CodigoDaCondicaoDePagamento,
                        incoterm1,
                        incoterm2,
                        transportadorasDoPedido.Transportadora,
                        transportadorasDoPedido.TransportadoraDeRedespacho,
                        transportadorasDoPedido.TransportadoraDeRedespachoCif,
                        Convert.ToString(usuarioConectado.CodigoDoFornecedor),
                        pedido.Observacao
                        //status
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

                for (int i = 0; i < pedido.Itens.Count; i++)
                {
                    PedidoVendaSalvarItemVm item = pedido.Itens[i];

                    Material material = materiaisDosItens.Single(x => x.pro_id_material == item.IdMaterial);

                    ListaPreco listaDePreco = listasDePreco.Single(lista => lista.Codigo == item.CodigoDaListaDePreco);

                    MotivoDeRecusa motivoDeRecusa = string.IsNullOrEmpty(item.CodigoDoMotivoDeRecusa)
                        ? null
                        : motivosDeRecusaDosItens.Single(m => m.Codigo == item.CodigoDoMotivoDeRecusa);

                    var linhasPedido = new PedidoVendaLinha(material,item.Quantidade,listaDePreco,item.Desconto,motivoDeRecusa);
                    
                    pedidoVenda.AdicionarItem(linhasPedido);
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
                    Status = pedidoVenda.Status,
                    ValorTotal = pedidoVenda.ValorTotal,
                    Itens = pedidoVenda.Itens.Select(item => new PedidoSapItemRetornoDTO
                    {
                        NumeroDoItem = item.Numero,
                        Status = item.Status,
                        ValorDeTabela = item.ValorTabela,
                        ValorPolitica = item.ValorPolitica,
                        CondicoesDePreco = item.CondicoesDePreco.Select(condicaoDePreco => new CondicaoDePrecoDTO
                        {
                            Nivel = condicaoDePreco.Nivel,
                            Tipo = condicaoDePreco.Tipo,
                            Base = condicaoDePreco.Base,
                            Montante = condicaoDePreco.Montante,
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
