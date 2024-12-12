using DotNetApiChallenge.Models;
using System.Diagnostics.Metrics;

namespace DotNetApiChallenge.Dto
{
    /// <summary>
    /// Represents a person with a color as a string, while concealing fields not needed for the public
    /// </summary>
    public class PersonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string Color { get; set; }
    }
}
