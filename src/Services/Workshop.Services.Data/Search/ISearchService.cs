namespace Workshop.Services.Data.Search
{
    using System.Collections.Generic;

    public interface ISearchService
    {
        ICollection<T> Users<T>(string searchWord);
    }
}
