using Progas.Portal.Common;
using Progas.Portal.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Progas.Portal.Infra.Mappings
{
    public class UsuarioMap: ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            Table("Usuario");
            Id(x => x.Login);
            References(x => x.Fornecedor).Column("CodigoFornecedor");
            Map(u => u.Nome);
            Map(u => u.Senha);
            Map(u => u.Email);
            Map(u => u.Status).CustomType<Enumeradores.StatusUsuario>();

            HasMany(x => x.Perfis)
                .Cascade.All()/*.Not.LazyLoad()*/
                .Table("UsuarioPerfil")
                .KeyColumn("Login")
                .Element("Perfil");

            //Map(u => u.Perfil).CustomType<Enumeradores.Perfil>()

        }
    }
}
