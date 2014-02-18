using Progas.Portal.Domain.Entities;
using Progas.Portal.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progas.Portal.Application.Queries.Builders
{
    public class ListaPrecoCadastroBuilder : IBuilder<ListaPreco, ListaPrecoCadastroVm>
    {
        public ListaPrecoCadastroVm BuildSingle(ListaPreco listaPreco)
        {
            return new ListaPrecoCadastroVm()
            {
                Codigo = listaPreco.Codigo,
                Descricao = listaPreco.Descricao
            };
        }

        public IList<ListaPrecoCadastroVm> BuildList(IList<ListaPreco> listasPreco)
        {
            return listasPreco.Select(listaPreco => new ListaPrecoCadastroVm()
            {
                Codigo    = listaPreco.Codigo,
                Descricao = listaPreco.Descricao

            }).ToList();
        }
    }
}
