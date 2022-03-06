using CU.Application.Shared.DataRequests.SchoolItems.Queries;
using CU.Application.Shared.ViewModels.Students;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using CASE = CU.Application.Shared.Common.Exceptions;
using static CU.Application.Shared.CommonDefs;
using CU.Application.Shared.ViewModels;

namespace ContosoUniversity.Components.Students
{
    public partial class Students
    {
        [Parameter] public SchoolItemViewModel StudentsVM { get; set; }

        [Inject] protected ILogger<Students> Logger { get; set; }
        [Inject] ISender Mediator { get; set; }

        protected string Message { get; set; }
        protected UIMode UIMode { get; set; }
        protected StudentListItem SelectedStudent { get; set; }
        //protected StudentItem SelectedStudentDetails { get; set; }
        protected StudentEditDto Student2Edit { get; set; }


        public async Task StudentAction(SchoolItemEventArgs args)
        {
            if (args != null)
            {
                Message = null;
                try
                {
                    if (args.ItemID != 0)
                    {
                        if (args.UIMode == UIMode.Edit)
                        {
                            GetStudentEditDtoQuery query = new GetStudentEditDtoQuery
                            {
                                StudentId = args.ItemID
                            };
                            Student2Edit = await Mediator.Send(query);
                            if (Student2Edit != null)
                            {
                                UIMode = args.UIMode;
                            }
                            else
                            {
                                //TODO: Something?  In theory, should never get here
                            }
                        }
                    }
                    else
                    {
                        if (args.UIMode == UIMode.List)
                        {
                            if (StudentsVM != null)
                            {
                                StudentsVM.ViewMode = 0; //Clear initial ViewMode from page load
                            }
                            UIMode = args.UIMode;
                        }
                        else if (args.UIMode == UIMode.Create)
                        {
                            Student2Edit = new StudentEditDto
                            {
                                EnrollmentDate = DateTime.Now.Date
                            };
                            UIMode = args.UIMode;
                        }
                    }
                }
                catch (CASE.NotFoundException ex)
                {
                    Logger.LogError(ex, "Students-StudentAction id={0}, uiMode={1} - {2}: {3}",
                        args.ItemID, args.UIMode, ex.GetType().Name, ex.Message);
                    Message = $"Error setting up {args.UIMode} with StudentID = {args.ItemID} - Student not found - contact Support";
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Students-StudentAction id={0}, uiMode={1} - {2}: {3}",
                        args.ItemID, args.UIMode, ex.GetType().Name, ex.Message);
                    Message = $"Error setting up {args.UIMode} with StudentID = {args.ItemID} - contact Support";
                }
            }
        }

        #region events

        protected async Task OnCreateStudent()
        {
            SchoolItemEventArgs args = new SchoolItemEventArgs
            {
                UIMode = UIMode.Create
            };
            await StudentAction(args);
        }

        #endregion events

    }
}
