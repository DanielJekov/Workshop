namespace Workshop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Workshop.Data.Common.Models;

    public class Course : BaseDeletableModel<int>
    {
        public Course()
        {
            this.Topics = new HashSet<Topic>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Topic> Topics { get; set; }
    }
}
