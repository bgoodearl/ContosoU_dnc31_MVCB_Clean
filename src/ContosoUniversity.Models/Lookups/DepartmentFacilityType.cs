using CU.Definitions.Lookups;
using System.Collections.Generic;

namespace ContosoUniversity.Models.Lookups
{
    public class DepartmentFacilityType : LookupBaseWith2cKey
    {
        public DepartmentFacilityType()
        {
            LookupTypeId = (short)CULookupTypes.DepartmentFacilityType;
        }

        private ICollection<Department> _departments;
        public virtual ICollection<Department> Departments
        {
            get { return _departments ?? (_departments = new List<Department>()); }
            protected set { _departments = value; }
        }
    }
}
