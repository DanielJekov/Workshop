namespace Workshop.Services.Data.Notes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Workshop.Services.InputDtos.Notes;

    public interface INotesService
    {
        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);

        Task CreateAsync(NoteCreateInputDto input);

        Task DeleteAsync(int id);

        Task UpdateAsync(NoteUpdateInputDto input);
    }
}
