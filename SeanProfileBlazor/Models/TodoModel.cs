using System.ComponentModel.DataAnnotations;

namespace SeanProfileBlazor
{
    public class TodoModel
    {
        public int Id { get; set; }
        
        [Required]
        [RegularExpression(@".*\S+.*$", ErrorMessage = "Field Cannot be Blank Or Whitespace")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Maximum 100 characters")]
        public string Title { get; set; }
        public string Status { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}