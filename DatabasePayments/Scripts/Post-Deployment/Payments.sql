USE RR61
go

IF exists(select * from dbo.sysobjects where id = OBJECT_ID(N'[dbo].[Payments]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
drop table [dbo].[Payments]
go

CREATE TABLE [dbo].[Payments] (
    [Id]             BIGINT   Identity(1,1)             NOT NULL,
    [IRN]            VARCHAR(50)  NULL,
    [FUN]            varchar (10)        NULL,
    [FUNDS_ORIG_AMT] MONEY            NULL,
    [FUNDS_MOP]      varchar (20)       NULL,
    [AsofDate]           DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO