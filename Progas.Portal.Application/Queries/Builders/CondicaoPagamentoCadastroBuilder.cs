using System.Collections.Generic;
using System.Linq;
using Progas.Portal.Domain.Entities;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Builders
{
    public class CondicaoPagamentoCadastroBuilder : IBuilder<CondicaoDePagamento, CondicaoDePagamentoCadastroVm>
    {
        public CondicaoDePagamentoCadastroVm BuildSingle(CondicaoDePagamento condicaoDePagamento)
        {
            return new CondicaoDePagamentoCadastroVm()
            {
                Codigo = condicaoDePagamento.Codigo,
                Descricao = condicaoDePagamento.Descricao
            };
        }

        public IList<CondicaoDePagamentoCadastroVm> BuildList(IList<CondicaoDePagamento> condicoesDePagamento)
        {
            return condicoesDePagamento.Select(condicaoDePagamento => new CondicaoDePagamentoCadastroVm()
            {
                Codigo = condicaoDePagamento.Codigo,
                Descricao = condicaoDePagamento.Descricao
            }).ToList();
        }
    }

}
