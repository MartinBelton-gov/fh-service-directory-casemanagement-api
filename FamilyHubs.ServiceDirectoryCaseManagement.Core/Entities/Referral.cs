﻿using FamilyHubs.ServiceDirectoryCaseManagement.Core.Interfaces;
using FamilyHubs.SharedKernel;
using FamilyHubs.SharedKernel.Interfaces;

namespace FamilyHubs.ServiceDirectoryCaseManagement.Core.Entities;

public class Referral : EntityBase<string>, IAggregateRoot
{
    public string ServiceId { get; set; } = default!;
    public string ServiceName { get; set; } = default!;
    public string ServiceDescription { get; set; } = default!;
    public string ServiceAsJson { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string HasSpecialNeeds { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string ReasonForSupport { get; set; } = default!;
    public virtual ICollection<ReferralStatus> Status { get; set; } = default!;

}