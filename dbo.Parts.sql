CREATE TABLE [dbo].[Parts] (
    [PartNumber]  VARCHAR (50)  NOT NULL,
    [Description] VARCHAR (MAX) NOT NULL,
    [EAN]         DECIMAL (13)  NOT NULL,
    [FreeStock]   INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([PartNumber] ASC)
);