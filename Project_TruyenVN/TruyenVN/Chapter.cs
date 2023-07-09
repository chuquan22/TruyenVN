using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace TruyenVNAPI.Model
{
    public class Chapter
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int chapter_id { get; set; }
        [Required]
        public double chapter_number { get; set; }
        [ForeignKey("Stories")]
        public int story_id { get; set; }
        [AllowNull]
        public string title { get; set; }
        [Required]
        public string content { get; set; }
        [AllowNull]
        public DateTime create_at { get; set; }
        [AllowNull]
        public DateTime update_at { get; set; }

        public virtual Story Stories { get; set; }
        public virtual ICollection<Viewed> Vieweds { get; set; }

    }
}
