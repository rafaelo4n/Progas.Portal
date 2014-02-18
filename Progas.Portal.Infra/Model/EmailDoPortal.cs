using System.Configuration;

namespace Progas.Portal.Infra.Model
{
    public class EmailDoPortal : ConfigurationSection
    {
        [ConfigurationProperty("RemetenteProgas", DefaultValue = "rafaelo4n@gmail.com", IsRequired = false)]
        public string RemetenteProgas
        {
            get
            {
                return this["RemetenteProgas"] as string;
            }
        }

        [ConfigurationProperty("Servidor", DefaultValue = "smtp.gmail.com", IsRequired = false)]
        public string Servidor
        {
            get
            {
                return this["Servidor"] as string;
            }
        }

        [ConfigurationProperty("Porta", DefaultValue = 587, IsRequired = false)]
        public int Porta
        {
            get
            {
                return (int) this["Porta"] ;
            }
        }

        [ConfigurationProperty("Dominio", DefaultValue = "", IsRequired = false)]
        public string Dominio
        {
            get
            {
                return this["Dominio"] as string;
            }
        }

        [ConfigurationProperty("Usuario", DefaultValue = "rafaelo4n", IsRequired = false)]
        public string Usuario
        {
            get
            {
                return this["Usuario"] as string;
            }
        }

        [ConfigurationProperty("Senha", DefaultValue = "blink18241", IsRequired = false)]
        public string Senha
        {
            get
            {
                return this["Senha"] as string;
            }
        }

        [ConfigurationProperty("HabilitarSsl", DefaultValue = false, IsRequired = false)]
        public bool HabilitarSsl
        {
            get
            {
                return this["HabilitarSsl"] is bool && (bool) this["HabilitarSsl"];
            }
        }
        


    }
}