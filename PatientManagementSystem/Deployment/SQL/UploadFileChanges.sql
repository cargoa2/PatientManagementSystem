Use PatientManagementSystem
Go

--Alter Table [dbo].[Documents]
--Add [FileContent] [varbinary](max) NULL



select p.FirstName + ' ' + p.LastName as 'Patient Name', d.DocDate,
case when d.DocType = 0  then 'Insurance'
	 when d.DocType = 1 then 'Laboratory'
     when d.DocType = 2 then 'Radiology'
	 when d.DocType = 3 Then 'Other'
end as 'Document Type'
,d.FilePath
 from [dbo].[Documents] d
inner join Patient p on d.PatientId = p.Id

