using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using TruyenVNAPI.DTO;
using TruyenVNAPI.Model;

namespace TruyenVNClient.Pages
{
    public class ReadComicModel : PageModel
    {
        private readonly HttpClient client = null;
        private string StoryAPIUrl = "";
        private string ChapterAPIUrl = "";
        public ReadComicModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            StoryAPIUrl = "https://localhost:7029/odata/Stories";
            ChapterAPIUrl = "https://localhost:7029/odata/Chapters";
        }
        [BindProperty]
        public List<Chapter> Chapters { get; set; }
        [BindProperty]
        public Story story { get; set; }
        public async Task<IActionResult> OnGetAsync(float chapter, int storyId)
        {
            try
            {
                HttpResponseMessage responseMessage = client.GetAsync($"{ChapterAPIUrl}?$expand=Stories&$filter=chapter_number eq {chapter} and story_id eq {storyId}").Result;
                string strData = responseMessage.Content.ReadAsStringAsync().Result;

                dynamic temp = JObject.Parse(strData);
                Chapters = ((JArray)temp.value).Select(x => new Chapter
                {
                    chapter_id = (int)x["chapter_id"],
                    chapter_number = (int)x["chapter_number"],
                    title = (string)x["title"],
                    content = (string)x["content"],
                    create_at = (DateTime)x["create_at"],
                    update_at = (DateTime)x["update_at"],
                    Stories = new Story
                    {
                        story_id = (int)x["Stories"]["story_id"],
                        story_name = (string)x["Stories"]["story_name"],
                        story_image = (string)x["Stories"]["story_image"],
                        author_id = (int)x["Stories"]["author_id"],
                        create_at = (DateTime)x["Stories"]["create_at"],
                        update_at = (DateTime)x["Stories"]["update_at"],
                        description = (string)x["Stories"]["description"],
                        isComic = (bool)x["Stories"]["isComic"],
                        View = (int)x["Stories"]["View"]
                    }
                }).ToList();
                story = Chapters.First().Stories;
                EditViewStory(storyId);
                if (HttpContext.Session.GetInt32("id") != null)
                {
                    AddViewed(Chapters.First().chapter_id);
                }
                return Page();
            }
            catch (Exception ex)
            {
                ViewData["mess"] = ex.Message;
                return Page();
            }
        }

        public void EditViewStory(int storyId)
        {
            story.View += 1;
            var json = JsonConvert.SerializeObject(story);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync($"{StoryAPIUrl}({storyId})", content).Result;

            string strData = response.Content.ReadAsStringAsync().Result;
        }

        public void AddViewed(int chapterId)
        {
            var viewed = new Viewed
            {
                chapter_id = chapterId,
                user_id = (int)HttpContext.Session.GetInt32("id"),
                date_view = DateTime.Now
            };
            var json = JsonConvert.SerializeObject(viewed);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync("https://localhost:7029/odata/Vieweds", content).Result;

            string strData = response.Content.ReadAsStringAsync().Result;
        }

        public IActionResult OnGetReport(int id)
        {
            if(HttpContext.Session.GetInt32("id") == null)
            {
                ViewData["error"] = "error";
                return RedirectToPage("/TruyenTranh?id=3");
            }
            var report = new Report
            {
                chapter_id = id,
                user_id = (int)HttpContext.Session.GetInt32("id"),
                message = "Chương bị lỗi ảnh",
                send_at= DateTime.Now
            };
            var json = JsonConvert.SerializeObject(report);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync("https://localhost:7029/odata/Reports", content).Result;

            string strData = response.Content.ReadAsStringAsync().Result;
            return RedirectToPage("/Index");
        }
    }
}
