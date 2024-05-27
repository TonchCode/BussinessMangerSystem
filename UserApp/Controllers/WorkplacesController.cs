using DataTransferObjects.DTOs;
using DataTransferObjects.DTOs.JoinDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using X.PagedList;

namespace UserApp.Controllers
{
    public class WorkplacesController : Controller
    {
        // GET: WorkplacesController
        public IActionResult Index(int? page, string? searchString, string? searchby)
        {
            string api = "https://localhost:7245/api/Workplaces";
            string apiResult = "";

            using (ApiClient wc = new())
            {
                apiResult = wc.GetStringAsync(api).Result;
            }

            IEnumerable<Workplace> model = JsonConvert.DeserializeObject<List<Workplace>>(apiResult);

            Workplace temporalCheck = new Workplace();
            var properties = temporalCheck.GetType().GetProperties();
            List<string> data = new List<string>();
            foreach (var item in properties)
            {
                data.Add(item.Name);
            }
            IEnumerable<SelectListItem> selectWorkItems = new SelectList(data);
            data.Remove("Id");
            ViewData["datalist"] = selectWorkItems;

            if (!String.IsNullOrEmpty(searchString))
            {
                switch (searchby)
                {
                    case "Name":
                        model = model.Where(p => p.Name.ToLower().Contains(searchString.ToLower())).ToPagedList(page ?? 1, 10);
                        break;
                    case "Holder":
                        model = model.Where(p => p.Holder.ToLower().Contains(searchString.ToLower())).ToPagedList(page ?? 1, 10);
                        break;
                    case "DateCreated":
                        model = model.Where(p => p.DateCreated.Year.Equals(searchString) || p.DateCreated.Month.Equals(searchString)).ToPagedList(page ?? 1, 10);
                        break;
                    case "City":
                        model = model.Where(p => p.City.ToString().ToLower().Contains(searchString.ToLower())).ToPagedList(page ?? 1, 10);
                        break;
                    case "RatingWithStars":
                        model = model.Where(p => p.RatingWithStars.ToString().ToLower().ToLower().Contains(searchString.ToLower().ToLower())).ToPagedList(page ?? 1, 10);
                        break;
                    default:
                        break;
                }
            }

            return View(model.ToPagedList(page ?? 1, 6));
        }

        // GET: WorkplacesController/Details/5
        [HttpGet]
        public IActionResult Details(int id, int? page)
        {
            string url = "https://localhost:7245/api/Workplaces/" + id;
            var json = "";

            using (ApiClient client = new())
            {
                json = client.GetStringAsync(url).Result;
            }
            Workplace j = JsonConvert.DeserializeObject<Workplace>(json);

            string joinUrl = "https://localhost:7245/api/WorkJobs";
            using (ApiClient client = new())
            {
                json = client.GetStringAsync(joinUrl).Result;
            }
            List<WorkJobs> workJobs = JsonConvert.DeserializeObject<List<WorkJobs>>(json);

            List<Job> jobs = new List<Job>();
            foreach(WorkJobs wj in workJobs)
            {
                if (wj.WorkId != id) continue;
                string selectQuery = "https://localhost:7245/api/Jobs/"+wj.JobId;
                using (ApiClient client = new())
                {
                    json = client.GetStringAsync(selectQuery).Result;
                }
                jobs.Add(JsonConvert.DeserializeObject<Job>(json));
            }

            ViewData["List<Jobs>"] = jobs.ToPagedList(page ?? 1, 6);

            return View(j);
        }

