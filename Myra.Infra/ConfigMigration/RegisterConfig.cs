using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Myra.Domain.Core;

namespace Myra.Infra.ConfigMigration
{
    public class RegisterConfig<T> : IEntityTypeConfiguration<T> where T : Register
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasIndex(r => r.Id);
            builder.Property(r => r.CreatedAt).IsRequired();
            builder.Property(r => r.UpdatedAt).IsRequired();
        }
    }
}
