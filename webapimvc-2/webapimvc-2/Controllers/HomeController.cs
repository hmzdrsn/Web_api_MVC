using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using webapi_1.Models;
using webapimvc_2.Models;



namespace webapimvc_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task <IActionResult> GetAll()
        {
            string baseUrl = Request.GetEncodedUrl();
            Console.WriteLine(baseUrl);
            DataTable dt = new DataTable();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage getData = await client.GetAsync("api/Item/GetAll");
                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    dt = JsonConvert.DeserializeObject<DataTable>(results);
                }
                else
                {
                    Console.WriteLine("Hata");
                }
                ViewData.Model = dt;
            }

            return View();
        }
        [HttpPost]
        public async Task <ActionResult> GetAll(int id)
        {
            string baseUrl = Request.GetEncodedUrl();
            Console.WriteLine(baseUrl);
            DataTable dt = new DataTable();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage getData = await client.GetAsync("api/Item/GetById/" + id);
                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    
                    dt = JsonConvert.DeserializeObject<DataTable>(results);
                    
                }
                else
                {
                    Console.WriteLine("Hata");
                }
                ViewData.Model = dt;
            }

            return View("GetById",dt);
        }


        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public async Task <ActionResult> Insert(item model)
        {
            string baseUrl = Request.GetEncodedUrl();
            using (var client = new HttpClient())
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7102/api/Item/Insert?id=" + model.id + "&carName=" + model.carName + "&description=" + model.description); 
                client.SendAsync(requestMessage).GetAwaiter().GetResult();        
            }
            return RedirectToAction("GetAll");
        }
        [HttpPost]
        public async Task<ActionResult> Delete(int did)
        {
            string baseUrl = Request.GetEncodedUrl();
            using (var client = new HttpClient())
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7102/api/Item/Delete/"+did);
                client.SendAsync(requestMessage).GetAwaiter().GetResult();
            }
            return RedirectToAction("GetAll");
        }
        public ActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Update(item model)
        {
            string baseUrl = Request.GetEncodedUrl();
            using (var client = new HttpClient())
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7102/api/Item/Update?id=" + model.id + "&carName=" + model.carName + "&description=" + model.description);
                client.SendAsync(requestMessage).GetAwaiter().GetResult();
            }
            return RedirectToAction("GetAll");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}