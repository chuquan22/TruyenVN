using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using TruyenVNAPI.Model;

namespace TruyenVNClient.Pages.Admin.Reports
{
    public class IndexModel : PageModel
    {

        private readonly HttpClient client = null;
        private string ReportAPIUrl = "";
        private string StoryAPIUrl = "";
        public IndexModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ReportAPIUrl = "https://localhost:7029/odata/Reports";
            StoryAPIUrl = "https://localhost:7029/odata/Stories";
        }
        [BindProperty]
        public List<Report> ListReport { get; set; }
        public IActionResult OnGet()
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{ReportAPIUrl}?$expand=Chapters&$expand=Users ").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic temp = JObject.Parse(strData);
            ListReport = ((JArray)temp.value).Select(x => new Report
            {
                Id = (int)x["Id"],
                Chapters = new Chapter
                {
                    title = (string)x["Chapters"]["title"],
                    Stories = new Story
                    {
                        story_name = GetStory((int)x["Chapters"]["story_id"])
                    }
                },
                user_id = (int)x["user_id"],
                Users = new User
                {
                    Name = (string)x["Users"]["Name"]
                },
                message = (string)x["message"],
                send_at = (DateTime)x["send_at"]

            }).ToList();
            return Page();
        }

        public string GetStory(int id)
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{StoryAPIUrl}/{id}").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic x = JObject.Parse(strData);
            var Story = new Story
            {
                story_id = (int)x["story_id"],
                story_name = (string)x["story_name"],
            };
            return Story.story_name;
        }

        public IActionResult OnGetRemove(int id)
        {
            HttpResponseMessage responseMessage = client.DeleteAsync($"{ReportAPIUrl}({id})").Result;

            string strData =  responseMessage.Content.ReadAsStringAsync().Result;
            ViewData["error"] = strData;
            return RedirectToPage("/Admin/Reports/Index");

        }
    }
}
