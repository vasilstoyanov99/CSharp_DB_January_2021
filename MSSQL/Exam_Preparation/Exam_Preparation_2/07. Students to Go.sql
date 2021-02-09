SELECT FirstName + ' ' + LastName AS 'Full Name'
FROM Students AS s
	LEFT JOIN StudentsExams AS se ON s.Id = se.StudentId
WHERE se.StudentId IS NULL
ORDER BY [Full Name] ASC