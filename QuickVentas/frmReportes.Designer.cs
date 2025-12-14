using System;
using System.Windows.Forms;

namespace QuickVentas
{
    partial class frmReportes
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
            this.dgvReportes = new System.Windows.Forms.DataGridView();
            this.lblTotalVentas = new System.Windows.Forms.Label();
            this.lblMontoTotal = new System.Windows.Forms.Label();
            this.btnActualizar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportes)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvReportes
            // 
            this.dgvReportes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReportes.Location = new System.Drawing.Point(12, 38);
            this.dgvReportes.Name = "dgvReportes";
            this.dgvReportes.RowHeadersWidth = 62;
            this.dgvReportes.RowTemplate.Height = 28;
            this.dgvReportes.Size = new System.Drawing.Size(776, 187);
            this.dgvReportes.TabIndex = 0;
            this.dgvReportes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReportes_CellContentClick);
            // 
            // lblTotalVentas
            // 
            this.lblTotalVentas.AutoSize = true;
            this.lblTotalVentas.Location = new System.Drawing.Point(50, 288);
            this.lblTotalVentas.Name = "lblTotalVentas";
            this.lblTotalVentas.Size = new System.Drawing.Size(134, 20);
            this.lblTotalVentas.TabIndex = 1;
            this.lblTotalVentas.Text = "Total de ventas: 0";
            this.lblTotalVentas.Click += new System.EventHandler(this.lblTotalVentas_Click);
            // 
            // lblMontoTotal
            // 
            this.lblMontoTotal.AutoSize = true;
            this.lblMontoTotal.Location = new System.Drawing.Point(298, 288);
            this.lblMontoTotal.Name = "lblMontoTotal";
            this.lblMontoTotal.Size = new System.Drawing.Size(106, 20);
            this.lblMontoTotal.TabIndex = 2;
            this.lblMontoTotal.Text = "Monto total: 0";
            this.lblMontoTotal.Click += new System.EventHandler(this.lblMontoTotal_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(501, 281);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(105, 35);
            this.btnActualizar.TabIndex = 3;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // frmReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.lblMontoTotal);
            this.Controls.Add(this.lblTotalVentas);
            this.Controls.Add(this.dgvReportes);
            this.Name = "frmReportes";
            this.Text = "frmReportes";
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void dgvReportes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void lblMontoTotal_Click(object sender, EventArgs e)
        {
            
        }

        private void lblTotalVentas_Click(object sender, EventArgs e)
        {

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvReportes;
        private System.Windows.Forms.Label lblTotalVentas;
        private System.Windows.Forms.Label lblMontoTotal;
        private System.Windows.Forms.Button btnActualizar;
    }
}