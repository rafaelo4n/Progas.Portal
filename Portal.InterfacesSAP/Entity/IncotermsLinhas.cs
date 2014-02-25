using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DadosSap.Entity
{
    public class IncotermsLinhas
    {
        public virtual int pro_id_incotermLinha   { get; set; }        
        public virtual string CodigoIncotermCab   { get; set; }
        public virtual string IncotermLinha       { get; set; }
        public virtual DateTime Data_criacao      { get; set; }
        public virtual string Pacote              { get; set; }
        public virtual string Hora_criacao        { get; set; }
    }
}
