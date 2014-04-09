using Progas.Portal.Domain.Entities;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Builders
{
    public class FornecedorCadastroBuilder : Builder<Fornecedor, FornecedorCadastroVm>
    {
        public override FornecedorCadastroVm BuildSingle(Fornecedor model)
        {
            return new FornecedorCadastroVm()
            {
                Codigo = model.Codigo,
                Nome = model.Nome,
                Cpf = model.Cpf,
                Cnpj = model.Cnpj,
                nr_ie_for = model.Nr_ie_for,
                cep = model.Cep,
                endereco = model.Endereco,
                numero = model.Numero,
                complemento = model.Complemento,
                municipio = model.Municipio,
                bairro = model.Bairro,
                uf = model.Uf,
                pais = model.Pais,
                tel_res = model.Tel_res,
                tel_cel = model.Tel_cel,
                email = model.Email
            };
        }
    }
}
