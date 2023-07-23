using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using TruyenVN;
using TruyenVNAPI.Model;

namespace TruyenVNClient.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly HttpClient client = null;
        private string StoryAPIUrl = "";
        private string CategoryAPI = "";
        private string storyCategoryAPI = "";
       

        public CategoryModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            StoryAPIUrl = "https://localhost:7029/odata/Stories";
            CategoryAPI = "https://localhost:7029/odata/Categories";
            storyCategoryAPI = "https://localhost:7029/odata/StoryCategories";
        }
        [BindProperty]
        public List<Story> Stories { get; set; }

        [BindProperty]
        public List<Category> Categories { get; set; }
        [BindProperty]
        public Category CurrentCategory { get; set; }
        public List<StoryCategory> storyCategories { get; set; }
        
        public IActionResult OnGet(int catgoryId)
        {
            try
            {
                HttpResponseMessage responseMessage = client.GetAsync($"{CategoryAPI}/{catgoryId}").Result;
                string strData = responseMessage.Content.ReadAsStringAsync().Result;

                dynamic x = JObject.Parse(strData);
                CurrentCategory = new Category
                {
                    cate_id = (int)x["cate_id"],
                    cate_name = (string)x["cate_name"],
                    cate_description = (string)x["cate_description"]
                };
                GetAllCategory();
                GetListStoryId(catgoryId);
                List<Story> stories= new List<Story>();
                foreach(var item in storyCategories)
                {
                   stories.Add(getStoryByCategory(item.story_id));
                }
                Stories = stories;
                return Page();
            }catch(Exception ex)
            {
                return Page();
            }
        }

        public void GetAllCategory()
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{CategoryAPI}").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic temp = JObject.Parse(strData);
            Categories = ((JArray)temp.value).Select(x => new Category
            {
                cate_id = (int)x["cate_id"],
                cate_name = (string)x["cate_name"]
            }).ToList();
        }

        public void GetListStoryId(int categoryId)
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{storyCategoryAPI}?$filter=cate_id eq {categoryId}").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic temp = JObject.Parse(strData);
            storyCategories = ((JArray)temp.value).Select(x => new StoryCategory
            {
                story_id = (int)x["story_id"]
            }).ToList();
        }

        public Story getStoryByCategory(int Id)
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{StoryAPIUrl}/{Id}").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic temp = JObject.Parse(strData);
            Story story = new Story
            {
                story_id = (int)temp["story_id"],
                story_name = (string)temp["story_name"],
                story_image = (string)temp["story_image"],
            };
            return story;
        }
    }
}
