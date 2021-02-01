using Copasa.Atende.Model.Core;
using System.Collections.Generic;

namespace Copasa.Atende.WebService.Models
{
    /// <summary>
    /// Classe para a nova certidão negativa de débito
    /// </summary>
    public class CertidaoNegativaViewModel : BaseModel
    {
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public CertidaoNegativaViewModel()
        {
            identificadores = new List<CertidaoNegativaIdentificadorViewModel>();
        }

        /// <summary>
        /// Mensagem de Retorno.
        /// </summary>        
        public string mensagemRetorno { get; set; }

        /// <summary>
        /// Texto Corpo.
        /// </summary>
        public string textoCorpo { get; set; }

        /// <summary>
        /// Local.
        /// </summary>
        public string local { get; set; }

        /// <summary>
        /// Texto Débitos.
        /// </summary>
        public string textoDebitos { get; set; }

        /// <summary>
        /// Texto Débitos a Vencer.
        /// </summary>
        public string textoDebitosVencer { get; set; }

        /// <summary>
        /// Texto Parcelamentos.
        /// </summary>
        public string textoParcelamentos { get; set; }

        /// <summary>
        /// Texto Lançamentos.
        /// </summary>
        public string textoLancamentos { get; set; }

        /// <summary>
        /// Lista de matrículas e endereços
        /// </summary>
        public List<CertidaoNegativaIdentificadorViewModel> identificadores { get; set; }

        /// <summary>
        /// Texto do rodapé da página
        /// </summary>
        public string textoRodape { get; set; }

        /// <summary>
        /// Texto complementar
        /// </summary>
        public string textoComplementar { get; set; }

        /// <summary>
        /// TextoComplementar 2
        /// </summary>
        public string textoComplementar2 { get; set; }

    }
}