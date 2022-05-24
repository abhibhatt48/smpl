using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    
    public class EmployeeController : Controller
    {
        private ApiDataDbContext context;

        public static bool validateDepartment(Employee emp)
        {
            ApiDataDbContext context = new ApiDataDbContext();
            Department temp = context.departments.First(x => x.DepartmentId == emp.DepartmentId);
            return (temp == null) ? false : true;
        }
        
        [Route("api/employee/[Controller]")]
        [HttpGet]
        public IActionResult Get()
        {
            context = new ApiDataDbContext();
            var data = context.employees;
            return Ok(data);
        }
        [HttpPost("api/employee")]
        public IActionResult Post([FromBody] Employee emp)
        {
            context = new ApiDataDbContext();
            var data = context.employees.FirstOrDefault(x => x.EmployeeId == emp.EmployeeId); 
            if(data == null) {
                
                if (validateDepartment(emp))
                {
                    context.employees.Add(emp);
                    context.SaveChanges();
                    return Created("api/employee", emp);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest(data);
            }
            
        }
        [HttpPut("api/employee/{id}")]
        public IActionResult Patch(string id,[FromBody] Employee emp)
        {
            context = new ApiDataDbContext();
            int _id = int.Parse(id);
            var data = context.employees.FirstOrDefault(e => e.EmployeeId == _id);
            if (data == null) { return NotFound(); }
            else
            {
                if (validateDepartment(emp))
                {
                    data.Surname = (emp.Surname == null) ? data.Surname : emp.Surname;
                    data.MobileNumber = (emp.MobileNumber == 0) ? data.MobileNumber : emp.MobileNumber;
                    data.Address = (emp.Address == null) ? data.Address : emp.Address;
                    data.Qualification = (emp.Qualification == null) ? data.Qualification : emp.Qualification;
                    data.Name = (emp.Name == null) ? data.Name : emp.Name;
                    data.DepartmentId = (emp.DepartmentId == 0) ? data.DepartmentId : emp.DepartmentId;
                    context.SaveChanges();
                    return Ok(data);
                }
                else
                {
                    return BadRequest();
                }
            }
        }
        [HttpDelete("api/employee/{id}")]
        public IActionResult Delete(string id)
        {
            context = new ApiDataDbContext();
            int _id = int.Parse(id);
            var data = context.employees.FirstOrDefault(e => e.EmployeeId == _id);
            if (data == null) { return NotFound(); }
            else
            {
                context.employees.Remove(data);
                context.SaveChanges();
                return Ok("eployee deleted");
            }
        }

    }
}
