using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using TruyenVNAPI.Model;

namespace TruyenVNClient.Pages
{
    public class ChangePasswordModel : PageModel
    {
        private readonly HttpClient client = null;
        private string UserAPIUrl = "";


        public ChangePasswordModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            UserAPIUrl = "https://localhost:7029/odata/Users";
        }

        [BindProperty]
        public int user_id { get; set; }
        public void OnGet(int id)
        {
            user_id = id;
        }

        public IActionResult OnPost(string password_old, string password_new, string confirm_password_new)
        {
           if(!checkPassword(user_id, password_old))
            {
                ViewData["error"] = "Passwor not correct!";
                return Page();
            }
            if (!password_new.Equals(confirm_password_new))
            {
                ViewData["error"] = "Passwor new and password confirm not match!";
                return Page();
            }
            var user = GetUser(user_id);
            user.Password = password_new;
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync($"{UserAPIUrl}({user.user_id})", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                ViewData["error"] = response.Content.ReadAsStringAsync();
                return Page();
            }
        }

        public User GetUser(int id)
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{UserAPIUrl}/{id}").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic x = JObject.Parse(strData);
            var user = new User
            {
                user_id = (int)x["user_id"],
                Name = (string)x["Name"],
                Email = (string)x["Email"],
                DateOfBirth = (string)x["DateOfBirth"],
                Gender = (string)x["Gender"],
                isActive = (bool)x["isActive"],
                CreateAt = (DateTime)x["CreateAt"],
                Role = (int)x["Role"]
            };
            return user;
        }


        public bool checkPassword(int id, string password)
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{UserAPIUrl}/{id}").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic x = JObject.Parse(strData);
            var User = new User
            {
                Password = (string)x["Password"]
            };
            if (User.Password.Equals(password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
