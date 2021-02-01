namespace Copasa.Atende.Model.Core
{
    /// <summary>
    /// Classe Base das Entidades para envio de dados para o IBM
    /// </summary>
    public abstract class BaseModelSend : BaseModel
    {
        /// <summary>
        /// Empresa.
        /// </summary>
        public string empresa { get; set; }

        /// <summary>
        /// Protocolo.
        /// </summary>
        public string protocolo { get; set; }

        /// <summary>
        /// Origem
        /// </summary>
        public string Origem { get; set; }
    }
}
