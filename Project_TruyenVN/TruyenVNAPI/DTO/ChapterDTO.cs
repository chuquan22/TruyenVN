using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TruyenVNAPI.DTO
{
    public class ChapterDTO
    {
        public double chapter_number { get; set; }
        public int story_id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public DateTime create_at { get; set; }
        public DateTime update_at { get; set; }
    }
}
