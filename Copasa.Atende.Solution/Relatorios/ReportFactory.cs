using System;
using System.Data;
using Microsoft.Reporting.WinForms;
using System.Xml;
using System.Net;
using System.IO;
using System.Text;
using System.Drawing.Printing;
using System.Net.Sockets;
using System.Drawing;
using System.Globalization;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Relatorios
{
    public class ReportFactory
    {
        private string mapPath;
        private Funcoes f;

        private Font printFont;
        private StreamReader streamToPrint;

        private PageSettings m_pageSettings;
        private List<Stream> m_pages = new List<Stream>();
        public List<Stream> getReport(string dados, string nomeRelat)
        {
            Warning[] warnings;
            ResultsetFactory rf = new ResultsetFactory();
            rf.setFuncoes(f);
            rf.setReceiveText(dados);
            rf.setTemplateName(nomeRelat);
            rf.setMapPath(mapPath);
            rf.geraResultSet();
            string xml = rf.getXml();
            string xmlRecordSetRelat = rf.sbXmlDatasetRelat.ToString();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                XmlElement root = doc.DocumentElement;
                String nomeRelatorio = "";
                if (root.HasAttribute("Relatorio"))
                {
                    nomeRelatorio = root.GetAttribute("Relatorio");
                    WebClient client = new WebClient();
                    string url = mapPath + "Relatorios\\" + nomeRelatorio;
                    //string url = mapPath + "servicos\\FaturaPorEmail\\Relatorios\\" + nomeRelatorio;
                    string stringRelatorio = client.DownloadString(url);
                    //string stringRelatorio = System.IO.File.ReadAllText(@url);
                    //stringRelatorio.Replace("www2.copasa.com.br", ConfigurationSettings.AppSettings["urlCodigoBarras"]);
                    stringRelatorio = stringRelatorio.Replace("<Body>", xmlRecordSetRelat + "<Body>");
                    MemoryStream streamRelatorio = new MemoryStream(ASCIIEncoding.Default.GetBytes(stringRelatorio));
                    streamRelatorio.Position = 0;
                    DataSet ds = new DataSet();
                    StringReader sr = new StringReader(xml);
                    XmlReader xmlReader = XmlReader.Create(sr);
                    ds.ReadXml(xmlReader);
                    BindingSource regBindingSource = new BindingSource();
                    regBindingSource.DataMember = "Reg";
                    regBindingSource.DataSource = ds;

                    ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                    reportDataSource1.Name = "Dados_Reg";
                    reportDataSource1.Value = regBindingSource;

                    LocalReport localReport = new LocalReport();
                    localReport.DataSources.Add(reportDataSource1);
                    localReport.EnableExternalImages = true;
                    localReport.LoadReportDefinition(streamRelatorio);

                    try
                    {
                        setPageSettings(localReport);
                        string deviceInfo = CreateEMFDeviceInfo();

                        localReport.Render("PDF", deviceInfo, LocalReportCreateStreamCallback, out warnings);
                    }
                    catch (Exception e)
                    {
                        f.gravaLog("Erro ao gerar arquivo:" + e.Message);
                    }

                    /*
                    Relatorio pd = new Relatorio();
                    pd.m_pages = m_pages;
                    pd.PrinterSettings.PrinterName = "PrimoPDF";
                    pd.Print();
                    */

                    /*
                    SaidaRelat autoprintme = new SaidaRelat(localReport);
                    //autoprintme.m_pages = m_pages;                    
                    //autoprintme.PrinterSettings.PrinterName = "Microsoft XPS Document Writer";
                    autoprintme.PrinterSettings.PrinterName = "PrimoPDF"; 
                    autoprintme.Print();
                    */

                    //printFile(bytes);
                    //printTestePcl();
                    //printFile();
                    //Printing(f.serverMapPath + "Sicom\\saida2.prn");
                }
            }
            catch (Exception ex)
            {
                string exm = ex.Message;
                f.gravaLog("Erro:"+exm);
            }
            return m_pages;
            //return bytes;
        }

        public void setMapPath(string mapPath)
        {
            this.mapPath = mapPath;
        }
        public void setFuncoes(Funcoes f)
        {
            this.f = f;
        }
        /*
            Type type = typeof(env2avia);
            MethodInfo method = type.GetMethod("teste");
            
            //MyReflectionClass c = new MyReflectionClass();
            string result = (string)method.Invoke(this, new object[]{"farofa",5});

         */

        private void printFile()
        {
            byte[] pclData = System.IO.File.ReadAllBytes(f.serverMapPath + "\\testeImpressora.prn");
            //byte[] pclData = f.stringToByteArray(f.readStreamFileSystem("documento.xps").ReadToEnd());
            //    Stream inputStream = f.readStreamFileSystem("documento.xps");
            Socket printerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //printerSocket.Connect("10.1.14.21", 9100); //IP address of printer
            printerSocket.Connect("10.228.1.21", 9100); //IP address of printer pp35
            //printerSocket.Connect("10.228.1.25", 9100); //IP address of printer
            //printerSocket.Connect("10.228.1.41", 9100); //IP address of printer

            StringBuilder pclBuilder = new StringBuilder();
            printerSocket.Send(pclData);

        }

        private void printTestePcl()
        {
            Socket printerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            printerSocket.Connect("10.1.14.21", 9100); //IP address of printer

            StringBuilder pclBuilder = new StringBuilder();
            pclBuilder.Append("\x1BE"); //Reset printer
            pclBuilder.Append("\x1B%-12345X"); //Universal Exit

            pclBuilder.Append("\x1B&l1X"); //1 Copy
            pclBuilder.Append("\x1B&l0S"); //Simplex Printing
            pclBuilder.Append("\x1B&l4A"); //A5 Paper size
            pclBuilder.Append("\x1B&l0O"); //Portrait
            pclBuilder.Append("\x1B&l0E"); //Top margin = 0

            //Draw black rectangle
            pclBuilder.Append("\x1B*p300x400Y"); //Move cursor to 300, 400
            pclBuilder.Append("\x1B*c900A"); //900 wide
            pclBuilder.Append("\x1B*c1500B"); //1500 heigh
            pclBuilder.Append("\x1B*c0P"); //Black fill

            pclBuilder.Append("\x1B&l1T"); //End Job
            pclBuilder.Append("\x1B%-12345X"); //Universal Exit

            byte[] pclData = Encoding.ASCII.GetBytes(pclBuilder.ToString());
            printerSocket.Send(pclData);
        }

        private void printTesteZcl()
        {
            // Printer IP Address and communication port
            string ipAddress = "10.1.14.21";
            int port = 9100;

            // ZPL Command(s)
            string ZPLString =
                "^XA" +
                "^FO50,50" +
                "^A0N50,50" +
                "^FDHello, World!^FS" +
                "^XZ";

            try
            {
                // Open connection
                System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();
                client.Connect(ipAddress, port);

                // Write ZPL String to connection
                System.IO.StreamWriter writer =
            new System.IO.StreamWriter(client.GetStream());
                writer.Write(ZPLString);
                writer.Flush();

                // Close Connection
                writer.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                // Catch Exception
            }
        }

        public void Printing(string filePath)
        {
            try
            {
                streamToPrint = new StreamReader(filePath);
                try
                {
                    printFont = new Font("Arial", 10);
                    PrintDocument pd = new PrintDocument();
                    pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                    // Print the document.
                    pd.Print();
                }
                finally
                {
                    streamToPrint.Close();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        // The PrintPage event is raised for each page to be printed.
        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            String line = null;

            // Calculate the number of lines per page.
            linesPerPage = ev.MarginBounds.Height /
               printFont.GetHeight(ev.Graphics);

            // Iterate over the file, printing each line.
            while (count < linesPerPage &&
               ((line = streamToPrint.ReadLine()) != null))
            {
                yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, Brushes.Black,
                   leftMargin, yPos, new StringFormat());
                count++;
            }

            // If more lines exist, print another page.
            if (line != null)
                ev.HasMorePages = true;
            else
                ev.HasMorePages = false;
        }

        
        private string CreateEMFDeviceInfo()
        {
            PaperSize paperSize = m_pageSettings.PaperSize;
            Margins margins = m_pageSettings.Margins;

            // The device info string defines the page range to print as well as the size of the page.
            // A start and end page of 0 means generate all pages.
            return string.Format(
                CultureInfo.InvariantCulture,
                "<DeviceInfo><OutputFormat>emf</OutputFormat><StartPage>0</StartPage><EndPage>0</EndPage><MarginTop>{0}</MarginTop><MarginLeft>{1}</MarginLeft><MarginRight>{2}</MarginRight><MarginBottom>{3}</MarginBottom><PageHeight>{4}</PageHeight><PageWidth>{5}</PageWidth></DeviceInfo>",
                ToInches(0),
                ToInches(0),
                ToInches(0),
                ToInches(0),
                ToInches(paperSize.Height),
                ToInches(paperSize.Width));
        }

        private static string ToInches(int hundrethsOfInch)
        {
            double inches = hundrethsOfInch / 100.0;
            return inches.ToString(CultureInfo.InvariantCulture) + "in";
        }

        private void setPageSettings(Report report)
        {
            // Set the page settings to the default defined in the report
            //ReportPageSettings reportPageSettings = report.GetDefaultPageSettings();

            // The page settings object will use the default printer unless
            // PageSettings.PrinterSettings is changed.  This assumes there
            // is a default printer.
            m_pageSettings = new PageSettings();
            //m_pageSettings.PaperSize = reportPageSettings.PaperSize;
            //m_pageSettings.Margins = reportPageSettings.Margins;

            
            PaperSize paperSize = new PaperSize("Relatorio", 827, 1169);
            paperSize.RawKind = (int)PaperKind.Custom;
            m_pageSettings.PaperSize = paperSize;

            m_pageSettings.Margins.Left = 100;
            m_pageSettings.Margins.Right = 100;
            m_pageSettings.Margins.Top = 100;
            m_pageSettings.Margins.Bottom = 100;
            
        }

        private Stream LocalReportCreateStreamCallback(
            string name,
            string extension,
            Encoding encoding,
            string mimeType,
            bool willSeek)
        {
            MemoryStream stream = new MemoryStream();            
            m_pages.Add(stream);

            return stream;
        }

    }
}
