using Dapper;
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
    public class TasksController : ControllerBase
    {
        private readonly IDapper _dapper;
        public TasksController(IDapper dapper)
        {
            _dapper = dapper;
        }

        [HttpPost(nameof(Create))]
        public async Task<int> Create(Tasks data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("id", data.Id, DbType.Int32);
            dbparams.Add("name", data.Name, DbType.String);
            dbparams.Add("description", data.Description, DbType.String);
            dbparams.Add("status", data.Status, DbType.Boolean);
            dbparams.Add("employeeId", data.EmployeeId, DbType.Int32);
            dbparams.Add("statementType", "Insert", DbType.String);
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_Save_Task]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            return result;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<List<Tasks>> GetAll()
        {
            var result = await Task.FromResult(_dapper.GetAll<Tasks>($"Select " +
                $"Tasks.Id," +
                $"Tasks.Name," +
                $"Description," +
                $"Status," +
                $"EmployeeId," +
                $"emp.Name as EmployeeName," +
                $"emp.Position from [Tasks] " +
                $"inner join Employees emp on EmployeeId = emp.Id", null, commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetById))]
        public async Task<Tasks> GetById(int Id)
        {
            var result = await Task.FromResult(_dapper.Get<Tasks>($"Select * from [Tasks] where Id = {Id}", null, commandType: CommandType.Text));
            return result;
        }

        [HttpPost(nameof(Update))]
        public async Task<int> Update(Tasks data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("id", data.Id, DbType.Int32);
            dbparams.Add("name", data.Name, DbType.String);
            dbparams.Add("description", data.Description, DbType.String);
            dbparams.Add("status", data.Status, DbType.Boolean);
            dbparams.Add("employeeId", data.EmployeeId, DbType.Int32);
            dbparams.Add("statementType", "Update", DbType.String);
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_Save_Task]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            return result;
        }

        [HttpPost(nameof(Delete))]
        public async Task<int> Delete(Tasks data)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("id", data.Id, DbType.Int32);
                var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_Delete_Task]"
                                , dbparams,
                                commandType: CommandType.StoredProcedure));
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
