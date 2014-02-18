using System.Collections.Generic;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Services.Contracts
{
    public interface ICadastroCondicaoPagamento
    {
        //void Novo(CondicaoDePagamentoCadastroVm condicaoDePagamentoCadastroVm);
        void AtualizarCondicoesDePagamento(IList<CondicaoDePagamentoCadastroVm> condicoesDePagamento);
    }
}