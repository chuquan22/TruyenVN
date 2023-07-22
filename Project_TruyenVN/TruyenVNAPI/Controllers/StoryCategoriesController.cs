
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using TruyenVN;
using TruyenVNAPI.Model;

namespace TruyenVNAPI.Controllers
{
    
    public class StoryCategoriesController : ControllerBase
    {
        private readonly TruyenVNDbContext _context;

        public StoryCategoriesController(TruyenVNDbContext context)
        {
            _context = context;
        }

        [EnableQuery(PageSize = 10)]
        public IActionResult Get()
        {
            return Ok(_context.StoryCategories);
        }

        [HttpGet("GetStoryByCategory/{categoryId}")]
        public IActionResult GetCategory(int categoryId)
        {
            var Category = _context.StoryCategories.Where(x => x.cate_id == categoryId);
            if (Category == null)
            {
                return BadRequest("Not found Story");
            }
            
            return Ok(Category);
        }

        [HttpGet("GetCategoryByStory/{storyId}")]
        public IActionResult GetStory(int storyId)
        {
            var story = _context.StoryCategories.Where(x => x.story_id == storyId).ToList();
            if (story == null)
            {
                return BadRequest("Not found Story");
            }
            return Ok(story);
        }

        [HttpPost]
        public IActionResult Post([FromBody] StoryCategory storyCategory)
        {
            try
            {
                _context.StoryCategories.Add(storyCategory);
                _context.SaveChanges();

                return Ok(storyCategory);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
