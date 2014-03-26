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

        IList<T> ObterRegistrosUmCampo(String Campo, String busca);

        IList<T> ObterRegistrosDoisCampos(String Campo1, String busca1, String campo2, String busca2);

        IList<T> ObterRegistrosTresCampos(string campo1, string busca1, string campo2, string busca2, string campo3, string busca3);

        IList<T> ObterRegistrosQuatroCampos(string campo1, string busca1, string campo2, string busca2, string campo3, string busca3, string campo4, string busca4);        

        IList<T> PesquisaIncotermLinha(String CampoCodigoIncotermCab, String campoIncotermLinha, String valorCodigoIncotermCab, String valorIncotermLinha);
    }
}
