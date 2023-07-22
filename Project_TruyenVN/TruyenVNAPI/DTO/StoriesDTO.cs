namespace TruyenVNAPI.DTO
{
    public class StoriesDTO
    {
        public int story_id { get; set; }
        public string story_name { get; set; }

        public string? description { get; set; }

        public int? View { get; set; }

        public int author_id { get; set; }

        public bool isComic { get; set; }
        public double chapter_last { get; set; }
        public string? story_image { get; set; }
        public DateTime create_at { get; set; }
        public DateTime update_at { get; set; }
    }
}
