USE MyWork;

-- если таблица People не существует
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Employees')

BEGIN
	-- создать таблицу Employees
	CREATE TABLE Employees
	(
		Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
		FirstName NVARCHAR(20) NOT NULL,
		LastName NVARCHAR(20) NOT NULL,
		Age INT NOT NULL
	);
END

ELSE
	-- Очистить таблицу Employees
	DELETE FROM Employees