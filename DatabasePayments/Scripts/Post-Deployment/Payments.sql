USE RR61
go

IF exists(select * from dbo.sysobjects where id = OBJECT_ID(N'[dbo].[Payments]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
drop table [dbo].[Payments]
go

CREATE TABLE [dbo].[Payments] (
    [Id]             INT   Identity(1,1)             NOT NULL,
    [IRN]            DECIMAL (28, 5) NULL,
    [FUN]            CHAR (10)        NULL,
    [FUNDS_ORIG_AMT] MONEY            NULL,
    [FUNDS_MOP]      NCHAR (10)       NULL,
    [AsofDate]           DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO