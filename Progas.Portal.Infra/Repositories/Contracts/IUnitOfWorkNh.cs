using NHibernate;

namespace Progas.Portal.Infra.Repositories.Contracts
{
    public interface IUnitOfWorkNh: IUnitOfWork
    {
        ISession Session { get; }         
    }
}