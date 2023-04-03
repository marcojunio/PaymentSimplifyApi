using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentSimplify.Domain.Entities;
using PaymentSimplify.Infra.Persistence.Configurations.Base;

namespace PaymentSimplify.Infra.Persistence.Configurations;

public class CostumerConfiguration : EntityTypeConfigurationBase<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("CUSTOMER");
        
        base.Configure(builder);

        builder.Property(f => f.FistName).HasMaxLength(250).IsRequired();
        builder.Property(f => f.LastName).HasMaxLength(250).IsRequired();

        builder.OwnsOne(f => f.Document, action =>
        {
            action.Property(f => f.TypeDocument).HasColumnName("TYPE_DOCUMENT").IsRequired();
            action.Property(f => f.Doc).HasColumnName("DOCUMENT").HasMaxLength(20).IsRequired();
        });

        builder.HasOne(f => f.AccountBank).WithOne().HasForeignKey("ACCOUNT_BANK_ID");

        builder.HasIndex("DOCUMENT").IsUnique();
    }
}