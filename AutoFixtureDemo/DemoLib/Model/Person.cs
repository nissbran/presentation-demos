namespace DemoLib.Model
{
    using System.ComponentModel.DataAnnotations;

    public class Person
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(20)]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Range(18, 200)]
        public int Age { get; set; }
    }
}