        // GET: WorkplacesController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkplacesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Workplace model)
        {
            try
            {
                string api = "https://localhost:7245/api/Workplaces";
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
        public IActionResult AddAJobToWorkplace(int workId, int? page)
        {
            ViewData["workId"] = workId;
            Console.WriteLine(workId);
            string api = "https://localhost:7245/api/Jobs";
            string apiResult = "";

            using (ApiClient wc = new())
            {
                apiResult = wc.GetStringAsync(api).Result;
            }

            IEnumerable<Job> model = JsonConvert.DeserializeObject<List<Job>>(apiResult).ToPagedList(page ?? 1, 6);

            return View(model);
        }

        // GET: WorkplacesController/Edit/5
        public IActionResult Edit(int id, int? page = 1)
        {
            string url = "https://localhost:7245/api/Workplaces/" + id;
            var json = "";

            using (ApiClient client = new())
            {
                json = client.GetStringAsync(url).Result;
            }
            Workplace j = JsonConvert.DeserializeObject<Workplace>(json);

            string joinUrl = "https://localhost:7245/api/WorkJobs";
            using (ApiClient client = new())
            {
                json = client.GetStringAsync(joinUrl).Result;
            }
            List<WorkJobs> workJobs = JsonConvert.DeserializeObject<List<WorkJobs>>(json);

            List<Job> jobs = new List<Job>();
            foreach (WorkJobs wj in workJobs)
            {
                if (wj.WorkId != id) continue;
                string selectQuery = "https://localhost:7245/api/Jobs/" + wj.JobId;
                using (ApiClient client = new())
                {
                    json = client.GetStringAsync(selectQuery).Result;
                }
                jobs.Add(JsonConvert.DeserializeObject<Job>(json));
            }

            ViewData["List<Jobs>"] = jobs.ToPagedList(page??1,6);

            return View(j);
        }

        public IActionResult Edit(int id, int jobId, int workId)
        {
            if(jobId == 0) return Edit(id);

            var json = "";
            string joinUrl = "https://localhost:7245/api/WorkJobs";
            using (ApiClient client = new())
            {
                json = client.GetStringAsync(joinUrl).Result;
            }
            List<WorkJobs> workJobs = JsonConvert.DeserializeObject<List<WorkJobs>>(json);
           
            if(workJobs.Where(item => item.WorkId == workId && item.JobId == jobId).ToList().FirstOrDefault() != null) 
                return Edit(id);

            WorkJobs work = new WorkJobs();

            work.WorkId = workId;
            work.JobId = jobId;

            json = JsonConvert.SerializeObject(work);
            StringContent content = new(json, Encoding.UTF8, "application/json");

            using (ApiClient client = new())
            {
                var resp = client.PostAsync(joinUrl, content).Result;
                Console.WriteLine(resp);
            }

            return Edit(id);
        }

        [HttpGet]
        public IActionResult Edit(int id, int jobId, int workId, int rem, int? page)
        {
            if (jobId == 0) return Edit(id, page);
            if (rem != 1) return Edit(id, jobId, workId);
            try
            {
                var json = "";
                string joinUrl = "https://localhost:7245/api/WorkJobs";
                using (ApiClient client = new())
                {
                    json = client.GetStringAsync(joinUrl).Result;
                }
                List<WorkJobs> workJobs = JsonConvert.DeserializeObject<List<WorkJobs>>(json);

                foreach (WorkJobs wj in workJobs)
                {
                    if (wj.WorkId != workId || wj.JobId != jobId)
                        continue;

                    string selectQuery = "https://localhost:7245/api/WorkJobs/" + wj.Id;
                    using (ApiClient client = new())
                    {
                        var json2 = client.DeleteAsync(selectQuery).Result;
                        Console.WriteLine(json2);
                    }
                }
            }
            catch (Exception ex)
            {
                return Edit(id);
            }

            return Edit(id);
        }

        // POST: WorkplacesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Workplace model)
        {
            try
            {
                string api = "https://localhost:7245/api/Workplaces/" + model.Id;
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

        // POST: WorkplacesController/Delete/5
        public IActionResult Delete(int id)
        {
            string url = "https://localhost:7245/api/Workplaces/" + id;

            using (var client = new ApiClient())
            {
                HttpResponseMessage resp = client.DeleteAsync(url).Result;
                Console.WriteLine(resp);
            }

            return RedirectToAction("Index");
        }
    }
}
