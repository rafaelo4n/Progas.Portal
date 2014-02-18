using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Contracts
{
    public interface IConsultaTransRedespachoCif
    {
        KendoGridVm Listar(PaginacaoVm paginacaoVm, FornecedorFiltroVm filtro);
        TransRedespachoCifCadastroVm ConsultaPorCodigo(string codigoDoFornecedor);
        string ConsultaPorCnpj(string cnpj);
    }
}
