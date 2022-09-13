using FamilyHubs.ServiceDirectoryCaseManagement.Common.Dto;
using FamilyHubs.ServiceDirectoryCaseManagement.Api.Commands.CreateReferral;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using FamilyHubs.ServiceDirectoryCaseManagement.Api.Queries.GetReferrals;

namespace FamilyHubs.ServiceDirectoryCaseManagement.Api.Endpoints;

public class MinimalReferralEndPoints
{
    public void RegisterReferralEndPoints(WebApplication app)
    {
        app.MapPost("api/referrals", async ([FromBody] ReferralDto request, CancellationToken cancellationToken, ISender _mediator) =>
        {
            try
            {
                CreateReferralCommand command = new(request);
                var result = await _mediator.Send(command, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }).WithMetadata(new SwaggerOperationAttribute("Referrals", "Create Referral") { Tags = new[] { "Referrals" } });

        app.MapGet("api/referrals/{referrer}", async (string referrer, int? pageNumber, int? pageSize, CancellationToken cancellationToken, ISender _mediator) =>
        {
            try
            {
                GetReferralsByReferrerCommand request = new(referrer, pageNumber, pageSize);
                var result = await _mediator.Send(request, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }).WithMetadata(new SwaggerOperationAttribute("Get Referrals", "Get Referral By Referrer") { Tags = new[] { "Referrals" } });

    }
}
