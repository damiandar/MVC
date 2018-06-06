using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebProy;

namespace WebProy.Controllers
{
    public class PersonasController : Controller
    {
        private evaluacionconnection db = new evaluacionconnection();

        // GET: Personas
        public ActionResult Index()
        {
            var persona = db.Persona.Include(p => p.TipoDocumento);
            return View(persona.ToList());
        }
         
        // GET: Personas/Create
        public ActionResult Create()
        {
            ViewBag.TipoDocumento_id = new SelectList(db.TipoDocumento, "id", "Nombre");
            return View();
        }

        // POST: Personas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Apellido,Nombre,NumeroDocumento,TipoDocumento_id")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Persona.Add(persona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TipoDocumento_id = new SelectList(db.TipoDocumento, "id", "Nombre", persona.TipoDocumento_id);
            return View(persona);
        }

        // GET: Personas/Edit/5
        public ActionResult Edit(int? id )
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return View("Index");
               // return HttpNotFound();
            }
            ViewBag.TipoDocumento_id = new SelectList(db.TipoDocumento, "id", "Nombre", persona.TipoDocumento_id);
            return View(persona);
        }

        // POST: Personas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Apellido,Nombre,NumeroDocumento,TipoDocumento_id,Domicilio")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipoDocumento_id = new SelectList(db.TipoDocumento, "id", "Nombre", persona.TipoDocumento_id);
           
            return View(persona);
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
