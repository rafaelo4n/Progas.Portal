using System.Collections.Generic;
using System.Linq;
using Progas.Portal.Domain.Entities;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Builders
{
    //public class TipoPedidoCadastroBuilder : Builder<TipoPedido, TipoPedidoCadastroVm>
    public class IncotermCadastroBuilder : IBuilder<Incoterm, IncotermCadastroVm>
    {
        public IncotermCadastroVm BuildSingle(Incoterm incoterm)
        {
            return new IncotermCadastroVm()
            {
                CodigoIncoterm = incoterm.CodigoIncoterm,
                Descricao      = incoterm.Descricao,
                Tipo           = incoterm.Tipo
            };
        }

        public IList<IncotermCadastroVm> BuildList(IList<Incoterm> incoterms)
        {
            return incoterms.Select(incoterm => new IncotermCadastroVm()
            {
                CodigoIncoterm = incoterm.CodigoIncoterm,
                Descricao      = incoterm.Descricao,
                Tipo           = incoterm.Tipo
            }).ToList();
        }
    }

}
