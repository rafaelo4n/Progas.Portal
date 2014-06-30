using System;
using Progas.Portal.Domain;
using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Infra.Filters
{
    public static class FiltroDeCondicaoDePagamentoDoCliente
    {
        public static Func<CondicaoDePagamentoDoCliente, bool> CondicoesAtivas()
        {
            return x => x.Eliminacao != null && !x.Eliminacao.Equals("X");
        }
    }
}
