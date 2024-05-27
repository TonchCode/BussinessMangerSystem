using DataTransferObjects.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using X.PagedList;

namespace UserApp.Controllers
{
    public class JobsController : Controller
    {
        // GET: JobsController
        public IActionResult Index(int? page, string? searchString, string? searchby)
        {
            string api = "https://localhost:7245/api/Jobs";
            string apiResult = "";

            using (ApiClient wc = new())
            {
                apiResult = wc.GetStringAsync(api).Result;
            }

            IEnumerable<Job> model = JsonConvert.DeserializeObject<List<Job>>(apiResult);

            Job temporalCheck = new Job();
            var properties = temporalCheck.GetType().GetProperties();
            List<string> data = new List<string>();
            foreach (var item in properties)
            {
                data.Add(item.Name);
            }
            IEnumerable<SelectListItem> selectWorkItems = new SelectList(data);
            data.Remove("Id");
            data.Remove("IsRemoteAvailable");
            ViewData["datalist"] = selectWorkItems;

            if (!String.IsNullOrEmpty(searchString))
            {
                switch (searchby)
                {
                    case "MinHours":
                        model = model.Where(p => p.MinHours.ToString().ToLower().Contains(searchString.ToLower())).ToPagedList(page ?? 1, 6);
                        break;
                    case "MinSalary":
                        model = model.Where(p => p.MinSalary.ToString().ToLower().Contains(searchString.ToLower())).ToPagedList(page ?? 1, 6);
                        break;
                    case "JobCreationDate":
                        model = model.Where(p => p.JobCreationDate.Year.ToString().Contains(searchString) || p.JobCreationDate.Month.ToString().Contains(searchString)).ToPagedList(page ?? 1, 6);
                        break;
                    case "Title":
                        model = model.Where(p => p.Title.ToString().ToLower().Contains(searchString.ToLower())).ToPagedList(page ?? 1, 6);
                        break;
                    default:
                        break;
                }
            }

            return View(model.ToPagedList(page??1,6));
        }

        // GET: JobsController/Details/5
        [HttpGet]
        public IActionResult Details(int id)
        {
            string url = "https://localhost:7245/api/Jobs/" + id;
            var json = "";

            using (ApiClient client = new())
            {
                json = client.GetStringAsync(url).Result;
            }
            Job j = JsonConvert.DeserializeObject<Job>(json);

            return View(j);
        }

        // GET: JobsController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Job model)
        {
            try
            {
                string api = "https://localhost:7245/api/Jobs";
                string apiResult = JsonConvert.SerializeObject(model);
                StringContent content = new(apiResult, Encoding.UTF8, "application/json");
                
                using (ApiClient client = new())
                {
                    var resp = client.PostAsync(api, content).Result;
                    Console.WriteLine(resp);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: JobsController/Edit/5
        public IActionResult Edit(int id)
        {
            string url = "https://localhost:7245/api/Jobs/" + id;
            var json = "";

            using (ApiClient client = new())
            {
                json = client.GetStringAsync(url).Result;
            }
            Job j = JsonConvert.DeserializeObject<Job>(json);

            return View(j);
        }

        // POST: JobsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Job model)
        {
            try
            {
                string api = "https://localhost:7245/api/Jobs/" + model.Id;
                string apiResult = JsonConvert.SerializeObject(model);
                StringContent content = new(apiResult, Encoding.UTF8, "application/json");

                using (ApiClient client = new())
                {
                    var resp = client.PutAsync(api, content).Result;
                    Console.WriteLine(resp);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: JobsController/Delete/5
        public IActionResult Delete(int id)
        {
            string url = "https://localhost:7245/api/Jobs/" + id;

            using (var client = new ApiClient())
            {
                HttpResponseMessage resp = client.DeleteAsync(url).Result;
                Console.WriteLine(resp);
            }

            return RedirectToAction("Index");
        }
    }
}
