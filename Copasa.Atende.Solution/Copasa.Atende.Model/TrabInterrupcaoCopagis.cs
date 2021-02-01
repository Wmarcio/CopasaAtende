using Copasa.Atende.Model.Core;
using System;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// InterrupcaoCopagis - Dados provenientes do CopaGis sobre interrupções de água
    /// </summary>
    public class TrabInterrupcaoCopagis : BaseModel
    {
        /// <summary>
        /// Código da Localidade e do Bairro.
        /// </summary>
        public string CodigoLocalidadeBairro { get; set; }

        /// <summary>
        /// Objetivo.
        /// </summary>
        public string Objetivo { get; set; }

        /// <summary>
        /// Data Início Previsto.
        /// </summary>
        public DateTime DtInicio { get; set; }

        /// <summary>
        /// Data Fim Previsto.
        /// </summary>
        public DateTime DtFim { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Distrito { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? DtNormalizacao { get; set; }

        /// <summary>
        /// Descrição da solicitação
        /// </summary>
        public string Descricao { get; set; }
    }
}
