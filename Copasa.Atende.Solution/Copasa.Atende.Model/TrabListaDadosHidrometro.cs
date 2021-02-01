using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// TrabListaDadosHidrometro Lista dados de hidrômetro
    /// </summary>
    public class TrabListaDadosHidrometro : BaseModelReceive
    {
        /// <summary>
        /// CodigoRetornoSicom.
        /// </summary>
        [XmlElement("_COD-MSG")]
        [JsonIgnore]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// DtProgramacaoLeitura.
        /// </summary>
        [XmlElement("_DT-PROGRAMACAO-LEITURA")]
        public string dtProgramacaoLeitura { get; set; }

        /// <summary>
        /// DtProgramacao.
        /// </summary>
        [XmlElement("_DT-PROGRAMACAO")]
        public string dtProgramacao { get; set; }

        /// <summary>
        /// ind.
        /// </summary>
        [XmlElement("_IND")]
        public string ind { get; set; }

        /// <summary>
        /// Refer.
        /// </summary>
        [XmlElement("_REFER")]
        public string refer { get; set; }

        /// <summary>
        /// CF20.
        /// </summary>
        [XmlElement("_CF20")]
        public string CF20 { get; set; }

        /// <summary>
        /// Fatura.
        /// </summary>
        [XmlElement("_FATURA")]
        public string fatura { get; set; }

        /// <summary>
        /// InformaLeitura.
        /// </summary>
        [XmlElement("_INFORMA-LEITURA")]
        public string informaLeitura { get; set; }

        /// <summary>
        /// Tabelas
        /// </summary>
        [XmlElement("_TABELAS")]
        [JsonIgnore]
        public SCN5IS01ReceiveTabelas[] tabelas { get; set; }

        /// <summary>
        /// Lista de Hidromêtros
        /// </summary>
        public List<SCN5IS01ReceiveTabelas> medidores { get; set; }

        /// <summary>
        /// CodigoServicoInsumo
        /// </summary>
        public string codigoServicoInsumo { get; set; }

        /// <summary>
        /// ClasseServico
        /// </summary>
        public string classeServico { get; set; }

        /// <summary>
        /// EnviaCRM
        /// </summary>
        public string enviaCRM { get; set; }

        /// <summary>
        /// ValorServico
        /// </summary>
        public string valorServico { get; set; }
    }
}
