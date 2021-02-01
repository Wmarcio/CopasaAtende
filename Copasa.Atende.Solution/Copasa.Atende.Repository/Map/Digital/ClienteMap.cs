using Copasa.Atende.Model.Digital;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Copasa.Atende.Repository.Map.Digital
{
    /// <summary>
    /// Classe de mapeamento para a entidade APP_CLIENTE.
    /// </summary>
    public class ClienteMap : EntityTypeConfiguration<ClienteModel>
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        public ClienteMap()
        {
            ToTable("APP_CLIENTE");

            HasKey(x => x.CpfCnpj)
                .Property(x => x.CpfCnpj)
                .HasColumnName("NU_CLIENTE_CPF_CNPJ")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            Property(x => x.Nome)
                .HasColumnName("NM_CLIENTE")
                .HasMaxLength(40)
                .IsRequired();


            Property(x => x.Senha)
                .HasColumnName("SENHA")
                .HasMaxLength(50)
                .IsRequired();

            Property(x => x.Email)
                .HasColumnName("E-MAIL")
                .HasMaxLength(50)
                .IsRequired();


            Property(x => x.FlagTermoAceite)
                .HasColumnName("FL_TERMO_ACEITE")
                .IsRequired();

            Property(x => x.FlagPoliticaPrivacidade)
                .HasColumnName("FL_POLITICA_PRIVACIDADE")
                .IsRequired();

            Property(x => x.ImagemPerfil)
                .HasColumnName("IMG_PERFIL")
                .IsOptional();

            Property(x => x.RepresentanteComercial)
                .HasColumnName("NM_REPRESENTANTE_COMERCIAL")
                .IsOptional();

            Property(x => x.TipoCliente)
                .HasColumnName("NM_TIPO_CLIENTE")
                .IsOptional();

            Property(x => x.DataAlteracao)
                .HasColumnName("DT_ALTERACAO")
                .IsOptional();

            Property(x => x.StatusEmail)
                .HasColumnName("ST_EMAIL")
                .IsOptional();

            Property(x => x.Id)
                .HasColumnName("ID_USUARIO")
                .IsOptional();
                


            Property(x => x.Validado)
         .HasColumnName("VALIDADO")
         .IsOptional();

            //Ignore(x => x.Id);
            Ignore(x => x.Descricao);
            Ignore(x => x.IsAtivo);
            Ignore(x => x.ImagemPerfilBase64);

        }
    }
}
