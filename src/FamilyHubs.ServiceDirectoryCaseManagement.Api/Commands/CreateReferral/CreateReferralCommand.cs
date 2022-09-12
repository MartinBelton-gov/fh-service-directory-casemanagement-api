using FamilyHubs.ServiceDirectoryCaseManagement.Common.Dto;
using MediatR;
using FamilyHubs.ServiceDirectoryCaseManagement.Core.Interfaces.Commands;
using FamilyHubs.ServiceDirectoryCaseManagement.Infra.Persistence.Repository;
using AutoMapper;
using FamilyHubs.ServiceDirectoryCaseManagement.Core.Entities;
using FamilyHubs.ServiceDirectoryCaseManagement.Core.Events;

namespace FamilyHubs.ServiceDirectoryCaseManagement.Api.Commands.CreateReferral;

public class CreateReferralCommand : IRequest<string>, ICreateReferralCommand
{
    public CreateReferralCommand(ReferralDto referralDto)
    {
        ReferralDto = referralDto;
    }

    public ReferralDto ReferralDto { get; }
}

public class CreateReferralCommandHandler : IRequestHandler<CreateReferralCommand, string>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateReferralCommandHandler> _logger;
    public CreateReferralCommandHandler(ApplicationDbContext context, IMapper mapper, ILogger<CreateReferralCommandHandler> logger)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;   
    }
    public async Task<string> Handle(CreateReferralCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _mapper.Map<Referral>(request.ReferralDto);
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            entity.RegisterDomainEvent(new ReferralCreatedEvent(entity));
            _context.Referrals.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred creating referral. {exceptionMessage}", ex.Message);
            throw new Exception(ex.Message, ex);
        }

        if (request is not null && request.ReferralDto is not null)
            return request.ReferralDto.Id;
        else
            return string.Empty;
    }
}
    
