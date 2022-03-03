namespace Workshop.Services.Data.Search
{
    using System.Collections.Generic;

    public interface ISearchService
    {
        ICollection<T> Users<T>(string searchWord);

        ICollection<T> Topics<T>(string searchWord);

        ICollection<T> Courses<T>(string searchWord);
    }
}
