
USE RR61
Go

-- ================================================
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (
        SELECT type_desc, type
        FROM sys.procedures WITH(NOLOCK)
        WHERE NAME = 'AddPaymentsExchange'
            AND type = 'P'
      )
     DROP PROCEDURE dbo.AddPaymentsExchange
GO
-- =============================================
-- Author:		<Martinez,Cesar>
-- Create date: <01/26/2021>
-- Description:	<Description: Insert the ExChange Rate per day review is the Country oriing and asoddate already exist then we can update teh exchange rater if  the decide to chamge the value same day >
-- We are trying to avoid to insert duplicates
-- =============================================
CREATE PROCEDURE dbo.AddPaymentsExchange (@Country char(3) ,@Origin char(5), @ftxousd decimal(28,18) = 0, @fxtoeur decimal(28,18) = 0, @AsofDate datetime, @Comments varchar(100) = '') 
AS
IF EXISTS (SELECT [Country],[Origin],[ftxousd],[fxtoeur],[AsofDate],[Comments]  from [dbo].[PaymentsExhange] WHERE
       [Country] = @Country and [Origin] = @Origin 
	   --and [ftxousd] = @ftxousd and [fxtoeur] = @fxtoeur 
	   and AsofDate = @AsofDate 
	   --and [Comments] = @Comments  
  )
	UPDATE [dbo].[PaymentsExhange] SET [Country] = @Country , [Origin] = @Origin , [ftxousd] = @ftxousd , [fxtoeur] = @fxtoeur , AsofDate = @AsofDate , [Comments] = @Comments 
	WHERE  [Country] = @Country and [Origin] = @Origin  and AsofDate = @AsofDate
ELSE 
	INSERT [dbo].[PaymentsExhange] ([Country],[Origin],[ftxousd],[fxtoeur],[AsofDate],[Comments]) VALUES (@Country,@Origin,@ftxousd,@fxtoeur,@AsofDate,@Comments)
GO