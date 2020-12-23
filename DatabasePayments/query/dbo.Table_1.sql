USE RR61
go

IF exists(select * from dbo.sysobjects where id = OBJECT_ID(N'[dbo].[PaymentsExhange]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
drop table [dbo].[PaymentsExhange]
go

CREATE TABLE [dbo].[PaymentsExhange] (
    [Id]             INT              NOT NULL,
	[Country]            VARCHAR(50) NULL,
	[Origin]            CHAR(50) NULL,
    [ftxousd]            DECIMAL (28, 18) NULL,
    [fxtoeur]            CHAR (10)        NULL,
    [Date]           DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO