using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISAAReceiveAgencia Agências atendimento
    /// </summary>
    public class SCN6ISAAReceiveAgenciaSicom : BaseModel
    {
        /// <summary>
        /// Nome da Agência
        /// </summary>
        [XmlElement("_SND-NOME-AGENCIA")]
        public string nomeAgencia { get; set; }

        /// <summary>
        /// Unidade de Atendimento
        /// </summary>
        [XmlElement("_SND-UNID-ATEND")]
        public string unidadeAtendimento { get; set; }

        /// <summary>
        /// Endereço da Agência
        /// </summary>
        [XmlElement("_SND-END-AGENCIA1")]
        public string enderecoAgencia1 { get; set; }

        /// <summary>
        /// Endereço da Agência 2
        /// </summary>
        [XmlElement("_SND-END-AGENCIA2")]
        public string enderecoAgencia2 { get; set; }

        /// <summary>
        /// Horário de atendimento da Agência
        /// </summary>
        [XmlElement("_SND-HORARIO-ATENDIMENTO")]
        public string horarioAtendimento { get; set; }

        /// <summary>
        /// Telefone da Agência 
        /// </summary>
        [XmlElement("_SND-TEL-AGENCIA")]
        public string telefoneAgencia { get; set; }
    }
}