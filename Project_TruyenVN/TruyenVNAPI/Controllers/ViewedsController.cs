using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using TruyenVNAPI.DTO;
using TruyenVNAPI.Model;

namespace TruyenVNAPI.Controllers
{
    public class ViewedsController : ODataController
    {

        private readonly TruyenVNDbContext _context;
        private readonly IMapper _mapper;

        public ViewedsController(TruyenVNDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [EnableQuery(PageSize = 10)]
        public IActionResult Get()
        {
            return Ok(_context.Vieweds);
        }

        [EnableQuery]
        public IActionResult Get(int key)
        {
            var viewed = _context.Vieweds.Find(key);
            if (viewed == null)
            {
                return BadRequest("Viewed not found");
            }
            return Ok(viewed);
        }

        [EnableQuery]
        public IActionResult Post([FromBody] ViewedDTO viewedDTO)
        {
            try
            {
                var view = _mapper.Map<Viewed>(viewedDTO);
                _context.Vieweds.Add(view);
                _context.SaveChanges();

                return Created(view);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}

    
