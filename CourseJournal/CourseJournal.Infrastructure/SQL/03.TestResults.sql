CREATE TABLE [TestResults] (
[Id] INT IDENTITY(1,1) PRIMARY KEY,
[TestId] INT NOT NULL,
[TestName] VARCHAR(255) NOT NULL,
[TestDate] DATETIME2 NOT NULL,
[CourseId] INT FOREIGN KEY ([CourseId]) REFERENCES [Courses]([Id])
);

CREATE TABLE [StudentsResults] (
[Id] INT IDENTITY(1,1) PRIMARY KEY,
[StudentId] INT FOREIGN KEY ([StudentId]) REFERENCES [Students]([Id]),
[TestId] INT FOREIGN KEY ([TestId]) REFERENCES [TestResults]([Id]),
[StudentResult] INT NOT NULL,
);