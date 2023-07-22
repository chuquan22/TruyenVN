using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TruyenVNAPI.DTO
{
    public class ViewedDTO
    {
        public int chapter_id { get; set; }
        public int user_id { get; set; }
        public DateTime date_view { get; set; }
    }
}
