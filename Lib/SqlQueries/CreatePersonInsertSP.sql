USE [MyWork];

IF NOT EXISTS (SELECT * FROM sys.objects
			   WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.spPersonInsert'))

EXEC ('
CREATE PROCEDURE [dbo].[spPersonInsert]
AS
BEGIN
    SET NOCOUNT ON;
END;
')

EXEC ('
ALTER PROCEDURE [dbo].[spPersonInsert]
	@FirstName nvarchar(20),
	@LastName nvarchar(20),
	@Age int
AS
BEGIN

	SET NOCOUNT ON;

	INSERT INTO dbo.Employees (FirstName, LastName, Age)
	VALUES (@FirstName, @LastName, @Age)
END
')