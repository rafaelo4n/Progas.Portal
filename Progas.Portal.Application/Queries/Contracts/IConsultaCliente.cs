﻿using Progas.Portal.ViewModel;
using System.Collections.Generic;

namespace Progas.Portal.Application.Queries.Contracts
{
    public interface IConsultaCliente
    {
        KendoGridVm Listar(PaginacaoVm paginacaoVm, ClienteFiltroVm filtro);

        IList<ClienteCadastroVm> Listar(PaginacaoVm paginacaoVm, ClienteCadastroVm filtro);
        KendoGridVm ListarParaSelecao(PaginacaoVm paginacaoVm, ClienteFiltroVm filtro);
        IList<ClienteCadastroVm> Listar();
        IList<CondicaoDePagamentoCadastroVm> ListarCondicoesDePagamento(string idDoCliente, string codigoDaCondicaoDePagamento);
    }
}
