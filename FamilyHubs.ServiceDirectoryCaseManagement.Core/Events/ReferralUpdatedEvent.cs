using FamilyHubs.ServiceDirectoryCaseManagement.Core.Entities;
using FamilyHubs.ServiceDirectoryCaseManagement.Core.Interfaces.Events;
using FamilyHubs.SharedKernel;

namespace FamilyHubs.ServiceDirectoryCaseManagement.Core.Events;


public class ReferralUpdatedEvent : DomainEventBase, IReferralCreatedEvent
{
    public ReferralUpdatedEvent(Referral item)
    {
        Item = item;
    }

    public Referral Item { get; }
}

