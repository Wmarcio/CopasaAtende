using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;
using Copasa.Atende.Util.Constantes;
using Copasa.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Caching;

namespace Copasa.Atende.Repository.Infrastructure
{
    /// <summary>
    /// MensagemRepository.
    /// </summary>
    public class MensagemRepository : IMensagemRepository
    {
        private IORAMensagemRepository _oRAMensagemRepository;
        private ILog _log;

        private string TEXTO_COMPL_FUNCAO41_SICOM = " Caso seja necessário gerar nova solicitação utilize a função 4.1 do SICOM.";
        private string TEXTO_COMPL_NUMERO_PROTOCOLO =  " Número do protocolo <T!numeroProtocoloSS>.";
        private string TEXTO_COMPL_PREVISAO_EXECUCAO = " A previsão de execução é dia <D!dataPrevisaoSS>.";
        private string TEXTO_COMPL_PREVISAO_SOLUCAO = " A previsão de solução é dia <D!dataPrevisaoSS>.";
        private string TEXTO_COMPL_CONFIRMACAO_ENDERECO = " A solicitacao aberta é de Número: <T!numeroSolicitacaoServico> - no endereço: <T!nomeLogradouroSS>,<T!numeroImovelSS> <T!nomeBairroSS>. Favor confirmar com o solicitante se é o mesmo local de sua reclamação";

        private int PRAZO_VENCIDO = 1;
        private int PRAZO_NAO_VENCIDO = 2;
        private int PROTOCOLO_COM = 3;
        private int PROTOCOLO_SEM = 4;

        private int USUARIO_INTERNO = 0;
        private int USUARIO_EXTERNO = 1;
        private int USUARIO_TODOS = 2;

        private List<TrabMensagemCorrespondenciaSicomOracle> _correspondenciaSicomOracle;

        /// <summary>
        /// Construtor - BuscaMensagem.
        /// </summary>
        /// <param name="oRAMensagemRepository">IORAMensagensRepository.</param>
        /// <param name="log">ILog</param>
        public MensagemRepository(IORAMensagemRepository oRAMensagemRepository, ILog log)
        {
            _oRAMensagemRepository = oRAMensagemRepository;
            _log = log;

            _correspondenciaSicomOracle = new List<TrabMensagemCorrespondenciaSicomOracle>();
            carregarCorrespondenciaSicomOracle();
        }

        /// <summary>
        /// Gera mensagem
        /// </summary>
        public string geraMensagemComDataPrazo(int idMensagem, string dataPrevisao, BaseModel baseModel)
        {
            int noPrazo = PRAZO_NAO_VENCIDO;
            if (dataPrevisao != null && !"".Equals(dataPrevisao) && !"0".Equals(dataPrevisao) && dataPrevisao.ToDateTime("yyyyMMdd") < DateTime.Today)
                noPrazo = PRAZO_VENCIDO;

            return verificaCodigoCorrespondente(idMensagem, PROTOCOLO_SEM, USUARIO_TODOS, baseModel);
        }

        /// <summary>
        /// Gera mensagem
        /// </summary>
        public string geraMensagemComDataPrazoEProtocolo(int idMensagem, string dataPrevisao, bool usuarioInterno,BaseModel baseModel)
        {
            int tipoUsuario = USUARIO_EXTERNO;
            if (usuarioInterno)
                tipoUsuario = USUARIO_INTERNO;
            int noPrazo = PRAZO_NAO_VENCIDO;
            if (dataPrevisao != null && !"".Equals(dataPrevisao) && !"0".Equals(dataPrevisao) && dataPrevisao.ToDateTime("yyyyMMdd") < DateTime.Today)
                noPrazo = PRAZO_VENCIDO;
            
            return verificaCodigoCorrespondente(idMensagem, noPrazo, tipoUsuario, baseModel);
        }

        /// <summary>
        /// Gera mensagem
        /// </summary>
        public string geraMensagem(string idMensagem)
        {
            if (idMensagem != null && idMensagem.Length > 1)
            {
                if (idMensagem.ToUpper()[0] != 'M')
                {
                    idMensagem = "M" + idMensagem.PadLeft(4, '0');
                }
            }
            string mensagem = pesquisaOracle(idMensagem);
            return mensagem;
        }

