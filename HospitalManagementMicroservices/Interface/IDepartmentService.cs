using HospitalManagementMicroservices.Entity;

namespace HospitalManagementMicroservices.Interface
{
    public interface IDepartmentService
    {
        public Task<IEnumerable<DepartmentEntity>> GetAllDepartments();
        public Task<DepartmentEntity> GetDepartmentById(int departmentId);
        public Task<int> AddDepartment(DepartmentEntity department);
        public Task<int> UpdateDepartment(int id, DepartmentEntity department);
    }
}
