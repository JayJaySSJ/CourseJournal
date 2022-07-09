CREATE TABLE [HomeworksResults] (
[Id] INT IDENTITY(1,1) PRIMARY KEY,
[NameHomework] VARCHAR(255) NOT NULL,
[ReturnDate] DATETIME2 NOT NULL,
[Result] DECIMAL,
[StudentId] INT FOREIGN KEY([StudentId]) REFERENCES [Students]([Id]),
[CourseId] INT FOREIGN KEY([CourseId]) REFERENCES [Courses]([Id])
);

