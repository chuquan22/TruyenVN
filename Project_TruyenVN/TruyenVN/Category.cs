using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TruyenVN;

namespace TruyenVNAPI.Model
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cate_id { get; set; }
        [Required]
        [StringLength(50)]
        public string cate_name { get; set; }
        [Required]
        [StringLength(500)]
        public string cate_description { get; set; }

        public virtual ICollection<StoryCategory> StoryCategories { get; set; }

    }
}
