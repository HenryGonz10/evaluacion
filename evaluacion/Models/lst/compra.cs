using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace evaluacion.Models.lst
{
    public class lstcompra
    {
        public List<lst.compra> compras { get; set; }
    }

    public class compra
    {
        public int id { get; set; }
        public DateTime? Fecha { get; set; }
        public string Distribuidora { get; set; }
        public decimal Total { get; set; }
    }
}