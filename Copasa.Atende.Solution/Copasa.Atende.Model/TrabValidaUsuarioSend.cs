using Copasa.Atende.Model.Core;
using Newtonsoft.Json;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// TrabValidaUsuarioSend - Valida Usuário
    /// </summary>
    public class TrabValidaUsuarioSend: BaseModelSend
    {
        /// <summary>
        /// CPF/CPNJ
        /// </summary>
        public string CpfCnpj { get; set; }

        /// <summary>
        /// Identificador
        /// </summary>
        public string identificador{ get; set; }
    }
}
