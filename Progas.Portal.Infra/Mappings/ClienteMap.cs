using Progas.Portal.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Progas.Portal.Infra.Mappings
{
    public class ClienteMap : ClassMap<Cliente>
    {
        public ClienteMap()
        {
            Table("pro_cliente");
            Id(x => x.Id, "pro_id_cliente");
            Map(x => x.Id_cliente);
            Map(x => x.Nome);
            Map(x => x.Cpf);
            Map(x => x.Cnpj);
            Map(x => x.Nr_ie_cli);
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
            Map(x => x.Email);
            Map(x => x.Pacote);
            Map(x => x.Hora_criacao);
            Map(x => x.Data_criacao);
        }
    }
}
