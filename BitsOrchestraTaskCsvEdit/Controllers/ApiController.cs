using BitsOrchestraTaskCsvEdit.Models;
using BitsOrchestraTaskCsvEdit.Models.Database;
using BitsOrchestraTaskCsvEdit.Models.DTOs;
using BitsOrchestraTaskCsvEdit.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BitsOrchestraTaskCsvEdit.Controllers
{
    [ApiController]
    public class ApiController : Controller
    {
        private readonly IMainService _mainService;

        public ApiController(IMainService mainService)
        {
            _mainService = mainService;
        }

        [HttpGet("Api/GetRecords")]
        public async Task<IActionResult> GetRecords()
        {
            var response = await _mainService.GetRecords();

            switch (response.Status)
            {
                case HttpStatusCode.OK:
                    if (response.Data == null) { return StatusCode((int)HttpStatusCode.InternalServerError); }
                    return Ok(response.Data);
                default:
                    return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPatch("Api/EditRecord/{id}")]
        public async Task<IActionResult> EditRecord(int id, [FromBody] EditEmployeeDto dto)
        {
            var response = await _mainService.EditRecord(id, dto);

            switch (response.Status)
            {
                case HttpStatusCode.OK:
                    return Ok();
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.Error);
                default:
                    return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("Api/DeleteRecord/{id}")]
        public async Task<IActionResult> DeleteRecord(int id)
        {
            var response = await _mainService.DeleteRecord(id);

            switch (response.Status)
            {
                case HttpStatusCode.OK:
                    return Ok();
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.Error);
                default:
                    return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("Api/UploadCsv")]
        public async Task<IActionResult> UploadCsv( FileDto dto)
        {
            var response = await _mainService.UploadCsv(dto);

            switch (response.Status)
            {
                case HttpStatusCode.OK:
                    return Ok();
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.Error);
                default:
                    return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("Api/DownloadCsv")]
        public async Task<IActionResult> DownloadCsv()
        {
            var response = await _mainService.DownloadCsv();

            switch (response.Status)
            {
                case HttpStatusCode.OK:
                    if (response.Data == null) { return StatusCode((int)HttpStatusCode.InternalServerError); }
                    return File(response.Data, "text/csv", "Employees.csv");
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.Error);
                default:
                    return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
