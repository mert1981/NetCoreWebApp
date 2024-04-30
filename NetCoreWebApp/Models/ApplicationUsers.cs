using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace NetCoreWebApp.Models
{
    public class ApplicationUsers : IdentityUser
    {
        [Required] 
        public int OgrenciNo { get; set; }
        public string? Adres {  get; set; }
        public string? Fakülte { get; set; }
        public string? Bolum { get; set; }

    }
}
