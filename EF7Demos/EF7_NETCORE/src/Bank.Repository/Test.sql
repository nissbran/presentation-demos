IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK_HistoryRow] PRIMARY KEY ([MigrationId])
    );

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20151017224439_Initial')
BEGIN
    CREATE SEQUENCE [EntityFrameworkHiLoSequence] START WITH 1 INCREMENT BY 10 NO MINVALUE NO MAXVALUE NO CYCLE;
END

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20151017224439_Initial')
BEGIN
    CREATE TABLE [BankCustomer] (
        [CustomerId] bigint NOT NULL,
        [CustomerType] int NOT NULL,
        [Name] nvarchar(max),
        [FirstName] nvarchar(max),
        [LastName] nvarchar(max),
        CONSTRAINT [PK_BankCustomer] PRIMARY KEY ([CustomerId])
    );
END

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20151017224439_Initial')
BEGIN
    CREATE TABLE [BankTransaction] (
        [BankTransactionId] uniqueidentifier NOT NULL,
        [Amount] decimal(18, 2) NOT NULL,
        [CustomerId] bigint NOT NULL,
        CONSTRAINT [PK_BankTransaction] PRIMARY KEY ([BankTransactionId]),
        CONSTRAINT [FK_BankTransaction_BankCustomer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [BankCustomer] ([CustomerId])
    );
END

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20151017224439_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20151017224439_Initial', N'7.0.0-beta8-15964');
END

GO

