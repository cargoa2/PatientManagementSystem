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
    public class CommunicationsController : Controller
    {
        private PatientContext db = new PatientContext();

        // GET: Communications
        public ActionResult Index()
        {
            var communication = db.Communication.Include(p => p.Patient);
            return View(communication.ToList());
        }

        // GET: Communications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Communication communication = db.Communication.Find(id);
            if (communication == null)
            {
                return HttpNotFound();
            }
            return View(communication);
        }

        public bool CheckforCommunications(int id)
        {
            List<Communication> comm = db.Communication.ToList();
            int recordsExists = comm.FindAll(p => p.PatientId == id).Count();
            if(recordsExists > 0)           
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ActionResult CommIndex(int id)
        {
            if (CheckforCommunications(id) == true)
            {
                List<Communication> Communications = db.Communication.ToList();
                var cList = Communications.Where(p => p.PatientId == id)
                                            .OrderByDescending(CommDate => CommDate.CommDate);
                return View("Index", cList);
            }
            else
            {
                return RedirectToAction("Create", new { id = id });
            }
        }

        public ActionResult GetCommFile(int id)
        {
            string fileName = (from d in db.Communication
                               where d.CommId == id
                               select d.FilePath).SingleOrDefault();

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
                return RedirectToAction("Index", "Communications", new { id = id });
            }
        }

        public ActionResult CommDocs(int id)
        {
            List<Communication> comm = db.Communication.ToList();
            var cList = comm.Where(p => p.PatientId == id)
                        .OrderByDescending(CommDate => CommDate);
            return View("Index", cList);
        }

        // GET: Communications/Create
        public ActionResult Create(int id)
        {
            var patient = db.Patients.Find(id);
            Communication comm = new Communication();
            comm.PatientId = id;
            comm.FullName = patient.FullName;
            comm.CommDate = DateTime.Now;
            return View(comm);
           
        }

        // POST: Communications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommId,PatientId,Type,CommDate,Notes,FilePath")] Communication communication, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    try
                    {
                        communication.FilePath = Path.GetFullPath(file.FileName);
                        db.Communication.Add(communication);
                        db.SaveChanges();
                        return RedirectToAction("CommIndex", "Communications", new { id = communication.PatientId });
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    db.Communication.Add(communication);
                    db.SaveChanges();
                    return RedirectToAction("CommIndex", "Communications", new { id = communication.PatientId });
                }
            }            
            return View(communication);
        }

        // GET: Communications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Communication communication = db.Communication.Find(id);
            if (communication == null)
            {
                return HttpNotFound();
            }
            return View(communication);
        }

        // POST: Communications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommId,PatientId,Type,CommDate,Notes,FilePath")] Communication communication, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {

                    try
                    {
                        communication.FilePath = Path.GetFullPath(file.FileName);
                        db.Entry(communication).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("CommIndex", "Communications", new { id = communication.PatientId });
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    db.Entry(communication).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("CommIndex", "Communications", new { id = communication.PatientId });
                }
            }
            return View(communication);
        }

        // GET: Communications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Communication communication = db.Communication.Find(id);
            if (communication == null)
            {
                return HttpNotFound();
            }
            return View(communication);
        }

        // POST: Communications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Communication communication = db.Communication.Find(id);
            db.Communication.Remove(communication);
            db.SaveChanges();
            return RedirectToAction("CommIndex", "Communications", new { id = communication.PatientId });
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
