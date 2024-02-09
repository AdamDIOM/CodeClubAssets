using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CodeClubAssets.Models
{
    public class PATTest
    {
        [Key]
        [Required]
        [MaxLength(10)]
        public string TestID { get; set; }
        [Required]
        [MaxLength(10)]
        public string AssetID { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public bool Outcome { get; set; }

    }
}
