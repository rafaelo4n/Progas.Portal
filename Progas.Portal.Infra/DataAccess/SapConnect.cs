// Realiza a Conexão no SAP conforme os dados do arquivo Web.config.

using System;
using System.Configuration;
using NHibernate.Criterion;
using SAP.Middleware.Connector;

namespace Progas.Portal.Infra.DataAccess
{
    public class SapConnect : IDestinationConfiguration
    {
        // Dados de Parametros do Web.config
        public static string appserverhost = ConfigurationSettings.AppSettings["AppServerHost"];
        public static string saprouter = ConfigurationSettings.AppSettings["SAPRouter"];
        public static string systemnumber = ConfigurationSettings.AppSettings["SystemNumber"];
        public static string systemid = ConfigurationSettings.AppSettings["SystemID"];
        public static string user = ConfigurationSettings.AppSettings["User"];
        public static string password = ConfigurationSettings.AppSettings["Password"];
        public static string client = ConfigurationSettings.AppSettings["Client"];
        public static string poolsize = ConfigurationSettings.AppSettings["PoolSize"];
        public static string repositorydestination = ConfigurationSettings.AppSettings["RepositoryDestination"];

        public RfcConfigParameters GetParameters(String destinationName)
        {
            if (repositorydestination != destinationName) return null;
            var parametros = new RfcConfigParameters
            {
                {RfcConfigParameters.AppServerHost, appserverhost},
                {RfcConfigParameters.SAPRouter, saprouter},
                {RfcConfigParameters.SystemNumber, systemnumber},
                {RfcConfigParameters.SystemID, systemid},
                {RfcConfigParameters.User, user},
                {RfcConfigParameters.Password, password},
                {RfcConfigParameters.Client, client},
                {RfcConfigParameters.PoolSize, poolsize}
            };
            return parametros;
        }

        public RfcDestination Conectar()
        {
            GetParameters("DEV");

            RfcDestinationManager.RegisterDestinationConfiguration(this);
            RfcDestination dest = RfcDestinationManager.GetDestination("DEV");

            return dest;

        }

        // The following two are not used in this example:
        public bool ChangeEventsSupported()
        {
            return false;
        }

        public event RfcDestinationManager.ConfigurationChangeHandler
            ConfigurationChanged;
    }
}
