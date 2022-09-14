using FluentValidation;

namespace FamilyHubs.ServiceDirectoryCaseManagement.Api.Queries.GetReferrals;

public class GetReferralsByOrganisationIdCommandValidator : AbstractValidator<GetReferralsByOrganisationIdCommand>
{
    public GetReferralsByOrganisationIdCommandValidator()
    {
        RuleFor(v => v.OrganisationId)
            .NotNull()
            .NotEmpty();
    }
}
