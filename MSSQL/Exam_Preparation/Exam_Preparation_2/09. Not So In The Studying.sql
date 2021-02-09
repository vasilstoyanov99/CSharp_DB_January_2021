SELECT FirstName + ' ' + ISNULL(MiddleName + ' ', '') + LastName AS 'Full Name'
FROM Students AS s
	LEFT JOIN StudentsSubjects AS ss ON s.Id = ss.StudentId
WHERE ss.SubjectId IS NULL
ORDER BY [Full Name]