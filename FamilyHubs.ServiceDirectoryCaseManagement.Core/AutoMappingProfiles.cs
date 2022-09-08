using AutoMapper;
using FamilyHubs.ServiceDirectoryCaseManagement.Common.Dto;
using FamilyHubs.ServiceDirectoryCaseManagement.Core.Entities;

namespace FamilyHubs.ServiceDirectoryCaseManagement.Core;

public class AutoMappingProfiles : Profile
{
    public AutoMappingProfiles()
    {
        CreateMap<ReferralDto, Referral>();
        CreateMap<ReferralStatusDto, ReferralStatus>();
    }
}
