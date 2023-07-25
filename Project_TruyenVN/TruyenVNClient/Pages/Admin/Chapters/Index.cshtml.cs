using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using TruyenVNAPI.Model;

namespace TruyenVNClient.Pages.Admin.Chapters
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient client = null;
        private string StoryAPIUrl = "";
        private string ChapterAPIUrl = "";
        public IndexModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ChapterAPIUrl = "https://localhost:7029/odata/Chapters";
            StoryAPIUrl = "https://localhost:7029/odata/Stories";
        }
        [BindProperty]
        public List<Chapter> ListChapters { get; set; }
        [BindProperty]
        public double count { get; set; }
        [BindProperty]
        public int currentcount { get; set; }
        [BindProperty]
        public Story Story { get; set; }
        public IActionResult OnGet(int pageNumber)
        {
            currentcount = (pageNumber == 0) ? 1 : pageNumber;
            Story = new Story();
            Story.story_id = 1;
            GetChapter(currentcount);
            GetCount();
            return Page();
        }

        public void GetChapter(int count1)
        {
            ViewData["story"] = new SelectList(GetStoryList(), "story_id", "story_name");
            HttpResponseMessage responseMessage = client.GetAsync($"{ChapterAPIUrl}?$expand=Stories&$skip={(count1 - 1) * 10}").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;


            dynamic temp = JObject.Parse(strData);
            ListChapters = ((JArray)temp.value).Select(x => new Chapter
            {
               chapter_id = (int)x["chapter_id"],
               chapter_number = (int)x["chapter_number"],
               title = (string)x["title"],
               content = (string)x["content"],
               create_at = (DateTime)x["create_at"],
               update_at = (DateTime)x["update_at"],
               Stories = new Story
               {
                   story_name = (string)x["Stories"]["story_name"]
               }
            }).ToList();
        }
        public void GetCount()
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{ChapterAPIUrl}/$count").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            double totalNumber = double.Parse(strData);
            double number = totalNumber / 10.0;
            count = Math.Ceiling(number);
        }

        public async Task<IActionResult> OnGetDelete(int id)
        {
            GetChapter(1);
            GetCount();
            ViewData["story"] = new SelectList(GetStoryList(), "story_id", "story_name");
            HttpResponseMessage responseMessage = await client.DeleteAsync($"{ChapterAPIUrl}({id})");

            if (!responseMessage.IsSuccessStatusCode)
            {
                string strData = await responseMessage.Content.ReadAsStringAsync();
                ViewData["error"] = strData;
                return Page();
            }
            else
            {
                ViewData["success"] = "Delete successfull!";
                return Page();
            }
        }

        public List<Story> GetStoryList()
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{StoryAPIUrl}").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;


            dynamic temp = JObject.Parse(strData);
            var ListStory = ((JArray)temp.value).Select(x => new Story
            {
                story_id = (int)x["story_id"],
                story_name = (string)x["story_name"],
            }).ToList();
            return ListStory;
        }

        public async Task<IActionResult> OnPost()
        {
            ViewData["story"] = new SelectList(GetStoryList(), "story_id", "story_name");
            Story = new Story();
            Story.story_id = 1;
            HttpResponseMessage responseMessage = client.GetAsync($"{ChapterAPIUrl}?$expand=Stories&$filter=story_id eq {Story.story_id}").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic temp = JObject.Parse(strData);
            ListChapters = ((JArray)temp.value).Select(x => new Chapter
            {
                chapter_id = (int)x["chapter_id"],
                chapter_number = (int)x["chapter_number"],
                title = (string)x["title"],
                content = (string)x["content"],
                create_at = (DateTime)x["create_at"],
                update_at = (DateTime)x["update_at"],
                Stories = new Story
                {
                    story_name = (string)x["Stories"]["story_name"]
                }
            }).ToList();
            double number = ListChapters.Count / 10.0;
            count = Math.Ceiling(number);
            return Page();
        }

    }
}