        /// <summary>
        /// Gera mensagem para codigos de comprimento igual a 1.
        /// </summary>
        public string GeraMensagemCustom(string idMensagem)
        {
            if (idMensagem != null && idMensagem.Length == 1)
            {
                if (idMensagem.ToUpper()[0] != 'M')
                {
                    idMensagem = "M" + idMensagem.PadLeft(4, '0');
                }
            }
            string mensagem = pesquisaOracle(idMensagem);
            return mensagem;
        }

        /// <summary>
        /// Parse de codigo de mensagem
        /// </summary>
        public string parseMensagem(string idMensagem)
        {
            return parseMensagem(idMensagem,USUARIO_TODOS, null);
        }

        /// <summary>
        /// Parse de codigo de mensagem
        /// </summary>
        public string parseMensagem(string idMensagem, BaseModel baseModel)
        {
            return parseMensagem(idMensagem, USUARIO_TODOS, baseModel);
        }

        /// <summary>
        /// Parse de codigo de mensagem
        /// </summary>
        public string parseMensagem(string idMensagem, bool usuarioInterno, BaseModel baseModel)
        {
            int tipoUsuario = USUARIO_EXTERNO;
            if (usuarioInterno)
                tipoUsuario = USUARIO_INTERNO;
            return parseMensagem(idMensagem, tipoUsuario, baseModel);
        }

        /// <summary>
        /// Parse de codigo de mensagem
        /// </summary>
        private string parseMensagem(string idMensagem, int tipoUsuario, BaseModel baseModel)
        {
            int codigoSicom = 0;
            if (idMensagem != null && idMensagem.Length > 1)
            {
                if (idMensagem.ToUpper()[0] == 'M')
                {
                    string mensagem = trataMensagem(pesquisaOracle(idMensagem),baseModel);
                    return mensagem;
                }
                else if (int.TryParse(idMensagem, out codigoSicom))
                {
                    if (codigoSicom > 0)
                        return verificaCodigoCorrespondente(codigoSicom,-1, tipoUsuario, baseModel);
                    else
                        return "";
                }
                else
                {
                    return idMensagem;
                }
            }
            return "";
        }

        /// <summary>
        /// Trata mensagem
        /// </summary>
        public string trataMensagem(string descricaoMensagem, BaseModel model)
        {
            List<BaseModel> models = new List<BaseModel>();
            if (model != null)
                models.Add(model);
            return trataMensagem(descricaoMensagem, models);
        }

        /// <summary>
        /// Trata mensagem
        /// </summary>
        public string trataMensagem(string descricaoMensagem, List<BaseModel> models)
        {
            descricaoMensagem = descricaoMensagem.Replace('|', '\n');
            string resultado = "";
            int pos = 0;
            foreach (BaseModel model in models)
            {
                do
                {
                    pos = descricaoMensagem.IndexOf("<D!");
                    if (pos > -1)
                    {
                        int posFim;
                        resultado = resultado + substituiValor(descricaoMensagem, pos, model,out posFim);
                        descricaoMensagem = descricaoMensagem.Substring(pos + posFim + 1);
                    }
                    else
                    {
                        pos = descricaoMensagem.IndexOf("<T!");
                        if (pos > -1)
                        {
                            int posFim;
                            resultado = resultado + substituiValor(descricaoMensagem, pos, model, out posFim);
                            descricaoMensagem = descricaoMensagem.Substring(pos + posFim + 1);
                        }
                        else
                        {
                            resultado = resultado + descricaoMensagem;
                        }
                    }
                }
                while (pos > -1);
            }
            if (pos == 0)
                resultado = descricaoMensagem;
            return resultado;
        }

