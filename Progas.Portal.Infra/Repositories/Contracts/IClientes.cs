using Progas.Portal.Domain.Entities;

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
        IClientes ComCnpj(string cnpj);
        IClientes MunicipioContendo(string municipio);
        IClientes ComCpf(string cpf);
    }
}
