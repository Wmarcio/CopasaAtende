using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISPDSend - Consulta parcelamento de débito
    /// </summary>
    public class SCN6ISPDSend : BaseModelSend
    {
        /// <summary>
        /// Matricula
        /// </summary>
        [XmlElement("_RCV-COD-MATRICULA")]
        public string matricula { get; set; }

        /// <summary>
        /// Identificador
        /// </summary>
        [XmlElement("_RCV-IDENTIFICADOR-USUARIO")]
        public string identificador { get; set; }

        /// <summary>
        /// ConsiderarFaturaEmAberto
        /// </summary>
        [XmlElement("_RCV-CONSIDERAR-FAT-EM-ABERTO")]
        public string considerarFaturaEmAberto { get; set; }

        /// <summary>
        /// AlteraSomatorioFatura
        /// </summary>
        [XmlElement("_RCV-SEGUNDO-ACESSO")]
        public string alteraSomatorioFatura { get; set; }

        /// <summary>
        /// ValorEntrada
        /// </summary>
        [XmlElement("_RCV-VALOR-ENTRADA")]
        public string valorEntrada { get; set; }

        /// <summary>
        /// Categoria
        /// </summary>
        [XmlElement("_RCV-CATEGORIA")]
        public string categoria { get; set; }

        /// <summary>
        /// ValorTotalHistAtivo
        /// </summary>
        [XmlElement("_RCV-TOTAL-HIST-ATIVO")]
        public string valorTotalHistAtivo { get; set; }

        /// <summary>
        /// ValorTotalHistoricoAtualAtivo
        /// </summary>
        [XmlElement("_RCV-TOTAL-HIST-ATUAL-ATIVO")]
        public string valorTotalHistoricoAtualAtivo { get; set; }

        /// <summary>
        /// ValorTotalHistoricoJurosAtivo
        /// </summary>
        [XmlElement("_RCV-TOTAL-HIST-JUROS-ATIVO")]
        public string valorTotalHistoricoJurosAtivo { get; set; }

        /// <summary>
        /// ValorTotalHistoricoMultaAtivo
        /// </summary>
        [XmlElement("_RCV-TOTAL-HIST-MULTA-ATIVO")]
        public string valorTotalHistoricoMultaAtivo { get; set; }

        /// <summary>
        /// ValorTotalHistoricoProvisao
        /// </summary>
        [XmlElement("_RCV-TOTAL-HIST-PROVISAO")]
        public string valorTotalHistoricoProvisao { get; set; }

        /// <summary>
        /// ValorTotalHistoricoJurosProvisao
        /// </summary>
        [XmlElement("_RCV-TOTAL-HIST-JUROS-PROVISAO")]
        public string valorTotalHistoricoJurosProvisao { get; set; }

        /// <summary>
        /// ValorTotalHistoricoAtualProvisao
        /// </summary>
        [XmlElement("_RCV-TOTAL-HIST-ATUAL-PROVISAO")]
        public string valorTotalHistoricoAtualProvisao { get; set; }

        /// <summary>
        /// ValorTotalHistoricoMultaProvisao
        /// </summary>
        [XmlElement("_RCV-TOTAL-HIST-MULTA-PROVISAO")]
        public string valorTotalHistoricoMultaProvisao { get; set; }

    }
}