        private string substituiValor(string descricaoMensagem,int pos,BaseModel model,out int posFim)
        {
            string resultado;
            char tipo = descricaoMensagem[pos + 1];
            posFim = descricaoMensagem.Substring(pos).IndexOf('>');
            //string conteudo = descricaoMensagem.Substring(pos + 3, posFim - 3);
            string conteudo = "";
            string nomeCampo = descricaoMensagem.Substring(pos + 3, posFim - 3);
            if (model.existe(nomeCampo))
            {
                if (tipo == 'D')
                    conteudo = model.getValor(nomeCampo).ToDateTime("yyyyMMdd").ToString("dd/MM/yyyy");
                else
                    conteudo = model.getValor(nomeCampo);

            }
            string descricaoInicio = "";
            if (pos > 0)
                descricaoInicio = descricaoMensagem.Substring(0, pos);
            resultado = descricaoInicio + conteudo;
            descricaoMensagem = descricaoMensagem.Substring(pos + posFim + 1);
            return resultado;
        }

        private string verificaCodigoCorrespondente(int codigoSicomtipoUsuario)
        {
            return verificaCodigoCorrespondente(codigoSicomtipoUsuario, -1, USUARIO_INTERNO,null);
        }

        private string verificaCodigoCorrespondente(int codigoSicomtipoUsuario, BaseModel baseModel)
        {
            return verificaCodigoCorrespondente(codigoSicomtipoUsuario, -1, USUARIO_INTERNO, baseModel);
        }

        private string verificaCodigoCorrespondente(int codigoSicom, int condicaoGenerica, int tipoUsuario,BaseModel baseModel)
        {
            //var codigoOracle = _correspondenciaSicomOracle.Where(y => y.codigoSicom == idMensagem && y.programaSicom == nomePrograma && y.condicaoGenerica == condicaoGenerica).Select(x => x.codigoOracle).FirstOrDefault().ToString();
            //var listWhere = _correspondenciaSicomOracle.Where(y => y.codigoSicom == idMensagem && y.programaSicom == nomePrograma && y.usuarioInterno == usuarioInterno && y.condicaoGenerica == condicaoGenerica).ToList();
            foreach (TrabMensagemCorrespondenciaSicomOracle correspondencia in _correspondenciaSicomOracle)
            {
                if (correspondencia.codigoSicom == codigoSicom
                     // && nomePrograma.Equals(correspondencia.programaSicom)
                     && ((tipoUsuario == correspondencia.tipoUsuario)
                     || (correspondencia.tipoUsuario == USUARIO_TODOS)
                     || (tipoUsuario == USUARIO_TODOS && correspondencia.tipoUsuario == USUARIO_INTERNO))
                     )
                {
                    if (correspondencia.condicaoGenerica == -1 || condicaoGenerica == correspondencia.condicaoGenerica)
                    {
                        var codigoOracle = correspondencia.codigoOracle;
                        string descricaoMensagem = pesquisaOracle(codigoOracle);
                        descricaoMensagem = trataMensagem(descricaoMensagem + correspondencia.textoComplementar + correspondencia.textoComplementar2, baseModel);
                        return descricaoMensagem;
                    }
                }
            }
            return codigoSicom.ToString();
        }

        private void trataCamposBaseModelSend(BaseModel objOut)
        {
            Type objtype = objOut.GetType();
            foreach (PropertyInfo p in objtype.GetProperties())
            {
                if ("String".Equals(p.PropertyType.Name))
                {
                    objtype.GetProperty(p.Name).SetValue(objOut, "");
                }
                else if ("Int32".Equals(p.PropertyType.Name) || "Int64".Equals(p.PropertyType.Name))
                {
                    objtype.GetProperty(p.Name).SetValue(objOut, 0);
                }
                else if ("List`1".Equals(p.PropertyType.Name))
                {
                    object objectModelo = Activator.CreateInstance(p.PropertyType);
                    objtype.GetProperty(p.Name).SetValue(objOut, objectModelo);
                }
            }
        }

