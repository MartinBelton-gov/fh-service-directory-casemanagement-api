using FamilyHubs.ServiceDirectoryCaseManagement.Common.Dto;

namespace FamilyHubs.ServiceDirectoryCaseManagement.Core.Interfaces.Commands;

public interface ICreateReferralCommand
{
    public ReferralDto ReferralDto { get; }
}
