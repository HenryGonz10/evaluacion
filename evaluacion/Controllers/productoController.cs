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
    public class productoController : Controller
    {
        private evaluacionEntities db = new evaluacionEntities();

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,cod,_producto,cantidad,costo,venta")] Models.lst.producto producto)
        {
            if (ModelState.IsValid)
            {
                using (Models.evaluacionEntities _db = new evaluacionEntities())
                {
                    using (var dbContext = _db.Database.BeginTransaction())
                    {
                        try
                        {
                            var p = _db.producto.FirstOrDefault(x => x.producto_nombre == producto._producto);
                            if (p == null)
                            {
                                p = new Models.producto();
                                p.codigo_barras = producto.cod;
                                p.producto_nombre = producto._producto;
                                p.estado = true;

                                _db.producto.Add(p);

                                var c = _db.producto_costo.FirstOrDefault(x => x.idproducto == producto.id && x.estado == true && x.monto == producto.costo);

                                if (c == null)
                                {
                                    c = _db.producto_costo.FirstOrDefault(x => x.idproducto == producto.id && x.estado == true);
                                    if (c != null)
                                        c.estado = false;

                                    var costo = new Models.producto_costo();
                                    costo.estado = true;
                                    costo.idproducto = producto.id;
                                    costo.monto = producto.costo;

                                    _db.producto_costo.Add(costo);
                                }

                                var v = _db.producto_precioventa.FirstOrDefault(x => x.idproducto == producto.id && x.estado == true && x.monto == producto.venta);

                                if (v == null)
                                {
                                    v = _db.producto_precioventa.FirstOrDefault(x => x.idproducto == producto.id && x.estado == true);
                                    if (v != null)
                                        v.estado = false;

                                    var venta = new Models.producto_precioventa();
                                    venta.estado = true;
                                    venta.idproducto = producto.id;
                                    venta.monto = producto.venta;

                                    _db.producto_precioventa.Add(venta);
                                }

                                var b = _db.bodega.FirstOrDefault(x => x.idproducto == producto.id && x.estado == true && x.cantidad == producto.cantidad);

                                if (b == null)
                                {
                                    b = _db.bodega.FirstOrDefault(x => x.idproducto == producto.id && x.estado == true);

                                    if (b != null)
                                        b.estado = false;

                                    var bodega = new Models.bodega();
                                    bodega.idproducto = producto.id;
                                    bodega.cantidad = producto.cantidad;
                                    bodega.estado = true;

                                    _db.bodega.Add(bodega);
                                }

                                _db.SaveChanges();
                                dbContext.Commit();
                            }
                        }
                        catch
                        {
                            dbContext.Rollback();
                        }
                    }
                }
                return RedirectToAction("Index","Home");
            }

            return View(producto);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var producto = GetProducto(id);

            if (producto == null)
            {
                return HttpNotFound();
            }

            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,cod,_producto,cantidad,costo,venta")] Models.lst.producto producto)
        {

            if (ModelState.IsValid)
            {
                using (Models.evaluacionEntities _db = new evaluacionEntities())
                {
                    using (var dbContext = _db.Database.BeginTransaction())
                    {
                        try
                        {
                            var p = _db.producto.FirstOrDefault(x => x.idproducto == producto.id);
                            if (p != null)
                            {
                                p.producto_nombre = producto._producto;
                                p.codigo_barras = producto.cod;

                                var c = _db.producto_costo.FirstOrDefault(x => x.idproducto == producto.id && x.estado == true && x.monto == producto.costo);

                                if (c == null)
                                {
                                    c = _db.producto_costo.FirstOrDefault(x => x.idproducto == producto.id && x.estado == true);
                                    if (c != null)
                                        c.estado = false;

                                    var costo = new Models.producto_costo();
                                    costo.estado = true;
                                    costo.idproducto = producto.id;
                                    costo.monto = producto.costo;

                                    _db.producto_costo.Add(costo);
                                }

                                var v = _db.producto_precioventa.FirstOrDefault(x => x.idproducto == producto.id && x.estado == true && x.monto == producto.venta);

                                if (v == null)
                                {
                                    v = _db.producto_precioventa.FirstOrDefault(x => x.idproducto == producto.id && x.estado == true);
                                    if (v != null)
                                        v.estado = false;

                                    var venta = new Models.producto_precioventa();
                                    venta.estado = true;
                                    venta.idproducto = producto.id;
                                    venta.monto = producto.venta;

                                    _db.producto_precioventa.Add(venta);
                                }

                                var b = _db.bodega.FirstOrDefault(x => x.idproducto == producto.id && x.estado == true && x.cantidad == producto.cantidad);

                                if (b == null)
                                {
                                    b = _db.bodega.FirstOrDefault(x => x.idproducto == producto.id && x.estado == true);
                                    
                                    if (b != null)
                                        b.estado = false;

                                    var bodega = new Models.bodega();
                                    bodega.idproducto = producto.id;
                                    bodega.cantidad = producto.cantidad;
                                    bodega.estado = true;

                                    _db.bodega.Add(bodega);
                                }

                                _db.SaveChanges();
                                dbContext.Commit();
                            }
                        }
                        catch
                        {
                            dbContext.Rollback();
                        }
                    }
                }
                return RedirectToAction("Index", "Home");
            }

            return View(producto);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var producto = GetProducto(id);

            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            producto producto = db.producto.Find(id);
            producto.estado = false;
            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        public Models.lst.producto GetProducto(int? id)
        {
            Models.lst.producto producto = (from p in db.producto

                                            join b in db.bodega on p.idproducto equals b.idproducto into pb
                                            from subpb in pb.DefaultIfEmpty()

                                            join c in db.producto_costo on p.idproducto equals c.idproducto into pc
                                            from subpc in pc.DefaultIfEmpty()

                                            join v in db.producto_precioventa on p.idproducto equals v.idproducto into pv
                                            from subpv in pv.DefaultIfEmpty()

                                            where p.estado == true && subpb.estado == true && subpc.estado == true && subpv.estado == true
                                            select new Models.lst.producto
                                            {
                                                id = p.idproducto,
                                                cod = p.codigo_barras,
                                                _producto = p.producto_nombre,
                                                cantidad = subpb.cantidad,
                                                costo = subpc.monto,
                                                venta = subpv.monto
                                            }).FirstOrDefault();

            return producto;
        }
    }
}
