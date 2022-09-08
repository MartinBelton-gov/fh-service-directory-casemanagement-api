using FamilyHubs.ServiceDirectoryCaseManagement.Core.Entities;

namespace FamilyHubs.ServiceDirectoryCaseManagement.Core.Interfaces.Events;

public interface IReferralCreatedEvent
{
    Referral Item { get; }
}
