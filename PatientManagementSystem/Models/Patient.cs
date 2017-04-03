using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace PatientManagementSystem.Models
{
    public class Patient
    {
        public Patient() { }

        #region Patient Fields
        [Key]
        public int Id {get; set;}

        public bool Active { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
       
        [MaxLength(1)]
        [Display(Name ="Middle Initial")]
        public string MiddleInitial { get; set; }

        [Display(Name = "Patient Name")]
        public string FullName { get { return FirstName + " " + LastName.ToString(); } }
        public string FullLastName {  get { return LastName + ", " + FirstName.ToString(); } }

        [Required(ErrorMessage = "Street address is required.")]
        [Display(Name = "Address")]
        [MaxLength(200)]
        public string StreetAddress { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [MaxLength(50)]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required.")]
        [MaxLength(2)]
        public string State { get; set; }

        [Required(ErrorMessage = "Postal code is required.")]
        [RegularExpression(@"(^\d{5})")]
        [Display(Name = "Zip")]
        [MaxLength(5)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "At least one patient phone number is required.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "The entered phone number is not valid.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        [MaxLength(10, ErrorMessage = "Please enter the phone number without symbols.")]
        [Display(Name = "Patient Phone")]
        public string PatientPhone { get; set; }

        [Display(Name = "Phone Type")]
        public PhoneNumberTypes PatientPhoneType { get; set; }

        [Display(Name = "Other Patient Phone")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "The entered phone number is not valid.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        [MaxLength(10, ErrorMessage = "Please enter the phone number without symbols.")]
        public string OtherPatientPhone { get; set; }

        [Display(Name = "Phone Type")]
        public PhoneNumberTypes OtherPhoneType { get; set; }

        [Display(Name = "Birth Date")]
        [Required(ErrorMessage = "Date of birth is required.")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        [NotMapped]
        public int Age { get
            {
                var today = DateTime.Today;
                var age = today.Year - BirthDate.Year;
                return age;
            }            
        }

        [Display(Name = "Social")]
        [Required(ErrorMessage = "Social security number is required.")]
        [DisplayFormat(DataFormatString = "{0:###-##-####}", ApplyFormatInEditMode = true)]
        [MaxLength(10, ErrorMessage = "Please only enter numbers without symbols.")]
        [RegularExpression(@"^(?!219099999|078051120)(?!666|000|9\d{2})\d{3}(?!00)\d{2}(?!0{4})\d{4}$", ErrorMessage = "Invalid Social Security Number")]
        public string SOC { get; set; }

        [Required()]
        public Gender Gender { get; set; }

        public MaritalStatus  Status { get; set; } 

        public bool Employed { get; set; }

        [Display(Name = "Employer Name")]
        [MaxLength(50, ErrorMessage = "Name can be no longer than 50 characters.")]
        public string EmployerName { get; set; }
               
        
        [Display(Name = "Employer Phone")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "The entered phone number is not valid.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        [MaxLength(10, ErrorMessage = "Please enter the phone number without symbols.")]
        public string EmployerPhone { get; set; }

        [Required(ErrorMessage = "Please choose a relation to patient.")]
        public Relation Relation { get; set; }

        [MaxLength(50)]
        [Display(Name = "Subscriber Last Name")]
        public string SubscriberLastName { get; set; }


        [MaxLength(50)]
        [Display(Name = "Subscriber First Name")]
        public string SubscriberFirstName { get; set; }

        [MaxLength(1)]
        [Display(Name = " Subscriber Middle Initial")]
        public string SubscriberMiddleInitial { get; set; }

        [Display(Name = "Subscriber Birth Date")]
        public DateTime? SubscriberBirthDate { get; set; }

        [NotMapped]
        public int? SubscriberAge
        {
            get
            {
                if (SubscriberBirthDate != null)
                {
                    var today = DateTime.Today;
                    var age = today.Year - SubscriberBirthDate.Value.Year;
                    return age;
                }
                else
                {
                    return null;
                }
            }
        }

        [Display(Name = "Subscriber Social")]
        [DisplayFormat(DataFormatString = "{0:###-##-####}", ApplyFormatInEditMode = true)]
        [MaxLength(10, ErrorMessage = "Please only enter numbers without symbols.")]
        [RegularExpression(@"^(?!219099999|078051120)(?!666|000|9\d{2})\d{3}(?!00)\d{2}(?!0{4})\d{4}$", ErrorMessage = "Invalid Social Security Number")]
        public string SubscriberSOC { get; set; }

        [Display(Name = "Subscriber Gender")]
        public Gender? SubscriberGender { get; set; }


        [MaxLength(50, ErrorMessage = "Name can be no longer than 50 characters.")]
        [Display(Name ="Referring Physician")]
        public string ReferingPhysician { get; set; }
       
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "The entered phone number is not valid.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        [MaxLength(10, ErrorMessage = "Please enter the phone number without symbols.")]
        
        public string ReferingPhysicianPhone { get; set; }
        
        [Display(Name = "Emergency Contact")]
        [Required(ErrorMessage = "Please enter the name of nearest relative or friend not living with patient.")]
        [MaxLength(50, ErrorMessage = "Name can be no longer than 50 characters.")]
        public string EmergencyContact { get; set; }
                
        [Display(Name = "Emergency Phone")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "The entered phone number is not valid.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        [MaxLength(10, ErrorMessage = "Please enter the phone number without symbols.")]
        public string EmergencyPhone { get; set; }
        #endregion

        [Display(Name = "Primary Insurance")]
        [MaxLength(50, ErrorMessage = "Name can be no longer than 50 characters.")]
        public string PrimaryInsurance { get; set; }

        [Display(Name = "Secondary Insurance")]
        [MaxLength(50, ErrorMessage = "Name can be no longer than 50 characters.")]
        public string SecondaryInsurance { get; set; }

        
        [DataType(DataType.PhoneNumber, ErrorMessage = "The entered phone number is not valid.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        [MaxLength(10, ErrorMessage = "Please enter the phone number without symbols.")]
        [Display(Name = "Pharmacy Phone")]
        public string PharmacyPhone { get; set; }           

        public virtual List<Visits> Visits { get; set; }
        public virtual List<HIVManagement> hivManagement { get; set; }

        public virtual List<Documents> Documents { get; set; }

        public virtual List<Immunizations> Immunizations { get; set; }

        [ReadOnly(true)]
        public bool IsOtherManagement(int? id)
        {
            var checkOtherVisits = from v in Visits
                                   where v.IsOtherManagement == true
                                   select v;

            if (checkOtherVisits.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsHIVManagement(int? id)
        {
            var checkOtherVisits = from v in Visits
                                   where v.IsHIVManagement == true
                                   select v;

            if (checkOtherVisits.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [Display(Name = "Date of HIV Diagnosis")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? HIVDiagnosisDate { get; set; }

        [Display(Name = "T-Cell at Diagnsosis")]
        public int? TCellAtDiagnosis { get; set; }

        [Display(Name = "Viral Load at Diagnosis")]
        public int? ViralLoadAtDiagnosis { get; set; }
        public bool Signature { get; set; }

        [Display(Name = "Date Signed")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateSigned { get; set; }

       

    }

    public enum PhoneNumberTypes
    {
        Home,
        Mobile,
        Other,
        None  
    }

    public enum Gender
    {
        [Display(Name = "Male")]
        M,
        [Display(Name = "Female")]
        F,
        [Display(Name = "Other")]
        O
    }

    public enum MaritalStatus
    {
        [Display(Name = "Single")]
        S,
        [Display(Name = "Married")]
        M,
        [Display(Name = "Divorced")]
        D,
        [Display(Name = "Widowed")]
        W
    }

    public enum Relation
    {
        Self,
        Spouce,
        Dependant,
        Other
    }

}