using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Portal.DadosSap;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace ServicoPortalProgasInterfaces
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new ServicoPortalProgasInterfaces() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
