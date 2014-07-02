using System;
using System.Collections.Generic;

namespace Progas.Portal.Domain.Entities
{

    public class Cliente : IAggregateRoot 
    {

        protected Cliente()
        {
            this.AreasDeVenda = new List<ClienteVenda>();
            this.CondicoesDePagamento = new List<CondicaoDePagamentoDoCliente>();
            this.Transportadores = new List<TransportadoraDoCliente>();
        }

        public virtual string Id_cliente { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Cpf { get; set; }
        public virtual string Cnpj { get; set; }
        public virtual string Nr_ie_cli { get; set; }
        public virtual string Cep { get; set; }
        public virtual string Endereco { get; set; }
        public virtual string Numero { get; set; }
        public virtual string Complemento { get; set; }
        public virtual string Municipio { get; set; }
        public virtual string Bairro { get; set; }
        public virtual string Uf { get; set; }
        public virtual string Pais { get; set; }
        public virtual string Tel_res { get; set; }
        public virtual string Tel_cel { get; set; }
        public virtual string Fax { get; set; }
        public virtual string Email { get; set; }
        public virtual string Pacote { get; set; }
        public virtual string Hora_criacao { get; set; }
        public virtual DateTime Data_criacao { get; set; }
        public virtual IList<ClienteVenda> AreasDeVenda { get; set; }
        public virtual IList<CondicaoDePagamentoDoCliente> CondicoesDePagamento { get; protected set; }
        public virtual IList<TransportadoraDoCliente> Transportadores { get; protected set; }
        public virtual string Eliminacao { get; set; }

        public Cliente( string id_cliente, string nome, string cpf, string cnpj, string nr_ie_cli,
                        string cep, string endereco, string numero, string complemento, 
                        string municipio, string bairro, string uf, string pais, string tel_res, string tel_cel,
                        string email ): this()
    {
        Id_cliente = id_cliente;
        Nome = nome;
        Cpf = cpf;
        Cnpj = cnpj;
        Nr_ie_cli = nr_ie_cli;
        Cep = cep;
        Endereco = endereco;
        Numero = numero;
        Complemento = complemento;
        Municipio = municipio;
        Bairro = bairro;
        Uf = uf;
        Pais = pais;
        Tel_res = tel_res;
        Tel_cel = tel_cel;
        Email = email;

    }

  }
}
