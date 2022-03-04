// ReSharper disable VirtualMemberCallInConstructor
namespace Workshop.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    using Workshop.Data.Common.Models;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.MessagesReceive = new HashSet<Message>();
            this.MessagesSend = new HashSet<Message>();
            this.NotificationsReceive = new HashSet<Notification>();
            this.NotificationsSend = new HashSet<Notification>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string AvatarUrl { get; set; }

        public string AvatarHash { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Message> MessagesSend { get; set; }

        public virtual ICollection<Message> MessagesReceive { get; set; }

        public virtual ICollection<Notification> NotificationsReceive { get; set; }

        public virtual ICollection<Notification> NotificationsSend { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
