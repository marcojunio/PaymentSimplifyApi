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

        builder.OwnsOne(f => f.Amount, action =>
        {
            action.Property(f => f.Amount).HasColumnName("AMOUNT").IsRequired();
            action.Property(f => f.Currency).HasColumnName("CURRENCY").IsRequired();
        });
        
        builder.HasOne(f => f.AccountBankPayee)
            .WithMany(f => f.TransactionsPayee)
            .HasForeignKey("ID_ACCOUNT_BANK_PAYEE");
        
        builder.HasOne(f => f.AccountBankPayer)
            .WithMany(f => f.TransactionsPayer)
            .HasForeignKey("ID_ACCOUNT_BANK_PAYER");
    }
}