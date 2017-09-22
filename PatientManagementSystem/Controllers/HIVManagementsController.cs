using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PatientManagementSystem.Models;

namespace PatientManagementSystem.Controllers
{
    public class HIVManagementsController : Controller
    {
        private PatientContext db = new PatientContext();

        // GET: HIVManagements
        public ActionResult Index()
        {
            return View(db.HIVManegements.ToList());           
        }

        public bool CheckForHIVRecords(int id)
        {
            Logger.Log(LogLevel.Debug, "Starting HIVManagementsController CheckForHIVRecords.", "Patient Id = " + id.ToString(), "", "");

            List<HIVManagement> hivMan = db.HIVManegements.ToList();
            var recordsExists = hivMan.Find(i => i.PatientId == id);

            if (recordsExists != null)
            {
                Logger.Log(LogLevel.Debug, "Returning HIVManagementsController CheckForHIVRecords.", "Patient Id = " + id.ToString(), "", "True");
                return true;
            }
            else
            {
                Logger.Log(LogLevel.Debug, "Returning HIVManagementsController CheckForHIVRecords.", "Patient Id = " + id.ToString(), "", "False");
                return false;
            }
        }

        public ActionResult HIVIndex(int id)
        {
            Logger.Log(LogLevel.Debug, "Starting HIVManagementsController HIVIndex.", "Patient Id = " + id.ToString(), "", "");
            if (CheckForHIVRecords(id) == true)
            {
                List<HIVManagement> hivMan = db.HIVManegements.ToList();
                List<Visits> visits = db.Visits.ToList();
                var hList = hivMan.Where(h => h.PatientId == id).OrderByDescending(h => h.VisitDate);

                Logger.Log(LogLevel.Debug, "Returning HIVManagementsController HIVIndex.", "Patient Id = " + id.ToString(), "", "hList");
                return View("Index", hList);                
            }
            else
            {
                Logger.Log(LogLevel.Debug, "Returning HIVManagementsController HIVIndex.", "", "", "Create");
                return RedirectToAction("Create", new { id = id });
            }
        }

