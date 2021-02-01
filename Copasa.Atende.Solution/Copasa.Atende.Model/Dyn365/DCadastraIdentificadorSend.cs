using Copasa.Atende.Model.Core;
using Newtonsoft.Json;

namespace Copasa.Atende.Model.Dyn365
{
    /// <summary>
    /// CreateDyn365IdentifierSend - Cria um Novo Identificador no Microsoft Dynamics 365.
    /// </summary>
    public class DCadastraIdentificadorSend : BaseModel
    {
        /// <summary>
        /// Usuário do Dyn365
        /// </summary>
        [Dyn365Name("username")]
        public string Username { get; set; }

        /// <summary>
        /// Senha do Usr do Dyn365 criptografada
        /// </summary>
        [Dyn365Name("password")]
        public string Password { get; set; }

        /// <summary>
        /// Código do Identificador
        /// </summary>
        [Dyn365Name("copasa_codigo")]
        public string CopasaCodigo { get; set; }
    }
}
