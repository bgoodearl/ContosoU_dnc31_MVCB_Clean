
namespace CU.Application.Shared
{
    public static class CommonDefs
    {
        public enum UIMode
        { 
            NoChange = -1,
            List = 0,
            Details,
            Edit,
            Create,
            Delete
        }

        public enum SecondaryOps
        {
            NoOp = 0,
            ShowCoursesForInstructor = 1
        }

    }
}
