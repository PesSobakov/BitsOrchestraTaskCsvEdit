namespace BitsOrchestraTaskCsvEdit.Services
{
    public interface IErrorResponse
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public Dictionary<string, List<string>> Errors { get; set; }
    }
}
