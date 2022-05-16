using CU.Definitions.Lookups;
using System.Collections.Generic;

namespace ContosoUniversity.Models.Lookups
{
    public class CoursePresentationType : LookupBaseWith2cKey
    {
        public CoursePresentationType()
        {
            LookupTypeId = (short)CULookupTypes.CoursePresentationType;
        }

        private ICollection<Course> _courses;
        public virtual ICollection<Course> Courses
        {
            get { return _courses ?? (_courses = new List<Course>()); }
            protected set { _courses = value; }
        }
    }
}
