using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// Dyn365ProtocoloURA - Tabela de protocolo/incident com dados da URA no Dynamics
    /// </summary>
    public class Dyn365ProtocoloURASend : BaseModel
    {
        /// <summary>
        /// CpfCnpj.
        /// </summary>
        public string cpfCnpj { get; set; }

        /// <summary>
        /// IDSolicitante.
        /// </summary>
        public string idSolicitante { get; set; }

        /// <summary>
        /// IDProtocolo.
        /// </summary>
        public string idProtocolo { get; set; }

        /// <summary>
        /// NumeroProtocolo.
        /// </summary>
        public string numeroProtocolo { get; set; }

        /// <summary>
        /// Identificador.
        /// </summary>
        public string identificador { get; set; }

        /// <summary>
        /// Matricula.
        /// </summary>
        public string matricula { get; set; }

        /// <summary>
        /// TipoOcorrencia.
        /// </summary>
        public string tipoOcorrencia { get; set; }

        /// <summary>
        /// CodigoServico.
        /// </summary>
        public string codigoServico { get; set; }

        /// <summary>
        /// InformarProtocolo.
        /// </summary>
        public string informarProtocolo { get; set; }

        /// <summary>
        /// ProtocoloRelacionado.
        /// </summary>
        public string protocoloRelacionado { get; set; }

        /// <summary>
        /// CodigoLocalidade.
        /// </summary>
        public string codigoLocalidade { get; set; }

        /// <summary>
        /// TipoLogradouro.
        /// </summary>
        public string tipoLogradouro { get; set; }

        /// <summary>
        /// CodigoLogradouro.
        /// </summary>
        public string codigoLogradouro { get; set; }

        /// <summary>
        /// CodigoBairro.
        /// </summary>
        public string codigoBairro { get; set; }

        /// <summary>
        /// NumeroImovel.
        /// </summary>
        public string numeroImovel { get; set; }

        /// <summary>
        /// TipoComplementoImovel.
        /// </summary>
        public string tipoComplementoImovel { get; set; }

        /// <summary>
        /// ComplementoImovel.
        /// </summary>
        public string complementoImovel { get; set; }

        /// <summary>
        /// EntreRuas.
        /// </summary>
        public string entreRuas { get; set; }

        /// <summary>
        /// DescricaoSolicitacao.
        /// </summary>
        public string descricaoSolicitacao { get; set; }

        /// <summary>
        /// InformacoesAtendente.
        /// </summary>
        public string informacoesAtendente { get; set; }

        /// <summary>
        /// SituacaoProtocolo.
        /// </summary>
        public string situacaoProtocolo { get; set; }

        /// <summary>
        /// SituacaoSolicitacao.
        /// </summary>
        public string situacaoSolicitacao { get; set; }

        /// <summary>
        /// Titulo.
        /// </summary>
        public string titulo { get; set; }

        /// <summary>
        /// TelefoneURA.
        /// </summary>
        public string telefoneURA { get; set; }

        /// <summary>
        /// NomeSolicitante.
        /// </summary>
        public string nomeSolicitante { get; set; }

        /// <summary>
        /// TelefoneSolicitante.
        /// </summary>
        public string telefoneSolicitante { get; set; }

        /// <summary>
        /// EmailSolicitante.
        /// </summary>
        public string emailSolicitante { get; set; }
    }
}
