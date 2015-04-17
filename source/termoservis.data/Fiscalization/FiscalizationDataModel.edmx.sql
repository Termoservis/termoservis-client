
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 07/16/2013 00:45:33
-- Generated from EDMX file: C:\Users\Aleksandar\Dropbox\Documents\Projects - Software\Project - Termoservis\Termoservis.Data\Fiscalization\FiscalizationDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TermoservisFiscalization];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ItemEntityAccountEntity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ItemEntitySet] DROP CONSTRAINT [FK_ItemEntityAccountEntity];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountEntityCustomerEntity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountEntitySet] DROP CONSTRAINT [FK_AccountEntityCustomerEntity];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ItemEntitySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ItemEntitySet];
GO
IF OBJECT_ID(N'[dbo].[AccountEntitySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccountEntitySet];
GO
IF OBJECT_ID(N'[dbo].[CustomerEntitySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerEntitySet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ItemEntitySet'
CREATE TABLE [dbo].[ItemEntitySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Type] int  NOT NULL,
    [Amount] float  NOT NULL,
    [Price] float  NOT NULL,
    [Discount] float  NOT NULL,
    [DiscountAmount] float  NOT NULL,
    [TotalPrice] float  NOT NULL,
    [TotalPriceValue] float  NOT NULL,
    [AccountEntity_Id] int  NOT NULL
);
GO

-- Creating table 'AccountEntitySet'
CREATE TABLE [dbo].[AccountEntitySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StoreName] nvarchar(max)  NOT NULL,
    [TreasuryName] nvarchar(max)  NOT NULL,
    [Number] int  NOT NULL,
    [Date] datetime  NOT NULL,
    [CancellationAccountNumber] int  NOT NULL,
    [CancellationAccountDate] datetime  NULL,
    [PaymentMethod] nvarchar(max)  NOT NULL,
    [CustomerEntity_Id] int  NOT NULL
);
GO

-- Creating table 'CustomerEntitySet'
CREATE TABLE [dbo].[CustomerEntitySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [PIN] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [ZIP] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ItemEntitySet'
ALTER TABLE [dbo].[ItemEntitySet]
ADD CONSTRAINT [PK_ItemEntitySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AccountEntitySet'
ALTER TABLE [dbo].[AccountEntitySet]
ADD CONSTRAINT [PK_AccountEntitySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CustomerEntitySet'
ALTER TABLE [dbo].[CustomerEntitySet]
ADD CONSTRAINT [PK_CustomerEntitySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AccountEntity_Id] in table 'ItemEntitySet'
ALTER TABLE [dbo].[ItemEntitySet]
ADD CONSTRAINT [FK_ItemEntityAccountEntity]
    FOREIGN KEY ([AccountEntity_Id])
    REFERENCES [dbo].[AccountEntitySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemEntityAccountEntity'
CREATE INDEX [IX_FK_ItemEntityAccountEntity]
ON [dbo].[ItemEntitySet]
    ([AccountEntity_Id]);
GO

-- Creating foreign key on [CustomerEntity_Id] in table 'AccountEntitySet'
ALTER TABLE [dbo].[AccountEntitySet]
ADD CONSTRAINT [FK_AccountEntityCustomerEntity]
    FOREIGN KEY ([CustomerEntity_Id])
    REFERENCES [dbo].[CustomerEntitySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountEntityCustomerEntity'
CREATE INDEX [IX_FK_AccountEntityCustomerEntity]
ON [dbo].[AccountEntitySet]
    ([CustomerEntity_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------