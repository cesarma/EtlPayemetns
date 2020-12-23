CREATE TABLE [dbo].[Payments] (
    [Id]             INT   Identity(1,1)          NOT NULL,
    [IRN]            DECIMAL (28, 5) NULL,
    [FUN]            CHAR (10)       NULL,
    [FUNDS_ORIG_AMT] MONEY           NULL,
    [FUNDS_MOP]      NCHAR (10)      NULL,
    [Date]           DATETIME        NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

