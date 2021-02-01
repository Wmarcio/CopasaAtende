using Copasa.Atende.Model.Core;
using Newtonsoft.Json;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6UEFIReceiveMatriculas - Informa identificador e retorna matriculas
    /// </summary>
    public class SCN6UEFIReceiveMatriculas : BaseModel
    {
        /// <summary>
        /// Matricula.
        /// </summary>
        public string matricula { get; set; }

        /// <summary>
        /// Endereco.
        /// </summary>
        public string endereco { get; set; }

        /// <summary>
        /// inicio.
        /// </summary>
        public string inicio { get; set; }

        /// <summary>
        /// Termino.
        /// </summary>
        public string termino { get; set; }

        /// <summary>
        /// Bairro.
        /// </summary>
        public string bairro { get; set; }

        /// <summary>
        /// Localidade.
        /// </summary>
        public string localidade { get; set; }
    }
}
