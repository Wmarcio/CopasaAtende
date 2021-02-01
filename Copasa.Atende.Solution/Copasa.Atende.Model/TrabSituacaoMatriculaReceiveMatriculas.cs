using Copasa.Atende.Model.Core;
using System.Collections.Generic;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SituacaoMatriculaReceiveMatriculas Dados sobre situação do Imovel.
    /// </summary>
    public class TrabSituacaoMatriculaReceiveMatriculas : BaseModel
    {
        /// <summary>
        /// Matricula.
        /// </summary>
        public string matricula { get; set; }

        /// <summary>
        /// DescricaoSituacao.
        /// </summary>
        public string descricaoSituacao { get; set; }

    }
}
