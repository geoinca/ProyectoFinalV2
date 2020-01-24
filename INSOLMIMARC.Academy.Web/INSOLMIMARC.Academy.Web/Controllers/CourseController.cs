using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using INSOLMIMARC.Academy.Web.Models;
using System.Net.Http;
using IdentityModel.Client;

namespace INSOLMIMARC.Academy.Web.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {

            IEnumerable<Course> courses = null;

            ////////
     
            //////////
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50300/api/courses");

                //Called Member default GET All records
                //GetAsync to send a GET request 
                var responseTask = client.GetAsync("courses");
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
            }
            return View(courses);
        }


    }
}