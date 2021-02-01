using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISPDReceiveParcelas - Consulta parcelamento de débito
    /// </summary>
    public class SCN6ISPDReceiveParcelas : BaseModel
    {
        /// <summary>
        /// QuantidadeParcelasFinal
        /// </summary>
        [XmlElement("_SND-QTDE-PARCELAS-FINAL")]
        public string quantidadeParcelasFinal { get; set; }

        /// <summary>
        /// ValorParcelaFinal
        /// </summary>
        [XmlElement("_SND-VALOR-PARCELA-FINAL")]
        public string valorParcela { get; set; }

        /// <summary>
        /// ValorTotalFinal
        /// </summary>
        [XmlElement("_SND-VALOR-TOTAL-FINAL")]
        public string valorTotal { get; set; }

        /// <summary>
        /// ValorJurosFinal
        /// </summary>
        [XmlElement("_SND-VALOR-JUROS-FINAL")]
        public string valorJurosFinal { get; set; }

        /// <summary>
        /// ValorJurosAtivo
        /// </summary>
        [XmlElement("_SND-VALOR-JUROS-ATIVO")]
        public string valorJurosAtivo { get; set; }

        /// <summary>
        /// ValorParcelaOriginalAtivo
        /// </summary>
        [XmlElement("_SND-VR-PARC-ORIGINAL-ATIVO")]
        public string valorParcelaOriginalAtivo { get; set; }

        /// <summary>
        /// ValorResiduoParcelaOriginalAtivo
        /// </summary>
        [XmlElement("_SND-VR-RES-PARC-ORIGINAL-ATIVO")]
        public string valorResiduoParcelaOriginalAtivo { get; set; }

        /// <summary>
        /// ValorParcelaJurosAtivo
        /// </summary>
        [XmlElement("_SND-VR-PARCELA-JUROS-ATIVO")]
        public string valorParcelaJurosAtivo { get; set; }

        /// <summary>
        /// ValorResiduoParcelaJurosAtivo
        /// </summary>
        [XmlElement("_SND-VR-RES-PARCELA-JUROS-ATIVO")]
        public string valorResiduoParcelaJurosAtivo { get; set; }

        /// <summary>
        /// ValorParcelaJurosOriginalAtivo
        /// </summary>
        [XmlElement("_SND-VR-PARC-JUROS-ORIG-ATIVO")]
        public string valorParcelaJurosOriginalAtivo { get; set; }

        /// <summary>
        /// ValorResiduoParcelaJurosOriginalAtivo
        /// </summary>
        [XmlElement("_SND-VR-RES-PARC-JUR-ORIG-ATIVO")]
        public string valorResiduoParcelaJurosOriginalAtivo { get; set; }

        /// <summary>
        /// ValorParcelaAtualOriginalAtivo
        /// </summary>
        [XmlElement("_SND-VR-PARC-ATUAL-ORIG-ATIVO")]
        public string valorParcelaAtualOriginalAtivo { get; set; }

        /// <summary>
        /// ValorResiduoParcelaAtualOriginalAtivo
        /// </summary>
        [XmlElement("_SND-VR-RES-PARC-ATUAL-ORIG-ATIV")]
        public string valorResiduoParcelaAtualOriginalAtivo { get; set; }

        /// <summary>
        /// ValorParcelaMultaOriginalAtivo
        /// </summary>
        [XmlElement("_SND-VR-PARC-MULTA-ORIG-ATIVO")]
        public string valorParcelaMultaOriginalAtivo { get; set; }

        /// <summary>
        /// ValorResiduoParcelaMultaOriginalAtivo
        /// </summary>
        [XmlElement("_SND-VR-RES-PARC-MULTA-ORIG-ATIV")]
        public string valorResiduoParcelaMultaOriginalAtivo { get; set; }

        /// <summary>
        /// ValorJurosProvisao
        /// </summary>
        [XmlElement("_SND-VALOR-JUROS-PROVISAO")]
        public string valorJurosProvisao { get; set; }

        /// <summary>
        /// ValorParcelaOriginalProvisao
        /// </summary>
        [XmlElement("_SND-VR-PARC-ORIG-PROVISAO")]
        public string valorParcelaOriginalProvisao { get; set; }

        /// <summary>
        /// ValorResiduoParcelaOriginalProvisao
        /// </summary>
        [XmlElement("_SND-VR-RES-PARC-ORIGINAL-PROV")]
        public string valorResiduoParcelaOriginalProvisao { get; set; }

        /// <summary>
        /// ValorParcelaJurosProvisao
        /// </summary>
        [XmlElement("_SND-VR-PARCELA-JUROS-PROVISAO")]
        public string valorParcelaJurosProvisao { get; set; }

        /// <summary>
        /// ValorResiduoParcelaJurosProvisao
        /// </summary>
        [XmlElement("_SND-VR-RES-PARCELA-JUROS-PROV")]
        public string valorResiduoParcelaJurosProvisao { get; set; }

        /// <summary>
        /// ValorParcelaJurosOriginalProvisao
        /// </summary>
        [XmlElement("_SND-VR-PARC-JUROS-ORIG-PROVISAO")]
        public string valorParcelaJurosOriginalProvisao { get; set; }

        /// <summary>
        /// ValorResiduoParcelaJurosOriginalProvisao
        /// </summary>
        [XmlElement("_SND-VR-RES-PARC-JUR-ORIG-PROV")]
        public string valorResiduoParcelaJurosOriginalProvisao { get; set; }

        /// <summary>
        /// ValorParcelaAtualOriginalProvisao
        /// </summary>
        [XmlElement("_SND-VR-PARC-ATUAL-ORIG-PROVISAO")]
        public string valorParcelaAtualOriginalProvisao { get; set; }

        /// <summary>
        /// ValorResiduoParcelaAtualOriginalProvisao
        /// </summary>
        [XmlElement("_SND-VR-RES-PARC-ATUAL-ORIG-PROV")]
        public string valorResiduoParcelaAtualOriginalProvisao { get; set; }

        /// <summary>
        /// ValorParcelaMultaOriginalProvisao
        /// </summary>
        [XmlElement("_SND-VR-PARC-MULTA-ORIG-PROVISAO")]
        public string valorParcelaMultaOriginalProvisao { get; set; }

        /// <summary>
        /// valorResiduoParcelaMuldaOriginalProvisao
        /// </summary>
        [XmlElement("_SND-VR-RES-PARC-MULTA-ORIG-PROV")]
        public string ValorResiduoParcelaMuldaOriginalProvisao { get; set; }

    }
}
