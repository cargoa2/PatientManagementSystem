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
            //var hIVManegements = db.HIVManegements.Include(h => h.Visit);
            //return View(hIVManegements.ToList());
        }

        public bool CheckForHIVRecords(int id)
        {
            List<HIVManagement> hivMan = db.HIVManegements.ToList();
            var recordsExists = hivMan.Find(i => i.PatientId == id);

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
                List<HIVManagement> hivMan = db.HIVManegements.ToList();
                var hList = hivMan.Where(h => h.PatientId == id).OrderByDescending(h => h.VisitDate);
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
                         join p in db.Patients on h.PatientId equals p.Id
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
            var patient = db.Patients.Find(hIVManagement.PatientId);
            hIVManagement.FullName = patient.FullName;
            if (hIVManagement == null)
            {
                return HttpNotFound();
            }
            return View(hIVManagement);
        }

        // GET: HIVManagements/Create
        public ActionResult Create(int id)
        {
            var patient = db.Patients.Find(id);
            HIVManagement hivManagement = new HIVManagement();
            hivManagement.PatientId = id;
            hivManagement.FullName = patient.FullName;
            hivManagement.VisitDate = DateTime.Now;
            return View(hivManagement);
        }

        // POST: HIVManagements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HIVManagmentId,PatientId,Problem,VisitDate,ICD10,MedicationStart,Medication,MedDiscDate")] HIVManagement hIVManagement)
        {
            if (ModelState.IsValid)
            {
                db.HIVManegements.Add(hIVManagement);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("HIVIndex", "HIVManagements", new { id = hIVManagement.PatientId });
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
        public ActionResult Edit([Bind(Include = "HIVManagmentId,PatientId,Problem,VisitDate,ICD10,MedicationStart,Medication,MedDiscDate")] HIVManagement hIVManagement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hIVManagement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("HIVIndex", "HIVManagements", new { id = hIVManagement.PatientId } );
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
            return RedirectToAction("HIVIndex", "HIVManagements", new { id = hIVManagement.PatientId });
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
