USE [master]
GO
/****** Object:  Database [PatientManagementSystem]    Script Date: 3/18/2017 8:15:32 PM ******/
CREATE DATABASE [PatientManagementSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
--( NAME = N'PatientManagementSystem.mdf', FILENAME = N'C:\Users\Carolyn\Documents\Visual Studio 2015\Projects\PatientManagementSystem\PatientManagementSystem\App_Data\PatientManagementSystem.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
( NAME = N'PatientManagementSystem.mdf', FILENAME = N'C:\PatientManagementSystem\App_Data\PatientManagementSystem.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
--( NAME = N'PatientManagementSystem_log.ldf', FILENAME = N'C:\Users\Carolyn\Documents\Visual Studio 2015\Projects\PatientManagementSystem\PatientManagementSystem\App_Data\PatientManagementSystem_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
( NAME = N'PatientManagementSystem_log.ldf', FILENAME = N'C:\PatientManagementSystem\App_Data\PatientManagementSystem_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [PatientManagementSystem] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PatientManagementSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PatientManagementSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PatientManagementSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PatientManagementSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PatientManagementSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PatientManagementSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [PatientManagementSystem] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [PatientManagementSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PatientManagementSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PatientManagementSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PatientManagementSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PatientManagementSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PatientManagementSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PatientManagementSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PatientManagementSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PatientManagementSystem] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PatientManagementSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PatientManagementSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PatientManagementSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PatientManagementSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PatientManagementSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PatientManagementSystem] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [PatientManagementSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PatientManagementSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PatientManagementSystem] SET  MULTI_USER 
GO
ALTER DATABASE [PatientManagementSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PatientManagementSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PatientManagementSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PatientManagementSystem] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [PatientManagementSystem] SET DELAYED_DURABILITY = DISABLED 
GO
USE [PatientManagementSystem]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 3/18/2017 8:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Communication]    Script Date: 3/18/2017 8:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Communication](
	[CommId] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[CommDate] [datetime] NOT NULL,
	[Notes] [nvarchar](500) NULL,
	[FilePath] [nvarchar](255) NULL,
 CONSTRAINT [PK_dbo.Communication] PRIMARY KEY CLUSTERED 
(
	[CommId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Documents]    Script Date: 3/18/2017 8:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Documents](
	[DocFileId] [int] IDENTITY(1,1) NOT NULL,
	[DocDate] [datetime] NOT NULL,
	[PatientId] [int] NOT NULL,
	[DocType] [int] NOT NULL,
	[Notes] [nvarchar](255) NULL,
	[FilePath] [nvarchar](255) NULL,
 CONSTRAINT [PK_dbo.Documents] PRIMARY KEY CLUSTERED 
(
	[DocFileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HIVManagement]    Script Date: 3/18/2017 8:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HIVManagement](
	[HIVManagmentId] [int] IDENTITY(1,1) NOT NULL,
	[VisitId] [int] NOT NULL,
	[Problem] [nvarchar](100) NULL,
	[ICD10] [nvarchar](10) NULL,
	[MedicationStart] [datetime] NOT NULL,
	[Medication] [nvarchar](50) NULL,
	[DiagnosisCode] [nvarchar](10) NULL,
	[Patient_Id] [int] NULL,
 CONSTRAINT [PK_dbo.HIVManagement] PRIMARY KEY CLUSTERED 
(
	[HIVManagmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Immunizations]    Script Date: 3/18/2017 8:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Immunizations](
	[ImmId] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[ImmDate] [datetime] NOT NULL,
	[Notes] [nvarchar](255) NULL,
	[FilePath] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Immunizations] PRIMARY KEY CLUSTERED 
(
	[ImmId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OtherManagement]    Script Date: 3/18/2017 8:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OtherManagement](
	[OtherId] [int] IDENTITY(1,1) NOT NULL,
	[VisitId] [int] NOT NULL,
	[TCell] [int] NOT NULL,
	[ViralLoad] [int] NOT NULL,
	[WhiteBloodCell] [int] NOT NULL,
	[Hemoglobin] [decimal](18, 2) NOT NULL,
	[PlasmaIronTurnover] [decimal](18, 2) NOT NULL,
	[OtherWeight] [decimal](18, 2) NOT NULL,
	[Triglycerides] [int] NOT NULL,
	[TotalCholesterol] [int] NOT NULL,
	[OtherDocuments] [nvarchar](max) NULL,
	[Patient_Id] [int] NULL,
 CONSTRAINT [PK_dbo.OtherManagement] PRIMARY KEY CLUSTERED 
(
	[OtherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Patient]    Script Date: 3/18/2017 8:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleInitial] [nvarchar](1) NULL,
	[StreetAddress] [nvarchar](200) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[State] [nvarchar](2) NOT NULL,
	[PostalCode] [nvarchar](5) NOT NULL,
	[PatientPhone] [nvarchar](10) NOT NULL,
	[PatientPhoneType] [int] NOT NULL,
	[OtherPatientPhone] [nvarchar](10) NULL,
	[OtherPhoneType] [int] NOT NULL,
	[BirthDate] [datetime] NOT NULL,
	[SOC] [nvarchar](10) NOT NULL,
	[Gender] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[Employed] [bit] NOT NULL,
	[EmployerName] [nvarchar](50) NULL,
	[EmployerPhone] [nvarchar](10) NULL,
	[Relation] [int] NOT NULL,
	[SubscriberLastName] [nvarchar](50) NULL,
	[SubscriberFirstName] [nvarchar](50) NULL,
	[SubscriberMiddleInitial] [nvarchar](1) NULL,
	[SubscriberBirthDate] [datetime] NULL,
	[SubscriberSOC] [nvarchar](10) NULL,
	[SubscriberGender] [int] NULL,
	[ReferingPhysician] [nvarchar](50) NULL,
	[ReferingPhysicianPhone] [nvarchar](10) NULL,
	[EmergencyContact] [nvarchar](50) NOT NULL,
	[EmergencyPhone] [nvarchar](10) NULL,
	[PrimaryInsurance] [nvarchar](50) NULL,
	[SecondaryInsurance] [nvarchar](50) NULL,
	[PharmacyPhone] [nvarchar](10) NULL,
	[HIVDiagnosisDate] [datetime] NULL,
	[TCellAtDiagnosis] [int] NULL,
	[ViralLoadAtDiagnosis] [int] NULL,
	[Signature] [bit] NOT NULL,
	[DateSigned] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Patient] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Visits]    Script Date: 3/18/2017 8:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Visits](
	[VisitId] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[Initial] [bit] NOT NULL,
	[VisitType] [int] NOT NULL,
	[VisitDate] [datetime] NOT NULL,
	[DiagnosisCode] [nvarchar](10) NOT NULL,
	[CoPay] [decimal](18, 2) NOT NULL,
	[PaymentType] [int] NOT NULL,
	[CheckNumber] [nvarchar](10) NULL,
	[TotalPaid] [decimal](18, 2) NOT NULL,
	[MedicalAllergy] [nvarchar](max) NULL,
	[ReferralReason] [nvarchar](max) NULL,
	[History] [nvarchar](max) NULL,
	[PastHistory] [nvarchar](max) NULL,
	[Epidemiology] [nvarchar](max) NULL,
	[FamilyHistory] [nvarchar](max) NULL,
	[SocialHistory] [nvarchar](max) NULL,
	[RosGeneral] [nvarchar](50) NULL,
	[RosHeent] [nvarchar](50) NULL,
	[Respiratory] [nvarchar](50) NULL,
	[Cardiovascular] [nvarchar](50) NULL,
	[Gastrointestinal] [nvarchar](50) NULL,
	[Genitourniary] [nvarchar](50) NULL,
	[RosNeurological] [nvarchar](50) NULL,
	[psychosocial] [nvarchar](50) NULL,
	[Medications] [nvarchar](max) NULL,
	[PeGeneral] [nvarchar](50) NULL,
	[BloodPressure] [nvarchar](50) NULL,
	[HeartRate] [int] NOT NULL,
	[Tempurature] [decimal](18, 2) NOT NULL,
	[Weight] [decimal](18, 2) NOT NULL,
	[PeHeent] [nvarchar](50) NULL,
	[Neck] [nvarchar](50) NULL,
	[Skin] [nvarchar](50) NULL,
	[Lungs] [nvarchar](50) NULL,
	[Heart] [nvarchar](50) NULL,
	[Abdomen] [nvarchar](50) NULL,
	[Musculoskeletal] [nvarchar](50) NULL,
	[PeNeurological] [nvarchar](100) NULL,
	[Additional] [nvarchar](100) NULL,
	[Documentsoratory] [nvarchar](max) NULL,
	[Assessment] [nvarchar](max) NULL,
	[Plan] [nvarchar](max) NULL,
	[IsHIVManagement] [bit] NOT NULL,
	[IsOtherManagement] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Visits] PRIMARY KEY CLUSTERED 
(
	[VisitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Index [IX_PatientId]    Script Date: 3/18/2017 8:15:32 PM ******/
CREATE NONCLUSTERED INDEX [IX_PatientId] ON [dbo].[Communication]
(
	[PatientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PatientId]    Script Date: 3/18/2017 8:15:32 PM ******/
CREATE NONCLUSTERED INDEX [IX_PatientId] ON [dbo].[Documents]
(
	[PatientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Patient_Id]    Script Date: 3/18/2017 8:15:32 PM ******/
CREATE NONCLUSTERED INDEX [IX_Patient_Id] ON [dbo].[HIVManagement]
(
	[Patient_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_VisitId]    Script Date: 3/18/2017 8:15:32 PM ******/
CREATE NONCLUSTERED INDEX [IX_VisitId] ON [dbo].[HIVManagement]
(
	[VisitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PatientId]    Script Date: 3/18/2017 8:15:32 PM ******/
CREATE NONCLUSTERED INDEX [IX_PatientId] ON [dbo].[Immunizations]
(
	[PatientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Patient_Id]    Script Date: 3/18/2017 8:15:32 PM ******/
CREATE NONCLUSTERED INDEX [IX_Patient_Id] ON [dbo].[OtherManagement]
(
	[Patient_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_VisitId]    Script Date: 3/18/2017 8:15:32 PM ******/
CREATE NONCLUSTERED INDEX [IX_VisitId] ON [dbo].[OtherManagement]
(
	[VisitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PatientId]    Script Date: 3/18/2017 8:15:32 PM ******/
CREATE NONCLUSTERED INDEX [IX_PatientId] ON [dbo].[Visits]
(
	[PatientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Communication]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Communication_dbo.Patient_PatientId] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Communication] CHECK CONSTRAINT [FK_dbo.Communication_dbo.Patient_PatientId]
GO
ALTER TABLE [dbo].[Documents]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Documents_dbo.Patient_PatientId] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Documents] CHECK CONSTRAINT [FK_dbo.Documents_dbo.Patient_PatientId]
GO
ALTER TABLE [dbo].[HIVManagement]  WITH CHECK ADD  CONSTRAINT [FK_dbo.HIVManagement_dbo.Patient_Patient_Id] FOREIGN KEY([Patient_Id])
REFERENCES [dbo].[Patient] ([Id])
GO
ALTER TABLE [dbo].[HIVManagement] CHECK CONSTRAINT [FK_dbo.HIVManagement_dbo.Patient_Patient_Id]
GO
ALTER TABLE [dbo].[HIVManagement]  WITH CHECK ADD  CONSTRAINT [FK_dbo.HIVManagement_dbo.Visits_VisitId] FOREIGN KEY([VisitId])
REFERENCES [dbo].[Visits] ([VisitId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HIVManagement] CHECK CONSTRAINT [FK_dbo.HIVManagement_dbo.Visits_VisitId]
GO
ALTER TABLE [dbo].[Immunizations]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Immunizations_dbo.Patient_PatientId] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Immunizations] CHECK CONSTRAINT [FK_dbo.Immunizations_dbo.Patient_PatientId]
GO
ALTER TABLE [dbo].[OtherManagement]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OtherManagement_dbo.Patient_Patient_Id] FOREIGN KEY([Patient_Id])
REFERENCES [dbo].[Patient] ([Id])
GO
ALTER TABLE [dbo].[OtherManagement] CHECK CONSTRAINT [FK_dbo.OtherManagement_dbo.Patient_Patient_Id]
GO
ALTER TABLE [dbo].[OtherManagement]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OtherManagement_dbo.Visits_VisitId] FOREIGN KEY([VisitId])
REFERENCES [dbo].[Visits] ([VisitId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OtherManagement] CHECK CONSTRAINT [FK_dbo.OtherManagement_dbo.Visits_VisitId]
GO
ALTER TABLE [dbo].[Visits]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Visits_dbo.Patient_PatientId] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Visits] CHECK CONSTRAINT [FK_dbo.Visits_dbo.Patient_PatientId]
GO
USE [master]
GO
ALTER DATABASE [PatientManagementSystem] SET  READ_WRITE 
GO
