using FamilyHubs.SharedKernel.Interfaces;

namespace FamilyHubs.ServiceDirectoryCaseManagement.Infra.Service;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
