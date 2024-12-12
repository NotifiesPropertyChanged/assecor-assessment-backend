using DotNetApiChallenge.Contracts;
using DotNetApiChallenge.DataContext;
using DotNetApiChallenge.Models;

namespace DotNetApiChallenge.Services
{
    /// <summary>
    /// Handles the interactions with EF and the entity "Person" and decouples the controllers from the Db framework.
    /// If another ORM or framework were to be used on the aplication this class should be renamed
    /// to a more descriptive name ("EntityFrameworkColorRepository") and the
    /// dependecy injection from the app builder changed to use the new Repository service.
    /// <see cref="Person"/>
    /// </summary>
    public class PersonRepository : IPersonRepository
    {
        private readonly ApiChallengeDataContext _context;

        public PersonRepository(ApiChallengeDataContext context)
        {
            _context = context;
            var debugColor = _context.Colors.ToList();
            var debugPerson = _context.Persons.ToList();
        }

        public bool CreatePerson(Person Person)
        {
            _context.Add(Person);
            return Save();
        }

        public bool DeletePerson(Person Person)
        {
            _context.Remove(Person);
            return Save();
        }

        public Person GetPerson(int PersonId)
        {
            return _context.Persons.Where(o => o.Id == PersonId).FirstOrDefault();
        }

        public ICollection<Person> GetPersonsByColor(int colorId)
        {
            return _context.Persons.Where(p => p.Color.Id == colorId).ToList(); //Select(o => o.Person)
        }

        public ICollection<Person> GetPersons()
        {
            return _context.Persons.ToList();
        }

        public bool PersonExists(int PersonId)
        {
            return _context.Persons.Any(o => o.Id == PersonId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePerson(Person Person)
        {
            _context.Update(Person);
            return Save();
        }
    }
}
