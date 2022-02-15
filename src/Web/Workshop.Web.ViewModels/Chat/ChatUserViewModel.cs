namespace Workshop.Web.ViewModels.Chat
{
    using System;

    public class ChatUserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? LastActiveOn { get; set; }
    }
}
