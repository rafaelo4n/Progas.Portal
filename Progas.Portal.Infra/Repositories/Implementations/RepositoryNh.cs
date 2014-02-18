using System.Linq;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;
using NHibernate.Linq;

namespace Progas.Portal.Infra.Repositories.Implementations
{
    public abstract class RepositoryNh<TEntity> where TEntity: IAggregateRoot
    {
        protected IUnitOfWorkNh UnitOfWorkNh;
        protected IQueryable<TEntity> Query;

        protected RepositoryNh(IUnitOfWorkNh unitOfWorkNh)
        {
            UnitOfWorkNh = unitOfWorkNh;
            Query = UnitOfWorkNh.Session.Query<TEntity>();
        }
    }
}
