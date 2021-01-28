USE RR61
go

IF exists(select * from dbo.sysobjects where id = OBJECT_ID(N'[dbo].[PaymentsResults]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
drop table [dbo].[PaymentsResults]
go

CREATE TABLE [dbo].[PaymentsResults] (
    [Id]             BIGINT     Identity(1,1)           NOT NULL,
	[ReportId]             VARCHAR(50) NULL,
	[BoxID]            VARCHAR(50) NULL,
	[BranchID]          char(5) NULL,
	[Amount]            DECIMAL (28, 20) NULL,
    [GL]            VARCHAR (50) NULL,    	
    [StartDate]           DATETIME         NULL,
	[EndDate]           DATETIME         NULL,
	[Comments]            VARCHAR(50) NULL,		
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO




