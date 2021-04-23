using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using evaluacion.Models;

namespace evaluacion.Controllers
{
    public class comprasController : Controller
    {
        private evaluacionEntities db = new evaluacionEntities();

        // GET: compras
        public ActionResult Index()
        {
            var detalle_compra = db.detalle_compra.Include(d => d.compra).Include(d => d.producto);
            return View(detalle_compra.ToList());
        }

        // GET: compras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalle_compra detalle_compra = db.detalle_compra.Find(id);
            if (detalle_compra == null)
            {
                return HttpNotFound();
            }
            return View(detalle_compra);
        }

        // GET: compras/Create
        public ActionResult Create()
        {
            ViewBag.idcompra = new SelectList(db.compra, "idcompra", "idcompra");
            ViewBag.idproducto = new SelectList(db.producto, "idproducto", "codigo_barras");
            return View();
        }

        // POST: compras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "iddetalle_compra,idcompra,idproducto,cantidad,monto")] detalle_compra detalle_compra)
        {
            if (ModelState.IsValid)
            {
                db.detalle_compra.Add(detalle_compra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idcompra = new SelectList(db.compra, "idcompra", "idcompra", detalle_compra.idcompra);
            ViewBag.idproducto = new SelectList(db.producto, "idproducto", "codigo_barras", detalle_compra.idproducto);
            return View(detalle_compra);
        }

        // GET: compras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalle_compra detalle_compra = db.detalle_compra.Find(id);
            if (detalle_compra == null)
            {
                return HttpNotFound();
            }
            ViewBag.idcompra = new SelectList(db.compra, "idcompra", "idcompra", detalle_compra.idcompra);
            ViewBag.idproducto = new SelectList(db.producto, "idproducto", "codigo_barras", detalle_compra.idproducto);
            return View(detalle_compra);
        }

        // POST: compras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "iddetalle_compra,idcompra,idproducto,cantidad,monto")] detalle_compra detalle_compra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalle_compra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idcompra = new SelectList(db.compra, "idcompra", "idcompra", detalle_compra.idcompra);
            ViewBag.idproducto = new SelectList(db.producto, "idproducto", "codigo_barras", detalle_compra.idproducto);
            return View(detalle_compra);
        }

        // GET: compras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalle_compra detalle_compra = db.detalle_compra.Find(id);
            if (detalle_compra == null)
            {
                return HttpNotFound();
            }
            return View(detalle_compra);
        }

        // POST: compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            detalle_compra detalle_compra = db.detalle_compra.Find(id);
            db.detalle_compra.Remove(detalle_compra);
            db.SaveChanges();
            return RedirectToAction("Index");
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
