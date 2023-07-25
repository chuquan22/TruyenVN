using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using TruyenVNAPI.Model;
using static System.Net.WebRequestMethods;

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
        [BindProperty]
        public Chapter chapter { get; set; }
        public IActionResult OnGet(int storyid)
        {
            if (storyid == 0) { storyid = 1; }
            var StoryAPIUrl = "https://localhost:7029/odata/Chapters";
            HttpResponseMessage responseMessage = client.GetAsync($"{StoryAPIUrl}?$orderby=chapter_number desc&$filter=story_id eq {storyid}&$Top=1").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic x = JObject.Parse(strData);

            chapterMax = x["value"][0]["chapter_number"] != null ? (int)x["value"][0]["chapter_number"] : 1;
            storyId = storyid;
            return Page();
        }

       
        public IActionResult OnPost(List<IFormFile> images)
        {
            chapter.chapter_number += 1;
            foreach (var imgItem in images)
            {
                chapter.content = "/img/" + imgItem.FileName;
                var json = JsonConvert.SerializeObject(chapter);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync("https://localhost:7029/odata/Chapters", content).Result;
                
            }
            return RedirectToPage("/Admin/Chapters/Index");
        }
    }
}
