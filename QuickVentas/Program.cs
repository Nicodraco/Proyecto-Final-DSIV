using System;
using System.Windows.Forms;
using QuickVentas.AccesoDatos;

namespace QuickVentas
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Crear base de datos si no existe
            try
            {
                ConexionBD.CrearBaseDeDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Advertencia al crear BD: " + ex.Message);
            }

            // Ejecutar formulario principal
            Application.Run(new frmPrincipal());
        }
    }
}