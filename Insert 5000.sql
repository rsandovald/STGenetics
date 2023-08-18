--AVERAGE EXECUTION TIME 2.5 MINUTES

DECLARE @Name AS  varchar(50)
DECLARE @Breed AS  varchar(50)
DECLARE @BirthDate AS  date
DECLARE @Sex varchar(1)
DECLARE @Price AS  numeric(18,2)
DECLARE @Status AS  BIT
DECLARE @RegisterCount INT = 1 
DECLARE @RegisterTotal  INT = 5000
DECLARE @BirthDatesFrom DATE  = '2020-01-01'
DECLARE @BirthDatesUntil DATE 
DECLARE @TemporalInt INT

SET @BirthDatesUntil = GETDATE ()

WHILE (@RegisterCount <= @RegisterTotal)
BEGIN

	EXEC  dbo.GenerateRandomStrings  'Names', @Name OUT 
	EXEC  dbo.GenerateRandomStrings  'Breed', @Breed OUT
	EXEC  dbo.GenerateRandomDates @BirthDatesFrom, @BirthDatesUntil, @BirthDate OUT 
	EXEC  dbo.GenerateRandomInt 1, 2 , @TemporalInt OUT
	 
	IF (@TemporalInt= 1) 
	BEGIN
		SET  @Sex = 'F'
	END
	ELSE 
	BEGIN 
		SET @Sex = 'M'
	END 

	EXEC  dbo.GenerateRandomInt 100, 1000, @Price OUT
	EXEC  dbo.GenerateRandomInt 0, 1 , @Status OUT


	INSERT INTO [dbo].[Animals]
			   ([Name]
			   ,[Breed]
			   ,[BirthDate]
			   ,[Sex]
			   ,[Price]
			   ,[Status])
		 VALUES
			   (@Name
			   ,@Breed
			   ,@BirthDate
			   ,@Sex
			   ,@Price
			   ,@Status)

	SET @RegisterCount = @RegisterCount + 1 

END 

PRINT 'DONE'
