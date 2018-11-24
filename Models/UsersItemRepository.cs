using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace astro_bot_back.Models
{
    public interface IUsersRepository
    {
        void Add (UsersItem item);
        IEnumerable<UsersItem> GetAll ();
        UsersItem Find (string key);
        UsersItem Remove (string key);
        void Update (UsersItem item);
    }

    public class UsersRepository : IUsersRepository
    {
        private static ConcurrentDictionary<string, UsersItem> _users =
            new ConcurrentDictionary<string, UsersItem> ();

        public UsersRepository ()
        { }

        public IEnumerable<UsersItem> GetAll ()
        {
            return _users.Values;
        }

        public void Add (UsersItem item)
        {
            item.User = Guid.NewGuid ().ToString ();
            _users[item.User] = item;
        }

        public UsersItem Find (string key)
        {
            UsersItem item;
            _users.TryGetValue (key, out item);
            return item;
        }

        public UsersItem Remove (string key)
        {
            UsersItem item;
            _users.TryRemove (key, out item);
            return item;
        }

        public void Update (UsersItem item)
        {
            _users[item.User] = item;
        }
    }
}