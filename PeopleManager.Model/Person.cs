using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PeopleManager.Model
{
    public class Person
    {
        public int Id { get; set; }

        [DisplayName("First name")]
        [Required]
        public required string FirstName { get; set; }

        [DisplayName("Last name")]
        [Required]
        public required string LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        //Optional
        public int? OrganizationId { get; set; }
        public Organization? Organization { get; set; }
    }
}
