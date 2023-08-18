-- 3) Create a script (Insert / Update / Delete) for record in Animal table. 

CREATE OR ALTER PROCEDURE InsertAnimal
    @Name varchar(50),
    @Breed varchar(50),
    @BirthDate date,
    @Sex varchar(1),
    @Price numeric(18, 2),
    @Status bit
AS
BEGIN
/*
TESTING: 
EXEC InsertAnimal 'Max', 'Dog', '2020-01-15', 'M', 500.00, 1;
*/ 

    INSERT INTO [dbo].[Animals] ([AnimalId], [Name], [Breed], [BirthDate], [Sex], [Price], [Status])
    VALUES (NEWID(), @Name, @Breed, @BirthDate, @Sex, @Price, @Status);


END;

GO

CREATE OR ALTER PROCEDURE UpdateAnimal
    @AnimalId uniqueidentifier,
    @Name varchar(50),
    @Breed varchar(50),
    @BirthDate date,
    @Sex varchar(1),
    @Price numeric(18, 2),
    @Status bit
AS

BEGIN
/*TESTING 
DECLARE @AnimalIdToUpdate uniqueidentifier =  NEWID ()
EXEC UpdateAnimal @AnimalIdToUpdate, 'Updated Max', 'Updated Dog', '2022-01-15', 'M', 550.00, 1;
*/ 
    UPDATE [dbo].[Animals]
    SET [Name] = @Name,
        [Breed] = @Breed,
        [BirthDate] = @BirthDate,
        [Sex] = @Sex,
        [Price] = @Price,
        [Status] = @Status
    WHERE [AnimalId] = @AnimalId;
END;

GO 

CREATE OR ALTER PROCEDURE DeleteAnimal
    @AnimalId uniqueidentifier
AS
BEGIN
/*TESTING
DECLARE @AnimalIdToDelete uniqueidentifier = NEWID ()

EXEC DeleteAnimal @AnimalIdToDelete;

*/
    DELETE FROM [dbo].[Animals]
    WHERE [AnimalId] = @AnimalId;
END;