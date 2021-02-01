using System.Collections.Generic;

namespace Copasa.Atende.WebService.Models
{
    /// <summary>
    /// Classe referente aos identificadores
    /// </summary>
    public class CertidaoNegativaIdentificadorViewModel
    {
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public CertidaoNegativaIdentificadorViewModel()
        {
            Enderecos = new List<CertidaoNegativaEnderecoViewModel>();
        }
        /// <summary>
        /// Identificador do usuário
        /// </summary>
        public string IdentificadorCliente { get; set; }

        /// <summary>
        /// Lista dos endereços dos clientes
        /// </summary>
        public List<CertidaoNegativaEnderecoViewModel> Enderecos { get; set; }
    }
}