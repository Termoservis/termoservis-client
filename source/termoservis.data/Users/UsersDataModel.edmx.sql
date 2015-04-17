
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/12/2013 18:54:49
-- Generated from EDMX file: C:\Users\Aleksandar\Dropbox\Documents\Projects - Software\Project - Termoservis\Termoservis.Data\Users\UsersDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TermoservisUsers];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ContactInfoUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContactInfoSet] DROP CONSTRAINT [FK_ContactInfoUser];
GO
IF OBJECT_ID(N'[dbo].[FK_AddressContactInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContactInfoSet] DROP CONSTRAINT [FK_AddressContactInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_PlaceAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AddressSet] DROP CONSTRAINT [FK_PlaceAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_TelephoneContactInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TelephoneSet] DROP CONSTRAINT [FK_TelephoneContactInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_TelephoneCounty]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TelephoneSet] DROP CONSTRAINT [FK_TelephoneCounty];
GO
IF OBJECT_ID(N'[dbo].[FK_CountryCounty]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CountySet] DROP CONSTRAINT [FK_CountryCounty];
GO
IF OBJECT_ID(N'[dbo].[FK_PostalOfficePlace]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlaceSet] DROP CONSTRAINT [FK_PostalOfficePlace];
GO
IF OBJECT_ID(N'[dbo].[FK_CountyPostalOffice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostalOfficeSet] DROP CONSTRAINT [FK_CountyPostalOffice];
GO
IF OBJECT_ID(N'[dbo].[FK_RepairCustomerDevice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WorkItemSet_Repair] DROP CONSTRAINT [FK_RepairCustomerDevice];
GO
IF OBJECT_ID(N'[dbo].[FK_ServiceCustomerDevice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WorkItemSet_Service] DROP CONSTRAINT [FK_ServiceCustomerDevice];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerDeviceCustomer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerDeviceSet] DROP CONSTRAINT [FK_CustomerDeviceCustomer];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerDeviceCommissioner]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerDeviceSet] DROP CONSTRAINT [FK_CustomerDeviceCommissioner];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerDeviceDevice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerDeviceSet] DROP CONSTRAINT [FK_CustomerDeviceDevice];
GO
IF OBJECT_ID(N'[dbo].[FK_ManufacturerDevice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DeviceSet] DROP CONSTRAINT [FK_ManufacturerDevice];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeWorkItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WorkItemSet] DROP CONSTRAINT [FK_EmployeeWorkItem];
GO
IF OBJECT_ID(N'[dbo].[FK_JobRoleEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSet_Employee] DROP CONSTRAINT [FK_JobRoleEmployee];
GO
IF OBJECT_ID(N'[dbo].[FK_Repair_inherits_WorkItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WorkItemSet_Repair] DROP CONSTRAINT [FK_Repair_inherits_WorkItem];
GO
IF OBJECT_ID(N'[dbo].[FK_Service_inherits_WorkItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WorkItemSet_Service] DROP CONSTRAINT [FK_Service_inherits_WorkItem];
GO
IF OBJECT_ID(N'[dbo].[FK_Customer_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSet_Customer] DROP CONSTRAINT [FK_Customer_inherits_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Commissioner_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSet_Commissioner] DROP CONSTRAINT [FK_Commissioner_inherits_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Manufacturer_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSet_Manufacturer] DROP CONSTRAINT [FK_Manufacturer_inherits_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Employee_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSet_Employee] DROP CONSTRAINT [FK_Employee_inherits_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[AddressSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AddressSet];
GO
IF OBJECT_ID(N'[dbo].[ContactInfoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContactInfoSet];
GO
IF OBJECT_ID(N'[dbo].[TelephoneSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TelephoneSet];
GO
IF OBJECT_ID(N'[dbo].[CountrySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CountrySet];
GO
IF OBJECT_ID(N'[dbo].[PlaceSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PlaceSet];
GO
IF OBJECT_ID(N'[dbo].[PostalOfficeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PostalOfficeSet];
GO
IF OBJECT_ID(N'[dbo].[CustomerDeviceSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerDeviceSet];
GO
IF OBJECT_ID(N'[dbo].[DeviceSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DeviceSet];
GO
IF OBJECT_ID(N'[dbo].[WorkItemSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WorkItemSet];
GO
IF OBJECT_ID(N'[dbo].[JobRoleSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobRoleSet];
GO
IF OBJECT_ID(N'[dbo].[CountySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CountySet];
GO
IF OBJECT_ID(N'[dbo].[WorkItemSet_Repair]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WorkItemSet_Repair];
GO
IF OBJECT_ID(N'[dbo].[WorkItemSet_Service]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WorkItemSet_Service];
GO
IF OBJECT_ID(N'[dbo].[UserSet_Customer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet_Customer];
GO
IF OBJECT_ID(N'[dbo].[UserSet_Commissioner]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet_Commissioner];
GO
IF OBJECT_ID(N'[dbo].[UserSet_Manufacturer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet_Manufacturer];
GO
IF OBJECT_ID(N'[dbo].[UserSet_Employee]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet_Employee];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [PIN] nvarchar(max)  NOT NULL,
    [Type] int  NOT NULL,
    [Note] nvarchar(max)  NULL
);
GO

-- Creating table 'AddressSet'
CREATE TABLE [dbo].[AddressSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StreetLine] nvarchar(max)  NOT NULL,
    [Place_Id] int  NOT NULL
);
GO

-- Creating table 'ContactInfoSet'
CREATE TABLE [dbo].[ContactInfoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [User_Id] int  NOT NULL,
    [Address_Id] int  NOT NULL
);
GO

-- Creating table 'TelephoneSet'
CREATE TABLE [dbo].[TelephoneSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [IsCustom] bit  NOT NULL,
    [ContactInfo_Id] int  NOT NULL,
    [County_Id] int  NOT NULL
);
GO

-- Creating table 'CountrySet'
CREATE TABLE [dbo].[CountrySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CallCode] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PlaceSet'
CREATE TABLE [dbo].[PlaceSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [PostalOffice_Id] int  NOT NULL
);
GO

-- Creating table 'PostalOfficeSet'
CREATE TABLE [dbo].[PostalOfficeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ZIP] nvarchar(max)  NOT NULL,
    [County_Id] int  NOT NULL
);
GO

-- Creating table 'CustomerDeviceSet'
CREATE TABLE [dbo].[CustomerDeviceSet] (
    [CommissionDate] datetime  NULL,
    [IsObsolete] bit  NOT NULL,
    [Id] int IDENTITY(1,1) NOT NULL,
    [Note] nvarchar(max)  NULL,
    [Customer_Id] int  NOT NULL,
    [Commissioner_Id] int  NOT NULL,
    [Device_Id] int  NOT NULL
);
GO

-- Creating table 'DeviceSet'
CREATE TABLE [dbo].[DeviceSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ProductionYear] nvarchar(max)  NOT NULL,
    [Manufacturer_Id] int  NOT NULL
);
GO

-- Creating table 'WorkItemSet'
CREATE TABLE [dbo].[WorkItemSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NULL,
    [Price] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Employee_Id] int  NOT NULL
);
GO

-- Creating table 'JobRoleSet'
CREATE TABLE [dbo].[JobRoleSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CountySet'
CREATE TABLE [dbo].[CountySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [CallCode] nvarchar(max)  NOT NULL,
    [Country_Id] int  NOT NULL
);
GO

-- Creating table 'WorkItemSet_Repair'
CREATE TABLE [dbo].[WorkItemSet_Repair] (
    [Id] int  NOT NULL,
    [CustomerDevice_Id] int  NOT NULL
);
GO

-- Creating table 'WorkItemSet_Service'
CREATE TABLE [dbo].[WorkItemSet_Service] (
    [Id] int  NOT NULL,
    [CustomerDevice_Id] int  NOT NULL
);
GO

-- Creating table 'UserSet_Customer'
CREATE TABLE [dbo].[UserSet_Customer] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'UserSet_Commissioner'
CREATE TABLE [dbo].[UserSet_Commissioner] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'UserSet_Manufacturer'
CREATE TABLE [dbo].[UserSet_Manufacturer] (
    [Website] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'UserSet_Employee'
CREATE TABLE [dbo].[UserSet_Employee] (
    [EmployedSince] datetime  NULL,
    [Id] int  NOT NULL,
    [JobRole_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AddressSet'
ALTER TABLE [dbo].[AddressSet]
ADD CONSTRAINT [PK_AddressSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ContactInfoSet'
ALTER TABLE [dbo].[ContactInfoSet]
ADD CONSTRAINT [PK_ContactInfoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TelephoneSet'
ALTER TABLE [dbo].[TelephoneSet]
ADD CONSTRAINT [PK_TelephoneSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CountrySet'
ALTER TABLE [dbo].[CountrySet]
ADD CONSTRAINT [PK_CountrySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PlaceSet'
ALTER TABLE [dbo].[PlaceSet]
ADD CONSTRAINT [PK_PlaceSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PostalOfficeSet'
ALTER TABLE [dbo].[PostalOfficeSet]
ADD CONSTRAINT [PK_PostalOfficeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CustomerDeviceSet'
ALTER TABLE [dbo].[CustomerDeviceSet]
ADD CONSTRAINT [PK_CustomerDeviceSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DeviceSet'
ALTER TABLE [dbo].[DeviceSet]
ADD CONSTRAINT [PK_DeviceSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WorkItemSet'
ALTER TABLE [dbo].[WorkItemSet]
ADD CONSTRAINT [PK_WorkItemSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'JobRoleSet'
ALTER TABLE [dbo].[JobRoleSet]
ADD CONSTRAINT [PK_JobRoleSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CountySet'
ALTER TABLE [dbo].[CountySet]
ADD CONSTRAINT [PK_CountySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WorkItemSet_Repair'
ALTER TABLE [dbo].[WorkItemSet_Repair]
ADD CONSTRAINT [PK_WorkItemSet_Repair]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WorkItemSet_Service'
ALTER TABLE [dbo].[WorkItemSet_Service]
ADD CONSTRAINT [PK_WorkItemSet_Service]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSet_Customer'
ALTER TABLE [dbo].[UserSet_Customer]
ADD CONSTRAINT [PK_UserSet_Customer]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSet_Commissioner'
ALTER TABLE [dbo].[UserSet_Commissioner]
ADD CONSTRAINT [PK_UserSet_Commissioner]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSet_Manufacturer'
ALTER TABLE [dbo].[UserSet_Manufacturer]
ADD CONSTRAINT [PK_UserSet_Manufacturer]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSet_Employee'
ALTER TABLE [dbo].[UserSet_Employee]
ADD CONSTRAINT [PK_UserSet_Employee]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [User_Id] in table 'ContactInfoSet'
ALTER TABLE [dbo].[ContactInfoSet]
ADD CONSTRAINT [FK_ContactInfoUser]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactInfoUser'
CREATE INDEX [IX_FK_ContactInfoUser]
ON [dbo].[ContactInfoSet]
    ([User_Id]);
GO

-- Creating foreign key on [Address_Id] in table 'ContactInfoSet'
ALTER TABLE [dbo].[ContactInfoSet]
ADD CONSTRAINT [FK_AddressContactInfo]
    FOREIGN KEY ([Address_Id])
    REFERENCES [dbo].[AddressSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AddressContactInfo'
CREATE INDEX [IX_FK_AddressContactInfo]
ON [dbo].[ContactInfoSet]
    ([Address_Id]);
GO

-- Creating foreign key on [Place_Id] in table 'AddressSet'
ALTER TABLE [dbo].[AddressSet]
ADD CONSTRAINT [FK_PlaceAddress]
    FOREIGN KEY ([Place_Id])
    REFERENCES [dbo].[PlaceSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PlaceAddress'
CREATE INDEX [IX_FK_PlaceAddress]
ON [dbo].[AddressSet]
    ([Place_Id]);
GO

-- Creating foreign key on [ContactInfo_Id] in table 'TelephoneSet'
ALTER TABLE [dbo].[TelephoneSet]
ADD CONSTRAINT [FK_TelephoneContactInfo]
    FOREIGN KEY ([ContactInfo_Id])
    REFERENCES [dbo].[ContactInfoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TelephoneContactInfo'
CREATE INDEX [IX_FK_TelephoneContactInfo]
ON [dbo].[TelephoneSet]
    ([ContactInfo_Id]);
GO

-- Creating foreign key on [County_Id] in table 'TelephoneSet'
ALTER TABLE [dbo].[TelephoneSet]
ADD CONSTRAINT [FK_TelephoneCounty]
    FOREIGN KEY ([County_Id])
    REFERENCES [dbo].[CountySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TelephoneCounty'
CREATE INDEX [IX_FK_TelephoneCounty]
ON [dbo].[TelephoneSet]
    ([County_Id]);
GO

-- Creating foreign key on [Country_Id] in table 'CountySet'
ALTER TABLE [dbo].[CountySet]
ADD CONSTRAINT [FK_CountryCounty]
    FOREIGN KEY ([Country_Id])
    REFERENCES [dbo].[CountrySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CountryCounty'
CREATE INDEX [IX_FK_CountryCounty]
ON [dbo].[CountySet]
    ([Country_Id]);
GO

-- Creating foreign key on [PostalOffice_Id] in table 'PlaceSet'
ALTER TABLE [dbo].[PlaceSet]
ADD CONSTRAINT [FK_PostalOfficePlace]
    FOREIGN KEY ([PostalOffice_Id])
    REFERENCES [dbo].[PostalOfficeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PostalOfficePlace'
CREATE INDEX [IX_FK_PostalOfficePlace]
ON [dbo].[PlaceSet]
    ([PostalOffice_Id]);
GO

-- Creating foreign key on [County_Id] in table 'PostalOfficeSet'
ALTER TABLE [dbo].[PostalOfficeSet]
ADD CONSTRAINT [FK_CountyPostalOffice]
    FOREIGN KEY ([County_Id])
    REFERENCES [dbo].[CountySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CountyPostalOffice'
CREATE INDEX [IX_FK_CountyPostalOffice]
ON [dbo].[PostalOfficeSet]
    ([County_Id]);
GO

-- Creating foreign key on [CustomerDevice_Id] in table 'WorkItemSet_Repair'
ALTER TABLE [dbo].[WorkItemSet_Repair]
ADD CONSTRAINT [FK_RepairCustomerDevice]
    FOREIGN KEY ([CustomerDevice_Id])
    REFERENCES [dbo].[CustomerDeviceSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RepairCustomerDevice'
CREATE INDEX [IX_FK_RepairCustomerDevice]
ON [dbo].[WorkItemSet_Repair]
    ([CustomerDevice_Id]);
GO

-- Creating foreign key on [CustomerDevice_Id] in table 'WorkItemSet_Service'
ALTER TABLE [dbo].[WorkItemSet_Service]
ADD CONSTRAINT [FK_ServiceCustomerDevice]
    FOREIGN KEY ([CustomerDevice_Id])
    REFERENCES [dbo].[CustomerDeviceSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ServiceCustomerDevice'
CREATE INDEX [IX_FK_ServiceCustomerDevice]
ON [dbo].[WorkItemSet_Service]
    ([CustomerDevice_Id]);
GO

-- Creating foreign key on [Customer_Id] in table 'CustomerDeviceSet'
ALTER TABLE [dbo].[CustomerDeviceSet]
ADD CONSTRAINT [FK_CustomerDeviceCustomer]
    FOREIGN KEY ([Customer_Id])
    REFERENCES [dbo].[UserSet_Customer]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerDeviceCustomer'
CREATE INDEX [IX_FK_CustomerDeviceCustomer]
ON [dbo].[CustomerDeviceSet]
    ([Customer_Id]);
GO

-- Creating foreign key on [Commissioner_Id] in table 'CustomerDeviceSet'
ALTER TABLE [dbo].[CustomerDeviceSet]
ADD CONSTRAINT [FK_CustomerDeviceCommissioner]
    FOREIGN KEY ([Commissioner_Id])
    REFERENCES [dbo].[UserSet_Commissioner]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerDeviceCommissioner'
CREATE INDEX [IX_FK_CustomerDeviceCommissioner]
ON [dbo].[CustomerDeviceSet]
    ([Commissioner_Id]);
GO

-- Creating foreign key on [Device_Id] in table 'CustomerDeviceSet'
ALTER TABLE [dbo].[CustomerDeviceSet]
ADD CONSTRAINT [FK_CustomerDeviceDevice]
    FOREIGN KEY ([Device_Id])
    REFERENCES [dbo].[DeviceSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerDeviceDevice'
CREATE INDEX [IX_FK_CustomerDeviceDevice]
ON [dbo].[CustomerDeviceSet]
    ([Device_Id]);
GO

-- Creating foreign key on [Manufacturer_Id] in table 'DeviceSet'
ALTER TABLE [dbo].[DeviceSet]
ADD CONSTRAINT [FK_ManufacturerDevice]
    FOREIGN KEY ([Manufacturer_Id])
    REFERENCES [dbo].[UserSet_Manufacturer]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ManufacturerDevice'
CREATE INDEX [IX_FK_ManufacturerDevice]
ON [dbo].[DeviceSet]
    ([Manufacturer_Id]);
GO

-- Creating foreign key on [Employee_Id] in table 'WorkItemSet'
ALTER TABLE [dbo].[WorkItemSet]
ADD CONSTRAINT [FK_EmployeeWorkItem]
    FOREIGN KEY ([Employee_Id])
    REFERENCES [dbo].[UserSet_Employee]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeWorkItem'
CREATE INDEX [IX_FK_EmployeeWorkItem]
ON [dbo].[WorkItemSet]
    ([Employee_Id]);
GO

-- Creating foreign key on [JobRole_Id] in table 'UserSet_Employee'
ALTER TABLE [dbo].[UserSet_Employee]
ADD CONSTRAINT [FK_JobRoleEmployee]
    FOREIGN KEY ([JobRole_Id])
    REFERENCES [dbo].[JobRoleSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_JobRoleEmployee'
CREATE INDEX [IX_FK_JobRoleEmployee]
ON [dbo].[UserSet_Employee]
    ([JobRole_Id]);
GO

-- Creating foreign key on [Id] in table 'WorkItemSet_Repair'
ALTER TABLE [dbo].[WorkItemSet_Repair]
ADD CONSTRAINT [FK_Repair_inherits_WorkItem]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[WorkItemSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'WorkItemSet_Service'
ALTER TABLE [dbo].[WorkItemSet_Service]
ADD CONSTRAINT [FK_Service_inherits_WorkItem]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[WorkItemSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'UserSet_Customer'
ALTER TABLE [dbo].[UserSet_Customer]
ADD CONSTRAINT [FK_Customer_inherits_User]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'UserSet_Commissioner'
ALTER TABLE [dbo].[UserSet_Commissioner]
ADD CONSTRAINT [FK_Commissioner_inherits_User]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'UserSet_Manufacturer'
ALTER TABLE [dbo].[UserSet_Manufacturer]
ADD CONSTRAINT [FK_Manufacturer_inherits_User]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'UserSet_Employee'
ALTER TABLE [dbo].[UserSet_Employee]
ADD CONSTRAINT [FK_Employee_inherits_User]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------