﻿using System;
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
    [Route("api/Teachers")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TeachersController : Controller
    {
        private readonly SchoolContext _context;

        public TeachersController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/Teachers
        [HttpGet]
        public IEnumerable<Teacher> GetTeachers()
        {
            return _context.Teachers.ToList().Select(t => new Teacher
            {
                ID = t.ID,
                LastName = t.LastName,
                Job=t.Job,
                FirstMidName = t.FirstMidName,
                Email = t.Email
            }).AsQueryable();
        }

        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacher([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teacher = await _context.Teachers.SingleOrDefaultAsync(m => m.ID == id);

            if (teacher == null)
            {
                return NotFound();
            }
            else
            {
                teacher.Dni = null;
            }
                
            return Ok(teacher);
        }

        // PUT: api/Teachers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher([FromRoute] int id, [FromBody] Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teacher.ID)
            {
                return BadRequest();
            }

            _context.Entry(teacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
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

        // POST: api/Teachers
        [HttpPost]
        public async Task<IActionResult> PostTeacher([FromBody] Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeacher", new { id = teacher.ID }, teacher);
        }

        // DELETE: api/Teachers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teacher = await _context.Teachers.SingleOrDefaultAsync(m => m.ID == id);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return Ok(teacher);
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.ID == id);
        }
    }
}