using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Contracts
{
    public interface IConsultaTransRedespacho
    {
        KendoGridVm Listar(PaginacaoVm paginacaoVm, FornecedorFiltroVm filtro);
        TransRedespachoCadastroVm ConsultaPorCodigo(string codigoDoFornecedor);        
        string ConsultaPorCnpj(string cnpj);
    }
}
