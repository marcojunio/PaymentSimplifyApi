using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentSimplify.Domain.Entities;
using PaymentSimplify.Infra.Persistence.Configurations.Base;

namespace PaymentSimplify.Infra.Persistence.Configurations;

public class AccountBankConfiguration : EntityTypeConfigurationBase<AccountBank>
{
    public override void Configure(EntityTypeBuilder<AccountBank> builder)
    {
        builder.ToTable("ACCOUNT_BANK");

        base.Configure(builder);

        builder.OwnsOne(f => f.Balance, action =>
        {
            action.Property(f => f.Amount).HasColumnName("AMOUNT").IsRequired();
            action.Property(f => f.Currency).HasColumnName("CURRENCY").IsRequired();
        });
    }
}