using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace DotNetApiChallenge.Models
{
    /// <summary>
    /// A simple Person. ColorId is an administrative field for relationship tracking under EF
    /// </summary>
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        
        public int? ColorId { get; set; }
        [ForeignKey(nameof(ColorId))]
        public virtual Color? Color { get; set; }
    }
}
