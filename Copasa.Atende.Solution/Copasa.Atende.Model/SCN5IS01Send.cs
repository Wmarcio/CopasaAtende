using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN5IS01Send Informar leitura - Entrada Matrícula
    /// </summary>
    public class SCN5IS01Send : BaseModelSend    {
        ///// <summary>
        ///// Nome do Programa
        ///// </summary>
        //[XmlElement("_NOME-PGM")]
        //public string nomePrograma { get; set; }
        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_MATRICULA")]
        public string matricula { get; set; }

    }
}
