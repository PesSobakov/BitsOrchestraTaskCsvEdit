using BitsOrchestraTaskCsvEdit.Models;
using BitsOrchestraTaskCsvEdit.Models.Database;
using BitsOrchestraTaskCsvEdit.Models.DTOs;

namespace BitsOrchestraTaskCsvEdit.Services
{
    public interface IMainService
    {
        Task<ServiceResponse> UploadCsv(FileDto file);
        Task<ServiceResponse<Stream>> DownloadCsv();
        Task<ServiceResponse<List<Employee>>> GetRecords();
        Task<ServiceResponse> EditRecord(int id, EditEmployeeDto dto);
        Task<ServiceResponse> DeleteRecord(int id);
    }
}
