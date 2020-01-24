using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using INSOLMIMARC.Academy.Webb.Models;
using System.Net.Http;
using IdentityModel.Client;
using Newtonsoft.Json;

namespace INSOLMIMARC.Academy.Webb.Controllers
{
    public class HomeController : Controller
    {
        public HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:50300/api/newsletter/")
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

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public async Task<ActionResult> Create(Newsletter newsletter)
        {
            var accessToken = await GetToken();
            client.SetBearerToken(accessToken);

            string stringData = JsonConvert.SerializeObject(newsletter);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("/api/newsletter/", contentData).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");
        }
    }
}
