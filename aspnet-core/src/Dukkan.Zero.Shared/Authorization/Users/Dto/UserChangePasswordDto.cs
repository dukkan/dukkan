using System.ComponentModel.DataAnnotations;

namespace Dukkan.Authorization.Users.Dto
{
    public class UserChangePasswordDto
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
