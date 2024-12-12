using DotNetApiChallenge.Contracts;
using DotNetApiChallenge.DataContext;
using DotNetApiChallenge.Models;

namespace DotNetApiChallenge.Services
{
    /// <summary>
    /// Service used to populate the database. Colors will be automatically populated but
    /// persons require a dedicated data adapter. The service should not be initiated with
    /// any mechanism which disposes the service after leaving the scope. Disposing the service
    /// disposes the dataContext reference too which leads to unexpected behavior.
    /// </summary>
    public class DbSeedService
    {
        private readonly ApiChallengeDataContext _dataContext;
        private IDataAdapter<Person> _dataAdapter;

        public DbSeedService(ApiChallengeDataContext context )
        {
            this._dataContext = context;        
        }

        public void SetDataAdapter(IDataAdapter<Person> adapter)
        {
            this._dataAdapter = adapter;
        }
        public ApiChallengeDataContext GetContext()
        {
            return this._dataContext;
        }
        /// <summary>
        /// Call this to populate an empty database. If the database already contains colors, no changes
        /// will be performed. If no data adaper was supplied, no person will be created.
        /// </summary>
        public void Seed()
        {
            if (this._dataContext.Colors.Any())
            {
                return;
            }
            //This should be an static resource (enumeration, config class, physical file)
            var colors = new List<Color>()
            {
                new Color()
                {
                    Id = 1,
                    Name = "blau",
                },
                new Color()
                {
                    Id = 2,
                    Name = "grün",
                },
                new Color()
                {
                    Id = 3,
                    Name = "violett",
                },
                new Color()
                {
                    Id = 4,
                    Name = "rot",
                },
                new Color()
                {
                    Id = 5,
                    Name = "gelb",
                },
                new Color()
                {
                    Id = 6,
                    Name = "türkis",
                },
                new Color()
                {
                    Id = 7,
                    Name = "weiß",
                },
            };
            _dataContext.Colors.AddRange(colors);
            _dataContext.SaveChanges();

            if (_dataAdapter is null) 
            {
                return;
            }

            var persons = _dataAdapter.GetData();

            persons.ForEach(p => p.Color = _dataContext.Colors.FirstOrDefault(color => color.Id == p.ColorId));

            _dataContext.Persons.AddRange(persons);
            _dataContext.SaveChanges();

            var debugColor = _dataContext.Colors.ToList();
            var debugPersons = _dataContext.Persons.ToList();
        }
    }
}
