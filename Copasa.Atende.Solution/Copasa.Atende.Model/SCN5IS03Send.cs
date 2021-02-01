using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN5IS03Send Informar leitura - Envia leitura CF20
    /// </summary>
    public class SCN5IS03Send : BaseModelSend
    {
        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_MATRICULA")]
        public string matricula { get; set; }

        /// <summary>
        /// NumProtocolo.
        /// </summary>
        [XmlElement("_PROTOCOLO")]
        public string numeroProtocolo { get; set; }

        /// <summary>
        /// NumeroFatura.
        /// </summary>
        [XmlElement("_FATURA")]
        public string numeroFatura { get; set; }

        /// <summary>
        /// DataReferencia.
        /// </summary>
        [XmlElement("_DATA-REFERENCIA")]
        public string dataReferencia { get; set; }

        /// <summary>
        /// DataLeitura.
        /// </summary>
        [XmlElement("_DATA-LEITURA")]
        public string dataLeitura { get; set; }

        /// <summary>
        /// InformarLeitura.
        /// </summary>
        public string informarLeitura { get; set; }

        /// <summary>
        /// CF20.
        /// </summary>
        public string CF20 { get; set; }
        /// <summary>
        /// Solicitante.
        /// </summary>
        [XmlElement("_SOLICITANTE")]
        public string solicitante { get; set; }

        /// <summary>
        /// Telefone.
        /// </summary>
        [XmlElement("_TELEFONE")]
        public string telefone { get; set; }

        /// <summary>
        /// TipoOrigem.
        /// </summary>
        [XmlElement("_FLAG-ORIGEM")]
        [JsonIgnore]
        public string tipoOrigem { get; set; }

        /// <summary>
        /// Origem.
        /// </summary>
        [XmlElement("_IDENT-ORIGEM")]
        public string origem { get; set; }

        /// <summary>
        /// AgenciaUsuario.
        /// </summary>
        public string agenciaUsuario { get; set; }

        /// <summary>
        /// Tabelas
        /// </summary>
        [XmlElement("_TABELAS")]
        public SCN5IS02SendTabelas[] medidores { get; set; }

        /// <summary>
        /// UsuarioInterno.
        /// </summary>
        [JsonIgnore]
        public bool usuarioInterno { get; set; }
    }
}
