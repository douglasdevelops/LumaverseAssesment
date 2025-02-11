using Domain;
using EmployeeLocatorFrontEnd.Views.ViewModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using ValidateAntiForgeryTokenAttribute = System.Web.Mvc.ValidateAntiForgeryTokenAttribute;
using System.Text;
using System.Data.Entity;

namespace EmployeeLocatorFrontEnd.Controllers
{
    public class EngineerController : BaseController
    {
        private readonly IConfiguration _config;

        public EngineerController(IConfiguration config) : base(config)
        {
            _config = config;
        }

        // GET: Engineer
        public async Task<ActionResult> Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.StatusSortParm = sortOrder == "Status" ? "status_desc" : "Status";

            List<Engineer> engineers = new List<Engineer>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseAPIUrl}/Engineer"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    engineers = JsonConvert.DeserializeObject<List<Engineer>>(apiResponse);

                    switch (sortOrder)
                    {
                        case "name_desc":
                            engineers = engineers.OrderByDescending(e => e.LastName).ToList();
                            break;
                        case "Date":
                            engineers = engineers.OrderBy(e => e.HireDate).ToList();
                            break;
                        case "date_desc":
                            engineers = engineers.OrderByDescending(e => e.HireDate).ToList();
                            break;
                        case "status_desc":
                            engineers = engineers.OrderByDescending(e => e.EmployeeStatus).ToList();
                            break;
                        case "Status":
                            engineers = engineers.OrderBy(e => e.EmployeeStatus).ToList();
                            break;
                        default:
                            engineers = engineers.OrderBy(e => e.LastName).ToList();
                            break;
                    }

                    foreach (var item in engineers)
                    {
                        using (var locationResp = await httpClient.GetAsync($"{BaseAPIUrl}/Location/{item.LastKnownLocationId}"))
                        {
                            apiResponse = await locationResp.Content.ReadAsStringAsync();
                            var locationForEngineer = JsonConvert.DeserializeObject<Location>(apiResponse);

                            item.LastKnownLocation = locationForEngineer;
                        }
                    }
                    
                    if (!String.IsNullOrEmpty(searchString))
                    {
                        engineers = engineers.Where(s => s.LastName.ToLower().Contains(searchString.ToLower()) || s.FirstName.ToLower().Contains(searchString.ToLower()) || s.LastKnownLocation.City.ToLower().Contains(searchString.ToLower())).ToList();
                    }
                    return View(engineers.ToList());

                }
            }
        }

        // GET: Engineer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Engineer/Create
        public async Task<ActionResult> Create()
        {
            EngineerViewModel vm = new EngineerViewModel();
            List<Location> locations = new List<Location>();

            using (var httpClient = new HttpClient())
            {
                using (var locationResponse = await httpClient.GetAsync($"{BaseAPIUrl}/Location"))
                {
                    var apiResponse = await locationResponse.Content.ReadAsStringAsync();
                    locations = JsonConvert.DeserializeObject<List<Location>>(apiResponse);

                    vm.Locations.Options = locations.Select(v => new SelectListItem { Text = v.City, Value = v.Id.ToString() }).ToList();
                    vm.EmployeeStatus.Options = new List<SelectListItem>
                        {
                            new SelectListItem("Working","1"),
                            new SelectListItem("Sick","2"),
                            new SelectListItem("Vacation","3"),
                            new SelectListItem("Other","4"),
                        };

                    vm.EmployeeStatus.SelectedValue = "1";
                    vm.Locations.SelectedValue = "1";

                    return View(vm);

                }
            }
        }

        // POST: Engineer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var engineer = await FormToEngineer(collection);
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(engineer), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync($"{BaseAPIUrl}/Engineer/", content))
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

        // GET: Engineer/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseAPIUrl}/Engineer/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var engineer = JsonConvert.DeserializeObject<Engineer>(apiResponse);

                    EngineerViewModel vm = new EngineerViewModel();

                    vm.Engineer = engineer;

                    List<Location> locations = new List<Location>();

                    using (var locationResponse = await httpClient.GetAsync($"{BaseAPIUrl}/Location"))
                    {
                        apiResponse = await locationResponse.Content.ReadAsStringAsync();
                        locations = JsonConvert.DeserializeObject<List<Location>>(apiResponse);

                        vm.Locations.Options = locations.Select(v => new SelectListItem { Text = v.City, Value = v.Id.ToString() }).ToList();
                        vm.EmployeeStatus.Options = new List<SelectListItem>
                        {
                            new SelectListItem("Working","1"),
                            new SelectListItem("Sick","2"),
                            new SelectListItem("Vacation","3"),
                            new SelectListItem("Other","4"),
                        };

                        vm.EmployeeStatus.SelectedValue = ((int)vm.Engineer.EmployeeStatus).ToString();

                        using (var individualLocationResponse = await httpClient.GetAsync($"{BaseAPIUrl}/Location/{id}"))
                        {
                            apiResponse = await individualLocationResponse.Content.ReadAsStringAsync();
                            var location = JsonConvert.DeserializeObject<Location>(apiResponse);
                            vm.Locations.SelectedValue = location.Id.ToString();
                            return View(vm);
                        }
                    }
                }
            }
        }

        // POST: Engineer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var engineer = await FormToEngineer(collection);
                    string content1 = JsonConvert.SerializeObject(engineer);
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(engineer), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync($"{BaseAPIUrl}/Engineer/{id}", content))
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

        // GET: Engineer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Engineer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                List<Engineer> engineers = new List<Engineer>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync($"{BaseAPIUrl}/Engineer/{id}"))
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

        public async Task<Engineer> FormToEngineer(IFormCollection form)
        {
            var engineer = new Engineer();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseAPIUrl}/Location/{form["Locations.SelectedValue"]}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var Location = JsonConvert.DeserializeObject<Location>(apiResponse);

                    engineer.LastName = form["Engineer.LastName"];
                    engineer.FirstName = form["Engineer.FirstName"];
                    engineer.PhoneNumber = "8025551515";
                    engineer.HireDate = Convert.ToDateTime(form["Engineer.HireDate"]);
                    //engineer.LastKnownLocation = Location;
                    engineer.LastKnownLocationId = Location.Id;
                    engineer.Id = Convert.ToInt32(form["Engineer.id"]);

                    switch (form["EmployeeStatus.SelectedValue"])
                    {
                        case "1":
                            engineer.EmployeeStatus = EmployeeStatus.Working;
                            break;
                        case "2":
                            engineer.EmployeeStatus = EmployeeStatus.Sick;
                            break;
                        case "3":
                            engineer.EmployeeStatus = EmployeeStatus.Vacation;
                            break;
                        case "4":
                            engineer.EmployeeStatus = EmployeeStatus.Other;
                            break;
                        default:
                            break;
                    }

                    return engineer;

                }
            }
        }
    }
}
