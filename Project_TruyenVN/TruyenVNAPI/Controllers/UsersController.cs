using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using TruyenVNAPI.DTO;
using TruyenVNAPI.Model;

namespace TruyenVNAPI.Controllers
{
    public class UsersController : ODataController
    {
        private readonly TruyenVNDbContext _context;
        private readonly IMapper _mapper;

        public UsersController(TruyenVNDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [EnableQuery(PageSize = 10)]
        
        public IActionResult Get()
        {
            return Ok(_context.Users);
        }

        [EnableQuery]
        public IActionResult Get(int key)
        {
            var user = _context.Users.Find(key);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            return Ok(user);
        }

        [EnableQuery]
        public IActionResult Post([FromBody]UserDTO userDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userDTO);
                _context.Users.Add(user);
                _context.SaveChanges();

                return Created(user);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [EnableQuery]
        public IActionResult Put(int key,[FromBody]UserDTO userDTO)
        {
            try
            {
                var user = _context.Users.Find(key);
                if (user == null)
                {
                    return BadRequest("Not Find user");
                }
                _mapper.Map(userDTO, user);
                _context.Users.Update(user);
                _context.SaveChanges();

                return Updated(user);
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
                var user = _context.Users.Find(key);
                if (user == null)
                {
                    return BadRequest("Not Find user");
                }
                _context.Users.Remove(user);
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
