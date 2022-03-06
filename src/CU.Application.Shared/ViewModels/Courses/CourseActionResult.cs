
namespace CU.Application.Shared.ViewModels.Courses
{
    public class CourseActionResult
    {
        public string Action { get; set; } = string.Empty;
        public int CourseID { get; set; }
        public int CourseIDNew { get; set; }
        public int ChangeCount { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
