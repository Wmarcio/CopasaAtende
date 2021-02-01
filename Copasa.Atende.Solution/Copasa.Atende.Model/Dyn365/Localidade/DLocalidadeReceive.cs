using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365.Localidade
{
    /// <summary>
    /// Retorno da busca de localidade
    /// </summary>
    public class DLocalidadeReceive : BaseModelReceive
    {
        /// <summary>
        /// lista de localidades
        /// </summary>
        public List<DLocalidade> Localidades { get; set; }
    }

    /// <summary>
    /// Item da lista
    /// </summary>
    public class DLocalidade : BaseModel
    {
        /// <summary>
        /// nome da localidade.
        /// </summary>
        [Dyn365Id("copasa_name")]
        public string LocalidadeNome { get; set; }

        /// <summary>
        /// codigo da localidade.
        /// </summary>
        [Dyn365Id("copasa_codigo")]
        public string LocalidadeCodigo { get; set; }

        /// <summary>
        /// id da localidade.
        /// </summary>
        [Dyn365Id("copasa_localidadeid")]
        public string LocalidadeId { get; set; }
    }
}
