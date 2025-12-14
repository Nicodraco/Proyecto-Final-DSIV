using System;
using System.IO;
using System.Data.SQLite;

namespace QuickVentas.AccesoDatos
{
    public static class ConexionBD
    {
        // Cadena de conexión para SQLite
        private static string cadenaConexion = "Data Source=QuickVentas.db;Version=3;";

        // Método para obtener una conexión
        public static SQLiteConnection ObtenerConexion()
        {
            return new SQLiteConnection(cadenaConexion);
        }

        // Método para crear la base de datos y tablas si no existen
        public static void CrearBaseDeDatos()
        {
            try
            {
                if (!File.Exists("QuickVentas.db"))
                {
                    SQLiteConnection.CreateFile("QuickVentas.db");
                    Console.WriteLine("✅ Base de datos creada: QuickVentas.db");
                }

                using (var conexion = ObtenerConexion())
                {
                    conexion.Open();

                    // 1. Crear tabla Productos SI NO EXISTE
                    string sqlProductos = @"
                CREATE TABLE IF NOT EXISTS Productos (
                    ProductoID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nombre TEXT NOT NULL,
                    Precio REAL NOT NULL,
                    Stock INTEGER DEFAULT 0,
                    Categoria TEXT,
                    FechaCreacion DATETIME DEFAULT CURRENT_TIMESTAMP
                );";

                    using (var comando = new SQLiteCommand(sqlProductos, conexion))
                    {
                        comando.ExecuteNonQuery();
                    }

                    // 2. Verificar si ya hay productos
                    string contarProductos = "SELECT COUNT(*) FROM Productos";
                    int cantidadProductos = 0;

                    using (var comando = new SQLiteCommand(contarProductos, conexion))
                    {
                        cantidadProductos = Convert.ToInt32(comando.ExecuteScalar());
                    }

                    // 3. Solo insertar datos de prueba si la tabla está vacía
                    if (cantidadProductos == 0)
                    {
                        string insertarProductos = @"
                    INSERT INTO Productos (Nombre, Precio, Stock, Categoria) 
                    VALUES 
                        ('Empanada de Carne', 0.75, 50, 'Frituras'),
                        ('Refresco 500ml', 1.00, 100, 'Bebidas'),
                        ('Café', 0.50, 80, 'Bebidas');";

                        using (var comando = new SQLiteCommand(insertarProductos, conexion))
                        {
                            comando.ExecuteNonQuery();
                            Console.WriteLine("✅ Datos de prueba de productos insertados.");
                        }
                    }

                    // 4. Crear tabla Clientes SI NO EXISTE
                    string sqlClientes = @"
                CREATE TABLE IF NOT EXISTS Clientes (
                    ClienteID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nombre TEXT NOT NULL,
                    Telefono TEXT,
                    Email TEXT,
                    FechaRegistro DATETIME DEFAULT CURRENT_TIMESTAMP
                );";

                    using (var comando = new SQLiteCommand(sqlClientes, conexion))
                    {
                        comando.ExecuteNonQuery();
                    }

                    // 5. Verificar si ya hay clientes
                    string contarClientes = "SELECT COUNT(*) FROM Clientes";
                    int cantidadClientes = 0;

                    using (var comando = new SQLiteCommand(contarClientes, conexion))
                    {
                        cantidadClientes = Convert.ToInt32(comando.ExecuteScalar());
                    }

                    // 6. Solo insertar datos de prueba si la tabla está vacía
                    if (cantidadClientes == 0)
                    {
                        string insertarClientes = @"
                    INSERT INTO Clientes (Nombre, Telefono, Email) 
                    VALUES 
                        ('Juan Pérez', '6123-4567', 'juan@email.com'),
                        ('María Rodríguez', '6789-0123', 'maria@email.com'),
                        ('Carlos López', '6456-7890', 'carlos@email.com');";

                        using (var comando = new SQLiteCommand(insertarClientes, conexion))
                        {
                            comando.ExecuteNonQuery();
                            Console.WriteLine("✅ Datos de prueba de clientes insertados.");
                        }
                    }

                    conexion.Close();
                }

                CrearTablasVentas();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al crear BD: {ex.Message}");
                throw;
            }
        }
        public static void CrearTablaClientes()
        {
            try
            {
                using (var conexion = ObtenerConexion())
                {
                    conexion.Open();

                    string sql = @"
                CREATE TABLE IF NOT EXISTS Clientes (
                    ClienteID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nombre TEXT NOT NULL,
                    Telefono TEXT,
                    Email TEXT,
                    FechaRegistro DATETIME DEFAULT CURRENT_TIMESTAMP
                );
                
                -- Insertar algunos clientes de prueba
                INSERT OR IGNORE INTO Clientes (Nombre, Telefono, Email) 
                VALUES 
                    ('Juan Pérez', '6123-4567', 'juan@email.com'),
                    ('María Rodríguez', '6789-0123', 'maria@email.com'),
                    ('Carlos López', '6456-7890', 'carlos@email.com');
            ";

                    using (var comando = new SQLiteCommand(sql, conexion))
                    {
                        comando.ExecuteNonQuery();
                    }

                    conexion.Close();
                    Console.WriteLine("✅ Tabla Clientes verificada/creada.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al crear tabla Clientes: {ex.Message}");
            }
        }

        public static void CrearTablasVentas()
        {
            try
            {
                using (var conexion = ObtenerConexion())
                {
                    conexion.Open();

                    // Tabla Ventas
                    string sqlVentas = @"
                CREATE TABLE IF NOT EXISTS Ventas (
                    VentaID INTEGER PRIMARY KEY AUTOINCREMENT,
                    ClienteID INTEGER,
                    FechaVenta DATETIME DEFAULT CURRENT_TIMESTAMP,
                    Total DECIMAL(10,2) DEFAULT 0,
                    FOREIGN KEY (ClienteID) REFERENCES Clientes(ClienteID)
                );";

                    // Tabla Detalles de Venta
                    string sqlDetalles = @"
                CREATE TABLE IF NOT EXISTS VentaDetalles (
                    DetalleID INTEGER PRIMARY KEY AUTOINCREMENT,
                    VentaID INTEGER,
                    ProductoID INTEGER,
                    Cantidad INTEGER,
                    PrecioUnitario DECIMAL(10,2),
                    Subtotal DECIMAL(10,2),
                    FOREIGN KEY (VentaID) REFERENCES Ventas(VentaID),
                    FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID)
                );";

                    using (var comando = new SQLiteCommand(sqlVentas, conexion))
                    {
                        comando.ExecuteNonQuery();
                    }

                    using (var comando = new SQLiteCommand(sqlDetalles, conexion))
                    {
                        comando.ExecuteNonQuery();
                    }

                    conexion.Close();
                    Console.WriteLine("✅ Tablas de ventas verificadas/creadas.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al crear tablas de ventas: {ex.Message}");
            }
        }
    }
}