using Copasa.Atende.Model.Core;
using Newtonsoft.Json;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// TrabValidaUsuarioReceive - Valida Usuário
    /// </summary>
    public class TrabValidaUsuarioReceive : BaseModelReceive
    {
        /// <summary>
        /// CPF/CPNJ
        /// </summary>
        public string CpfCnpj { get; set; }

        /// <summary>
        /// Identificador
        /// </summary>
        public string identificador { get; set; }
    }
}
