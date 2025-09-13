namespace BitsOrchestraTaskCsvEdit.Services
{
    public class BadRequestResponse:IErrorResponse
    {
        public string Type { get; set; } = "https://tools.ietf.org/html/rfc9110#section-15.5.1";
        public string Title { get; set; } = "One or more validation errors occurred.";
        public int Status { get; set; } = 400;
        public Dictionary<string, List<string>> Errors { get; set; } = [];
    }
}
