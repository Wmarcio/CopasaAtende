using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365
{
    /// <summary>
    /// Lista das doze últimas faturas do cliente
    /// </summary>
    public class DUltimasFaturasReceive : BaseModelReceive
    {
        /// <summary>
        /// Matricula
        /// </summary>
        public string Matricula { get; set; }

        /// <summary>
        /// Identificador
        /// </summary>
        public string Identificador { get; set; }

        /// <summary>
        /// Lista das Faturas
        /// </summary>
       public List<Fatura> Faturas { get; set; }
    }

    /// <summary>
    /// Objeto da lista
    /// </summary>
    public class Fatura
    {
        /// <summary>
        /// mes/ano de referencia
        /// </summary>
        public string Referencia  { get; set; }

        /// <summary>
        /// numero da fatura
        /// </summary>
        public string NumeroFatura { get; set; }

        /// <summary>
        /// Valor da fatura
        /// </summary>
        public string ValorFatura { get; set; }

        /// <summary>
        /// data de vencimento
        /// </summary>
        public DateTime DataVencimento { get; set; }

        #region debitos
        
        /// <summary>
        /// Emitida S/N
        /// </summary>
        public string  Emitida { get; set; }

        /// <summary>
        /// tem cf20 S/N
        /// </summary>
        public string TemCF20 { get; set; }

        /// <summary>
        /// codigo de barras formtado
        /// </summary>
        public string NumeroCodigoBarrasFormatado { get; set; }

        /// <summary>
        /// numero do codigo de barras
        /// </summary>
        public string NumeroCodigoBarras { get; set; }

        #endregion

        #region pagas

        /// <summary>
        /// Codigo do banco
        /// </summary>
        public string CodigoBanco { get; set; }

        /// <summary>
        /// Retificada
        /// </summary>
        public string Retificada { get; set; }

        /// <summary>
        /// DataPagamento.
        /// </summary>
        public string DataPagamento { get; set; }
        #endregion

    }
}
