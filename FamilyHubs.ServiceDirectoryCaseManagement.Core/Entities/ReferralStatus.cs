﻿using FamilyHubs.SharedKernel;

namespace FamilyHubs.ServiceDirectoryCaseManagement.Core.Entities;

public class ReferralStatus : EntityBase<string>
{
    private ReferralStatus() { }
    public ReferralStatus(string id, string status)
    {
        Id = id;
        Status = status;
    }

    public string Status { get; set; } = default!;

}
