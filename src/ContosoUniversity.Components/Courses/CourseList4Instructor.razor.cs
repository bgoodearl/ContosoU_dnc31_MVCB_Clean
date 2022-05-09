using CU.Application.Shared.DataRequests.SchoolItems.Queries;
using CU.Application.Shared.Models.SchoolDtos;
using CU.Application.Shared.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Components.Courses
{
    public partial class CourseList4Instructor
    {
        [Parameter] public int InstructorID { get; set; }
        [Parameter] public EventCallback<SchoolItemEventArgs> SchoolItemAction { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.
        [Inject] ISender Mediator { get; set; }
        [Inject] ILogger<CourseList4Instructor> Logger { get; set; }
#pragma warning restore CS8618

        protected bool Loading { get; set; }

        private int? InstructorIDSaved { get; set; }

        public IEnumerable<CourseListItemDto> CourseItemList { get; set; } = new List<CourseListItemDto>();

        #region data access

        private async Task LoadCourses()
        {
            try
            {
                Loading = true;
                GetCourseListItemsQuery query = new GetCourseListItemsQuery
                {
                    InstructorID = InstructorID
                };
                InstructorIDSaved = InstructorID;
                CourseItemList = await Mediator.Send(query);
                if (CourseItemList == null)
                {
                    Logger.LogError($"CourseList.LoadDataFromDb Instr ID = {InstructorID} - NO RESULTS");
                }
                else
                {
                    Logger.LogInformation($"CourseList.LoadDataFromDb Instr ID = {InstructorID} itemCount = {CourseItemList.Count()}");
                    await InvokeAsync(() => StateHasChanged()); //Needed to get first instructor's courses to load
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "CourseList4Instructor: ({0}) LoadCourses {1}: {2}",
                    InstructorID, ex.GetType().Name, ex.Message);
            }
            Loading = false;
        }

        #endregion data access

        #region events

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            bool shouldLoad = (Mediator != null) && !Loading && (CourseItemList.Count() == 0);
            if (!shouldLoad && (InstructorID > 0))
            {
                if (!InstructorIDSaved.HasValue || (InstructorIDSaved.Value != InstructorID))
                {
                    shouldLoad = (Mediator != null) && !Loading;
                }
            }

            if (Logger != null) { Logger.LogDebug($"CourseList4Instr.OnAfterRenderAsync: First Render == {firstRender}, shouldLoad={shouldLoad}, InstId = {InstructorID}, InstIdSaved = {InstructorIDSaved}"); }

            if (shouldLoad)
            {
                await LoadCourses();
            }
        }

        ////Uncomment OnInitializedAsync() if needed for debugging problems with initial loading
        //protected override async Task OnInitializedAsync()
        //{
        //    bool shouldLoad = ((Mediator != null) && !Loading && (CourseItemList.Count() == 0));
        //    if (Logger != null) { Logger.LogDebug($"CourseList4Instr.OnInitializedAsync shouldLoad={shouldLoad}"); }
        //}

        #endregion events

    }
}
