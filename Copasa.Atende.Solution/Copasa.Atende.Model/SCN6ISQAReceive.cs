using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISQAReceive - Quitação anual de débito
    /// </summary>
    public class SCN6ISQAReceive : BaseModelReceive
    {
        /// <summary>
        /// CodigoRetornoSicom.
        /// </summary>
        [XmlElement("_SND-COD-RETORNO")]
        [JsonIgnore]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// DescricaoRetornoSicom.
        /// </summary>
        [XmlElement("_SND-DESC-ERRO")]
        [JsonIgnore]
        public string descricaoRetornoSicom { get; set; }

        /// <summary>
        /// PossuiFaturaEmDebito.
        /// </summary>
        [XmlElement("_SND-FLAG-FATURA-EM-DEBITO")]
        public string possuiFaturaEmDebito { get; set; }

        /// <summary>
        /// CodigoLogradouro.
        /// </summary>
        [XmlElement("_SND-COD-LOGRADOURO")]
        public string codigoLogradouro { get; set; }

        /// <summary>
        /// TipoLogradouro.
        /// </summary>
        [XmlElement("_SND-TIPO-LOGRADOURO")]
        public string tipoLogradouro { get; set; }

        /// <summary>
        /// NomeLogradouro.
        /// </summary>
        [XmlElement("_SND-NOME-LOGRADOURO")]
        public string nomeLogradouro { get; set; }

        /// <summary>
        /// NumeroImovel.
        /// </summary>
        [XmlElement("_SND-NUM-IMOVEL")]
        public string numeroImovel { get; set; }

        /// <summary>
        /// TipoComplemento.
        /// </summary>
        [XmlElement("_SND-TP-COMPLEMENTO")]
        public string tipoComplementoImovel { get; set; }

        /// <summary>
        /// ComplementoImovel.
        /// </summary>
        [XmlElement("_SND-COMPL-INFO")]
        public string complementoImovel { get; set; }

        /// <summary>
        /// CodigoBairro.
        /// </summary>
        [XmlElement("_SND-COD-BAIRRO")]
        public string codigoBairro { get; set; }

        /// <summary>
        /// NomeBairro.
        /// </summary>
        [XmlElement("_SND-NOME-BAIRRO")]
        public string nomeBairro { get; set; }

        /// <summary>
        /// CodigoLocalidade.
        /// </summary>
        [XmlElement("_SND-COD-LOCALIDADE")]
        public string codigoLocalidade { get; set; }

        /// <summary>
        /// NomeLocalidade.
        /// </summary>
        [XmlElement("SND-NOME-LOCALIDADE")]
        public string nomeLocalidade { get; set; }

        /// <summary>
        /// CepImovel.
        /// </summary>
        [XmlElement("_SND-CEP-IMOVEL")]
        public string cepImovel { get; set; }

        /// <summary>
        /// DescricaoRetornoSicom.
        /// </summary>
        [XmlElement("_SCN-FATURAS-EM-DEBITO")]
        public SCN6ISQAReceiveFaturaEmDebito[] faturasEmDebito { get; set; }

        /// <summary>
        /// Texto Corpo.
        /// </summary>
        public string textoCorpo { get; set; }

        /// <summary>
        /// Texto Consideracoes.
        /// </summary>
        public string textoConsideracoes { get; set; }

        /// <summary>
        /// Texto Debito.
        /// </summary>
        public string textoDebito { get; set; }

        /// <summary>
        /// Local.
        /// </summary>
        public string local { get; set; }

        /// <summary>
        /// Deferido ou indeferido
        /// </summary>
        public string deferimento { get; set; }
    }
}
