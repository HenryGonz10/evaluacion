using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace evaluacion.Models.lst
{
    public class lstproducto
    {
        public List<lst.producto> productos { get; set; }
    }

    public class producto
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Codigo de Barras")]
        [StringLength(50)]
        public string cod { get; set; }
        [Required]
        [Display(Name = "Nombre del producto")]
        [StringLength(45)]
        public string _producto { get; set; }        
        [Display(Name = "Cantidad en bodega")]        
        public int? cantidad { get; set; }
        [Display(Name = "Precio costo")]
        public decimal? costo { get; set; }
        [Display(Name = "Precio de Venta")]
        public decimal? venta { get; set; }
    }
}