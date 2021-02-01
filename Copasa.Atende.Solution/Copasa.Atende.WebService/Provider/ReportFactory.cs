using System;
using System.Data;
using Microsoft.Reporting.WinForms;
using System.Xml;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing;
using System.Globalization;
using System.Collections.Generic;
using Copasa.Atende.Util;
using System.Net;
using System.Web;
using System.Web.Caching;

namespace Copasa.Atende.WebService.Provider
{
    public class ReportFactory
    {
        private string mapPath;
        private Funcoes f;

        private PageSettings m_pageSettings;
        private List<Stream> m_pages = new List<Stream>();


        public List<Stream> getReport(string dados, string nomeRelat)
        {
            Warning[] warnings =null;
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
                if (root.HasAttribute("Relatorio"))
                {
                    string nomeArquivoRelatorio = root.GetAttribute("Relatorio");
                    string stringRelatorio = getStringRelatorio(xmlRecordSetRelat, nomeRelat, nomeArquivoRelatorio);
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
                        //setPageSettings(localReport);
                        
                        string deviceInfo = CreateEMFDeviceInfo();
                        localReport.Render("PDF", deviceInfo, LocalReportCreateStreamCallback, out warnings);
                       
                    }
                    catch (Exception e){}
                }
            }
            catch (Exception ){}
            return m_pages;
        }

        public void setMapPath(string mapPath)
        {
            this.mapPath = mapPath;
        }
        public void setFuncoes(Funcoes f)
        {
            this.f = f;
        }
 
        private string getStringRelatorio(string xmlRecordSetRelat, string nome, string nomeArquivoRelatorio)
        {
            string stringRelatorio = null;
            if (HttpContext.Current != null && HttpContext.Current.Cache != null)
            {
                if (HttpContext.Current.Cache[nome] == null)
                {
                    WebClient client = new WebClient();
                    string url = mapPath + "\\Views\\SegundaViaFatura\\" + nomeArquivoRelatorio;
                    stringRelatorio = client.DownloadString(url);
                    stringRelatorio = stringRelatorio.Replace("<Body>", xmlRecordSetRelat + "<Body>");

                    DateTime expiration = DateTime.Now.AddHours(5);
                    HttpContext.Current.Cache.Add(nome, stringRelatorio, null, expiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                else
                {
                    stringRelatorio = (string)HttpContext.Current.Cache[nome];
                }
            }
            if (stringRelatorio == null)
            {
                WebClient client = new WebClient();
                string url = mapPath + "\\Views\\SegundaViaFatura\\" + nomeArquivoRelatorio;
                stringRelatorio = client.DownloadString(url);
                stringRelatorio = stringRelatorio.Replace("<Body>", xmlRecordSetRelat + "<Body>");
            }
            return stringRelatorio;
        }
        private string CreateEMFDeviceInfo()
        {
            m_pageSettings = new PageSettings();
            PaperSize paperSize = new PaperSize("Relatorio", 827, 1169);
            paperSize.RawKind = (int)PaperKind.Custom;
            m_pageSettings.PaperSize = paperSize;

            m_pageSettings.Margins.Left = 100;
            m_pageSettings.Margins.Right = 100;
            m_pageSettings.Margins.Top = 100;
            m_pageSettings.Margins.Bottom = 100;
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

        private string ToInches(int hundrethsOfInch)
        {
            double inches = hundrethsOfInch / 100.0;
            return inches.ToString(CultureInfo.InvariantCulture) + "in";
        }

        private void setPageSettings(Report report)
        {
            // Set the page settings to the default defined in the report
            ReportPageSettings reportPageSettings = report.GetDefaultPageSettings();

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
