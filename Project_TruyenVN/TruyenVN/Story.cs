
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using TruyenVN;

namespace TruyenVNAPI.Model
{
    public class Story
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int story_id { get; set; }
        [Required]
        [StringLength(50)]
        public string story_name { get; set; }
        [AllowNull]
        [StringLength(700)]
        public string description { get; set; }
        [AllowNull]
        [DefaultValue(0)]
        public int View { get; set; }
        [ForeignKey("Authors")]
        public int author_id { get; set; }
        [Required]
        public bool isComic { get; set; }
        [AllowNull]
        public string story_image { get; set; }
        [AllowNull]
        public DateTime create_at { get; set; }
        [AllowNull]
        public DateTime update_at { get; set; }

        public virtual Author Authors { get; set; }
        public virtual ICollection<Chapter> Chapters { get; set; }
        public virtual ICollection<StoryCategory> StoryCategories { get; set; }
    }
}
