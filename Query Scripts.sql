CREATE OR ALTER  PROCEDURE dbo.GenerateRandomDates	
@StartDate DATE, 
@EndDate DATE, 
@Result DATE OUTPUT
AS
BEGIN
/*
TESTING: 
DECLARE @Result VARCHAR (50) 
EXEC  dbo.GenerateRandomDates  '2020-01-01', '2023-01-01', @Result OUT 
SELECT @Result

*/

SELECT @Result =  DATEADD(DAY, ABS(CHECKSUM(NEWID())) % (DATEDIFF(DAY, @StartDate, @EndDate) + 1), @StartDate) 

END 