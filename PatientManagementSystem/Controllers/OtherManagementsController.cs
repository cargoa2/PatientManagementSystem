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
    public class OtherManagementsController : Controller
    {
        private PatientContext db = new PatientContext();

        // GET: OtherManagements
        public ActionResult Index()
        {           
            return View(db.OtherManagements.ToList());
        }

        public bool CheckForOtherManagementRecords(int id)
        {
            Logger.Log(LogLevel.Debug, "Starting OtherManagementsController CheckForOtherManagementRecords.", "Patient Id = " + id.ToString(), "", "");
            List<OtherManagement> otherMan = db.OtherManagements.ToList();
            var recordsExists = otherMan.Find(o => o.PatientId == id);

            if(recordsExists != null)
            {
                Logger.Log(LogLevel.Debug, "returning OtherManagementsController CheckForOtherManagementRecords.", "Patient Id = " + id.ToString(), "", "True");
                return true;
            }
            else
            {
                Logger.Log(LogLevel.Debug, "returning OtherManagementsController CheckForOtherManagementRecords.", "Patient Id = " + id.ToString(), "", "False");
                return false;
            }
        }

        public ActionResult OtherIndex(int id)
        {
            Logger.Log(LogLevel.Debug, "Starting OtherManagementsController OtherIndex.", "Patient Id = " + id.ToString(), "", "");
            if (CheckForOtherManagementRecords(id) == true)
            {
                List<OtherManagement> othMan = db.OtherManagements.ToList();
                var oList = othMan.Where(o => o.PatientId == id).OrderByDescending(o => o.VisitDate);
                Logger.Log(LogLevel.Debug, "Returning OtherManagementsController OtherIndex.", "Patient Id = " + id.ToString(), "", "");
                return View("Index", oList);
            }
            else
            {
                Logger.Log(LogLevel.Debug, "Returning OtherManagementsController OtherIndex.", "", "", "Create");
                return RedirectToAction("Create", new { id = id });
            }
        }

        public ActionResult PatientOtherManagement(int id)
        {
            Logger.Log(LogLevel.Debug, "Starting OtherManagementsController PatientOtherManagement.", "Patient Id = " + id.ToString(), "", "");
            var pList = (from o in db.OtherManagements                         
                         join p in db.Patients on o.PatientId equals p.Id
                         where p.Id == id
                         select o).ToList();
            Logger.Log(LogLevel.Debug, "Returning OtherManagementsController PatientOtherManagement.", "Patient Id = " + id.ToString(), "", "pList");
            return View("PatientOtherIndex", pList);
        }
        

        // GET: OtherManagements/Details/5
        public ActionResult Details(int? id)
        {
            Logger.Log(LogLevel.Debug, "Starting OtherManagementsController Get Details.", "Patient Id = " + id.ToString(), "", "");
            if (id == null)
            {
                Logger.Log(LogLevel.Debug, "Returning OtherManagementsController Get Details.", "Patient Id = " + id.ToString(), "", "Bad Request");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            OtherManagement otherManagement = db.OtherManagements.Find(id);
            var patient = db.Patients.Find(otherManagement.PatientId);
            otherManagement.FullName = patient.FullName;
            if (otherManagement == null)
            {
                Logger.Log(LogLevel.Debug, "Returning OtherManagementsController Get Details.", "Patient Id = " + id.ToString(), "", "HttpNotFound");
                return HttpNotFound();
            }

            Logger.Log(LogLevel.Debug, "Returning OtherManagementsController Get Details.", "Patient Id = " + id.ToString(), "", "");
            return View(otherManagement);
        }

        // GET: OtherManagements/Create
        public ActionResult Create(int id)
        {
            Logger.Log(LogLevel.Debug, "Starting OtherManagementsController Get Create.", "Patient Id = " + id.ToString(), "", "");
            var patient = db.Patients.Find(id);
            OtherManagement otherManagement = new OtherManagement();
            otherManagement.PatientId = id;
            otherManagement.FullName = patient.FullName;
            otherManagement.BirthDate = patient.BirthDate;
            otherManagement.VisitDate = DateTime.Now;
            Logger.Log(LogLevel.Debug, "Returning OtherManagementsController Get Create.", "Patient Id = " + id.ToString(), "", "");
            return View(otherManagement);
        }

        // POST: OtherManagements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OtherId,PatientId,VisitDate,TCell,ViralLoad,WhiteBloodCell,Hemoglobin,PlasmaIronTurnover,OtherWeight,Triglycerides,TotalCholesterol,OtherDocuments")] OtherManagement otherManagement)
        {
            Logger.Log(LogLevel.Debug, "Starting OtherManagementsController Post Create.", "Patient Id = " + otherManagement.PatientId.ToString(), "", "");
            if (ModelState.IsValid)
            {
                db.OtherManagements.Add(otherManagement);
                db.SaveChanges();
                Logger.Log(LogLevel.Debug, "Returning OtherManagementsController Post Create.", "Patient Id = " + otherManagement.PatientId.ToString(), "", "Saved changes");
                return RedirectToAction("OtherIndex", "OtherManagements", new { id = otherManagement.PatientId });
            }

            Logger.Log(LogLevel.Debug, "Returning OtherManagementsController Post Create.", "Patient Id = " + otherManagement.PatientId.ToString(), "", "");
            return View(otherManagement);
        }

        // GET: OtherManagements/Edit/5
        public ActionResult Edit(int? id)
        {
            Logger.Log(LogLevel.Debug, "Starting OtherManagementsController Get Edit.", "Patient Id = " + id.ToString(), "", "");
            if (id == null)
            {
                Logger.Log(LogLevel.Debug, "Returning OtherManagementsController Get Edit.", "Patient Id = " + id.ToString(), "", "Bad Request");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtherManagement otherManagement = db.OtherManagements.Find(id);
            if (otherManagement == null)
            {
                Logger.Log(LogLevel.Debug, "Returning OtherManagementsController Get Edit.", "Patient Id = " + id.ToString(), "", "HttpNotFound");
                return HttpNotFound();
            }

            Logger.Log(LogLevel.Debug, "Returning OtherManagementsController Get Edit.", "Patient Id = " + id.ToString(), "", "");
            return View(otherManagement);
        }

        // POST: OtherManagements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OtherId,PatientId,VisitDate,TCell,ViralLoad,WhiteBloodCell,Hemoglobin,PlasmaIronTurnover,OtherWeight,Triglycerides,TotalCholesterol,OtherDocuments")] OtherManagement otherManagement)
        {
            Logger.Log(LogLevel.Debug, "Starting OtherManagementsController Post Edit.", "Patient Id = " + otherManagement.PatientId.ToString(), "", "");
            if (ModelState.IsValid)
            {
                db.Entry(otherManagement).State = EntityState.Modified;
                db.SaveChanges();
                Logger.Log(LogLevel.Debug, "Returning OtherManagementsController Post Edit.", "Patient Id = " + otherManagement.PatientId.ToString(), "", "Saved Changes");
                return RedirectToAction("OtherIndex", "OtherManagements", new {id = otherManagement.PatientId } );              
            }
            Logger.Log(LogLevel.Debug, "Returning OtherManagementsController Post Edit.", "Patient Id = " + otherManagement.PatientId.ToString(), "", "");
            return View(otherManagement);
        }

        // GET: OtherManagements/Delete/5
        public ActionResult Delete(int? id)
        {
            Logger.Log(LogLevel.Debug, "Starting OtherManagementsController Get Delete.", "Patient Id = " + id.ToString(), "", "");
            if (id == null)
            {
                Logger.Log(LogLevel.Debug, "Returning OtherManagementsController Get Delete.", "Patient Id = " + id.ToString(), "", "Bad Request");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtherManagement otherManagement = db.OtherManagements.Find(id);
            if (otherManagement == null)
            {
                Logger.Log(LogLevel.Debug, "Returning OtherManagementsController Get Delete.", "Patient Id = " + id.ToString(), "", "HttpNotFound");
                return HttpNotFound();
            }
            Logger.Log(LogLevel.Debug, "Returning OtherManagementsController Get Delete.", "Patient Id = " + id.ToString(), "", "");
            return View(otherManagement);
        }

        // POST: OtherManagements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Logger.Log(LogLevel.Debug, "Starting OtherManagementsController Post Delete.", "Patient Id = " + id.ToString(), "", "");
            OtherManagement otherManagement = db.OtherManagements.Find(id);
            db.OtherManagements.Remove(otherManagement);
            db.SaveChanges();
            Logger.Log(LogLevel.Debug, "Returning OtherManagementsController Post Delete.", "Patient Id = " + id.ToString(), "", "Saved changes");
            return RedirectToAction("OtherIndex", "OtherManagements", new { id = otherManagement.PatientId });
        }

        protected override void Dispose(bool disposing)
        {
            Logger.Log(LogLevel.Debug, "Starting OtherManagementsController Dispose.", "", "", "");
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
            Logger.Log(LogLevel.Debug, "Ending OtherManagementsController Dispose.", "", "", "");
        }
    }
}
