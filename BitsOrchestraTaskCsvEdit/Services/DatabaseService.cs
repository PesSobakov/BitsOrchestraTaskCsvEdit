using BitsOrchestraTaskCsvEdit.Models.Database;
using BitsOrchestraTaskCsvEdit.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BitsOrchestraTaskCsvEdit.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly EmployeeContext _employeeContext;
        public DatabaseService(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _employeeContext.Database.EnsureCreated();
        }

        public async Task<ServiceResponse> InsertData(List<Employee> employees)
        {
            _employeeContext.RemoveRange(_employeeContext.Employees);
            await _employeeContext.Employees.AddRangeAsync(employees);
            try
            {
                await _employeeContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new ServiceResponse()
                {
                    Status = System.Net.HttpStatusCode.BadRequest,
                    Error = new BadRequestResponse()
                    {
                        Errors = new() { { "Concurrency error", ["Update failed due to conflicting changes. Try again later"] } }
                    }
                };
            }

            return new ServiceResponse() { Status = System.Net.HttpStatusCode.OK };
        }

        public async Task<ServiceResponse<List<Employee>>> GetRecords()
        {
            List<Employee> employees = await _employeeContext.Employees.ToListAsync();
            return new ServiceResponse<List<Employee>>()
            {
                Status = System.Net.HttpStatusCode.OK,
                Data = employees
            };
        }

        public async Task<ServiceResponse> EditRecord(int id, EditEmployeeDto dto)
        {
            Employee? employee = await _employeeContext.Employees.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (employee == null)
            {
                return new ServiceResponse()
                {
                    Status = System.Net.HttpStatusCode.BadRequest,
                    Error = new BadRequestResponse()
                    {
                        Errors = new() { { "Id", ["Entity with specified id does not exist"] } }
                    }
                };
            }

            employee.Name = dto.Name;
            employee.DateOfBirth = dto.DateOfBirth;//DateOnly.FromDateTime(employeeDto.DateOfBirth.Value);
            employee.Married = dto.Married;
            employee.Phone = dto.Phone;
            employee.Salary = dto.Salary;

            try
            {
                await _employeeContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new ServiceResponse()
                {
                    Status = System.Net.HttpStatusCode.BadRequest,
                    Error = new BadRequestResponse()
                    {
                        Errors = new() { { "Concurrency error", ["Update failed due to conflicting changes. Try again later"] } }
                    }
                };
            }
            return new ServiceResponse() { Status = System.Net.HttpStatusCode.OK };
        }

        public async Task<ServiceResponse> DeleteRecord(int id)
        {
            Employee? employee = await _employeeContext.Employees.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (employee == null)
            {
                return new ServiceResponse()
                {
                    Status = System.Net.HttpStatusCode.BadRequest,
                    Error = new BadRequestResponse()
                    {
                        Errors = new() { { "Id", ["Entity with specified id does not exist"] } }
                    }
                };
            }

            _employeeContext.Employees.Remove(employee);
            try
            {
                await _employeeContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new ServiceResponse()
                {
                    Status = System.Net.HttpStatusCode.BadRequest,
                    Error = new BadRequestResponse()
                    {
                        Errors = new() { { "Concurrency error", ["Update failed due to conflicting changes. Try again later"] } }
                    }
                };
            }
            return new ServiceResponse() { Status = System.Net.HttpStatusCode.OK };
        }
    }
}
