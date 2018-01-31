USE PatientManagementSystem
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE sp_Doc_Count @id int
as
SELECT count(*)
from Documents
where PatientId = @id
 
GO


CREATE PROCEDURE sp_Imm_Count @id int
as
SELECT count(*)
from Immunizations
where PatientId = @id
 
GO

CREATE PROCEDURE sp_Visits_Count @id int
as
SELECT count(*)
from Visits
where PatientId = @id
 
GO


CREATE PROCEDURE sp_Comm_Count @id int
as
SELECT count(*)
from Communication
where PatientId = @id
 
GO

CREATE PROCEDURE sp_Hiv_Count @id int
as
SELECT count(*)
from HIVManagement
where PatientId = @id
 
GO

CREATE PROCEDURE sp_Other_Count @id int
as
SELECT count(*)
from OtherManagement
where PatientId = @id
 
GO