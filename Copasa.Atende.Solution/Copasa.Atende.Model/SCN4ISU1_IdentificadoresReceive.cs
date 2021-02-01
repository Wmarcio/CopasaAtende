using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISU1_IdentificadoresReceive Busca dados do cliente sem array de matrículas
    /// </summary>
    public class SCN4ISU1_IdentificadoresReceive : BaseModelReceive
    {
        /// <summary>
        /// descricaoRetornoSicom.
        /// </summary>
        [XmlElement("_CRM-DESC-ERRO-O")]
        [JsonIgnore]
        public string descricaoRetornoSicom { get; set; }

        /// <summary>
        /// IdentificadoresSicom
        /// </summary>
        [XmlElement("_CRM-USUARIOS-O")]    
        [JsonIgnore]
        public SCN4ISU1ReceiveUsuarios[] identificadoresSicom { get; set; }

        /// <summary>
        /// Identificadores
        /// </summary>
        public List<SCN4ISU1ReceiveUsuarios> identificadores{ get; set; }
    }
}
