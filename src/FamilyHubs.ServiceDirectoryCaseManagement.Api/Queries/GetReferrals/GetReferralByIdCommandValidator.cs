using FluentValidation;

namespace FamilyHubs.ServiceDirectoryCaseManagement.Api.Queries.GetReferrals;

public class GetReferralByIdCommandValidator : AbstractValidator<GetReferralByIdCommand>
{
    public GetReferralByIdCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotNull()
            .NotEmpty();
    }

}
