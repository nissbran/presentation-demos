USE [EF7Context]
GO
/****** Object:  Trigger [dbo].[UpdateTime]    Script Date: 2016-03-03 16:49:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create TRIGGER [dbo].[UpdateTime]
   ON  [dbo].[BankCustomer]
   AFTER UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE dbo.BankCustomer
	SET dbo.BankCustomer.UpdatedOn = CONVERT(DATETIME2(0),SYSDATETIME())
	FROM inserted
	where dbo.BankCustomer.CustomerId = inserted.CustomerId

    -- Insert statements for trigger here

END
