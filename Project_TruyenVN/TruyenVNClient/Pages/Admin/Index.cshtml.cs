using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace TruyenVNClient.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient client = null;
        private string StoryAPIUrl = "";
        private string ChapterAPIUrl = "";
        private string UserAPIUrl = "";
        public IndexModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            StoryAPIUrl = "https://localhost:7029/odata/Stories";
            ChapterAPIUrl = "https://localhost:7029/odata/Chapters";
        }
        public void OnGet()
        {
        }
    }
}
