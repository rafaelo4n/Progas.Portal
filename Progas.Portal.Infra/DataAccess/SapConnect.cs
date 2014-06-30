// Realiza a Conexão no SAP conforme os dados do arquivo Web.config.

using System;
using System.Configuration;
using SAP.Middleware.Connector;

namespace Progas.Portal.Infra.DataAccess
{
    public class SapConnect : IDestinationConfiguration
    {
        public RfcConfigParameters GetParameters(string destinationName)
        {

            string appserverhost = ConfigurationManager.AppSettings["AppServerHost"];
            string saprouter = ConfigurationManager.AppSettings["SAPRouter"];
            string systemnumber = ConfigurationManager.AppSettings["SystemNumber"];
            string systemid = ConfigurationManager.AppSettings["SystemID"];
            string user = ConfigurationManager.AppSettings["User"];
            string password = ConfigurationManager.AppSettings["Password"];
            string client = ConfigurationManager.AppSettings["Client"];
            string poolsize = ConfigurationManager.AppSettings["PoolSize"];

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
            string destinationName = ConfigurationManager.AppSettings["DestinationName"];
            GetParameters(destinationName);

            RfcDestinationManager.RegisterDestinationConfiguration(this);
            RfcDestination dest = RfcDestinationManager.GetDestination("DEV");

            return dest;

        }

        public bool ChangeEventsSupported()
        {
            return false;
        }

        public event RfcDestinationManager.ConfigurationChangeHandler
            ConfigurationChanged;
    }
}
