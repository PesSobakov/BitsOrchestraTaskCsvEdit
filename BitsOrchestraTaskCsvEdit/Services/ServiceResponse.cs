using System.Net;

namespace BitsOrchestraTaskCsvEdit.Services
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public HttpStatusCode Status { get; set; }
        public IErrorResponse? Error { get; set; }
        public static implicit operator ServiceResponse<T>(ServiceResponse serviceResponse) => new ServiceResponse<T>() { Status = serviceResponse.Status, Error = serviceResponse.Error };
    }
    public class ServiceResponse
    {
        public HttpStatusCode Status { get; set; }
        public IErrorResponse? Error { get; set; }
    }
}
