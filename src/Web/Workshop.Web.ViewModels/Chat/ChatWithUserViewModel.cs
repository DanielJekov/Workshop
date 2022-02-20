namespace Workshop.Web.ViewModels.Chat
{
    using System.Collections.Generic;

    using Workshop.Web.ViewModels.Users;

    public class ChatWithUserViewModel
    {
        public UserViewModel CurrentUser { get; set; }

        public UserViewModel OtherUser { get; set; }

        public ICollection<MessageViewModel> Messages { get; set; }

        public ICollection<UserViewModel> Users { get; set; }
    }
}
