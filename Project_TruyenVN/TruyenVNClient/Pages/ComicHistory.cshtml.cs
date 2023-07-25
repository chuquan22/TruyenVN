using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using TruyenVNAPI.Model;

namespace TruyenVNClient.Pages
{
    public class ComicHistoryModel : PageModel
    {
        private readonly HttpClient client = null;
        private string ViewedAPIUrl = "";
        private string StoryAPIUrl = "";
        public ComicHistoryModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ViewedAPIUrl = "https://localhost:7029/odata/Vieweds";
            StoryAPIUrl = "https://localhost:7029/odata/Stories";
        }
        [BindProperty]
        public List<Viewed> ListVieweds { get; set; }
        public void OnGet()
        {
            int id = (int)HttpContext.Session.GetInt32("id");
            HttpResponseMessage responseMessage = client.GetAsync($"{ViewedAPIUrl}?$expand=Chapters&$filter=user_id eq {id}").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic temp = JObject.Parse(strData);
            ListVieweds = ((JArray)temp.value).Select(x => new Viewed
            {
                chapter_id = (int)x["chapter_id"],
                Id= (int)x["Id"],
                Chapters = new Chapter
                {
                    Stories = GetStory((int)x["Chapters"]["story_id"])
                }
            }).ToList();
        }

        public Story GetStory(int id)
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{StoryAPIUrl}/{id}").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic x = JObject.Parse(strData);
           var Story = new Story
            {
                story_id = (int)x["story_id"],
                story_name = (string)x["story_name"],
                story_image = (string)x["story_image"],
                description = (string)x["description"],
                isComic = (bool)x["isComic"],
                View = (int)x["View"],
                author_id = (int)x["author_id"],
                update_at = (DateTime)x["update_at"],
            };
            return Story;
        }
    }
}
