using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN3ISMTReceive - Busca matrícula pelo endereço
    /// </summary>
    public class SCN3ISMTReceive : BaseModelReceive
    {
        /// <summary>
        /// MatriculasSicom.
        /// </summary>
        [XmlElement("_SND-COD-ERRO")]
        [JsonIgnore]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// MatriculasSicom.
        /// </summary>
        [XmlElement("_TAB-MATRICULAS-ATUAIS")]
        [JsonIgnore]
        public SCN3ISMTReceiveMatricula[] matriculasSicom { get; set; }

        /// <summary>
        /// Matriculas.
        /// </summary>
        public List<SCN3ISMTReceiveMatricula> matriculas { get; set; }
    }
}
