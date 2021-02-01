using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISPDReceive - Consulta parcelamento de débito
    /// </summary>
    public class SCN6ISPDReceive : BaseModelReceive
    {
        /// <summary>
        /// Código de erro do Sicom
        /// </summary>
        [XmlElement("_SND-COD-ERRO")]
        [JsonIgnore]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// ValorTotalFatura
        /// </summary>
        [XmlElement("_SND-TOTAL-FATURA")]
        public string valorTotalFatura { get; set; }

        /// <summary>
        /// ValorTotalAtual
        /// </summary>
        [XmlElement("_SND-TOTAL-ATUAL-MON")]
        public string valorTotalAtual { get; set; }

        /// <summary>
        /// ValorTotalJuros
        /// </summary>
        [XmlElement("_SND-TOTAL-JUROS")]
        public string valorTotalJuros { get; set; }

        /// <summary>
        /// ValorTotalMulda
        /// </summary>
        [XmlElement("_SND-TOTAL-MULTA")]
        public string valorTotalMulta { get; set; }

        /// <summary>
        /// ValorTotalParcelamento
        /// </summary>
        [XmlElement("_SND-TOTAL-PARCELAMENTO")]
        public string valorTotalParcelamento { get; set; }

        /// <summary>
        /// ValorEntradaMinimo
        /// </summary>
        [XmlElement("_SND-VALOR-ENTRADA-MINIMO")]
        public string valorEntradaMinimo { get; set; }

        /// <summary>
        /// DetalhesParcelas
        /// </summary>
        public List<SCN6ISPDReceiveParcelas> detalhesParcelas { get; set; }

        /// <summary>
        /// DetalhesParcelasSicom
        /// </summary>
        [XmlElement("_SND-DETALHES-PARCELAS")]
        [JsonIgnore]
        public SCN6ISPDReceiveParcelas[] detalhesParcelasSicom { get; set; }

        /// <summary>
        /// Categoria
        /// </summary>
        [XmlElement("_SND-CATEGORIA")]
        public string categoria { get; set; }

        /// <summary>
        /// ValorTotalHistoricoAtivo
        /// </summary>
        [XmlElement("_SND-TOTAL-HIST-ATIVO")]
        public string valorTotalHistoricoAtivo { get; set; }

        /// <summary>
        /// ValorTotalHistoricoAtualAtivo
        /// </summary>
        [XmlElement("_SND-TOTAL-HIST-ATUAL-ATIVO")]
        public string valorTotalHistoricoAtualAtivo { get; set; }

        /// <summary>
        /// ValorTotalHistoricoJurosAtivo
        /// </summary>
        [XmlElement("_SND-TOTAL-HIST-JUROS-ATIVO")]
        public string valorTotalHistoricoJurosAtivo { get; set; }

        /// <summary>
        /// valorTotalHistoricoMultaAtivo.
        /// </summary>
        [XmlElement("_SND-TOTAL-HIST-MULTA-ATIVO")]
        public string valorTotalHistoricoMultaAtivo { get; set; }

        /// <summary>
        /// ValorTotalHistoricoProvisao.
        /// </summary>
        [XmlElement("_SND-TOTAL-HIST-PROVISAO")]
        public string valorTotalHistoricoProvisao { get; set; }

        /// <summary>
        /// ValorTotalHistoricoJurosProvisao.
        /// </summary>
        [XmlElement("_SND-TOTAL-HIST-JUROS-PROVISAO")]
        public string valorTotalHistoricoJurosProvisao { get; set; }

        /// <summary>
        /// ValorTotalHistoricoAtualProvisao.
        /// </summary>
        [XmlElement("_SND-TOTAL-HIST-ATUAL-PROVISAO")]
        public string valorTotalHistoricoAtualProvisao { get; set; }

        /// <summary>
        /// ValorTotalHistoricoMultaProvisao
        /// </summary>
        [XmlElement("_SND-TOTAL-HIST-MULTA-PROVISAO")]
        public string valorTotalHistoricoMultaProvisao { get; set; }

        /// <summary>
        /// FaturasSicom.
        /// </summary>
        [XmlElement("_SND-FATURAS-SELECIONADAS")]
        [JsonIgnore]
        public SCN6ISPDReceiveFatura[] faturasSicom { get; set; }

        /// <summary>
        /// Faturas.
        /// </summary>
        public List<SCN6ISPDReceiveFatura> faturas { get; set; }

    }
}
