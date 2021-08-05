namespace Senga
{
    partial class Racion 
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Racion));
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnHecho = new System.Windows.Forms.Button();
            this.txtTamano = new System.Windows.Forms.TextBox();
            this.lblTamaño = new System.Windows.Forms.Label();
            this.txtCargado = new System.Windows.Forms.TextBox();
            this.lblCargado = new System.Windows.Forms.Label();
            this.btnTara = new System.Windows.Forms.Button();
            this.txtBruto = new System.Windows.Forms.TextBox();
            this.lblBruto = new System.Windows.Forms.Label();
            this.lblRacion = new System.Windows.Forms.Label();
            this.timerR = new System.Windows.Forms.Timer(this.components);
            this.lblEstable = new System.Windows.Forms.Label();
            this.btnRacion = new System.Windows.Forms.Button();
            this.txtServida = new System.Windows.Forms.TextBox();
            this.txtCapacidad = new System.Windows.Forms.TextBox();
            this.txtCargadoIni = new System.Windows.Forms.TextBox();
            this.txtBrutoV = new System.Windows.Forms.TextBox();
            this.txtCargadoV = new System.Windows.Forms.TextBox();
            this.txtTamanoV = new System.Windows.Forms.TextBox();
            this.txtFisicoV = new System.Windows.Forms.TextBox();
            this.lblFisico = new System.Windows.Forms.Label();
            this.txtFisico = new System.Windows.Forms.TextBox();
            this.txtFisicoIni = new System.Windows.Forms.TextBox();
            this.pbFisico = new System.Windows.Forms.ProgressBar();
            this.pbCargado = new System.Windows.Forms.ProgressBar();
            this.picFisico = new System.Windows.Forms.PictureBox();
            this.picFisicoF = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelAnimacion = new System.Windows.Forms.Panel();
            this.lblPorcentaje = new System.Windows.Forms.Label();
            this.lblVueltas = new System.Windows.Forms.Label();
            this.txtServido = new System.Windows.Forms.TextBox();
            this.txtTamanoAct = new System.Windows.Forms.TextBox();
            this.txtFalta = new System.Windows.Forms.TextBox();
            this.txtFaltaV = new System.Windows.Forms.TextBox();
            this.pbCarga = new System.Windows.Forms.ProgressBar();
            this.panelValores = new System.Windows.Forms.Panel();
            this.btnSolicitar = new System.Windows.Forms.Button();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.btnTolva = new System.Windows.Forms.Button();
            this.lblTolva = new System.Windows.Forms.Label();
            this.lblZero = new System.Windows.Forms.Label();
            this.pbVariacion = new System.Windows.Forms.ProgressBar();
            this.txtBascula = new System.Windows.Forms.TextBox();
            this.txtStop = new System.Windows.Forms.TextBox();
            this.txtStopV = new System.Windows.Forms.TextBox();
            this.txtPlanta = new System.Windows.Forms.TextBox();
            this.timerBeep = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picFisico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFisicoF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelAnimacion.SuspendLayout();
            this.panelValores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.Location = new System.Drawing.Point(590, 95);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(286, 62);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnHecho
            // 
            this.btnHecho.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHecho.Image = ((System.Drawing.Image)(resources.GetObject("btnHecho.Image")));
            this.btnHecho.Location = new System.Drawing.Point(586, 12);
            this.btnHecho.Name = "btnHecho";
            this.btnHecho.Size = new System.Drawing.Size(290, 77);
            this.btnHecho.TabIndex = 9;
            this.btnHecho.Text = "HECHO";
            this.btnHecho.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHecho.UseVisualStyleBackColor = true;
            this.btnHecho.Click += new System.EventHandler(this.btnHecho_Click);
            // 
            // txtTamano
            // 
            this.txtTamano.BackColor = System.Drawing.Color.White;
            this.txtTamano.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTamano.ForeColor = System.Drawing.Color.Black;
            this.txtTamano.Location = new System.Drawing.Point(954, 101);
            this.txtTamano.Name = "txtTamano";
            this.txtTamano.ReadOnly = true;
            this.txtTamano.Size = new System.Drawing.Size(17, 83);
            this.txtTamano.TabIndex = 46;
            this.txtTamano.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTamano.Visible = false;
            // 
            // lblTamaño
            // 
            this.lblTamaño.AutoSize = true;
            this.lblTamaño.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTamaño.Location = new System.Drawing.Point(204, 229);
            this.lblTamaño.Name = "lblTamaño";
            this.lblTamaño.Size = new System.Drawing.Size(81, 25);
            this.lblTamaño.TabIndex = 45;
            this.lblTamaño.Text = "Solicitar";
            // 
            // txtCargado
            // 
            this.txtCargado.BackColor = System.Drawing.Color.White;
            this.txtCargado.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCargado.ForeColor = System.Drawing.Color.Black;
            this.txtCargado.Location = new System.Drawing.Point(923, 101);
            this.txtCargado.Name = "txtCargado";
            this.txtCargado.ReadOnly = true;
            this.txtCargado.Size = new System.Drawing.Size(25, 83);
            this.txtCargado.TabIndex = 48;
            this.txtCargado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCargado.Visible = false;
            // 
            // lblCargado
            // 
            this.lblCargado.AutoSize = true;
            this.lblCargado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCargado.Location = new System.Drawing.Point(204, 340);
            this.lblCargado.Name = "lblCargado";
            this.lblCargado.Size = new System.Drawing.Size(66, 25);
            this.lblCargado.TabIndex = 47;
            this.lblCargado.Text = "Carga";
            // 
            // btnTara
            // 
            this.btnTara.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTara.Image = ((System.Drawing.Image)(resources.GetObject("btnTara.Image")));
            this.btnTara.Location = new System.Drawing.Point(593, 163);
            this.btnTara.Name = "btnTara";
            this.btnTara.Size = new System.Drawing.Size(227, 55);
            this.btnTara.TabIndex = 49;
            this.btnTara.Text = "TARA";
            this.btnTara.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTara.UseVisualStyleBackColor = true;
            this.btnTara.Visible = false;
            // 
            // txtBruto
            // 
            this.txtBruto.BackColor = System.Drawing.Color.Black;
            this.txtBruto.Font = new System.Drawing.Font("Microsoft Sans Serif", 70F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBruto.ForeColor = System.Drawing.Color.Lime;
            this.txtBruto.Location = new System.Drawing.Point(882, 13);
            this.txtBruto.Name = "txtBruto";
            this.txtBruto.Size = new System.Drawing.Size(37, 113);
            this.txtBruto.TabIndex = 51;
            this.txtBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBruto.Visible = false;
            // 
            // lblBruto
            // 
            this.lblBruto.AutoSize = true;
            this.lblBruto.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBruto.Location = new System.Drawing.Point(204, 570);
            this.lblBruto.Name = "lblBruto";
            this.lblBruto.Size = new System.Drawing.Size(82, 25);
            this.lblBruto.TabIndex = 50;
            this.lblBruto.Text = "Bascula";
            // 
            // lblRacion
            // 
            this.lblRacion.AutoSize = true;
            this.lblRacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRacion.Location = new System.Drawing.Point(204, 119);
            this.lblRacion.Name = "lblRacion";
            this.lblRacion.Size = new System.Drawing.Size(72, 25);
            this.lblRacion.TabIndex = 52;
            this.lblRacion.Text = "Racion";
            // 
            // timerR
            // 
            this.timerR.Interval = 1000;
            this.timerR.Tick += new System.EventHandler(this.timerR_Tick);
            // 
            // lblEstable
            // 
            this.lblEstable.AutoSize = true;
            this.lblEstable.BackColor = System.Drawing.Color.Black;
            this.lblEstable.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstable.ForeColor = System.Drawing.Color.Lime;
            this.lblEstable.Location = new System.Drawing.Point(30, 608);
            this.lblEstable.Name = "lblEstable";
            this.lblEstable.Size = new System.Drawing.Size(33, 31);
            this.lblEstable.TabIndex = 62;
            this.lblEstable.Text = "E";
            this.lblEstable.Visible = false;
            // 
            // btnRacion
            // 
            this.btnRacion.BackColor = System.Drawing.SystemColors.Control;
            this.btnRacion.Enabled = false;
            this.btnRacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRacion.Location = new System.Drawing.Point(21, 144);
            this.btnRacion.Name = "btnRacion";
            this.btnRacion.Size = new System.Drawing.Size(513, 83);
            this.btnRacion.TabIndex = 63;
            this.btnRacion.Text = "R4";
            this.btnRacion.UseVisualStyleBackColor = false;
            this.btnRacion.Click += new System.EventHandler(this.btnRacion_Click);
            // 
            // txtServida
            // 
            this.txtServida.BackColor = System.Drawing.Color.White;
            this.txtServida.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServida.ForeColor = System.Drawing.Color.Black;
            this.txtServida.Location = new System.Drawing.Point(1002, 102);
            this.txtServida.Name = "txtServida";
            this.txtServida.ReadOnly = true;
            this.txtServida.Size = new System.Drawing.Size(24, 38);
            this.txtServida.TabIndex = 64;
            this.txtServida.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtServida.Visible = false;
            // 
            // txtCapacidad
            // 
            this.txtCapacidad.BackColor = System.Drawing.Color.White;
            this.txtCapacidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCapacidad.ForeColor = System.Drawing.Color.Black;
            this.txtCapacidad.Location = new System.Drawing.Point(1029, 102);
            this.txtCapacidad.Name = "txtCapacidad";
            this.txtCapacidad.ReadOnly = true;
            this.txtCapacidad.Size = new System.Drawing.Size(24, 38);
            this.txtCapacidad.TabIndex = 65;
            this.txtCapacidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCapacidad.Visible = false;
            // 
            // txtCargadoIni
            // 
            this.txtCargadoIni.BackColor = System.Drawing.Color.White;
            this.txtCargadoIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCargadoIni.ForeColor = System.Drawing.Color.Black;
            this.txtCargadoIni.Location = new System.Drawing.Point(1002, 146);
            this.txtCargadoIni.Name = "txtCargadoIni";
            this.txtCargadoIni.ReadOnly = true;
            this.txtCargadoIni.Size = new System.Drawing.Size(24, 38);
            this.txtCargadoIni.TabIndex = 66;
            this.txtCargadoIni.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCargadoIni.Visible = false;
            // 
            // txtBrutoV
            // 
            this.txtBrutoV.BackColor = System.Drawing.Color.Black;
            this.txtBrutoV.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBrutoV.ForeColor = System.Drawing.Color.Lime;
            this.txtBrutoV.Location = new System.Drawing.Point(21, 598);
            this.txtBrutoV.Name = "txtBrutoV";
            this.txtBrutoV.Size = new System.Drawing.Size(513, 83);
            this.txtBrutoV.TabIndex = 67;
            this.txtBrutoV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBrutoV.TextChanged += new System.EventHandler(this.txtBrutoV_TextChanged);
            // 
            // txtCargadoV
            // 
            this.txtCargadoV.BackColor = System.Drawing.Color.White;
            this.txtCargadoV.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCargadoV.ForeColor = System.Drawing.Color.Black;
            this.txtCargadoV.Location = new System.Drawing.Point(21, 368);
            this.txtCargadoV.Name = "txtCargadoV";
            this.txtCargadoV.ReadOnly = true;
            this.txtCargadoV.Size = new System.Drawing.Size(513, 83);
            this.txtCargadoV.TabIndex = 68;
            this.txtCargadoV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTamanoV
            // 
            this.txtTamanoV.BackColor = System.Drawing.Color.White;
            this.txtTamanoV.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTamanoV.ForeColor = System.Drawing.Color.Black;
            this.txtTamanoV.Location = new System.Drawing.Point(21, 255);
            this.txtTamanoV.Name = "txtTamanoV";
            this.txtTamanoV.ReadOnly = true;
            this.txtTamanoV.Size = new System.Drawing.Size(513, 83);
            this.txtTamanoV.TabIndex = 69;
            this.txtTamanoV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtFisicoV
            // 
            this.txtFisicoV.BackColor = System.Drawing.Color.White;
            this.txtFisicoV.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFisicoV.ForeColor = System.Drawing.Color.Black;
            this.txtFisicoV.Location = new System.Drawing.Point(978, 12);
            this.txtFisicoV.Name = "txtFisicoV";
            this.txtFisicoV.ReadOnly = true;
            this.txtFisicoV.Size = new System.Drawing.Size(24, 83);
            this.txtFisicoV.TabIndex = 70;
            this.txtFisicoV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFisicoV.Visible = false;
            // 
            // lblFisico
            // 
            this.lblFisico.AutoSize = true;
            this.lblFisico.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFisico.Location = new System.Drawing.Point(204, 457);
            this.lblFisico.Name = "lblFisico";
            this.lblFisico.Size = new System.Drawing.Size(94, 25);
            this.lblFisico.TabIndex = 71;
            this.lblFisico.Text = "Variacion";
            // 
            // txtFisico
            // 
            this.txtFisico.BackColor = System.Drawing.Color.White;
            this.txtFisico.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFisico.ForeColor = System.Drawing.Color.Black;
            this.txtFisico.Location = new System.Drawing.Point(931, 13);
            this.txtFisico.Name = "txtFisico";
            this.txtFisico.ReadOnly = true;
            this.txtFisico.Size = new System.Drawing.Size(40, 83);
            this.txtFisico.TabIndex = 72;
            this.txtFisico.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFisico.Visible = false;
            // 
            // txtFisicoIni
            // 
            this.txtFisicoIni.BackColor = System.Drawing.Color.White;
            this.txtFisicoIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFisicoIni.ForeColor = System.Drawing.Color.Black;
            this.txtFisicoIni.Location = new System.Drawing.Point(1029, 146);
            this.txtFisicoIni.Name = "txtFisicoIni";
            this.txtFisicoIni.ReadOnly = true;
            this.txtFisicoIni.Size = new System.Drawing.Size(24, 38);
            this.txtFisicoIni.TabIndex = 73;
            this.txtFisicoIni.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFisicoIni.Visible = false;
            // 
            // pbFisico
            // 
            this.pbFisico.Location = new System.Drawing.Point(957, 190);
            this.pbFisico.Name = "pbFisico";
            this.pbFisico.Size = new System.Drawing.Size(88, 37);
            this.pbFisico.TabIndex = 74;
            this.pbFisico.Visible = false;
            // 
            // pbCargado
            // 
            this.pbCargado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbCargado.Location = new System.Drawing.Point(0, 98);
            this.pbCargado.Name = "pbCargado";
            this.pbCargado.Size = new System.Drawing.Size(446, 45);
            this.pbCargado.TabIndex = 75;
            this.pbCargado.Visible = false;
            // 
            // picFisico
            // 
            this.picFisico.BackgroundImage = global::Senga.Properties.Resources.barrafull;
            this.picFisico.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picFisico.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picFisico.Location = new System.Drawing.Point(80, 21);
            this.picFisico.Name = "picFisico";
            this.picFisico.Size = new System.Drawing.Size(107, 100);
            this.picFisico.TabIndex = 76;
            this.picFisico.TabStop = false;
            // 
            // picFisicoF
            // 
            this.picFisicoF.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picFisicoF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picFisicoF.Location = new System.Drawing.Point(80, 21);
            this.picFisicoF.Name = "picFisicoF";
            this.picFisicoF.Size = new System.Drawing.Size(107, 100);
            this.picFisicoF.TabIndex = 77;
            this.picFisicoF.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(3, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(328, 195);
            this.pictureBox1.TabIndex = 78;
            this.pictureBox1.TabStop = false;
            // 
            // panelAnimacion
            // 
            this.panelAnimacion.Controls.Add(this.lblPorcentaje);
            this.panelAnimacion.Controls.Add(this.picFisicoF);
            this.panelAnimacion.Controls.Add(this.picFisico);
            this.panelAnimacion.Controls.Add(this.pictureBox1);
            this.panelAnimacion.Location = new System.Drawing.Point(586, 287);
            this.panelAnimacion.Name = "panelAnimacion";
            this.panelAnimacion.Size = new System.Drawing.Size(450, 245);
            this.panelAnimacion.TabIndex = 79;
            this.panelAnimacion.Visible = false;
            // 
            // lblPorcentaje
            // 
            this.lblPorcentaje.AutoSize = true;
            this.lblPorcentaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 70F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPorcentaje.Location = new System.Drawing.Point(319, 39);
            this.lblPorcentaje.Name = "lblPorcentaje";
            this.lblPorcentaje.Size = new System.Drawing.Size(206, 107);
            this.lblPorcentaje.TabIndex = 79;
            this.lblPorcentaje.Text = "0 %";
            // 
            // lblVueltas
            // 
            this.lblVueltas.AutoSize = true;
            this.lblVueltas.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVueltas.Location = new System.Drawing.Point(3, 3);
            this.lblVueltas.Name = "lblVueltas";
            this.lblVueltas.Size = new System.Drawing.Size(245, 63);
            this.lblVueltas.TabIndex = 80;
            this.lblVueltas.Text = "0 Cargas";
            this.lblVueltas.Visible = false;
            // 
            // txtServido
            // 
            this.txtServido.BackColor = System.Drawing.Color.White;
            this.txtServido.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServido.ForeColor = System.Drawing.Color.Black;
            this.txtServido.Location = new System.Drawing.Point(1029, 13);
            this.txtServido.Name = "txtServido";
            this.txtServido.ReadOnly = true;
            this.txtServido.Size = new System.Drawing.Size(20, 83);
            this.txtServido.TabIndex = 80;
            this.txtServido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtServido.Visible = false;
            // 
            // txtTamanoAct
            // 
            this.txtTamanoAct.BackColor = System.Drawing.Color.White;
            this.txtTamanoAct.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTamanoAct.ForeColor = System.Drawing.Color.Black;
            this.txtTamanoAct.Location = new System.Drawing.Point(977, 101);
            this.txtTamanoAct.Name = "txtTamanoAct";
            this.txtTamanoAct.ReadOnly = true;
            this.txtTamanoAct.Size = new System.Drawing.Size(17, 83);
            this.txtTamanoAct.TabIndex = 81;
            this.txtTamanoAct.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTamanoAct.Visible = false;
            // 
            // txtFalta
            // 
            this.txtFalta.BackColor = System.Drawing.Color.White;
            this.txtFalta.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFalta.ForeColor = System.Drawing.Color.Black;
            this.txtFalta.Location = new System.Drawing.Point(1008, 13);
            this.txtFalta.Name = "txtFalta";
            this.txtFalta.ReadOnly = true;
            this.txtFalta.Size = new System.Drawing.Size(15, 83);
            this.txtFalta.TabIndex = 82;
            this.txtFalta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFalta.Visible = false;
            // 
            // txtFaltaV
            // 
            this.txtFaltaV.BackColor = System.Drawing.Color.White;
            this.txtFaltaV.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFaltaV.ForeColor = System.Drawing.Color.Black;
            this.txtFaltaV.Location = new System.Drawing.Point(21, 485);
            this.txtFaltaV.Name = "txtFaltaV";
            this.txtFaltaV.ReadOnly = true;
            this.txtFaltaV.Size = new System.Drawing.Size(513, 83);
            this.txtFaltaV.TabIndex = 83;
            this.txtFaltaV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pbCarga
            // 
            this.pbCarga.Location = new System.Drawing.Point(923, 187);
            this.pbCarga.Name = "pbCarga";
            this.pbCarga.Size = new System.Drawing.Size(24, 40);
            this.pbCarga.TabIndex = 84;
            this.pbCarga.Visible = false;
            // 
            // panelValores
            // 
            this.panelValores.Controls.Add(this.lblVueltas);
            this.panelValores.Controls.Add(this.pbCargado);
            this.panelValores.Location = new System.Drawing.Point(589, 538);
            this.panelValores.Name = "panelValores";
            this.panelValores.Size = new System.Drawing.Size(446, 143);
            this.panelValores.TabIndex = 85;
            this.panelValores.Visible = false;
            // 
            // btnSolicitar
            // 
            this.btnSolicitar.Enabled = false;
            this.btnSolicitar.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSolicitar.Image = ((System.Drawing.Image)(resources.GetObject("btnSolicitar.Image")));
            this.btnSolicitar.Location = new System.Drawing.Point(590, 229);
            this.btnSolicitar.Name = "btnSolicitar";
            this.btnSolicitar.Size = new System.Drawing.Size(308, 52);
            this.btnSolicitar.TabIndex = 88;
            this.btnSolicitar.Text = "SOLICITAR";
            this.btnSolicitar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSolicitar.UseVisualStyleBackColor = true;
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.White;
            this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
            this.picLogo.Location = new System.Drawing.Point(931, 233);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(148, 32);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 43;
            this.picLogo.TabStop = false;
            this.picLogo.Visible = false;
            // 
            // btnTolva
            // 
            this.btnTolva.BackColor = System.Drawing.SystemColors.Control;
            this.btnTolva.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTolva.Location = new System.Drawing.Point(21, 32);
            this.btnTolva.Name = "btnTolva";
            this.btnTolva.Size = new System.Drawing.Size(513, 83);
            this.btnTolva.TabIndex = 86;
            this.btnTolva.Text = "1";
            this.btnTolva.UseVisualStyleBackColor = false;
            this.btnTolva.Click += new System.EventHandler(this.btnTolva_Click);
            // 
            // lblTolva
            // 
            this.lblTolva.AutoSize = true;
            this.lblTolva.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTolva.Location = new System.Drawing.Point(213, 6);
            this.lblTolva.Name = "lblTolva";
            this.lblTolva.Size = new System.Drawing.Size(61, 25);
            this.lblTolva.TabIndex = 87;
            this.lblTolva.Text = "Tolva";
            // 
            // lblZero
            // 
            this.lblZero.AutoSize = true;
            this.lblZero.BackColor = System.Drawing.Color.Black;
            this.lblZero.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZero.ForeColor = System.Drawing.Color.Lime;
            this.lblZero.Location = new System.Drawing.Point(31, 639);
            this.lblZero.Name = "lblZero";
            this.lblZero.Size = new System.Drawing.Size(32, 31);
            this.lblZero.TabIndex = 89;
            this.lblZero.Text = "Z";
            this.lblZero.Visible = false;
            // 
            // pbVariacion
            // 
            this.pbVariacion.Location = new System.Drawing.Point(874, 183);
            this.pbVariacion.Name = "pbVariacion";
            this.pbVariacion.Size = new System.Drawing.Size(24, 40);
            this.pbVariacion.TabIndex = 90;
            this.pbVariacion.Visible = false;
            // 
            // txtBascula
            // 
            this.txtBascula.BackColor = System.Drawing.Color.Black;
            this.txtBascula.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBascula.ForeColor = System.Drawing.Color.Lime;
            this.txtBascula.Location = new System.Drawing.Point(302, 574);
            this.txtBascula.Name = "txtBascula";
            this.txtBascula.Size = new System.Drawing.Size(266, 23);
            this.txtBascula.TabIndex = 91;
            this.txtBascula.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBascula.Visible = false;
            // 
            // txtStop
            // 
            this.txtStop.BackColor = System.Drawing.Color.Black;
            this.txtStop.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStop.ForeColor = System.Drawing.Color.Lime;
            this.txtStop.Location = new System.Drawing.Point(540, 632);
            this.txtStop.Name = "txtStop";
            this.txtStop.Size = new System.Drawing.Size(59, 16);
            this.txtStop.TabIndex = 92;
            this.txtStop.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtStop.Visible = false;
            // 
            // txtStopV
            // 
            this.txtStopV.BackColor = System.Drawing.Color.Black;
            this.txtStopV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStopV.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStopV.ForeColor = System.Drawing.Color.Lime;
            this.txtStopV.Location = new System.Drawing.Point(540, 654);
            this.txtStopV.Name = "txtStopV";
            this.txtStopV.Size = new System.Drawing.Size(59, 16);
            this.txtStopV.TabIndex = 93;
            this.txtStopV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtStopV.Visible = false;
            // 
            // txtPlanta
            // 
            this.txtPlanta.BackColor = System.Drawing.Color.White;
            this.txtPlanta.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlanta.ForeColor = System.Drawing.Color.Black;
            this.txtPlanta.Location = new System.Drawing.Point(1059, 102);
            this.txtPlanta.Name = "txtPlanta";
            this.txtPlanta.ReadOnly = true;
            this.txtPlanta.Size = new System.Drawing.Size(24, 38);
            this.txtPlanta.TabIndex = 94;
            this.txtPlanta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPlanta.Visible = false;
            // 
            // timerBeep
            // 
            this.timerBeep.Enabled = true;
            this.timerBeep.Interval = 1000;
            this.timerBeep.Tick += new System.EventHandler(this.timerBeep_Tick);
            // 
            // Racion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 693);
            this.ControlBox = false;
            this.Controls.Add(this.txtPlanta);
            this.Controls.Add(this.txtStopV);
            this.Controls.Add(this.txtStop);
            this.Controls.Add(this.txtBascula);
            this.Controls.Add(this.pbVariacion);
            this.Controls.Add(this.lblZero);
            this.Controls.Add(this.btnSolicitar);
            this.Controls.Add(this.lblTolva);
            this.Controls.Add(this.btnTolva);
            this.Controls.Add(this.panelValores);
            this.Controls.Add(this.pbCarga);
            this.Controls.Add(this.txtFaltaV);
            this.Controls.Add(this.txtFalta);
            this.Controls.Add(this.txtTamanoAct);
            this.Controls.Add(this.txtServido);
            this.Controls.Add(this.panelAnimacion);
            this.Controls.Add(this.pbFisico);
            this.Controls.Add(this.txtFisicoIni);
            this.Controls.Add(this.txtFisico);
            this.Controls.Add(this.lblFisico);
            this.Controls.Add(this.txtFisicoV);
            this.Controls.Add(this.txtTamanoV);
            this.Controls.Add(this.txtCargadoV);
            this.Controls.Add(this.lblEstable);
            this.Controls.Add(this.txtBrutoV);
            this.Controls.Add(this.txtCargadoIni);
            this.Controls.Add(this.txtCapacidad);
            this.Controls.Add(this.txtServida);
            this.Controls.Add(this.btnRacion);
            this.Controls.Add(this.lblRacion);
            this.Controls.Add(this.txtBruto);
            this.Controls.Add(this.lblBruto);
            this.Controls.Add(this.btnTara);
            this.Controls.Add(this.txtCargado);
            this.Controls.Add(this.lblCargado);
            this.Controls.Add(this.txtTamano);
            this.Controls.Add(this.lblTamaño);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.btnHecho);
            this.Controls.Add(this.btnCancelar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Racion";
            this.Text = "Racion";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.Raciones_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Raciones_FormClosed);
            this.Load += new System.EventHandler(this.Raciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picFisico)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFisicoF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelAnimacion.ResumeLayout(false);
            this.panelAnimacion.PerformLayout();
            this.panelValores.ResumeLayout(false);
            this.panelValores.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnHecho;
        private System.Windows.Forms.TextBox txtTamano;
        private System.Windows.Forms.Label lblTamaño;
        private System.Windows.Forms.TextBox txtCargado;
        private System.Windows.Forms.Label lblCargado;
        private System.Windows.Forms.Button btnTara;
        private System.Windows.Forms.TextBox txtBruto;
        private System.Windows.Forms.Label lblBruto;
        private System.Windows.Forms.Label lblRacion;
        private System.Windows.Forms.Timer timerR;
        private System.Windows.Forms.Label lblEstable;
        private System.Windows.Forms.Button btnRacion;
        private System.Windows.Forms.TextBox txtServida;
        private System.Windows.Forms.TextBox txtCapacidad;
        private System.Windows.Forms.TextBox txtCargadoIni;
        private System.Windows.Forms.TextBox txtBrutoV;
        private System.Windows.Forms.TextBox txtCargadoV;
        private System.Windows.Forms.TextBox txtTamanoV;
        private System.Windows.Forms.TextBox txtFisicoV;
        private System.Windows.Forms.Label lblFisico;
        private System.Windows.Forms.TextBox txtFisico;
        private System.Windows.Forms.TextBox txtFisicoIni;
        private System.Windows.Forms.ProgressBar pbFisico;
        private System.Windows.Forms.ProgressBar pbCargado;
        private System.Windows.Forms.PictureBox picFisico;
        private System.Windows.Forms.PictureBox picFisicoF;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelAnimacion;
        private System.Windows.Forms.Label lblPorcentaje;
        private System.Windows.Forms.Label lblVueltas;
        private System.Windows.Forms.TextBox txtServido;
        private System.Windows.Forms.TextBox txtTamanoAct;
        private System.Windows.Forms.TextBox txtFalta;
        private System.Windows.Forms.TextBox txtFaltaV;
        private System.Windows.Forms.ProgressBar pbCarga;
        private System.Windows.Forms.Panel panelValores;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Button btnTolva;
        private System.Windows.Forms.Label lblTolva;
        private System.Windows.Forms.Button btnSolicitar;
        private System.Windows.Forms.Label lblZero;
        private System.Windows.Forms.ProgressBar pbVariacion;
        private System.Windows.Forms.TextBox txtBascula;
        private System.Windows.Forms.TextBox txtStop;
        private System.Windows.Forms.TextBox txtStopV;
        private System.Windows.Forms.TextBox txtPlanta;
        private System.Windows.Forms.Timer timerBeep;
    }
}