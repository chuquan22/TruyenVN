using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using TruyenVNAPI.DTO;
using TruyenVNAPI.Model;

namespace TruyenVNAPI.Controllers
{
    public class ReportsController : ODataController
    {
        private readonly TruyenVNDbContext _context;
        private readonly IMapper _mapper;

        public ReportsController(TruyenVNDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [EnableQuery(PageSize =10)]
        public IActionResult Get()
        {
            return Ok(_context.Reports);
        }

        [EnableQuery]
        public IActionResult Get(int key)
        {
            var report = _context.Reports.Find(key);
            if (report == null)
            {
                return BadRequest("Report not found");
            }
            return Ok(report);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ReportDTO reportDTO)
        {
            try
            {
                var report = _mapper.Map<Report>(reportDTO);
                _context.Reports.Add(report);
                _context.SaveChanges();

                return Created(report);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [EnableQuery]
        public IActionResult Put(int key, [FromBody] ReportDTO reportDTO)
        {
            try
            {
                var report = _context.Reports.Find(key);
                if (report == null)
                {
                    return BadRequest("Not Find Report");
                }
                _mapper.Map(report, reportDTO);
                _context.Reports.Update(report);
                _context.SaveChanges();

                return Updated(report);
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
                var report = _context.Reports.Find(key);
                if (report == null)
                {
                    return BadRequest("Not Find category");
                }
                _context.Reports.Remove(report);
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
