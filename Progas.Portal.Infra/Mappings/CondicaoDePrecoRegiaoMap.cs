using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Infra.Mappings
{
    public class CondicaoDePrecoRegiaoMap: ClassMap<CondicaoDePrecoRegiao>
    {
        public CondicaoDePrecoRegiaoMap()
        {
            Table("pro_condicaopreco_regiao");
            Id(x => x.Id);
            Map(x => x.Regiao);
            Map(x => x.Id_material);

        }
    }
}
