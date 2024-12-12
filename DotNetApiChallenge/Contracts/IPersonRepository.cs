using DotNetApiChallenge.Models;

namespace DotNetApiChallenge.Contracts
{
    public interface IPersonRepository
    {
        ICollection<Person> GetPersons();
        Person GetPerson(int personId);
        ICollection<Person> GetPersonsByColor(int colorId);
        bool PersonExists(int personId);
        bool CreatePerson(Person person);
        bool UpdatePerson(Person person);
        bool DeletePerson(Person person);
        bool Save();
    }
}
