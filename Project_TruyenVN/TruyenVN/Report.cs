using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TruyenVNAPI.Model
{
    public class Report
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Chapters")]
        public int chapter_id { get; set; }
        [Required]
        public string message { get;set; }
        [Required]
        public DateTime send_at { get; set; }
        [ForeignKey("Users")]
        public int user_id { get; set; }

        public virtual Chapter Chapters { get; set; }
        public virtual User Users { get; set; }
    }
}
