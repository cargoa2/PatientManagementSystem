USE PatientManagementSystem
GO

BEGIN
ALTER TABLE [dbo].[OtherManagement]
	ALTER COLUMN TCell VARCHAR(100)	
END

BEGIN
ALTER TABLE [dbo].[OtherManagement]
	ALTER COLUMN ViralLoad VARCHAR(100)	
END

BEGIN
ALTER TABLE [dbo].[OtherManagement]
	ALTER COLUMN WhiteBloodCell VARCHAR(100)	
END

BEGIN
ALTER TABLE [dbo].[Communication]
	DROP COLUMN [Type]
END

BEGIN
ALTER TABLE [dbo].[Communication]
	Drop COLUMN FilePath
END

BEGIN
ALTER TABLE [dbo].[Communication]
	ALTER COLUMN Notes VARCHAR(Max)	
END





