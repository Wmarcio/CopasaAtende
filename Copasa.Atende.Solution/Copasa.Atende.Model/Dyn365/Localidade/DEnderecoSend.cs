using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365.Localidade
{
    /// <summary>
    /// Paramentros de entrada do endereço
    /// </summary>
    public class DEnderecoSend : BaseModel
    {
        /// <summary>
        /// Nome da localidade
        /// </summary>
        public string Localidade { get; set; }

        /// <summary>
        /// Nome do bairro
        /// </summary>
        public string Bairro { get; set; }

        /// <summary>
        /// Descrção do logradouro
        /// </summary>
        public string Logradouro { get; set; }

        /// <summary>
        /// Npme da Empresa (COPASA/COPANOR)
        /// </summary>
        public string Empresa { get; set; }

        /// <summary>
        /// Tipo vazamento (ESGOTO/AGUA)
        /// </summary>
        public string TipoVazamento { get; set; }

    }
}
