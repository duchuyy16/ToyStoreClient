using System.ComponentModel.DataAnnotations;

namespace ToyStoreClient.Models
{
    public class ContactModel
    {
        public int ContactId { get; set; }

        [Required]
        public string ContactName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Message { get; set; } = null!;
        public DateTime CreateAt { get; set; }
    }
}
