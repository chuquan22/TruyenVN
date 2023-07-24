using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using TruyenVNAPI.Model;

namespace TruyenVNClient.Pages.Admin.Chapters
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient client = null;
        public CreateModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }
        [BindProperty]
        public int chapterMax { get; set; }
        [BindProperty]
        public int storyId { get; set; }
        public IActionResult OnGet(int storyid)
        {
            var StoryAPIUrl = "https://localhost:7029/odata/Chapters";
            HttpResponseMessage responseMessage = client.GetAsync($"{StoryAPIUrl}?$orderby=chapter_number desc&$filter=story_id eq {storyid}&$Top=1").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic x = JObject.Parse(strData);
            var chapter = new Chapter
            {
                chapter_number = (int)x["chapter_number"]
            };
            chapterMax = chapter.chapter_number;
            storyId = storyid;
            return Page();
        }
    }
}
