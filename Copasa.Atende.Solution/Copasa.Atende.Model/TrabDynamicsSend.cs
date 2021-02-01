using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// DynamicsSend - Contém os dados para serem enviados na chamada ao web service nativo do sistema Dynamics
    /// </summary>
    public class TrabDynamicsSend : BaseModel
    {
        /// <summary>
        /// Method.
        /// </summary>
        public string method { get; set; }

        /// <summary>
        /// Host.
        /// </summary>
        public string host { get; set; }

        /// <summary>
        /// Servico.
        /// </summary>
        public string servico { get; set; }

        /// <summary>
        /// Authorization.
        /// </summary>
        public string authorization { get; set; }

        /// <summary>
        /// ContenType.
        /// </summary>
        public string contenType { get; set; }

        /// <summary>
        /// Accept.
        /// </summary>
        public string accept { get; set; }

        /// <summary>
        /// Content.
        /// </summary>
        public string content { get; set; }

    }
}
