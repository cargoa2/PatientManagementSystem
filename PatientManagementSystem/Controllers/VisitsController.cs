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
    public class VisitsController : Controller
    {
        private PatientContext db = new PatientContext();

        // GET: Visits
        public ActionResult Index()
        {
            return View(db.Visits.ToList());
        }

        public bool CheckVisits(int id)
        {
          //  Logger.Log(LogLevel.Debug, "Starting VisitsController CheckVisits.", "Patient Id = " + id.ToString(), "", "");
            List<Visits> visits = db.Visits.ToList();
            var visitsExists = visits.Find(i => i.PatientId == id);

            if (visitsExists != null)
            {
       //         Logger.Log(LogLevel.Debug, "Returning VisitsController CheckVisits.", "Patient Id = " + id.ToString(), "", "True");
                return true;                
            }
            else
            {
       //         Logger.Log(LogLevel.Debug, "Returning VisitsController CheckVisits.", "Patient Id = " + id.ToString(), "", "False");
                return false;                
            }
        }
        public ActionResult PatientIndex(int id)
        {
      //      Logger.Log(LogLevel.Debug, "Starting VisitsController PatientIndex.", "Patient Id = " + id.ToString(), "", "");
            if (CheckVisits(id) == true)
            {
                List<Visits> visits = db.Visits.ToList();
                var vList = visits.Where(v => v.PatientId == id).OrderByDescending(v => v.VisitDate);
      //          Logger.Log(LogLevel.Debug, "Returning VisitsController PatientIndex.", "Patient Id = " + id.ToString(), "", "vList");
                return View("Index", vList);
            }
            else
            {
      //          Logger.Log(LogLevel.Debug, "Returning VisitsController PatientIndex.", "Patient Id = " + id.ToString(), "", "New");
                return RedirectToAction("Create", new { id = id });             
            }
           
        }

        public ActionResult PrintDetails(int? id)
        {
      //      Logger.Log(LogLevel.Debug, "Starting VisitsController PrintDetails.", "Patient Id = " + id.ToString(), "", "");
            if (id == null)
            {
      //          Logger.Log(LogLevel.Debug, "Returning VisitsController PrintDetails.", "Patient Id = " + id.ToString(), "", "BadRequest");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visits visit = db.Visits.Find(id);
            if (visit == null)
            {
      //          Logger.Log(LogLevel.Debug, "Returning VisitsController PrintDetails.", "Patient Id = " + id.ToString(), "", "HttpNotFound");
                return HttpNotFound();
            }
     //       Logger.Log(LogLevel.Debug, "Returning VisitsController PrintDetails.", "Patient Id = " + id.ToString(), "", "");
            return View(visit);
        }

        public ActionResult BillingDetails(int? id)
        {
      //      Logger.Log(LogLevel.Debug, "Starting VisitsController BillingDetails.", "Patient Id = " + id.ToString(), "", "");
            if (id == null)
            {
      //          Logger.Log(LogLevel.Debug, "Returning VisitsController BillingDetails.", "Patient Id = " + id.ToString(), "", "BadRequest");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visits visit = db.Visits.Find(id);
            if (visit == null)
            {
      //          Logger.Log(LogLevel.Debug, "Returning VisitsController BillingDetails.", "Patient Id = " + id.ToString(), "", "HttpNotFound");
                return HttpNotFound();
            }
      //      Logger.Log(LogLevel.Debug, "Returning VisitsController BillingDetails.", "Patient Id = " + id.ToString(), "", "");
            return View(visit);
        }    


        // GET: Visits/Details/5
        public ActionResult Details(int? id)
        {
      //      Logger.Log(LogLevel.Debug, "Starting VisitsController Get Details.", "Patient Id = " + id.ToString(), "", "");
            if (id == null)
            {
      //          Logger.Log(LogLevel.Debug, "Returning VisitsController Get Details.", "Patient Id = " + id.ToString(), "", "BadRequest");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visits visit = db.Visits.Find(id);

            visit.PatientPhone = FormatPhoneNumber(visit.patient.PatientPhone);

            if (visit == null)
            {
      //          Logger.Log(LogLevel.Debug, "Returning VisitsController Get Details.", "Patient Id = " + id.ToString(), "", "HttpNotFound");
                return HttpNotFound();
            }
     //       Logger.Log(LogLevel.Debug, "Returning VisitsController Get Details.", "Patient Id = " + id.ToString(), "", "");
            return View(visit);
        }

        public static string FormatPhoneNumber(string number)
        {
      //      Logger.Log(LogLevel.Debug, "Starting VisitsController FormatPhoneNumber.", "", "", number.ToString());
            string formatNumber = "(" + number.Substring(0, 3) + ")" +
                                  " " + number.Substring(3, 3) + "-" +
                                        number.Substring(6, 4);

      //      Logger.Log(LogLevel.Debug, "Returning VisitsController FormatPhoneNumber.", "", "", formatNumber);
            return formatNumber;
        }


        // GET: Visits/Create
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create(int id)
        {
      //      Logger.Log(LogLevel.Debug, "Starting VisitsController Get Create.", "Patient Id = " + id.ToString(), "", "");

            List<Visits> visits = db.Visits.ToList();

            var listVisits = visits.Where(v => v.PatientId == id).OrderByDescending(v => v.VisitDate);

            var patient = db.Patients.Find(id);
                Visits visit = new Visits();
                visit.PatientId = Convert.ToInt32(id);
                visit.FullName = patient.FullName;
                visit.BirthDate = patient.BirthDate;
                visit.PatientPhone = patient.PatientPhone;
                visit.VisitDate = DateTime.Now;
            visit.DateSignedByPhys = DateTime.Now;

            if (listVisits.Count() > 0)
            {
                Visits lastVisit = visits.Where(v => v.PatientId == id).OrderBy(v => v.VisitId).LastOrDefault();

                visit.DiagnosisCode = lastVisit.DiagnosisCode;
                visit.ProblemList = lastVisit.ProblemList;
                visit.MedicalAllergy = lastVisit.MedicalAllergy;
                visit.ReferralReason = lastVisit.ReferralReason;
                visit.History = lastVisit.History;
                visit.PastHistory = lastVisit.PastHistory;                
                visit.FamilyHistory = lastVisit.FamilyHistory;
                visit.SocialHistory = lastVisit.SocialHistory;
                visit.RosGeneral = lastVisit.RosGeneral;
                visit.RosHeent = lastVisit.RosHeent;
                visit.Respiratory = lastVisit.Respiratory;
                visit.Cardiovascular = lastVisit.Cardiovascular;
                visit.Gastrointestinal = lastVisit.Gastrointestinal;
                visit.Genitourniary = lastVisit.Genitourniary;
                visit.RosNeurological = lastVisit.RosNeurological;
                visit.psychosocial = lastVisit.psychosocial;
                visit.Medications = lastVisit.Medications;
                visit.PeGeneral = lastVisit.PeGeneral;
                visit.BloodPressure = lastVisit.BloodPressure;
                visit.HeartRate = lastVisit.HeartRate;
                visit.Tempurature = lastVisit.Tempurature;
                visit.Weight = lastVisit.Weight;
                visit.PeHeent = lastVisit.PeHeent;
                visit.Neck = lastVisit.Neck;
                visit.Skin = lastVisit.Skin;
                visit.Lungs = lastVisit.Lungs;
                visit.Heart = lastVisit.Heart;
                visit.Abdomen = lastVisit.Abdomen;
                visit.Musculoskeletal = lastVisit.Musculoskeletal;
                visit.PeNeurological = lastVisit.PeNeurological;
                visit.Additional = lastVisit.Additional;
                visit.Documentsoratory = lastVisit.Documentsoratory;
                visit.Assessment = lastVisit.Assessment;
                visit.Plan = lastVisit.Plan;
            }
      //      Logger.Log(LogLevel.Debug, "Returning VisitsController Get Create.", "Patient Id = " + id.ToString(), "", "");
            return View(visit);
        }

        // POST: Visits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AcceptVerbs("Post")]
        public ActionResult Create([Bind(Include = "VisitId,PatientId,Initial,VisitType,VisitDate,DiagnosisCode,CoPay,PaymentType,CheckNumber,TotalPaid,MedicalAllergy,ReferralReason,History,PastHistory,Epidemiology,FamilyHistory,SocialHistory,RosGeneral,RosHeent,Respiratory,Cardiovascular,Gastrointestinal,Genitourniary,RosNeurological,psychosocial,Medications,PeGeneral,BloodPressure,HeartRate,Tempurature,Weight,PeHeent,Neck,Skin,Lungs,Heart,Abdomen,Musculoskeletal,PeNeurological,Additional,Documentsoratory,Assessment,Plan,ProblemList,DateSignedByPhys")] Visits visit)
        {
       //     Logger.Log(LogLevel.Debug, "Starting VisitsController Post Create.", "Patient Id = " + visit.PatientId.ToString(), "", "");
            if (ModelState.IsValid)
            {
                db.Visits.Add(visit);
                db.SaveChanges();
       //         Logger.Log(LogLevel.Debug, "Returning VisitsController Post Create.", "Patient Id = " + visit.PatientId.ToString(), "", "Saved changes");
                return RedirectToAction("PatientIndex", new { id = visit.PatientId });
            }
      //      Logger.Log(LogLevel.Debug, "Returning VisitsController Post Create.", "Patient Id = " + visit.PatientId.ToString(), "", "");
            return View(visit);
        }

        // GET: Visits/Edit/5
        public ActionResult Edit(int? id)
        {
     //       Logger.Log(LogLevel.Debug, "Starting VisitsController Get Edit.", "Patient Id = " + id.ToString(), "", "");
            if (id == null)
            {
     //           Logger.Log(LogLevel.Debug, "Returning VisitsController Get Edit.", "Patient Id = " + id.ToString(), "", "BadRequest");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visits visit = db.Visits.Find(id);
            if (visit == null)
            {
     //           Logger.Log(LogLevel.Debug, "Returning VisitsController Get Edit.", "Patient Id = " + id.ToString(), "", "HttpNotFound");
                return HttpNotFound();
            }
    //        Logger.Log(LogLevel.Debug, "Returning VisitsController Get Edit.", "Patient Id = " + id.ToString(), "", "");
            return View(visit);
        }

        // POST: Visits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VisitId,PatientId,Initial,VisitType,VisitDate,DiagnosisCode,CoPay,PaymentType,CheckNumber,TotalPaid,MedicalAllergy,ReferralReason,History,PastHistory,Epidemiology,FamilyHistory,SocialHistory,RosGeneral,RosHeent,Respiratory,Cardiovascular,Gastrointestinal,Genitourniary,RosNeurological,psychosocial,Medications,PeGeneral,BloodPressure,HeartRate,Tempurature,Weight,PeHeent,Neck,Skin,Lungs,Heart,Abdomen,Musculoskeletal,PeNeurological,Additional,Documentsoratory,Assessment,Plan,ProblemList,DateSignedByPhys")] Visits visit)
        {
     //       Logger.Log(LogLevel.Debug, "Starting VisitsController Post Edit.", "Patient Id = " + visit.PatientId.ToString(), "", "");
            if (ModelState.IsValid)
            {        
                db.Entry(visit).State = EntityState.Modified;
                db.SaveChanges();
      //          Logger.Log(LogLevel.Debug, "Returning VisitsController Post Edit.", "Patient Id = " + visit.PatientId.ToString(), "", "Saved changes");
                return RedirectToAction("PatientIndex", new { id = visit.PatientId });
            }
      //      Logger.Log(LogLevel.Debug, "Returning VisitsController Post Edit.", "Patient Id = " + visit.PatientId.ToString(), "", "");
            return View(visit);
        }

        // GET: Visits/Delete/5
        public ActionResult Delete(int? id)
        {
     //       Logger.Log(LogLevel.Debug, "Starting VisitsController Get Delete.", "Patient Id = " + id.ToString(), "", "");
            if (id == null)
            {
    //            Logger.Log(LogLevel.Debug, "Returning VisitsController Get Delete.", "Patient Id = " + id.ToString(), "", "BadRequest");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visits visit = db.Visits.Find(id);
            if (visit == null)
            {
      //          Logger.Log(LogLevel.Debug, "Returning VisitsController Get Delete.", "Patient Id = " + id.ToString(), "", "HttpNotFound");
                return HttpNotFound();
            }
     //       Logger.Log(LogLevel.Debug, "Returning VisitsController Get Delete.", "Patient Id = " + id.ToString(), "", "");
            return View(visit);
        }

        // POST: Visits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
     //       Logger.Log(LogLevel.Debug, "Starting VisitsController Post Delete.", "Patient Id = " + id.ToString(), "", "");
            Visits visit = db.Visits.Find(id);
            db.Visits.Remove(visit);
            db.SaveChanges();
    //        Logger.Log(LogLevel.Debug, "Returning VisitsController Post Delete.", "Patient Id = " + id.ToString(), "", "Saved changes");
            return RedirectToAction("PatientIndex", new { id = visit.PatientId });
        }

        private IEnumerable<VisitTypes> PopulateVisitTypes()
        {
     //       Logger.Log(LogLevel.Debug, "Starting VisitsController PopulateVisitTypes.", "", "", "");
            List<VisitTypes> visitType = new List<VisitTypes>();
    //        Logger.Log(LogLevel.Debug, "Returning VisitsController PopulateVisitTypes.", "", "", "");
            return visitType;
        }

        protected override void Dispose(bool disposing)
        {
     //       Logger.Log(LogLevel.Debug, "Starting VisitsController Dispose.", "", "", "");
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
     //       Logger.Log(LogLevel.Debug, "Ending VisitsController Dispose.", "", "", "");
        }
    }
}
