namespace Progas.Portal.ViewModel
{
    public class ClienteFiltroVm:ListagemVm
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string Cpf { get; set; }
        public string Municipio { get; set; }
    }

    public class ClienteParaSelecaoVm: ClienteFiltroVm
    {
        public int Id { get; set; }
        public string Telefone { get; set; }
    }


}
