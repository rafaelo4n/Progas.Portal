using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Infra.Repositories.Contracts
{
    public interface ICompleteRepository<TEntidade> : IReadOnlyRepository<TEntidade> where TEntidade:IAggregateRoot
    {
        void Save(TEntidade entidade);
        void Delete(TEntidade entidade);
    }
}
