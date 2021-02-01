using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365
{
    /// <summary>
    /// Informações para a registro de imagem do vazamento na tabela annotations
    /// </summary>
    public class DImagemVazamentoSend : BaseModel
    {
        /// <summary>
        /// Tipo Vazamento - Vazamento na Rua/Imóvel
        /// </summary>
        [Dyn365Name("subject")]
        public string TipoVazamento { get; set; }

        /// <summary>
        /// Id do Protocolo para associação
        /// </summary>
        [Dyn365Name("objectid_incident@odata.bind")]
        public string IdIncident{ get; set; }

        /// <summary>
        /// Nome do arquivo em png
        /// </summary>
        [Dyn365Name("filename")]
        public string NomeArquivo { get; set; }

        /// <summary>
        /// String da imagem na Base64
        /// </summary>
        [Dyn365Name("documentbody")]
        public string ImgBase64 { get; set; }
    }
}
