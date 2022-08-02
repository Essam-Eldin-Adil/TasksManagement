using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IDapper _dapper;
        public EmployeesController(IDapper dapper)
        {
            _dapper = dapper;
        }

        [HttpGet(nameof(GetEmployees))]
        public async Task<List<Employee>> GetEmployees()
        {
            var result = await System.Threading.Tasks.Task.FromResult(_dapper.GetAll<Employee>($"Select Id,Name+' - '+Position as Name,Position from [Employees]", null, commandType: CommandType.Text));
            return result;
        }
    }
}
