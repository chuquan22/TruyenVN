using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using TruyenVNAPI.DTO;
using TruyenVNAPI.Model;

namespace TruyenVNAPI.Controllers
{
  
    public class StoriesController : ODataController
    {
        private readonly TruyenVNDbContext _context;
        private readonly IMapper _mapper;

        public StoriesController(TruyenVNDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [EnableQuery(PageSize = 10)]
        public IActionResult Get()
        {
            return Ok(_context.Stories);
        }

        [EnableQuery]
        public IActionResult Get(int key)
        {
            var story = _context.Stories.Find(key);
            if (story == null)
            {
                return BadRequest("Story not found");
            }
            return Ok(story);
        }

        [HttpPost]
        public IActionResult Post([FromBody] StoryDTO storyDTO)
        {
            try
            {
                var story = _mapper.Map<Story>(storyDTO);
                _context.Stories.Add(story);
                _context.SaveChanges();

                return Created(story);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        public IActionResult Put(int key, [FromBody] StoryDTO storyDTO)
        {
            try
            {
                var story = _context.Stories.Find(key);
                if (story == null)
                {
                    return BadRequest("Not Find Story");
                }
                _mapper.Map(story, storyDTO);
                _context.Stories.Update(story);
                _context.SaveChanges();

                return Updated(story);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [EnableQuery]
        public IActionResult Delete(int key)
        {
            try
            {
                var story = _context.Stories.Find(key);
                if (story == null)
                {
                    return BadRequest("Not Find Story");
                }
                _context.Stories.Remove(story);
                var index = _context.SaveChanges();

                return Ok(index);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
