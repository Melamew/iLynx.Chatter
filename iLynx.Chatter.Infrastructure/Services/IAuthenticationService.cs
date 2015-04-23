using System;
using iLynx.Chatter.Infrastructure.Domain;
using iLynx.Networking.ClientServer;
using iLynx.Networking.Interfaces;

namespace iLynx.Chatter.Infrastructure.Services
{
    public interface IAuthenticationService
    {
        bool IsClientAuthenticated(Guid clientId);
        User GetAuthenticatedUser(Guid clientId);
        Guid GetAuthenticatedId(User user);
    }

    public interface IAuthenticationService<out TMessage, TKey> : IAuthenticationService where TMessage : IKeyedMessage<TKey>
    {
        bool IsClientAuthenticated(IClient<TMessage, TKey> client);
    }
}
