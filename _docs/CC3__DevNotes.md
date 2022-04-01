# Contoso University Clean 3.1 - Dev Notes

<table>
    <tr>
        <th>Date</th><th>Dev</th>
		<th>Notes</th>
    </tr>
    <tr>
        <td>4/1/2022</td><td>bg</td>
		<td>
            Changed render-mode for Blazor components to "Server"<br/>
		</td>
    </tr>
    <tr>
        <td>3/27/2022</td><td>bg</td>
		<td>
            Changed http port in launchSettings to fix VS2019 debug startup<br/>
		</td>
    </tr>
    <tr>
        <td>3/6/2022</td><td>bg</td>
		<td>
            Added GetCourseListItemsQuery with handler and test<br/>
            Extended SchoolItemEventArgs with additional properties<br/>
            Minor code cleanup<br/>
            Added component CourseList4Instructor, 
            use optionally when Instructors list is visible<br/>
		</td>
    </tr>
    <tr>
        <td>3/5/2022</td><td>bg</td>
		<td>
            Tweaked use of ISchoolDbContextFactory<br/>
            Added test CanGetCoursesWithInstructorsAsync() (from .NET 6 version)<br/>
            Added UpdateStudentItemCommand, handler, validator, test<br/>
            Added StudentEdit component<br/>
            Added GetInstructorListItemsWithPaginationQuery, test<br/>
            Added razor components: Instructors, InstructorList<br/>
            Updated Instructors Index MVC page to use razor components by default<br/>
            Refactored Components - share SchoolItemEventArgs, SchoolItemViewModel,
            Moved Course components to Courses folder<br/>
            Fixed problem with saving changed DepartmentID when saving Course edit<br/>
            Added optional InstructorID to GetCourseListItemsWithPaginationQuery,
            updated handler, added test<br/>
		</td>
    </tr>
    <tr>
        <td>3/4/2022</td><td>bg</td>
		<td>
            Minor re-org:
            Removed DomainEvent artifacts - deferring implementation of DomainEvent
            until after a rexamination of the topic<br/>
            Moved ISchoolDbContext and ISchoolDbContextFactory from Application
            to CU.Application.Data.Common so Infrastructure doesn't depend on Application<br/>
            Added unit test for MediatR requests having handlers<br/>
            Updated persistent models with private default constructors and
            public constructors with parameters as in .NET 6 implementation,
            updated Seeding<br/>
            Rebuilt migrations, updated Schema1 SQL script, tweaked
            data seeding to follow code from .NET 6 version of project<br/>
            Integrated additional code from Jason Taylor's Clean sample to support
            MediatR commands<br/>
            Added first command: CreateStudentItemCommand with validation, handler and test<br/>
            Moved ValidationException from CU.Application.Common to CU.Application.Shared<br/>
		</td>
    </tr>
    <tr>
        <td>3/3/2022</td><td>bg</td>
		<td>
            Added SchoolRepository and related test<br/>
            Added SchoolViewDataRepository and related test<br/>
            Added ASP.NET Core 3.1 MVC web app - ContosoUniversity<br/>
            Switched to attribute routing<br/>
            Migrated CUControllerBase from layered app<br/>
            Added logging with NLog<br/>
            Added Departments controller and view<br/>
            Added Instructors and Students controllers and views<br/>
            Added Courses controller for MVC pages, and related views<br/>
            Enabled Blazor, added demo Blazor components<br/>
            Migrated ContosoUniversity.Components from layered app<br/>
            Added first MediatR query - GetDepartmentListItemsQuery
            with handler in CU.Application, and with integration test<br/>
            Dto model, Query in CU.Application.Shared<br/>
            Handler, AutoMapper mapping in CU.Application<br/>
            Added DependencyInjection to CU.Application<br/>
            Integration test GetDepartmentsTests.CanGetDepartmentsList()<br/>
            Wired up CU.Application to CU web app,
            use MediatR query for Departments Index MVC page<br/>
            Added MediatR query GetInstructorListItemsQuery,
            added integration test, wired up to Instructors Index page<br/>
            Incorporated Jason Taylor's PaginatedListHandler.
            Added MediatR query GetCourseListItemsWithPaginationQuery,
            handler and integration test<br/>
            Added Pager component to ContosoUniversity.Components<br/>
            Use Pager and MediatR query GetCourseListItemsWithPaginationQuery
            with Blazor CourseList<br/>
            Added initial Student MediatR queries and tests<br/>
            Added Students Blazor component, use it as default
            view on Students Index page<br/>
		</td>
    </tr>
    <tr>
        <td>3/2/2022</td><td>bg</td>
		<td>
            Started with CU.SharedKernel<br/>
            Added ContosoUniversity.Models - persistent
            object models - from ContosoUniversity_dnc31_MVC<br/>
            Wired up entity models to EntityBaseT<br/>
            Added CU.Application.Shared and CU.Application.Common<br/>
            Added CU.Application and CU.Infrastructure<br/>
            Migrated SchoolRepository and factory from layered app<br/>
            Added Infrastructure DependencyInjection<br/>
            Added CU.ApplicationIntegrationTests, first test:
            CanGetCoursesAsync - using ISchoolDbContext<br/>
		</td>
    </tr>
    <tr>
        <td></td><td></td>
		<td>
		</td>
    </tr>
</table>
