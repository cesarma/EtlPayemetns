USE RR61
go

IF exists(select * from dbo.sysobjects where id = OBJECT_ID(N'[dbo].[PaymentsExhange]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
drop table [dbo].[PaymentsExhange]
go

CREATE TABLE [dbo].[PaymentsExhange] (
    [Id]             INT     Identity(1,1)           NOT NULL,
	[Country]            VARCHAR(50) NULL,
	[Origin]            CHAR(50) NULL,
    [ftxousd]            DECIMAL (28, 5) NULL,
    [fxtoeur]            DECIMAL (28, 5) NULL,
    [AsofDate]           DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO