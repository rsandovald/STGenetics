USE [STGenetics]
GO
/****** Object:  StoredProcedure [dbo].[GenerateRandomStrings]    Script Date: 8/17/2023 9:08:44 PM ******/
DROP PROCEDURE [dbo].[GenerateRandomStrings]
GO
/****** Object:  StoredProcedure [dbo].[GenerateRandomInt]    Script Date: 8/17/2023 9:08:44 PM ******/
DROP PROCEDURE [dbo].[GenerateRandomInt]
GO
/****** Object:  StoredProcedure [dbo].[GenerateRandomDates]    Script Date: 8/17/2023 9:08:44 PM ******/
DROP PROCEDURE [dbo].[GenerateRandomDates]
GO
/****** Object:  StoredProcedure [dbo].[AnimalGet]    Script Date: 8/17/2023 9:08:44 PM ******/
DROP PROCEDURE [dbo].[AnimalGet]
GO
ALTER TABLE [dbo].[OrderDetails] DROP CONSTRAINT [FK_OrderDetails_Orders]
GO
ALTER TABLE [dbo].[OrderDetails] DROP CONSTRAINT [FK_OrderDetails_Animals]
GO
ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [DF_Orders_OrderId]
GO
ALTER TABLE [dbo].[OrderDetails] DROP CONSTRAINT [DF_OrderDetails_OrderDetailId]
GO
ALTER TABLE [dbo].[Animals] DROP CONSTRAINT [DF_Animals_AnimalId]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 8/17/2023 9:08:44 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Orders]') AND type in (N'U'))
DROP TABLE [dbo].[Orders]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 8/17/2023 9:08:44 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderDetails]') AND type in (N'U'))
DROP TABLE [dbo].[OrderDetails]
GO
/****** Object:  Table [dbo].[Animals]    Script Date: 8/17/2023 9:08:44 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Animals]') AND type in (N'U'))
DROP TABLE [dbo].[Animals]
GO
/****** Object:  Table [dbo].[Animals]    Script Date: 8/17/2023 9:08:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Animals](
	[AnimalId] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Breed] [varchar](50) NOT NULL,
	[BirthDate] [date] NOT NULL,
	[Sex] [varchar](1) NOT NULL,
	[Price] [numeric](18, 2) NOT NULL,
	[Status] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AnimalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 8/17/2023 9:08:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderDetailId] [uniqueidentifier] NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[AnimalId] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NOT NULL,
	[unitPrice] [decimal](18, 2) NOT NULL,
	[DiscountPercentage] [decimal](5, 2) NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 8/17/2023 9:08:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[CustomerId] [varchar](50) NOT NULL,
	[SubTotal] [decimal](18, 2) NOT NULL,
	[DiscountPercentage] [decimal](5, 2) NOT NULL,
	[Freight] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Animals] ADD  CONSTRAINT [DF_Animals_AnimalId]  DEFAULT (newid()) FOR [AnimalId]
GO
ALTER TABLE [dbo].[OrderDetails] ADD  CONSTRAINT [DF_OrderDetails_OrderDetailId]  DEFAULT (newid()) FOR [OrderDetailId]
GO
ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF_Orders_OrderId]  DEFAULT (newid()) FOR [OrderId]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Animals] FOREIGN KEY([AnimalId])
REFERENCES [dbo].[Animals] ([AnimalId])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Animals]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Orders] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Orders]
GO
/****** Object:  StoredProcedure [dbo].[AnimalGet]    Script Date: 8/17/2023 9:08:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--o Create an endpoint that filters animals by AnimalId or Name or Sex or Status (Active or Inactive). 


CREATE    PROCEDURE [dbo].[AnimalGet]
	@Name AS VARCHAR (50), 
	@Sex AS VARCHAR (1), 
	@Status AS SMALLINT 
AS
BEGIN 
/*
TESTING: 
dbo.AnimalGet 'Cody%', 'F', -1
*/ 
	DECLARE @includeActive AS SMALLINT = -1
	DECLARE @includeInactivo AS SMALLINT = -1

	IF (@Status = 1)
	BEGIN
		SET @includeActive = 1 
	END 

	IF (@Status = 0)
	BEGIN
		SET @includeInactivo = 0 
	END 

	IF (@Status = -1 ) -- It is no requerid filter by Status
	BEGIN
		SET @includeActive = 1 
		SET @includeInactivo = 0 
	END 

	SELECT [AnimalId]
		  ,[Name]
		  ,[Breed]
		  ,[BirthDate]
		  ,[Sex]
		  ,[Price]
		  ,[Status]
	FROM [dbo].[Animals]
	WHERE
	Name LIKE @Name
	AND Sex LIKE @Sex
	AND Status IN ( @includeActive, @includeInactivo)
