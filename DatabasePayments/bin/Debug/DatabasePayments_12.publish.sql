﻿/*
Deployment script for RR61

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "RR61"
:setvar DefaultFilePrefix "RR61"
:setvar DefaultDataPath "M:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\Data\"
:setvar DefaultLogPath "L:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\Data\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

--:r .\Reporter\payments\ETLPaymentsProcess\DatabasePayments\dbo\Tables\Payments.sql
USE RR61
GO

IF exists(select * from dbo.sysobjects where id = OBJECT_ID(N'[dbo].[Payments]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
drop table [dbo].[Payments]
GO

CREATE TABLE [dbo].[Payments] (
    [Id]             INT   Identity(1,1)             NOT NULL,
    [IRN]            DECIMAL (28, 5) NULL,
    [FUN]            varchar (10)        NULL,
    [FUNDS_ORIG_AMT] MONEY            NULL,
    [FUNDS_MOP]      varchar (20)       NULL,
    [AsofDate]           DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
USE RR61
GO

IF exists(select * from dbo.sysobjects where id = OBJECT_ID(N'[dbo].[PaymentsExhange]') and OBJECTPROPERTY(id, N'IsUserTable')=1)
drop table [dbo].[PaymentsExhange]
GO

CREATE TABLE [dbo].[PaymentsExhange] (
    [Id]             INT     Identity(1,1)           NOT NULL,
	[Country]            VARCHAR(50) NULL,
	[Origin]            varchar(50) NULL,
    [ftxousd]            DECIMAL (28, 20) NULL,
    [fxtoeur]            DECIMAL (28, 20) NULL,
    [AsofDate]           DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

GO
PRINT N'Update complete.';


GO
