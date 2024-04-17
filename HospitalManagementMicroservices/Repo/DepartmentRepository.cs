using Dapper;
using HospitalManagementMicroservices.Context;
using HospitalManagementMicroservices.Entity;
using HospitalManagementMicroservices.Interface;

namespace HospitalManagementMicroservices.Repo
{
    public class DepartmentRepository : IDepartmentService
    {
        private readonly DapperContext _context;

        public DepartmentRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DepartmentEntity>> GetAllDepartments()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<DepartmentEntity>("SELECT * FROM Departments");
        }

        public async Task<DepartmentEntity> GetDepartmentById(int departmentId)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<DepartmentEntity>(
                "SELECT * FROM Departments WHERE DepartmentId = @Id", new { Id = departmentId });
        }

        public async Task<int> AddDepartment(DepartmentEntity department)
        {
            using var connection = _context.CreateConnection();
            var sql = "INSERT INTO Departments (Name, Description) VALUES (@Name, @Description)";
            return await connection.ExecuteAsync(sql, department);
        }


        public async Task<int> UpdateDepartment(int id, DepartmentEntity department)
        {
            using var connection = _context.CreateConnection();
            var sql = "UPDATE Departments SET Name = @Name, Description = @Description WHERE DepartmentId = @Id";
            return await connection.ExecuteAsync(sql, new { Id = id, department.Name, department.Description });
        }

        public async Task<int> DeleteDepartment(int id)
        {
            using var connection = _context.CreateConnection();
            var sql = "DELETE FROM Departments WHERE DepartmentId = @Id";
            return await connection.ExecuteAsync(sql, new { Id = id });
        }
    }

}
