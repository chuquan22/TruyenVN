using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public IndexModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            StoryAPIUrl = "https://localhost:7029/odata/Chapters";
        }
        [BindProperty]
        public List<Chapter> ListChapters { get; set; }
        [BindProperty]
        public double count { get; set; }
        [BindProperty]
        public int currentcount { get; set; }
        public IActionResult OnGet(int pageNumber)
        {
            currentcount = (pageNumber == 0) ? 1 : pageNumber;
            GetChapter(currentcount);
            GetCount();
            return Page();
        }

        public void GetChapter(int count1)
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{StoryAPIUrl}?$expand=Stories&$skip={(count1 - 1) * 10}").Result;
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
                   story_name = (string)x["story_name"]
               }
            }).ToList();
        }
        public void GetCount()
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{StoryAPIUrl}/$count").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            double totalNumber = double.Parse(strData);
            double number = totalNumber / 10.0;
            count = Math.Ceiling(number);
        }

        public async Task<IActionResult> OnGetDelete(int id)
        {
            GetChapter(1);
            GetCount();
            HttpResponseMessage responseMessage = await client.DeleteAsync($"{StoryAPIUrl}({id})");

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
    }
}
