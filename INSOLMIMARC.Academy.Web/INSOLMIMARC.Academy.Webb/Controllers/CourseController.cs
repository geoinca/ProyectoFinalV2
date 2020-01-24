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
using Microsoft.Extensions.Configuration;

 
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;

namespace INSOLMIMARC.Academy.Webb.Controllers
{
    public class CourseController : Controller
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
            BaseAddress = new Uri("http://localhost:50300/api/courses/"),
        };

        public async Task<IActionResult> Index()
        {
            IEnumerable<Course> courses = null;
            var accessToken =  await GetToken();
            client.SetBearerToken(accessToken);

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType); 
            var responseTask = client.GetAsync("/api/courses/");
            responseTask.Wait();

                //To store result of web api response. 
                var result = responseTask.Result;

                //If success received 
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Course>>();
                    readTask.Wait();
                    courses = readTask.Result;
                }
                else
                {
                    //Error response received 
                    courses = Enumerable.Empty<Course>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
           /* }*/
            return View(courses);
        }

        public async Task<ActionResult> Details([FromRoute] int? id)
        {
            var accessToken = await GetToken();
            client.SetBearerToken(accessToken);

            Course course = null;
            if (id == null)
            {
                return NotFound();
            }
 

                //Called Member default GET All records
                //GetAsync to send a GET request 
                var responseTask = client.GetAsync("/api/courses/" + id.ToString());
                responseTask.Wait();

                //To store result of web api response. 
                var result = responseTask.Result;

                //If success received 
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Course>();
                    readTask.Wait();

                    course = readTask.Result;
                }
                else
                {
                    //Error response received 

                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
           
            return View(course);

        }

        // GET: Teachers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //IEnumerable<Course> course = null;
              Course course = null;
            var accessToken = await GetToken();
            client.SetBearerToken(accessToken);

            //Called Member default GET All records
            //GetAsync to send a GET request 
            var responseTask = client.GetAsync("/api/courses/" + id.ToString());
                responseTask.Wait();

                //To store result of web api response. 
                var result = responseTask.Result;

                //If success received 
                if (result.IsSuccessStatusCode)
                {
                //var readTask = result.Content.ReadAsAsync<Course>();
                var readTask = result.Content.ReadAsAsync<Course>();
                readTask.Wait();

                    course = readTask.Result;
                }
                else
                {
                    //Error response received 

                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            

            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Course course)
        {
            var accessToken = await GetToken();
            client.SetBearerToken(accessToken);

            string stringData = JsonConvert.SerializeObject(course);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync("/api/courses/" + course.ID, contentData).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return View(course);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Course course = null;
            var accessToken = await GetToken();
            client.SetBearerToken(accessToken);

            //Called Member default GET All records
            //GetAsync to send a GET request 
            var responseTask = client.GetAsync("/api/courses/" + id.ToString());
                responseTask.Wait();

                //To store result of web api response. 
                var result = responseTask.Result;

                //If success received 
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Course>();
                    readTask.Wait();

                    course = readTask.Result;
                }
                else
                {
                    //Error response received 

                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            

            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
 

        [HttpPost]
        public async Task<ActionResult> Delete(Course course)
        {
            var accessToken = await GetToken();
            client.SetBearerToken(accessToken);
            if (course.ID.ToString() == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = client.DeleteAsync("/api/courses/" + course.ID).Result;
            TempData["Message"] = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Course course)
        {
            var accessToken = await GetToken();
            client.SetBearerToken(accessToken);
            string stringData = JsonConvert.SerializeObject(course);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("/api/courses/", contentData).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");
        }



    }
}
