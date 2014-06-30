using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DadosSap.Entity
{
    public class Material
    {
        public virtual int pro_id_material { get; set; }
        public virtual string Id_material { get; set; }
        public virtual string Id_cliente { get; set; }
        public virtual string Id_centro { get; set; }
        public virtual string Descricao { get; set; }
        public virtual string Tip_mat { get; set; }
        public virtual string Uni_med { get; set; }
        public virtual string Peso_bru { get; set; }
        public virtual string Peso_liq { get; set; }
        public virtual string Volume { get; set; }
        public virtual string Status_mat { get; set; }
        public virtual DateTime? Data_criacao { get; set; }
        public virtual string Pacote { get; set; }
        public virtual string Hora_criacao { get; set; }
        public virtual string Eliminacao { get; set; }
        
    }
}
