using Ambev.DeveloperEvaluation.Domain.Events;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser
{
    

    public class UserCreatedEventHandler : IHandleMessages<UserCreatedEvent>
    {
        public async Task Handle(UserCreatedEvent message)
        {
            Console.WriteLine($"{nameof(UserCreatedEvent)} received. Username: {message.UserName}");
        }
    }
}
