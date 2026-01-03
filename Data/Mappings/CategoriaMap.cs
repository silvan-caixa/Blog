using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings;

public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
    public void Configure(EntityTypeBuilder<Categoria> builder)
        {
        // Table
        builder.ToTable("Categorias", "Api");
        //PK
        builder.HasKey(c => c.Id)
            .HasName("CategoriaId");
        // Id Incremental
        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        // Propriedade
        builder.Property(c => c.Nome)
            .IsRequired()
            .HasColumnName("Nome")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);
        builder.Property(c => c.Slug)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(60);
        builder.Property(c => c.DataCriacao)
            .IsRequired()
            .HasColumnName("DataCriacao")
            .HasColumnType("DATETIME")
            //.HasDefaultValueSql("GETDATE()")
            ;
        // Index
        // Relacionamento
        }
    }

