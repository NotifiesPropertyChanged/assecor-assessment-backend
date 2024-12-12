using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace DotNetApiChallenge.Models
{
    /// <summary>
    /// A color. The collection exists for relationship tracking under EF.
    /// The ToString Override simplifies the representation of the object at the public end of the API
    /// </summary>
    public class Color
    {
        public int Id { get; set; }       
        
        public string Name { get; set; }
        public virtual ICollection<Person> Persons { get; set; }

        public override string ToString() => Name;        
    }
}
