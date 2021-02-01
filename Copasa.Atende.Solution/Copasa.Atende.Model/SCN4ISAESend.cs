using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISAESend - Gera alteração de economias
    /// </summary>
    public class SCN4ISAESend : BaseModelSend
    {
        /// <summary>
        /// Origem.
        /// </summary>
        [XmlElement("_RCV-FLAG-ORIGEM")]
        public string origem { get; set; }

        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_RCV-COD-MATRICULA")]
        public string matricula { get; set; }

        /// <summary>
        /// NomeSolicitante.
        /// </summary>
        [XmlElement("_RCV-NOME-SOLICITANTE")]
        public string nomeSolicitante { get; set; }

        /// <summary>
        /// NumeroProtocolo.
        /// </summary>
        [XmlElement("_RCV-NUM-PROTOCOLO-CRM")]
        public string numeroProtocolo { get; set; }

        /// <summary>
        /// CodigoServico.
        /// </summary>
        [XmlElement("_RCV-COD-SERVICO")]
        public string codigoServico { get; set; }

        /// <summary>
        /// ClasseServico.
        /// </summary>
        [XmlElement("_RCV-CLASSE-SERVICO")]
        public string classeServico { get; set; }

        /// <summary>
        /// EnviaCRM.
        /// </summary>
        [XmlElement("_RCV-FLAG-ENVIA-CRM")]
        public string EnviaCRM { get; set; }

        /// <summary>
        /// UsuarioGeracao.
        /// </summary>
        [XmlElement("_RCV-USUARIO-GERACAO")]
        public string usuarioGeracao { get; set; }

        /// <summary>
        /// NomeUsuario.
        /// </summary>
        [XmlElement("_RCV-NOME-USUARIO")]
        public string nomeUsuario { get; set; }

        /// <summary>
        /// RuaReferenciaSS.
        /// </summary>
        [XmlElement("_RCV-RUA-REF-SS")]
        public string ruaReferenciaSS { get; set; }

        /// <summary>
        /// DescricaoSSSicom.
        /// </summary>
        [XmlArrayItem("_RCV-DESCRICAO-SS")]
        [XmlArray("Array")]
        [JsonIgnore]
        public string[] descricaoSSSicom { get; set; }

        /// <summary>
        /// DescricaoSS.
        /// </summary>
        public string descricaoSS { get; set; }
    }
}
