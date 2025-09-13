using BitsOrchestraTaskCsvEdit.Models.Database;
using BitsOrchestraTaskCsvEdit.Models.DTOs;
using CsvHelper;
using System.Globalization;

namespace BitsOrchestraTaskCsvEdit.Services
{
    public class CsvService : ICsvService
    {
        private readonly CsvHelper.Configuration.CsvConfiguration _csvConfigurationWithHeader;
        private readonly CsvHelper.Configuration.CsvConfiguration _csvConfigurationWithoutHeader;
        public CsvService()
        {
            _csvConfigurationWithHeader = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                DetectDelimiter = true,
                HasHeaderRecord = true,
                HeaderValidated = null
            };
            _csvConfigurationWithoutHeader = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                DetectDelimiter = true,
                HasHeaderRecord = false,
                HeaderValidated = null
            };
        }

        public async Task<ServiceResponse<List<Employee>>> ReadCsv(Stream stream)
        {
            try
            {
                List<CsvEmployeeDto> csvData;
                using (var reader = new StreamReader(stream))
                using (var csv = new CsvReader(reader, _csvConfigurationWithoutHeader))
                {
                    csvData = csv.GetRecords<CsvEmployeeDto>().ToList();
                }
                List<Employee> employees = csvData.Select(MapCsvEmployeeDtoToEmployee).ToList();

                return new ServiceResponse<List<Employee>>()
                {
                    Status = System.Net.HttpStatusCode.OK,
                    Data = employees
                };
            }
            catch (CsvHelperException e)
            {
                return new ServiceResponse()
                {
                    Status = System.Net.HttpStatusCode.BadRequest,
                    Error = new BadRequestResponse()
                    {
                        Errors = new() { { "Parsing error", ["File has incorrect format"] } }
                    }
                };
            }
        }

        public async Task<ServiceResponse<Stream>> WriteCsv(List<Employee> employees)
        {
            List<CsvEmployeeDto> dtos = employees.Select(MapEmployeeToCsvEmployeeDto).ToList();
            MemoryStream csvFile = new MemoryStream();
            try
            {
                using (var writer = new StreamWriter(csvFile, leaveOpen: true))
                using (var csvWriter = new CsvWriter(writer, _csvConfigurationWithoutHeader))
                {
                    csvWriter.WriteRecords(dtos);
                }
                csvFile.Seek(0,SeekOrigin.Begin);
                return new ServiceResponse<Stream>()
                {
                    Status = System.Net.HttpStatusCode.OK,
                    Data = csvFile
                };
            }
            catch (CsvHelperException e)
            {
                return new ServiceResponse()
                {
                    Status = System.Net.HttpStatusCode.BadRequest,
                    Error = new BadRequestResponse()
                    {
                        Errors = new() { { "Writing error", ["Unknown error during writing csv file"] } }
                    }
                };
            }
        }

        private CsvEmployeeDto MapEmployeeToCsvEmployeeDto(Employee employee)
        {
            return new CsvEmployeeDto()
            {
                DateOfBirth = employee.DateOfBirth,
                Married = employee.Married,
                Name = employee.Name,
                Phone = employee.Phone,
                Salary = employee.Salary
            };
        }

        private Employee MapCsvEmployeeDtoToEmployee(CsvEmployeeDto dto)
        {
            return new Employee()
            {
                DateOfBirth = dto.DateOfBirth,
                Married = dto.Married,
                Name = dto.Name,
                Phone = dto.Phone,
                Salary = dto.Salary
            };
        }

    }
}
