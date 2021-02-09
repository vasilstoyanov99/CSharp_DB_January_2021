CREATE TABLE Students
(
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(30) NOT NULL,
	MiddleName NVARCHAR(25),
	LastName NVARCHAR(30) NOT NULL,
	Age INT CHECK (Age >= 5 AND Age <= 100),
	[Address] NVARCHAR(50),
	Phone NVARCHAR(10),
)

CREATE TABLE Subjects
(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(20) NOT NULL,
	Lessons INT CHECK (Lessons > 0) NOT NULL
)

CREATE TABLE StudentsSubjects
(
	Id INT PRIMARY KEY IDENTITY,
	StudentId INT REFERENCES Students(Id),
	SubjectId INT REFERENCES Subjects(Id),
	Grade DECIMAL(12, 2) CHECK (Grade >= 2 AND Grade <= 6) NOT NULL
)

CREATE TABLE Exams
(
	Id INT PRIMARY KEY IDENTITY,
	[Date] DATETIME,
	SubjectId INT REFERENCES Subjects(Id)
)

CREATE TABLE StudentsExams
(
	StudentId INT NOT NULL,
	ExamId INT NOT NULL,
	Grade DECIMAL(12, 2) CHECK (Grade >= 2 AND Grade <= 6) NOT NULL,

	CONSTRAINT PK_StudentsExams PRIMARY KEY (StudentId, ExamId),
	CONSTRAINT FK_StudentsExams_Students FOREIGN KEY (StudentId) REFERENCES Students(Id),
	CONSTRAINT FK_StudentsExams_Exams FOREIGN KEY (ExamId) REFERENCES Exams(Id)
)

CREATE TABLE Teachers
(
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(20) NOT NULL,
	LastName NVARCHAR(20) NOT NULL,
	[Address] NVARCHAR(20),
	Phone NVARCHAR(10),
	SubjectId INT REFERENCES Subjects(Id)
)

CREATE TABLE StudentsTeachers
(
	StudentId INT NOT NULL,
	TeacherId INT NOT NULL,

	CONSTRAINT PK_StudentsTeachers PRIMARY KEY (StudentId, TeacherId),
	CONSTRAINT FK_StudentsTeachers_Students FOREIGN KEY (StudentId) REFERENCES Students(Id),
	CONSTRAINT FK_StudentsTeachers_Teachers FOREIGN KEY (TeacherId) REFERENCES Teachers(Id)
)