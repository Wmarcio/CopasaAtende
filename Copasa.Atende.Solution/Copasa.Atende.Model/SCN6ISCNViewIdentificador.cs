using Copasa.Atende.Model.Core;
using System.Collections.Generic;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISCNViewIdentificador - Certidão negativa de débito
    /// </summary>
    public class SCN6ISCNViewIdentificador : BaseModel
    {
        /// <summary>
        /// Identificador do usuário
        /// </summary>
        public string IdentificadorCliente { get; set; }

        /// <summary>
        /// Lista dos endereços dos clientes
        /// </summary>
        public List<SCN6ISCNViewEnderecoIdentificador> Enderecos { get; set; }
    }
}
