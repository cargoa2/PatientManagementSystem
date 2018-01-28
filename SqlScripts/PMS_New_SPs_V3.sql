USE PatientManagementSystem
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE sp_Get_Patient_List
AS
SELECT Id, LastName, FirstName, MiddleInitial, BirthDate, SOC, Gender
FROM Patient
ORDER BY LastName
GO

CREATE PROCEDURE sp_Get_Patient_Doc_List @id int
AS
SELECT d.PatientId, p.LastName + ' ' + p.FirstName as FullName, p.BirthDate, d.DocDate, d.DocFileId, d.DocType, d.Notes, d.FilePath 
FROM Documents d INNER JOIN Patient p ON p.id = d.PatientId
WHERE D.PatientId = @id
ORDER BY DocDate DESC
GO

CREATE PROCEDURE sp_Get_Patient_Imm_List @id int
AS
SELECT i.PatientId, p.LastName + ' ' + p.FirstName as FullName, p.BirthDate, i.ImmId, i.ImmDate, i.Notes, i.FilePath 
FROM Immunizations i
INNER JOIN	Patient p ON i.PatientId = p.Id
WHERE i.PatientId = @id
ORDER BY i.ImmDate DESC
GO

CREATE PROCEDURE sp_Get_Patient_Visit_List @id int
AS
SELECT v.PatientId, p.LastName + ' ' + p.FirstName as FullName, p.BirthDate, v.VisitId, v.VisitDate, v.Initial 
FROM Visits v
INNER JOIN	Patient p ON v.PatientId = p.Id
WHERE v.PatientId = @id
ORDER BY v.VisitDate DESC, v.VisitId DESC
GO
