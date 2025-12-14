using System;
using System.Collections.Generic;
using System.Data.SQLite;
using QuickVentas.Entidades;
using QuickVentas.AccesoDatos;

namespace QuickVentas.LogicaNegocio
{
    public class VentaBL
    {
        // Procesar una nueva venta
        public int ProcesarVenta(Venta venta)
        {
            int ventaID = 0;

            using (SQLiteConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (var transaction = conexion.BeginTransaction())
                {
                    try
                    {
                        // 1. Insertar la venta
                        string sqlVenta = @"INSERT INTO Ventas (ClienteID, Total) 
                                           VALUES (@ClienteID, @Total);
                                           SELECT last_insert_rowid();";

                        using (var comando = new SQLiteCommand(sqlVenta, conexion, transaction))
                        {
                            comando.Parameters.AddWithValue("@ClienteID",
                                venta.ClienteID.HasValue ? (object)venta.ClienteID.Value : DBNull.Value);
                            comando.Parameters.AddWithValue("@Total", venta.Total);

                            ventaID = Convert.ToInt32(comando.ExecuteScalar());
                        }

                        // 2. Insertar los detalles de la venta
                        foreach (var detalle in venta.Detalles)
                        {
                            string sqlDetalle = @"INSERT INTO VentaDetalles 
                                                (VentaID, ProductoID, Cantidad, PrecioUnitario, Subtotal)
                                                VALUES (@VentaID, @ProductoID, @Cantidad, @PrecioUnitario, @Subtotal)";

                            using (var comando = new SQLiteCommand(sqlDetalle, conexion, transaction))
                            {
                                comando.Parameters.AddWithValue("@VentaID", ventaID);
                                comando.Parameters.AddWithValue("@ProductoID", detalle.ProductoID);
                                comando.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                                comando.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
                                comando.Parameters.AddWithValue("@Subtotal", detalle.Subtotal);

                                comando.ExecuteNonQuery();
                            }

                            // 3. Actualizar stock del producto
                            string actualizarStock = @"UPDATE Productos 
                                                      SET Stock = Stock - @Cantidad 
                                                      WHERE ProductoID = @ProductoID";

                            using (var comando = new SQLiteCommand(actualizarStock, conexion, transaction))
                            {
                                comando.Parameters.AddWithValue("@ProductoID", detalle.ProductoID);
                                comando.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                                comando.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        return ventaID;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // Obtener ventas recientes
        public List<Venta> ObtenerVentasRecientes(int limite = 50)
        {
            var ventas = new List<Venta>();

            using (SQLiteConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();

                string sql = @"
                    SELECT v.VentaID, v.ClienteID, c.Nombre as ClienteNombre, 
                           v.FechaVenta, v.Total
                    FROM Ventas v
                    LEFT JOIN Clientes c ON v.ClienteID = c.ClienteID
                    ORDER BY v.FechaVenta DESC
                    LIMIT @Limite";

                using (var comando = new SQLiteCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@Limite", limite);

                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            var venta = new Venta
                            {
                                VentaID = Convert.ToInt32(lector["VentaID"]),
                                ClienteID = lector["ClienteID"] != DBNull.Value ?
                                           Convert.ToInt32(lector["ClienteID"]) : (int?)null,
                                FechaVenta = Convert.ToDateTime(lector["FechaVenta"]),
                                Total = Convert.ToDecimal(lector["Total"])
                            };

                            // Cargar detalles de la venta
                            venta.Detalles = ObtenerDetallesVenta(venta.VentaID, conexion);
                            ventas.Add(venta);
                        }
                    }
                }
            }

            return ventas;
        }

        // Obtener detalles de una venta específica
        private List<VentaDetalle> ObtenerDetallesVenta(int ventaID, SQLiteConnection conexion)
        {
            var detalles = new List<VentaDetalle>();

            string sql = @"
                SELECT vd.DetalleID, vd.ProductoID, p.Nombre as NombreProducto,
                       vd.Cantidad, vd.PrecioUnitario, vd.Subtotal
                FROM VentaDetalles vd
                JOIN Productos p ON vd.ProductoID = p.ProductoID
                WHERE vd.VentaID = @VentaID";

            using (var comando = new SQLiteCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@VentaID", ventaID);

                using (var lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        detalles.Add(new VentaDetalle
                        {
                            DetalleID = Convert.ToInt32(lector["DetalleID"]),
                            ProductoID = Convert.ToInt32(lector["ProductoID"]),
                            NombreProducto = lector["NombreProducto"].ToString(),
                            Cantidad = Convert.ToInt32(lector["Cantidad"]),
                            PrecioUnitario = Convert.ToDecimal(lector["PrecioUnitario"]),
                            Subtotal = Convert.ToDecimal(lector["Subtotal"])
                        });
                    }
                }
            }

            return detalles;
        }
    }
}