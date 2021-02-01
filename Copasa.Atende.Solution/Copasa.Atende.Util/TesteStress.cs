using Copasa.Atende.Model;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Web;

namespace Copasa.Atende.Util
{
    public class TesteStress
    {
        public void Testar(int totalRajadas,int opcao, ILog log)
        {
            List<string> identificadores = new List<string>();
            List<string> matriculas = new List<string>();
            List<string> cpfs = new List<string>();
            string nomeArquivo = "cpfs.txt";
            if (opcao != 1)
                nomeArquivo = "identificadores.txt";
            string FilePath = HttpContext.Current.Request.PhysicalApplicationPath + nomeArquivo;
            if (File.Exists(FilePath))
            {
                System.IO.StreamReader file = new System.IO.StreamReader(FilePath);
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    if (opcao == 1)
                        cpfs.Add(line.Trim());
                    else
                    {
                        string[] valores = line.Split(',');
                        identificadores.Add(valores[0].Trim());
                        matriculas.Add(valores[1].Trim());
                    }
                }

                List<Thread> threads = new List<Thread>();
                string[] arrayMatriculas = matriculas.ToArray();
                string[] arrayIdentificadores = identificadores.ToArray();
                string[] arrayCpfs = cpfs.ToArray();
                int contador = 0;
                int limite = 0;
                if (opcao == 1)
                    limite = arrayCpfs.Length;
                else
                    limite = arrayMatriculas.Length;
                for (int i = 0; i < totalRajadas; i++)
                {
                    contador++;
                    if (contador >= limite)
                        contador = 0;
                    TesteStressThread testeStressThread = null;
                    if (opcao == 1)
                        testeStressThread = new TesteStressThread(ConfigurationManager.AppSettings.Get("HostLocal"), arrayCpfs[contador],opcao);
                    else
                        testeStressThread = new TesteStressThread(ConfigurationManager.AppSettings.Get("HostLocal"), arrayIdentificadores[contador], arrayMatriculas[contador], opcao);
                    Thread thr = new Thread(new ThreadStart(testeStressThread.Run));
                    threads.Add(thr);

                }

                foreach (Thread thr in threads)
                    {
                        thr.Start();
                    }

                    bool temThread = false;
                    do
                    {
                        temThread = false;
                        foreach (Thread thr in threads)
                        {
                            if (thr.IsAlive)
                            {
                                temThread = true;
                                break;
                            }
                        }
                        Thread.Sleep(1000);
                    } while (temThread);
                    //Thread.Sleep(3000);
                //}
            }
            else
                log.AddLog("Arquivo não existe");
        }
    }
}
