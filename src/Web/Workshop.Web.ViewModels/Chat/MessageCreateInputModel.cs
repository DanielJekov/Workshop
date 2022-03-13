namespace Workshop.Web.ViewModels.Chat
{
    using System.ComponentModel.DataAnnotations;

    public class MessageCreateInputModel
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public string SenderId { get; set; }

        [Required]
        public string ReceiverId { get; set; }
    }
}
