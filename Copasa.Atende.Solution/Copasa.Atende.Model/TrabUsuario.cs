using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// TrabUsuario - Dados do usuário solicitante do serviço
    /// </summary>
    public class TrabUsuario : BaseModel
    {
        /// <summary>
        /// CodigoUsuario.
        /// </summary>
        public string codigoUsuario { get; set; }

        /// <summary>
        /// NomeUsuario.
        /// </summary>
        public string nomeUsuario { get; set; }

        /// <summary>
        /// UnidadeUsuario.
        /// </summary>
        public string agenciaUsuario { get; set; }
    }
}
