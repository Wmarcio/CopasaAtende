using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365
{
    /// <summary>
    /// SCN5ISHCReceive Histórico de consumo
    /// </summary>
   public class DHistoricoConsumoBarra : BaseModelReceive
    {
            /// <summary>
            /// Ocorrencias.
            /// </summary>
            public List<DHistoricoConsumoBarraItem> ocorrencias { get; set; }        
    }

    /// <summary>
    /// SCN5ISHCReceiveDados Histórico de consumo
    /// </summary>
    public class DHistoricoConsumoBarraItem : BaseModel
    {
        /// <summary>
        /// Referencia.
        /// </summary>
        public string referencia { get; set; }

        /// <summary>
        /// leitura do hidrometro
        /// </summary>
        public string leitura { get; set; }

        /// <summary>
        /// DataLeitura.
        /// </summary>
        public string dataLeitura { get; set; }

        /// <summary>
        /// Volume.
        /// </summary>
        public string volume { get; set; }

        /// <summary>
        /// MediaConsumo.
        /// </summary>
        public string mediaConsumo { get; set; }

        /// <summary>
        /// Valor.
        /// </summary>
        public string valor { get; set; }

        /// <summary>
        /// DataVencimento.
        /// </summary>
        public string dataVencimento { get; set; }

        /// <summary>
        /// DataPagamento.
        /// </summary>
        public string dataPagamento { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CodigoBarra { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string CodigoBarraFormatado { get; set; }
    }

}
