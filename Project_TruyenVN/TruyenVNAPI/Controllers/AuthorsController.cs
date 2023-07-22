using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using TruyenVNAPI.DTO;
using TruyenVNAPI.Model;

namespace TruyenVNAPI.Controllers
{
   
    public class AuthorsController : ODataController
    {
        private readonly TruyenVNDbContext _context;
        private readonly IMapper _mapper;

        public AuthorsController(TruyenVNDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [EnableQuery(PageSize = 10)]
        public IActionResult Get()
        {
            return Ok(_context.Authors);
        }

        [EnableQuery]
        public IActionResult Get(int key)
        {
            var author = _context.Authors.Find(key);
            if (author == null)
            {
                return BadRequest("Author not found");
            }
            return Ok(author);
        }

        [EnableQuery]
        public IActionResult Post([FromBody] AuthorDTO authorDTO)
        {
            try
            {
                var author = _mapper.Map<Author>(authorDTO);
                _context.Authors.Add(author);
                _context.SaveChanges();

                return Created(author);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [EnableQuery]
        public IActionResult Put(int key, [FromBody] AuthorDTO authorDTO)
        {
            try
            {
                var author = _context.Authors.Find(key);
                if (author == null)
                {
                    return BadRequest("Not Find Author");
                }
                _mapper.Map(author, authorDTO);
                _context.Authors.Update(author);
                _context.SaveChanges();

                return Updated(author);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
