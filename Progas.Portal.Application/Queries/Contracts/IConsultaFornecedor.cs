using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Contracts
{
    public interface IConsultaFornecedor
    {

        KendoGridVm Listar(PaginacaoVm paginacaoVm, FornecedorFiltroVm filtro);

        FornecedorCadastroVm ConsultaPorCodigo(string codigoDoFornecedor);
    }
}