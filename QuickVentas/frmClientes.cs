using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuickVentas.LogicaNegocio;
using QuickVentas.Entidades;

namespace QuickVentas
{
    public partial class frmClientes : Form
    {
        private ClienteBL clienteBL;

        public frmClientes()
        {
            InitializeComponent();
            EstilosAplicacion.AplicarEstiloFormulario(this, "Gestión de Clientes");
            dgvClientes.DataBindingComplete += DgvClientes_DataBindingComplete;
            clienteBL = new ClienteBL();
            CargarClientes();
        }
        private void DgvClientes_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgvClientes.Columns.Contains("ClienteID"))
            {
                dgvClientes.Columns["ClienteID"].Width = 50;
            }
            
        }

        private void CargarClientes()
        {
            try
            {
                var clientes = clienteBL.ObtenerClientes();
                dgvClientes.DataSource = clientes;

                // Esperar un momento para que se creen las columnas
                Application.DoEvents();

                // Formatear columnas solo si existen
                if (dgvClientes.Columns.Count > 0)
                {
                    // Verificar cada columna antes de acceder
                    if (dgvClientes.Columns.Contains("ClienteID"))
                    {
                        dgvClientes.Columns["ClienteID"].HeaderText = "ID";
                        dgvClientes.Columns["ClienteID"].Width = 50;
                    }

                    if (dgvClientes.Columns.Contains("Nombre"))
                    {
                        dgvClientes.Columns["Nombre"].HeaderText = "Nombre";
                        dgvClientes.Columns["Nombre"].Width = 150;
                    }

                    if (dgvClientes.Columns.Contains("Telefono"))
                    {
                        dgvClientes.Columns["Telefono"].HeaderText = "Teléfono";
                        dgvClientes.Columns["Telefono"].Width = 100;
                    }

                    if (dgvClientes.Columns.Contains("Email"))
                    {
                        dgvClientes.Columns["Email"].HeaderText = "Email";
                        dgvClientes.Columns["Email"].Width = 150;
                    }

                    if (dgvClientes.Columns.Contains("FechaRegistro"))
                    {
                        dgvClientes.Columns["FechaRegistro"].HeaderText = "Fecha Registro";
                        dgvClientes.Columns["FechaRegistro"].Width = 120;
                        dgvClientes.Columns["FechaRegistro"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                    }
                }

                // Actualizar label solo si existe
                if (lblTotal != null)
                {
                    lblTotal.Text = $"Total: {clientes.Count} clientes";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                try
                {
                    var clientes = clienteBL.BuscarClientes(txtBuscar.Text);
                    dgvClientes.DataSource = clientes;
                    lblTotal.Text = $"Resultados: {clientes.Count} clientes";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al buscar: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                CargarClientes();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using (frmClienteDetalle detalle = new frmClienteDetalle())
            {
                if (detalle.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (clienteBL.InsertarCliente(detalle.Cliente))
                        {
                            MessageBox.Show("Cliente creado correctamente", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarClientes();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al crear cliente: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                int clienteID = Convert.ToInt32(dgvClientes.SelectedRows[0].Cells["ClienteID"].Value);

                try
                {
                    // Obtener la lista actual de clientes del DataGridView
                    var clientes = (List<Cliente>)dgvClientes.DataSource;
                    Cliente cliente = clientes.Find(c => c.ClienteID == clienteID);

                    if (cliente != null)
                    {
                        using (frmClienteDetalle detalle = new frmClienteDetalle(cliente))
                        {
                            if (detalle.ShowDialog() == DialogResult.OK)
                            {
                                // Actualizar el cliente
                                if (clienteBL.ActualizarCliente(detalle.Cliente))
                                {
                                    MessageBox.Show("Cliente actualizado correctamente", "Éxito",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    CargarClientes();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un cliente para editar", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                int clienteID = Convert.ToInt32(dgvClientes.SelectedRows[0].Cells["ClienteID"].Value);
                string nombre = dgvClientes.SelectedRows[0].Cells["Nombre"].Value.ToString();

                DialogResult resultado = MessageBox.Show(
                    $"¿Está seguro de eliminar al cliente '{nombre}'?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        if (clienteBL.EliminarCliente(clienteID))
                        {
                            MessageBox.Show("Cliente eliminado correctamente", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarClientes();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un cliente para eliminar", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarClientes();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnBuscar.PerformClick();
                e.Handled = true;
            }
        }

        private void dgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnEditar.PerformClick();
            }
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            
        }

        

        private void lblTotal_Click(object sender, EventArgs e)
        {
            
        }

        private void ConfigurarDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;

            // Encabezados de columnas
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 136, 229);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 40;

            // Filas alternadas
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 245);

            // Selección
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 181, 246);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;

            // Grid lines
            dgv.GridColor = Color.FromArgb(224, 224, 224);

            // Permitir reordenamiento de columnas
            dgv.AllowUserToOrderColumns = true;

            // Auto ajustar columnas
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}