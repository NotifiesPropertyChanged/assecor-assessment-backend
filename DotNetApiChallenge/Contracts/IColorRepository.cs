using DotNetApiChallenge.Models;
using System.Diagnostics.Metrics;

namespace DotNetApiChallenge.Contracts
{
    public interface IColorRepository
    {
        ICollection<Color> GetColors();
        Color GetColor(int id);
        ICollection<Person> GetPersonsFromAColor(int colorId);
        
        bool ColorExists(int id);
        bool CreateColor(Color color);
        bool UpdateColor(Color color);
        bool DeleteColor(Color color);
        
        bool Save();
    }
}
