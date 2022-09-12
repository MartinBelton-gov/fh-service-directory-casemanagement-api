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

        app.MapGet("api/referrals/{referrer}", async (string referrer, CancellationToken cancellationToken, ISender _mediator) =>
        {
            try
            {
                GetReferralsByReferrerCommand request = new(referrer);
                var result = await _mediator.Send(request, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }).WithMetadata(new SwaggerOperationAttribute("Get Referrals", "Get Referral By Referrer") { Tags = new[] { "Referrals" } });

        //app.MapGet("api/organizations", async (CancellationToken cancellationToken, ISender _mediator) =>
        //{
        //    try
        //    {
        //        ListOpenReferralOrganisationCommand request = new();
        //        var result = await _mediator.Send(request, cancellationToken);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //        throw;
        //    }
        //}).WithMetadata(new SwaggerOperationAttribute("List Organisations", "List Organisations") { Tags = new[] { "Organisations" } });

        //app.MapPut("api/organizations/{id}", async (string id, [FromBody] OpenReferralOrganisationWithServicesRecord request, CancellationToken cancellationToken, ISender _mediator, IMapper mapper) =>
        //{
        //    try
        //    {

        //        OpenReferralOrganisation openReferralOrganisation = mapper.Map<OpenReferralOrganisation>(request);
        //        var newService = openReferralOrganisation?.Services?.FirstOrDefault();
        //        if (openReferralOrganisation != null && newService != null)
        //        {

        //            var requestService = request?.Services?.FirstOrDefault();
        //            if (requestService != null)
        //            {


        //                List<OpenReferralEligibility> openReferralEligibility = new();

        //                if (requestService != null && requestService.Eligibilities != null)
        //                {
        //                    foreach (var item in requestService.Eligibilities)
        //                    {
        //                        var eleg = mapper.Map<OpenReferralEligibility>(item);
        //                        eleg.OpenReferralServiceId = newService.Id;
        //                        openReferralEligibility.Add(eleg);
        //                    }
        //                    OpenReferralService service = new(requestService.Id,
        //                    openReferralOrganisation.Id,
        //                    requestService.Name,
        //                    requestService.Description,
        //                    requestService.Accreditations,
        //                    requestService.Assured_date,
        //                    requestService.Attending_access,
        //                    requestService.Attending_type,
        //                    requestService.Deliverable_type,
        //                    requestService.Status,
        //                    requestService.Url,
        //                    requestService.Email,
        //                    requestService.Fees,
        //                    newService.ServiceDelivery,
        //                    openReferralEligibility,
        //                    newService.Fundings
        //                    , newService.Holiday_schedules
        //                    , newService.Languages
        //                    , newService.Regular_schedules
        //                    , newService.Reviews
        //                    , newService.Contacts
        //                    , newService.Cost_options
        //                    , newService.Service_areas
        //                    , newService.Service_at_locations
        //                    , newService.Service_taxonomys);
        //                }

        //                openReferralOrganisation = new(openReferralOrganisation.Id
        //                , openReferralOrganisation.Name
        //                , openReferralOrganisation.Description
        //                , openReferralOrganisation.Logo
        //                , openReferralOrganisation.Uri
        //                , openReferralOrganisation.Url
        //                , openReferralOrganisation.Reviews
        //                , openReferralOrganisation.Services);

        //            }
        //        }

        //        ArgumentNullException.ThrowIfNull(openReferralOrganisation, nameof(openReferralOrganisation));

        //        UpdateOpenReferralOrganisationCommand command = new(id, openReferralOrganisation);
        //        var result = await _mediator.Send(command, cancellationToken);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //        throw;
        //    }
        //}).WithMetadata(new SwaggerOperationAttribute("Update Organisation", "Update Organisation") { Tags = new[] { "Organisations" } });
    }
}
