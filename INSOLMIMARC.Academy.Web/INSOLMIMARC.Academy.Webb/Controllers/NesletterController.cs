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
using Newtonsoft.Json;

namespace INSOLMIMARC.Academy.Webb.Controllers
{
    public class NesletterController : Controller
    {
        public HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:50300/api/tutors/")
        };

        private async Task<string> GetToken()
        {
            const string TokenEndpoint = "http://localhost:52277/connect/token";
            var client = new TokenClient(TokenEndpoint, "INSOLMIMARC", "INSOLMIMARC");
            var response = await client.RequestClientCredentialsAsync("APIPublic");

            return response.AccessToken;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Newsletter newsletter)
        {
            var accessToken = await GetToken();
            client.SetBearerToken(accessToken);

            string stringData = JsonConvert.SerializeObject(newsletter);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("/api/tutors/", contentData).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");
        }
    }
}