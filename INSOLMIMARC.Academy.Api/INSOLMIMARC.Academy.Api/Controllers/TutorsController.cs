using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using INSOLMIMARC.Academy.Api.Data;
using INSOLMIMARC.Academy.Api.Models;
using Microsoft.AspNetCore.Authorization;

namespace INSOLMIMARC.Academy.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Tutors")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TutorsController : Controller
    {
        private readonly SchoolContext _context;

        public TutorsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/Tutors
        [HttpGet]
        public IEnumerable<Tutor> GetTutors()
        {
            return _context.Tutors;
        }

        // GET: api/Tutors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTutor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tutor = await _context.Tutors.SingleOrDefaultAsync(m => m.ID == id);

            if (tutor == null)
            {
                return NotFound();
            }

            return Ok(tutor);
        }

        // PUT: api/Tutors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTutor([FromRoute] int id, [FromBody] Tutor tutor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tutor.ID)
            {
                return BadRequest();
            }

            _context.Entry(tutor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TutorExists(id))
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

        // POST: api/Tutors
        [HttpPost]
        public async Task<IActionResult> PostTutor([FromBody] Tutor tutor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Tutors.Add(tutor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTutor", new { id = tutor.ID }, tutor);
        }

        // DELETE: api/Tutors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTutor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tutor = await _context.Tutors.SingleOrDefaultAsync(m => m.ID == id);
            if (tutor == null)
            {
                return NotFound();
            }

            _context.Tutors.Remove(tutor);
            await _context.SaveChangesAsync();

            return Ok(tutor);
        }

        private bool TutorExists(int id)
        {
            return _context.Tutors.Any(e => e.ID == id);
        }
    }
}