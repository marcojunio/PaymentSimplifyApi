using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using PaymentSimplify.Application.Common.Interfaces;
using PaymentSimplify.Common.Settings;
using PaymentSimplify.Domain.Common;

namespace PaymentSimplify.Infra.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly PaymentSimplifyContext _dbContext;
    private readonly IDateTime _dateTime;
    private readonly ICurrentUserService _currentUserService;
    private readonly AppSettings _appSettings;

    public UnitOfWork(
        PaymentSimplifyContext dbContext,
        IDateTime dateTime,
        AppSettings appSettings, ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _dateTime = dateTime;
        _appSettings = appSettings;
        _currentUserService = currentUserService;
    }

    public DbTransaction BeginTransaction()
    {
        var transaction = _dbContext.Database.BeginTransaction();
        return transaction.GetDbTransaction();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        UpdateEntities();

        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private void UpdateEntities()
    {
        var user = _currentUserService.GetIdUser();

        foreach (var entry in _dbContext.ChangeTracker.Entries<BaseAuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = string.IsNullOrEmpty(user) ? _appSettings.IdUserAdmin : user;
                entry.Entity.Created = _dateTime.UtcNow;
            }

            if (entry.State != EntityState.Added &&
                entry.State != EntityState.Modified &&
                !HasChangedOwnedEntities(entry)) continue;

            entry.Entity.LastModifiedBy = string.IsNullOrEmpty(user) ? _appSettings.IdUserAdmin : user;
            entry.Entity.LastModified = _dateTime.UtcNow;
        }
    }

    private static bool HasChangedOwnedEntities(EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            r.TargetEntry.State is EntityState.Added or EntityState.Modified);
}