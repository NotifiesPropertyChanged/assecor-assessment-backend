namespace DotNetApiChallenge.Dto
{
    /// <summary>
    /// Represents a person with a nested color while concealing fields not needed for the public
    /// </summary>
    public class ComplexPersonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public ColorDto Color { get; set; }
    }
}
