using BitsOrchestraTaskCsvEdit.Models.Database;
using BitsOrchestraTaskCsvEdit.Models.DTOs;

namespace BitsOrchestraTaskCsvEdit.Services
{
    public interface IDatabaseService
    {
        Task<ServiceResponse> InsertData(List<Employee> employees);
        Task<ServiceResponse<List<Employee>>> GetRecords();
        Task<ServiceResponse> EditRecord(int id, EditEmployeeDto dto);
        Task<ServiceResponse> DeleteRecord(int id);
    }
}
