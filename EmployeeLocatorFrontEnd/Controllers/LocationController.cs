using Domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace EmployeeLocatorFrontEnd.Controllers
{
    public class LocationController : BaseController
    {
        private readonly IConfiguration _config;

        public LocationController(IConfiguration config) : base(config)
        {
            _config = config;
        }
        // GET: LocationController
        public async Task<ActionResult> Index()
        {
            List<Location> locations = new List<Location>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseAPIUrl}/Location"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    locations = JsonConvert.DeserializeObject<List<Location>>(apiResponse);
                }
            }
            return View(locations);
        }

        // GET: LocationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LocationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var location = FormToLocation(collection);
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(location), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync($"{BaseAPIUrl}/Location/", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LocationController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseAPIUrl}/Location/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var location = JsonConvert.DeserializeObject<Location>(apiResponse);
                    return View(location);
                }
            }
        }

        // POST: LocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var location = FormToLocation(collection);
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(location),Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync($"{BaseAPIUrl}/Location/{id}",content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LocationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LocationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync($"{BaseAPIUrl}/Location/{id}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public Location FormToLocation(IFormCollection form)
        {
            var location = new Location();

            // Map form fields to properties in the Person object
            location.City = form["City"];
            location.State = form["State"];
            location.Id = Convert.ToInt32(form["Id"]);

            return location;
        }
    }
}
