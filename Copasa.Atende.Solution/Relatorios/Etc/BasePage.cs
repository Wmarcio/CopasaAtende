using System.Web.UI;
using System.Collections.Generic;
using System;

namespace Etc
{
    /// <summary>
    /// Página base para as dmais páginas do projeto.
    /// Implementa a interface IErrorNotifier para realização de parte do tratamento e apresentação de erro.
    /// </summary>
    public class BasePage : Page, IErrorNotifier
    {
        public const string SELECIONE = "- Selecione -";
        /// <summary>
        /// @todo Documentation
        /// </summary>
        public BasePage()
        {
            //oraDatabase = new OraDatabase();
            //oraDatabase.ManterConexaoAberta = true;
            ClientParameters = new Dictionary<string, ClientParameter>();
            Target = string.Empty;
        }
        /// <summary>
        /// @todo Documentation
        /// </summary>
        public string ClientSaveTarget { get { return GetClientSaveTarget(); } }
        /// <summary>
        /// @todo Documentation
        /// </summary>
        public string ClientDeleteTarget { get { return GetClientDeleteTarget(); } }

        #region Protected stuff
        /// <summary>
        /// @todo Documentation
        /// </summary>
        protected string NomeCampoDescricao { get { return "descricao"; } }
        /// <summary>
        /// @todo Documentation
        /// </summary>
        protected Dictionary<string, ClientParameter> ClientParameters { get; set; }
        /// <summary>
        /// É o objeto alvo recebido pelo cliente no evento __EVENTTARGET, é comparado com o valor de ClientSaveTarget ou ClientDeleteTarget.
        /// </summary>
        protected string Target { get; set; }
        /// <summary>
        /// @todo Documentation
        /// </summary>
        public string ErrorMessage { get; set; } // private
        /// <summary>
        /// @todo Documentation
        /// </summary>
        /// <returns></returns>
        public string GetErrorMessage()
        {
            return ErrorMessage;
        }

        //public OraDatabase OraDatabase { get { return oraDatabase; } }
        
        
        /// <summary>
        /// @todo Documentation
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(System.EventArgs e)
        {
            if (IsPostBack)
                LoadClientParameters();

            base.OnLoad(e);

            try
            {
                if (IsPostBack)
                {
                    if (Target == ClientSaveTarget)
                        Salvar();
                    else if (Target == ClientDeleteTarget)
                        Deletar();
                }

            }
            catch (Exception ex) // ApplicationException
            {
                //Prototipo.Site master = null;
                //if (Master != null)
                //    master = Master as Prototipo.SiteMaster;

                ErrorMessage = ex.Message.Replace("\"", "").Replace("'", "").Replace("\r\n", "").Replace("\n", ""); //.Substring(0, 15)
            }

        }

        /// <summary>
        /// @todo Documentation
        /// </summary>
        protected virtual void Salvar() { }
        /// <summary>
        /// @todo Documentation
        /// </summary>
        protected virtual void Deletar() { }
        /// <summary>
        /// @todo Documentation
        /// </summary>
        protected void LoadClientParameters()
        {
            string parameters = null;

            if (Page.Request.Params["__EVENTTARGET"] != null)
                Target = Page.Request.Params["__EVENTTARGET"];

            if (Page.Request.Params["__EVENTARGUMENT"] != null)
                parameters = Page.Request.Params["__EVENTARGUMENT"];

            parseClientParameters(parameters);

        }
        /// <summary>
        /// Partial formating extracting data
        /// expected (only objects) in format
        /// name = value, name = value
        /// 
        /// @todo throw exception when  is in wrong format.
        /// </summary>
        /// <param name="clientParameters"></param>
        /// <returns></returns>    
        protected bool parseClientParameters(string clientParameters)
        {
            //TODO use Regex to validate format or exctract

            string[] parameters = clientParameters.Split(new string[] { "=", ";" }, System.StringSplitOptions.None); // TODO Improve

            ClientParameters.Clear();
            for (int i = 0; i < parameters.Length - 1; i += 2)
                ClientParameters.Add(parameters[i].Trim(), new ClientParameter(parameters[i].Trim(), parameters[i + 1].Trim()));


            return ClientParameters.Count > 0;
        }
        /// <summary>
        /// @todo Documentation
        /// </summary>
        /// <returns></returns>
        virtual protected string GetClientSaveTarget()
        {
            return "btnSalvar";
        }
        /// <summary>
        /// @todo Documentation
        /// </summary>
        /// <returns></returns>
        virtual protected string GetClientDeleteTarget()
        {
            return "btnDeletar";
        }
        /// <summary>
        /// No PreInit, pode-se definir o tema do site dinamicamente.
        /// Uma vez definido o tema na seção por exemplo, atribua a propriedade da página Page.Theme.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            //if (Session[ThemeKey] != null)
            //{
            //    Page.Theme = (string)Session[ThemeKey];
            //}

            //base.OnPreInit(e);
        }

        //private OraDatabase oraDatabase = null;


        #endregion

    }
    /// <summary>
    /// @todo Documentation
    /// </summary>
    public class ClientParameter
    {
        /// <summary>
        /// @todo Documentation
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// @todo Documentation
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// @todo Documentation
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public ClientParameter(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            string n;
            if (string.IsNullOrEmpty(Name))
                n = base.ToString();
            else
                n = Name;

            return string.Format("{0} ({1})", n, Value);
        }
    }
}