using System.ComponentModel.DataAnnotations;

namespace ToyStoreClient.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "CurrentPassword is required")]
        public string? CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "NewPassword is required")]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "ConfirmNewPassword is required")]
        public string? ConfirmNewPassword { get; set; }
    }
}
