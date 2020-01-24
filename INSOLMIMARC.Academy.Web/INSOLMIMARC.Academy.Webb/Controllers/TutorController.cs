using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INSOLMIMARC.Academy.Webb.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using IdentityModel.Client;

namespace INSOLMIMARC.Academy.Webb.Controllers
{
    public class TutorController : Controller
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
            BaseAddress = new Uri("http://localhost:50300/api/tutors/")
        };

        public async Task<IActionResult> Index()
        {
            IEnumerable<Tutor> tutors = null;
            var accessToken = await GetToken();
            client.SetBearerToken(accessToken);

                 MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                //Called Member default GET All records
                //GetAsync to send a GET request 
                var responseTask = client.GetAsync("/api/tutors/");
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
           /* }*/
            return View(tutors);
        }

        public async Task<ActionResult> Details([FromRoute] int? id)
        {
            var accessToken = await GetToken();
            client.SetBearerToken(accessToken);

            Tutor tutor = null;
            if (id == null)
            {
                return NotFound();
            }

 
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

            return View(tutor);

        }

        // GET: tutors/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Tutor tutor = null;
            var accessToken = await GetToken();
            client.SetBearerToken(accessToken);


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
           

            if (tutor == null)
            {
                return NotFound();
            }
            return View(tutor);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Tutor tutor)
        {
            var accessToken = await GetToken();
            client.SetBearerToken(accessToken);

            string stringData = JsonConvert.SerializeObject(tutor);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync("/api/tutors/" + tutor.ID, contentData).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return View(tutor);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Tutor tutor = null;
            var accessToken = await GetToken();
            client.SetBearerToken(accessToken);


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
            

            if (tutor == null)
            {
                return NotFound();
            }
            return View(tutor);
        }


        [HttpPost]
        public async Task<ActionResult> Delete(Tutor tutor)
        {
            var accessToken = await GetToken();
            client.SetBearerToken(accessToken);

            if (tutor.ID.ToString() == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = client.DeleteAsync("/api/tutors/" + tutor.ID).Result;
            TempData["Message"] = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Tutor tutor)
        {
            var accessToken = await GetToken();
            client.SetBearerToken(accessToken);

            string stringData = JsonConvert.SerializeObject(tutor);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("/api/tutors/", contentData).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");
        }

    }
}
