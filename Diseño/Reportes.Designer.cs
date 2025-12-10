
namespace Senga
{
    partial class Reportes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reportes));
            this.btnCerrar = new System.Windows.Forms.Button();
            this.txtReporte = new System.Windows.Forms.TextBox();
            this.comboBoxOpciones = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridSQLResultados = new System.Windows.Forms.DataGridView();
            this.btnCalculadora = new System.Windows.Forms.Button();
            this.btnInfo = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSQLResultados)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCerrar
            // 
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.Location = new System.Drawing.Point(1234, 12);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(120, 149);
            this.btnCerrar.TabIndex = 10;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // txtReporte
            // 
            this.txtReporte.AllowDrop = true;
            this.txtReporte.BackColor = System.Drawing.Color.Black;
            this.txtReporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReporte.ForeColor = System.Drawing.Color.Lime;
            this.txtReporte.Location = new System.Drawing.Point(12, 12);
            this.txtReporte.Multiline = true;
            this.txtReporte.Name = "txtReporte";
            this.txtReporte.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtReporte.Size = new System.Drawing.Size(156, 46);
            this.txtReporte.TabIndex = 11;
            this.txtReporte.Text = "Jornada";
            this.txtReporte.Visible = false;
            // 
            // comboBoxOpciones
            // 
            this.comboBoxOpciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxOpciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOpciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxOpciones.FormattingEnabled = true;
            this.comboBoxOpciones.Location = new System.Drawing.Point(0, 0);
            this.comboBoxOpciones.Name = "comboBoxOpciones";
            this.comboBoxOpciones.Size = new System.Drawing.Size(1386, 54);
            this.comboBoxOpciones.TabIndex = 12;
            this.comboBoxOpciones.SelectedIndexChanged += new System.EventHandler(this.comboBoxOpciones_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridSQLResultados);
            this.panel1.Controls.Add(this.comboBoxOpciones);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 167);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1386, 296);
            this.panel1.TabIndex = 13;
            // 
            // dataGridSQLResultados
            // 
            this.dataGridSQLResultados.AllowDrop = true;
            this.dataGridSQLResultados.AllowUserToAddRows = false;
            this.dataGridSQLResultados.AllowUserToDeleteRows = false;
            this.dataGridSQLResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridSQLResultados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridSQLResultados.Location = new System.Drawing.Point(0, 54);
            this.dataGridSQLResultados.Name = "dataGridSQLResultados";
            this.dataGridSQLResultados.ReadOnly = true;
            this.dataGridSQLResultados.RowHeadersWidth = 70;
            this.dataGridSQLResultados.RowTemplate.Height = 35;
            this.dataGridSQLResultados.Size = new System.Drawing.Size(1386, 242);
            this.dataGridSQLResultados.TabIndex = 81;
            // 
            // btnCalculadora
            // 
            this.btnCalculadora.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalculadora.Image = ((System.Drawing.Image)(resources.GetObject("btnCalculadora.Image")));
            this.btnCalculadora.Location = new System.Drawing.Point(1068, 12);
            this.btnCalculadora.Name = "btnCalculadora";
            this.btnCalculadora.Size = new System.Drawing.Size(120, 149);
            this.btnCalculadora.TabIndex = 14;
            this.btnCalculadora.Text = "Calculador";
            this.btnCalculadora.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCalculadora.UseVisualStyleBackColor = true;
            this.btnCalculadora.Click += new System.EventHandler(this.btnCalculadora_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInfo.Location = new System.Drawing.Point(675, 12);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(120, 149);
            this.btnInfo.TabIndex = 15;
            this.btnInfo.Text = "Info";
            this.btnInfo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // Reportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1386, 463);
            this.ControlBox = false;
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.btnCalculadora);
            this.Controls.Add(this.txtReporte);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCerrar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "Reportes";
            this.ShowInTaskbar = false;
            this.Text = "Reporte";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Reportes_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSQLResultados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.TextBox txtReporte;
        private System.Windows.Forms.ComboBox comboBoxOpciones;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCalculadora;
        private System.Windows.Forms.DataGridView dataGridSQLResultados;
        private System.Windows.Forms.Button btnInfo;
    }
}