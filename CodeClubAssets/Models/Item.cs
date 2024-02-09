using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CodeClubAssets.Models
{
    public class Item
    {
        [Key]
        [Required]
        [MaxLength(10)]
        public string ID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(250)]
        public string Description { get; set; }
        [Required]
        [MaxLength(3)]
        public string Location { get; set; }
        [MaxLength(100)]
        public string? SerialNumber { get; set; }
        [MaxLength(10)]
        public string? ParentID { get; set; }
        [MaxLength(150)]
        public string? Tags { get; set; }
        [Required]
        public bool PATRequired { get; set; }
        [MaxLength(450)]
        public string? Note { get; set; }
        [Required]
        public bool Out { get; set; }
    }
}
