using System.ComponentModel.DataAnnotations;

namespace ToyStoreClient.Models
{
    public class ResetPasswordModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "NewPassword is required")]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "ConfirmNewPassword is required")]
        public string? ConfirmNewPassword { get; set; }

        public string? Token { get; set; }
    }
}
