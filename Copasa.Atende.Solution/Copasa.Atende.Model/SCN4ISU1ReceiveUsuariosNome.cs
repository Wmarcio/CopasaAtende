using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISU1ReceiveUsuariosNome Busca dados do cliente
    /// </summary>
    public class SCN4ISU1ReceiveUsuariosNome : BaseModel
    {
        /// <summary>
        /// CpfCnpj.
        /// </summary>
        public string cpfCnpj { get; set; }

        /// <summary>
        /// NomeUsuario.
        /// </summary>
        public string nomeUsuario { get; set; }

    }
}
