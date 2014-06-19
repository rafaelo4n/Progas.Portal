using System.ComponentModel;

namespace Progas.Portal.DTO
{
    public class RepresentanteDTO
    {
        public string Codigo { get; set; }
        [DisplayName("Representante:")]
        public string Nome { get; set; }

    }
}
