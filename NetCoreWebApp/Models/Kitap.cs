using System.ComponentModel.DataAnnotations;

namespace NetCoreWebApp.Models
{
    public class Kitap
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string KitapAdi { get; set; }
        public string Tanim { get; set; }
        [Required]
        public string Yazar {  get; set; }
        [Required]
        [Range(20, 500)]
        public double Fiyat {  get; set; }
    }
}
