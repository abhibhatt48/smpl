using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {
        private ApiDataDbContext context;
        [Route("api/department")]
        [HttpGet]
        public IActionResult Get()
        {
            context = new ApiDataDbContext();
            var data = context.departments;
            return Ok(data);
        }

        [HttpPost("api/department")]
        public IActionResult Post([FromBody]Department dept)
        {
            if (dept is null)
            {
                throw new ArgumentNullException(nameof(dept));
            }

            context = new ApiDataDbContext();
            context.departments.Add(dept);
            context.SaveChanges();
            return Ok(dept);
        }
        [HttpPut("api/department/{id}/{name}")]
        public IActionResult Patch(string id, string name)
        {
            context = new ApiDataDbContext();
            int _id = int.Parse(id);
            var data = context.departments.FirstOrDefault(d => d.DepartmentId == _id);
            if (data == null) { return NotFound(); }
            else
            {
                data.DepartmentName = name;
                context.SaveChanges();
                return Ok(data);

            }
        }
        [HttpDelete("api/department/{id}")]
        public IActionResult Delete(string id)
        {
            context = new ApiDataDbContext();
            int _id = int.Parse(id);
            var data = context.departments.FirstOrDefault(d => d.DepartmentId == _id);
            if (data == null) { return NotFound(); }
            else
            {
                context.departments.Remove(data);
                context.SaveChanges();
                return Ok("department deleted");
            }


        }
    }
}
