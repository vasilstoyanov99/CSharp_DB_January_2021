CREATE PROC usp_ExcludeFromSchool(@StudentId INT)
AS
	BEGIN
		IF (SELECT COUNT(*) FROM Students WHERE Id = @studentId) < 1
			THROW 50001, 'This school has no student with the provided id!', 1

		DELETE StudentsSubjects
		WHERE StudentId = @StudentId

		DELETE StudentsExams
		WHERE StudentId = @StudentId

		DELETE StudentsTeachers
		WHERE StudentId = @StudentId

		DELETE Students
		WHERE Id = @StudentId
	END
