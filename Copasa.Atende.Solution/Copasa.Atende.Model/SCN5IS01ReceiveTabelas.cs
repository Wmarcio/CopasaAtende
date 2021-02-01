using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN5IS01ReceiveTabelas Informar leitura - Entrada Matrícula / Tabelas
    /// </summary>
    public class SCN5IS01ReceiveTabelas : BaseModel
    {
        /// <summary>
        /// NumMedidorABNT.
        /// </summary>
        [XmlElement("_NUM-MEDIDOR-ABNT")]
        public string numMedidorABNT { get; set; }

        /// <summary>
        /// LeituraAnterior.
        /// </summary>
        [XmlElement("_LEITURA-ANTERIOR")]
        public string leituraAnterior { get; set; }

        /// <summary>
        /// LeituraMinima.
        /// </summary>
        [XmlElement("_LEITURA-MINIMA")]
        public string leituraMinima { get; set; }

        /// <summary>
        /// LeituraMaxima.
        /// </summary>
        [XmlElement("_LEITURA-MAXIMA")]
        public string leituraMaxima { get; set; }

        /// <summary>
        /// LeituraVirada.
        /// </summary>
        [XmlElement("_LEITURA-VIRADA")]
        public string leituraVirada { get; set; }

        /// <summary>
        /// LeituraReal.
        /// </summary>
        [XmlElement("_LEITURA-REAL")]
        public string leituraReal { get; set; }

        /// <summary>
        /// DataLeitura.
        /// </summary>
        [XmlElement("_DATA-LEITURA")]
        public string dataLeitura { get; set; }

    }
}
