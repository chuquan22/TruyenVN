using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using TruyenVNAPI.DTO;
using TruyenVNAPI.Model;


namespace TruyenVNAPI.Controllers
{
    
    public class ChaptersController : ODataController
    {
        private readonly TruyenVNDbContext _context;
        private readonly IMapper _mapper;

        public ChaptersController(TruyenVNDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.Chapters);
        }

        [EnableQuery]
        public IActionResult Get(int key)
        {
            var author = _context.Chapters.Find(key);
            if (author == null)
            {
                return BadRequest("chapter not found");
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
                    return BadRequest("Not Find chapter");
                }
                _mapper.Map(authorDTO, author);
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
