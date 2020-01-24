using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using INSOLMIMARC.Academy.Web.Models;

namespace INSOLMIMARC.Academy.Web.Controllers
{
    public class TeacherController : Controller
    {
 
        public IActionResult Index()
        {
            IEnumerable<Teacher> teachers = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50300/api/teachers");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                //Called Member default GET All records
                //GetAsync to send a GET request 
                var responseTask = client.GetAsync("teachers");
                responseTask.Wait();

                //To store result of web api response. 
                var result = responseTask.Result;

                //If success received 
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Teacher>>();
                    readTask.Wait();

                    teachers = readTask.Result;
                }
                else
                {
                    //Error response received 
                    teachers = Enumerable.Empty<Teacher>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(teachers);
        }

        public ActionResult Details([FromRoute] int? id)
        {

            Teacher teacher = null;
            if (id == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                 
                client.BaseAddress = new Uri("http://localhost:50300/api/teachers/");

                //Called Member default GET All records
                //GetAsync to send a GET request 
                var responseTask =   client.GetAsync("/api/teachers/" + id.ToString());
                responseTask.Wait();

                //To store result of web api response. 
                var result = responseTask.Result;

                //If success received 
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Teacher>();
                    readTask.Wait();

                    teacher = readTask.Result;
                }
                else
                {
                    //Error response received 
                   
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(teacher);

        }

        // GET: Teachers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Teacher teacher = null;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:50300/api/teachers/");

                //Called Member default GET All records
                //GetAsync to send a GET request 
                var responseTask = client.GetAsync("/api/teachers/" + id.ToString());
                responseTask.Wait();

                //To store result of web api response. 
                var result = responseTask.Result;

                //If success received 
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Teacher>();
                    readTask.Wait();

                    teacher = readTask.Result;
                }
                else
                {
                    //Error response received 

                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        [HttpPost]
        public ActionResult Edit(Teacher teacher)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:50300/api/teachers/");
            string stringData = JsonConvert.SerializeObject(teacher);
            var contentData = new StringContent(stringData,System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync("/api/teachers/" + teacher.ID,contentData).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return View(teacher);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Teacher teacher = null;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:50300/api/teachers/");

                //Called Member default GET All records
                //GetAsync to send a GET request 
                var responseTask = client.GetAsync("/api/teachers/" + id.ToString());
                responseTask.Wait();

                //To store result of web api response. 
                var result = responseTask.Result;

                //If success received 
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Teacher>();
                    readTask.Wait();

                    teacher = readTask.Result;
                }
                else
                {
                    //Error response received 

                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }
        public ActionResult DeleteX(int? id)
        {
            var client = new HttpClient();
            

            client.BaseAddress = new Uri("http://localhost:50300/api/teachers/");
            if (id == null)
            {
                return NotFound();
            }
            HttpResponseMessage response =        client.GetAsync("/api/customerservice/" + id).Result;
            string stringData = response.Content.ReadAsStringAsync().Result;
            Teacher data = JsonConvert.DeserializeObject<Teacher>(stringData);
            return View(data);
        }

        [HttpPost]
        public ActionResult Delete(Teacher teacher)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:50300/api/teachers/");
            if (teacher.ID.ToString() == null)
            {
                return NotFound();
            }

            HttpResponseMessage response =        client.DeleteAsync("/api/teachers/" + teacher.ID).Result;
            TempData["Message"] =        response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View( );
        }
        [HttpPost]
        public ActionResult Create(TeacherCreate teacher)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:50300/api/teachers/");
            string stringData = JsonConvert.SerializeObject(teacher);
            var contentData = new StringContent         (stringData, System.Text.Encoding.UTF8,     "application/json");
            HttpResponseMessage response = client.PostAsync         ("/api/teachers/", contentData).Result;
            ViewBag.Message = response.Content.        ReadAsStringAsync().Result;
            return RedirectToAction("Index");
        }

    }

}