END 
GO
/****** Object:  StoredProcedure [dbo].[GenerateRandomDates]    Script Date: 8/17/2023 9:08:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    PROCEDURE [dbo].[GenerateRandomDates]	
@StartDate DATE, 
@EndDate DATE, 
@Result DATE OUTPUT
AS
BEGIN
/*
TESTING: 
DECLARE @Result VARCHAR (50) 
EXEC  dbo.GenerateRandomDates  '2020-01-01', '2023-01-01' @Result OUT 
SELECT @Result

*/

SELECT @Result =  DATEADD(DAY, ABS(CHECKSUM(NEWID())) % (DATEDIFF(DAY, @StartDate, @EndDate) + 1), @StartDate) 

END 
GO
/****** Object:  StoredProcedure [dbo].[GenerateRandomInt]    Script Date: 8/17/2023 9:08:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE    PROCEDURE [dbo].[GenerateRandomInt]
@From INT, 
@Until INT, 
@Result INT OUT
AS
BEGIN 
/*
TESTING: 
DECLARE @Result INT 
EXEC  dbo.GenerateRandomInt 1, 2 , @Result OUT
SELECT @Result
*/ 


SELECT  @Result = ROUND(((@Until - @From) * RAND() + @From), 0)

END 
GO
/****** Object:  StoredProcedure [dbo].[GenerateRandomStrings]    Script Date: 8/17/2023 9:08:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    PROCEDURE [dbo].[GenerateRandomStrings]	
	@Type AS VARCHAR (20), 
	@Result AS VARCHAR (50) OUTPUT
AS
BEGIN
/*
TESTING: 
DECLARE @Result VARCHAR (50) 
EXEC  dbo.GenerateRandomStrings  'Names', @Result OUT 
SELECT @Result

*/ 

	DECLARE @Names VARCHAR(MAX) =  'Angus,Bessie,Brisket,Brownie,Buttercup,Calfy,Charolais,Cheesesteak,Chloe,Chops,Chuck,Clover,Cody,Daisy,Dexter,Dolly,Domino,Dorito,Duke,Edamame,Eggnog,Feta,Flapjack,Flossie,Floyd,Freckles,Gouda,Gravy,Guernsey,Hamlet,Hayley,Hazel,Heifer,Hershey,Holstein,Honey,Huckleberry,Jersey,Jolene,Juniper,Kabob,Ketchup,Ladybug,Larry,Lenny,Licorice,Luna,Macadamia,Marmalade,Maybelle,Meadow,Mocha,Muffin,Munchkin,Niblet,Nutmeg,Oatmeal,Olive,Oreo,Pancake,Pecan,Pepper,Pickles,Pippin,Pistachio,Popcorn,Pudding,Pumpkin,Queso,Radish,Reese,Rodeo,Rolo,Rosie,Ruby,Rye,Smores,Salsa,Sausage,Scones,Sheba,Sherbet,Snickers,Snickers,Snowball,Speckles,Sprout,Stew,Sunshine,Sweetie,Taco,TaterTot,Toffee,Tomato,Twix,Twizzler,Vanilla,Waffles,Whiskey,Yodel,Yolk,Zucchini'
    DECLARE @Breeds VARCHAR(MAX) = 'Angus,Hereford,Holstein,Charolais,Limousin,Brahman,Simmental,Highland,Galloway,Jersey,Guernsey,Shorthorn,Longhorn,Aberdeen,Ayrshire,Devon,Brangus,SantaGertrudis,BeltedGalloway,Wagyu'
	DECLARE @Words VARCHAR(MAX); 
	DECLARE @Numbers VARCHAR(MAX) = '1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,96,97,98,99,100'
    DECLARE @RandomName VARCHAR(50)
	
	IF @Type = 'Names' 
	BEGIN
		SET @Words = @Names

		SELECT TOP 1 @RandomName = Word + CAST(Number AS VARCHAR(2))
		FROM (
			SELECT value AS Word FROM STRING_SPLIT(@Words, ',')
		) WordsTable
		CROSS JOIN (
			SELECT value AS Number FROM STRING_SPLIT(@Numbers, ',')
		) NumbersTable
		ORDER BY NEWID()
	END
	ELSE
	BEGIN
		SET @Words  = @Breeds

		SELECT TOP 1 @RandomName = Word 
		FROM (
			SELECT value AS Word FROM STRING_SPLIT(@Words, ',')
		) WordsTable
		ORDER BY NEWID()
	END 
	
    SELECT @Result = @RandomName 
END




GO
