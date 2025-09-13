using BitsOrchestraTaskCsvEdit.Models.Database;

namespace BitsOrchestraTaskCsvEdit.Services
{
    public interface ICsvService
    {
        Task<ServiceResponse<List<Employee>>> ReadCsv(Stream stream);
        Task<ServiceResponse<Stream>> WriteCsv(List<Employee> employees);

    }
}
