using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System;
using TruyenVNAPI.Model;
using Newtonsoft.Json.Linq;
using TruyenVNAPI.DTO;

namespace TruyenVNClient.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient client = null;
        private string StoryAPIUrl = "";
        private string ChapterAPIUrl = "";
        public IndexModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            StoryAPIUrl = "https://localhost:7029/odata/Stories";
            ChapterAPIUrl = "https://localhost:7029/odata/Chapters";
        }
        [BindProperty]
        public List<StoriesDTO> ListStoriesUpdate { get; set; }

        [BindProperty]
        public List<StoriesDTO> ListStoriesGood { get; set; }

        [BindProperty]
        public List<Chapter> ListChapterLast { get; set; }

        [BindProperty]
        public StoriesDTO Top1Stories { get; set; }

        [BindProperty]
        public List<StoriesDTO> Top2and3Stories { get; set; }

        [BindProperty]
        public List<StoriesDTO> Top4and5Stories { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                HttpResponseMessage responseMessage = client.GetAsync($"{StoryAPIUrl}?$orderby=update_at desc").Result;
                string strData = responseMessage.Content.ReadAsStringAsync().Result;

                dynamic temp = JObject.Parse(strData);
                ListStoriesUpdate = ((JArray)temp.value).Select(x => new StoriesDTO
                {
                    story_id = (int)x["story_id"],
                    story_name = (string)x["story_name"],
                    story_image = (string)x["story_image"],
                    description = (string)x["description"],
                    isComic = (bool)x["isComic"],
                    update_at = (DateTime)x["update_at"],
                    chapter_last = GetChapterLast((int)x["story_id"]),
                }).ToList();
                GetTop1();
                GetTop2and3();
                GetTop4and5();
                GetListStoryGood();
                return Page();
            }
            catch (Exception ex)
            {
                ViewData["mess"] = ex.Message;
                return Page();
            }
        }

        public List<Chapter> GetChapterLast()
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{ChapterAPIUrl}?$orderby=chapter_number desc").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic temp = JObject.Parse(strData);
            var chapter = ((JArray)temp.value).Select(x => new Chapter
            {
                story_id = (int)x["story_id"],
                chapter_number = (int)x["chapter_number"],
            }).ToList();
            return chapter;
        }

        public double GetChapterLast(int id)
        {
            double chapterlast = 0;
            foreach(var i in GetChapterLast())
            {
                if(i.story_id == id)
                {
                    chapterlast = i.chapter_number;
                    return chapterlast;
                }
            }
            return chapterlast;
        }

        public void GetTop1()
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{StoryAPIUrl}?$orderby=View desc&$top=1").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic temp = JObject.Parse(strData);
            Top1Stories = ((JArray)temp.value).Select(x => new StoriesDTO
            {
                story_id = (int)x["story_id"],
                story_name = (string)x["story_name"],
                story_image = (string)x["story_image"],
                description = (string)x["description"],
                isComic = (bool)x["isComic"],
                update_at = (DateTime)x["update_at"],
                chapter_last = GetChapterLast((int)x["story_id"]),
            }).SingleOrDefault();
        }

        public void GetTop2and3()
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{StoryAPIUrl}?$orderby=View desc&$skip=1&$top=2").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic temp = JObject.Parse(strData);
            Top2and3Stories = ((JArray)temp.value).Select(x => new StoriesDTO
            {
                story_id = (int)x["story_id"],
                story_name = (string)x["story_name"],
                story_image = (string)x["story_image"],
                description = (string)x["description"],
                isComic = (bool)x["isComic"],
                update_at = (DateTime)x["update_at"],
                chapter_last = GetChapterLast((int)x["story_id"]),
            }).ToList();
        }

        public void GetTop4and5()
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{StoryAPIUrl}?$orderby=View desc&$skip=3&$top=2").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic temp = JObject.Parse(strData);
            Top4and5Stories = ((JArray)temp.value).Select(x => new StoriesDTO
            {
                story_id = (int)x["story_id"],
                story_name = (string)x["story_name"],
                story_image = (string)x["story_image"],
                description = (string)x["description"],
                isComic = (bool)x["isComic"],
                update_at = (DateTime)x["update_at"],
                chapter_last = GetChapterLast((int)x["story_id"]),
            }).ToList();
        }


        public void GetListStoryGood()
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{StoryAPIUrl}?$filter=isComic eq true&$top=7").Result;
            string strData = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic temp = JObject.Parse(strData);
            ListStoriesGood = ((JArray)temp.value).Select(x => new StoriesDTO
            {
                story_id = (int)x["story_id"],
                story_name = (string)x["story_name"],
                story_image = (string)x["story_image"],
                description = (string)x["description"],
                isComic = (bool)x["isComic"],
                update_at = (DateTime)x["update_at"],
                chapter_last = GetChapterLast((int)x["story_id"]),
            }).ToList();
        }

    }
}
