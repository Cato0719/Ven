using System.ComponentModel.DataAnnotations;

namespace Ven.Shared.Entities
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        [Required]
        public string Name { get; set; } = null;
        //public bool Activate { get; set; }
    }
}
