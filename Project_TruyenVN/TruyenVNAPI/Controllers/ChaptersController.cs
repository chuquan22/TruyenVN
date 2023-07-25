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
        public IActionResult Post([FromBody] ChapterDTO chapterDTO)
        {
            try
            {
                var chapter = _mapper.Map<Chapter>(chapterDTO);
                chapter.create_at= DateTime.Now;
                chapter.update_at= DateTime.Now;
                _context.Chapters.Add(chapter);
                _context.SaveChanges();

                return Created(chapter);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [EnableQuery]
        public IActionResult Put(int key, [FromBody] ChapterDTO chapterDTO)
        {
            try
            {
                var chapter = _context.Chapters.Find(key);
                if (chapter == null)
                {
                    return BadRequest("Not Find chapter");
                }
                _mapper.Map(chapterDTO, chapter);
                _context.Chapters.Update(chapter);
                _context.SaveChanges();

                return Updated(chapter);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
