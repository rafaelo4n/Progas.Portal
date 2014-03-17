using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Infra.Repositories.Contracts
{
    public interface IMotivosDeRecusa: IReadOnlyRepository<MotivoDeRecusa>
    {
        IMotivosDeRecusa BuscarLista(string[] codigoDosMotivosDeRecusa);
    }
}