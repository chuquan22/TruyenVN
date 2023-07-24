using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using TruyenVNAPI.Model;

namespace TruyenVNClient.Pages.Admin.Stories
{
    public class EditModel : PageModel
    {
        private readonly HttpClient client = null;
        private string StoryAPIUrl = "";


        public EditModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            StoryAPIUrl = "https://localhost:7029/odata/Stories";
        }
        [BindProperty]
        public Story Story { get; set; }
        [BindProperty]
        public Author Author { get; set; }

        public List<Author> AuthorList { get; set; }
        public IActionResult OnGet(int id)
        {
            GetListAuthor();
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
                View = (int)x["View"],
                author_id = (int)x["author_id"],
                update_at = (DateTime)x["update_at"],
            };
            return Page();
        }

        public void GetListAuthor()
        {
            HttpResponseMessage responseMessage = client.GetAsync($"https://localhost:7029/odata/Authors").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic temp = JObject.Parse(strData);
            AuthorList = ((JArray)temp.value).Select(x => new Author
            {
                author_id = (int)x["author_id"],
                author_name = (string)x["author_name"]
            }).ToList();
            ViewData["author"] = new SelectList(AuthorList, "author_id", "author_name");
        }

        public IActionResult OnPost()
        {
            GetListAuthor();
            try
            {
                Story.story_image = "/img/" + Story.story_image;
                var json = JsonConvert.SerializeObject(Story);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PutAsync($"{StoryAPIUrl}({Story.story_id})", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Admin/Stories/Index");
                }
                else
                {
                    ViewData["error"] = response.Content;
                    return Page();
                }

            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.Message;
                return Page();
            }
        }
    }
}
