using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365.Localidade
{
    /// <summary>
    /// Retorno da códigos do bairro e localidade
    /// </summary>
    public class DEnderecoReceive : BaseModelReceive
    {
        /// <summary>
        /// codigo da localidade.
        /// </summary>
        [Dyn365Id("codigo_localidade")]
        public string LocalidadeCodigo { get; set; }

        /// <summary>
        /// id da localidade.
        /// </summary>
        [Dyn365Id("id_localidade")]
        public string LocalidadeId { get; set; }

        /// <summary>
        /// codigo.
        /// </summary>
        [Dyn365Id("codigo_bairro")]
        public string BairroCodigo { get; set; }

        /// <summary>
        /// id.
        /// </summary>
        [Dyn365Id("id_bairro")]
        public string BairroId { get; set; }

        /// <summary>
        /// codigo.
        /// </summary>
        [Dyn365Id("codigo_logradouro")]
        public string LogradouroCodigo { get; set; }

        /// <summary>
        /// id.
        /// </summary>
        [Dyn365Id("id_logradouro")]
        public string LogradouroId { get; set; }

    }
}
