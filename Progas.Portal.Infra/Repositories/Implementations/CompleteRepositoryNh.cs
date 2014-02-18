using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;

namespace Progas.Portal.Infra.Repositories.Implementations
{
    public class CompleteRepositoryNh<TEntity>: ReadOnlyRepositoryNh<TEntity>, ICompleteRepository<TEntity> where TEntity: class, IAggregateRoot
    {
        public CompleteRepositoryNh(IUnitOfWorkNh unitOfWork) : base(unitOfWork)
        {
        }

        public void Save(TEntity entidade)
        {
            //UnitOfWorkNh.Session.SaveOrUpdate(entidade);
            UnitOfWorkNh.Session.Save(entidade);
        }

        public void Delete(TEntity entidade)
        {
            UnitOfWorkNh.Session.Delete(entidade);
        }
    }
}
