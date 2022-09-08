using FamilyHubs.ServiceDirectoryCaseManagement.Core.Entities;

namespace FamilyHubs.ServiceDirectoryCaseManagement.Core.Interfaces.Events;

public interface IReferralUpdateEvent
{
    public Referral Item { get; }
}
