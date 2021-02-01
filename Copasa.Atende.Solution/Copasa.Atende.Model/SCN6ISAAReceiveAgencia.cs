using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISAAReceiveAgencia Agências atendimento
    /// </summary>
    public class SCN6ISAAReceiveAgencia : BaseModel
    {
        /// <summary>
        /// Nome da Agência
        /// </summary>
        public string nomeAgencia { get; set; }
        /// <summary>
        /// Unidade de Atendimento
        /// </summary>
        public string unidadeAtendimento { get; set; }
        /// <summary>
        /// Endereço da Agência
        /// </summary>
        public string enderecoAgencia { get; set; }
        /// <summary>
        /// Horário de atendimento da Agência
        /// </summary>
        public string horarioAtendimento { get; set; }
        /// <summary>
        /// Telefone da Agência 
        /// </summary>
        public string telefoneAgencia { get; set; }
    }
}