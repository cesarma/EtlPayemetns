
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
        WHERE NAME = 'GetExchangePaymentsbyDateandBranchName'
            AND type = 'P'
      )
     DROP PROCEDURE dbo.GetExchangePaymentsbyDateandBranchName
GO
-- =============================================
-- Author:		<Martinez,Cesar>
-- Create date: <12/23/2020>
-- Description:	<Description:After impor the files from egift and exchange rate the below sp going to build que qquery up and generate the output file>
-- =============================================
CREATE PROCEDURE dbo.GetExchangePaymentsbyDateandBranchName 
	-- Add the parameters for the stored procedure here
	 @Branch varchar(50) = 'NY', 
	 @AsOfDateEgiftperDay DATETIME,  --'12/23/2020'
	 @AsOfDatePaymentExchangeperDay DATETIME   -- = '11/20/2020'
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Select statements for procedure here	
	--add Risk to the quey 
--input  Branch ESp,NY, MI , asofdate for payemntexchange , asofdate for payments or egift load file

SELECT 'FRY15' as ReportID, BoxId, 'NYB' as BranchID, SUM( ((Funds_ORIG_AMT/1) * (1/ftxousd)))  as Amount,
'outgoing payments' as GL,    AsofDate as StartDate,AsofDate as EndDate , [Description] as Comments --, FUN 
FROM [RR61].[dbo].[Payments]  as Payments inner join 
(
select Country, Origin, ftxousd from [RR61].[dbo].[PaymentsExhange]   where AsofDate = @AsOfDateEgiftperDay
and Country =  @Branch
) as PaymentsEX on Payments.FUN = PaymentsEX.Origin  inner join (
select DISTINCT BoxId,SUBSTRING([Description],Charindex('(',[Description])+1,3) as Country, [Description] from  [RR61].dbo.vBoxDetails where  BoxId in ('RISKM377','RISKM378','RISKM379','RISKM380','RISKM381','RISKM382','RISKM383','RISKM384','RISKM385','RISKM386','RISKY835','RISKM387','RISKM388','RISKY836','RISKY837','RISKM389','RISKM436','RISKKW46','RISKKW48','RISKKW50','RISKKW52')
and [Description] like  '%[\((*?)\)]%'
) as Risk on Payments.FUN = Risk.Country where Payments.[AsofDate] =  @AsOfDatePaymentExchangeperDay
Group by FUN , BoxId,[Description],AsofDate Order by FUN desc


END
GO