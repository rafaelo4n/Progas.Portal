using Progas.Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progas.Portal.Infra.Repositories.Contracts
{
    public interface IClientes : ICompleteRepository<Cliente>
    {
        // Consulta os clientes pelo id
        Cliente ConsultaClientes (string id_cliente);
        Cliente BuscaPeloCodigo(string codigoSap);
        IClientes CodigoContendo(string filtroCodigo);
        IClientes FiltraPelaDescricao(string descricao);
        IClientes NomeContendo(string filtroNome);
    }
}
