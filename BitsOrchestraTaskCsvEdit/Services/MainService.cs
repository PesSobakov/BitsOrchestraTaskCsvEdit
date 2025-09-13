using BitsOrchestraTaskCsvEdit.Models;
using BitsOrchestraTaskCsvEdit.Models.Database;
using BitsOrchestraTaskCsvEdit.Models.DTOs;

namespace BitsOrchestraTaskCsvEdit.Services
{
    public class MainService : IMainService
    {
        private readonly ICsvService _csvService;
        private readonly IDatabaseService _databaseService;
        public MainService(ICsvService csvService, IDatabaseService databaseService)
        {
            _csvService = csvService;
            _databaseService = databaseService;
        }

        public async Task<ServiceResponse> UploadCsv(FileDto file)
        {
            var csvResponse = await _csvService.ReadCsv(file.File.OpenReadStream());
            if (csvResponse.Status != System.Net.HttpStatusCode.OK)
            {
                return new ServiceResponse()
                {
                    Status = csvResponse.Status,
                    Error = csvResponse.Error
                };
            }
            if (csvResponse.Data == null)
            {
                return new ServiceResponse() { Status = System.Net.HttpStatusCode.InternalServerError };
            }

            List<Employee> employees = csvResponse.Data;
            var databaseResponse = await _databaseService.InsertData(employees);
            if (databaseResponse.Status != System.Net.HttpStatusCode.OK)
            {
                return new ServiceResponse()
                {
                    Status = databaseResponse.Status,
                    Error = databaseResponse.Error
                };
            }

            return new ServiceResponse() { Status = System.Net.HttpStatusCode.OK };
        }

        public async Task<ServiceResponse<Stream>> DownloadCsv()
        {
            var databaseResponse = await _databaseService.GetRecords();
            if (databaseResponse.Status != System.Net.HttpStatusCode.OK)
            {
                return new ServiceResponse()
                {
                    Status = databaseResponse.Status,
                    Error = databaseResponse.Error
                };
            }
            if (databaseResponse.Data == null)
            {
                return new ServiceResponse() { Status = System.Net.HttpStatusCode.InternalServerError };
            }

            List<Employee> employees = databaseResponse.Data;
            var csvResponse = await _csvService.WriteCsv(employees);
            if (csvResponse.Status != System.Net.HttpStatusCode.OK)
            {
                return new ServiceResponse()
                {
                    Status = csvResponse.Status,
                    Error = csvResponse.Error
                };
            }
            if (csvResponse.Data == null)
            {
                return new ServiceResponse() { Status = System.Net.HttpStatusCode.InternalServerError };
            }

            Stream stream = csvResponse.Data;

            return new ServiceResponse<Stream>()
            {
                Status = System.Net.HttpStatusCode.OK,
                Data = stream
            };
        }

        public async Task<ServiceResponse<List<Employee>>> GetRecords()
        {
            var databaseResponse = await _databaseService.GetRecords();
            return databaseResponse;
        }

        public async Task<ServiceResponse> EditRecord(int id, EditEmployeeDto dto)
        {
            var databaseResponse = await _databaseService.EditRecord(id,dto);
            return databaseResponse;
        }

        public async Task<ServiceResponse> DeleteRecord(int id)
        {
            var databaseResponse = await _databaseService.DeleteRecord(id);
            return databaseResponse;
        }
    }
}
