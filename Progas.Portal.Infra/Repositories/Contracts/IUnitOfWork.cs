using System;
using System.Linq;

namespace Progas.Portal.Infra.Repositories.Contracts
{
    public interface IUnitOfWork:IDisposable
    {
        void BeginTransaction();
        void Commit();
        void RollBack();
    }
}
