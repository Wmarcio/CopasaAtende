


namespace Copasa.Sigos.Repository.Infrastructure
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;


    // TODO Trocar o nome desta classe para o nome do seu projeto ou do schema.

    /// <summary>
    /// Classe AppDataContext.
    /// </summary>
    public class AppDataContext : DbContext
    {
        static AppDataContext()
        {
            Database.SetInitializer<AppDataContext>(null);
        }

        /// <summary>
        /// Construtor da classe AppDataContext
        /// </summary>
        public AppDataContext()
            : base("Name=AppDataContext")
        {
        }

        /// <summary>
        /// Metodo OnModelCreating.
        /// </summary>
        /// <param name="modelBuilder">DbModelBuilder.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }




    }
}
