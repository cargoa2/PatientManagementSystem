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
            List<Visits> visits = db.Visits.ToList();
            var visitsExists = visits.Find(i => i.PatientId == id);

            if (visitsExists != null)
            {
                return true;                
            }
            else
            {
                return false;                
            }
        }
        public ActionResult PatientIndex(int id)
        {
            if(CheckVisits(id) == true)
            {
                List<Visits> visits = db.Visits.ToList();
                var vList = visits.Where(v => v.PatientId == id).OrderByDescending(v => v.VisitDate);                
                return View("Index", vList);
            }
            else
            {               
                return RedirectToAction("Create", new { id = id });             
            }
           
        }

        public ActionResult BillingDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visits visit = db.Visits.Find(id);
            if (visit == null)
            {
                return HttpNotFound();
            }
            return View(visit);
        }

        


        // GET: Visits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visits visit = db.Visits.Find(id);
            if (visit == null)
            {
                return HttpNotFound();
            }
            return View(visit);
        }


        // GET: Visits/Create
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create(int id)
        {
            List<Visits> visits = db.Visits.ToList();

            var listVisits = visits.Where(v => v.PatientId == id).OrderByDescending(v => v.VisitDate);

            var patient = db.Patients.Find(id);
                Visits visit = new Visits();
                visit.PatientId = Convert.ToInt32(id);
                visit.FullName = patient.FullName;
                visit.VisitDate = DateTime.Now;

            if (listVisits.Count() > 0)
            {                
                Visits lastVisit = visits.Where(v => v.PatientId == id).OrderByDescending(v => v.VisitDate).FirstOrDefault();

                visit.History = lastVisit.History;
                visit.PastHistory = lastVisit.PastHistory;
                visit.Epidemiology = lastVisit.Epidemiology;
                visit.FamilyHistory = lastVisit.FamilyHistory;
                visit.SocialHistory = lastVisit.SocialHistory;
                visit.MedicalAllergy = lastVisit.MedicalAllergy;
                visit.Medications = lastVisit.Medications;
            }           

            if (CheckVisits(id) == true)
            {
                if (patient.Active == false)
                {
                    visit.Initial = true;
                }
                else
                {
                    visit.Initial = false;
                }
            }
            else
            {
                visit.Initial = true;
            }
                return View(visit);
        }

        // POST: Visits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AcceptVerbs("Post")]
        public ActionResult Create([Bind(Include = "VisitId,PatientId,Initial,VisitType,VisitDate,DiagnosisCode,CoPay,PaymentType,CheckNumber,TotalPaid,MedicalAllergy,ReferralReason,History,PastHistory,Epidemiology,FamilyHitory,SocialHistory,RosGeneral,RosHeent,Respiratory,Cardiovascular,Gastrointestinal,Genitourniary,RosNeurological,psychosocial,Medications,PeGeneral,BloodPressure,HeartRate,Tempurature,Weight,PeHeent,Neck,Skin,Lungs,Heart,Abdomen,Musculoskeletal,PeNeurological,Additional,Documentsoratory,Assessment,Plan,IsHIVManagement,IsOtherManagement")] Visits visit)
        {
            if (ModelState.IsValid)
            {
                db.Visits.Add(visit);
                db.SaveChanges();
                return RedirectToAction("PatientIndex", new { id = visit.PatientId });
            }
            return View(visit);
        }

        // GET: Visits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visits visit = db.Visits.Find(id);
            if (visit == null)
            {
                return HttpNotFound();
            }
            return View(visit);
        }

        // POST: Visits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VisitId,PatientId,Initial,VisitType,VisitDate,DiagnosisCode,CoPay,PaymentType,CheckNumber,TotalPaid,MedicalAllergy,ReferralReason,History,PastHistory,Epidemiology,FamilyHitory,SocialHistory,RosGeneral,RosHeent,Respiratory,Cardiovascular,Gastrointestinal,Genitourniary,RosNeurological,psychosocial,Medications,PeGeneral,BloodPressure,HeartRate,Tempurature,Weight,PeHeent,Neck,Skin,Lungs,Heart,Abdomen,Musculoskeletal,PeNeurological,Additional,Documentsoratory,Assessment,Plan,IsHIVManagement,IsOtherManagement")] Visits visit)
        {
            if (ModelState.IsValid)
            {   
                     
                db.Entry(visit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PatientIndex", new { id = visit.PatientId });
            }
            return View(visit);
        }

        // GET: Visits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visits visit = db.Visits.Find(id);
            if (visit == null)
            {
                return HttpNotFound();
            }
            return View(visit);
        }

        // POST: Visits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Visits visit = db.Visits.Find(id);
            db.Visits.Remove(visit);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private IEnumerable<VisitTypes> PopulateVisitTypes()
        {
            List<VisitTypes> visitType = new List<VisitTypes>();
            return visitType;
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
