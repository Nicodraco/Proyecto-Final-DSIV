using System;
using System.Collections.Generic;
using System.Data.SQLite;
using QuickVentas.Entidades;
using QuickVentas.AccesoDatos;

namespace QuickVentas.LogicaNegocio
{
    public class ProductoBL
    {
        public List<Producto> ObtenerProductos()
        {
            List<Producto> productos = new List<Producto>();

            using (SQLiteConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string sql = "SELECT * FROM Productos";

                using (SQLiteCommand comando = new SQLiteCommand(sql, conexion))
                using (SQLiteDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        productos.Add(new Producto
                        {
                            ProductoID = Convert.ToInt32(lector["ProductoID"]),
                            Nombre = lector["Nombre"].ToString(),
                            Precio = Convert.ToDecimal(lector["Precio"]),
                            Stock = Convert.ToInt32(lector["Stock"]),
                            Categoria = lector["Categoria"].ToString()
                        });
                    }
                }
            }

            return productos;
        }

        public bool InsertarProducto(Producto producto)
        {
            using (SQLiteConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string sql = "INSERT INTO Productos (Nombre, Precio, Stock, Categoria) VALUES (@Nombre, @Precio, @Stock, @Categoria)";

                using (SQLiteCommand comando = new SQLiteCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    comando.Parameters.AddWithValue("@Precio", producto.Precio);
                    comando.Parameters.AddWithValue("@Stock", producto.Stock);
                    comando.Parameters.AddWithValue("@Categoria", producto.Categoria);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool ActualizarProducto(Producto producto)
        {
            using (SQLiteConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string sql = "UPDATE Productos SET Nombre = @Nombre, Precio = @Precio, Stock = @Stock, Categoria = @Categoria WHERE ProductoID = @ProductoID";

                using (SQLiteCommand comando = new SQLiteCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@ProductoID", producto.ProductoID);
                    comando.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    comando.Parameters.AddWithValue("@Precio", producto.Precio);
                    comando.Parameters.AddWithValue("@Stock", producto.Stock);
                    comando.Parameters.AddWithValue("@Categoria", producto.Categoria);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool EliminarProducto(int productoID)
        {
            using (SQLiteConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string sql = "DELETE FROM Productos WHERE ProductoID = @ProductoID";

                using (SQLiteCommand comando = new SQLiteCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@ProductoID", productoID);
                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        public List<Producto> BuscarProductos(string criterio)
        {
            List<Producto> productos = new List<Producto>();

            using (SQLiteConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string sql = "SELECT * FROM Productos WHERE Nombre LIKE @Criterio OR Categoria LIKE @Criterio";

                using (SQLiteCommand comando = new SQLiteCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@Criterio", "%" + criterio + "%");

                    using (SQLiteDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            productos.Add(new Producto
                            {
                                ProductoID = Convert.ToInt32(lector["ProductoID"]),
                                Nombre = lector["Nombre"].ToString(),
                                Precio = Convert.ToDecimal(lector["Precio"]),
                                Stock = Convert.ToInt32(lector["Stock"]),
                                Categoria = lector["Categoria"].ToString()
                            });
                        }
                    }
                }
            }

            return productos;
        }
    }
}