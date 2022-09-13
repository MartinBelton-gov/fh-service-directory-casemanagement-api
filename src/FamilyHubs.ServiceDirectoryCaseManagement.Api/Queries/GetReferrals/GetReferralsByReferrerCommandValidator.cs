using FluentValidation;

namespace FamilyHubs.ServiceDirectoryCaseManagement.Api.Queries.GetReferrals;

public class GetReferralsByReferrerCommandValidator : AbstractValidator<GetReferralsByReferrerCommand>
{
    public GetReferralsByReferrerCommandValidator()
    {
        RuleFor(v => v.Referrer)
            .NotNull()
            .NotEmpty();
    }

}
