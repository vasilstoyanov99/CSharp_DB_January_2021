DELETE StudentsTeachers
WHERE TeacherId IN ((SELECT Id FROM Teachers WHERE Phone LIKE '%72%'))

UPDATE Teachers
SET SubjectId = NULL
WHERE Phone LIKE '%72%'

DELETE Teachers
WHERE Phone LIKE '%72%'