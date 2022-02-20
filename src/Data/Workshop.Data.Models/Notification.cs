namespace Workshop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Workshop.Data.Common.Models;

    public class Notification : BaseDeletableModel<int>
    {
        [Required]
        public string Description { get; set; }

        public bool IsSeen { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        public ApplicationUser Receiver { get; set; }

        //Notification may be from the server and so it must support no sender.
        public string SenderId { get; set; }

        public ApplicationUser Sender { get; set; }
    }
}
