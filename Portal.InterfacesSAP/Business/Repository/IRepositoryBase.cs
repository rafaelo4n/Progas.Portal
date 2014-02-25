using Portal.DadosSap.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DadosSap.Business.Repository
{
    // Base para todas outras interfaces
    public interface IRepositoryBase<T>
    {
        T Salvar(T entity);

        T Alterar(T entity);

        void Excluir(T entity);

        T ObterPorId(string id);

        IList<T> ObterTodos();

        IList<T> ObterTodosComCampo(String Campo,String busca);

        IList<T> PesquisaClienteVendas(String CampoCliente, String buscaCliente, String CampoOrg, String buscaOrg);

        IList<T> PesquisaIncotermLinha(String CampoCodigoIncotermCab, String campoIncotermLinha, String valorCodigoIncotermCab, String valorIncotermLinha);
    }
}
