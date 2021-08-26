
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
            this.btnCalculadora = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCerrar
            // 
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.Location = new System.Drawing.Point(1234, 12);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(140, 149);
            this.btnCerrar.TabIndex = 10;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // txtReporte
            // 
            this.txtReporte.AllowDrop = true;
            this.txtReporte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReporte.Location = new System.Drawing.Point(0, 54);
            this.txtReporte.Multiline = true;
            this.txtReporte.Name = "txtReporte";
            this.txtReporte.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtReporte.Size = new System.Drawing.Size(1133, 409);
            this.txtReporte.TabIndex = 11;
            this.txtReporte.Text = "Jornada";
            // 
            // comboBoxOpciones
            // 
            this.comboBoxOpciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxOpciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOpciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxOpciones.FormattingEnabled = true;
            this.comboBoxOpciones.Location = new System.Drawing.Point(0, 0);
            this.comboBoxOpciones.Name = "comboBoxOpciones";
            this.comboBoxOpciones.Size = new System.Drawing.Size(1133, 54);
            this.comboBoxOpciones.TabIndex = 12;
            this.comboBoxOpciones.SelectedIndexChanged += new System.EventHandler(this.comboBoxOpciones_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtReporte);
            this.panel1.Controls.Add(this.comboBoxOpciones);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1133, 463);
            this.panel1.TabIndex = 13;
            // 
            // btnCalculadora
            // 
            this.btnCalculadora.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalculadora.Image = ((System.Drawing.Image)(resources.GetObject("btnCalculadora.Image")));
            this.btnCalculadora.Location = new System.Drawing.Point(1234, 275);
            this.btnCalculadora.Name = "btnCalculadora";
            this.btnCalculadora.Size = new System.Drawing.Size(140, 149);
            this.btnCalculadora.TabIndex = 14;
            this.btnCalculadora.Text = "Cerrar";
            this.btnCalculadora.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCalculadora.UseVisualStyleBackColor = true;
            this.btnCalculadora.Visible = false;
            this.btnCalculadora.Click += new System.EventHandler(this.btnCalculadora_Click);
            // 
            // Reportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1386, 463);
            this.ControlBox = false;
            this.Controls.Add(this.btnCalculadora);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCerrar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "Reportes";
            this.Text = "Reporte";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Reportes_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.TextBox txtReporte;
        private System.Windows.Forms.ComboBox comboBoxOpciones;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCalculadora;
    }
}