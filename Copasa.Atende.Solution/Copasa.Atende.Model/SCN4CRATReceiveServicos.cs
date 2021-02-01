using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4CRATSend - Enviar serviço
    /// </summary>
    public class SCN4CRATReceiveServicos : BaseModel
    {
        /// <summary>
        /// TipoInformacaoSS.
        /// </summary>
        [XmlElement("_SND-TIPO-INF-SS")]
        public string tipoInformacaoSS { get; set; }

        /// <summary>
        /// CPF.
        /// </summary>
        [XmlElement("_SND-CPF")]
        public string CPF { get; set; }

        /// <summary>
        /// CNPJ.
        /// </summary>
        [XmlElement("_SND-CNPJ")]
        public string CNPJ { get; set; }

        /// <summary>
        /// IdentidadeUsuario.
        /// </summary>
        [XmlElement("_SND-IDENT-USUARIO")]
        public string identidadeUsuario { get; set; }

        /// <summary>
        /// MatriculaImovel.
        /// </summary>
        [XmlElement("_SND-MATRICULA-IMOVEL")]
        public string matriculaImovel { get; set; }

        /// <summary>
        /// NomeUsuario.
        /// </summary>
        [XmlElement("_SND-NOME-USUARIO")]
        public string nomeUsuario { get; set; }

        /// <summary>
        /// CodigoLocalidade.
        /// </summary>
        [XmlElement("_SND-COD-LOCALIDADE")]
        public string codigoLocalidade { get; set; }

        /// <summary>
        /// CodigoLogradouro.
        /// </summary>
        [XmlElement("_SND-COD-LOGRADOURO")]
        public string codigoLogradouro { get; set; }

        /// <summary>
        /// NumeroLogradouro.
        /// </summary>
        [XmlElement("_SND-NUM-IMOVEL")]
        public string numeroLogradouro { get; set; }

        /// <summary>
        /// TipoComplementoLogradouro.
        /// </summary>
        [XmlElement("_SND-TIPO-COMPL")]
        public string tipoComplementoLogradouro { get; set; }

        /// <summary>
        /// ComplementoLogradouro.
        /// </summary>
        [XmlElement("_SND-COMPL-IMOVEL")]
        public string complementoLogradouro { get; set; }

        /// <summary>
        /// NomeSolicitante.
        /// </summary>
        [XmlElement("_SND-NOME-SOLICITANTE")]
        public string nomeSolicitante { get; set; }

        /// <summary>
        /// CodigoBairro.
        /// </summary>
        [XmlElement("_SND-COD-BAIRRO")]
        public string codigoBairro { get; set; }

        /// <summary>
        /// TelefoneSolicitante.
        /// </summary>
        [XmlElement("_SND-FONE-SOLICITANTE")]
        public string telefoneSolicitante { get; set; }

        /// <summary>
        /// ReferenciaEndereco.
        /// </summary>
        [XmlElement("_SND-REFER-ENDERECO")]
        public string referenciaEndereco { get; set; }

        /// <summary>
        /// NumeroProtocoloSS.
        /// </summary>
        [XmlElement("_SND-NUM-PROTOCOLO-SS")]
        public string numeroProtocoloSS { get; set; }

        /// <summary>
        /// NumeroSolicitacaoServico.
        /// </summary>
        [XmlElement("_SND-NUM-SS")]
        public string numeroSolicitacaoServico { get; set; }

        /// <summary>
        /// CodigoServicoSS.
        /// </summary>
        [XmlElement("_SND-COD-SERVICO-SS")]
        public string codigoServicoSS { get; set; }

        /// <summary>
        /// DescricaoServicoSS.
        /// </summary>
        [XmlElement("_SND-DESC-SERVICO-SS")]
        public string descricaoServicoSS { get; set; }

        /// <summary>
        /// DataGeracaoSS.
        /// </summary>
        [XmlElement("_SND-DATA-GERACAO-SS")]
        public string dataGeracaoSS { get; set; }

        /// <summary>
        /// HoraGeracaoSS.
        /// </summary>
        [XmlElement("_SND-HORA-GERACAO-SS")]
        public string horaGeracaoSS { get; set; }

        /// <summary>
        /// DataPrevisaoSS.
        /// </summary>
        [XmlElement("_SND-DATA-PREVISAO-SS")]
        public string dataPrevisaoSS { get; set; }

        /// <summary>
        /// HoraPrevisaoSS.
        /// </summary>
        [XmlElement("_SND-HORA-PREVISAO-SS")]
        public string horaPrevisaoSS { get; set; }

        /// <summary>
        /// SituacaoSS.
        /// </summary>
        [XmlElement("_SND-SITUACAO-SS")]
        public string situacaoSS { get; set; }

        /// <summary>
        /// DescricaoSituacaoSS.
        /// </summary>
        [XmlElement("_SND-DESC-SITUACAO-SS")]
        public string descricaoSituacaoSS { get; set; }

        /// <summary>
        /// ObservacaoSS.
        /// </summary>
        [XmlElement("_SND-OBS-SS")]
        public string observacaoSS { get; set; }

        /// <summary>
        /// TipoInformacaoOS.
        /// </summary>
        [XmlElement("_SND-TIPO-INF-OS")]
        public string tipoInformacaoOS { get; set; }

        /// <summary>
        /// NumeroProtocoloOS.
        /// </summary>
        [XmlElement("_SND-NUM-PROTOCOLO-OS")]
        public string numeroProtocoloOS { get; set; }

        /// <summary>
        /// NumeroSolicServicoOS.
        /// </summary>
        [XmlElement("_SND-NUM-SS-OS")]
        public string numeroSolicServicoOS { get; set; }

        /// <summary>
        /// NumeroOrdemServico.
        /// </summary>
        [XmlElement("_SND-NUM-OS")]
        public string numeroOrdemServico { get; set; }

        /// <summary>
        /// CodigoServicoOS.
        /// </summary>
        [XmlElement("_SND-COD-SERVICO-OS")]
        public string codigoServicoOS { get; set; }

        /// <summary>
        /// DescricaoServicoOS.
        /// </summary>
        [XmlElement("_SND-DESC-SERVICO-OS")]
        public string descricaoServicoOS { get; set; }

        /// <summary>
        /// DataGeracaoOS.
        /// </summary>
        [XmlElement("_SND-DATA-GERACAO-OS")]
        public string dataGeracaoOS { get; set; }

        /// <summary>
        /// HoraGeracaoOS.
        /// </summary>
        [XmlElement("_SND-HORA-GERACAO-OS")]
        public string horaGeracaoOS { get; set; }

        /// <summary>
        /// DataPrevisaoOS.
        /// </summary>
        [XmlElement("_SND-DATA-PREVISAO-OS")]
        public string dataPrevisaoOS { get; set; }

        /// <summary>
        /// HoraPrevisaoOS.
        /// </summary>
        [XmlElement("_SND-HORA-PREVISAO-OS")]
        public string horaPrevisaoOS { get; set; }

        /// <summary>
        /// SituacaoOS.
        /// </summary>
        [XmlElement("_SND-SITUACAO-OS")]
        public string situacaoOS { get; set; }

        /// <summary>
        /// DescricaoSituacaoOS.
        /// </summary>
        [XmlElement("_SND-DESC-SITUACAO-OS")]
        public string descricaoSituacaoOS { get; set; }

    }
}