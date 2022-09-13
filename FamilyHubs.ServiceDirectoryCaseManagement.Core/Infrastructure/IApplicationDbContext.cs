using FamilyHubs.ServiceDirectoryCaseManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.ServiceDirectoryCaseManagement.Core.Infrastructure;

public interface IApplicationDbContext
{
    DbSet<Referral> Referrals { get; }
    DbSet<ReferralStatus> ReferralStatuses { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
