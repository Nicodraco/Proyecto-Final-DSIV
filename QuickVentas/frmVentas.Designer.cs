using System;
using System.Windows.Forms;

namespace QuickVentas
{
    partial class frmVentas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbClientes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvProductosDisponibles = new System.Windows.Forms.DataGridView();
            this.numCantidad = new System.Windows.Forms.NumericUpDown();
            this.btnAgregarProducto = new System.Windows.Forms.Button();
            this.dgvDetallesVenta = new System.Windows.Forms.DataGridView();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnProcesarVenta = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductosDisponibles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallesVenta)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbClientes
            // 
            this.cmbClientes.FormattingEnabled = true;
            this.cmbClientes.Location = new System.Drawing.Point(58, 55);
            this.cmbClientes.Name = "cmbClientes";
            this.cmbClientes.Size = new System.Drawing.Size(121, 28);
            this.cmbClientes.TabIndex = 0;
            this.cmbClientes.SelectedIndexChanged += new System.EventHandler(this.cmbClientes_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cliente:";
            // 
            // dgvProductosDisponibles
            // 
            this.dgvProductosDisponibles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductosDisponibles.Location = new System.Drawing.Point(58, 89);
            this.dgvProductosDisponibles.Name = "dgvProductosDisponibles";
            this.dgvProductosDisponibles.RowHeadersWidth = 62;
            this.dgvProductosDisponibles.RowTemplate.Height = 28;
            this.dgvProductosDisponibles.Size = new System.Drawing.Size(714, 137);
            this.dgvProductosDisponibles.TabIndex = 2;
            this.dgvProductosDisponibles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductosDisponibles_CellContentClick);
            // 
            // numCantidad
            // 
            this.numCantidad.Location = new System.Drawing.Point(216, 57);
            this.numCantidad.Name = "numCantidad";
            this.numCantidad.Size = new System.Drawing.Size(120, 26);
            this.numCantidad.TabIndex = 3;
            this.numCantidad.ValueChanged += new System.EventHandler(this.numCantidad_ValueChanged);
            // 
            // btnAgregarProducto
            // 
            this.btnAgregarProducto.Location = new System.Drawing.Point(151, 8);
            this.btnAgregarProducto.Name = "btnAgregarProducto";
            this.btnAgregarProducto.Size = new System.Drawing.Size(176, 26);
            this.btnAgregarProducto.TabIndex = 4;
            this.btnAgregarProducto.Text = "AgregarProducto";
            this.btnAgregarProducto.UseVisualStyleBackColor = true;
            this.btnAgregarProducto.Click += new System.EventHandler(this.btnAgregarProducto_Click);
            // 
            // dgvDetallesVenta
            // 
            this.dgvDetallesVenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetallesVenta.Location = new System.Drawing.Point(58, 232);
            this.dgvDetallesVenta.Name = "dgvDetallesVenta";
            this.dgvDetallesVenta.RowHeadersWidth = 62;
            this.dgvDetallesVenta.RowTemplate.Height = 28;
            this.dgvDetallesVenta.Size = new System.Drawing.Size(714, 136);
            this.dgvDetallesVenta.TabIndex = 5;
            this.dgvDetallesVenta.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetallesVenta_CellContentClick);
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Location = new System.Drawing.Point(378, 8);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(73, 20);
            this.lblSubtotal.TabIndex = 6;
            this.lblSubtotal.Text = "SubTotal";
            this.lblSubtotal.Click += new System.EventHandler(this.lblSubtotal_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(559, 8);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(44, 20);
            this.lblTotal.TabIndex = 7;
            this.lblTotal.Text = "Total";
            this.lblTotal.Click += new System.EventHandler(this.lblTotal_Click);
            // 
            // btnProcesarVenta
            // 
            this.btnProcesarVenta.Location = new System.Drawing.Point(382, 40);
            this.btnProcesarVenta.Name = "btnProcesarVenta";
            this.btnProcesarVenta.Size = new System.Drawing.Size(134, 31);
            this.btnProcesarVenta.TabIndex = 8;
            this.btnProcesarVenta.Text = "ProcesarVenta";
            this.btnProcesarVenta.UseVisualStyleBackColor = true;
            this.btnProcesarVenta.Click += new System.EventHandler(this.btnProcesarVenta_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(552, 40);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(93, 28);
            this.btnCancelar.TabIndex = 9;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // frmVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnProcesarVenta);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblSubtotal);
            this.Controls.Add(this.dgvDetallesVenta);
            this.Controls.Add(this.btnAgregarProducto);
            this.Controls.Add(this.numCantidad);
            this.Controls.Add(this.dgvProductosDisponibles);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbClientes);
            this.Name = "frmVentas";
            this.Text = "frmVentas";
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductosDisponibles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallesVenta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void lblTotal_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void lblSubtotal_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void dgvDetallesVenta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.ComboBox cmbClientes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvProductosDisponibles;
        private System.Windows.Forms.NumericUpDown numCantidad;
        private System.Windows.Forms.Button btnAgregarProducto;
        private System.Windows.Forms.DataGridView dgvDetallesVenta;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnProcesarVenta;
        private System.Windows.Forms.Button btnCancelar;
    }
}