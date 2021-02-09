SELECT TOP(10) 
	Result.[First Name], 
	Result.[Last Name], 
	FORMAT(Result.AvgGrade, 'N')
FROM (	SELECT s.FirstName AS 'First Name', 
			   s.LastName AS 'Last Name', 
			   AVG(se.Grade) AS 'AvgGrade'
	     FROM Students AS s
			JOIN StudentsExams AS se ON s.Id = se.StudentId
		GROUP BY s.FirstName, s.LastName) AS [Result]
ORDER BY AvgGrade DESC, [First Name] ASC, [Last Name] ASC