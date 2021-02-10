CREATE FUNCTION udf_ExamGradesToUpdate(@studentId INT, @grade DECIMAL(12, 2))
RETURNS NVARCHAR(MAX)
AS
	BEGIN
		IF (SELECT COUNT(*) FROM Students WHERE Id = @studentId) < 1
			RETURN 'The student with provided id does not exist in the school!'
		IF @grade > 6.00
			RETURN 'Grade cannot be above 6.00!'
		DECLARE @gradesCount INT
		DECLARE @studentName NVARCHAR(30)
		DECLARE @upperGrade DECIMAL = @grade + 0.50
		SET @studentName = (SELECT FirstName
								FROM Students
							WHERE Id = @studentId)
		SET @gradesCount = (SELECT COUNT(Grade)
								FROM StudentsSubjects
							WHERE StudentId = 1 AND Grade BETWEEN @grade AND @upperGrade)
		RETURN CONCAT('You have to update ', 
					  @gradesCount, 
					  ' grades for the student ', 
					  @studentName)
	END