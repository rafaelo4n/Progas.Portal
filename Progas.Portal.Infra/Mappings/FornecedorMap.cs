using Progas.Portal.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Progas.Portal.Infra.Mappings
{
    public class FornecedorMap : ClassMap<Fornecedor>
    {
        public FornecedorMap()
        {
            Table("pro_fornecedor");
            Id(x => x.Codigo);
            Map(x => x.Nome);
            Map(x => x.Cpf);
            Map(x => x.Cnpj);
            Map(x => x.Nr_ie_for);
            Map(x => x.Cep);
            Map(x => x.Endereco);
            Map(x => x.Numero);
            Map(x => x.Complemento);
            Map(x => x.Municipio);
            Map(x => x.Bairro);
            Map(x => x.Uf);
            Map(x => x.Pais);
            Map(x => x.Tel_res);
            Map(x => x.Tel_cel);
            Map(x => x.Fax);
                              
        }
    }
}
