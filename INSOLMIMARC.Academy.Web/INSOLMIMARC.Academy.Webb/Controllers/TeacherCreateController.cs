using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INSOLMIMARC.Academy.Webb.Models;
using System.Net.Http;
using IdentityModel.Client;

namespace INSOLMIMARC.Academy.Webb.Controllers
{
    public class TeacherCreateController : Controller
    {
        private readonly INSOLMIMARCAcademyWebbContext _context;

        public TeacherCreateController(INSOLMIMARCAcademyWebbContext context)
        {
            _context = context;
        }

        // GET: TeacherCreate
        public async Task<IActionResult> Index()
        {
            IEnumerable<Teacher> teachers = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:50300/api/courses/")
            };

            //return View(await _context.TeacherCreate.ToListAsync());
            var discoveryClient = await client.GetAsync("http://localhost:52277");
            //var tokenClient = new TokenClient(discoveryClient.TokenEndpoint, "INSOLMIMARC", "INSOLMIMARC");
            //var response = await tokenClient.RequestClientCredentialsAsync("APIPublic");

            if (discoveryClient.IsSuccessStatusCode)
            {
                teachers = Enumerable.Empty<Teacher>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return NotFound();

        }

        // GET: TeacherCreate/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherCreate = await _context.TeacherCreate
                .FirstOrDefaultAsync(m => m.ID == id);
            if (teacherCreate == null)
            {
                return NotFound();
            }

            return View(teacherCreate);
        }

        // GET: TeacherCreate/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TeacherCreate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstMidName,Dni,Address,Email,Job,Description,Image,Phone")] TeacherCreate teacherCreate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacherCreate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teacherCreate);
        }

        // GET: TeacherCreate/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherCreate = await _context.TeacherCreate.FindAsync(id);
            if (teacherCreate == null)
            {
                return NotFound();
            }
            return View(teacherCreate);
        }

        // POST: TeacherCreate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstMidName,Dni,Address,Email,Job,Description,Image,Phone")] TeacherCreate teacherCreate)
        {
            if (id != teacherCreate.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacherCreate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherCreateExists(teacherCreate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(teacherCreate);
        }

        // GET: TeacherCreate/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherCreate = await _context.TeacherCreate
                .FirstOrDefaultAsync(m => m.ID == id);
            if (teacherCreate == null)
            {
                return NotFound();
            }

            return View(teacherCreate);
        }

        // POST: TeacherCreate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacherCreate = await _context.TeacherCreate.FindAsync(id);
            _context.TeacherCreate.Remove(teacherCreate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherCreateExists(int id)
        {
            return _context.TeacherCreate.Any(e => e.ID == id);
        }
    }
}
