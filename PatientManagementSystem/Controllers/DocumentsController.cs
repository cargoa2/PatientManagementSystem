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
    public class DocumentsController : Controller
    {
        private PatientContext db = new PatientContext();

        // GET: Documents
        public ActionResult Index()
        {
            var documents = db.Documents.Include(d => d.Patient);
            return View(documents.ToList());            
        }

        // GET: Documents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documents documents = db.Documents.Find(id);
            if (documents == null)
            {
                return HttpNotFound();
            }
            return View(documents);
        }

       

        public bool CheckForDocuments(int id)
        {
            List<Documents> files = db.Documents.ToList();
            var recordsxists = files.Find(i => i.PatientId == id);

            if (recordsxists != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public ActionResult DocFileIndex(int id)
        {
            if (CheckForDocuments(id) == true)
            {
                List<Documents> Documents = db.Documents.ToList();
                var lList = Documents.Where(p => p.PatientId == id)
                            .OrderByDescending(DocFile => DocFile.DocDate);
                return View("Index", lList);
            }
            else
            {
                return RedirectToAction("Create", new { id = id });
            }
        }

        public ActionResult GetDocFile(int id)
        {
            string fileName = (from l in db.Documents
                               where l.DocFileId == id
                               select l.FilePath).SingleOrDefault();


            var fileStream = new FileStream(fileName,
                                    FileMode.Open,
                                    FileAccess.Read
                                  );
            var fsResult = new FileStreamResult(fileStream, "application/pdf");
            return fsResult;

        }

        public ActionResult PatientDocs(int id)
        {
            List<Documents> Documents = db.Documents.ToList();
            var lList = Documents.Where(p => p.PatientId == id)
                        .OrderByDescending(DocFile => DocFile.DocDate);
            return View("Index", lList);
        }

        // GET: Documents/Create
        public ActionResult Create(int id)
        {
            var patient = db.Patients.Find(id);
            Documents Documents = new Documents();
            Documents.PatientId = id;
            Documents.FullName = patient.FullName;
            Documents.DocDate = DateTime.Now;
            return View(Documents);

        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocFileId,DocDate,DocType,PatientId,Notes,FilePath")] Documents Documents, HttpPostedFileBase file)
        {                          
                if (ModelState.IsValid)
                {
                    try
                    {
                        Documents.FilePath = Path.GetFullPath(file.FileName);
                        db.Documents.Add(Documents);
                        db.SaveChanges();
                        return RedirectToAction("Index", "Documents", new { id = Documents.PatientId });
                    }
                    catch(Exception ex)
                    {

                    }
                }                
                return View(Documents);
        }

        // GET: Documents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documents documents = db.Documents.Find(id);
            if (documents == null)
            {
                return HttpNotFound();
            }
                  
            return View(documents);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocFileId,DocDate,DocType,PatientId,Notes,FilePath")] Documents documents, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    documents.FilePath = Path.GetFullPath(file.FileName);
                    db.Entry(documents).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Documents", new { id = documents.PatientId });
                }
                catch(Exception ex)
                {

                }               
            }
            return View(documents);

        }

        // GET: Documents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documents documents = db.Documents.Find(id);
            if (documents == null)
            {
                return HttpNotFound();
            }
            return View(documents);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Documents documents = db.Documents.Find(id);
            db.Documents.Remove(documents);
            db.SaveChanges();
            return RedirectToAction("Index", "Documents", new { id = documents.PatientId });
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
