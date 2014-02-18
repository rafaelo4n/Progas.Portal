using System.Text;
using System.Threading.Tasks;
using Progas.Portal.Domain.Entities;
using Progas.Portal.ViewModel;


namespace Progas.Portal.Application.Queries.Builders
{
    public class PedidoVendaLinhaCadastroBuilder : Builder<PedidoVendaLinha, PedidoVendaLinhaCadastroVm>
    {
        public override PedidoVendaLinhaCadastroVm BuildSingle(PedidoVendaLinha model)
        {
            return new PedidoVendaLinhaCadastroVm()
            {
                id_cotacao          = model.Id_cotacao,
                id_item             = model.Id_item,
                id_material         = model.Id_material,
                listpre             = model.Listpre,
                CodigoUnidadeMedida = model.Unimed,
                descma              = model.Descma,
                Quant               = model.Quant,
                valfin              = model.Valfin,
                valpol              = model.Valpol,
                valtab              = model.Valtab
            };
        }
    }
}
