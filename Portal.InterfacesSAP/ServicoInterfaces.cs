using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DadosSap
{
    partial class ServicoInterfaces : ServiceBase
    {
        private static void Main(string[] args)
        {
            var service = new ServicoInterfaces();
            if (Environment.UserInteractive)
            {
                service.OnStart(args);

                Console.Read();
                //service.OnStop();
            }
            else
            {
                Run(service);
            }
        }

        public ServicoInterfaces()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // Inicializa a conexão RFC
            Interfaces.conexaoRFC();
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
