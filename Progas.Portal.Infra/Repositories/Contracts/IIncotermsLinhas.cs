﻿using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Infra.Repositories.Contracts
{
    public interface IIncotermsLinhas : ICompleteRepository<IncotermLinha>
    {
        IIncotermsLinhas FiltraPorId(int id);
        IIncotermsLinhas DoCabecalho(string codigoDoCabecalho);
    }
}
