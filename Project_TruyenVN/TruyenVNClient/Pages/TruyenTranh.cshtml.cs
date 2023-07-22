using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http.Headers;
using TruyenVN;
using TruyenVNAPI.DTO;
using TruyenVNAPI.Model;

namespace TruyenVNClient.Pages
{
    public class TruyenTranhModel : PageModel
    {
        private readonly HttpClient client = null;
        private string StoryAPIUrl = "";
        private string CategoryAPI = "";
        private string StoryCategoryAPI = "";
        private string ChapterAPI = "";

        public TruyenTranhModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            StoryAPIUrl = "https://localhost:7029/odata/Stories";
            CategoryAPI = "https://localhost:7029/odata/Categories";
            StoryCategoryAPI = "https://localhost:7029/odata/StoryCategories";
            ChapterAPI = "https://localhost:7029/odata/Chapters";
        }
        [BindProperty]
        public Story Story { get; set; }

        [BindProperty]
        public List<Category> Categories { get; set; }

        public List<StoryCategory> storyCategories { get; set; }

        [BindProperty]
        public List<Chapter> chapters { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                HttpResponseMessage responseMessage = client.GetAsync($"{StoryAPIUrl}/{id}").Result;
                string strData = responseMessage.Content.ReadAsStringAsync().Result;

                dynamic x = JObject.Parse(strData);
                Story = new Story
                {
                    story_id = (int)x["story_id"],
                    story_name = (string)x["story_name"],
                    story_image = (string)x["story_image"],
                    description = (string)x["description"],
                    isComic = (bool)x["isComic"],
                    update_at = (DateTime)x["update_at"],
                };
                GetcategoryId(id);
                GetListChapter(id);
                return Page();
            }catch(Exception ex)
            {
                ViewData["error"] = ex.Message;
                return Page();
            }
        }

        public void GetcategoryId(int storyId)
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{StoryCategoryAPI}?$filter=story_id eq {storyId}").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic temp = JObject.Parse(strData);
            Categories =  ((JArray)temp.value).Select(x => new Category
            {
                cate_id = (int)x["cate_id"],
                cate_name = GetListCategory((int)x["cate_id"])
            }).ToList();
        }

        public string GetListCategory(int id)
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{CategoryAPI}?$filter=cate_id eq {id}").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic x = JObject.Parse(strData);

            return (string)x["value"][0]["cate_name"];
        }

        public void GetListChapter(int id)
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{ChapterAPI}?$filter=story_id eq {id}").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic temp = JObject.Parse(strData);
            chapters = ((JArray)temp.value).Select(x => new Chapter
            {
                chapter_id = (int)x["chapter_id"],
                chapter_number = (int)x["chapter_number"],
                title = (string)x["title"],
                create_at = (DateTime)x["create_at"]
            }).ToList();

        }
    }
}
