namespace Senga
{
    partial class Principal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Principal));
            this.timerWs = new System.Windows.Forms.Timer(this.components);
            this.panelGps = new System.Windows.Forms.Panel();
            this.txtLectura = new System.Windows.Forms.TextBox();
            this.picVerde = new System.Windows.Forms.PictureBox();
            this.picRojo = new System.Windows.Forms.PictureBox();
            this.chkWarning = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.lblHora = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblLatitud = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.lblLongitud = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnIniciaGps = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
            this.lblConectado = new System.Windows.Forms.ToolStripLabel();
            this.lblConectadoF = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.lblWsActivo = new System.Windows.Forms.ToolStripLabel();
            this.lblWsActivoF = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.lblGpsActivoF = new System.Windows.Forms.ToolStripProgressBar();
            this.txtError = new System.Windows.Forms.ToolStripTextBox();
            this.lblContWs = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.lblServer = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.lblMandante = new System.Windows.Forms.ToolStripLabel();
            this.lblSenalRed = new System.Windows.Forms.ToolStripLabel();
            this.timerReloj = new System.Windows.Forms.Timer(this.components);
            this.timerRecorrido = new System.Windows.Forms.Timer(this.components);
            this.timerError = new System.Windows.Forms.Timer(this.components);
            this.listBoxRecorridos = new System.Windows.Forms.ListBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnReparto = new System.Windows.Forms.Button();
            this.btnRecorrido = new System.Windows.Forms.Button();
            this.btnConfigurar = new System.Windows.Forms.Button();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnServidas = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panelGps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picVerde)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRojo)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timerWs
            // 
            this.timerWs.Interval = 10000;
            this.timerWs.Tick += new System.EventHandler(this.timerWs_Tick);
            // 
            // panelGps
            // 
            this.panelGps.BackColor = System.Drawing.Color.White;
            this.panelGps.Controls.Add(this.txtLectura);
            this.panelGps.Controls.Add(this.picVerde);
            this.panelGps.Controls.Add(this.picRojo);
            this.panelGps.Controls.Add(this.chkWarning);
            this.panelGps.Location = new System.Drawing.Point(33, 517);
            this.panelGps.Name = "panelGps";
            this.panelGps.Size = new System.Drawing.Size(307, 57);
            this.panelGps.TabIndex = 48;
            this.panelGps.Visible = false;
            // 
            // txtLectura
            // 
            this.txtLectura.Location = new System.Drawing.Point(3, 26);
            this.txtLectura.Name = "txtLectura";
            this.txtLectura.Size = new System.Drawing.Size(299, 20);
            this.txtLectura.TabIndex = 68;
            // 
            // picVerde
            // 
            this.picVerde.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picVerde.Image = ((System.Drawing.Image)(resources.GetObject("picVerde.Image")));
            this.picVerde.Location = new System.Drawing.Point(308, 3);
            this.picVerde.Name = "picVerde";
            this.picVerde.Size = new System.Drawing.Size(38, 36);
            this.picVerde.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picVerde.TabIndex = 106;
            this.picVerde.TabStop = false;
            this.picVerde.Visible = false;
            // 
            // picRojo
            // 
            this.picRojo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picRojo.Image = ((System.Drawing.Image)(resources.GetObject("picRojo.Image")));
            this.picRojo.Location = new System.Drawing.Point(308, 3);
            this.picRojo.Name = "picRojo";
            this.picRojo.Size = new System.Drawing.Size(38, 36);
            this.picRojo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRojo.TabIndex = 107;
            this.picRojo.TabStop = false;
            this.picRojo.Visible = false;
            // 
            // chkWarning
            // 
            this.chkWarning.AutoSize = true;
            this.chkWarning.Location = new System.Drawing.Point(3, 3);
            this.chkWarning.Name = "chkWarning";
            this.chkWarning.Size = new System.Drawing.Size(66, 17);
            this.chkWarning.TabIndex = 57;
            this.chkWarning.Text = "Warning";
            this.chkWarning.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblHora,
            this.toolStripSeparator13,
            this.toolStripLabel1,
            this.toolStripSeparator1,
            this.lblLatitud,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.toolStripSeparator4,
            this.lblLongitud,
            this.toolStripSeparator3,
            this.btnIniciaGps});
            this.toolStrip1.Location = new System.Drawing.Point(0, 661);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1244, 40);
            this.toolStrip1.TabIndex = 49;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // lblHora
            // 
            this.lblHora.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(75, 37);
            this.lblHora.Text = "Hora";
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 40);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(100, 37);
            this.toolStripLabel1.Text = "Latitud";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // lblLatitud
            // 
            this.lblLatitud.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.lblLatitud.Name = "lblLatitud";
            this.lblLatitud.Size = new System.Drawing.Size(32, 37);
            this.lblLatitud.Text = "0";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(124, 37);
            this.toolStripLabel2.Text = "Longitud";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 40);
            // 
            // lblLongitud
            // 
            this.lblLongitud.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.lblLongitud.Name = "lblLongitud";
            this.lblLongitud.Size = new System.Drawing.Size(32, 37);
            this.lblLongitud.Text = "0";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 40);
            // 
            // btnIniciaGps
            // 
            this.btnIniciaGps.BackColor = System.Drawing.Color.LightGray;
            this.btnIniciaGps.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnIniciaGps.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.btnIniciaGps.Image = ((System.Drawing.Image)(resources.GetObject("btnIniciaGps.Image")));
            this.btnIniciaGps.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIniciaGps.Name = "btnIniciaGps";
            this.btnIniciaGps.Size = new System.Drawing.Size(116, 41);
            this.btnIniciaGps.Text = "ON GPS";
            this.btnIniciaGps.ToolTipText = "ON GPS";
            this.btnIniciaGps.Visible = false;
            this.btnIniciaGps.Click += new System.EventHandler(this.btnIniciaGps_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel7,
            this.lblConectado,
            this.lblConectadoF,
            this.toolStripLabel5,
            this.lblWsActivo,
            this.lblWsActivoF,
            this.toolStripLabel4,
            this.lblGpsActivoF,
            this.txtError,
            this.lblContWs,
            this.toolStripLabel3,
            this.lblServer,
            this.toolStripLabel6,
            this.lblMandante,
            this.lblSenalRed});
            this.toolStrip2.Location = new System.Drawing.Point(0, 627);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1244, 34);
            this.toolStrip2.TabIndex = 50;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel7
            // 
            this.toolStripLabel7.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.toolStripLabel7.Name = "toolStripLabel7";
            this.toolStripLabel7.Size = new System.Drawing.Size(45, 31);
            this.toolStripLabel7.Text = "Red";
            // 
            // lblConectado
            // 
            this.lblConectado.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.lblConectado.Name = "lblConectado";
            this.lblConectado.Size = new System.Drawing.Size(47, 31);
            this.lblConectado.Text = "OFF";
            this.lblConectado.Visible = false;
            // 
            // lblConectadoF
            // 
            this.lblConectadoF.MarqueeAnimationSpeed = 1;
            this.lblConectadoF.Maximum = 1;
            this.lblConectadoF.Name = "lblConectadoF";
            this.lblConectadoF.Size = new System.Drawing.Size(10, 31);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(39, 31);
            this.toolStripLabel5.Text = "Ws";
            // 
            // lblWsActivo
            // 
            this.lblWsActivo.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.lblWsActivo.Name = "lblWsActivo";
            this.lblWsActivo.Size = new System.Drawing.Size(47, 31);
            this.lblWsActivo.Text = "OFF";
            this.lblWsActivo.Visible = false;
            // 
            // lblWsActivoF
            // 
            this.lblWsActivoF.MarqueeAnimationSpeed = 1;
            this.lblWsActivoF.Maximum = 1;
            this.lblWsActivoF.Name = "lblWsActivoF";
            this.lblWsActivoF.Size = new System.Drawing.Size(10, 31);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(46, 31);
            this.toolStripLabel4.Text = "Gps";
            // 
            // lblGpsActivoF
            // 
            this.lblGpsActivoF.MarqueeAnimationSpeed = 1;
            this.lblGpsActivoF.Maximum = 1;
            this.lblGpsActivoF.Name = "lblGpsActivoF";
            this.lblGpsActivoF.Size = new System.Drawing.Size(10, 31);
            // 
            // txtError
            // 
            this.txtError.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.txtError.Name = "txtError";
            this.txtError.ReadOnly = true;
            this.txtError.Size = new System.Drawing.Size(400, 34);
            // 
            // lblContWs
            // 
            this.lblContWs.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.lblContWs.Name = "lblContWs";
            this.lblContWs.Size = new System.Drawing.Size(23, 31);
            this.lblContWs.Text = "0";
            this.lblContWs.Visible = false;
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(67, 31);
            this.toolStripLabel3.Text = "Server";
            this.toolStripLabel3.Visible = false;
            // 
            // lblServer
            // 
            this.lblServer.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(23, 31);
            this.lblServer.Text = "0";
            this.lblServer.Visible = false;
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(101, 31);
            this.toolStripLabel6.Text = "Mandante";
            this.toolStripLabel6.Visible = false;
            // 
            // lblMandante
            // 
            this.lblMandante.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.lblMandante.Name = "lblMandante";
            this.lblMandante.Size = new System.Drawing.Size(23, 31);
            this.lblMandante.Text = "0";
            this.lblMandante.Visible = false;
            // 
            // lblSenalRed
            // 
            this.lblSenalRed.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.lblSenalRed.Name = "lblSenalRed";
            this.lblSenalRed.Size = new System.Drawing.Size(28, 31);
            this.lblSenalRed.Text = " 0";
            this.lblSenalRed.Visible = false;
            // 
            // timerReloj
            // 
            this.timerReloj.Enabled = true;
            this.timerReloj.Interval = 1000;
            this.timerReloj.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timerRecorrido
            // 
            this.timerRecorrido.Interval = 60000;
            this.timerRecorrido.Tick += new System.EventHandler(this.timerRecorrido_Tick);
            // 
            // timerError
            // 
            this.timerError.Enabled = true;
            this.timerError.Interval = 30000;
            this.timerError.Tick += new System.EventHandler(this.timerError_Tick);
            // 
            // listBoxRecorridos
            // 
            this.listBoxRecorridos.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxRecorridos.FormattingEnabled = true;
            this.listBoxRecorridos.ItemHeight = 31;
            this.listBoxRecorridos.Location = new System.Drawing.Point(665, 177);
            this.listBoxRecorridos.Name = "listBoxRecorridos";
            this.listBoxRecorridos.Size = new System.Drawing.Size(149, 221);
            this.listBoxRecorridos.TabIndex = 52;
            this.listBoxRecorridos.Visible = false;
            this.listBoxRecorridos.SelectedIndexChanged += new System.EventHandler(this.listBoxRecorridos_SelectedIndexChanged);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.Black;
            this.lblVersion.Location = new System.Drawing.Point(59, 144);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(35, 13);
            this.lblVersion.TabIndex = 53;
            this.lblVersion.Text = "label1";
            // 
            // btnReparto
            // 
            this.btnReparto.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReparto.Image = ((System.Drawing.Image)(resources.GetObject("btnReparto.Image")));
            this.btnReparto.Location = new System.Drawing.Point(415, 12);
            this.btnReparto.Name = "btnReparto";
            this.btnReparto.Size = new System.Drawing.Size(149, 149);
            this.btnReparto.TabIndex = 5;
            this.btnReparto.Text = "Reparto";
            this.btnReparto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReparto.UseVisualStyleBackColor = true;
            this.btnReparto.Click += new System.EventHandler(this.btnReparto_Click);
            // 
            // btnRecorrido
            // 
            this.btnRecorrido.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecorrido.Image = ((System.Drawing.Image)(resources.GetObject("btnRecorrido.Image")));
            this.btnRecorrido.Location = new System.Drawing.Point(545, 10);
            this.btnRecorrido.Name = "btnRecorrido";
            this.btnRecorrido.Size = new System.Drawing.Size(149, 149);
            this.btnRecorrido.TabIndex = 4;
            this.btnRecorrido.Text = "Recorrido";
            this.btnRecorrido.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRecorrido.UseVisualStyleBackColor = true;
            this.btnRecorrido.Click += new System.EventHandler(this.btnRecorrido_Click);
            // 
            // btnConfigurar
            // 
            this.btnConfigurar.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfigurar.Image = ((System.Drawing.Image)(resources.GetObject("btnConfigurar.Image")));
            this.btnConfigurar.Location = new System.Drawing.Point(690, 12);
            this.btnConfigurar.Name = "btnConfigurar";
            this.btnConfigurar.Size = new System.Drawing.Size(149, 149);
            this.btnConfigurar.TabIndex = 1;
            this.btnConfigurar.Text = "Configurar";
            this.btnConfigurar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConfigurar.UseVisualStyleBackColor = true;
            this.btnConfigurar.Click += new System.EventHandler(this.btnConfigurar_Click);
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.White;
            this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
            this.picLogo.Location = new System.Drawing.Point(0, 0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(241, 161);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 45;
            this.picLogo.TabStop = false;
            // 
            // btnSalir
            // 
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.Location = new System.Drawing.Point(1000, 12);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(149, 149);
            this.btnSalir.TabIndex = 3;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnServidas
            // 
            this.btnServidas.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnServidas.Image = ((System.Drawing.Image)(resources.GetObject("btnServidas.Image")));
            this.btnServidas.Location = new System.Drawing.Point(845, 12);
            this.btnServidas.Name = "btnServidas";
            this.btnServidas.Size = new System.Drawing.Size(149, 149);
            this.btnServidas.TabIndex = 2;
            this.btnServidas.Text = "Jornada";
            this.btnServidas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnServidas.UseVisualStyleBackColor = true;
            this.btnServidas.Click += new System.EventHandler(this.btnServidas_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1244, 701);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 46;
            this.pictureBox1.TabStop = false;
            // 
            // btnActualizar
            // 
            this.btnActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizar.Image")));
            this.btnActualizar.Location = new System.Drawing.Point(316, 12);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(149, 149);
            this.btnActualizar.TabIndex = 6;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(17, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 54;
            this.label1.Text = "SENGA";
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1244, 701);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.listBoxRecorridos);
            this.Controls.Add(this.btnReparto);
            this.Controls.Add(this.btnRecorrido);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panelGps);
            this.Controls.Add(this.btnConfigurar);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnServidas);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.Principal_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Principal_FormClosed);
            this.Load += new System.EventHandler(this.Principal_Load);
            this.panelGps.ResumeLayout(false);
            this.panelGps.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picVerde)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRojo)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnServidas;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnConfigurar;
        private System.Windows.Forms.Panel panelGps;
        private System.Windows.Forms.PictureBox picVerde;
        private System.Windows.Forms.PictureBox picRojo;
        private System.Windows.Forms.CheckBox chkWarning;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel lblLatitud;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel lblLongitud;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel lblServer;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripLabel lblMandante;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripTextBox txtError;
        private System.Windows.Forms.ToolStripLabel lblHora;
        private System.Windows.Forms.Timer timerReloj;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.TextBox txtLectura;
        private System.Windows.Forms.Button btnRecorrido;
        private System.Windows.Forms.ToolStripLabel lblWsActivo;
        private System.Windows.Forms.Button btnReparto;
        private System.Windows.Forms.ToolStripLabel toolStripLabel7;
        private System.Windows.Forms.ToolStripLabel lblConectado;
        private System.Windows.Forms.Timer timerError;
        private System.Windows.Forms.ListBox listBoxRecorridos;
        private System.Windows.Forms.ToolStripButton btnIniciaGps;
        private System.Windows.Forms.ToolStripLabel lblContWs;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripLabel lblSenalRed;
        public System.Windows.Forms.Timer timerWs;
        public System.Windows.Forms.Timer timerRecorrido;
        public System.Windows.Forms.ToolStripProgressBar lblWsActivoF;
        public System.Windows.Forms.ToolStripProgressBar lblConectadoF;
        public System.Windows.Forms.ToolStripProgressBar lblGpsActivoF;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Label label1;
    }
}

