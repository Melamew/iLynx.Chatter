using System.Collections.Generic;
using iLynx.Chatter.Infrastructure.Domain;
using iLynx.Common;

namespace iLynx.Chatter.Server
{
    public class UserInfo : NotificationBase
    {
        private bool isOnline;
        private string nickName;
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        public bool IsOnline
        {
            get { return isOnline; }
            set
            {
                if (value.Equals(isOnline)) return;
                isOnline = value;
                OnPropertyChanged();
            }
        }

        public string NickName
        {
            get { return nickName; }
            set
            {
                if (value == nickName) return;
                nickName = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<Permission> Permissions { get; set; } 
    }
}
