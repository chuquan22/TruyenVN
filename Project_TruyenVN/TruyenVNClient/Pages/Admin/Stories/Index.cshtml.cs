using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using TruyenVNAPI.DTO;
using TruyenVNAPI.Model;

namespace TruyenVNClient.Pages.Admin.Stories
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
            StoryAPIUrl = "https://localhost:7029/odata/Stories";
        }
        [BindProperty]
        public List<Story> ListStories { get; set; }
        [BindProperty]
        public double count { get; set; }
        [BindProperty]
        public int currentcount { get; set; }
        public IActionResult OnGet(int pageNumber)
        {
            currentcount = (pageNumber == 0) ? 1 : pageNumber;
            GetStory(currentcount);
            GetCount();
            return Page();
        }
        public void GetStory(int count1)
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{StoryAPIUrl}?$expand=Authors&$skip={(count1 - 1) * 12}").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic temp = JObject.Parse(strData);
            ListStories = ((JArray)temp.value).Select(x => new Story
            {
                story_id = (int)x["story_id"],
                story_name = (string)x["story_name"],
                story_image = (string)x["story_image"],
                description = (string)x["description"],
                isComic = (bool)x["isComic"],
                update_at = (DateTime)x["update_at"],
                create_at = (DateTime)x["create_at"],
                View = (int)x["View"],
                Authors = new Author
                {
                    author_name = (string)x["Authors"]["author_name"]
                }
            }).ToList();
        }
        public void GetCount()
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{StoryAPIUrl}/$count").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            double totalNumber = double.Parse(strData);
            double number = totalNumber / 12.0;
            count = Math.Ceiling(number);
        }

        public async Task<IActionResult> OnGetDelete(int id)
        {
            GetStory(1);
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
