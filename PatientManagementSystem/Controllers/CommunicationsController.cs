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
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace PatientManagementSystem.Controllers
{
    public class CommunicationsController : Controller
    {
        private PatientContext db = new PatientContext();

        // GET: Communications
        public ActionResult Index()
        {
            Logger.Log(LogLevel.Debug, "Starting Patient CommunicationController Get Index.", "", "", "");

            var communication = db.Communication.Include(p => p.Patient);

            Logger.Log(LogLevel.Debug, "Completed Patient CommunicationController Get Index.", "", "", "");

            return View(communication.ToList());
        }

        // GET: Communications/Details/5
        public ActionResult Details(int? id)
        {
            Logger.Log(LogLevel.Debug, "Starting Patient CommunicationController Get Details.", "Patient Id = " + id.ToString(), "", "");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Communication communication = db.Communication.Find(id);
            if (communication == null)
            {
                return HttpNotFound();
            }

            Logger.Log(LogLevel.Debug, "Completed Patient CommunicationController Get Details.", "Patient Id = " + id.ToString(), "", "");
            return View(communication);
            
        }

        public bool CheckforCommunications(int id)
        {
            Logger.Log(LogLevel.Debug, "Starting Patient CommunicationController CheckforCommunications.", "Patient Id = " + id.ToString(), "", "");

            List<Communication> comm = db.Communication.ToList();
            int recordsExists = comm.FindAll(p => p.PatientId == id).Count();
            if(recordsExists > 0)           
            {
                Logger.Log(LogLevel.Debug, "Returning Patient CommunicationController CheckforCommunications true.", "Patient Id = " + id.ToString(), "", "");
                return true;                
            }
            else
            {
                Logger.Log(LogLevel.Debug, "Returning Patient CommunicationController CheckforCommunications false.", "Patient Id = " + id.ToString(), "", "");
                return false;                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult CommIndex(int id)
        {
            Logger.Log(LogLevel.Debug, "Starting Patient CommunicationController CommIndex.", "Patient Id = " + id.ToString(), "", "");

            if (CheckforCommunications(id) == true)
            {

                List<Communication> Communications = db.Communication.ToList();

                // Needed to strip out the HTML in the string saved in the rich text control.  This is a summary.
                foreach (var item in Communications)
                {
                    HtmlDocument note = new HtmlDocument();
                    note.LoadHtml(item.Notes);
                    string ih = note.DocumentNode.InnerHtml;
                    string it = note.DocumentNode.InnerText;
                    string newit = HttpUtility.HtmlDecode(it);
                    item.Notes = newit;                    
                }

                var cList = Communications.Where(p => p.PatientId == id)
                                            .OrderByDescending(CommDate => CommDate.CommDate);

                Logger.Log(LogLevel.Debug, "Returning Patient CommunicationController Details cList.", "Patient Id = " + id.ToString(), "", "");

                return View("Index", cList);
            }
            else
            {
                Logger.Log(LogLevel.Debug, "Returning Patient CommunicationController Details Create New.", "Patient Id = " + id.ToString(), "", "");
                return RedirectToAction("Create", new { id = id });
            }
        }

        public ActionResult CommDocs(int id)
        {
            Logger.Log(LogLevel.Debug, "Starting Patient CommunicationController CommDocs.", "Patient Id = " + id.ToString(), "", "");

            List<Communication> comm = db.Communication.ToList();
            var cList = comm.Where(p => p.PatientId == id)
                        .OrderByDescending(CommDate => CommDate);

            Logger.Log(LogLevel.Debug, "Returning Patient CommunicationController CommDocs.", "Patient Id = " + id.ToString(), "", "");
            return View("Index", cList);
        }

        // GET: Communications/Create
        public ActionResult Create(int id)
        {
            Logger.Log(LogLevel.Debug, "Starting Patient CommunicationController Get Create.", "Patient Id = " + id.ToString(), "", "");

            var patient = db.Patients.Find(id);
            Communication comm = new Communication();
            comm.PatientId = id;
            comm.FullName = patient.FullName;
            comm.BirthDate = patient.BirthDate;
            comm.CommDate = DateTime.Now;
            Logger.Log(LogLevel.Debug, "Completed Patient CommunicationController Get Create.", "Patient Id = " + id.ToString(), "", "");
            return View(comm);
        }

        // POST: Communications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommId,PatientId,CommDate,Notes")] Communication communication, HttpPostedFileBase file)
        {
            Logger.Log(LogLevel.Debug, "Starting Patient CommunicationController Post Create.", "New Id", "", "");

            if (ModelState.IsValid)
            {   
                db.Communication.Add(communication);
                db.SaveChanges();                
                return RedirectToAction("CommIndex", "Communications", new { id = communication.PatientId });

            }

            Logger.Log(LogLevel.Debug, "Returning Patient CommunicationController Post Create.", "New Patient Id = " + communication.PatientId.ToString(), "", "");
            return View(communication);
        }

        // GET: Communications/Edit/5
        public ActionResult Edit(int? id)
        {
            Logger.Log(LogLevel.Debug, "Starting Patient CommunicationController Get Edit.", "Patient Id = " + id.ToString(), "", "");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Communication communication = db.Communication.Find(id);
            if (communication == null)
            {
                return HttpNotFound();
            }
            Logger.Log(LogLevel.Debug, "Returning Patient CommunicationController Get Edit.", "Patient Id = " + id.ToString(), "", "");

            return View(communication);
        }

        // POST: Communications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommId,PatientId,CommDate,Notes")] Communication communication, HttpPostedFileBase file)
        {
            Logger.Log(LogLevel.Debug, "Starting Patient CommunicationController Post Edit.", "Patient Id = " + communication.PatientId.ToString(), "", "");

            if (ModelState.IsValid)
            {
                db.Entry(communication).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CommIndex", "Communications", new { id = communication.PatientId });
            }
            Logger.Log(LogLevel.Debug, "Returning Patient CommunicationController Post Edit.", "Patient Id = " + communication.PatientId.ToString(), "", "");
            return View(communication);
        }

        // GET: Communications/Delete/5
        public ActionResult Delete(int? id)
        {
            Logger.Log(LogLevel.Debug, "Starting Patient CommunicationController Get Delete.", "Patient Id = " + id.ToString(), "", "");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Communication communication = db.Communication.Find(id);

            // Needed to strip out the HTML in the string saved in the rich text control.  This is a summary.          
                HtmlDocument note = new HtmlDocument();
                note.LoadHtml(communication.Notes);
                string ih = note.DocumentNode.InnerHtml;
                string it = note.DocumentNode.InnerText;
                string newit = HttpUtility.HtmlDecode(it);
                communication.Notes = newit;
          
            if (communication == null)
            {
                return HttpNotFound();
            }

            Logger.Log(LogLevel.Debug, "Returning Patient CommunicationController Get Delete.", "Patient Id = " + id.ToString(), "", "");
            return View(communication);
        }

        // POST: Communications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Logger.Log(LogLevel.Debug, "Starting Patient CommunicationController Post Delete.", "Patient Id = " + id.ToString(), "", "");

            Communication communication = db.Communication.Find(id);
            db.Communication.Remove(communication);
            db.SaveChanges();
            Logger.Log(LogLevel.Debug, "Returning Patient CommunicationController Post Delete.", "Patient Id = " + id.ToString(), "", "");
            return RedirectToAction("CommIndex", "Communications", new { id = communication.PatientId });
        }

        protected override void Dispose(bool disposing)
        {
            Logger.Log(LogLevel.Debug, "Starting Patient CommunicationController Dispose.", "", "", "");
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
            Logger.Log(LogLevel.Debug, "Completed Patient CommunicationController Dispose.", "", "", "");
        }
    }
}
