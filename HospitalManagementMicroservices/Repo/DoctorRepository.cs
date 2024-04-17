using Dapper;
using HospitalManagementMicroservices.Context;
using HospitalManagementMicroservices.Entity;
using HospitalManagementMicroservices.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace HospitalManagementMicroservices.Repo
{
    public class DoctorRepository : IDoctorService
    {
        private readonly DapperContext _context;
        private readonly IDepartmentService _departmentService;

        public DoctorRepository(DapperContext context, IDepartmentService departmentService)
        {
            _context = context;
            _departmentService = departmentService; 
        }

        public async Task<IEnumerable<DoctorEntity>> GetAllDoctors()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<DoctorEntity>("SELECT * FROM Doctors");
        }

        public async Task<DoctorEntity> GetDoctorById(int doctorId)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<DoctorEntity>(
                "SELECT * FROM Doctors WHERE DoctorId = @Id", new { Id = doctorId });
        }

        public async Task<int> AddDoctor(DoctorEntity doctor)
        {
            // Check if the provided DepartmentId exists
            var departmentExists = await _departmentService.GetDepartmentById(doctor.DepartmentId);
            if (departmentExists==null)
            {
                throw new Exception("Invalid DepartmentId. Department does not exist.");
            }

            // Proceed with adding the doctor
            var sql = "INSERT INTO Doctors (Name, DepartmentId, Specialization) VALUES (@Name, @DepartmentId,@Specialization)";
            return await _context.CreateConnection().ExecuteAsync(sql, doctor);
        }

        public async Task<int> UpdateDoctor(int id, DoctorEntity doctor)
        {
            using var connection = _context.CreateConnection();
            var sql = "UPDATE Doctors SET Name = @Name, Specialization = @Specialization WHERE DoctorId = @Id";
            return await connection.ExecuteAsync(sql, new { Id = id, doctor.Name, doctor.Specialization });
        }

        public async Task<int> DeleteDoctor(int id)
        {
            using var connection = _context.CreateConnection();
            var sql = "DELETE FROM Doctors WHERE DoctorId = @Id";
            return await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
