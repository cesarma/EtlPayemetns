using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETLPaymentsProcess.Operations;
using ETLPaymentsProcess.Models;
using Rhino.Etl.Core;

namespace ETL_aymentsProcess.Tests
{
    [TestClass]
    [DeploymentItem(@".\SampleData\payments.txt", "SampleData")]
    [DeploymentItem(@".\SampleData\cd_sensibilidades.normalized_fx_20201120.csv", "SampleData")]
    public class PaymentsReadRecordsTest : BaseTestClass
    {
        [TestMethod]
        public void CanReadPaymentFiles()
        {
            var _Records = TestOperation(
                new FlatFileRead<EgiftInfo>(@".\SampleData\payments.txt")
                );

            AreSameRows(new[] {
                Row.FromObject(new EgiftInfo{  IRN="202011160001393", FUN="USD", FUNDS_ORIG_AMT=1800000000, FUNDS_MOP="2", AsofDate = DateTime.Parse("2020/12/23")}),
                Row.FromObject(new EgiftInfo{  IRN="202011160002041", FUN="USD", FUNDS_ORIG_AMT=1008750, FUNDS_MOP="2", AsofDate = DateTime.Parse("2020/12/23")}),
                Row.FromObject(new EgiftInfo{  IRN="202011160001908", FUN="USD", FUNDS_ORIG_AMT=Decimal.Parse(".01"), FUNDS_MOP="8", AsofDate = DateTime.Parse("2020/12/23")})
            }, _Records);
        }



        [TestMethod]
        public void CanReadExchangeFiles()
        {
            var _RecordsExchangeRate = TestOperation(
                new FlatFileRead<Exchangerate>(@".\SampleData\cd_sensibilidades.normalized_fx_20201120.csv")
                );

            AreSameRows(new[] {
                Row.FromObject(new Exchangerate{   Country="SB", Origin="USD", ftxousd=1, fxtoeur=Decimal.Parse("0.843379991710680"), AsofDate = DateTime.Parse("2020/11/20")}),
                Row.FromObject(new Exchangerate{   Country="SB", Origin="CSU", ftxousd=Decimal.Parse("0.11605673"), fxtoeur=Decimal.Parse("0.097879924056360"), AsofDate = DateTime.Parse("2020/11/20")}),
                Row.FromObject(new Exchangerate{   Country="SB", Origin="BRT", ftxousd=1, fxtoeur=Decimal.Parse("0.843379991710680"), AsofDate = DateTime.Parse("2020/11/20")})

            }, _RecordsExchangeRate);   

        }


    }
}
