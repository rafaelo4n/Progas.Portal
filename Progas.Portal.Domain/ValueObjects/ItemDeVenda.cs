using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Domain.ValueObjects
{
    public class ItemDeVenda
    {
        public Material Material { get; set; }
        public ListaPreco ListaPreco { get; set; }
        public decimal Quantidade { get; set; }
        public decimal DescontoManual { get; set; }
        public MotivoDeRecusa MotivoDeRecusa { get; set; }
    }
}