namespace Workshop.Services.InputModels.Notifications
{
    using System.ComponentModel.DataAnnotations;

    public class NotificationCreateInputModel
    {

        public string SenderId { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
