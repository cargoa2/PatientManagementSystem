using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PatientManagementSystem.Models;
using System.Globalization;

namespace PatientManagementSystem.Controllers
{
    public class PatientsController : Controller
    {       
        private PatientContext db = new PatientContext();

        public ActionResult Search(string SearchBox)
        {
         //   Logger.Log(LogLevel.Debug, "Starting PatientsController Search.", "Search text = " + SearchBox.ToString(), "", "");
            var patients = from p in db.Patients select p;

            DateTime searchBirthDate;

            if(!string.IsNullOrEmpty(SearchBox))
            {

                bool isDateSearch = DateTime.TryParse(SearchBox, out searchBirthDate);
                if (isDateSearch)
                {
                    patients = patients.Where(p => p.BirthDate.Equals(searchBirthDate));
                }
                else
                {
                    patients = from p in db.Patients
                                where
                                p.LastName.Contains(SearchBox)
                                || p.FirstName.Contains(SearchBox)
                                || p.SOC.Contains(SearchBox)
                                select p;
                }
            }
         //   Logger.Log(LogLevel.Debug, "Returning PatientsController Search.", "Search text = " + SearchBox.ToString(), "", "");
            return View("Index", patients.ToList());
        }

        // GET: Patients
        public ActionResult Index()
        {
      //      Logger.Log(LogLevel.Debug, "Starting PatientsController Index.", "", "", "");
            var patients = from p in db.Patients
                            orderby p.LastName
                            select p;
       //     Logger.Log(LogLevel.Debug, "Returning PatientsController Index.", "", "", "");
            return View("Index", patients.ToList()); 
        }

        public ActionResult PatientIndex(int id)
        {
      //      Logger.Log(LogLevel.Debug, "Starting PatientsController PatientIndex.", "Patient Id = " + id.ToString(), "", "");
            var patients = from p in db.Patients
                           where p.Id == id
                           select p;
      //      Logger.Log(LogLevel.Debug, "Returning PatientsController PatientIndex.", "Patient Id = " + id.ToString(), "", "");
            return View("Index", patients);
        }

        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
      //      Logger.Log(LogLevel.Debug, "Starting PatientsController Get Details.", "Patient Id = " + id.ToString(), "", "");
            if (id == null)
            {
     //           Logger.Log(LogLevel.Debug, "Returning PatientsController Get Details.", "Patient Id = " + id.ToString(), "", "BadRequest");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);

            if (patient.PatientPhone != null)
            {
                patient.PatientPhone = FormatPhoneNumber(patient.PatientPhone);
            }
            if (patient.OtherPatientPhone != null)
            {
                patient.OtherPatientPhone = FormatPhoneNumber(patient.OtherPatientPhone);
            }
            if (patient.EmployerPhone != null)
            {
                patient.EmployerPhone = FormatPhoneNumber(patient.EmployerPhone);
            }
            if (patient.ReferingPhysicianPhone != null)
            {
                patient.ReferingPhysicianPhone = FormatPhoneNumber(patient.ReferingPhysicianPhone);
            }
            if (patient.EmployerPhone != null)
            {
                patient.EmergencyPhone = FormatPhoneNumber(patient.EmergencyPhone);
            }
            if (patient.PharmacyPhone != null)
            {
                patient.PharmacyPhone = FormatPhoneNumber(patient.PharmacyPhone);
            }

            if (patient.OtherPatientPhone == null)
            {
                patient.OtherPatientPhone = "None";
                patient.OtherPhoneType = PhoneNumberTypes.None;               
            }
            var age = DateTime.Now - patient.BirthDate;
            if (patient.Employed == false)
            {
                patient.EmployerName = "None";
                patient.EmployerPhone = "None";
            }
            if (patient == null)
            {
       //         Logger.Log(LogLevel.Debug, "Returning PatientsController Get Details.", "Patient Id = " + id.ToString(), "", "HttpNotFound");
                return HttpNotFound();
            }
       //     Logger.Log(LogLevel.Debug, "Returning PatientsController Get Details.", "Patient Id = " + id.ToString(), "", "");
            return View(patient);
        }

        public static string FormatPhoneNumber(string number)
        {
            string formatNumber = "";
      //      Logger.Log(LogLevel.Debug, "Starting PatientsController FormatPhoneNumber.", "String number = " + number.ToString(), "", "formatPhoneNumber");
            if (number != null)
            {
                formatNumber = "(" + number.Substring(0, 3) + ")" +
                                  " " + number.Substring(3, 3) + "-" +
                                        number.Substring(6, 4);
       //         Logger.Log(LogLevel.Debug, "Returning PatientsController FormatPhoneNumber.", "String number = " + number.ToString(), "", formatNumber);
                //return formatNumber;
            }
            return formatNumber;

            //else
            //{
            //    Logger.Log(LogLevel.Debug, "Returning PatientsController FormatPhoneNumber.", "String number = " + number.ToString(), "", "null");
            //    return null;
            //}

        }

