using System.ComponentModel.DataAnnotations;
namespace MVCReferenceProject.Models;
public class LoginModel{
    [Required]
    [MinLength(5)]
    public string? Username {get; set;}

   [MinLength(5)]
   [Required]
    public string? Password {get; set;}


    //Url EmailAddress filter
}