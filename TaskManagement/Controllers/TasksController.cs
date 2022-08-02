using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Helpers;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    public class TasksController : Controller
    {
        readonly IConfiguration _Configure;
        readonly string apiBaseUrl="";
        public TasksController(IConfiguration configuration)
        {
            _Configure = configuration;
            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
        }
        public async Task<IActionResult> Index()
        {
            var result = await APIRequester.GetRequest(apiBaseUrl + "/Tasks/GetAll");
            var tasks = JsonConvert.DeserializeObject<List<Tasks>>(result.ToString());
            return View(tasks);
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                var task = new Tasks();
                //Get Employees List
                var empResult = await APIRequester.GetRequest(apiBaseUrl + "/Employees/GetEmployees");
                var emps = JsonConvert.DeserializeObject<List<Employee>>(empResult.ToString());
                ViewBag.EmployeeId = new SelectList(emps, "Id", "Name");
                return View(task);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Opps looks like there's error \n" + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tasks Task)
        {
            try
            {
                var result = await APIRequester.PostRequest(apiBaseUrl + $"/Tasks/Create", Task);


                //Get Employees List
                var empResult = await APIRequester.GetRequest(apiBaseUrl + "/Employees/GetEmployees");
                var emps = JsonConvert.DeserializeObject<List<Employee>>(empResult.ToString());
                ViewBag.EmployeeId = new SelectList(emps, "Id", "Name");

                if (result != null)
                {
                    TempData["Success"] = "Data added successfully";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "There's error please try agin!!";
                return View(Task);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Opps looks like there's error \n" + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var result = await APIRequester.GetRequest(apiBaseUrl + $"/Tasks/GetById?id={id}");
                if (result == null)
                {
                    TempData["Error"] = "There's error please try agin!!";
                    return RedirectToAction(nameof(Index));
                }
                var task = JsonConvert.DeserializeObject<Tasks>(result.ToString());

                //Get Employees List
                var empResult = await APIRequester.GetRequest(apiBaseUrl + "/Employees/GetEmployees");
                var emps = JsonConvert.DeserializeObject<List<Employee>>(empResult.ToString());
                ViewBag.EmployeeId = new SelectList(emps, "Id", "Name");
                return View(task);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Opps looks like there's error \n" + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Tasks Task)
        {
            try
            {
                var result = await APIRequester.PostRequest(apiBaseUrl + $"/Tasks/Update", Task);


                //Get Employees List
                var empResult = await APIRequester.GetRequest(apiBaseUrl + "/Employees/GetEmployees");
                var emps = JsonConvert.DeserializeObject<List<Employee>>(empResult.ToString());
                ViewBag.EmployeeId = new SelectList(emps, "Id", "Name");

                if (result != null)
                {
                    TempData["Success"] = "Data updated successfully";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "There's error please try agin!!";
                return View(Task);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Opps looks like there's error \n"+ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await APIRequester.GetRequest(apiBaseUrl + $"/Tasks/GetById?id={id}");
                if (result == null)
                {
                    TempData["Error"] = "There's error please try agin!!";
                    return RedirectToAction(nameof(Index));
                }
                var task = JsonConvert.DeserializeObject<Tasks>(result.ToString());
                return View(task);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Opps looks like there's error \n" + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Tasks Task)
        {
            try
            {
                var result = await APIRequester.PostRequest(apiBaseUrl + $"/Tasks/Delete", Task);

                if (result != null)
                {
                    TempData["Success"] = "Data updated successfully";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "There's error please try agin!!";
                return View(Task);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Opps looks like there's error \n" + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
