using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// ORAMensagem - Mensagens de retorno do sistema
    /// </summary>
    public class ORAMensagem : BaseModel
    {
        /// <summary>
        /// IdMensagem.
        /// </summary>
        public string idMensagem { get; set; }

        /// <summary>
        /// DescricaoServico.
        /// </summary>
        public string descricaoServico { get; set; }

        /// <summary>
        /// DescricaoMensagem.
        /// </summary>
        public string descricaoMensagem { get; set; }
    }
}
