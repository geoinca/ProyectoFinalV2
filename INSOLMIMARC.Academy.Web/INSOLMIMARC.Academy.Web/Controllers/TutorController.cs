using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

using INSOLMIMARC.Academy.Web.Models;
namespace INSOLMIMARC.Academy.Web.Controllers
{
    public class TutorController : Controller
    {
        private readonly INSOLMIMARCAcademyWebContext _context;

        public TutorController(INSOLMIMARCAcademyWebContext context)
        {
            _context = context;
        }

        // GET: Tutor
        public IActionResult Index()
        {
            IEnumerable<Tutor> tutors = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50300/api/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                //Called Member default GET All records
                //GetAsync to send a GET request 
                var responseTask = client.GetAsync("tutors");
                responseTask.Wait();

                //To store result of web api response. 
                var result = responseTask.Result;

                //If success received 
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Tutor>>();
                    readTask.Wait();

                    tutors = readTask.Result;
                }
                else
                {
                    //Error response received 
                    tutors = Enumerable.Empty<Tutor>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(tutors);
        }

        // GET: Tutor/Details/5
        public ActionResult Details([FromRoute]int? id)
        {
            Tutor   tutor = null;

            if (id == null)
            {
                return NotFound();
            }
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:50300/api/");

                //Called Member default GET All records
                //GetAsync to send a GET request 
                var responseTask = client.GetAsync("/api/tutors/" + id.ToString());
                responseTask.Wait();

                //To store result of web api response. 
                var result = responseTask.Result;

                //If success received 
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Tutor>();
                    readTask.Wait();

                    tutor = readTask.Result;
                }
                else
                {
                    //Error response received 

                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            return View(tutor);
        }

        // GET: Tutor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tutor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstMidName,Address,Email,Type,Phone,EnrollmentDate")] Tutor tutor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tutor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tutor);
        }

        // GET: Tutor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutor = await _context.Tutor.SingleOrDefaultAsync(m => m.ID == id);
            if (tutor == null)
            {
                return NotFound();
            }
            return View(tutor);
        }

        // POST: Tutor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstMidName,Address,Email,Type,Phone,EnrollmentDate")] Tutor tutor)
        {
            if (id != tutor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tutor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TutorExists(tutor.ID))
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
            return View(tutor);
        }

        // GET: Tutor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutor = await _context.Tutor
                .SingleOrDefaultAsync(m => m.ID == id);
            if (tutor == null)
            {
                return NotFound();
            }

            return View(tutor);
        }

        // POST: Tutor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tutor = await _context.Tutor.SingleOrDefaultAsync(m => m.ID == id);
            _context.Tutor.Remove(tutor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TutorExists(int id)
        {
            return _context.Tutor.Any(e => e.ID == id);
        }
    }
}
