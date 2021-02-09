SELECT s.FirstName, s.LastName, COUNT(s.FirstName) AS 'TeachersCount'
FROM Students AS s
	JOIN StudentsTeachers AS st ON s.Id = st.StudentId
	JOIN Teachers AS t ON t.Id = st.TeacherId
GROUP BY s.FirstName, s.LastName