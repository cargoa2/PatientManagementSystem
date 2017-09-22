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
    public class ImmunizationsController : Controller
    {
        private PatientContext db = new PatientContext();

        // GET: Immunizations
        public ActionResult Index()
        {
            return View(db.Immunizations.ToList());
        }

        // GET: Immunizations/Details/5
        public ActionResult Details(int? id)
        {
            Logger.Log(LogLevel.Debug, "Starting ImmunizationsController Get Details.", "Patient Id = " + id.ToString(), "", "");
            if (id == null)
            {
                Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Get Details.", "Patient Id = " + id.ToString(), "", "BadRequest");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Immunizations immunizations = db.Immunizations.Find(id);
            if (immunizations == null)
            {
                Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Get Details.", "Patient Id = " + id.ToString(), "", "HttpNotFound");
                return HttpNotFound();
            }
            Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Get Details.", "Patient Id = " + id.ToString(), "", "immunizations");
            return View(immunizations);
        }

        public bool CheckforImmunizations(int id)
        {
            Logger.Log(LogLevel.Debug, "Starting ImmunizationsController CheckforImmunizations.", "Patient Id = " + id.ToString(), "", "");
            List<Immunizations> im = db.Immunizations.ToList();
            if(im.FindAll(p => p.PatientId == id).Count > 0)
            {
                Logger.Log(LogLevel.Debug, "Returning ImmunizationsController CheckforImmunizations.", "Patient Id = " + id.ToString(), "", "True");
                return true;
            }
            else
            {
                Logger.Log(LogLevel.Debug, "Returning ImmunizationsController CheckforImmunizations.", "Patient Id = " + id.ToString(), "", "False");
                return false;
            }
        }

        public ActionResult ImmIndex(int id)
        {
            Logger.Log(LogLevel.Debug, "Starting ImmunizationsController ImmIndex.", "Patient Id = " + id.ToString(), "", "");

            if (CheckforImmunizations(id) == true)
            {
                List<Immunizations> immunizations = db.Immunizations.ToList();
                var iList = immunizations.Where(i => i.PatientId == id)
                            .OrderByDescending(ImmDate => ImmDate.ImmDate);
                Logger.Log(LogLevel.Debug, "Returning ImmunizationsController ImmIndex.", "Patient Id = " + id.ToString(), "", "iList");
                return View("Index", iList);
            }
            else
            {
                Logger.Log(LogLevel.Debug, "Returning ImmunizationsController ImmIndex.", "Patient Id = " + id.ToString(), "", "Create");
                return RedirectToAction("Create", new { id = id });
            }
        }

        public ActionResult GetImmFile(int id)
        {
            Logger.Log(LogLevel.Debug, "Starting ImmunizationsController GetImmFile.", "Patient Id = " + id.ToString(), "", "");
            string fileName = (from i in db.Immunizations
                               where i.ImmId == id
                               select i.FilePath).SingleOrDefault();
            if (fileName != null)
            {
                var fileStream = new FileStream(fileName,
                                FileMode.Open,
                                FileAccess.Read
                                );

                var fsResult = new FileStreamResult(fileStream, "application/pdf");
                Logger.Log(LogLevel.Debug, "Returning ImmunizationsController GetImmFile.", "Patient Id = " + id.ToString(), "", "");
                return fsResult;
            }
            else
            {
                Logger.Log(LogLevel.Debug, "Returning ImmunizationsController GetImmFile.", "Patient Id = " + id.ToString(), "", "New");
                return RedirectToAction("ImmIndex", "Immunizations", new { id = id });
            }
        }

        public ActionResult ImmDocs(int id)
        {
            Logger.Log(LogLevel.Debug, "Starting ImmunizationsController ImmDocs.", "Patient Id = " + id.ToString(), "", "");
            List<Immunizations> imm = db.Immunizations.ToList();
            var iList = imm.Where(i => i.PatientId == id)
                        .OrderByDescending(ImmDate => ImmDate);
            Logger.Log(LogLevel.Debug, "Return ImmunizationsController ImmDocs.", "Patient Id = " + id.ToString(), "", "iList");
            return View("Index", iList);
        }

        // GET: Immunizations/Create
        public ActionResult Create(int id)
        {
            Logger.Log(LogLevel.Debug, "Starting ImmunizationsController Get Create.", "Patient Id = " + id.ToString(), "", "");
            var patient = db.Patients.Find(id);
            Immunizations imm = new Immunizations();
            imm.PatientId = id;
            imm.FullName = patient.FullName;
            imm.BirthDate = patient.BirthDate;
            imm.ImmDate = DateTime.Now;
            Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Get Create.", "Patient Id = " + id.ToString(), "", "");
            return View(imm);
        }

        // POST: Immunizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImmId,PatientId,ImmDate,Notes,FilePath")] Immunizations immunizations, HttpPostedFileBase file)
        {
            Logger.Log(LogLevel.Debug, "Starting ImmunizationsController Post Create.", "Patient Id = " + immunizations.PatientId.ToString(), "", "");
            if (ModelState.IsValid)
            {
                try
                {
                    if (Path.GetFullPath(file.FileName) != null)
                    {
                        immunizations.FilePath = Path.GetFullPath(file.FileName);
                        db.Immunizations.Add(immunizations);
                        db.SaveChanges();
                        Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Post Create.", "Patient Id = " + immunizations.PatientId.ToString(), "", "Saved changes full path name");
                    }
                    return RedirectToAction("ImmIndex", "Immunizations", new { id = immunizations.PatientId });
                }
                catch (Exception ex)
                {
                    db.Immunizations.Add(immunizations);
                    db.SaveChanges();
                    Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Post Create.", "Patient Id = " + immunizations.PatientId.ToString(), "", "Saved changes Exception");
                    return RedirectToAction("ImmIndex", "Immunizations", new { id = immunizations.PatientId });
                }                
            }
            Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Post Create.", "Patient Id = " + immunizations.PatientId.ToString(), "", "");
            return View(immunizations);
        }

        // GET: Immunizations/Edit/5
        public ActionResult Edit(int? id)
        {
            Logger.Log(LogLevel.Debug, "Starting ImmunizationsController Get Edit.", "Patient Id = " + id.ToString(), "", "");
            if (id == null)
            {
                Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Get Edit.", "Patient Id = " + id.ToString(), "", "BadRequest");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Immunizations immunizations = db.Immunizations.Find(id);
            if (immunizations == null)
            {
                Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Get Edit.", "Patient Id = " + id.ToString(), "", "HttpNotFound");
                return HttpNotFound();
            }
            Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Get Edit.", "Patient Id = " + id.ToString(), "", "");
            return View(immunizations);
        }

        // POST: Immunizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImmId,PatientId,ImmDate,Notes,FilePath")] Immunizations immunizations, HttpPostedFileBase file)
        {
            Logger.Log(LogLevel.Debug, "Starting ImmunizationsController Post Edit.", "Patient Id = " + immunizations.PatientId.ToString(), "", "");
            if (ModelState.IsValid)
            {
                try
                {
                    immunizations.FilePath = Path.GetFullPath(file.FileName);
                    db.Entry(immunizations).State = EntityState.Modified;
                    db.SaveChanges();
                    Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Post Edit.", "Patient Id = " + immunizations.PatientId.ToString(), "", "Saved changes full path name");
                    return RedirectToAction("ImmIndex", "Immunizations", new { id = immunizations.PatientId });
                }
                catch(Exception ex)
                {

                }
                finally
                {
                    db.Immunizations.Add(immunizations);
                    db.SaveChanges();
                    Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Post Edit.", "Patient Id = " + immunizations.PatientId.ToString(), "", "Saved changes Finally");
                }
                Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Post Edit.", "Patient Id = " + immunizations.PatientId.ToString(), "", "");
                return RedirectToAction("ImmIndex", "Immunizations", new { id = immunizations.PatientId });
            }
            return View(immunizations);
        }

        // GET: Immunizations/Delete/5
        public ActionResult Delete(int? id)
        {
            Logger.Log(LogLevel.Debug, "Starting ImmunizationsController Get Delete.", "Patient Id = " + id.ToString(), "", "");
            if (id == null)
            {
                Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Get Delete.", "Patient Id = " + id.ToString(), "", "BadRequest");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Immunizations immunizations = db.Immunizations.Find(id);
            if (immunizations == null)
            {
                Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Get Delete.", "Patient Id = " + id.ToString(), "", "HttpNotFound");
                return HttpNotFound();
            }
            Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Get Delete.", "Patient Id = " + id.ToString(), "", "");
            return View(immunizations);
        }

        // POST: Immunizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Logger.Log(LogLevel.Debug, "Starting ImmunizationsController Post Delete.", "Patient Id = " + id.ToString(), "", "");
            Immunizations immunizations = db.Immunizations.Find(id);
            db.Immunizations.Remove(immunizations);
            db.SaveChanges();
            Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Post Delete.", "Patient Id = " + id.ToString(), "", "Saved changes");
            return RedirectToAction("ImmIndex", "Immunizations", new { id = immunizations.PatientId });
        }

        protected override void Dispose(bool disposing)
        {
            Logger.Log(LogLevel.Debug, "Starting ImmunizationsController Dispose.", "", "", "");
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
            Logger.Log(LogLevel.Debug, "Ending ImmunizationsController Dispose.", "", "", "");
        }
    }
}
