namespace Workshop.Web.ViewModels.Notifications
{
    using System.Collections.Generic;

    public class NotificationsViewComponentViewModel
    {
        public int UnseenCount { get; set; }

        public ICollection<NotificationViewModel> Notifications { get; set; }
    }
}
