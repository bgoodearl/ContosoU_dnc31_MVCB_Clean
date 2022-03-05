using CU.Application.Shared.Interfaces;
using CU.Application.Shared.ViewModels;
using CU.Application.Shared.ViewModels.Courses;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using static CU.Application.Shared.CommonDefs;

namespace ContosoUniversity.Components.Courses
{
    public partial class Courses
    {
        [Parameter] 
        public SchoolItemViewModel CoursesVM { get; set; }

        protected CourseEditDto Course2Edit { get; set; }
        protected string Message { get; set; }
        protected UIMode UIMode { get; set; }
        protected CourseListItem SelectedCourse { get; set; }
        protected CourseItem SelectedCourseDetails { get; set; }

        [Inject] 
        protected ILogger<Courses> Logger { get; set; }

        [Inject]
        protected ISchoolViewDataRepositoryFactory SchoolViewDataRepositoryFactory { get; set; }

        protected async Task OnCreateCourse()
        {
            SchoolItemEventArgs args = new SchoolItemEventArgs
            {
                UIMode = UIMode.Create
            };
            await SchoolItemAction(args);
        }

        public async Task SchoolItemAction(SchoolItemEventArgs args)
        {
            if (args != null)
            {
                Message = null;
                try
                {
                    ISchoolViewDataRepository dataHelper = SchoolViewDataRepositoryFactory.GetViewDataRepository();
                    if (args.ItemID != 0)
                    {
                        if (args.UIMode == UIMode.Details)
                        {
                            var details = await dataHelper.GetCourseDetailsNoTrackingAsync(args.ItemID);
                            if (details == null)
                            {
                                Message = "Course not found";
                            }
                            else
                            {
                                SelectedCourseDetails = details;
                                UIMode = args.UIMode;
                            }
                        }
                        else if (args.UIMode == UIMode.Edit)
                        {
                            Course2Edit = await dataHelper.GetCourse2EditAsync(args.ItemID);
                            if (Course2Edit == null)
                            {
                                Message = "Course not found";
                            }
                            else
                            {
                                UIMode = args.UIMode;
                            }
                        }
                        else if (args.UIMode == UIMode.Delete)
                        {
                            var details = await dataHelper.GetCourseDetailsNoTrackingAsync(args.ItemID);
                            if (details == null)
                            {
                                Message = "Course not found";
                            }
                            else
                            {
                                SelectedCourseDetails = details;
                                UIMode = args.UIMode;
                            }
                        }
                    }
                    else
                    {
                        if (args.UIMode == UIMode.List)
                        {
                            CoursesVM.ViewMode = 0; //Clear initial ViewMode from page load
                            UIMode = args.UIMode;
                        }
                        else if (args.UIMode == UIMode.Create)
                        {
                            UIMode = args.UIMode;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Courses-CourseAction id={0}, uiMode={1} - {2}: {3}",
                        args.ItemID, args.UIMode, ex.GetType().Name, ex.Message);
                    Message = $"Error setting up {args.UIMode} with CourseID = {args.ItemID} - contact Support";
                }
            }
        }

        protected async Task OnConfirmDelete()
        {
            try
            {
                if (SelectedCourseDetails == null)
                {
                    Message = $"Could not delete course - contact Support";
                }
                else
                {
                    ISchoolViewDataRepository dataHelper = SchoolViewDataRepositoryFactory.GetViewDataRepository();
                    CourseActionResult result = await dataHelper.DeleteCourseAsync(SelectedCourseDetails.CourseID);

                    if (result.ChangeCount < 1)
                    {
                        Logger.LogError("Courses-ConfirmDelete id={0}, action={1}, changeCount={2} | {3}",
                            SelectedCourseDetails.CourseID, result.Action, result.ChangeCount, result.ErrorMessage);
                        Message = $"Could not delete course {SelectedCourseDetails.CourseID} - contact Support";
                    }
                    else
                    {
                        SelectedCourseDetails = null;
                        SchoolItemEventArgs args = new SchoolItemEventArgs
                        {
                            UIMode = UIMode.List
                        };
                        await SchoolItemAction(args);
                    }
                }
            }
            catch (Exception ex)
            {
                int? courseId = SelectedCourseDetails != null ? SelectedCourseDetails.CourseID : (int?)null;
                Logger.LogError(ex, "Courses-ConfirmDelete id={0}, uiMode={1} - {2}: {3}",
                    courseId, UIMode, ex.GetType().Name, ex.Message);
                Message = $"Error setting up {UIMode} with CourseID = {courseId} - contact Support";
            }
        }

        protected override async Task OnInitializedAsync()
        {
            ISchoolViewDataRepository dataHelper = SchoolViewDataRepositoryFactory.GetViewDataRepository();
            if (CoursesVM != null)
            {
                CourseItem details = null;

                if ((CoursesVM.ViewMode == (int)UIMode.Details) && (CoursesVM.ItemID.HasValue))
                {
                    details = await dataHelper.GetCourseDetailsNoTrackingAsync(CoursesVM.ItemID.Value);
                    if ((details == null) || (details.CourseID == 0))
                    {
                        CoursesVM.ViewMode = (int)UIMode.List;
                        Message = $"Course {CoursesVM.ItemID} not found";
                        details = null;
                        UIMode = UIMode.List;
                    }
                }
                if (details != null)
                {
                    SelectedCourseDetails = details;
                    UIMode = UIMode.Details;
                }
            }
            else
            {
                CoursesVM = new SchoolItemViewModel();
            }
        }

        protected async Task OnReturnToList()
        {
            SchoolItemEventArgs args = new SchoolItemEventArgs
            {
                UIMode = UIMode.List
            };
            await SchoolItemAction(args);
        }
    }
}
