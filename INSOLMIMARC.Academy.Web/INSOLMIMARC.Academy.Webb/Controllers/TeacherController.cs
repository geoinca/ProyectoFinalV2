using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INSOLMIMARC.Academy.Webb.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using IdentityModel.Client;

namespace INSOLMIMARC.Academy.Webb.Controllers
{
    public class TeacherController : Controller
    {

        private async Task<string> GetToken()
        {
            const string TokenEndpoint = "http://localhost:52277/connect/token";
            var client = new TokenClient(TokenEndpoint, "INSOLMIMARC", "INSOLMIMARC");
            var response = await client.RequestClientCredentialsAsync("APIPublic");

            return response.AccessToken;
        }



        public HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:50300/api/teachers/"),
        };

        public async Task<IActionResult> Index()
        {
            IEnumerable<Teacher> teachers = null;
            var accessToken = await GetToken();
            client.SetBearerToken(accessToken);

            MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                //Called Member default GET All records
                //GetAsync to send a GET request 
                var responseTask = client.GetAsync("/api/teachers/");
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
             
            return View(teachers);
        }

        public async Task<ActionResult> Details([FromRoute] int? id)
        {

            Teacher teacher = null;
            var accessToken = await GetToken();
            client.SetBearerToken(accessToken);

            if (id == null)
            {
                return NotFound();
            }

  
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
           
            return View(teacher);

        }

        // GET: Teachers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            TeacherCreate teacher = null;
            var accessToken = await GetToken();
            client.SetBearerToken(accessToken);

                var responseTask = client.GetAsync("/api/teachers/" + id.ToString());
                responseTask.Wait();

                //To store result of web api response. 
                var result = responseTask.Result;

                //If success received 
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<TeacherCreate>();
                    readTask.Wait();

                    teacher = readTask.Result;
                }
                else
                {
                    //Error response received 

                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            

            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(TeacherCreate teacher)
        {
            var accessToken = await GetToken();
            client.SetBearerToken(accessToken);

            string stringData = JsonConvert.SerializeObject(teacher);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync("/api/teachers/" + teacher.ID, contentData).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return View(teacher);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            TeacherCreate teacher = null;
            var accessToken = await GetToken();
            client.SetBearerToken(accessToken);

                var responseTask = client.GetAsync("/api/teachers/" + id.ToString());
                responseTask.Wait();

                //To store result of web api response. 
                var result = responseTask.Result;

                //If success received 
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<TeacherCreate>();
                    readTask.Wait();

                    teacher = readTask.Result;
                }
                else
                {
                    //Error response received 

                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
           

            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }


        [HttpPost]
        public async Task<ActionResult> Delete(TeacherCreate teacher)
        {
            var accessToken = await GetToken();
            client.SetBearerToken(accessToken);

            if (teacher.ID.ToString() == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = client.DeleteAsync("/api/teachers/" + teacher.ID).Result;
            TempData["Message"] = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(TeacherCreate teacher)
        {
            var accessToken = await GetToken();
            client.SetBearerToken(accessToken);

            string stringData = JsonConvert.SerializeObject(teacher);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("/api/teachers/", contentData).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");
        }

    }
}
