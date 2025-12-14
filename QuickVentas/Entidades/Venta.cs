using System;
using System.Collections.Generic;

namespace QuickVentas.Entidades
{
    public class Venta
    {
        public int VentaID { get; set; }
        public int? ClienteID { get; set; } // Nullable por si es venta sin cliente
        public DateTime FechaVenta { get; set; }
        public decimal Total { get; set; }
        public List<VentaDetalle> Detalles { get; set; }

        public Venta()
        {
            Detalles = new List<VentaDetalle>();
            FechaVenta = DateTime.Now;
        }
    }
}