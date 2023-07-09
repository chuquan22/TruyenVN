using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace TruyenVNAPI.Model
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int user_id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [AllowNull]
        [StringLength(50)]
        public string Status { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public String DateOfBirth { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public DateTime CreateAt { get; set; }
        [Required]
        [DefaultValue(0)]
        public int Role { get; set;}

        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Viewed> Vieweds { get; set; }

    }
}
