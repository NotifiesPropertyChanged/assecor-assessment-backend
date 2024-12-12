using AutoMapper;
using DotNetApiChallenge.Contracts;
using DotNetApiChallenge.DataContext;
using DotNetApiChallenge.Models;
using System.Diagnostics.Metrics;

namespace DotNetApiChallenge.Services
{
    /// <summary>
    /// Handles the interactions with EF and the entity "Color" and decouples the controllers from the Db framework.
    /// If another ORM or framework were to be used on the aplication, this class should be renamed
    /// to a more descriptive name ("EntityFrameworkColorRepository") and the
    /// dependecy injection from the app builder changed to use the new Repository service.
    /// <see cref="Color"/>
    /// </summary>
    public class ColorRepository : IColorRepository
    {
        private readonly ApiChallengeDataContext _context;
        private readonly IMapper _mapper;

        public ColorRepository(ApiChallengeDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool ColorExists(int id)
        {
            return _context.Colors.Any(c => c.Id == id);
        }

        public bool CreateColor(Color Color)
        {
            _context.Add(Color);
            return Save();
        }

        public bool DeleteColor(Color Color)
        {
            _context.Remove(Color);
            return Save();
        }

        public ICollection<Color> GetColors()
        {
            return _context.Colors.ToList();
        }

        public Color GetColor(int id)
        {
            return _context.Colors.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Person> GetPersonsFromAColor(int ColorId)
        {
            return _context.Persons.Where(c => c.Color.Id == ColorId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateColor(Color Color)
        {
            _context.Update(Color);
            return Save();
        }
    }
}
