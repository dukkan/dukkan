using System.ComponentModel.DataAnnotations;

namespace Dukkan.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}