﻿using System.Collections.Generic;
using System.Linq;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;
using NHibernate;

namespace Progas.Portal.Infra.Repositories.Implementations
{
    public class ReadOnlyRepositoryNh<TEntity>: RepositoryNh<TEntity>, IReadOnlyRepository<TEntity> where TEntity: class, IAggregateRoot
    {
        public ReadOnlyRepositoryNh(IUnitOfWorkNh unitOfWork) : base(unitOfWork)
        {
        }

        public IList<TEntity> List()
        {
            return Query.ToList();
        }

        public TEntity Single()
        {
            return Query.SingleOrDefault();
        }

        public int Count()
        {
            return Query.Count();
        }

        public IReadOnlyRepository<TEntity> Skip(int times)
        {
            Query = Query.Skip(times);
            return this;
        }

        public IReadOnlyRepository<TEntity> Take(int quantity)
        {
            Query = Query.Take(quantity);
            return this;
        }

        public IQueryable<TEntity> GetQuery()
        {
            return Query;
        }

        public IQueryOver<TEntity, TEntity> GetQueryOver()
        {
            return UnitOfWorkNh.Session.QueryOver<TEntity>();
        }
    }
}
