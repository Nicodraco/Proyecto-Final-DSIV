using System;
using System.Windows.Forms;
using System.Collections.Generic;  
using QuickVentas.LogicaNegocio;
using QuickVentas.Entidades;

namespace QuickVentas
{
    public partial class frmProductos : Form
    {
        private ProductoBL productoBL;

        public frmProductos()
        {
            InitializeComponent();
            EstilosAplicacion.AplicarEstiloFormulario(this, "Gestión de Productos");
            dgvProductos.DataBindingComplete += DgvProductos_DataBindingComplete;
            productoBL = new ProductoBL();
            CargarProductos();
        }
        private void DgvProductos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgvProductos.Columns.Contains("ProductoID"))
            {
                dgvProductos.Columns["ProductoID"].Width = 50;
            }
            
        }

        private void CargarProductos()
        {
            try
            {
                var productos = productoBL.ObtenerProductos();
                dgvProductos.DataSource = productos;

                // Esperar un momento para que se creen las columnas
                Application.DoEvents();

                // Formatear columnas solo si existen
                if (dgvProductos.Columns.Count > 0)
                {
                    // Verificar cada columna antes de acceder
                    if (dgvProductos.Columns.Contains("ProductoID"))
                    {
                        dgvProductos.Columns["ProductoID"].HeaderText = "ID";
                        dgvProductos.Columns["ProductoID"].Width = 50;
                    }

                    if (dgvProductos.Columns.Contains("Nombre"))
                    {
                        dgvProductos.Columns["Nombre"].HeaderText = "Nombre";
                        dgvProductos.Columns["Nombre"].Width = 150;
                    }

                    if (dgvProductos.Columns.Contains("Precio"))
                    {
                        dgvProductos.Columns["Precio"].HeaderText = "Precio ($)";
                        dgvProductos.Columns["Precio"].Width = 80;
                        dgvProductos.Columns["Precio"].DefaultCellStyle.Format = "N2";
                    }

                    if (dgvProductos.Columns.Contains("Stock"))
                    {
                        dgvProductos.Columns["Stock"].HeaderText = "Stock";
                        dgvProductos.Columns["Stock"].Width = 70;
                    }

                    if (dgvProductos.Columns.Contains("Categoria"))
                    {
                        dgvProductos.Columns["Categoria"].HeaderText = "Categoría";
                        dgvProductos.Columns["Categoria"].Width = 100;
                    }

                    if (dgvProductos.Columns.Contains("FechaCreacion"))
                    {
                        dgvProductos.Columns["FechaCreacion"].HeaderText = "Fecha Creación";
                        dgvProductos.Columns["FechaCreacion"].Width = 120;
                        dgvProductos.Columns["FechaCreacion"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                    }
                }

                // Actualizar label solo si existe
                if (lblTotal != null)
                {
                    lblTotal.Text = $"Total: {productos.Count} productos";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                try
                {
                    var productos = productoBL.BuscarProductos(txtBuscar.Text);
                    dgvProductos.DataSource = productos;

                    if (lblTotal != null)
                    {
                        lblTotal.Text = $"Resultados: {productos.Count} productos";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al buscar: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                CargarProductos();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            
            using (frmProductoDetalle detalle = new frmProductoDetalle())
            {
                if (detalle.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Insertar el producto usando la lógica de negocio
                        if (productoBL.InsertarProducto(detalle.Producto))
                        {
                            MessageBox.Show("Producto creado correctamente", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarProductos();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al crear producto: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                // Obtener ID del producto seleccionado
                int productoID = Convert.ToInt32(dgvProductos.SelectedRows[0].Cells["ProductoID"].Value);

                try
                {
                    // Buscar el producto en la lista actual
                    var productos = (List<Producto>)dgvProductos.DataSource;
                    Producto producto = productos.Find(p => p.ProductoID == productoID);

                    if (producto != null)
                    {
                        using (frmProductoDetalle detalle = new frmProductoDetalle(producto))
                        {
                            if (detalle.ShowDialog() == DialogResult.OK)
                            {
                                // Actualizar el producto
                                if (productoBL.ActualizarProducto(detalle.Producto))
                                {
                                    MessageBox.Show("Producto actualizado correctamente", "Éxito",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    CargarProductos();
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
                MessageBox.Show("Seleccione un producto para editar", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                int productoID = Convert.ToInt32(dgvProductos.SelectedRows[0].Cells["ProductoID"].Value);
                string nombre = dgvProductos.SelectedRows[0].Cells["Nombre"].Value.ToString();

                DialogResult resultado = MessageBox.Show(
                    $"¿Está seguro de eliminar el producto '{nombre}'?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        if (productoBL.EliminarProducto(productoID))
                        {
                            MessageBox.Show("Producto eliminado correctamente", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarProductos();
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
                MessageBox.Show("Seleccione un producto para eliminar", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarProductos();
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnBuscar.PerformClick();
                e.Handled = true;
            }
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnEditar.PerformClick();
            }
        }
    }
}