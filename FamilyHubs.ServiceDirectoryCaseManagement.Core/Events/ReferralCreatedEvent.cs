using FamilyHubs.ServiceDirectoryCaseManagement.Core.Entities;
using FamilyHubs.ServiceDirectoryCaseManagement.Core.Interfaces.Events;
using FamilyHubs.SharedKernel;

namespace FamilyHubs.ServiceDirectoryCaseManagement.Core.Events;

internal class ReferralCreatedEvent : DomainEventBase, IReferralCreatedEvent
{
    public ReferralCreatedEvent(Referral item)
    {
        Item = item;
    }

    public Referral Item { get; }
}
