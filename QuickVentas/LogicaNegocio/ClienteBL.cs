using System;
using System.Collections.Generic;
using System.Data.SQLite;
using QuickVentas.Entidades;
using QuickVentas.AccesoDatos;

namespace QuickVentas.LogicaNegocio
{
    public class ClienteBL
    {
        // Obtener todos los clientes
        public List<Cliente> ObtenerClientes()
        {
            List<Cliente> clientes = new List<Cliente>();

            using (SQLiteConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string sql = "SELECT * FROM Clientes";

                using (SQLiteCommand comando = new SQLiteCommand(sql, conexion))
                using (SQLiteDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        clientes.Add(new Cliente
                        {
                            ClienteID = Convert.ToInt32(lector["ClienteID"]),
                            Nombre = lector["Nombre"].ToString(),
                            Telefono = lector["Telefono"].ToString(),
                            Email = lector["Email"].ToString(),
                            FechaRegistro = Convert.ToDateTime(lector["FechaRegistro"])
                        });
                    }
                }
            }

            return clientes;
        }

        // Insertar nuevo cliente
        public bool InsertarCliente(Cliente cliente)
        {
            using (SQLiteConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string sql = "INSERT INTO Clientes (Nombre, Telefono, Email) VALUES (@Nombre, @Telefono, @Email)";

                using (SQLiteCommand comando = new SQLiteCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    comando.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                    comando.Parameters.AddWithValue("@Email", cliente.Email);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        // Actualizar cliente existente
        public bool ActualizarCliente(Cliente cliente)
        {
            using (SQLiteConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string sql = "UPDATE Clientes SET Nombre = @Nombre, Telefono = @Telefono, Email = @Email WHERE ClienteID = @ClienteID";

                using (SQLiteCommand comando = new SQLiteCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@ClienteID", cliente.ClienteID);
                    comando.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    comando.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                    comando.Parameters.AddWithValue("@Email", cliente.Email);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        // Eliminar cliente
        public bool EliminarCliente(int clienteID)
        {
            using (SQLiteConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string sql = "DELETE FROM Clientes WHERE ClienteID = @ClienteID";

                using (SQLiteCommand comando = new SQLiteCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@ClienteID", clienteID);
                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        // Buscar clientes
        public List<Cliente> BuscarClientes(string criterio)
        {
            List<Cliente> clientes = new List<Cliente>();

            using (SQLiteConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                string sql = "SELECT * FROM Clientes WHERE Nombre LIKE @Criterio OR Telefono LIKE @Criterio OR Email LIKE @Criterio";

                using (SQLiteCommand comando = new SQLiteCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@Criterio", "%" + criterio + "%");

                    using (SQLiteDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            clientes.Add(new Cliente
                            {
                                ClienteID = Convert.ToInt32(lector["ClienteID"]),
                                Nombre = lector["Nombre"].ToString(),
                                Telefono = lector["Telefono"].ToString(),
                                Email = lector["Email"].ToString(),
                                FechaRegistro = Convert.ToDateTime(lector["FechaRegistro"])
                            });
                        }
                    }
                }
            }

            return clientes;
        }
    }
}