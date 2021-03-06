﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PatientManagementSystem.Models;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

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
           // Logger.Log(LogLevel.Debug, "Starting ImmunizationsController Get Details.", "Patient Id = " + id.ToString(), "", "");
            if (id == null)
            {
               // Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Get Details.", "Patient Id = " + id.ToString(), "", "BadRequest");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Immunizations immunizations = db.Immunizations.Find(id);
            if (immunizations == null)
            {
                //Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Get Details.", "Patient Id = " + id.ToString(), "", "HttpNotFound");
                return HttpNotFound();
            }
           // Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Get Details.", "Patient Id = " + id.ToString(), "", "immunizations");
            return View(immunizations);
        }

        public bool CheckforImmunizations(int id)
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["PatientContext"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Imm_Count", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cn.Open();

                    int count = (Int32)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public ActionResult ImmIndex(int id)
        {
           // Logger.Log(LogLevel.Debug, "Starting ImmunizationsController ImmIndex.", "Patient Id = " + id.ToString(), "", "");

            if (CheckforImmunizations(id) == true)
            {               
                using(var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["PatientContext"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Get_Patient_Imm_List", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        cn.Open();

                        var rdr = cmd.ExecuteReader();
                        List<Immunizations> model = new List<Immunizations>();
                        while (rdr.Read())
                        {
                            var imms = new Immunizations
                            {
                                PatientId = id,
                                FullName = rdr["FullName"].ToString(),
                                ImmId = Convert.ToInt32(rdr["ImmId"]),
                                BirthDate = Convert.ToDateTime(rdr["BirthDate"]),
                                ImmDate = Convert.ToDateTime(rdr["ImmDate"]),
                                Notes = rdr["Notes"].ToString(),
                                FilePath = rdr["FilePath"].ToString()
                            };
                            model.Add(imms);
                                
                        }
                        return View("Index", model);
                    }
                }     
                // Logger.Log(LogLevel.Debug, "Returning ImmunizationsController ImmIndex.", "Patient Id = " + id.ToString(), "", "iList");
            }
            else
            {
               // Logger.Log(LogLevel.Debug, "Returning ImmunizationsController ImmIndex.", "Patient Id = " + id.ToString(), "", "Create");
                return RedirectToAction("Create", new { id = id });
            }
        }

        public ActionResult GetImmFile(int id)
        {
            var fileContent = (from i in db.Immunizations
                               where i.ImmId == id
                               select i.FileContent).SingleOrDefault();

            if (fileContent != null)
            {
                return File(fileContent, "application/pdf");
            }
            else
            {
                return RedirectToAction("ImmIndex", "Immunizations", new { id = id });
            }

        }

        public ActionResult ImmDocs(int id)
        {
           // Logger.Log(LogLevel.Debug, "Starting ImmunizationsController ImmDocs.", "Patient Id = " + id.ToString(), "", "");
            List<Immunizations> imm = db.Immunizations.ToList();
            var iList = imm.Where(i => i.PatientId == id)
                        .OrderByDescending(ImmDate => ImmDate);
           // Logger.Log(LogLevel.Debug, "Return ImmunizationsController ImmDocs.", "Patient Id = " + id.ToString(), "", "iList");
            return View("Index", iList);
        }

        // GET: Immunizations/Create
        public ActionResult Create(int id)
        {
            //Logger.Log(LogLevel.Debug, "Starting ImmunizationsController Get Create.", "Patient Id = " + id.ToString(), "", "");
            var patient = db.Patients.Find(id);
            Immunizations imm = new Immunizations();
            imm.PatientId = id;
            imm.FullName = patient.FullName;
            imm.BirthDate = patient.BirthDate;
            imm.ImmDate = DateTime.Now;
           // Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Get Create.", "Patient Id = " + id.ToString(), "", "");
            return View(imm);
        }

        // POST: Immunizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImmId,PatientId,ImmDate,Notes,FilePath, FileContent")] Immunizations immunizations, HttpPostedFileBase file)
        {
           // Logger.Log(LogLevel.Debug, "Starting ImmunizationsController Post Create.", "Patient Id = " + immunizations.PatientId.ToString(), "", "");
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
                        immunizations.FilePath = Path.GetFileName(file.FileName);
                        immunizations.FileContent = FileDet;
                    }
                  else
                    {
                        ViewBag.FileStatus = "Invalid file format.  Choose a pdf file.";
                        return View();
                    }
                    db.Immunizations.Add(immunizations);
                    db.SaveChanges();                    
                    return RedirectToAction("ImmIndex", "Immunizations", new { id = immunizations.PatientId });
                }
                catch (Exception ex)
                {
                    db.Immunizations.Add(immunizations);
                    db.SaveChanges();
                    //Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Post Create.", "Patient Id = " + immunizations.PatientId.ToString(), "", "Saved changes Exception");
                    return RedirectToAction("ImmIndex", "Immunizations", new { id = immunizations.PatientId });
                }                
            }
            //Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Post Create.", "Patient Id = " + immunizations.PatientId.ToString(), "", "");
            return View(immunizations);
        }

        // GET: Immunizations/Edit/5
        public ActionResult Edit(int? id)
        {
            //Logger.Log(LogLevel.Debug, "Starting ImmunizationsController Get Edit.", "Patient Id = " + id.ToString(), "", "");
            if (id == null)
            {
               // Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Get Edit.", "Patient Id = " + id.ToString(), "", "BadRequest");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Immunizations immunizations = db.Immunizations.Find(id);
            if (immunizations == null)
            {
               // Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Get Edit.", "Patient Id = " + id.ToString(), "", "HttpNotFound");
                return HttpNotFound();
            }
            //Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Get Edit.", "Patient Id = " + id.ToString(), "", "");
            return View(immunizations);
        }

        // POST: Immunizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImmId,PatientId,ImmDate,Notes,FilePath,FileContent")] Immunizations immunizations, HttpPostedFileBase file)
        {
            // Logger.Log(LogLevel.Debug, "Starting ImmunizationsController Post Edit.", "Patient Id = " + immunizations.PatientId.ToString(), "", "");
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
                        immunizations.FilePath = Path.GetFileName(file.FileName);
                        immunizations.FileContent = FileDet;
                    }
                    else
                    {
                        ViewBag.FileStatus = "Invalid file format.  Choose a pdf file.";
                        return View();
                    }

                    db.Entry(immunizations).State = EntityState.Modified;
                    db.SaveChanges();

                    //  Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Post Edit.", "Patient Id = " + immunizations.PatientId.ToString(), "", "Saved changes full path name");
                    return RedirectToAction("ImmIndex", "Immunizations", new { id = immunizations.PatientId });
                }
                catch (Exception ex)
                {

                }               
            }
            return View(immunizations);
        }

        // GET: Immunizations/Delete/5
        public ActionResult Delete(int? id)
        {
          //  Logger.Log(LogLevel.Debug, "Starting ImmunizationsController Get Delete.", "Patient Id = " + id.ToString(), "", "");
            if (id == null)
            {
             //   Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Get Delete.", "Patient Id = " + id.ToString(), "", "BadRequest");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Immunizations immunizations = db.Immunizations.Find(id);
            if (immunizations == null)
            {
             //   Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Get Delete.", "Patient Id = " + id.ToString(), "", "HttpNotFound");
                return HttpNotFound();
            }
          //  Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Get Delete.", "Patient Id = " + id.ToString(), "", "");
            return View(immunizations);
        }

        // POST: Immunizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
         //   Logger.Log(LogLevel.Debug, "Starting ImmunizationsController Post Delete.", "Patient Id = " + id.ToString(), "", "");
            Immunizations immunizations = db.Immunizations.Find(id);
            db.Immunizations.Remove(immunizations);
            db.SaveChanges();
         //   Logger.Log(LogLevel.Debug, "Returning ImmunizationsController Post Delete.", "Patient Id = " + id.ToString(), "", "Saved changes");
            return RedirectToAction("ImmIndex", "Immunizations", new { id = immunizations.PatientId });
        }

        protected override void Dispose(bool disposing)
        {
         //   Logger.Log(LogLevel.Debug, "Starting ImmunizationsController Dispose.", "", "", "");
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
          //  Logger.Log(LogLevel.Debug, "Ending ImmunizationsController Dispose.", "", "", "");
        }
    }
}
