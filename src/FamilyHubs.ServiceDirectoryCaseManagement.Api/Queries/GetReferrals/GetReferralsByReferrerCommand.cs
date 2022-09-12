using Ardalis.GuardClauses;
using FamilyHubs.ServiceDirectoryCaseManagement.Common.Dto;
using FamilyHubs.ServiceDirectoryCaseManagement.Core.Entities;
using FamilyHubs.ServiceDirectoryCaseManagement.Infra.Persistence.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.ServiceDirectoryCaseManagement.Api.Queries.GetReferrals;

public class GetReferralsByReferrerCommand : IRequest<List<ReferralDto>>
{
    public GetReferralsByReferrerCommand(string referrer)
    {
        Referrer = referrer;
    }

    public string Referrer { get; set; }
}

public class GetReferralsByReferrerCommandHandler : IRequestHandler<GetReferralsByReferrerCommand, List<ReferralDto>>
{
    private readonly ApplicationDbContext _context;

    public GetReferralsByReferrerCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<ReferralDto>> Handle(GetReferralsByReferrerCommand request, CancellationToken cancellationToken)
    {
        var entities = _context.Referrals
            .Include(x => x.Status)
            .Where(x => x.Referrer == request.Referrer);

        if (entities == null)
        {
            throw new NotFoundException(nameof(Referral), request.Referrer);
        }

        var results = await entities.Select(x => new ReferralDto(
            x.Id,
            x.ServiceId,
            x.ServiceName,
            x.ServiceDescription,
            x.ServiceAsJson,
            x.FullName,
            x.HasSpecialNeeds,
            x.Email,
            x.Phone,
            x.ReasonForSupport,
            x.Status.Select(x => new ReferralStatusDto(x.Id, x.Status)).ToList()
            )).ToListAsync();

        return results;
    }
}
