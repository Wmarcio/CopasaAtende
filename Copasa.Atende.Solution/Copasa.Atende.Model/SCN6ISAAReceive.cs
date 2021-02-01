using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISAAReceive Lista agências atendimento
    /// </summary>
    public class SCN6ISAAReceive : BaseModelReceive
    {
        /// <summary>
        /// Código do Erro
        /// </summary>
        [XmlElement("_SND-COD-ERRO")]
        public string codigoErro { get; set; }

        /// <summary>
        /// Agências Atendimento Sicom
        /// </summary>
        [XmlElement("_SND-TABELA-AGENCIAS")]
        [JsonIgnore]
        public SCN6ISAAReceiveAgenciaSicom[] agenciasAtendimentoSicom { get; set; }

        /// <summary>
        /// Agências Atendimento 
        /// </summary>
        public List<SCN6ISAAReceiveAgencia> agenciasAtendimento { get; set; }
    }
}
