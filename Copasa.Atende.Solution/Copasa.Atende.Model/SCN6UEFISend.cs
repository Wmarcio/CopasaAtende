using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6UEFISend - Informa identificador e retorna matriculas
    /// </summary>
    public class SCN6UEFISend : BaseModel
    {
        /// <summary>
        /// TipoProcessamento.
        /// </summary>
        [Broker("tipoProcessamento", 1, "A1")]
        public long tipoProcessamento { get; set; }

        /// <summary>
        /// Identificador.
        /// </summary>
        [Broker("identificador", 2, "N11")]
        public long identificador { get; set; }

        /// <summary>
        /// FlagExcecao.
        /// </summary>
        [Broker("flagExcecao", 3, "A1")]
        public string flagExcecao { get; set; }

    }
}
