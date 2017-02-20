using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PatientManagementSystem.Models;
using System.IO;

namespace PatientManagementSystem.Controllers
{
    public class LabFilesController : Controller
    {
        private PatientContext db = new PatientContext();

        // GET: LabFiles
        public ActionResult Index()
        {
            var patientLabFiles = db.LabFiles.Include(l => l.Visit);
            return View(patientLabFiles.ToList());
        }

        public bool CheckForLabFiles(int id)
        {
            List<LabFiles> files = db.LabFiles.ToList();
            var recordsxists = files.Find(i => i.VisitId == id);

            if(recordsxists != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ActionResult LabFileIndex(int id)
        {
            if (CheckForLabFiles(id) == true)
            {
                var LabList = (from l in db.LabFiles
                               join v in db.Visits on l.VisitId equals v.VisitId
                               where v.VisitId == id
                               select l).ToList();

                return View("Index", LabList);
            }
            else
            {
                return RedirectToAction("Create", new { id = id });
            }
        }

        public ActionResult GetPdf(int id)
        {
            string fileName = (from l in db.LabFiles
                               where l.LabFileId == id
                               select l.FileName).SingleOrDefault();


            var fileStream = new FileStream(fileName,
                                    FileMode.Open,
                                    FileAccess.Read
                                  );
            var fsResult = new FileStreamResult(fileStream, "application/pdf");
            return fsResult;

        }

        

        public ActionResult PatientLabs(int id)
        {
            var LabList = (from l in db.LabFiles
                           join v in db.Visits on l.VisitId equals v.VisitId
                           join p in db.Patients on v.PatientId equals p.Id
                           where p.Id == id
                           select l).ToList();

            return View("LabFileIndex", LabList);
        }

        // GET: LabFiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LabFiles labFiles = db.LabFiles.Find(id);
            var visit = db.Visits.Find(labFiles.VisitId);
           
            if (labFiles == null)
            {
                return HttpNotFound();
            }
            return View(labFiles);
        }

        // GET: LabFiles/Create
        public ActionResult Create(int id)
        {
            var visit = db.Visits.Find(id);
            LabFiles labFiles = new LabFiles();
            labFiles.VisitId = id;
            labFiles.FullName = visit.patient.FullName;
            return View(labFiles);
        }

        // POST: LabFiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LabFileId,VisitId,FileName")] LabFiles labFiles, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                labFiles.FileName = System.IO.Path.GetFullPath(upload.FileName);               

                db.LabFiles.Add(labFiles);
                db.SaveChanges();
                return RedirectToAction("LabFileIndex", "LabFiles", new { id = labFiles.VisitId });
                //return RedirectToAction("Index");
            }
            return View(labFiles);
        }

        // GET: LabFiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LabFiles labFiles = db.LabFiles.Find(id);
            if (labFiles == null)
            {
                return HttpNotFound();
            }            
            return View(labFiles);
        }

        // POST: LabFiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LabFileId,VisitId,FileName")] LabFiles labFiles, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                labFiles.FileName = System.IO.Path.GetFullPath(upload.FileName);

                db.Entry(labFiles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("LabFileIndex", "LabFiles", new { id = labFiles.VisitId });
            }
            
            return View(labFiles);
        }

        // GET: LabFiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LabFiles labFiles = db.LabFiles.Find(id);
            if (labFiles == null)
            {
                return HttpNotFound();
            }
            return View(labFiles);
        }

        // POST: LabFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LabFiles labFiles = db.LabFiles.Find(id);
            db.LabFiles.Remove(labFiles);
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
