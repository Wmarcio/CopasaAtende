using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4CASS - Classe de saída do Cancelamento de Serviço
    /// </summary>
    public class SCN4CASSReceive : BaseModelReceive
    {
        /// <summary>
        /// Código de erro
        /// </summary>
        [XmlElement("_SND-COD-ERRO")]
        public string codigoErro { get; set; }

        /// <summary>
        /// Descrição do erro
        /// </summary>
        [XmlElement("_SND-DESC-ERRO")]
        public string descricaoErro { get; set; }

        /// <summary>
        /// EmailUnidade
        /// </summary>
        [XmlElement("_SND-EMAIL-UNIDADE")]
        public string emailUnidade { get; set; }
    }
}
