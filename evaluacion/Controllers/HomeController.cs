using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace evaluacion.Controllers
{
    public class HomeController : Controller
    {
        private Models.evaluacionEntities db = new Models.evaluacionEntities();

        public ActionResult Index()
        {
            var producto = new Models.lst.lstproducto();

            try
            {
                producto.productos = (from p in db.producto

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
                                      }).ToList();
            }
            catch
            { producto.productos = new List<Models.lst.producto>(); }

            if (producto.productos == null)
            {
                return HttpNotFound();
            }

            return View(producto);
        }
    }
}