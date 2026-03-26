using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Users.Dto;

public class ChangeUserLanguageDto
{
    [Required]
    public string LanguageName { get; set; }
}