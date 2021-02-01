using Copasa.Atende.Model.Core;
using System.Collections.Generic;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// TrabPesquisaFaltaAguaReceive Verifica se há falta de água
    /// </summary>
    public class TrabPesquisaFaltaAguaReceive : BaseModelRetorno
    {
        /// <summary>
        /// DescricaoSituacao.
        /// </summary>
        public string descricaoSituacao { get; set; }

    }
}
