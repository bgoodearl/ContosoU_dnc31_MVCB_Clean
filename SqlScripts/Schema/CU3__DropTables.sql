USE ContosoU_c31_dev2;
GO

Declare   @DoClean bit;
--Declare @DoClean bit=1;

--exec sp_help 'Enrollment';

If ISNULL(@DoClean,0) != 1 Begin
	RAISERROR('Run again with @DoClean=1 to drop tables', 16, 1);
End Else Begin
	Print '';

	IF OBJECT_ID(N'[CourseInstructor]') IS NOT NULL BEGIN
		DROP TABLE dbo.CourseInstructor;
		Print 'Dropped table CourseInstructor';
	END ELSE BEGIN
		Print 'Table CourseInstructor does not exist';
	END;

	IF OBJECT_ID(N'[Enrollment]') IS NOT NULL BEGIN
		DROP TABLE dbo.Enrollment;
		Print 'Dropped table Enrollment';
	END ELSE BEGIN
		Print 'Table Enrollment does not exist';
	END;

	IF OBJECT_ID(N'[OfficeAssignment]') IS NOT NULL BEGIN
		DROP TABLE dbo.OfficeAssignment;
		Print 'Dropped table OfficeAssignment';
	END ELSE BEGIN
		Print 'Table OfficeAssignment does not exist';
	END;

	IF OBJECT_ID(N'[Course]') IS NOT NULL BEGIN
		DROP TABLE dbo.Course;
		Print 'Dropped table Course';
	END ELSE BEGIN
		Print 'Table Course does not exist';
	END;

	IF OBJECT_ID(N'[Department]') IS NOT NULL BEGIN
		DROP TABLE dbo.Department;
		Print 'Dropped table Department';
	END ELSE BEGIN
		Print 'Table Department does not exist';
	END;

	IF OBJECT_ID(N'[Instructor]') IS NOT NULL BEGIN
		DROP TABLE dbo.Instructor;
		Print 'Dropped table Instructor';
	END ELSE BEGIN
		Print 'Table Instructor does not exist';
	END;

	IF OBJECT_ID(N'[Student]') IS NOT NULL BEGIN
		DROP TABLE dbo.Student;
		Print 'Dropped table Student';
	END ELSE BEGIN
		Print 'Table Student does not exist';
	END;

	IF OBJECT_ID(N'[__MigrationHistory]') IS NOT NULL BEGIN
		DROP TABLE dbo.__MigrationHistory;
		Print 'Dropped table __MigrationHistory';
	END ELSE BEGIN
		Print 'Table __MigrationHistory does not exist';
	END;

End;

GO
