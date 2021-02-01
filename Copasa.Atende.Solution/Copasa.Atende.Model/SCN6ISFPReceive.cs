using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISFPReceive Lista faturas pagas
    /// </summary>
    public class SCN6ISFPReceive : BaseModelReceive
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        public SCN6ISFPReceive()
        {
            contas = new List<SCN6ISFPReceiveContas>();
        }

        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_MATRICULA")]
        public string matricula { get; set; }

        /// <summary>
        /// Identificador.
        /// </summary>
        [XmlElement("_IDENT")]
        public string identificador { get; set; }

        /// <summary>
        /// DescricaoRetornoSicom.
        /// </summary>
        [XmlElement("_MENSAGEM")]
        [JsonIgnore]
        public string descricaoRetornoSicom { get; set; }

        /// <summary>
        /// ContasSicom.
        /// </summary>
        [XmlElement("CONTAS")]
        [JsonIgnore]
        public SCN6ISFPReceiveContas[] contasSicom { get; set; }

        /// <summary>
        /// Contas.
        /// </summary>
        public List<SCN6ISFPReceiveContas> contas { get; set; }
    }
}
