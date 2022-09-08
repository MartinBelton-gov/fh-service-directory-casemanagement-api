using FamilyHubs.ServiceDirectoryCaseManagement.Core.Entities;
using FamilyHubs.ServiceDirectoryCaseManagement.Core.Infrastructure;
using FamilyHubs.ServiceDirectoryCaseManagement.Infra.Persistence.Interceptors;
using FamilyHubs.SharedKernel;
using FamilyHubs.SharedKernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FamilyHubs.ServiceDirectoryCaseManagement.Infra.Persistence.Repository;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IDomainEventDispatcher _dispatcher;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext
        (
            DbContextOptions<ApplicationDbContext> options,
            IDomainEventDispatcher dispatcher,
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor
        )
        : base(options)
    {
        _dispatcher = dispatcher;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        // ignore events if no dispatcher provided
        var entitiesWithEvents = ChangeTracker
            .Entries()
            .Select(e => e.Entity as EntityBase<string>)
            .Where(e => e?.DomainEvents != null && e.DomainEvents.Any())
            .ToArray();

        
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        if (entitiesWithEvents != null && entitiesWithEvents.Any())
            await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

        return result;
    }

    public DbSet<Referral> Referrals => Set<Referral>();
    public DbSet<ReferralStatus> ReferralStatuses => Set<ReferralStatus>();
}
