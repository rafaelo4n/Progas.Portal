using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DadosSap.Entity
{
    public class UnidadeMedida
    {       
        public virtual int pro_id_unidademedida { get; set; }
        public virtual string Id_unidademedida { get; set; }
        public virtual string Descricao { get; set; }
        public virtual string Dimensao { get; set; }
        public virtual string Aprestecnica { get; set; }
        public virtual DateTime data_criacao { get; set; }
        public virtual string pacote { get; set; }
        public virtual string hora_criacao { get; set; }
    }
}
