using DataTransferObjects.DTOs;
using DataTransferObjects.DTOs.JoinDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Reflection;
using System.Text;
using X.PagedList;

namespace UserApp.Controllers
{
    public class PeopleController : Controller
    {
        // GET: PeopleController
        public IActionResult Index(int? page, string? searchString, string? searchby)
        {
            string url = "https://localhost:7245/api/People";
            string json = "";

            using (ApiClient wc = new())
            {
                json = wc.GetStringAsync(url).Result;
            }
            IEnumerable<Person> model = JsonConvert.DeserializeObject<List<Person>>(json);
            foreach (Person item in model)
            {
                string ur2 = "https://localhost:7245/api/Jobs/" + item.JobId;
                var json2 = "";

                using (ApiClient wc = new())
                {
                    json2 = wc.GetStringAsync(ur2).Result;
                }
                item.Job = JsonConvert.DeserializeObject<Job>(json2);

                ur2 = "https://localhost:7245/api/Workplaces/" + item.WorkId;
                json2 = "";

                using (ApiClient wc = new())
                {
                    json2 = wc.GetStringAsync(ur2).Result;
                }
                item.Work = JsonConvert.DeserializeObject<Workplace>(json2);
            }


            Person temporalCheck = new Person();
            var properties = temporalCheck.GetType().GetProperties();
            List<string> data = new List<string>();
            foreach (var item in properties)
            {
                data.Add(item.Name);
            }
            IEnumerable<SelectListItem> selectWorkItems = new SelectList(data);
            data.Remove("Id");
            data.Remove("JobId");
            data.Remove("WorkId");
            ViewData["datalist"] = selectWorkItems;

            if (!String.IsNullOrEmpty(searchString))
            {
                switch (searchby)
                {
                    case "FName":
                        model = model.Where(p => p.FName.ToLower().Contains(searchString.ToLower())).ToPagedList(page ?? 1, 6);
                        break;
                    case "LName":
                        model = model.Where(p => p.LName.ToLower().Contains(searchString.ToLower())).ToPagedList(page ?? 1, 6);
                        break;
                    case "BirthDate":
                        model = model.Where(p => p.BirthDate.Year.Equals(searchString) || p.BirthDate.Month.Equals(searchString)).ToPagedList(page ?? 1, 6);
                        break;
                    case "Age":
                        model = model.Where(p => p.Age.ToString().ToLower().Contains(searchString.ToLower())).ToPagedList(page ?? 1, 6);
                        break;
                    case "Email":
                        model = model.Where(p => p.Email.ToLower().Contains(searchString.ToLower())).ToPagedList(page ?? 1, 6);
                        break;
                    case "Sex":
                        model = model.Where(p => p.Sex.ToLower().Contains(searchString.ToLower())).ToPagedList(page ?? 1, 6);
                        break;
                    case "Work":
                        model = model.Where(p => p.Work.Name.ToLower().Contains(searchString.ToLower())).ToPagedList(page ?? 1, 6);
                        break;
                    case "Job":
                        model = model.Where(p => p.Job.Title.ToLower().Contains(searchString.ToLower())).ToPagedList(page ?? 1, 6);
                        break;
                    default:
                        break;
                }
            }

            return View(model.ToPagedList(page ?? 1, 6));
        }

        // GET: PeopleController/Details/5
        public IActionResult Details(int id)
        {
            string url = "https://localhost:7245/api/People/" + id;
            var json = "";

            using (ApiClient wc = new())
            {
                json = wc.GetStringAsync(url).Result;
            }

            Person myObject = JsonConvert.DeserializeObject<Person>(json);

            string ur2 = "https://localhost:7245/api/Jobs/" + myObject.JobId;
            var json2 = "";

            using (ApiClient wc = new())
            {
                json2 = wc.GetStringAsync(ur2).Result;
            }
            myObject.Job = JsonConvert.DeserializeObject<Job>(json2);

            string api = "https://localhost:7245/api/Workplaces/"+myObject.WorkId;
            string apiResult = "";

            using (ApiClient wc = new())
            {
                apiResult = wc.GetStringAsync(api).Result;
            }

            Workplace works = JsonConvert.DeserializeObject<Workplace>(apiResult);
            myObject.Work = works;

            return View(myObject);
        }

