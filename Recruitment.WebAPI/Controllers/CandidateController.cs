using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Recruitment.Core;
using Recruitment.WebAPI.Data;
using Recruitment.WebAPI.ViewModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly RecruitmentDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public CandidateController(RecruitmentDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: api/Candidate
        [HttpGet]
        public ActionResult<IEnumerable<CandidateVM>> GetCandidate()
        {
            var listCandi = _context.Candidate
                .Select(x => new CandidateVM
                {
                    CandidateId = x.CandidateId,
                    LevelId = x.LevelId,
                    LevelName = x.Level.LevelName,
                    PositionId = x.PositionId,
                    PositionName = x.Position.PositionName,
                    FullName = x.FullName,
                    Birthday = x.Birthday,
                    Address = x.Address,
                    Phone = x.Phone,
                    Email = x.Email,
                    IntroduceName = x.IntroduceName,
                })
                .ToList();
            return listCandi;
        }

        [HttpGet("level")]
        public ActionResult<IEnumerable<Level>> GetLevel()
        {
            return _context.Level.ToList();
        }

        [HttpGet("position")]
        public ActionResult<IEnumerable<Position>> GetPosition()
        {
            return _context.Position.ToList();
        }

        // GET: api/Candidate/5
        [HttpGet("{id}")]
        public ActionResult<CandidateVM> GetCandidate(int id)
        {
            var listCandi = _context.Candidate
                .Select(x => new CandidateVM
                {
                    CandidateId = x.CandidateId,
                    LevelId = x.LevelId,
                    LevelName = x.Level.LevelName,
                    PositionId = x.PositionId,
                    PositionName = x.Position.PositionName,
                    FullName = x.FullName,
                    Birthday = x.Birthday,
                    Address = x.Address,
                    Phone = x.Phone,
                    Email = x.Email,
                    IntroduceName = x.IntroduceName == null ? "" : x.IntroduceName,
                    //CV = x.CV.ToString()
                })
                .ToList();
            var candidate = listCandi.Find(x => x.CandidateId == id);

            if (candidate == null)
            {
                return NotFound();
            }

            return candidate;
        }

        // PUT: api/Candidate/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidate(int id, Candidate candidate)
        {
            if (id != candidate.CandidateId)
            {
                return BadRequest();
            }

            _context.Entry(candidate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Candidate
        [HttpPost]
        public async Task<ActionResult<Candidate>> PostCandidate(Candidate candidate/*, IFormFile file*/)
        {
            _context.Candidate.Add(candidate);

            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetCandidate", new { id = candidate.CandidateId }, candidate);
            return CreatedAtAction(nameof(GetCandidate), new { id = candidate.CandidateId }, candidate);
        }

        // DELETE: api/Candidate/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            var candidate = await _context.Candidate.FindAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }

            _context.Candidate.Remove(candidate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CandidateExists(int id)
        {
            return _context.Candidate.Any(e => e.CandidateId == id);
        }
    }
}
