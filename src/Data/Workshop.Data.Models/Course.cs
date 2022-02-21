namespace Workshop.Data.Models
{
    using System.Collections.Generic;

    using Workshop.Data.Common.Models;

    public class Course : BaseDeletableModel<int>
    {
        public Course()
        {
            this.Topics = new HashSet<Topic>();
        }

        public string Name { get; set; }

        public ICollection<Topic> Topics { get; set; }
    }
}
