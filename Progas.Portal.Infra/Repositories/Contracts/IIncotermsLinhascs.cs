using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Infra.Repositories.Contracts
{
    public interface IIcontermslinhas : ICompleteRepository<IncotermLinhas>
    {
        IIcontermslinhas DoCabecalho(string codigoDoCabecalho);
    }
}
