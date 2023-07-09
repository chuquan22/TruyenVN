using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruyenVNAPI.Model;

namespace TruyenVN
{
    public  class StoryCategory
    {
        [Key, ForeignKey("Stories")]
        public int story_id { get; set; }
        [Key, ForeignKey("Categories")]
        public int cate_id { get; set; }

        public virtual Story Story { get; set; }
        public virtual Category Category { get; set; }
    }
}
