using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using TruyenVNAPI.Model;

namespace TruyenVNClient.Pages.Admin.Stories
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient client = null;
        private string StoryAPIUrl = "";
        

        public CreateModel()
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
        public void OnGet()
        {
            GetListAuthor();
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
                Story.author_id = Author.author_id;
                Story.create_at = DateTime.Now;
                Story.update_at = DateTime.Now;
                Story.View = 0;
                Story.isComic= true;
                Story.story_image = "/img/" + Story.story_image;
                var json = JsonConvert.SerializeObject(Story);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync($"{StoryAPIUrl}", content).Result;
                if(response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Admin/Stories/Index");
                }
                else
                {
                    ViewData["error"] = response.Content;
                    return Page();
                }
                
            }
            catch(Exception ex)
            {
                ViewData["error"] = ex.Message;
                return Page();
            }
        }
    }
}
