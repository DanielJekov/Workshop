namespace Workshop.Services.Data.Users
{
    using System.Collections.Generic;

    public interface IUsersService
    {
        public IEnumerable<T> GetAll<T>();
    }
}
