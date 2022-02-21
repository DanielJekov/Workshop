namespace Workshop.Services.Data.Users
{
    using System.Collections.Generic;

    public interface IUsersService
    {
        IEnumerable<T> GetAll<T>();
    }
}
