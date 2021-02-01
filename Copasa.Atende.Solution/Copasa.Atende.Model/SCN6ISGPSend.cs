using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISGPSend - Gera parcelamento de débito
    /// </summary>
    public class SCN6ISGPSend : BaseModelSend
    {
        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_RCV-COD-MATRICULA")]
        public string matricula { get; set; }

        /// <summary>
        /// Identificador.
        /// </summary>
        [XmlElement("_RCV-IDENTIFICADOR-USUARIO")]
        public string identificador { get; set; }

        /// <summary>
        /// ValorEntrada.
        /// </summary>
        [XmlElement("_RCV-VALOR-ENTRADA")]
        public string valorEntrada { get; set; }
        
        /// <summary>
        /// QuantidadeParcelas.
        /// </summary>
        [XmlElement("_RCV-QTDE-PARCELAS")]
        public string quantidadeParcelas { get; set; }

        /// <summary>
        /// Faturas
        /// </summary>
        [XmlArray("Array")]
        [XmlElement("_RCV-FATURAS-SELECIONADAS")]
        public SCN6ISGPSendFaturas[] faturas { get; set; }

        /// <summary>
        /// ValorHistoricoAtivo.
        /// </summary>
        [XmlElement("_RCV-TOTAL-HIST-ATIVO")]
        public string valorHistoricoAtivo { get; set; }

        /// <summary>
        /// ValorTotalHistoricoAtualAtivo.
        /// </summary>
        [XmlElement("_RCV-TOTAL-HIST-ATUAL-ATIVO")]
        public string valorTotalHistoricoAtualAtivo { get; set; }
        
        /// <summary>
        /// ValorTotalHistoricoJurosAtivo.
        /// </summary>
        [XmlElement("_RCV-TOTAL-HIST-JUROS-ATIVO")]
        public string valorTotalHistoricoJurosAtivo { get; set; }

        /// <summary>
        /// ValorTotalHistoricoMultaAtivo.
        /// </summary>
        [XmlElement("_RCV-TOTAL-HIST-MULTA-ATIVO")]
        public string valorTotalHistoricoMultaAtivo { get; set; }
        
        /// <summary>
        /// ValorTotalHistoricoProvisao.
        /// </summary>
        [XmlElement("_RCV-TOTAL-HIST-PROVISAO")]
        public string valorTotalHistoricoProvisao { get; set; }
        
        /// <summary>
        /// ValorTotalHistoricoJurosProvisao. 
        /// </summary>
        [XmlElement("_RCV-TOTAL-HIST-JUROS-PROVISAO")]
        public string valorTotalHistoricoJurosProvisao { get; set; }
        
        /// <summary>
        /// ValorTotalHistoricoAtualProvisao.
        /// </summary>
        [XmlElement("_RCV-TOTAL-HIST-ATUAL-PROVISAO")]
        public string valorTotalHistoricoAtualProvisao { get; set; }
        
        /// <summary>
        /// ValorTotalHistoricoMultaProvisao.
        /// </summary>
        [XmlElement("_RCV-TOTAL-HIST-MULTA-PROVISAO")]
        public string valorTotalHistoricoMultaProvisao { get; set; }
        
        /// <summary>
        /// ValorJurosAtivo.
        /// </summary>
        [XmlElement("_RCV-VALOR-JUROS-ATIVO")]
        public string valorJurosAtivo { get; set; }
        
        /// <summary>
        /// ValorParcelaOriginalAtivo.
        /// </summary>
        [XmlElement("_RCV-VR-PARC-ORIGINAL-ATIVO")]
        public string valorParcelaOriginalAtivo { get; set; }

        /// <summary>
        /// ValorResiduoParcelaOriginalAtivo.
        /// </summary>
        [XmlElement("_RCV-VR-RES-PARC-ORIGINAL-ATIVO")]
        public string valorResiduoParcelaOriginalAtivo { get; set; }
        
        /// <summary>
        /// ValorParcelaJurosAtivo.
        /// </summary>
        [XmlElement("_RCV-VR-PARCELA-JUROS-ATIVO")]
        public string valorParcelaJurosAtivo { get; set; }
        
        /// <summary>
        /// ValorResiduoParcelaJurosAtivo.
        /// </summary>
        [XmlElement("_RCV-VR-RES-PARCELA-JUROS-ATIVO")]
        public string valorResiduoParcelaJurosAtivo { get; set; }
        
        /// <summary>
        /// ValorParcelaJurosOriginalAtivo.
        /// </summary>
        [XmlElement("_RCV-VR-PARC-JUROS-ORIG-ATIVO")]
        public string valorParcelaJurosOriginalAtivo { get; set; }

        /// <summary>
        /// ValorResiduoParcelaJurosOriginalAtivo.
        /// </summary>
        [XmlElement("_RCV-VR-RES-PARC-JUR-ORIG-ATIVO")]
        public string valorResiduoParcelaJurosOriginalAtivo { get; set; }
        
        /// <summary>
        /// ValorParcelaAtualOriginalAtivo.
        /// </summary>
        [XmlElement("_RCV-VR-PARC-ATUAL-ORIG-ATIVO")]
        public string valorParcelaAtualOriginalAtivo { get; set; }

        /// <summary>
        /// ValorResiduoParcelaAtualOriginalAtivo.
        /// </summary>
        [XmlElement("_RCV-VR-RES-PARC-ATUAL-ORIG-ATIV")]
        public string valorResiduoParcelaAtualOriginalAtivo { get; set; }

        /// <summary>
        /// ValorParcelaMultaOriginalAtivo.
        /// </summary>
        [XmlElement("_RCV-VR-PARC-MULTA-ORIG-ATIVO")]
        public string valorParcelaMultaOriginalAtivo { get; set; }

        /// <summary>
        /// ValorResiduoParcelaMultaOriginalAtivo.
        /// </summary>
        [XmlElement("_RCV-VR-RES-PARC-MULTA-ORIG-ATIV")]
        public string valorResiduoParcelaMultaOriginalAtivo { get; set; }
        
        /// <summary>
        /// ValorJurosProvisao.
        /// </summary>
        [XmlElement("_RCV-VALOR-JUROS-PROVISAO")]
        public string valorJurosProvisao { get; set; }

        /// <summary>
        /// ValorParcelaOriginalProvisao.
        /// </summary>
        [XmlElement("_RCV-VR-PARC-ORIG-PROVISAO")]
        public string valorParcelaOriginalProvisao { get; set; }

        /// <summary>
        /// ValorResiduoParcelaOriginalProvisao.
        /// </summary>
        [XmlElement("_RCV-VR-RES-PARC-ORIGINAL-PROV")]
        public string valorResiduoParcelaOriginalProvisao { get; set; }
        
        /// <summary>
        /// ValorParcelaJurosProvisao.
        /// </summary>
        [XmlElement("_RCV-VR-PARCELA-JUROS-PROVISAO")]
        public string valorParcelaJurosProvisao { get; set; }

        /// <summary>
        /// valorResiduoParcelaJurosProvisao.
        /// </summary>
        [XmlElement("_RCV-VR-RES-PARCELA-JUROS-PROV")]
        public string valorResiduoParcelaJurosProvisao { get; set; }

        /// <summary>
        /// ValorParcelaJurosOriginalProvisao.
        /// </summary>
        [XmlElement("_RCV-VR-PARC-JUROS-ORIG-PROVISAO")]
        public string valorParcelaJurosOriginalProvisao { get; set; }

        /// <summary>
        /// ValorResiduoParcelaJurosOriginalProvisao.
        /// </summary>
        [XmlElement("_RCV-VR-RES-PARC-JUR-ORIG-PROV")]
        public string valorResiduoParcelaJurosOriginalProvisao { get; set; }

        /// <summary>
        /// ValorParcelaAtualOriginalProvisao.
        /// </summary>
        [XmlElement("_RCV-VR-PARC-ATUAL-ORIG-PROVISAO")]
        public string valorParcelaAtualOriginalProvisao { get; set; }

        /// <summary>
        /// ValorResiduoParcelaAtualOriginalProvisao.
        /// </summary>
        [XmlElement("_RCV-VR-RES-PARC-ATUAL-ORIG-PROV")]
        public string valorResiduoParcelaAtualOriginalProvisao { get; set; }

        /// <summary>
        /// ValorParcelaMultaOriginalProvisao.
        /// </summary>
        [XmlElement("_RCV-VR-PARC-MULTA-ORIG-PROVISAO")]
        public string valorParcelaMultaOriginalProvisao { get; set; }

        /// <summary>
        /// ValorResiduoParcelaMultaOriginalProvisao.
        /// </summary>
        [XmlElement("_RCV-VR-RES-PARC-MULTA-ORIG-PROV")]
        public string valorResiduoParcelaMultaOriginalProvisao { get; set; }
    }
}
