namespace Workshop.Web.ViewModels.Users
{
    using System;

    using Workshop.Data.Models;
    using Workshop.Services.Mapping;

    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        private string avatarUrl;

        public string Id { get; set; }

        public string UserName { get; set; }

        public string AvatarUrl
        {
            get
            {
                return this.avatarUrl;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    this.avatarUrl = @"/anonymous-avatar.jpg";
                }
                else
                {
                    this.avatarUrl = value;
                }
            }
        }

        public bool? IsActive { get; set; }

        public DateTime? LastActiveOn { get; set; }
    }
}
