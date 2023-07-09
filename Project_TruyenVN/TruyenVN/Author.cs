using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TruyenVNAPI.Model
{
    public class Author
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int author_id { get; set; }
        [Required]
        [StringLength(50)]
        public string author_name { get; set;}
        [Required]
        [StringLength(250)]
        public string author_description { get; set;}

        public virtual ICollection<Story> Stories { get; set; }

    }
}
