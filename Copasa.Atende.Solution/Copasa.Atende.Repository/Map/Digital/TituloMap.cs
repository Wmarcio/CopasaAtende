using Copasa.Atende.Model.Digital;
using System.Data.Entity.ModelConfiguration;

namespace Copasa.Atende.Repository.Map.Digital
{
    class TituloMap : EntityTypeConfiguration<TituloModel>
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        public TituloMap()
        {
            ToTable("APP_TITULO");

            HasKey(x => x.IdTitulo)
                .Property(x => x.IdTitulo)
                .HasColumnName("ID_TITULO")
                .IsRequired();

            Property(x => x.Descricao)
             .HasColumnName("DS_TITULO")
             .IsRequired();

            Property(x => x.Informacao)
             .HasColumnName("DS_INFORMACAO")
             .IsRequired();

            Ignore(x => x.IsAtivo);
        }
    }
}
