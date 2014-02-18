using System.ComponentModel;

namespace Progas.Portal.Common
{
    public class Enumeradores
    {
        public enum Perfil
        {
            [Description("Administrador")] Administrador = 4,
            [Description("Vendas")] Vendedor = 8,
            [Description("Cadastros")] Cadastros = 9
        }

        public enum StatusUsuario
        {
            Ativo = 1,
            Bloqueado = 2
        }        

        // portal de vendas
        public enum TipoPedido
        {
           VendaNormal = 1, // YNOR
           VendaAssisTecnica = 2 // YVAS
        }
    }
}
