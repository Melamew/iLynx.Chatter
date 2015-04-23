using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using iLynx.Chatter.Infrastructure;
using iLynx.Chatter.Infrastructure.Authentication;
using iLynx.Chatter.Infrastructure.Domain;
using iLynx.Chatter.Infrastructure.Events;
using iLynx.Chatter.Infrastructure.Services;
using iLynx.Common;
using iLynx.Common.DataAccess;
using iLynx.PubSub;

namespace iLynx.Chatter.Server.UI
{
    public class UserListViewModel : NotificationBase
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IUserPermissionService permissionService;
        private readonly INickManagerService nickManager;
        private readonly IDataAdapter<User> userAdapter;
        private readonly IBus<IApplicationEvent> applicationEventBus;
        private readonly ObservableCollection<UserInfo> users = new ObservableCollection<UserInfo>();

        public UserListViewModel(IUserPermissionService permissionService,
            INickManagerService nickManager,
            IDataAdapter<User> userAdapter,
            IBus<IApplicationEvent> applicationEventBus,
            IAuthenticationService authenticationService)
        {
            this.authenticationService = Guard.IsNull(() => authenticationService);
            this.permissionService = Guard.IsNull(() => permissionService);
            this.nickManager = Guard.IsNull(() => nickManager);
            this.userAdapter = Guard.IsNull(() => userAdapter);
            this.applicationEventBus = Guard.IsNull(() => applicationEventBus);
            LoadUsers();
            applicationEventBus.Subscribe<ClientConnectedEvent>(OnClientConnected);
            applicationEventBus.Subscribe<ClientDisconnectedEvent>(OnClientDisconnected);
            applicationEventBus.Subscribe<ClientAuthenticatedEvent>(OnClientAuthenticated);
        }

        private void OnClientAuthenticated(ClientAuthenticatedEvent message)
        {
            
        }

        private void OnClientDisconnected(ClientDisconnectedEvent message)
        {
            
        }

        private void OnClientConnected(ClientConnectedEvent message)
        {
            
        }

        private void LoadUsers()
        {
            users.AddRange(userAdapter.Query().Select(GetUserInfo));
        }

        private UserInfo GetUserInfo(User user)
        {
            var id = authenticationService.GetAuthenticatedId(user);
            var result = new UserInfo
            {
                IsOnline = id != Guid.Empty,
                PasswordHash = user.PasswordHash.ToString(":"),
                Permissions = permissionService.GetPermissions(user),
                Username = user.Username,
            };
            result.NickName = result.IsOnline ? nickManager.GetNickName(id) : string.Empty;
            return result;
        }

        public ObservableCollection<UserInfo> Users { get { return users; } }
    }
}
