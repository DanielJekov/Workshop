namespace Workshop.Services.Data.Notes
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Workshop.Data.Common.Repositories;
    using Workshop.Data.Models;
    using Workshop.Services.InputDtos.Notes;
    using Workshop.Services.Mapping;

    public class NotesService : INotesService
    {
        private readonly IDeletableEntityRepository<Note> noteRepository;

        public NotesService(IDeletableEntityRepository<Note> noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        public T GetById<T>(int id)
        {
            return this.noteRepository.AllAsNoTracking().Where(x => x.Id == id).To<T>().FirstOrDefault();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.noteRepository.AllAsNoTracking().To<T>().ToList();
        }

        public async Task CreateAsync(NoteCreateInputDto input)
        {
            var note = new Note()
            {
                Name = input.Name,
                Content = input.Content,
            };

            await this.noteRepository.AddAsync(note);
            await this.noteRepository.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new System.Exception();
        }

        public async Task UpdateAsync(NoteUpdateInputDto input)
        {
            var note = this.noteRepository.All().Where(x => x.Id == input.Id).FirstOrDefault();
            note.Name = input.Name;
            note.Content = input.Content;

            await this.noteRepository.SaveChangesAsync();
        }
    }
}
