using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISU1Receive Busca dados do cliente
    /// </summary>
    public class SCN4ISU1Receive : BaseModelReceive
    {
        /// <summary>
        /// descricaoRetornoSicom.
        /// </summary>
        [XmlElement("_CRM-DESC-ERRO-O")]
        [JsonIgnore]
        public string descricaoRetornoSicom { get; set; }

        /// <summary>
        /// Usuarios
        /// </summary>
        [XmlElement("_CRM-USUARIOS-O")]
        [JsonIgnore]
        public SCN4ISU1ReceiveUsuarios[] usuariosIS { get; set; }

        /// <summary>
        /// Matriculas.
        /// </summary>
        public List<SCN4ISU1ReceiveMatriculas> matriculas { get; set; }

    }
}
