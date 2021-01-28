
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



select 'FRY15' as ReportID, Risk.BoxId,  'NYB' as BranchID, PaymentsExchnge.Amount,'outgoing payments' as GL, '10/01/2019' as StartDate,  '10/10/2019' as EndDate,  Risk.Description as Comments 
from (
Select Payments.FUN, sum((ExchangeRate.ExRate * Payments.FUNDS_ORIG_AMT)) as Amount  from (
select IRN, FUN, FUNDS_ORIG_AMT, FUNDS_MOP, AsofDate from RR61.dbo.Payments where AsofDate between '10/01/2019' and '10/10/2019' ) as Payments inner join
(select avg(ftxousd) as ExRate, Origin from RR61.dbo.PaymentsExhange  where Country = 'NY' and AsofDate between '11/16/2020' and '11/20/2020'  group by Origin) as ExchangeRate on Payments.FUN = ExchangeRate.Origin
Group by Payments.FUN ) as PaymentsExchnge inner join (
select DISTINCT BoxId,SUBSTRING([Description],Charindex('(',[Description])+1,3) as Country, [Description] from  [RR61].dbo.vBoxDetails where  BoxId in ('RISKM377','RISKM378','RISKM379','RISKM380','RISKM381','RISKM382','RISKM383','RISKM384','RISKM385','RISKM386','RISKY835','RISKM387','RISKM388','RISKY836','RISKY837','RISKM389','RISKM436','RISKKW46','RISKKW48','RISKKW50','RISKKW52')
and [Description] like  '%[\((*?)\)]%'
)as Risk on PaymentsExchnge.FUN = Risk.Country Where Risk.Description != 'Mexican pesos (MXN)'
UNION 
select 'FRY15' as ReportID, BoxId,  'NYB' as BranchID, 0 as Amount, '' as GL,  '10/01/2019' as StartDate,  '10/10/2019' as EndDate , [Description] as Comments from  [RR61].dbo.vBoxDetails where  BoxId in ('RISKM436','RISKKW46','RISKKW48','RISKKW50','RISKKW52')
Order by Amount desc

END
GO