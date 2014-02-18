using Progas.Portal.Domain.Entities;
using Progas.Portal.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progas.Portal.Application.Queries.Builders
{
    public class ClienteCadastroBuilder : Builder<Cliente, ClienteCadastroVm>
    {
        public override ClienteCadastroVm BuildSingle(Cliente cliente)
        {
            return new ClienteCadastroVm()
            {
                     id_cliente  = cliente.Id_cliente,
                     nome        = cliente.Nome,
                     Cnpj        = cliente.Cnpj,
                     nr_ie_for   = cliente.Nr_ie_cli,
                     Cpf         = cliente.Cpf,
                     endereco    = cliente.Cpf,
                     numero      = cliente.Numero,
                     complemento = cliente.Complemento,
                     municipio   = cliente.Municipio,
                     uf          = cliente.Uf,
                     pais        = cliente.Pais,
                     tel_res     = cliente.Tel_res,
                     tel_cel     = cliente.Tel_cel,
                     email       = cliente.Email                                                                       
            };
        }
    }
}