namespace Workshop.Services.InputDtos.Messages
{
    using System.ComponentModel.DataAnnotations;

    public class MessageCreateInputDto
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public string SenderId { get; set; }

        [Required]
        public string ReceiverId { get; set; }
    }
}