        private string pesquisaOracle(string idMensagem)
        {
            string descricaoMensagem = "";
            if (idMensagem != null && idMensagem.Length > 1)
            {
                if (!"M0000".Equals(idMensagem))
                {
                    if (HttpContext.Current != null)
                        descricaoMensagem = (string)HttpContext.Current.Cache[idMensagem];
                    if (descricaoMensagem == null || "".Equals(descricaoMensagem))
                    {
                        descricaoMensagem = idMensagem.Substring(1);
                        IList<ORAMensagem> retornoMens = null;
                        for (int i = 0; i < 3; i++)
                        {
                            try
                            {
                                retornoMens = _oRAMensagemRepository.GetMany(x => x.idMensagem == idMensagem);
                                break;
                            }
                            catch (Exception e)
                            {
                                if (i == 2)
                                {
                                    string erro = e.Message;
                                    if (e.InnerException != null)
                                        erro = erro + " - " + e.InnerException.Message;
                                    throw new Exception("ErroOracle:" + erro);
                                }
                            }
                        }
                        if (retornoMens != null && retornoMens.Count > 0)
                        {
                            foreach (ORAMensagem mensagem in retornoMens)
                            {
                                descricaoMensagem = mensagem.descricaoMensagem;
                                DateTime expiration = DateTime.Now.AddHours(5);
                                HttpContext.Current.Cache.Add(idMensagem, descricaoMensagem, null, expiration, Cache.NoSlidingExpiration,CacheItemPriority.Normal,null);
                                break;
                            }
                        }
                    }
                }
            }
            return descricaoMensagem;
        }

