namespace DemoLib.Model
{
    using System.ComponentModel.DataAnnotations;

    public class Person : Customer
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
