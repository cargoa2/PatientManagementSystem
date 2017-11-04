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
using HtmlAgilityPack;

namespace PatientManagementSystem.Controllers
{
    public class DocumentsController : Controller
    {
        private PatientContext db = new PatientContext();

        // GET: Documents
        public ActionResult Index()
        {
           // Logger.Log(LogLevel.Debug, "Starting DocumentsController Get Index.", "", "", "");
            return View(db.Documents.ToList());
        }

        // GET: Documents/Details/5
        public ActionResult Details(int? id)
        {
            //Logger.Log(LogLevel.Debug, "Starting DocumentsController Get Details.", "Patient Id = " + id.ToString(), "", "");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documents documents = db.Documents.Find(id);
            if (documents == null)
            {
                return HttpNotFound();
            }

           // Logger.Log(LogLevel.Debug, "Returning DocumentsController Get Details.", "Patient Id = " + id.ToString(), "", "");

            return View(documents);
        }        

        public bool CheckForDocuments(int id)
        {
            //Logger.Log(LogLevel.Debug, "Starting DocumentsController CheckForDocuments.", "Patient Id = " + id.ToString(), "", "");

            List<Documents> files = db.Documents.ToList();
            if(files.FindAll(p => p.PatientId == id).Count > 0)            
            {
                //Logger.Log(LogLevel.Debug, "Returning DocumentsController CheckForDocuments true.", "Patient Id = " + id.ToString(), "", "");
                return true;
            }
            else
            {
               // Logger.Log(LogLevel.Debug, "Returning DocumentsController CheckForDocuments false.", "Patient Id = " + id.ToString(), "", "");
                return false;
            }
        }


        public ActionResult DocFileIndex(int id)
        {
            //Logger.Log(LogLevel.Debug, "Starting DocumentsController DocFileIndex.", "Patient Id = " + id.ToString(), "", "");
            if (CheckForDocuments(id) == true)
            {               
                List<Documents> Documents = db.Documents.ToList();
                var lList = Documents.Where(p => p.PatientId == id)
                            .OrderByDescending(DocFile => DocFile.DocDate);
               // Logger.Log(LogLevel.Debug, "Returning DocumentsController DocFileIndex true.", "Patient Id = " + id.ToString(), "", "");
                return View("Index", lList);
            }
            else
            {
               // Logger.Log(LogLevel.Debug, "Returning DocumentsController DocFileIndex false.", "Patient Id = " + id.ToString(), "", "");
                return RedirectToAction("Create", new { id = id });
            }
        }

        public ActionResult GetDocFile(int id)
        {
            //Logger.Log(LogLevel.Debug, "Starting DocumentsController GetDocFile.", "Patient Id = " + id.ToString(), "", "");

            var fileContent = (from l in db.Documents
                               where l.DocFileId == id
                               select l.FileContent).SingleOrDefault();

            if (fileContent != null)
            {
                return File(fileContent, "application/pdf");
                
            }
            else
            {
              //  Logger.Log(LogLevel.Debug, "Returning DocumentsController GetDocFile.", "Patient Id = " + id.ToString(), "", "No File");
                return RedirectToAction("Index", "Documents", new { id = id });
            }

        }

        public ActionResult PatientDocs(int id)
        {
            //Logger.Log(LogLevel.Debug, "Starting DocumentsController PatientDocs lList.", "Patient Id = " + id.ToString(), "", "");

            List<Documents> Documents = db.Documents.ToList();
            var lList = Documents.Where(p => p.PatientId == id)
                        .OrderByDescending(DocFile => DocFile.DocDate);
            //Logger.Log(LogLevel.Debug, "Returning DocumentsController PatientDocs lList.", "Patient Id = " + id.ToString(), "", "");
            return View("Index", lList);
        }