        private void carregarCorrespondenciaSicomOracle()
        {
            //_correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0001, "SCN4CRSS", "M0174", USUARIO_INTERNO));
            //_correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0001, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0001, "SCN4CRSS", "M0195", USUARIO_TODOS));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0002, "SCN4CRSS", "M0002", USUARIO_TODOS, TEXTO_COMPL_PREVISAO_SOLUCAO, TEXTO_COMPL_NUMERO_PROTOCOLO,PROTOCOLO_COM));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0002, "SCN4CRSS", "M0002", USUARIO_TODOS, TEXTO_COMPL_PREVISAO_SOLUCAO, PROTOCOLO_SEM));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0002, "SCN4CRSS", "M0002", USUARIO_TODOS, TEXTO_COMPL_PREVISAO_SOLUCAO, TEXTO_COMPL_NUMERO_PROTOCOLO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0007, "SCN4CRSS", "M0007", USUARIO_TODOS, TEXTO_COMPL_PREVISAO_SOLUCAO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0009, "SCN4CRSS", "M0009", USUARIO_TODOS, TEXTO_COMPL_PREVISAO_SOLUCAO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1258, "SCN4CRSS", "M0038", USUARIO_INTERNO, TEXTO_COMPL_PREVISAO_SOLUCAO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1258, "SCN4CRSS", "M0001", USUARIO_EXTERNO, TEXTO_COMPL_PREVISAO_SOLUCAO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1207, "SCN4CRSS", "M0076", USUARIO_INTERNO, TEXTO_COMPL_PREVISAO_EXECUCAO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1207, "SCN4CRSS", "M0001", USUARIO_EXTERNO, TEXTO_COMPL_PREVISAO_SOLUCAO, PRAZO_NAO_VENCIDO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1207, "SCN4CRSS", "M0038", USUARIO_EXTERNO, TEXTO_COMPL_PREVISAO_SOLUCAO, PRAZO_VENCIDO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2483, "SCN4CRSS", "M0038", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2483, "SCN4CRSS", "M0001", USUARIO_EXTERNO, TEXTO_COMPL_PREVISAO_SOLUCAO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2565, "SCN4CRSS", "M0038", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2565, "SCN4CRSS", "M0001", USUARIO_EXTERNO, TEXTO_COMPL_PREVISAO_SOLUCAO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2566, "SCN4CRSS", "M0051", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2566, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2567, "SCN4CRSS", "M0053", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2567, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1212, "SCN4CRSS", "M0042", USUARIO_TODOS));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1259, "SCN4CRSS", "M0056", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1259, "SCN4CRSS", "M0078", USUARIO_EXTERNO, TEXTO_COMPL_PREVISAO_SOLUCAO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0929, "SCN4CRSS", "M0051", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0929, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1266, "SCN4CRSS", "M0041", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1266, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1206, "SCN4CRSS", "M0110", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1206, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1208, "SCN4CRSS", "M0050", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1208, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1209, "SCN4CRSS", "M0044", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1209, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1210, "SCN4CRSS", "M0113", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1210, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1256, "SCN4CRSS", "M0114", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1256, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1257, "SCN4CRSS", "M0049", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1257, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1260, "SCN4CRSS", "M0036", USUARIO_TODOS));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1267, "SCN4CRSS", "M0050", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1267, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1321, "SCN4CRSS", "M0118", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1321, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1369, "SCN4CRSS", "M0119", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1369, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1579, "SCN4CRSS", "M0120", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1579, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2477, "SCN4CRSS", "M0046", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2477, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2481, "SCN4CRSS", "M0055", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2481, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2482, "SCN4CRSS", "M0036", USUARIO_TODOS));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2490, "SCN4CRSS", "M0124", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2490, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2511, "SCN4CRSS", "M0125", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2511, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2561, "SCN4CRSS", "M0054", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2561, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2581, "SCN4CRSS", "M0127", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2581, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2587, "SCN4CRSS", "M0128", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2587, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0614, "SCN4CRSS", "M0129", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0614, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0100, "SCN4CRSS", "M0130", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0100, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0101, "SCN4CRSS", "M0131", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0101, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0102, "SCN4CRSS", "M0132", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0102, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0103, "SCN4CRSS", "M0133", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0103, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0104, "SCN4CRSS", "M0134", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0104, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0105, "SCN4CRSS", "M0135", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0105, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0106, "SCN4CRSS", "M0136", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0106, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0107, "SCN4CRSS", "M0137", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0107, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0108, "SCN4CRSS", "M0138", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0108, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0109, "SCN4CRSS", "M0139", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0109, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0110, "SCN4CRSS", "M0140", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0110, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0111, "SCN4CRSS", "M0141", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0111, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0112, "SCN4CRSS", "M0142", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0112, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0113, "SCN4CRSS", "M0143", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0113, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0114, "SCN4CRSS", "M0144", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0114, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0115, "SCN4CRSS", "M0143", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0115, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0116, "SCN4CRSS", "M0145", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0116, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0117, "SCN4CRSS", "M0146", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0117, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0118, "SCN4CRSS", "M0147", USUARIO_TODOS));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0119, "SCN4CRSS", "M0148", USUARIO_TODOS));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0120, "SCN4CRSS", "M0148", USUARIO_TODOS));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0121, "SCN4CRSS", "M0148", USUARIO_TODOS));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0122, "SCN4CRSS", "M0149", USUARIO_TODOS));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0123, "SCN4CRSS", "M0148", USUARIO_TODOS));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0124, "SCN4CRSS", "M0150", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0124, "SCN4CRSS", "M0036", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0125, "SCN4CRSS", "M0151", USUARIO_INTERNO,TEXTO_COMPL_CONFIRMACAO_ENDERECO,TEXTO_COMPL_FUNCAO41_SICOM));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0125, "SCN4CRSS", "M0151", USUARIO_EXTERNO, TEXTO_COMPL_PREVISAO_SOLUCAO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0533, "SCN4CRSS", "M0042", USUARIO_TODOS));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0565, "SCN4CRSS", "M0175", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0565, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0909, "SCN4CRSS", "M0156", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0909, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1211, "SCN4CRSS", "M0113", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1211, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1322, "SCN4CRSS", "M0176", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1322, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1815, "SCN4CRSS", "M0177", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1815, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2005, "SCN4CRSS", "M0178", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2005, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2072, "SCN4CRSS", "M0179", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2072, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2073, "SCN4CRSS", "M0180", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2073, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2486, "SCN4CRSS", "M0181", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2486, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2487, "SCN4CRSS", "M0182", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2487, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2488, "SCN4CRSS", "M0183", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2488, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2489, "SCN4CRSS", "M0184", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2489, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2525, "SCN4CRSS", "M0185", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2525, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2531, "SCN4CRSS", "M0078", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2531, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2535, "SCN4CRSS", "M0186", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2535, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2550, "SCN4CRSS", "M0187", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(2550, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(3145, "SCN4CRSS", "M0188", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(3145, "SCN4CRSS", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0022, "SCN4ISAE", "M0022", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0022, "SCN4ISAE", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0031, "SCN4ISAE", "M0129", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0031, "SCN4ISAE", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0038, "SCN4ISAE", "M0038", USUARIO_TODOS, TEXTO_COMPL_PREVISAO_SOLUCAO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0077, "SCN4ISAE", "M0163", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0077, "SCN4ISAE", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0028, "SCN4ISAE", "M0027", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0028, "SCN4ISAE", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0019, "SCN4ISAE", "M0019", USUARIO_TODOS));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0045, "SCN4ISAE", "M0189", USUARIO_INTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(0045, "SCN4ISAE", "M0078", USUARIO_EXTERNO));
            _correspondenciaSicomOracle.Add(criaCorrespondenciaSicomOracle(1718, "SCN6ISDF", "M0193", USUARIO_TODOS));
        }

