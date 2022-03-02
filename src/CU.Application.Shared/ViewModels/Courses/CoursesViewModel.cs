﻿using System.Collections.Generic;

namespace CU.Application.Shared.ViewModels.Courses
{
    public class CoursesViewModel
    {
        public int? CourseID { get; set; }
        public IEnumerable<CourseListItem> CourseList { get; set; }
        public int ViewMode { get; set; }
    }
}
