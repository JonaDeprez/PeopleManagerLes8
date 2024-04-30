using System.ComponentModel.DataAnnotations;

namespace PeopleManager.Model
{
    public class Organization
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public string? Description { get; set; }

        public IList<Person> Members { get; set; } = new List<Person>();

    }
}
