--5) Create a script to list animals older than 2 years and female, sorted by name.

SELECT [AnimalId]
      ,[Name]
      ,[Breed]
      ,[BirthDate]
      ,[Sex]
      ,[Price]
      ,[Status]
  FROM [STGenetics].[dbo].[Animals]
  WHERE
  DATEDIFF (YY, BirthDate, GETDATE ()) > 2 
  AND Sex = 'F'
  ORDER BY 
  Name
	
--6) Create a script to list the quantity of animals by sex. 

SELECT [Sex]
      ,COUNT (*)
  FROM [STGenetics].[dbo].[Animals]
  GROUP BY 
  SEX