        private TrabMensagemCorrespondenciaSicomOracle criaCorrespondenciaSicomOracle(int codigoSicom, string programaSicom, string codigoOracle, int tipoUsuario)
        {
            return criaCorrespondenciaSicomOracle(codigoSicom, programaSicom, codigoOracle, tipoUsuario,"","", -1);
        }

        private TrabMensagemCorrespondenciaSicomOracle criaCorrespondenciaSicomOracle(int codigoSicom, string programaSicom, string codigoOracle, int tipoUsuario,int condicaoGenerica)
        {
            return criaCorrespondenciaSicomOracle(codigoSicom, programaSicom, codigoOracle, tipoUsuario, "", "", condicaoGenerica);
        }

        private TrabMensagemCorrespondenciaSicomOracle criaCorrespondenciaSicomOracle(int codigoSicom, string programaSicom, string codigoOracle, int tipoUsuario, string textoComplementar, int condicaoGenerica)
        {
            return criaCorrespondenciaSicomOracle(codigoSicom, programaSicom, codigoOracle, tipoUsuario, textoComplementar,"", condicaoGenerica);
        }

        private TrabMensagemCorrespondenciaSicomOracle criaCorrespondenciaSicomOracle(int codigoSicom, string programaSicom, string codigoOracle, int tipoUsuario, string textoComplementar)
        {
            return criaCorrespondenciaSicomOracle(codigoSicom, programaSicom, codigoOracle, tipoUsuario, textoComplementar, "", -1);
        }

        private TrabMensagemCorrespondenciaSicomOracle criaCorrespondenciaSicomOracle(int codigoSicom, string programaSicom, string codigoOracle, int tipoUsuario, string textoComplementar, string textoComplementar2)
        {
            return criaCorrespondenciaSicomOracle(codigoSicom, programaSicom, codigoOracle, tipoUsuario, textoComplementar, textoComplementar2, -1);
        }

        private TrabMensagemCorrespondenciaSicomOracle criaCorrespondenciaSicomOracle(
            int codigoSicom, 
            string programaSicom, 
            string codigoOracle,
            int tipoUsuario, 
            string textoComplementar,
            string textoComplementar2,
            int condicaoGenerica)
        {
            TrabMensagemCorrespondenciaSicomOracle trabMensagemCorrespondenciaSicomOracle = new TrabMensagemCorrespondenciaSicomOracle();
            trabMensagemCorrespondenciaSicomOracle.codigoOracle = codigoOracle;
            trabMensagemCorrespondenciaSicomOracle.codigoSicom = codigoSicom;
            trabMensagemCorrespondenciaSicomOracle.programaSicom = programaSicom;
            trabMensagemCorrespondenciaSicomOracle.tipoUsuario = tipoUsuario;
            trabMensagemCorrespondenciaSicomOracle.condicaoGenerica = condicaoGenerica;
            trabMensagemCorrespondenciaSicomOracle.textoComplementar = textoComplementar;
            trabMensagemCorrespondenciaSicomOracle.textoComplementar2 = textoComplementar2;
            return trabMensagemCorrespondenciaSicomOracle;
        }
    }
}
