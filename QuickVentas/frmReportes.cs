using System;
using System.Windows.Forms;
using QuickVentas.LogicaNegocio;

namespace QuickVentas
{
    public partial class frmReportes : Form
    {
        private VentaBL ventaBL;
        public frmReportes()
        {
            InitializeComponent();
            EstilosAplicacion.AplicarEstiloFormulario(this, "Reportes y Estadísticas");
            dgvReportes.DataBindingComplete += DgvReportes_DataBindingComplete;
            ventaBL = new VentaBL();
            CargarVentasRecientes(GetDgvReportes());
        }

        private void DgvReportes_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgvReportes.Columns.Contains("VentaID"))
            {
                dgvReportes.Columns["VentaID"].Width = 80;
            }
          
        }

        private DataGridView GetDgvReportes()
        {
            return dgvReportes;
        }

        private void CargarVentasRecientes(DataGridView dgvReportes)
        {
            try
            {
                var ventas = ventaBL.ObtenerVentasRecientes(20);
                dgvReportes.DataSource = ventas;

                // Esperar a que se generen las columnas
                Application.DoEvents();

                // Configurar columnas 
                if (dgvReportes.Columns.Count > 0)
                {
                    // Solo configurar columnas que existan
                    if (dgvReportes.Columns.Contains("VentaID"))
                    {
                        dgvReportes.Columns["VentaID"].HeaderText = "ID Venta";
                        dgvReportes.Columns["VentaID"].Width = 80;
                    }

                    if (dgvReportes.Columns.Contains("FechaVenta"))
                    {
                        dgvReportes.Columns["FechaVenta"].HeaderText = "Fecha";
                        dgvReportes.Columns["FechaVenta"].Width = 120;
                        dgvReportes.Columns["FechaVenta"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                    }

                    if (dgvReportes.Columns.Contains("Total"))
                    {
                        dgvReportes.Columns["Total"].HeaderText = "Total";
                        dgvReportes.Columns["Total"].Width = 100;
                        dgvReportes.Columns["Total"].DefaultCellStyle.Format = "N2";
                        dgvReportes.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }

                    if (dgvReportes.Columns.Contains("NombreCliente"))
                    {
                        dgvReportes.Columns["NombreCliente"].HeaderText = "Cliente";
                        dgvReportes.Columns["NombreCliente"].Width = 150;
                    }

                    // Ocultar columnas problemáticas
                    if (dgvReportes.Columns.Contains("ClienteID"))
                    {
                        dgvReportes.Columns["ClienteID"].Visible = false;
                    }

                    if (dgvReportes.Columns.Contains("Detalles"))
                    {
                        dgvReportes.Columns["Detalles"].Visible = false;
                    }
                }

                lblTotalVentas.Text = $"Total de ventas: {ventas.Count}";

                // Calcular total general
                decimal totalGeneral = 0;
                foreach (var venta in ventas)
                {
                    totalGeneral += venta.Total;
                }
                lblMontoTotal.Text = $"Monto total: ${totalGeneral:N2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar reportes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarVentasRecientes(GetDgvReportes());
        }
    }
}