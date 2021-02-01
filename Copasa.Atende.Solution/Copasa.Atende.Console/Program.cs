using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Sigos.Console
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            System.Console.WriteLine("Inicio processamento");
            if ("S".Equals(ConfigurationManager.AppSettings["ProcessarFTP"].ToString()))
            {
                FTP ftp = new FTP();
                ftp.EnviaArquivoOS();
                ftp.EnviaArquivoTabelas();
            }
            if ("S".Equals(ConfigurationManager.AppSettings["ProcessarWS"].ToString()))
            {
                StartWS startWS = new StartWS();
                startWS.EnviaLoteOsParaMaxProcess();
            }
            System.Console.WriteLine("Fim processamento");
        }
    }
}
