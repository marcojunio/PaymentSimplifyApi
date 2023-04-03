using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentSimplify.Domain.Common;

namespace PaymentSimplify.Infra.Persistence.Configurations.Base;

public class EntityTypeConfigurationBase<T> : IEntityTypeConfiguration<T> where T : BaseAuditableEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id).HasColumnName("ID").ValueGeneratedOnAdd().IsRequired();
        builder.Property(f => f.Created).ValueGeneratedOnAdd().HasColumnName("CREATED");
        builder.Property(f => f.LastModified).ValueGeneratedOnUpdate().HasColumnName("LAST_MODIFIED");
        builder.Property(f => f.CreatedBy).HasColumnName("CREATED_BY");
        builder.Property(f => f.LastModifiedBy).HasColumnName("LAST_MODIFIED_BE");
    }
}