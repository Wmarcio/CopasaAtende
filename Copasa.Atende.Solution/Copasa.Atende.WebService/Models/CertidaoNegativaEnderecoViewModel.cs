using System.Collections.Generic;

namespace Copasa.Atende.WebService.Models
{
    /// <summary>
    /// Classe referente aos endereços da certidão negativa de débito
    /// </summary>
    public class CertidaoNegativaEnderecoViewModel
    {
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public CertidaoNegativaEnderecoViewModel()
        {
            Debitos = new List<CertidaoNegativaDebitoViewModel>();
            Parcelamentos = new List<CertidaoNegativaParcelamentoViewModel>();
            Lancamentos = new List<CertidaoNegativaLancamentoViewModel>();
            DebitosVencer = new List<CertidaoNegativaDebitoViewModel>();
        }
        /// <summary>
        /// Descrição do tipo de logradouro
        /// </summary>
        public string descricaoTipoLogradouro { get; set; }

        /// <summary>
        /// Matrícula do cliente
        /// </summary>
        public string MatriculaCliente { get; set; }

        /// <summary>
        /// Código de retorno so Sicom
        /// </summary>
        public long CodigoRetorno { get; set; }

        /// <summary>
        /// Identificador do usuário
        /// </summary>
        public string IdentificadorCliente { get; set; }

        /// <summary>
        /// Lista de Débitos.
        /// </summary>
        public List<CertidaoNegativaDebitoViewModel> Debitos { get; set; }

        /// <summary>
        /// Lista de parcelamentos
        /// </summary>
        public List<CertidaoNegativaParcelamentoViewModel> Parcelamentos { get; set; }

        /// <summary>
        /// Lista de lançamentos
        /// </summary>
        public List<CertidaoNegativaLancamentoViewModel> Lancamentos { get; set; }

        /// <summary>
        /// Lista de debitos a vencer
        /// </summary>
        public List<CertidaoNegativaDebitoViewModel> DebitosVencer { get; set; }
    }
}