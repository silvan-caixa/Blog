using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings
    {
    public class UserMap : IEntityTypeConfiguration<User>
        {
        public void Configure(EntityTypeBuilder<User> builder)
            {
            // Table
            builder.ToTable("User", "Api");
            // PK
            builder.HasKey(x => x.Id)
                .HasName("UserId");
            // Incremental
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();
            // Propriedade
            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);
            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("Varchar")
                .HasMaxLength(200);
            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .HasColumnName("PasswordHash")
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);
            builder.Property(x => x.Slug)
                .IsRequired()
                .HasColumnName("Slug")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);
            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt")
                //.HasDefaultValueSql("GETDATE()") -- gera no banco
                //.HasDefaultValue(DateTime.Now.ToUniversalTime()) -- gera no codigo
                .HasColumnType("DATETIME");

            // Index
            // Relacionamento
            builder.HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity<Dictionary<string, object>>(
                "UserRole",
                    role => role.HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .HasConstraintName("FK_UserRole_RoleId")
                    .OnDelete(DeleteBehavior.Cascade),
                    user => user.HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_UserRole_UserId")
                    .OnDelete(DeleteBehavior.Cascade),
                    userRole =>
                    {
                        userRole.ToTable("UserRole", "Api");
                        userRole.HasKey("UserId", "RoleId");
                    }
                   );
            }
        }
    }