        // GET: Documents/Create
        public ActionResult Create(int id)
        {
           // Logger.Log(LogLevel.Debug, "Starting DocumentsController Get Create.", "Patient Id = " + id.ToString(), "", "");

            var patient = db.Patients.Find(id);
            Documents Documents = new Documents();
            Documents.PatientId = id;
            Documents.FullName = patient.FullName;
            Documents.BirthDate = patient.BirthDate;
            Documents.DocDate = DateTime.Now;

           // Logger.Log(LogLevel.Debug, "Returning DocumentsController Get Create.", "Patient Id = " + id.ToString(), "", patient.FullName);

            return View(Documents);

        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocFileId,DocDate,DocType,PatientId,Notes,FilePath, FileContent")] Documents documents, HttpPostedFileBase file)
        {
           // Logger.Log(LogLevel.Debug, "Starting DocumentsController Post Create.", "Patient id = " + documents.PatientId, "", documents.DocFileId.ToString());

            if (ModelState.IsValid)
                {
                    try
                    {

                    string FileExt = Path.GetExtension(file.FileName).ToUpper();

                    if (FileExt == ".PDF")
                    {
                        Stream str = file.InputStream;
                        BinaryReader Br = new BinaryReader(str);
                        Byte[] FileDet = Br.ReadBytes((Int32)str.Length);
                        documents.FilePath = Path.GetFileName(file.FileName);
                        documents.FileContent = FileDet;
                    }
                    else
                    {
                        ViewBag.FileStatus = "Invalid file format.  Choose a pdf file.";
                        return View();
                    }
                    
                    db.Documents.Add(documents);
                    db.SaveChanges();
                   // Logger.Log(LogLevel.Debug, "Saving DocumentsController Post Create.", "Patient id = " + documents.PatientId, "", documents.DocFileId.ToString());
                    return RedirectToAction("DocFileIndex", "Documents", new { id = documents.PatientId });
                    }
                    catch(Exception ex)
                    {

                    }
                }

            //Logger.Log(LogLevel.Debug, "Returning DocumentsController Post Create model not valid.", "Patient id = " + documents.PatientId, "", "");
            return View(documents);
        }

        // GET: Documents/Edit/5
        public ActionResult Edit(int? id)
        {
            //Logger.Log(LogLevel.Debug, "Starting DocumentsController Get Edit.", "Patient id = " + id, "", "");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documents documents = db.Documents.Find(id);
            if (documents == null)
            {
               // Logger.Log(LogLevel.Debug, "DocumentsController Get Edit document = null.", "Patient id = " + id, "", "");
                return HttpNotFound();
            }

           // Logger.Log(LogLevel.Debug, "Returning DocumentsController Get Edit.", "Patient id = " + id, "", "");
            return View(documents);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocFileId,DocDate,DocType,PatientId,Notes,FilePath, FileContent")] Documents documents, HttpPostedFileBase file)
        {
           // Logger.Log(LogLevel.Debug, "Starting DocumentsController Post Edit.", "Patient id = " + documents.PatientId, "", "");
            if (ModelState.IsValid)
            {
                try
                {
                    string FileExt = Path.GetExtension(file.FileName).ToUpper();

                    if (FileExt == ".PDF")
                    {
                        Stream str = file.InputStream;
                        BinaryReader Br = new BinaryReader(str);
                        Byte[] FileDet = Br.ReadBytes((Int32)str.Length);
                        documents.FilePath = Path.GetFileName(file.FileName);
                        documents.FileContent = FileDet;                        
                    }
                    else
                    {
                        ViewBag.FileStatus = "Invalid file format.  Choose a pdf file.";
                        return View();
                    }

                    db.Entry(documents).State = EntityState.Modified;
                    db.SaveChanges();
                   // Logger.Log(LogLevel.Debug, "Saved DocumentsController Get Edit.", "Patient id = " + documents.PatientId, "", documents.DocFileId.ToString());
                    return RedirectToAction("DocFileIndex", "Documents", new { id = documents.PatientId });
                }
                catch(Exception ex)
                {

                }               
            }
            //Logger.Log(LogLevel.Debug, "Model not valid DocumentsController Get Edit.", "Patient id = " + documents.PatientId, "", documents.DocFileId.ToString());
            return View(documents);

        }

        // GET: Documents/Delete/5
        public ActionResult Delete(int? id)
        {
            //Logger.Log(LogLevel.Debug, "Starting DocumentsController Get Delete.", "Document id = " + id, "", "");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documents documents = db.Documents.Find(id);
            if (documents == null)
            {
               // Logger.Log(LogLevel.Debug, "Returning DocumentsController Get Delete not found.", "Document id = " + id, "", "");
                return HttpNotFound();
            }

           // Logger.Log(LogLevel.Debug, "Returning DocumentsController Get Delete.", "Document id = " + id, "", "");
            return View(documents);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           // Logger.Log(LogLevel.Debug, "Starting DocumentsController Post Delete.", "Document id = " + id, "", "");

            Documents documents = db.Documents.Find(id);
            db.Documents.Remove(documents);
            db.SaveChanges();
            //Logger.Log(LogLevel.Debug, "Returning DocumentsController Post Delete.", "Document id = " + id, "", "");
            return RedirectToAction("DocFileIndex", "Documents", new { id = documents.PatientId });           
        }

        protected override void Dispose(bool disposing)
        {
           // Logger.Log(LogLevel.Debug, "Starting DocumentsController Dispose.", "", "", "");
            if (disposing)
            {
                db.Dispose();
            }
           // Logger.Log(LogLevel.Debug, "Ending DocumentsController Dispose.", "", "", "");
            base.Dispose(disposing);
        }
    }
}
