using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace NetCoreWebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public int OgrenciNo { get; set; }
        public string? Adres {  get; set; }
        public string? Fakulte { get; set; }
        public string? Bolum { get; set; }

    }
}
