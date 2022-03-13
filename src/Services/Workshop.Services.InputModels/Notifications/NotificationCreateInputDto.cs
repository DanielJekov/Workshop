namespace Workshop.Services.InputDtos.Notifications
{
    using System.ComponentModel.DataAnnotations;

    public class NotificationCreateInputDto
    {

        public string SenderId { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
