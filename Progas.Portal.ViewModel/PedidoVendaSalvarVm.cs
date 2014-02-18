using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progas.Portal.ViewModel
{
    // View que recebera os valores da pagina que serão utilizados no CRUD
    public class PedidoVendaSalvarVm
    {
        // cabecalho
        public virtual string  Tipo             { get; set; }//
        public virtual string  NumeroPedido     { get; set; }//
        public virtual string  CodigoCondpgto   { get; set; }//
        public virtual string  Centro            { get; set; }//
        public virtual DateTime  datap          { get; set; }
        public virtual string  CodigoTipoPedido { get; set; }//
        public virtual string  trans            { get; set; }//
        public virtual string  transred         { get; set; }//
        public virtual string  transredcif      { get; set; }//
        public virtual string  Cliente          { get; set; }//
        public virtual string  Inco1            { get; set; }//
        public virtual string  Inco2            { get; set; }//
        public virtual string  Observacao       { get; set; } //             
        // linhas
        public virtual string  CodigoMaterial   { get; set; }
        public virtual decimal Quantidade       { get; set; }
        public virtual string  UnidadeMedida    { get; set; }
        public virtual string  listapreco       { get; set; }
        public virtual decimal Desconto         { get; set; }   
    }
}
