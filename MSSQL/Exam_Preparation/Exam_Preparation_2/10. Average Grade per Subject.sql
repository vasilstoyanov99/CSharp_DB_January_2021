SELECT [Result].[Name], [Result].AverageGrade 
FROM (	SELECT s.[Name] AS 'Name', AVG(ss.Grade) AS 'AverageGrade', s.Id AS 'Id'
		 FROM Subjects AS s
			JOIN StudentsSubjects AS ss ON s.Id = ss.SubjectId
		GROUP BY s.[Name], s.Id) AS [Result]
ORDER BY [Result].Id