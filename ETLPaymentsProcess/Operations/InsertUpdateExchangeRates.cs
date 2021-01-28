﻿using Rhino.Etl.Core;
using Rhino.Etl.Core.ConventionOperations;
using System.Configuration;

namespace ETLPaymentsProcess.Operations
{
    public class InsertUpdateExchangeRates : ConventionInputCommandOperation
    {
        public InsertUpdateExchangeRates(ConnectionStringSettings csSettings, Row _params) : base(csSettings)
        {
            //Command = @"SELECT 'FRY15' as ReportID, BoxId, 'NYB' as BranchID, SUM( ((Funds_ORIG_AMT/1) * (1/ftxousd)))  as Amount, 'outgoing payments' as GL, [Description] as Comments, AsofDate as StartDate,AsofDate as EndDate ,  FUN FROM [RR61].[dbo].[Payments]  as Payments inner join (select Country, Origin, ftxousd from [RR61].[dbo].[PaymentsExhange]   where AsofDate = '11/20/2020'and Country =  'NY') as PaymentsEX on Payments.FUN = PaymentsEX.Origin  inner join (select DISTINCT BoxId,SUBSTRING([Description],Charindex('(',[Description])+1,3) as Country, [Description] from  [RR61].dbo.vBoxDetails where  BoxId in ('RISKM377','RISKM378','RISKM379','RISKM380','RISKM381','RISKM382','RISKM383','RISKM384','RISKM385','RISKM386','RISKY835','RISKM387','RISKM388','RISKY836','RISKY837','RISKM389','RISKM436','RISKKW46','RISKKW48','RISKKW50','RISKKW52')and [Description] like  '%[\((*?)\)]%') as Risk on Payments.FUN = Risk.Country where Payments.[AsofDate] =  '12/23/2020'Group by FUN , BoxId,[Description],AsofDate Order by FUN desc;";
            var comandito = @"            
            EXEC	[dbo].[AddPaymentsExchange]
                    @Country =N'" + _params["Country"] + @"',
                    @Origin  =N'" + _params["Origin"] + @"',
                    @ftxousd =" + _params["ftxousd"] + @",
                    @fxtoeur =NULL,@AsofDate  ='" + _params["AsofDate"] + @"',
                    @Comments  = NULL";

            Command = @"            
            EXEC	[dbo].[AddPaymentsExchange]
                    @Country =N'" + _params["Country"] + @"',
                    @Origin  =N'" + _params["Origin"] + @"',
                    @ftxousd =" + _params["ftxousd"] + @",
                    @fxtoeur =NULL,@AsofDate  ='" + _params["AsofDate"] + @"',
                    @Comments  = NULL";
            Timeout = 1000000;
        }        
    }
}


 