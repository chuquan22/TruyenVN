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
   
    public class CategoriesController : ODataController
    {
        private readonly TruyenVNDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(TruyenVNDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.Categories);
        }

        [EnableQuery]
        public IActionResult Get(int key)
        {
            var category = _context.Categories.Find(key);
            if (category == null)
            {
                return BadRequest("category not found");
            }
            return Ok(category);
        }

        [EnableQuery]
        public IActionResult Post([FromBody] CategoryDTO categoryDTO)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryDTO);
                _context.Categories.Add(category);
                _context.SaveChanges();

                return Created(category);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [EnableQuery]
        public IActionResult Put(int key, [FromBody] CategoryDTO categoryDTO)
        {
            try
            {
                var category = _context.Categories.Find(key);
                if (category == null)
                {
                    return BadRequest("Not Find category");
                }
                _mapper.Map(categoryDTO, category);
                _context.Categories.Update(category);
                _context.SaveChanges();

                return Updated(category);
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
                var category = _context.Categories.Find(key);
                if (category == null)
                {
                    return BadRequest("Not Find category");
                }
                _context.Categories.Remove(category);
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
