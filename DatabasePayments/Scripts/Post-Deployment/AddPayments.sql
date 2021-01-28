
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
        WHERE NAME = 'AddPayments'
            AND type = 'P'
      )
     DROP PROCEDURE dbo.AddPayments
GO
-- =============================================
-- Author:		<Martinez,Cesar>
-- Create date: <01/26/2021>
-- Description:	<Description: Insert the Payments nge Rate per day review is the Country oriing and asoddate already exist then we can update teh exchange rater if  the decide to chamge the value same day >
-- We are trying to avoid to insert duplicates
-- =============================================
CREATE PROCEDURE dbo.AddPayments (@IRN varchar(50) ,@FUN varchar(10)='', @FUNDS_ORIG_AMT money = null, @FUNDS_MOP varchar(20) = '', @AsofDate datetime =null) 
AS
IF EXISTS (SELECT [IRN],[FUN],[FUNDS_MOP],[AsofDate]  from [dbo].[Payments] WHERE         
       [IRN] = @IRN and [FUN] = @FUN
	   and [FUNDS_MOP] = @FUNDS_MOP
	    and AsofDate = @AsofDate 	   
  )
	UPDATE [dbo].[Payments] SET [FUNDS_ORIG_AMT] = @FUNDS_ORIG_AMT , [FUNDS_MOP] = @FUNDS_MOP
	WHERE  [IRN] = @IRN and [FUN] = @FUN and @FUNDS_MOP = [FUNDS_MOP]  and @AsofDate = [AsofDate]
ELSE 
	INSERT [dbo].[Payments] ([IRN],[FUN], [FUNDS_ORIG_AMT],[FUNDS_MOP],[AsofDate]) VALUES (@IRN,@FUN,@FUNDS_ORIG_AMT,@FUNDS_MOP,@AsofDate)
GO