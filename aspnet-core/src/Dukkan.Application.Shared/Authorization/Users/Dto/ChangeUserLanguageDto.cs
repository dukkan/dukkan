using System.ComponentModel.DataAnnotations;

namespace Dukkan.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}