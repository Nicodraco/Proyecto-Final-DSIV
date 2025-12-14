using QuickVentas.Entidades;
using QuickVentas.LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace QuickVentas
{
    public partial class frmVentas : Form
    {
        private List<Producto> productosDisponibles;
        private Venta ventaActual;
        private ProductoBL productoBL;
        private ClienteBL clienteBL;
        private VentaBL ventaBL;
        

        public frmVentas()
        {
            InitializeComponent();
            EstilosAplicacion.AplicarEstiloFormulario(this, "Registro de Ventas");
            InicializarControles();
            CargarDatos();
        }

        private void InicializarControles()
        {
            productosDisponibles = new List<Producto>();
            ventaActual = new Venta();
            productoBL = new ProductoBL();
            clienteBL = new ClienteBL();
            ventaBL = new VentaBL();

            numCantidad.Minimum = 1;
            numCantidad.Value = 1;
        }

        private void CargarDatos()
        {
            try
            {
                // Cargar productos disponibles
                productosDisponibles = productoBL.ObtenerProductos();
                dgvProductosDisponibles.DataSource = productosDisponibles;

                // Configurar columnas de productos
                if (dgvProductosDisponibles.Columns.Count > 0)
                {
                    dgvProductosDisponibles.Columns["ProductoID"].Visible = false;
                    dgvProductosDisponibles.Columns["Nombre"].HeaderText = "Producto";
                    dgvProductosDisponibles.Columns["Precio"].HeaderText = "Precio";
                    dgvProductosDisponibles.Columns["Stock"].HeaderText = "Disponible";
                    dgvProductosDisponibles.Columns["Categoria"].HeaderText = "Categoría";
                }

                // Cargar clientes en ComboBox
                var clientes = clienteBL.ObtenerClientes();
                cmbClientes.DataSource = clientes;
                cmbClientes.DisplayMember = "Nombre";
                cmbClientes.ValueMember = "ClienteID";
                cmbClientes.SelectedIndex = -1; // Ninguno seleccionado

                // Configurar DataGridView de detalles de venta
                dgvDetallesVenta.AutoGenerateColumns = false;
                dgvDetallesVenta.Columns.Clear();

                // Agregar columnas manualmente
                dgvDetallesVenta.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Producto",
                    HeaderText = "Producto",
                    DataPropertyName = "NombreProducto",
                    Width = 150
                });

                dgvDetallesVenta.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Cantidad",
                    HeaderText = "Cantidad",
                    DataPropertyName = "Cantidad",
                    Width = 80
                });

                dgvDetallesVenta.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Precio",
                    HeaderText = "Precio Unitario",
                    DataPropertyName = "PrecioUnitario",
                    Width = 100
                });

                dgvDetallesVenta.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Subtotal",
                    HeaderText = "Subtotal",
                    DataPropertyName = "Subtotal",
                    Width = 100
                });

                ActualizarTotales();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (dgvProductosDisponibles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto de la lista", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int productoID = Convert.ToInt32(dgvProductosDisponibles.SelectedRows[0].Cells["ProductoID"].Value);
            Producto producto = productosDisponibles.Find(p => p.ProductoID == productoID);

            if (producto == null)
            {
                MessageBox.Show("Producto no encontrado", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int cantidad = (int)numCantidad.Value;

            // Verificar stock disponible
            if (cantidad > producto.Stock)
            {
                MessageBox.Show($"Stock insuficiente. Disponible: {producto.Stock}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Verificar si el producto ya está en la venta
            var detalleExistente = ventaActual.Detalles.Find(d => d.ProductoID == productoID);

            if (detalleExistente != null)
            {
                // Actualizar cantidad del producto existente
                detalleExistente.Cantidad += cantidad;
                detalleExistente.CalcularSubtotal();
            }
            else
            {
                // Agregar nuevo detalle
                var nuevoDetalle = new VentaDetalle
                {
                    ProductoID = producto.ProductoID,
                    NombreProducto = producto.Nombre,
                    Cantidad = cantidad,
                    PrecioUnitario = producto.Precio
                };
                nuevoDetalle.CalcularSubtotal();

                ventaActual.Detalles.Add(nuevoDetalle);
            }

            // Actualizar DataGridView
            dgvDetallesVenta.DataSource = null;
            dgvDetallesVenta.DataSource = ventaActual.Detalles;

            // Actualizar totales
            ActualizarTotales();

            // Resetear cantidad
            numCantidad.Value = 1;
        }

        private void ActualizarTotales()
        {
            decimal subtotal = 0;

            foreach (var detalle in ventaActual.Detalles)
            {
                subtotal += detalle.Subtotal;
            }

            ventaActual.Total = subtotal; 

            lblSubtotal.Text = $"Subtotal: ${subtotal:N2}";
            lblTotal.Text = $"Total: ${ventaActual.Total:N2}";
        }

        private void btnProcesarVenta_Click(object sender, EventArgs e)
        {
            if (ventaActual.Detalles.Count == 0)
            {
                MessageBox.Show("Agregue al menos un producto a la venta", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Asignar cliente si se seleccionó uno
            if (cmbClientes.SelectedIndex >= 0)
            {
                ventaActual.ClienteID = (int)cmbClientes.SelectedValue;
            }

            DialogResult confirmacion = MessageBox.Show(
                $"¿Confirmar venta por ${ventaActual.Total:N2}?",
                "Confirmar Venta",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    int ventaID = ventaBL.ProcesarVenta(ventaActual);

                    MessageBox.Show($"Venta #${ventaID} procesada exitosamente\nTotal: ${ventaActual.Total:N2}",
                        "Venta Exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Limpiar venta actual
                    ventaActual = new Venta();
                    dgvDetallesVenta.DataSource = null;
                    ActualizarTotales();

                    // Recargar productos (stock actualizado)
                    CargarDatos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al procesar venta: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDetallesVenta_KeyDown(object sender, KeyEventArgs e)
        {
            // Permitir eliminar productos con tecla Delete
            if (e.KeyCode == Keys.Delete && dgvDetallesVenta.SelectedRows.Count > 0)
            {
                int index = dgvDetallesVenta.SelectedRows[0].Index;
                if (index >= 0 && index < ventaActual.Detalles.Count)
                {
                    ventaActual.Detalles.RemoveAt(index);
                    dgvDetallesVenta.DataSource = null;
                    dgvDetallesVenta.DataSource = ventaActual.Detalles;
                    ActualizarTotales();
                }
            }
        }

        // Métodos para eventos automáticos 
        private void cmbClientes_SelectedIndexChanged(object sender, EventArgs e) { }
        private void numCantidad_ValueChanged(object sender, EventArgs e) { }
        private void dgvProductosDisponibles_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}