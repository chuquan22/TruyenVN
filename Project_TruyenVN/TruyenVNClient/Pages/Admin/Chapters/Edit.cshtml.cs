using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using TruyenVNAPI.Model;

namespace TruyenVNClient.Pages.Admin.Chapters
{
    public class EditModel : PageModel
    {
        private readonly HttpClient client = null;
        private string ChapterAPIUrl = "";
        public EditModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ChapterAPIUrl = "https://localhost:7029/odata/Chapters";
        }
        [BindProperty]
        public Chapter chapter { get; set; }
        public IActionResult OnGet(int id)
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{ChapterAPIUrl}/{id}").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic x = JObject.Parse(strData);
            chapter =  new Chapter
            {
                chapter_id = (int)x["chapter_id"],
                chapter_number = (int)x["chapter_number"],
                title = (string)x["title"],
                story_id = (int)x["story_id"],
                content = (string)x["content"],
                create_at = (DateTime)x["create_at"],
                update_at = (DateTime)x["update_at"],
                
            };
            return Page();
        }

        public IActionResult OnPost(IFormFile images)
        {
            chapter.content = "/img/"+images.FileName;
            var json = JsonConvert.SerializeObject(chapter);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync($"{ChapterAPIUrl}({chapter.chapter_id})", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Admin/Chapters/Index");
            }
            else
            {
                ViewData["error"] = response.Content.ReadAsStringAsync().Result;
                return Page();
            }
        }
    }
}
