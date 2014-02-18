using Progas.Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progas.Portal.Infra.Repositories.Contracts
{
    public interface IMateriais : ICompleteRepository<Material>
    {
        Material   BuscaPeloCodigo(string codigoSap);
        IMateriais FiltraPelaDescricao(string descricao);
        IMateriais FiltraPorListaDeCodigos(string[] codigos);
        IMateriais CodigoContendo(string filtroCodigo);
        IMateriais NomeContendo(string filtroNome);
    }
}
