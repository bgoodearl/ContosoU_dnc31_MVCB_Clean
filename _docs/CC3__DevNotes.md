# Contoso University Clean 3.1 - Dev Notes

<table>
    <tr>
        <th>Date</th><th>Dev</th>
		<th>Notes</th>
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
