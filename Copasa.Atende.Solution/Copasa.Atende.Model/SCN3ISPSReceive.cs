using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN3ISPSReceive - Lista pontos serviço
    /// </summary>
    public class SCN3ISPSReceive : BaseModelReceive
    {
        /// <summary>
        /// CodigoRetornoSicom.
        /// </summary>
        [XmlElement("_COD-ERRO-O")]
        [JsonIgnore]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// MensagemRetornoSicom.
        /// </summary>
        [XmlElement("_DESC-ERRO-O")]
        [JsonIgnore]
        public string descricaoRetornoSicom { get; set; }
        /// <summary>
        /// PontosServicoAguaSicom
        /// </summary>
        [XmlElement("_TABELA-PS-AGUA-O")]
        [JsonIgnore]
        public SCN3ISPSReceivePSAgua[] pontosServicoAguaSicom { get; set; }

        /// <summary>
        /// PontosServicoAgua
        /// </summary>
        public List<SCN3ISPSReceivePSAgua> pontosServicoAgua { get; set; }

        /// <summary>
        /// PontosServicoEsgotoSicom.
        /// </summary>
        [XmlElement("_TABELA-PS-ESGOTO")]
        [JsonIgnore]
        public SCN3ISPSReceivePSEsgoto[] pontosServicoEsgotoSicom { get; set; }

        /// <summary>
        /// PontosServicoEsgoto.
        /// </summary>
        public List<SCN3ISPSReceivePSEsgoto> pontosServicoEsgoto { get; set; }

        /// <summary>
        /// PontosServicoFonteAlternativaSicom.
        /// </summary>
        [XmlElement("_TABELA-PS-FONTE-ALTERNATIVA")]
        [JsonIgnore]
        public SCN3ISPSReceiveFonteAlternativa[] pontosServicoFonteAlternativaSicom { get; set; }

        /// <summary>
        /// PontosServicoFonteAlternativa.
        /// </summary>
        public List<SCN3ISPSReceiveFonteAlternativa> pontosServicoFonteAlternativa { get; set; }

        /// <summary>
        /// PontosServicoDeducaoEsgotoSicom.
        /// </summary>
        [XmlElement("_TABELA-PS-DEDUCAO-ESGOTO")]
        [JsonIgnore]
        public SCN3ISPSReceiveDeducaoEsgoto[] pontosServicoDeducaoEsgotoSicom { get; set; }

        /// <summary>
        /// PontosServicoDeducaoEsgoto.
        /// </summary>
        public List<SCN3ISPSReceiveDeducaoEsgoto> pontosServicoDeducaoEsgoto { get; set; }

        /// <summary>
        /// PontosServicoMedicaoEsgotoSicom.
        /// </summary>
        [XmlElement("_TABELA-PS-MEDICAO-ESGOTO")]
        [JsonIgnore]
        public SCN3ISPSReceiveMedicaoEsgoto[] pontosServicoMedicaoEsgotoSicom { get; set; }

        /// <summary>
        /// PontosServicoMedicaoEsgoto.
        /// </summary>
        public List<SCN3ISPSReceiveMedicaoEsgoto> pontosServicoMedicaoEsgoto { get; set; }

    }
}
