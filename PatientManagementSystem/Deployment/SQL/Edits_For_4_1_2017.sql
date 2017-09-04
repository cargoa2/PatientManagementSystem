USE PatientManagementSystem
GO

IF EXISTS(
    SELECT *
    FROM sys.columns 
    WHERE Name      = N'DiagnosisCode'
      AND Object_ID = Object_ID(N'HIVManagement'))
BEGIN
    ALTER TABLE [HIVManagement]
	DROP COLUMN [DiagnosisCode]
END

IF NOT EXISTS(
    SELECT *
    FROM sys.columns 
    WHERE Name      = N'MedDiscDate'
      AND Object_ID = Object_ID(N'HIVManagement'))
BEGIN
ALTER TABLE [dbo].[HIVManagement] ADD MedDiscDate DATETIME
END

IF NOT EXISTS(
    SELECT *
    FROM sys.columns 
    WHERE Name      = N'ProblemList'
      AND Object_ID = Object_ID(N'Visits'))
BEGIN
ALTER TABLE [dbo].Visits ADD ProblemList varchar(Max) Null
END


UPDATE HIVManagement
SET MedDiscDate = '1/1/2017'

BEGIN
ALTER TABLE [DBO].[VISITS]
	ALTER COLUMN PeHeent VARCHAR(255)	
END

BEGIN
ALTER TABLE [DBO].[VISITS]
	ALTER COLUMN RosHeent VARCHAR(255)
END

BEGIN
ALTER TABLE [DBO].[VISITS]
	ALTER COLUMN DiagnosisCode VARCHAR(255)
END

BEGIN
ALTER TABLE [DBO].[VISITS]
	ALTER COLUMN Weight VARCHAR(50)
END