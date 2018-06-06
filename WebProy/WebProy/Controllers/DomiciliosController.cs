using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebProy;

namespace WebProy.Models
{
    public class DomiciliosController : Controller
    {
        private evaluacionconnection db = new evaluacionconnection();

        // GET: Domicilios
        public ActionResult Index()
        {
            var domicilio = db.Domicilio.Include(d => d.Persona);
            return View(domicilio.ToList());
        }

        // GET: Domicilios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Domicilio domicilio = db.Domicilio.Find(id);
            if (domicilio == null)
            {
                return HttpNotFound();
            }
            return View(domicilio);
        }

        // GET: Domicilios/Create
        public ActionResult Create(int? id)
        {
             
            if (id == null)
            {
                return RedirectToAction("Index", "Personas" );
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ViewBag.Persona_id = new SelectList(db.Persona, "id", "Apellido");
            ViewBag.Persona_id = id;
            //Persona p = db.Persona.Find(id);
            
            return View();
        }

        // POST: Domicilios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Calle,Numero,Persona_id")] Domicilio domicilio)
        {
            //String valor = idpersona;
            if (ModelState.IsValid)
            {
                db.Domicilio.Add(domicilio);
                db.SaveChanges();
                return RedirectToAction("Edit", "Personas", new { id = domicilio.Persona_id });
            }

            //ViewBag.Persona_id = new SelectList(db.Persona, "id", "Apellido", domicilio.Persona_id);

            return View(domicilio);
        }

        // GET: Domicilios/Edit/5
        public ActionResult Edit(int? id,String persona_id)
        {
            String valor = persona_id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ViewBag.Persona_id = id;
            Domicilio domicilio = db.Domicilio.Find(id);
            if (domicilio == null)
            {
                return HttpNotFound();
            }
            ViewBag.Persona_id = new SelectList(db.Persona, "id", "Apellido", domicilio.Persona_id);
            return View(domicilio);
        }

        // POST: Domicilios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Calle,Numero,Persona_id")] Domicilio domicilio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(domicilio).State = EntityState.Modified;
                db.SaveChanges();
               
                return RedirectToAction("Edit", "Personas", new { id = domicilio.Persona_id });
            }
            ViewBag.Persona_id = new SelectList(db.Persona, "id", "Apellido", domicilio.Persona_id);
            return View(domicilio);
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
