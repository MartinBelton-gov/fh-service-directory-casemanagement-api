using FamilyHubs.SharedKernel;

namespace FamilyHubs.ServiceDirectoryCaseManagement.Core.Entities;

public class ReferralStatus : EntityBase<string>
{
    public string Status { get; set; } = default!;

}
