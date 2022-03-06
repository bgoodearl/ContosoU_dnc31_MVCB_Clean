using CU.Application.Shared.DataRequests.SchoolItems.Commands;
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
    public partial class StudentEdit
    {
        [Parameter] public bool NewStudent { get; set; }

        [Parameter] public StudentEditDto Student2Edit { get; set; } = new StudentEditDto();

        [Parameter] public EventCallback<SchoolItemEventArgs> StudentAction { get; set; }

        [Inject] protected ILogger<StudentEdit> Logger { get; set; }
        [Inject] ISender Mediator { get; set; }

        protected string Message { get; set; }

        private async Task HandleValidSubmitAsync()
        {
            if (NewStudent)
            {
                var command = new CreateStudentItemCommand(Student2Edit);

                try
                {
                    int newStudentId = await Mediator.Send(command);
                    if (newStudentId != 0)
                    {
                        await OnReturnToList();
                    }
                }
                catch (CASE.ValidationException ex)
                {
                    Logger.LogError(ex, "StudentEdit: HandleValidSubmitAsync {0}: {1}",
                        ex.GetType().Name, ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "StudentEdit: HandleValidSubmitAsync {0}: {1}",
                        ex.GetType().Name, ex.Message);
                }
            }
            else
            {
                var command = new UpdateStudentItemCommand(Student2Edit);

                try
                {
                    int studentId = await Mediator.Send(command);
                    if (studentId != 0) //TODO: does value of studentId need to be checked here?
                    {
                        await OnReturnToList();
                    }
                }
                catch (CASE.NotFoundException ex)
                {
                    Logger.LogError(ex, "StudentEdit({0}): HandleValidSubmitAsync {1}: {2}",
                        Student2Edit.ID, ex.GetType().Name, ex.Message);
                    Message = $"Error saving StudentID = {Student2Edit.ID} - Student not found";
                }
                catch (CASE.ValidationException ex)
                {
                    //TODO: Handle validation messages
                    Logger.LogError(ex, "StudentEdit({0}): HandleValidSubmitAsync {1}: {2}",
                        Student2Edit.ID, ex.GetType().Name, ex.Message);
                    Message = $"Validation error saving student";
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "StudentEdit({0}): HandleValidSubmitAsync {1}: {2}",
                        Student2Edit.ID, ex.GetType().Name, ex.Message);
                    Message = $"System error saving student";
                }
            }
        }

        public async Task OnReturnToList()
        {
            SchoolItemEventArgs args = new SchoolItemEventArgs
            {
                UIMode = UIMode.List
            };
            await StudentAction.InvokeAsync(args);
        }

    }
}
