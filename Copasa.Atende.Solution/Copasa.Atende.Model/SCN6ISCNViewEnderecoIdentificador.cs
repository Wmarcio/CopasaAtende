using Copasa.Atende.Model.Core;
using System.Collections.Generic;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISCNViewEnderecoIdentificador - Certidão negativa de débito
    /// </summary>
    public class SCN6ISCNViewEnderecoIdentificador : BaseModel
    {
        /// <summary>
        /// Matrícula do cliente
        /// </summary>
        public string MatriculaCliente { get; set; }

        /// <summary>
        /// Código de retorno so Sicom
        /// </summary>
        public long CodigoRetorno { get; set; }

        /// <summary>
        /// DescricaoTipoLogradouro
        /// </summary>
        public string descricaoTipoLogradouro { get; set; }

        /// <summary>
        /// Lista de Débitos.
        /// </summary>
        public List<SCN6ISCNViewDebito> Debitos { get; set; }

        /// <summary>
        /// Lista de parcelamentos
        /// </summary>
        public List<SCN6ISCNReceiveParcelamento> Parcelamentos { get; set; }

        /// <summary>
        /// Lista de lançamentos
        /// </summary>
        public List<SCN6ISCNReceiveLancamento> Lancamentos { get; set; }

        /// <summary>
        /// Lista de debitos a vencer
        /// </summary>
        public List<SCN6ISCNViewDebito> DebitosVencer { get; set; }
    }
}
