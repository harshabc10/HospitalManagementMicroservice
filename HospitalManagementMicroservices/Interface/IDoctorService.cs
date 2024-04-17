using HospitalManagementMicroservices.Entity;
using System.Numerics;

namespace HospitalManagementMicroservices.Interface
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorEntity>> GetAllDoctors();
        Task<DoctorEntity> GetDoctorById(int doctorId);
        Task<int> AddDoctor(DoctorEntity doctor);
        Task<int> UpdateDoctor(int id, DoctorEntity doctor);
    }
}
