﻿using System;
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
    public class OtherManagementsController : Controller
    {
        private PatientContext db = new PatientContext();

        // GET: OtherManagements
        public ActionResult Index()
        {           
            return View(db.OtherManagements.ToList());
        }

        public bool CheckForOtherManagementRecords(int id)
        {
            List<OtherManagement> otherMan = db.OtherManagements.ToList();
            var recordsExists = otherMan.Find(o => o.PatientId == id);

            if(recordsExists != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ActionResult OtherIndex(int id)
        {
            if (CheckForOtherManagementRecords(id) == true)
            {
                List<OtherManagement> othMan = db.OtherManagements.ToList();
                var oList = othMan.Where(o => o.PatientId == id).OrderByDescending(o => o.VisitDate);
                return View("Index", oList);
            }
            else
            {
                return RedirectToAction("Create", new { id = id });
            }
        }

        public ActionResult PatientOtherManagement(int id)
        {
            var pList = (from o in db.OtherManagements                         
                         join p in db.Patients on o.PatientId equals p.Id
                         where p.Id == id
                         select o).ToList();
            return View("PatientOtherIndex", pList);
        }
        

        // GET: OtherManagements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            OtherManagement otherManagement = db.OtherManagements.Find(id);
            var patient = db.Patients.Find(otherManagement.PatientId);
            otherManagement.FullName = patient.FullName;
            if (otherManagement == null)
            {
                return HttpNotFound();
            }                      

            return View(otherManagement);
        }

        // GET: OtherManagements/Create
        public ActionResult Create(int id)
        {
            var patient = db.Patients.Find(id);
            OtherManagement otherManagement = new OtherManagement();
            otherManagement.PatientId = id;
            otherManagement.FullName = patient.FullName;
            otherManagement.BirthDate = patient.BirthDate;
            otherManagement.VisitDate = DateTime.Now;                      
            return View(otherManagement);
        }

        // POST: OtherManagements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OtherId,PatientId,VisitDate,TCell,ViralLoad,WhiteBloodCell,Hemoglobin,PlasmaIronTurnover,OtherWeight,Triglycerides,TotalCholesterol,OtherDocuments")] OtherManagement otherManagement)
        {
            if (ModelState.IsValid)
            {
                db.OtherManagements.Add(otherManagement);
                db.SaveChanges();               
                return RedirectToAction("OtherIndex", "OtherManagements", new { id = otherManagement.PatientId });
            }

            return View(otherManagement);
        }

        // GET: OtherManagements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtherManagement otherManagement = db.OtherManagements.Find(id);
            if (otherManagement == null)
            {
                return HttpNotFound();
            }
            return View(otherManagement);
        }

        // POST: OtherManagements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OtherId,PatientId,VisitDate,TCell,ViralLoad,WhiteBloodCell,Hemoglobin,PlasmaIronTurnover,OtherWeight,Triglycerides,TotalCholesterol,OtherDocuments")] OtherManagement otherManagement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(otherManagement).State = EntityState.Modified;
                db.SaveChanges();                
                return RedirectToAction("OtherIndex", "OtherManagements", new {id = otherManagement.PatientId } );              
            }
            return View(otherManagement);
        }

        // GET: OtherManagements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtherManagement otherManagement = db.OtherManagements.Find(id);
            if (otherManagement == null)
            {
                return HttpNotFound();
            }
            return View(otherManagement);
        }

        // POST: OtherManagements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {          
            OtherManagement otherManagement = db.OtherManagements.Find(id);
            db.OtherManagements.Remove(otherManagement);
            db.SaveChanges();
            return RedirectToAction("OtherIndex", "OtherManagements", new { id = otherManagement.PatientId });
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
