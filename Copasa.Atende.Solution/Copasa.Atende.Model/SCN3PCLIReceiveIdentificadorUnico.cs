using Newtonsoft.Json;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN3PCLIReceiveDadosUsuario - Dados básicos cliente - Usuario
    /// </summary>
    public class SCN3PCLIReceiveIdentificadorUnico : SCN3PCLIReceiveIdentificador
    {
        /// <summary>
        /// DescricaoRetorno.
        /// </summary>
        public string descricaoRetorno { get; set; }

    }
}
