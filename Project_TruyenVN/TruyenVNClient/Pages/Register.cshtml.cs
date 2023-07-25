using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using TruyenVNAPI.DTO;
using TruyenVNAPI.Model;

namespace TruyenVNClient.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly HttpClient client = null;
        private string UserAPIUrl = "";
        public RegisterModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            UserAPIUrl = "https://localhost:7029/odata/Users";
        }
        [BindProperty]
        public RegisterDTO User { get; set; }
        [BindProperty]
        public string confirmPassword { get; set; }
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            if (checkEmailExsit(User.Email))
            {
                ViewData["error"] = "Email was exsit in page!";
                return Page();
            }

            if(User.Password != confirmPassword)
            {
                ViewData["error"] = "Passwor and confrim password not match!";
                return Page();
            }
            
            var json = JsonConvert.SerializeObject(User);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync($"{UserAPIUrl}", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Login");
            }
            else
            {
                ViewData["error"] = response.Content.ReadAsStringAsync();
                return Page();
            }
        }


        public bool checkEmailExsit(string email)
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{UserAPIUrl}?$filter=Email eq '{email}'").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic temp = JObject.Parse(strData);
            if (temp["Email"] != null)
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
