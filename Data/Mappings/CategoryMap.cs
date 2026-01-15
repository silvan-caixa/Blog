using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings;

public class CategoryMap : IEntityTypeConfiguration<Category>
    {
    public void Configure(EntityTypeBuilder<Category> builder)
        {
        // Table
        builder.ToTable("Category", "Api");
        //PK
        builder.HasKey(c => c.Id)
            .HasName("CategoriaId");
        // Id Incremental
        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        // Propriedade
        builder.Property(c => c.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);
        builder.Property(c => c.Slug)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(60);
        builder.Property(c => c.CreatedAt)
            .IsRequired()
            .HasColumnName("CreatedAt")
            .HasColumnType("DATETIME")
            //.HasDefaultValueSql("GETDATE()")
            ;
        // Index
        // Relacionamento
        }
    }

