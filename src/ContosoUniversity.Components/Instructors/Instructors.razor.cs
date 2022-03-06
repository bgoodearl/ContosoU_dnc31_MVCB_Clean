using CU.Application.Shared.ViewModels.Instructors;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using CASE = CU.Application.Shared.Common.Exceptions;
using static CU.Application.Shared.CommonDefs;
using CU.Application.Shared.ViewModels;

namespace ContosoUniversity.Components.Instructors
{
    public partial class Instructors
    {
        [Parameter] public SchoolItemViewModel InstructorsVM { get; set; }

        [Inject] protected ILogger<Instructors> Logger { get; set; }
        [Inject] ISender Mediator { get; set; }

        protected int? InstructorId4Courses { get; set; }
        protected string InstructorName4Courses { get; set; }
        protected string Message { get; set; }
        protected UIMode UIMode { get; set; }

        public async Task InstructorAction(SchoolItemEventArgs args)
        {
            if (args != null)
            {
                InstructorId4Courses = null;
                Message = null;
                try
                {
                    if (args.UIMode == UIMode.NoChange)
                    {
                        Logger.LogDebug($"Instructors-InstructorAction UIMode {args.UIMode}, 2nd Id: {args.SecondaryId}, 2nd op: {args.SecondaryOperation}");
                        if (args.SecondaryOperation.HasValue && (args.SecondaryOperation.Value == (int)SecondaryOps.ShowCoursesForInstructor))
                        {
                            //Message = $"2nd Op = {args.SecondaryOperation}, Id = {args.SecondaryId}";

                            if (args.SecondaryId.HasValue)
                            {
                                bool refresh = false;
                                if (!InstructorId4Courses.HasValue || (InstructorId4Courses.Value != args.SecondaryId.Value))
                                {
                                    refresh = true;
                                }
                                InstructorId4Courses = args.SecondaryId.Value;
                                InstructorName4Courses = args.SecondaryOpString1;
                                if (refresh)
                                {
                                    await InvokeAsync(() => StateHasChanged());
                                }
                            }
                        }
                    }
                    else if (args.ItemID != 0)
                    {

                    }
                    else
                    {
                        if (args.UIMode == UIMode.List)
                        {
                            if (InstructorsVM != null)
                            {
                                InstructorsVM.ViewMode = 0; //Clear initial ViewMode from page load
                            }
                            UIMode = args.UIMode;
                        }

                    }
                }
                catch (CASE.NotFoundException ex)
                {
                    Logger.LogError(ex, "Instructors-InstructorAction id={0}, uiMode={1} - {2}: {3}",
                        args.ItemID, args.UIMode, ex.GetType().Name, ex.Message);
                    Message = $"Error setting up {args.UIMode} with InstructorID = {args.ItemID} - Instructor not found - contact Support";
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Instructors-InstructorAction id={0}, uiMode={1} - {2}: {3}",
                        args.ItemID, args.UIMode, ex.GetType().Name, ex.Message);
                    Message = $"Error setting up {args.UIMode} with InstructorID = {args.ItemID} - contact Support";
                }
            }
        }
    }
}
