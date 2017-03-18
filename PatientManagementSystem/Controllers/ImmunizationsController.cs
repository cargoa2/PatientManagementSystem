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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Immunizations immunizations = db.Immunizations.Find(id);
            if (immunizations == null)
            {
                return HttpNotFound();
            }
            return View(immunizations);
        }

        public bool CheckforImmunizations(int id)
        {
            List<Immunizations> im = db.Immunizations.ToList();
            if(im.FindAll(p => p.PatientId == id).Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ActionResult ImmIndex(int id)
        {
            if(CheckforImmunizations(id) == true)
            {
                List<Immunizations> immunizations = db.Immunizations.ToList();
                var iList = immunizations.Where(i => i.PatientId == id)
                            .OrderByDescending(ImmDate => ImmDate.ImmDate);
                return View("Index", iList);
            }
            else
            {
                return RedirectToAction("Create", new { id = id });
            }
        }

        public ActionResult GetImmFile(int id)
        {
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
                return fsResult;
            }
            else
            {
                return RedirectToAction("ImmIndex", "Immunizations", new { id = id });
            }
        }

        public ActionResult ImmDocs(int id)
        {
            List<Immunizations> imm = db.Immunizations.ToList();
            var iList = imm.Where(i => i.PatientId == id)
                        .OrderByDescending(ImmDate => ImmDate);
            return View("Index", iList);
        }

        // GET: Immunizations/Create
        public ActionResult Create(int id)
        {
            var patient = db.Patients.Find(id);
            Immunizations imm = new Immunizations();
            imm.PatientId = id;
            imm.FullName = patient.FullName;
            imm.ImmDate = DateTime.Now;
            return View(imm);
        }

        // POST: Immunizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImmId,PatientId,ImmDate,Notes,FilePath")] Immunizations immunizations, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    immunizations.FilePath = Path.GetFullPath(file.FileName);
                    db.Immunizations.Add(immunizations);
                    db.SaveChanges();
                    return RedirectToAction("ImmIndex", "Immunizations", new { id = immunizations.PatientId });
                }
                catch(Exception ex)
                {

                }
            }           

            return View(immunizations);
        }

        // GET: Immunizations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Immunizations immunizations = db.Immunizations.Find(id);
            if (immunizations == null)
            {
                return HttpNotFound();
            }
            return View(immunizations);
        }

        // POST: Immunizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImmId,PatientId,ImmDate,Notes,FilePath")] Immunizations immunizations, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    immunizations.FilePath = Path.GetFullPath(file.FileName);
                    db.Entry(immunizations).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ImmIndex", "Immunizations", new { id = immunizations.PatientId });
                }
                catch(Exception ex)
                {

                }
            }
            return View(immunizations);
        }

        // GET: Immunizations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Immunizations immunizations = db.Immunizations.Find(id);
            if (immunizations == null)
            {
                return HttpNotFound();
            }
            return View(immunizations);
        }

        // POST: Immunizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Immunizations immunizations = db.Immunizations.Find(id);
            db.Immunizations.Remove(immunizations);
            db.SaveChanges();
            return RedirectToAction("ImmIndex", "Immunizations", new { id = immunizations.PatientId });
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
