using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BitsOrchestraTaskCsvEdit.Models.DTOs
{
    public class EditEmployeeDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateOnly DateOfBirth { get; set; }
        [Required]
        public bool Married { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Salary { get; set; }

    }
}
