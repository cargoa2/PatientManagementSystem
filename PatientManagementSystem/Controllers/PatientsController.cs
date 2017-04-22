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
            return View("Index", patients.ToList());
        }

        // GET: Patients
        public ActionResult Index()
        {
           var patients = from p in db.Patients
                            orderby p.LastName
                            orderby p.FirstName
                            select p;
            return View("Index", patients.ToList()); 
        }

        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);

            if(patient.OtherPatientPhone == null)
            {
                patient.OtherPatientPhone = "None";
                patient.OtherPhoneType = PhoneNumberTypes.None;
                var age = DateTime.Now - patient.BirthDate;
                if(patient.Employed == false)
                {
                    patient.EmployerName = "None";
                    patient.EmployerPhone = "None";
                }
            }
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
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
            if (ModelState.IsValid)
            {
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
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
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }


        // GET: Patients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
