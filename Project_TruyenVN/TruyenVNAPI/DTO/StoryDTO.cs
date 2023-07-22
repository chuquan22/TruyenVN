using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace TruyenVNAPI.DTO
{
    public class StoryDTO
    {
        public string story_name { get; set; }
      
        public string? description { get; set; }
       
        public int? View { get; set; }
       
        public int author_id { get; set; }
       
        public bool isComic { get; set; }
       
        public string? story_image { get; set; }
        public DateTime create_at { get; set; }
        public DateTime update_at { get; set; }
    }
}
