using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using TruyenVNAPI.Model;

namespace TruyenVNClient.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly HttpClient client = null;
        private string UserAPIUrl = "";
        public ProfileModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            UserAPIUrl = "https://localhost:7029/odata/Users";
        }
        [BindProperty]
        public User User { get; set; }
        public IActionResult OnGet(int id)
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{UserAPIUrl}/{id}").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic x = JObject.Parse(strData);
            User =  new User
            {
                user_id = (int)x["user_id"],
                Name = (string)x["Name"],
                Email = (string)x["Email"],
                DateOfBirth = (string)x["DateOfBirth"],
                Gender = (string)x["Gender"],
                isActive = (bool)x["isActive"],
                CreateAt = (DateTime)x["CreateAt"],
            };
            return Page();
        }

        public IActionResult OnPost(string gender)
        {
            User.Gender = gender;
            User.isActive= true;
            var json = JsonConvert.SerializeObject(User);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync($"{UserAPIUrl}({User.user_id})", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                return Page();
            }
        }


    }
}
