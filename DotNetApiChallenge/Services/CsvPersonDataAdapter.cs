using DotNetApiChallenge.Contracts;
using DotNetApiChallenge.Models;
using System.Text.RegularExpressions;

namespace DotNetApiChallenge.Services
{
    /// <summary>
    /// Data Adapter to convert a csv file into a collection of Person objects.\n
    /// Important: SetDataSource has to be called prior to GetData. Architecturally this is not ideal
    /// and introduces a possibility for human error but if we want to migrate the application to another
    /// framework and/or language this helps decoupling the logic from the framework.
    /// For example this could be resolved through constructor injection injecting a config option.
    /// </summary>
    public class CsvPersonDataAdapter : IDataAdapter<Person>
    {
        string _filePath = string.Empty;
        //It just works with one caveat. See comment below. It creates a set of matches,
        //each one containing a set of groups:[full match] [1]-Name,[2]LastName,[3]Five digits zipcode
        //[4]A city or place [5] a digit representing an index of a color
        //For performance reasons the match is precompiled and uses extensively
        //lazy quantifiers.
        readonly Regex _PersonMatcher = new Regex(
            @"(.+?)[\s\n\,]+(.+?)[\s\n\,]+(\d{5})(.+?)[\s\n\,]+?(\d+)(\s*?\n)",
            RegexOptions.Multiline | RegexOptions.Compiled);

        /// <summary>
        /// Sets the location of the csv file containing the information. 
        /// </summary>
        /// <param name="filePath">Full path location of the file</param>
        public void SetDataSource(string filePath )
        {
            _filePath = filePath; ;
        }
        /// <summary>
        /// ´Reads and parses a csv file defined through the method <see cref="SetDataSource">Link text</see>
        /// </summary>
        /// <returns>A Collection of persons</returns>
        /// <exception cref="ArgumentNullException">No datasource was set</exception>
        public List<Person> GetData()
        {
            if (string.IsNullOrEmpty(_filePath))
            {
                //Log
                throw new ArgumentNullException("No source was provided");
            }
            if (!File.Exists(_filePath))
            {
                return new();
            }
                
            //The Regex pattern only works when the string ends with a known delimiter like EOL
            var elements = File.ReadAllText(_filePath) + "\n";
            var matches = _PersonMatcher.Matches(elements);
            var persons = new List<Person>();

            foreach (Match match in matches) 
            {
                try
                {
                    var person = new Person()
                    {
                        Name        = match.Groups[1].Value,
                        Lastname    = match.Groups[2].Value,
                        Zipcode     = match.Groups[3].Value,
                        City        = match.Groups[4].Value.TrimStart(), //The regex should be improved to make the trimming unnecessary
                        ColorId     = Convert.ToInt32(match.Groups[5].Value)
                    };

                    persons.Add(person);
                }
                catch (Exception)
                {
                    //Log Exception e
                    continue;
                }            
            }

            return persons;
        }
    }
}
