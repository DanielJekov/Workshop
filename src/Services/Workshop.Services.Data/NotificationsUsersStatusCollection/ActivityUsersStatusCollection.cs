namespace Workshop.Services.Data.ActivityUsersStatusCollection
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class ActivityUsersStatusCollection : IActivityUsersStatusCollection
    {
        private List<ActivityUsersStatusModel> users = new List<ActivityUsersStatusModel>();

        public ReadOnlyCollection<ActivityUsersStatusModel> UsersCollection
            => new ReadOnlyCollection<ActivityUsersStatusModel>(this.users);

        public void Add(string userId)
        {
            this.users.Add(new ActivityUsersStatusModel { UserId = userId });
            this.Active(userId);
        }

        public void Remove(string userId)
        {
            var userToRemove = this.users.FirstOrDefault(x => x.UserId == userId);
            this.users.Remove(userToRemove);
        }

        public void Active(string userId)
        {
            var user = this.users.FirstOrDefault(x => x.UserId == userId);
            user.IsActive = true;
            user.LastActiveOn = DateTime.UtcNow;
        }

        public void NonActive(string userId)
        {
            var user = this.users.FirstOrDefault(x => x.UserId == userId);
            user.IsActive = false;
            user.LastActiveOn = DateTime.UtcNow;
        }

        public bool IsActive(string userId)
        => this.users.FirstOrDefault(x => x.UserId == userId).IsActive;

        public bool IsInCollection(string userId)
        => this.users.FirstOrDefault(x => x.UserId == userId) != null ? true : false;
    }
}
