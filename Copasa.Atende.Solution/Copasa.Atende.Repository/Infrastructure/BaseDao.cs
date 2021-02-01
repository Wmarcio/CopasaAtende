using System;

namespace Copasa.Atende.Repository.Infrastructure
{
    /// <summary>
    /// BaseDao
    /// </summary>
    public class BaseDao : IBaseDao
    {
        /// <summary>
        /// OraDatabase
        /// </summary>
        public OraDatabase OraDatabase
        {
            get { return oraDatabase; }
        }

        /// <summary>
        /// BaseDao
        /// </summary>
        public BaseDao()
        {
            oraDatabase = new OraDatabase();
        }

        /// <summary>
        /// BaseDao
        /// </summary>
        public BaseDao(string conexaoString)
        {
            oraDatabase = new OraDatabase(conexaoString);
        }
        /// <summary>
        /// Construtor que recebe uma instancia do database, não pode ser nulo.
        /// </summary>
        /// <param name="oraDatabase"></param>
        public BaseDao(OraDatabase oraDatabase)
        {
            if (oraDatabase == null)
                throw new ArgumentNullException("oraDatabase", "OraDatabase não pode ser nulo.");

            this.oraDatabase = oraDatabase;
            oraDatabase.ManterConexaoAberta = true;
        }

        //public BaseDao(Page page)
        //{
        //    oraDatabase = new OraDatabase();
        //    oraDatabase.ManterConexaoAberta = true;

        //    if(page !=null)
        //        page.Unload += new EventHandler(page_Unload);
        //}

        /// <summary>
        /// FecharConexao
        /// </summary>
        public void FecharConexao()
        {
            oraDatabase.FecharConnexao();
        }

        private OraDatabase oraDatabase;

        private void page_Unload(object sender, System.EventArgs e)
        {
            oraDatabase.FecharConnexao();
        }

    }
}