using FamilyHubs.ServiceDirectoryCaseManagement.Api.Commands.CreateReferral;
using FamilyHubs.ServiceDirectoryCaseManagement.Common.Dto;
using FamilyHubs.ServiceDirectoryCaseManagement.Common.MassTransit;
using MassTransit;
using MediatR;
using System.Text.Json;

namespace FamilyHubs.ServiceDirectoryCaseManagement.Api;

public class CommandMessageConsumer : IConsumer<CommandMessage>
{
    public async Task Consume(ConsumeContext<CommandMessage> context)
    {
        var message = context.Message;
        await Console.Out.WriteLineAsync($"Message from Producer : {message.MessageString}");

        if (context != null && context.Message != null && !string.IsNullOrEmpty(context.Message.MessageString))
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            ReferralDto dto = JsonSerializer.Deserialize<ReferralDto>(context.Message.MessageString, options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (dto != null)
            {
                string id = string.Empty;
                try
                {
                    CreateReferralCommand command = new(dto);
                    using (var scope = Program.ServiceProvider.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetService<ISender>();
                        if (mediator != null)
                        {
                            var result = await mediator.Send(command, new CancellationToken());
                            id = result;
                        }
                    }

                        
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
                
            }
        }  
    }
}
