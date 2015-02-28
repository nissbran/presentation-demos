namespace DemoLib.Model
{
    using System.ComponentModel.DataAnnotations;

    public abstract class Person
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(20)]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }
    }
}
