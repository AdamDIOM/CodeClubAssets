using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeClubAssets.Models
{
    public class Loans
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [MaxLength(10)]
        public string AssetID { get; set; }
        [Required]
        [MaxLength(10)]
        public string MemberID { get; set; }
        [Required]
        public DateTime DateBorrowed { get; set; }
        [Required]
        public int LengthBorrowed { get; set; }
        [Required]
        public bool History { get; set; }
    }
}
