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

        protected string Message { get; set; }
        protected UIMode UIMode { get; set; }

        public async Task InstructorAction(SchoolItemEventArgs args)
        {
            if (args != null)
            {
                Message = null;
                try
                {
                    if (args.ItemID != 0)
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
