using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentSimplify.Domain.Entities;
using PaymentSimplify.Infra.Persistence.Configurations.Base;

namespace PaymentSimplify.Infra.Persistence.Configurations;

public class TransactionConfiguration : EntityTypeConfigurationBase<Transaction>
{
    public override void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("TRANSACTIONS");
        
        base.Configure(builder);

        builder.HasOne(f => f.Payee).WithMany().HasForeignKey("ID_COSTUMER_PAYEE");
        builder.HasOne(f => f.AccountBank).WithMany(f => f.Transactions).HasForeignKey("ID_ACCOUNT_BANK");
    }
}