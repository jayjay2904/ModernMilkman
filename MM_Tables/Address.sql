CREATE TABLE [dbo].[Address]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Address1] VARCHAR(80) NOT NULL, 
    [Address2] VARCHAR(80) NULL, 
    [Town] VARCHAR(50) NOT NULL, 
    [County] VARCHAR(50) NULL, 
    [PostCode] VARCHAR(10) NOT NULL, 
    [Country] VARCHAR(50) NULL , 
    [CustomerID] INT NOT NULL, 
    [Primary_Address] BIT NOT NULL 
)
