using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DadosSap.Entity
{
    public class ClienteTransportadoraLiberada
    {
        public virtual int Id { get; set; }
        public virtual string Id_cliente { get; set; }
        public virtual string Funcao_parceiro { get; set; }
        public virtual string Numero_agente_frete { get; set; }
        public virtual bool Padrao { get; set; }
        public virtual string Pacote { get; set; }
        public virtual DateTime Data_criacao { get; set; }
        public virtual string Hora_criacao { get; set; }
    }
}
