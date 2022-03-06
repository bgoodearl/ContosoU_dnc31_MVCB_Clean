using static CU.Application.Shared.CommonDefs;

namespace CU.Application.Shared.ViewModels
{
    public class SchoolItemEventArgs
    {
        public int ItemID { get; set; }
        public int? SecondaryId { get; set; }
        public int? SecondaryOperation { get; set; }
        public string SecondaryOpString1 { get; set; }
        public UIMode UIMode { get; set; }
    }
}
