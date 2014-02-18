using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Progas.Portal.Domain.Entities 
{
    public class Material : IAggregateRoot
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
        public virtual DateTime Data_criacao { get; set; }
        public virtual string Pacote  { get; set; }
        public virtual string Hora_criacao { get; set; }
        public virtual IList<Material> Materiais { get; set; }
    

        /*protected Material()
        {
            Materiais = new List<Material>();
        }*/

        public Material (string id_material,string descricao, string centro, string tip_mat, string uni_med  ):this()
        {
            Id_material = id_material;
            Descricao   = descricao;
            Id_centro = centro;
            Tip_mat = tip_mat;
            Uni_med = uni_med;
        }

        public Material()
        {
            // TODO: Complete member initialization
        }

    }
}
