using HospitalManagementMicroservices.Entity;
using HospitalManagementMicroservices.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementMicroservices.Controllers
{
    [ApiController]
    [Route("api/departments")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // GET: api/departments
        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _departmentService.GetAllDepartments();
            return Ok(departments);
        }

        // GET: api/departments/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var department = await _departmentService.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        // POST: api/departments
        [HttpPost]
        [Authorize(Role = "Admin")] // Restrict access to users with the "Admin" role
        public async Task<IActionResult> CreateDepartment(DepartmentEntity department)
        {
            var departmentId = await _departmentService.AddDepartment(department);
            if (departmentId > 0)
            {
                return CreatedAtAction(nameof(GetDepartmentById), new { id = departmentId }, department);
            }
            else
            {
                return BadRequest("Failed to create department");
            }
        }


        // PUT: api/departments/{id}
        [HttpPut("{id}")]
        [Authorize(Role = "Admin")]
        public async Task<IActionResult> UpdateDepartment(int id, DepartmentEntity department)
        {
            var updatedDepartment = await _departmentService.UpdateDepartment(id, department);
            if (updatedDepartment == null)
            {
                return NotFound();
            }
            return Ok(updatedDepartment);
        }


    }

}
