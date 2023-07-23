using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using TruyenVNAPI.Model;

namespace TruyenVNClient.Pages
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient client = null;
        private string UserAPIUrl = "";

        public LoginModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            UserAPIUrl = "https://localhost:7029/odata/Users";
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(string email, string password)
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{UserAPIUrl}?$filter=Email eq '{email} and password eq '{password}'").Result;
            
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic x = JObject.Parse(strData);
            int id = (int)x["user_id"];
            int role = (int)x["Role"];

            if (role == 0)
            {
                HttpContext.Session.SetInt32("id", id);
                HttpContext.Session.SetString("emailUser", email);
                return RedirectToPage("/Index");
            }else if(role == 1)
            {
                HttpContext.Session.SetInt32("id", id);
                HttpContext.Session.SetString("emailAdmin", email);
                return RedirectToPage("/Admin");
            }
            else
            {
                ViewData["error"] = "Email or password not correct";
                return Page();
            }
        }
    }
}
