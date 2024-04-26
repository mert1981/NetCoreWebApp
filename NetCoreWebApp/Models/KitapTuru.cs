using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetCoreWebApp.Models
{
    public class KitapTuru
    {
        [Key] //primary key
        public int Id { get; set; }


        [Required(ErrorMessage ="Kitap türü adı boş bırakılamaz.")] //not null
        [MaxLength(30)] 
        [DisplayName("Kitap Türü Adı: ")]
        public string Ad { get; set; }
    }
}
