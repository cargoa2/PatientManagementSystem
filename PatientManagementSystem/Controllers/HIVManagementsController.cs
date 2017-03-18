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
            var hIVManegements = db.HIVManegements.Include(h => h.Visit);
            return View(hIVManegements.ToList());
        }

        public bool CheckForHIVRecords(int id)
        {
            List<HIVManagement> hivMan = db.HIVManegements.ToList();
            var recordsExists = hivMan.Find(i => i.VisitId == id);

            if (recordsExists != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ActionResult HIVIndex(int id)
        {
            if (CheckForHIVRecords(id) == true)
            {
                var hList = (from h in db.HIVManegements
                             join v in db.Visits on h.VisitId equals v.VisitId
                             where v.VisitId == id
                             select h).ToList();

                return View("Index", hList);                
            }
            else
            {
                return RedirectToAction("Create", new { id = id });
            }
        }

        public ActionResult PatientHIVManagement(int id)
        {
            var pList = (from h in db.HIVManegements
                         join v in db.Visits on h.VisitId equals v.VisitId
                         join p in db.Patients on v.PatientId equals p.Id
                         where p.Id == id
                         select h).ToList();

            return View("PatientHIVIndex", pList);
        }
        // GET: HIVManagements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HIVManagement hIVManagement = db.HIVManegements.Find(id);
            var visit = db.Visits.Find(hIVManagement.VisitId);
            hIVManagement.FullName = visit.patient.FullName;
            if (hIVManagement == null)
            {
                return HttpNotFound();
            }
            return View(hIVManagement);
        }

        // GET: HIVManagements/Create
        public ActionResult Create(int id)
        {
            var visit = db.Visits.Find(id);
            HIVManagement hivManagement = new HIVManagement();
            hivManagement.VisitId = id;
            hivManagement.FullName = visit.patient.FullName;
            hivManagement.VisitDate = visit.VisitDate;
            hivManagement.PatientId = visit.PatientId;
            return View(hivManagement);
        }

        // POST: HIVManagements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HIVManagmentId,VisitId,Problem,ICD10,MedicationStart,Medication,DiagnosisCode")] HIVManagement hIVManagement)
        {
            if (ModelState.IsValid)
            {
                db.HIVManegements.Add(hIVManagement);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("HIVIndex", "HIVManagements", new { id = hIVManagement.VisitId });
            }           
            return View(hIVManagement);
        }

        // GET: HIVManagements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HIVManagement hIVManagement = db.HIVManegements.Find(id);
            if (hIVManagement == null)
            {
                return HttpNotFound();
            }            
            return View(hIVManagement);
        }

        // POST: HIVManagements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HIVManagmentId,VisitId,Problem,ICD10,MedicationStart,Medication,DiagnosisCode")] HIVManagement hIVManagement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hIVManagement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("HIVIndex", "HIVManagements", new { id = hIVManagement.VisitId } );
            }
            return View(hIVManagement);
        }

        // GET: HIVManagements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HIVManagement hIVManagement = db.HIVManegements.Find(id);
            if (hIVManagement == null)
            {
                return HttpNotFound();
            }
            return View(hIVManagement);
        }

        // POST: HIVManagements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
                HIVManagement hIVManagement = db.HIVManegements.Find(id);
                db.HIVManegements.Remove(hIVManagement);
                db.SaveChanges();
            return RedirectToAction("HIVIndex", "HIVManagements", new { id = hIVManagement.VisitId });
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
