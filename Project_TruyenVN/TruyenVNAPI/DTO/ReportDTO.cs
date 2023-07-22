using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TruyenVNAPI.DTO
{
    public class ReportDTO
    {
        public int chapter_id { get; set; }
        public string message { get; set; }
        public DateTime send_at { get; set; }
        public int user_id { get; set; }
    }
}
