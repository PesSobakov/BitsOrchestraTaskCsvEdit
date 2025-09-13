using System.ComponentModel.DataAnnotations;

namespace BitsOrchestraTaskCsvEdit.Models
{
    public class FileDto
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
