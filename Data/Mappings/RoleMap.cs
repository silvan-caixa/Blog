using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings
    {
    public class RoleMap : IEntityTypeConfiguration<Role>
        {
        public void Configure(EntityTypeBuilder<Role> builder)
            {
            // Table
            builder.ToTable("Role", "Api");
            // Pk
            builder.HasKey(r => r.Id)
                .HasName("RoleId");
            // Id Incremental
            builder.Property(r => r.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();
            // Propriedade
            builder.Property(r => r.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);
            builder.Property(r => r.Slug)
                .IsRequired()
                .HasColumnName("Slug")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);
            builder.Property(r => r.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt")
                .HasColumnType("DATETIME");
            // Index
            // Relacionamento

            }
        }
    }
