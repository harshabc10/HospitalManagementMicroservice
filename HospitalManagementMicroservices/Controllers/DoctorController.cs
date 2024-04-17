using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalManagementMicroservices.Entity;
using HospitalManagementMicroservices.Interface;
using Microsoft.AspNetCore.Authorization;

namespace HospitalManagementMicroservices.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorEntity>>> GetAllDoctors()
        {
            // Implement logic to get all doctors from the service
            var doctors = await _doctorService.GetAllDoctors();
            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorEntity>> GetDoctorById(int id)
        {
            // Implement logic to get a doctor by ID from the service
            var doctor = await _doctorService.GetDoctorById(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return Ok(doctor);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<int>> AddDoctor(DoctorEntity doctor)
        {
            // Implement logic to add a new doctor using the service
            var result = await _doctorService.AddDoctor(doctor);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<int>> UpdateDoctor(int id, DoctorEntity doctor)
        {
            // Implement logic to update a doctor using the service
            var result = await _doctorService.UpdateDoctor(id, doctor);
            return Ok(result);
        }

    }
}
