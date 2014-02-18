using System.Collections.Generic;
using Progas.Portal.Common;
using Progas.Portal.Infra.Model;

namespace Progas.Portal.Infra.Builders
{
    public class MenuUsuarioBuilder
    {
        private readonly IList<Enumeradores.Perfil>  _perfis;

        public MenuUsuarioBuilder(IList<Enumeradores.Perfil> perfis)
        {
            _perfis = perfis;
        }

        public IList<Menu> Construct()
        {
            var menus = new List<Menu>();            

            if (_perfis.Contains(Enumeradores.Perfil.Administrador))
            {
                menus.Add(new MenuAdministrativo());
            }           

            // Menu de Vendas
            if (_perfis.Contains(Enumeradores.Perfil.Vendedor))
            {
                menus.Add(new MenuVendas());
            }

            // Menu de Vendas
            if (_perfis.Contains(Enumeradores.Perfil.Cadastros))
            {
                menus.Add(new MenuCadastros());
            }
            return menus;
        }

    }       

    internal class MenuAdministrativo : Menu
    {
        public MenuAdministrativo()
            : base("Administrativo")
        {
            AdicionarItem("Usuários", "Usuario", "Index");
        }
    }

    // Menu de Vendas
    internal class MenuVendas : Menu
    {
        public MenuVendas()
            : base("Pedidos")
        {
            AdicionarItem("Criar", "PedidoVenda", "Index");
            AdicionarItem("Listar", "PedidoVenda", "Consultar");
        }
    }
    
    // Criar Menu Cadastros
    internal class MenuCadastros : Menu
    {
        public MenuCadastros()
            : base("Cadastros")
        {
            AdicionarItem("Material", "Cadastros", "Index");
            AdicionarItem("Condição de Pagamento", "Cadastros", "ConsultaCondicaoPagamento");
            AdicionarItem("Cliente", "Cadastros", "ConsultaCliente");
            AdicionarItem("Fornecedor", "Cadastros", "ConsultaFornecedor");
            AdicionarItem("Undiade de Medida", "Cadastros", "ConsultaUnidadeMedida");
        }
    }

}
