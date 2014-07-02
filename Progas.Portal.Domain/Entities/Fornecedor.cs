using System;
using System.Collections.Generic;

namespace Progas.Portal.Domain.Entities
{
    public class Fornecedor:IAggregateRoot
    {
        public virtual string Codigo { get; protected set; }
        public virtual string Nome { get; protected set; }
        public virtual string Cpf { get; protected set; }        
        public virtual string Cnpj { get; protected set; }
        public virtual string Nr_ie_for { get; protected set; }
        public virtual string Cep { get; protected set; }
        public virtual string Endereco { get; protected set; }
        public virtual string Numero { get; protected set; }
        public virtual string Complemento { get; protected set; }
        public virtual string Municipio { get; protected set; }
        public virtual string Bairro { get; protected set; }
        public virtual string Uf { get; protected set; }
        public virtual string Pais { get; protected set; }
        public virtual string Tel_res { get; protected set; }
        public virtual string Tel_cel { get; protected set; }
        public virtual string Fax { get; protected set; }
        public virtual string Email { get; protected set; }
        public virtual DateTime data_criacao { get; protected set; }
        public virtual string pacote { get; protected set; }
        public virtual string hora_criacao { get; protected set; }
        public virtual string Grupo_contas { get; protected set; }
        public virtual string Eliminacao { get; protected set; }
        public virtual IList<FornecedorDaEmpresa> Empresas { get; protected set; }
        public virtual IList<TransportadoraDoRepresentante> Transportadoras { get; protected set; }


        
        //public virtual IList<Produto>  Produtos { get; protected set; }
        public Fornecedor(string codigo, string nome, string cpf, string cnpj, string nr_ie_for,
                        string cep, string endereco, string numero, string complemento,
                        string municipio, string bairro, string uf, string pais, string tel_res, string tel_cel,
                        string email, string grupo_contas, string eliminacao)
            : this()
        {
            Codigo = codigo;
            Nome = nome;
            Cpf = cpf;
            Cnpj = cnpj;
            Nr_ie_for = nr_ie_for;
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
            Grupo_contas = grupo_contas;
            Eliminacao = eliminacao;
        }

        protected Fornecedor()
        { 
            Empresas = new List<FornecedorDaEmpresa>();
        
        }

        #region override
        protected bool Equals(Fornecedor other)
        {
            return string.Equals(Codigo, other.Codigo);
        }

        public override int GetHashCode()
        {
            return (Codigo != null ? Codigo.GetHashCode() : 0);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Fornecedor) obj);
        }
        #endregion
    }
}
