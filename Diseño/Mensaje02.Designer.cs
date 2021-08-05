namespace Senga
{
    partial class Mensaje02
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mensaje02));
            this.btnNo = new System.Windows.Forms.Button();
            this.btnSi = new System.Windows.Forms.Button();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.picImagen = new System.Windows.Forms.PictureBox();
            this.imagenLista = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImagen)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNo
            // 
            this.btnNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNo.Location = new System.Drawing.Point(306, 220);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(166, 150);
            this.btnNo.TabIndex = 4;
            this.btnNo.Text = "NO";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // btnSi
            // 
            this.btnSi.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSi.Location = new System.Drawing.Point(70, 222);
            this.btnSi.Name = "btnSi";
            this.btnSi.Size = new System.Drawing.Size(166, 148);
            this.btnSi.TabIndex = 3;
            this.btnSi.Text = "SI";
            this.btnSi.UseVisualStyleBackColor = true;
            this.btnSi.Click += new System.EventHandler(this.btnSi_Click);
            // 
            // lblMensaje
            // 
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.Location = new System.Drawing.Point(6, 16);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(664, 201);
            this.lblMensaje.TabIndex = 5;
            this.lblMensaje.Text = "Mensaje";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.picImagen);
            this.groupBox1.Controls.Add(this.lblMensaje);
            this.groupBox1.Controls.Add(this.btnSi);
            this.groupBox1.Controls.Add(this.btnNo);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(676, 376);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // picImagen
            // 
            this.picImagen.InitialImage = null;
            this.picImagen.Location = new System.Drawing.Point(539, 248);
            this.picImagen.Name = "picImagen";
            this.picImagen.Size = new System.Drawing.Size(96, 96);
            this.picImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picImagen.TabIndex = 6;
            this.picImagen.TabStop = false;
            // 
            // imagenLista
            // 
            this.imagenLista.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagenLista.ImageStream")));
            this.imagenLista.TransparentColor = System.Drawing.Color.Transparent;
            this.imagenLista.Images.SetKeyName(0, "ok.png");
            this.imagenLista.Images.SetKeyName(1, "warning.png");
            this.imagenLista.Images.SetKeyName(2, "stop.png");
            this.imagenLista.Images.SetKeyName(3, "question.png");
            // 
            // Mensaje02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(700, 400);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Mensaje02";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mensajes";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImagen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Button btnSi;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ImageList imagenLista;
        private System.Windows.Forms.PictureBox picImagen;
    }
}