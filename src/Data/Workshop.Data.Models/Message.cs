namespace Workshop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Workshop.Data.Common.Models;

    public class Message : BaseDeletableModel<int>
    {
        [Required]
        public string Content { get; set; }

        public bool IsSeen { get; set; }

        [Required]
        public string SenderId { get; set; }

        public ApplicationUser Sender { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        public ApplicationUser Receiver { get; set; }
    }
}