        public ActionResult PatientHIVManagement(int id)
        {
            Logger.Log(LogLevel.Debug, "Starting HIVManagementsController PatientHIVManagement.", "Patient Id = " + id.ToString(), "", "");
            var pList = (from h in db.HIVManegements                        
                         join p in db.Patients on h.PatientId equals p.Id
                         where p.Id == id
                         select h).ToList();
            Logger.Log(LogLevel.Debug, "Returning HIVManagementsController PatientHIVManagement.", "Patient Id = " + id.ToString(), "", "pList");
            return View("PatientHIVIndex", pList);
        }
        // GET: HIVManagements/Details/5
        public ActionResult Details(int? id)
        {
            Logger.Log(LogLevel.Debug, "Starting HIVManagementsController Get Details.", "Patient Id = " + id.ToString(), "", "");
            if (id == null)
            {
                Logger.Log(LogLevel.Debug, "Returning HIVManagementsController Get Details.", "Patient Id = " + id.ToString(), "", "Bad Request");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HIVManagement hIVManagement = db.HIVManegements.Find(id);
            var patient = db.Patients.Find(hIVManagement.PatientId);
            hIVManagement.FullName = patient.FullName;
            if (hIVManagement == null)
            {
                Logger.Log(LogLevel.Debug, "Returning HIVManagementsController Get Details.", "Patient Id = " + id.ToString(), "", "HttpNotFound");
                return HttpNotFound();
            }
            Logger.Log(LogLevel.Debug, "Returning HIVManagementsController Get Details.", "Patient Id = " + id.ToString(), "", "");
            return View(hIVManagement);
        }

        // GET: HIVManagements/Create
        public ActionResult Create(int id)
        {
            Logger.Log(LogLevel.Debug, "Starting HIVManagementsController Get Create.", "Patient Id = " + id.ToString(), "", "");
            var patient = db.Patients.Find(id);
            HIVManagement hivManagement = new HIVManagement();
            hivManagement.PatientId = id;
            hivManagement.FullName = patient.FullName;
            hivManagement.BirthDate = patient.BirthDate;
            hivManagement.VisitDate = DateTime.Now;
            Logger.Log(LogLevel.Debug, "Returning HIVManagementsController Get Create.", "Patient Id = " + id.ToString(), "", "");
            return View(hivManagement);
        }

        // POST: HIVManagements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HIVManagmentId,PatientId,Problem,VisitDate,ICD10,MedicationStart,Medication,MedDiscDate")] HIVManagement hIVManagement)
        {
            Logger.Log(LogLevel.Debug, "Starting HIVManagementsController Post Create.", "Patient Id = " + hIVManagement.PatientId.ToString(), "", "");
            if (ModelState.IsValid)
            {
                db.HIVManegements.Add(hIVManagement);
                db.SaveChanges();
                Logger.Log(LogLevel.Debug, "Returning HIVManagementsController Post Create.", "Patient Id = " + hIVManagement.PatientId.ToString(), "", "Modeil is valid");
                return RedirectToAction("HIVIndex", "HIVManagements", new { id = hIVManagement.PatientId });
            }
            Logger.Log(LogLevel.Debug, "Returning HIVManagementsController Post Create.", "Patient Id = " + hIVManagement.PatientId.ToString(), "", "");
            return View(hIVManagement);
        }

        // GET: HIVManagements/Edit/5
        public ActionResult Edit(int? id)
        {
            Logger.Log(LogLevel.Debug, "Starting HIVManagementsController Get Edit.", "Patient Id = " + id.ToString(), "", "");
            if (id == null)
            {
                Logger.Log(LogLevel.Debug, "returning HIVManagementsController Get Edit.", "Patient Id = " + id.ToString(), "", "Bad Request");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HIVManagement hIVManagement = db.HIVManegements.Find(id);
            if (hIVManagement == null)
            {
                Logger.Log(LogLevel.Debug, "returning HIVManagementsController Get Edit.", "Patient Id = " + id.ToString(), "", "HttpNotFound");
                return HttpNotFound();
            }
            Logger.Log(LogLevel.Debug, "returning HIVManagementsController Get Edit.", "Patient Id = " + id.ToString(), "", "");
            return View(hIVManagement);
        }

        // POST: HIVManagements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HIVManagmentId,PatientId,Problem,VisitDate,ICD10,MedicationStart,Medication,MedDiscDate")] HIVManagement hIVManagement)
        {
            Logger.Log(LogLevel.Debug, "Starting HIVManagementsController Post Edit.", "Patient Id = " + hIVManagement.PatientId.ToString(), "", "");
            if (ModelState.IsValid)
            {
                db.Entry(hIVManagement).State = EntityState.Modified;
                db.SaveChanges();
                Logger.Log(LogLevel.Debug, "Returning HIVManagementsController Post Edit.", "Patient Id = " + hIVManagement.PatientId.ToString(), "", "Saved Changes");
                return RedirectToAction("HIVIndex", "HIVManagements", new { id = hIVManagement.PatientId } );
            }
            Logger.Log(LogLevel.Debug, "Returning HIVManagementsController Post Edit.", "Patient Id = " + hIVManagement.PatientId.ToString(), "", "s");
            return View(hIVManagement);
        }

        // GET: HIVManagements/Delete/5
        public ActionResult Delete(int? id)
        {
            Logger.Log(LogLevel.Debug, "Starting HIVManagementsController Get Delete.", "Patient Id = " +id.ToString(), "", "");
            if (id == null)
            {
                Logger.Log(LogLevel.Debug, "Returning HIVManagementsController Get Delete.", "Patient Id = " + id.ToString(), "", "Bad Request");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HIVManagement hIVManagement = db.HIVManegements.Find(id);
            if (hIVManagement == null)
            {
                Logger.Log(LogLevel.Debug, "Returning HIVManagementsController Get Delete.", "Patient Id = " + id.ToString(), "", "HttpNotFound");
                return HttpNotFound();
            }
            Logger.Log(LogLevel.Debug, "Returning HIVManagementsController Get Delete.", "Patient Id = " + id.ToString(), "", "");
            return View(hIVManagement);
        }

        // POST: HIVManagements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Logger.Log(LogLevel.Debug, "Starting HIVManagementsController Post Delete.", "Patient Id = " + id.ToString(), "", "");
            HIVManagement hIVManagement = db.HIVManegements.Find(id);
                db.HIVManegements.Remove(hIVManagement);
                db.SaveChanges();
            Logger.Log(LogLevel.Debug, "Returning HIVManagementsController Post Delete.", "Patient Id = " + id.ToString(), "", "");
            return RedirectToAction("HIVIndex", "HIVManagements", new { id = hIVManagement.PatientId });
        }

        protected override void Dispose(bool disposing)
        {
            Logger.Log(LogLevel.Debug, "Starting HIVManagementsController Dispose.", "", "", "");
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
            Logger.Log(LogLevel.Debug, "Ending HIVManagementsController Dispose.", "", "", "");
        }
    }
}
