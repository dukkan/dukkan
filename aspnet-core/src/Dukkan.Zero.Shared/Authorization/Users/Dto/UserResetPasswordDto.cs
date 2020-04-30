using System.ComponentModel.DataAnnotations;

namespace Dukkan.Authorization.Users.Dto
{
    public class UserResetPasswordDto
    {
        [Required]
        public string AdminPassword { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
