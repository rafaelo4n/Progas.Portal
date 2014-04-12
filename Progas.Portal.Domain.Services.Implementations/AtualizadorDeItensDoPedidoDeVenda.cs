using System.Collections.Generic;
using System.Linq;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Domain.Services.Contracts;
using Progas.Portal.Domain.ValueObjects;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Domain.Services.Implementations
{
    public class AtualizadorDeItensDoPedidoDeVenda : IAtualizadorDeItensDoPedidoDeVenda
    {

        private readonly IMateriais _materiais;
        private readonly IListasPreco _listasPreco;
        private readonly IMotivosDeRecusa _motivosDeRecusa;

        public AtualizadorDeItensDoPedidoDeVenda(IMateriais materiais, IListasPreco listasPreco, IMotivosDeRecusa motivosDeRecusa)
        {
            _materiais = materiais;
            _listasPreco = listasPreco;
            _motivosDeRecusa = motivosDeRecusa;
        }

        public void Atualizar(PedidoVenda pedidoVenda, PedidoVendaSalvarVm pedidoAlterado)
        {
            IEnumerable<int> idDosItensParaRemover = pedidoVenda.Itens
                .Where(itemAtual => pedidoAlterado.Itens.All(itemAlterado => itemAlterado.IdDoItem != itemAtual.Id))
                .Select(itemAtual => itemAtual.Id).ToList();

            foreach (var id in idDosItensParaRemover)
            {
                PedidoVendaLinha itemParaRemover = pedidoVenda.Itens.Single(item => item.Id == id);
                pedidoVenda.Itens.Remove(itemParaRemover);
            }


            int[] idDosMateriais = pedidoAlterado.Itens.Select(x => x.IdMaterial).Distinct().ToArray();

            IList<Material> materiaisDosItens = _materiais.BuscarLista(idDosMateriais).List();

            string[] codigoDasListasDePreco =
                pedidoAlterado.Itens.Select(item => item.CodigoDaListaDePreco).Distinct().ToArray();

            IList<ListaPreco> listasDePreco = _listasPreco.FiltraPorListaDeCodigos(codigoDasListasDePreco).List();

            string[] codigoDosMotivosDeRecusa =
                pedidoAlterado.Itens.Where(i => !string.IsNullOrEmpty(i.CodigoDoMotivoDeRecusa))
                    .Select(i => i.CodigoDoMotivoDeRecusa).Distinct().ToArray();

            IList<MotivoDeRecusa> motivosDeRecusa = _motivosDeRecusa.BuscarLista(codigoDosMotivosDeRecusa).List();

            var itensParaAlterar = (from itemAtual in pedidoVenda.Itens
                join itemAlterado in pedidoAlterado.Itens
                    on itemAtual.Id equals itemAlterado.IdDoItem
                select new
                {
                    ItemAtual = itemAtual,
                    ItemAlterado = itemAlterado
                }).ToList();

            for (int i = 0; i < itensParaAlterar.Count; i++)
            {
                var itemParaAlterar = itensParaAlterar[i];

                PedidoVendaSalvarItemVm itemAlterado = itemParaAlterar.ItemAlterado;
                Material material = materiaisDosItens.Single(m => m.pro_id_material == itemAlterado.IdMaterial);
                ListaPreco listaPreco = listasDePreco.Single(l => l.Codigo == itemAlterado.CodigoDaListaDePreco);
                MotivoDeRecusa motivoDeRecusa = string.IsNullOrEmpty(itemAlterado.CodigoDoMotivoDeRecusa)
                    ? null
                    : motivosDeRecusa.Single(m => m.Codigo == itemAlterado.CodigoDoMotivoDeRecusa);

                var itemDeVenda = new ItemDeVenda
                {
                    Material = material,
                    Quantidade = itemAlterado.Quantidade,
                    ListaPreco = listaPreco,
                    DescontoManual = itemAlterado.Desconto,
                    MotivoDeRecusa = motivoDeRecusa

                };

                itemParaAlterar.ItemAtual.Alterar(itemDeVenda);

                
            }

            IEnumerable<PedidoVendaSalvarItemVm> itensParaAdicionar = pedidoAlterado.Itens.Where(
                itemAlterado => pedidoVenda.Itens.All(itemAtual => itemAtual.Id != itemAlterado.IdDoItem));

            foreach (var itemParaAdicionar in itensParaAdicionar)
            {

                Material material = materiaisDosItens.Single(m => m.pro_id_material == itemParaAdicionar.IdMaterial);
                ListaPreco listaDePreco = listasDePreco.Single(l => l.Codigo == itemParaAdicionar.CodigoDaListaDePreco);
                MotivoDeRecusa motivoDeRecusa = string.IsNullOrEmpty(itemParaAdicionar.CodigoDoMotivoDeRecusa)
                    ? null
                    : motivosDeRecusa.Single(m => m.Codigo == itemParaAdicionar.CodigoDoMotivoDeRecusa);

                var linhasPedido = new PedidoVendaLinha(itemParaAdicionar.Numero, material,itemParaAdicionar.Quantidade,listaDePreco,itemParaAdicionar.Desconto, motivoDeRecusa);

                pedidoVenda.AdicionarItem(linhasPedido);
                
            }


        }
    }
}