        // GET: Patients/Create
        public ActionResult Create()
        {
       //     Logger.Log(LogLevel.Debug, "PatientsController Get Create.", "", "", "");
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Active,LastName,FirstName,MiddleInitial,StreetAddress,City,State,PostalCode,PatientPhone,PatientPhoneType,OtherPatientPhone, OtherPhoneType," +
            " BirthDate,SOC,Gender,Status,Employed,EmployerName,EmployerPhone,Relation,SubscriberLastName,SubscriberFirstName,SubscriberMiddleInitial,SubscriberBirthDate,SubscriberSOC, " +
            " SubscriberGender,ReferingPhysician,ReferingPhysicianPhone,EmergencyContact,EmergencyPhone,PrimaryInsurance,SecondaryInsurance,PharmacyPhone,HIVDiagnosisDate,TCellAtDiagnosis,ViralLoadAtDiagnosis,Signature,DateSigned")] Patient patient)
        {
      //      Logger.Log(LogLevel.Debug, "Starting PatientsController Post Create.", "", "", "");
            if (ModelState.IsValid)
            {
                db.Patients.Add(patient);
                db.SaveChanges();
     //           Logger.Log(LogLevel.Debug, "Returning PatientsController Post Create.", "", "", "Saved changes");
                return RedirectToAction("Index");
            }

     //       Logger.Log(LogLevel.Debug, "Returning PatientsController Post Create.", "", "", "");
            return View(patient);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
      //      Logger.Log(LogLevel.Debug, "Starting PatientsController Get Edit.", "Patient Id = " + id.ToString(), "", "");
            if (id == null)
            {
     //           Logger.Log(LogLevel.Debug, "Returning PatientsController Get Edit.", "Patient Id = " + id.ToString(), "", "BadRequest");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
      //          Logger.Log(LogLevel.Debug, "Returning PatientsController Get Edit.", "Patient Id = " + id.ToString(), "", "HttpNotFound");
                return HttpNotFound();
            }

      //      Logger.Log(LogLevel.Debug, "Returning PatientsController Get Edit.", "Patient Id = " + id.ToString(), "", "");
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Active,LastName,FirstName,MiddleInitial,StreetAddress,City,State,PostalCode,PatientPhone,PatientPhoneType,OtherPatientPhone,OtherPhoneType," +
                                        "BirthDate,SOC,Gender,Status,Employed,EmployerName,EmployerPhone,Relation,SubscriberLastName,SubscriberMiddleInitial,SubscriberBirthDate,SubscriberSOC," +
                                        "SubscriberGender,ReferingPhysician,ReferingPhysicianPhone,EmergencyContact,EmergencyPhone,PrimaryInsurance,SecondaryInsurance,PharmacyPhone,HIVDiagnosisDate,TCellAtDiagnosis,ViralLoadAtDiagnosis,Signature,DateSigned")] Patient patient)
        {
    //        Logger.Log(LogLevel.Debug, "Starting PatientsController Post Edit.", "Patient Id = " + patient.Id.ToString(), "", "");
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
    //            Logger.Log(LogLevel.Debug, "Starting PatientsController Post Edit.", "Patient Id = " + patient.Id.ToString(), "", "Save changes");
                return RedirectToAction("Index");
            }
    //        Logger.Log(LogLevel.Debug, "Returning PatientsController Post Edit.", "Patient Id = " + patient.Id.ToString(), "", "");
            return View(patient);
        }


        // GET: Patients/Delete/5
        public ActionResult Delete(int? id)
        {
      //      Logger.Log(LogLevel.Debug, "Starting PatientsController Get Delete.", "Patient Id = " + id.ToString(), "", "");
            if (id == null)
            {
      //          Logger.Log(LogLevel.Debug, "Returning PatientsController Get Delete.", "Patient Id = " + id.ToString(), "", "BadRequest");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
     //           Logger.Log(LogLevel.Debug, "Returning PatientsController Get Delete.", "Patient Id = " + id.ToString(), "", "HttpNotFound");
                return HttpNotFound();
            }
    //        Logger.Log(LogLevel.Debug, "Returning PatientsController Get Delete.", "Patient Id = " + id.ToString(), "", "");
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
    //        Logger.Log(LogLevel.Debug, "Starting PatientsController Post Delete.", "Patient Id = " + id.ToString(), "", "");
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
            db.SaveChanges();
    //        Logger.Log(LogLevel.Debug, "Returning PatientsController Post Delete.", "Patient Id = " + id.ToString(), "", "Saved changes");
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
    //        Logger.Log(LogLevel.Debug, "Starting PatientsController Dispose.", "", "", "");
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
    //        Logger.Log(LogLevel.Debug, "Ending PatientsController Dispose.", "", "", "");
        }
    }
}
