using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365
{
    /// <summary>
    /// informações para a abertura de uma solicitação de servico para vazamento de rua
    /// </summary>
    public class DSolicitaServicoSend : BaseModel
    {
        /// <summary>
        /// CPF/CNPJ
        /// </summary>
        public string CpfCnpj { get; set; }

        /// <summary>
        /// CPF/CNPJ
        /// </summary>
        public string IdCpfCnpj { get; set; }

        /// <summary>
        /// Matricula
        /// </summary>
        public string Matricula { get; set; }

        /// <summary>
        /// identificador
        /// </summary>
        public string Identificador { get; set; }

        /// <summary>
        /// nome
        /// </summary>
        public string NomeUsuario { get; set; }


        /// <summary>
        /// nome
        /// </summary>
        public string NomeSolicitante { get; set; }

        /// <summary>
        /// telefone
        /// </summary>
        public string TelefoneSolicitante { get; set; }

        /// <summary>
        /// referencia do endereço
        /// </summary>
        public string Referencia { get; set; }

        /// <summary>
        /// observação sobre o serviço
        /// </summary>
        public string Observacao { get; set; }

        /// <summary>
        /// VAZ
        /// </summary>
        public string Servico { get; set; }

        /// <summary>
        /// agua = 14800/esgoto = 31500
        /// </summary>
        public string Subtipo { get; set; }

        /// <summary>
        /// pavimentação(depende do subtipo)
        /// </summary>
        public string Pavimentacao { get; set; }

        /// <summary>
        /// codigo SICOM
        /// </summary>
        public string CodigoLocalidade { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public string IdLocalidade { get; set; }

        /// <summary>
        /// codigo SICOM
        /// </summary>
        public string CodigoBairro { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public string IdBairro { get; set; }

        /// <summary>
        /// codigo SICOM
        /// </summary>
        public string CodigoLogradouro { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public string IdLogradouro { get; set; }

        /// <summary>
        /// Número no logradouro
        /// </summary>
        public string NumeroLogradouro { get; set; }

        /// <summary>
        /// complemento
        /// </summary>
        public string Complemento { get; set; }

        /// <summary>
        /// protocolo(caso de vazamento na rua)
        /// </summary>
        public string NumeroProtocolo { get; set; }

        /// <summary>
        /// protocolo id(caso de vazamento na rua)
        /// </summary>
        public string IdProtocolo { get; set; }

        /// <summary>
        /// Empresa
        /// </summary>
        public string Empresa { get; set; }

        /// <summary>
        /// Origem
        /// </summary>
        public string Origem { get; set; }

        /// <summary>
        /// String no formato base64 que representa a imagem 
        /// </summary>
        public string ImagemBase64 { get; set; }

        /// <summary>
        /// Nome do arquivo em png
        /// </summary>
        public string NomeArquivo { get; set; }


        /// <summary>
        /// Tipo de Vazamento - Rua ou no Imóvel
        /// </summary>
        public string TipoVazamento { get; set; }

        /// <summary>
        /// Descrição da Solicitação
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// CEP
        /// </summary>
        public string Cep { get; set; }

        /// <summary>
        /// Tipo de Logradouro
        /// </summary>
        public string TipoLogradouro { get; set; }


        /// <summary>
        /// Complemento
        /// </summary>
        public string TipoComplemento { get; set; }
    }
}
