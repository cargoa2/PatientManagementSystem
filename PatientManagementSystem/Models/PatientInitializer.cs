using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PatientManagementSystem.Models
{
    public class PatientInitializer : DropCreateDatabaseIfModelChanges<PatientContext>
    {
        protected override void Seed(PatientContext context)
        {
            //var patients = new List<Patient>
            //    {
            //        new Patient {
            //                        Active = true,
            //                        FirstName = "Alex",
            //                        MiddleInitial = "A",
            //                        LastName = "Rod",
            //                        StreetAddress = "100 SomeLane",
            //                        City = "Louisville",
            //                        State = "KY",
            //                        PostalCode = "40223",
            //                        PatientPhone = "5025000000",
            //                        PatientPhoneType = PhoneNumberTypes.Home,
            //                        BirthDate = Convert.ToDateTime("1995/1/1"),
            //                        SOC = "400101000",
            //                        Gender = Gender.M,
            //                        Status = MaritalStatus.S,
            //                        Employed = false,
            //                        EmployerName = null,
            //                        Relation = Relation.Self,
            //                        SubscriberLastName = null,
            //                        SubscriberFirstName = null,
            //                        SubscriberMiddleInitial = null,
            //                        SubscriberBirthDate = null,
            //                        SubscriberSOC = null,
            //                        SubscriberGender = null,
            //                        ReferingPhysician = "Dr Referral",
            //                        EmergencyContact = " Mom",
            //                        PrimaryInsurance = "Alex Primary Insurance",
            //                        SecondaryInsurance = "Alex Secondary Insurance",
            //                        PharmacyPhone = "5025010101",
            //                        MedicalAllergy = "There could be multiple allergies and this is a multiline text.",
            //                        HIVDiagnosisDate = Convert.ToDateTime("2012-12-01 00:00:00.000"),
            //                        TCellAtDiagnosis = 250,
            //                        ViralLoadAtDiagnosis = 500,
            //                        Signature = true,
            //                        DateSigned = DateTime.Now
            //                    },
            //         new Patient {
            //                        Active = true,
            //                        FirstName = "Lynda",
            //                        MiddleInitial = "B",
            //                        LastName = "Berry",
            //                        StreetAddress = "101 SomeLane",
            //                        City = "Paducah",
            //                        State = "KY",
            //                        PostalCode = "40220",
            //                        PatientPhone = "5024581727",
            //                        PatientPhoneType = PhoneNumberTypes.Home,
            //                        BirthDate = Convert.ToDateTime("1996/2/1"),
            //                        SOC = "401101001",
            //                        Gender = Gender.F,
            //                        Status = MaritalStatus.D,
            //                        Employed = true,
            //                        EmployerName = "Lynda's Employer",
            //                        Relation = Relation.Self,
            //                        SubscriberLastName = null,
            //                        SubscriberFirstName = null,
            //                        SubscriberMiddleInitial = null,
            //                        SubscriberBirthDate = null,
            //                        SubscriberSOC = null,
            //                        SubscriberGender = null,
            //                        ReferingPhysician = "Dr. Lynda's Doctor",
            //                        EmergencyContact = "Lynda's Mom",
            //                        PrimaryInsurance = "Lynda's Primary Insurance",
            //                        SecondaryInsurance = null,
            //                        Signature = true,
            //                        DateSigned = DateTime.Now,

            //                    },
            //        new Patient {
            //                        Active = true,
            //                        FirstName = "John",
            //                        MiddleInitial = "C",
            //                        LastName = "Doe",
            //                        StreetAddress = "102 SomeLane",
            //                        City = "New York",
            //                        State = "NY",
            //                        PostalCode = "40230",
            //                        PatientPhone = "5024000000",
            //                        PatientPhoneType = PhoneNumberTypes.Home,
            //                        BirthDate = Convert.ToDateTime("1996/2/1"),
            //                        SOC = "402101002",
            //                        Gender = Gender.M,
            //                        Status = MaritalStatus.M,
            //                        Employed = false,
            //                        EmployerName = null,
            //                        Relation = Relation.Spouce,
            //                        SubscriberLastName = "Doe",
            //                        SubscriberFirstName = "Jane",
            //                        SubscriberMiddleInitial = "D",
            //                        SubscriberBirthDate = Convert.ToDateTime("1968/3/6"),
            //                        SubscriberSOC = "500239878",
            //                        SubscriberGender = Gender.F,
            //                        ReferingPhysician = "Dr. john's Doctor",
            //                        EmergencyContact = "John's Mom",
            //                        PrimaryInsurance = "John's Primary Insurance",
            //                        SecondaryInsurance = null,
            //                        Signature = true,
            //                        DateSigned = DateTime.Now,
            //                    },
            //        new Patient {
            //                        Active = false,
            //                        FirstName = "Jim",
            //                        MiddleInitial = "C",
            //                        LastName = "John",
            //                        StreetAddress = "103 SomeLane",
            //                        City = "Louisville",
            //                        State = "KY",
            //                        PostalCode = "40230",
            //                        PatientPhone = "5025000000",
            //                        PatientPhoneType = PhoneNumberTypes.Home,
            //                        BirthDate = Convert.ToDateTime("1969/5/1"),
            //                        SOC = "403101003",
            //                        Gender = Gender.M,
            //                        Status = MaritalStatus.S,
            //                        Employed = false,
            //                        EmployerName = null,
            //                        Relation = Relation.Self,
            //                        SubscriberLastName = null,
            //                        SubscriberFirstName = null,
            //                        SubscriberMiddleInitial = null,
            //                        SubscriberBirthDate = null,
            //                        SubscriberSOC = null,
            //                        SubscriberGender = null,
            //                        ReferingPhysician = "Dr. Jim's Doctor",
            //                        EmergencyContact = "Jim's Mom",
            //                        PrimaryInsurance = "Jim's Primary Insurance",
            //                        SecondaryInsurance = null,
            //                        Signature = true,
            //                        DateSigned = DateTime.Now,
            //                    }

            //                 };

            //foreach (var temp in patients)
            //{
            //    context.Patients.Add(temp);

            //}
            //context.SaveChanges();

            //var visits = new List<Visits>
            //    {
            //        new Visits {PatientId = 1,
            //                    Initial = true,
            //                    VisitType = VisitTypes.N99201,
            //                    DiagnosisCode = "B20",
            //                    VisitDate = Convert.ToDateTime("1/15/2017"),
            //                    ReferralReason = "This is an initial consultation referral reason.",
            //                    History = "Patient has had HIV for 4 years.",
            //                    PastHistory = "Patient was healthy.",
            //                    Epidemiology = "HIV",
            //                    FamilyHistory = "Heart Disease.",
            //                    SocialHistory = "Drug Addict.",
            //                    RosGeneral = "ROS 1",
            //                    RosHeent = "Ros Normal",
            //                    Respiratory = "Good",
            //                    Cardiovascular = "Good",
            //                    Gastrointestinal = "Good",
            //                    Genitourniary = "Good",
            //                    RosNeurological = "Good",
            //                    psychosocial = "Good",
            //                    Medications = "Might end up being a list.",
            //                    Laboratory = "This test for 1.",
            //                    PeGeneral = "Good",
            //                    BloodPressure = "100/80",
            //                    HeartRate = 70,
            //                    Tempurature = 98.6M,
            //                    Weight = 180,
            //                    PeHeent = "Good",
            //                    Neck = "Good",
            //                    Lungs = "Good",
            //                    Heart = "Good",
            //                    Abdomen = "Good",
            //                    Musculoskeletal = "Good",
            //                    PeNeurological = "Good",
            //                    Additional = "Nothing",
            //                    Assessment = "This is the assement for patient 1",
            //                    Plan = "This patient will start coming once a week for T cell counts.",
            //                    IsHIVManagement = true,
            //                    IsOtherManagement = false
            //                    },
            //        new Visits {PatientId = 1,
            //                    Initial = false,
            //                    VisitType =VisitTypes.E99211,
            //                    DiagnosisCode = "B20",
            //                    VisitDate = DateTime.Now,
            //                    ReferralReason = "This is patient 1 second visit.",
            //                    History = "Patient has had HIV for 4 years.",
            //                    PastHistory = "Patient was healthy.",
            //                    Epidemiology = "HIV",
            //                    FamilyHistory = "Heart disease.",
            //                    SocialHistory = "Drug Addict.",
            //                    RosGeneral = "ROS 1",
            //                    RosHeent = "Ros Normal",
            //                    Respiratory = "Good",
            //                    Cardiovascular = "Good",
            //                    Gastrointestinal = "Good",
            //                    Genitourniary = "Good",
            //                    RosNeurological = "Good",
            //                    psychosocial = "Good",
            //                    Medications = "Might end up being a list.",
            //                    Laboratory = "This test for 1.",
            //                    PeGeneral = "Good",
            //                    BloodPressure = "100/80",
            //                    HeartRate = 70,
            //                    Tempurature = 98.6M,
            //                    Weight = 180,
            //                    PeHeent = "Good",
            //                    Neck = "Good",
            //                    Lungs = "Good",
            //                    Heart = "Good",
            //                    Abdomen = "Good",
            //                    Musculoskeletal = "Good",
            //                    PeNeurological = "Good",
            //                    Additional = "Nothing",
            //                    Assessment = "This is the assement for patient 1",
            //                    Plan = "This patient will start coming once a week for T cell counts.",
            //                    IsHIVManagement = true,
            //                    IsOtherManagement = false
            //                    },
            //        new Visits {PatientId = 2,
            //                    Initial = true,
            //                    VisitType = VisitTypes.N99201,
            //                    DiagnosisCode = "B20",
            //                    VisitDate = DateTime.Now,
            //                    ReferralReason = "This is an initial consultation referral reason.",
            //                    History = "Patient 2.",
            //                    PastHistory = "Patient was healthy.",
            //                    Epidemiology = "HIV",
            //                    FamilyHistory = "Heart disease.",
            //                    SocialHistory = "Drug Addict.",
            //                    RosGeneral = "ROS 1",
            //                    RosHeent = "Ros Normal",
            //                    Respiratory = "Good",
            //                    Cardiovascular = "Good",
            //                    Gastrointestinal = "Good",
            //                    Genitourniary = "Good",
            //                    RosNeurological = "Good",
            //                    psychosocial = "Good",
            //                    Medications = "Might end up being a list.",
            //                    Laboratory = "This test for 1.",
            //                    PeGeneral = "Good",
            //                    BloodPressure = "100/80",
            //                    HeartRate = 70,
            //                    Tempurature = 98.6M,
            //                    Weight = 180,
            //                    PeHeent = "Good",
            //                    Neck = "Good",
            //                    Lungs = "Good",
            //                    Heart = "Good",
            //                    Abdomen = "Good",
            //                    Musculoskeletal = "Good",
            //                    PeNeurological = "Good",
            //                    Additional = "Nothing",
            //                    Assessment = "This is the assement for patient 1",
            //                    Plan = "This patient will start coming once a week for T cell counts.",
            //                    IsHIVManagement = false,
            //                    IsOtherManagement = true
            //                    },

            //                    new Visits {PatientId = 4,
            //                    Initial = true,
            //                    VisitType = VisitTypes.N99201,
            //                    DiagnosisCode = "B20",
            //                    VisitDate = Convert.ToDateTime("3/1/2014"),
            //                    ReferralReason = "This is an initial consultation referral reason.",
            //                    History = "Patient 4.",
            //                    PastHistory = "Patient was healthy.",
            //                    Epidemiology = "Other",
            //                    FamilyHistory = "Heart disease.",
            //                    SocialHistory = "Drug Addict.",
            //                    RosGeneral = "ROS 1",
            //                    RosHeent = "Ros Normal",
            //                    Respiratory = "Good",
            //                    Cardiovascular = "Good",
            //                    Gastrointestinal = "Good",
            //                    Genitourniary = "Good",
            //                    RosNeurological = "Good",
            //                    psychosocial = "Good",
            //                    Medications = "Might end up being a list.",
            //                    Laboratory = "This test for 1.",
            //                    PeGeneral = "Good",
            //                    BloodPressure = "100/80",
            //                    HeartRate = 70,
            //                    Tempurature = 98.6M,
            //                    Weight = 180,
            //                    PeHeent = "Good",
            //                    Neck = "Good",
            //                    Lungs = "Good",
            //                    Heart = "Good",
            //                    Abdomen = "Good",
            //                    Musculoskeletal = "Good",
            //                    PeNeurological = "Good",
            //                    Additional = "Nothing",
            //                    Assessment = "This is the assement for patient 1",
            //                    Plan = "This patient is on medication and should not need followup.",
            //                    IsHIVManagement = true,
            //                    IsOtherManagement = false
            //                    }
            //        };
            //foreach (var temp in visits)
            //{
            //    context.Visits.Add(temp);
            //}
            //context.SaveChanges();

            //var hivManagement = new List<HIVManagement>
            //{  new HIVManagement
            //    {
            //        VisitId = 1,
            //        PatientId = 1,
            //        //HIVDiagnosisDat = Convert.ToDateTime("2012, 12,1"),
            //        //TCellAtDiagnosis = 250,
            //        //ViralLoadAtDiagnosis = 500,
            //        Problem = "Medicationis not working",
            //        ICD10 = "200.20",
            //        MedicationStart = Convert.ToDateTime("2015/3/1"),
            //        Medication = "Isentriss",
            //        DiagnosisCode = "100"
            //    },
            //    new HIVManagement {
            //        VisitId = 2,
            //        PatientId = 1,
            //        //HIVDiagnosis = Convert.ToDateTime("2012, 12,1"),
            //        //TCellAtDiagnosis = 250,
            //        //ViralLoadAtDiagnosis = 500,
            //        //ProblemDate = DateTime.Now,
            //        Problem = "Medicationis not working",
            //        ICD10 = "200.20",
            //        MedicationStart = Convert.ToDateTime("2016/3/1"),
            //        Medication = "Something else",
            //        DiagnosisCode = "100",
            //    }
            //};

            //foreach (var temp in hivManagement)
            //{
            //    context.HIVManegements.Add(temp);
            //}
            //context.SaveChanges();

            ////var HivDetails = new List<HIVManagementDetail>
            ////{
            ////    new HIVManagementDetail
            ////    {
            ////        HIVManagementId = 1,
            ////        ProblemDate = DateTime.Now,
            ////        Problem = "Medicationis not working",
            ////        ICD10 = "200.20",
            ////        MedicationStart = Convert.ToDateTime("2015/3/1"),
            ////        Medication = "Isentriss",
            ////        DiagnosisCode = "100"
            ////    },
            ////    new HIVManagementDetail
            ////    {
            ////        HIVManagementId = 1,
            ////        ProblemDate = Convert.ToDateTime("2012/01/01"),
            ////        Problem = "Medications not working",
            ////        ICD10 = "200.20",
            ////        MedicationStart = Convert.ToDateTime("2011/11/01"),
            ////        Medication = "New med",
            ////        DiagnosisCode = "200"
            ////    }
            ////};

            ////foreach (var temp in HivDetails)
            ////{
            ////    context.HIVManagementDetails.Add(temp);
            ////}
            ////context.SaveChanges();


            //var other = new List<OtherManagement>
            //    {
            //        new OtherManagement
            //        {
            //        VisitId = 1,
            //        TCell = 500,
            //        ViralLoad = 20,
            //        WhiteBloodCell = 500,
            //        Hemoglobin = Convert.ToDecimal(100.2),
            //        PlasmaIronTurnover = Convert.ToDecimal(80.8),
            //        OtherWeight = Convert.ToDecimal(160.6),
            //        Triglycerides = 50,
            //        TotalCholesterol = 20,
            //        OtherLab = "Lab for patient 1 first lab"
            //        },
            //        new OtherManagement
            //        {
            //        VisitId = 1,
            //        TCell = 300,
            //        ViralLoad = 200,
            //        WhiteBloodCell = 100,
            //        Hemoglobin = Convert.ToDecimal(100.0),
            //        PlasmaIronTurnover = Convert.ToDecimal(90),
            //        OtherWeight = Convert.ToDecimal(135),
            //        Triglycerides = 40,
            //        TotalCholesterol = 30,
            //        OtherLab = "Lab for patient 1 first lab"
            //        }
            //    };
            //foreach (var temp in other)
            //{
            //    context.OtherManagements.Add(temp);
            //}
            //context.SaveChanges();

            ////var usr = new List<UserAccount>
            ////{
            ////    new UserAccount
            ////    {
            ////        FirstName = "Barbara",
            ////        LastName = "Wojda",
            ////        role = roles.Doctor,
            ////        UserName = "bwojda",
            ////        Password = "password",
            ////        ConfirmPassword = "password"
            ////    }
            ////};
            ////foreach (var temp in usr)
            ////{
            ////    context.UserAccount.Add(temp);
            ////}
            ////context.SaveChanges();





            ////var visitTypes = new List<VisitTypes>
            ////{
            ////    new VisitTypes
            ////    {
            ////        Established = false,
            ////        VisitCode = "99201"
            ////    },
            ////    new VisitTypes
            ////    {
            ////        Established = false,
            ////        VisitCode = "99202"
            ////    },
            ////    new VisitTypes
            ////    {
            ////        Established = false,
            ////        VisitCode = "99203"
            ////    },
            ////    new VisitTypes
            ////    {
            ////        Established = false,
            ////        VisitCode = "99204"
            ////    },
            ////    new VisitTypes
            ////    {
            ////        Established = false,
            ////        VisitCode = "99205"
            ////    },
            ////    new VisitTypes
            ////    {
            ////        Established = true,
            ////        VisitCode = "99211"
            ////    },
            ////    new VisitTypes
            ////    {
            ////        Established = true,
            ////        VisitCode = "99212"
            ////    },
            ////    new VisitTypes
            ////    {
            ////        Established = true,
            ////        VisitCode = "99213"
            ////    },
            ////    new VisitTypes
            ////    {
            ////        Established = true,
            ////        VisitCode = "99214"
            ////    },
            ////    new VisitTypes
            ////    {
            ////        Established = true,
            ////        VisitCode = "99215"
            ////    }
            ////};
            ////foreach (var temp in visitTypes)
            ////{
            ////    context.VisitTypes.Add(temp);
            ////}
            ////context.SaveChanges();
        }

    }
}