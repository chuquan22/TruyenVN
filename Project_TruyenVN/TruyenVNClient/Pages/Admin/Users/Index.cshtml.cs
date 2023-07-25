using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using System.Text;
using TruyenVNAPI.Model;

namespace TruyenVNClient.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient client = null;
        private string UserAPIUrl = "";
        public IndexModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            UserAPIUrl = "https://localhost:7029/odata/Users";
        }
        [BindProperty]
        public List<User> Users { get; set; }
        [BindProperty]
        public double count { get; set; }
        [BindProperty]
        public int currentcount { get; set; }
        public async Task<IActionResult> OnGetAsync(int pageNumber)
        {
            currentcount = (pageNumber == 0) ? 1 : pageNumber;
            GetCount();
            HttpResponseMessage responseMessage = client.GetAsync($"{UserAPIUrl}?$filter=Role eq 0&$skip={(currentcount - 1) * 10}").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic temp = JObject.Parse(strData);
            Users = ((JArray)temp.value).Select(x => new User
            {
               user_id = (int)x["user_id"],
               Name = (string)x["Name"],
               Email = (string)x["Email"],
               DateOfBirth = (string)x["DateOfBirth"],
               Gender = (string)x["Gender"],
               isActive = (bool)x["isActive"],
               CreateAt = (DateTime)x["CreateAt"],
            }).ToList();
            return Page();
        }

        public IActionResult OnGetActive(int id)
        {
            var user = GetUser(id);
            user.isActive = user.isActive == true ? false : true;
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync($"{UserAPIUrl}({id})", content).Result;
            string strData = response.Content.ReadAsStringAsync().Result;
            return RedirectToPage("/Admin/Users/Index");
        }

       

        public User GetUser(int id)
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{UserAPIUrl}/{id}").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic x = JObject.Parse(strData);
            var User =  new User
            {
                user_id = (int)x["user_id"],
                Name = (string)x["Name"],
                Email = (string)x["Email"],
                DateOfBirth = (string)x["DateOfBirth"],
                Gender = (string)x["Gender"],
                isActive = (bool)x["isActive"],
                CreateAt = (DateTime)x["CreateAt"],
            };
            return User;
        }

        public void GetCount()
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{UserAPIUrl}/$count").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            double totalNumber = double.Parse(strData);
            double number = totalNumber / 12.0;
            count = Math.Ceiling(number);
        }

    }
}