        [HttpGet]
        public IActionResult Create()
        {
            string api = "https://localhost:7245/api/Jobs";
            string apiResult = "";

            using (ApiClient wc = new())
            {
                apiResult = wc.GetStringAsync(api).Result;
            }

            IEnumerable<Job> jobs = JsonConvert.DeserializeObject<List<Job>>(apiResult);
            IEnumerable<SelectListItem> selectListItems = new SelectList(jobs, "Id", "Title");
            ViewData["allJobs"] = selectListItems;

            api = "https://localhost:7245/api/Workplaces";
            apiResult = "";

            using (ApiClient wc = new())
            {
                apiResult = wc.GetStringAsync(api).Result;
            }

            IEnumerable<Workplace> works = JsonConvert.DeserializeObject<List<Workplace>>(apiResult);
            IEnumerable<SelectListItem> selectWorkItems = new SelectList(works, "Id", "Name");
            ViewData["allWork"] = selectWorkItems;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Person model)
        {
            string json = "";
            string joinUrl = "https://localhost:7245/api/WorkJobs";
            using (ApiClient client = new())
            {
                json = client.GetStringAsync(joinUrl).Result;
            }
            List<WorkJobs> workJobs = JsonConvert.DeserializeObject<List<WorkJobs>>(json);
            if (workJobs.Where(wj => wj.WorkId == model.WorkId && wj.JobId == model.JobId).FirstOrDefault() == null)
            {
                TempData["message"] = "This workplace does not have that job position!";
                return Create();
                //return RedirectToAction("Workplaces", "Edit", new { id = model.WorkId}); 
            }

            string url = "https://localhost:7245/api/People";
            json = JsonConvert.SerializeObject(model);
            StringContent content = new(json, Encoding.UTF8, "application/json");

            Console.WriteLine(content);

            using (var client = new ApiClient())
            {
                HttpResponseMessage resp = client.PostAsync(url, content).Result;
                Console.WriteLine(resp);
            }

            return RedirectToAction("Index");
        }

        // GET: PeopleController/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            string url = "https://localhost:7245/api/People/" + id;
            var json = "";

            using (ApiClient wc = new())
            {
                json = wc.GetStringAsync(url).Result;
            }

            Person myObject = JsonConvert.DeserializeObject<Person>(json);

            string api = "https://localhost:7245/api/Jobs";
            string apiResult = "";

            using (ApiClient wc = new())
            {
                apiResult = wc.GetStringAsync(api).Result;
            }

            IEnumerable<Job> jobs = JsonConvert.DeserializeObject<List<Job>>(apiResult);
            IEnumerable<SelectListItem> selectListItems = new SelectList(jobs, "Id", "Title");
            selectListItems.Select(i => i.Value == myObject.JobId.ToString());
            ViewData["allJobs"] = selectListItems;

            api = "https://localhost:7245/api/Workplaces";
            apiResult = "";

            using (ApiClient wc = new())
            {
                apiResult = wc.GetStringAsync(api).Result;
            }

            IEnumerable<Workplace> works = JsonConvert.DeserializeObject<List<Workplace>>(apiResult);
            IEnumerable<SelectListItem> selectWorkItems = new SelectList(works, "Id", "Name");
            selectWorkItems.Select(i => i.Value == myObject.WorkId.ToString());
            ViewData["allWork"] = selectWorkItems;

            return View(myObject);
        }

        // POST: PeopleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Person model)
        {
            try
            {
                string json = "";
                string joinUrl = "https://localhost:7245/api/WorkJobs";
                using (ApiClient client = new())
                {
                    json = client.GetStringAsync(joinUrl).Result;
                }
                List<WorkJobs> workJobs = JsonConvert.DeserializeObject<List<WorkJobs>>(json);
                if (workJobs.Where(wj => wj.WorkId == model.WorkId && wj.JobId == model.JobId).FirstOrDefault() == null)
                {
                    TempData["message"] = "This workplace does not have that job position!";
                    return Edit(model.Id);
                    //return RedirectToAction("Workplaces", "Edit", new { id = model.WorkId}); 
                }


                string url = "https://localhost:7245/api/People/" + model.Id;
                json = JsonConvert.SerializeObject(model);
                StringContent content = new(json, Encoding.UTF8, "application/json");

                Console.WriteLine(json);

                using (var client = new ApiClient())
                {
                    HttpResponseMessage resp = client.PutAsync(url, content).Result;
                    Console.WriteLine(resp);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: PeopleController/Delete/5
        public IActionResult Delete(int id)
        {
            string url = "https://localhost:7245/api/People/" + id;

            using (var client = new ApiClient())
            {
                HttpResponseMessage resp = client.DeleteAsync(url).Result;
                Console.WriteLine(resp);
            }

            return RedirectToAction("Index");
        }
    }
}
