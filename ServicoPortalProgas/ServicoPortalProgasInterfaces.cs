using SAP.Middleware.Connector;
using ServicoPortalProgasInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Portal.DadosSap;


namespace ServicoPortalProgasInterfaces
{
    public partial class ServicoPortalProgasInterfaces : ServiceBase
    {
        StreamWriter arquivoLog;

        public ServicoPortalProgasInterfaces()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //Portal.DadosSap.Program.Main(new String[] { });

            //Instancie a variável criada, que receberá como parâmetro o caminho de meu arquivo de texto,

            //que será o log destes eventos do meu serviço, e o parâmetro encoding com o valor true.

            arquivoLog = new StreamWriter(@"C:\Log_Interface_PortalProgas.txt", true);

            RFC rfc = new RFC();

            //Escrevo no arquivo texto no momento que o arquivo for iniciado

            arquivoLog.WriteLine("Serviço iniciado em: " + DateTime.Now);

            //Limpo o buffer com o método Flush

            arquivoLog.Flush();
        }

        protected override void OnStop()
        {
            //Escrevo no arquivo texto no momento exato que o arquivo for encerrado

            arquivoLog.WriteLine("Serviço encerrado em: " + DateTime.Now);

            //Fecho o arquivo com o método Close

            arquivoLog.Close();
        }
        
    }
}
