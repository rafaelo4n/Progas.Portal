using Progas.Portal.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progas.Portal.Application.Queries.Contracts
{
    public interface IConsultaFornecedor
    {

        KendoGridVm Listar(PaginacaoVm paginacaoVm, FornecedorFiltroVm filtro);
        IList<FornecedorCadastroVm> Listar(PaginacaoVm paginacaoVm, FornecedorCadastroVm filtro);  

        FornecedorCadastroVm ConsultaPorCodigo(string codigoDoFornecedor);
        string ConsultaPorCnpj(string cnpj);
    }
}