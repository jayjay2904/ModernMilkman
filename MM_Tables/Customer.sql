CREATE TABLE [dbo].[Table1]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Title] VARCHAR(20) NOT NULL, 
    [Forename] VARCHAR(50) NOT NULL, 
    [Surname] NCHAR(50) NOT NULL, 
    [Email_Address] VARCHAR(75) NOT NULL, 
    [Mobile_No] VARCHAR(15) NOT NULL, 
    [Active] BIT NOT NULL
)
