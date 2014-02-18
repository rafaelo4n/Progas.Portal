using Progas.Portal.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progas.Portal.Application.Services.Contracts
{
    public interface ICadastroPedidoVenda
    {
        void Salvar(IList<PedidoVendaSalvarVm> linhasCadastroVm);
        void Atualizar(IList<PedidoVendaSalvarVm> linhasCadastroVm);
    }
}
