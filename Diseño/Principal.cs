using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Threading;
using System.Net.NetworkInformation;
using System.Management;
using System.ServiceProcess;

namespace Senga
{
    public partial class Principal : Form//, ITimer
    {

        private delegate void DelegaAcceso(string accion);

        Comm commGps = new Comm();
        //public SerialPort serialPortPeso = new SerialPort();
        public SerialPort serialPortGps = new SerialPort();

        private string nameruta = "\\senga_archivos";
        private string nameapp = "\\app";
        private string nameappnom = "\\senga";
        private string vrutared;

        string servida = "";
        string racion = "";
        //string ip = "";
        string recibidos = "";

        public double gLatitudgps = 0;
        public double gLongitudgps = 0;
        string gfechagps = DateTime.Now.ToString("dd-MM-yyyy");
        string ghoragps = "00:00:00";

        DateTime ultError = new DateTime();
        DataTable dtCamiontmp;
        DataTable dtRepartotmp;

        ConnectedWs connectar;

        String usuario = "";
        String contrasena = "";
        String numcamion = "";
        Boolean wsactivo = false;
        String appgps = "GpsGateXP";
        String msgError = "";
        String versionAct = "";

        bool RecibirPeso = false;
        public bool pauseGps = false;


        //public void Start()
        //{
        //    timerWs.Start();
        //}

        //public void Stop()
        //{
        //    timerWs.Stop();
        //}

        public static Principal instance = null;
        
        public Principal()
        {
            //existeApp();
           
            instance = this;

            //Thread t = new Thread(new ThreadStart(StartForm));
            //t.Start();
            //Thread.Sleep(3000);
            
            InitializeComponent();
            
            //t.Abort();

         

        }

        public void StartForm()
        {
            Application.Run(new Inicio());
        }
      
        private void Principal_Load(object sender, EventArgs e)
        {

            //Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            //try
            //{

            //    //-------------------------------------------
            //    DataRow row1 = null;
            //    string vusuarios = row1["Usu"].ToString();
            //    //-------------------------------------------
            //}
            //catch { }

            //this.toolStrip2.Padding = new Padding((int)(this.Size.Width - 300), 0, 0, 0);


            this.Text = "v" + Application.ProductVersion + " - " + File.GetLastWriteTime(Application.ExecutablePath) + " - " + bitModo();

            versionAct = Application.ProductVersion;

            lblVersion.Text = "v" + versionAct + " - " + bitModo();

        
            //cambiosSQL(versionAct);


            string texto ="";

            posicionBotones();
            btnServidas.Focus();


            if (validaBD(ref texto))
            {
                Comun.mensajeVer(texto, Constants.MSG_BOTON_OK, Constants.MSG_IMG_WARNING);
                Close();
            }
            else
            {

                //iniciaPesoOpc2();

                DataRow row = obtieneConfig();

                if (row != null)
                {
                    usuario = row["Usuario"].ToString();
                    contrasena = row["Contrasena"].ToString();
                    numcamion = row["numcamion"].ToString();

                    wsactivo = row["ws"].ToString().Equals("S"); 
                    
                }

                carpeta_existe_local();

                tablaCamionTemp();
                tablaRepartoTemp();

                inicializa();
                inicializa2();

                //wsactivo = wsactivoConfigWs();
                if (wsactivo)
                {
                    lblWsActivo.Text = "ON";
                    //lblWsActivoF.Value = 1;
                    btnReparto.Width = 0;
                }
                else
                {
                    lblWsActivo.Text = "OFF";
                    //lblWsActivoF.Value = 0;
                }

                redEstado();

                //numcamion = numcamionConfigWs();
                //usuarioiWs();

                //refrescaGPS();
                //limpiaServidas();
                luzStatus(false);

                //operacionGPS();
                iniciaGps();

                ShowPowerStatus();

                statusTimer(true);
                    

                //ejecutaAppGPS(); 


                //ServicioGPS servicioGPS = new ServicioGPS();
                //servicioGPS.InitializeLifetimeService();

            }

        }

        //private void velodidadRed()
        //{

        //    NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
        //    foreach (NetworkInterface adapter in adapters)
        //    {
        //        if (adapter.Name.Equals("Wi-Fi"))
        //        {

        //            IPInterfaceProperties properties = adapter.GetIPProperties();
        //            IPv4InterfaceStatistics stats = adapter.GetIPv4Statistics();
        //            Console.WriteLine(adapter.Description);
        //            Console.WriteLine("     Speed .................................: {0}",
        //                adapter.Speed);
        //            Console.WriteLine("     Output queue length....................: {0}",
        //                stats.OutputQueueLength);

        //            lblSenalRed.Text = adapter.Speed.ToString();

        //        }

                 

        //    }
        //}

        private void redEstado()
        {

            if (redConectado())
            {
                lblConectado.Text = "ON";
                lblConectadoF.Value = 1;
                enviarWsdl();
            }
            else
            {
                lblConectado.Text = "OFF";
                lblConectadoF.Value = 0;
            }

        }

        private void existeApp()
        {
            string processName = Process.GetCurrentProcess().ProcessName;
            Process[] instances = Process.GetProcessesByName(processName);

            if (instances.Length > 1)

            {
                //MessageBox.Show("La aplicación ya se está ejecutando.");

                this.Close();
                return;
            }
        }

        private void posicionBotones()
        {
            //btnRaciones.Left = this.Width / 2 - btnRaciones.Width / 2;

            //btnServidas.Left = this.Width / 2 - btnServidas.Width / 2;
            //btnSalir.Left = this.Width / 2 - btnSalir.Width / 2;
            //picLogo.Left = this.Width / 2 - picLogo.Width / 2;

            //btnConfigurar.Top = this.Height - btnConfigurar.Height - 100;
            //btnConfigurar.Left = this.Width - btnConfigurar.Width - 50;

            //btnGPS.Top = this.Height - btnGPS.Height - 100;
            //btnGPS.Left = 50;

            btnActualizar.Top = 0;
            btnSalir.Top = 0;
            btnServidas.Top = 0;
            btnConfigurar.Top = 0;
            btnRecorrido.Top = 0;
            btnReparto.Top = 0;

            btnReparto.Width = 0;

            btnSalir.Left = this.Width - btnSalir.Width;
            btnServidas.Left = btnSalir.Left - btnServidas.Width;
            btnConfigurar.Left = btnServidas.Left - btnConfigurar.Width;
            btnRecorrido.Left = btnConfigurar.Left - btnRecorrido.Width;
            btnReparto.Left = btnRecorrido.Left - btnReparto.Width;
            btnActualizar.Left = btnReparto.Left - btnActualizar.Width;


            listBoxRecorridos.Top = btnRecorrido.Top + btnRecorrido.Height;
            listBoxRecorridos.Left = btnRecorrido.Left;


        }

        private String bitModo()
        {

            String valor = "";

            if (IntPtr.Size == 8)
            {
                valor = " 64 bits";
            }
            else if (IntPtr.Size == 4)
            {
                valor = " 32 bits";
            }

            return valor;
        }

        private void tablaCamionTemp()
        {

            dtCamiontmp = new DataTable();

            dtCamiontmp.Columns.Add("numcamion");
            dtCamiontmp.Columns.Add("hora");
            dtCamiontmp.Columns.Add("fecha");
            dtCamiontmp.Columns.Add("latitud");
            dtCamiontmp.Columns.Add("longitud");
            dtCamiontmp.Columns.Add("servida");
            dtCamiontmp.Columns.Add("racion");
            dtCamiontmp.Columns.Add("idd");

        }

        private void tablaRepartoTemp()
        {

            dtRepartotmp = new DataTable();

            dtRepartotmp.Columns.Add("corral");
            dtRepartotmp.Columns.Add("fecha");
            dtRepartotmp.Columns.Add("hora");
            dtRepartotmp.Columns.Add("numcamion");
            dtRepartotmp.Columns.Add("racion");
            dtRepartotmp.Columns.Add("servida");
            dtRepartotmp.Columns.Add("plan");
            dtRepartotmp.Columns.Add("servido");
            dtRepartotmp.Columns.Add("plandia");
            dtRepartotmp.Columns.Add("servidodia");
            dtRepartotmp.Columns.Add("corralgps");
            dtRepartotmp.Columns.Add("latitudi");
            dtRepartotmp.Columns.Add("longitudi");
            dtRepartotmp.Columns.Add("idd");

        }

        private void ejecutaAppGPS()
        {

            bool encontro = false;
            string programa = "";

            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            string ruta = key.GetValue("GPSsenga", "").ToString();

            if (!ruta.Equals(""))
            {

                if (File.Exists(ruta))
                {

                    Process[] p1 = Process.GetProcesses();

                    programa = Path.GetFileName(ruta).Replace(".exe", "");

                    foreach (Process pro in p1)
                    {
                        if (pro.ProcessName.ToUpper().Contains(programa))
                        {
                            encontro = true;
                        }
                    }

                    if (!encontro)
                    {
                        ProcessStartInfo info = new ProcessStartInfo();

                        info.UseShellExecute = true;
                        info.FileName = Path.GetFileName(ruta);
                        ruta = ruta.Replace("\\" + info.FileName, "");
                        info.WorkingDirectory = ruta;

                        Process.Start(info);

                    }

                }

            }

        }

        private void Principal_FormClosed(object sender, EventArgs e)
        {

            cerrarpuertoGps();



        }

        //private void cerrarpuertoGps()
        //{

        //    if (commGps.p_spgps is null)
        //    {

        //    }
        //    else
        //    {

        //        //if (gpsConfig().Equals("S"))
        //        //{
        //        //cerrarPuerto();
        //        commGps.cerrarPuertogps();
        //        commGps.liberarPuertogps();
        //        //}

        //    }

        //}

        private void btnServidas_Click(object sender, EventArgs e)
        {
            //Servidas frm = new Servidas("");
            ////frm.MdiParent = this;
            //frm.Show();

            
   
            Jornadas frm = new Jornadas();

            //frm.MdiParent = this;
                       
            frm.Show();

            //this.WindowState = FormWindowState.Minimized;

        }

        private Boolean procesoEjecucion(String programa)
        {
            Boolean resultado = false;

            //Process[] allRunningPrograms = Process.GetProcesses();
            Process[] instancia = Process.GetProcessesByName(programa);

            if (instancia.Length >= 1)
            {
                //Exe Corriendo
                resultado = true;
            }

            return resultado;

        }

        private void iniciaGps()
        {

            if (!procesoEjecucion(appgps))
            {
                txtError.Text = "La aplicacion GpsGate esta cerrada";
                btnIniciaGps.Visible = true;
            }
            else
            {

                btnIniciaGps.Visible = false;
                txtError.Text = "";
                try
                {

                    serialPortGps.PortName = puertoConfigGps();
                    serialPortGps.BaudRate = baudConfigGps();
                    serialPortGps.Parity = (Parity)Enum.Parse(typeof(Parity), parityConfigGps(), true);
                    serialPortGps.DataBits = dataConfigGps();
                    serialPortGps.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopConfigGps(), true);
                    //serialPortGps.DataReceived += new SerialDataReceivedEventHandler(SerialPortGps_DataReceived);
                    //serialPortGps.ErrorReceived += new SerialErrorReceivedEventHandler(SerialPort_ErrorReceivedHandler);
                    serialPortGps.ReadTimeout = 500;
                    serialPortGps.WriteTimeout = 500;
                    serialPortGps.Open();


                    timerGps.Start();


                }
                catch (Exception err)
                {
                    //MessageBox.Show(err.Message);
                    //ErrorLogging(err);
                    txtError.Text = "GPS "+err.ToString();
                }

            }


        }

        private void operacionGPS()
        {

            //if (commGps != null)
            //{
            //    commGps.cerrarPuertogps();
            //    commGps.liberarPuertogps();
            //}
            //if (serialPort2 != null)
            //{ 
            //    serialPort2.Close();
            //    serialPort2.Dispose();
            //}

            try
            {

                //commGps = new Comm();
                //serialPort2 = new SerialPort();

                //if (gpsConfig().Equals("S"))
                //{

                commGps.p_commgps = puertoConfigGps();
                commGps.p_baudrategps = baudConfigGps();
                commGps.p_databitsgps = dataConfigGps();
                commGps.p_paritygps = parityConfigGps();
                commGps.p_stopbitsgps = stopConfigGps();
                commGps.p_spgps = serialPortGps;

                if (!validapuertoExiste(commGps.p_commgps))
                {
                    //MessageBox.Show("Sin puerto Gps COM disponible", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //msgErrores("Sin puerto de GpsGate", Constants.MSG_ERROR);
                    txtError.Text = "Sin puerto de GpsGate";
                    //btnReConectar.Visible = true;
                    //statusTimer(false);
                    //luzStatus(false);
                }
                else
                {
                    if (!commGps.abrirPuertogps())
                    {
                        //Close();
                        //MessageBox.Show(commGps.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                        //msgErrores(commGps.p_error, Constants.MSG_ERROR);
                        txtError.Text = commGps.p_error;
                        //btnReConectar.Visible = true;

                        statusTimer(false);
                        //luzStatus(false);

                        //cerrarpuertoGps();                        

                    }
                    else
                    {

                        //msgErrores("", Constants.MSG_OK);


                        txtError.Text = "";

                        statusTimer(true);
                        //btnReConectar.Visible = false;
                        //luzStatus(true);

                    }



                }

            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //msgErrores(e.Message, Constants.MSG_ERROR);

                //txtError.Text = e.Message;

                //btnReConectar.Visible = true;
                //luzStatus(false);

                //cerrarpuertoGps();

            }

        }

        private void Principal_Activated(object sender, EventArgs e)
        {

            activar();

        }

        private void activar()
        {


        }

        //private void Principal_Resize(object sender, System.EventArgs e)
        //{
        //    Control control = (Control)sender;

        //    // Ensure the Form remains square (Height = Width).
        //    if (control.Size.Height != control.Size.Width)
        //    {
        //        control.Size = new Size(control.Size.Width, control.Size.Width);
        //    }
        //}

        private void btnSalir_Click(object sender, EventArgs e)
        {
        
            if (Comun.mensajeVer("¿ Desea salir de la aplicacion ?", Constants.MSG_BOTON_SINO, Constants.MSG_IMG_QUESTION) == Constants.MSG_BOTON_SI)
            { 
                  Close();    
            }
              
        }

        private Boolean validaBD(ref string texto) 
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            datos.p_sql = "SELECT * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                texto = datos.p_error;
                return true;
            }

            texto = "";
            return false;
        }

        private void inicializa()
        {
            Datos datos = new Datos();
            DataTable dt = new DataTable();

            int filas = 0;

            datos.p_sql = "SELECT * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Close();
            }
            else
            {
                filas = dt.Rows.Count;
            }

            if (filas == 0)
            {
                datos.p_sql = "INSERT INTO configuracion (numcamion,rutalocal,rutared) VALUES ('999','C:\\','C:\\')";

                if (!datos.consultaSQL(ref dt, true))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }

        }

        private void inicializa2()
        {
            Datos datos = new Datos();
            DataTable dt = new DataTable();

            int filas = 0;

            datos.p_sql = "SELECT * FROM operacion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                filas = dt.Rows.Count;
            }

            if (filas == 0)
            {
                datos.p_sql = "INSERT INTO operacion (servida) VALUES ('1')";

                if (!datos.consultaSQL(ref dt, true))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }

        }

        private void limpiaServidas()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            int dias = 100;

            datos.p_sql = "SELECT diaselim FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    dias = Convert.ToInt32(row["DiasElim"].ToString());
                }

            }

            //datos.p_sql = "DELETE FROM servidas WHERE fecha < DATE() - " + dias.ToString();


            //if (!datos.consultaSQL(ref dt, false))
            //{
            //    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //}
            //else
            //{
            //    //filas = dt.Rows.Count;
            //}


            //datos.p_sql = "DELETE FROM raciones WHERE fecha < DATE() - " + dias.ToString();


            //if (!datos.consultaSQL(ref dt, false))
            //{
            //    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //}
            //else
            //{
            //    //filas = dt.Rows.Count;
            //}

        }

        private void btnConfigurar_Click(object sender, EventArgs e)
        {
            Senga.Configurar frm = new Senga.Configurar();
            frm.Show();
        }

        private void timerWs_Tick(object sender, EventArgs e)
        {

            //panelGPS.Visible = true;


            //luzStatus(timer1.Enabled);

            //leerGps();

            //lblLatitud.Text = gLatitud.ToString();
            //lblLongitud.Text = gLongitud.ToString();

            guardaLecturaGps();


            if (redConectado())
            {
            //    lblConectado.Text = "ON";
                lblConectadoF.Value = 1;
                enviarWsdl();
            }
            else
            {
            //    lblConectado.Text = "OFF";
                lblConectadoF.Value = 0;
            }

            redEstado();
             
            //velodidadRed();

            //enviarWs();
            //picPunto.Visible = ! picPunto.Visible;

        }

        private void timerRecorrido_Tick(object sender, EventArgs e)
        {
            guardaLecturaGpsRecorrido();
        }

        private void enviarWsdl()
        {

            if (wsactivo)
            {

                consultaLocalizaCamion(dtCamiontmp);
                enviarWsdlLocalizaCamion(dtCamiontmp);

                consultaRepartoCorral(dtRepartotmp);
                enviarWsdlRepartoCorral(dtRepartotmp);

                //consultaRecorridoCamion(dtCamiontmp);
                //enviarWsdlRecorridoCamion(dtCamiontmp);     

            }

        }

        private Boolean redConectado()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }

        private void consultaLocalizaCamion(DataTable dtsalida)
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            datos.p_sql = "SELECT * FROM operacion WHERE latitud > 0";

            if (!datos.consultaSQL(ref dt, true))
            {
                //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //msgErrores(datos.p_error, Constants.MSG_ERROR);
                txtError.Text = datos.p_error;
            }
            else
            {

                dtsalida.Clear();

                foreach (DataRow row in dt.Rows)
                {

                    DataRow newRow = dtsalida.NewRow();

                    newRow["numcamion"] = numcamion;
                    newRow["hora"] = row["horagps"];
                    if (row["fechagps"] != DBNull.Value)
                    {
                        newRow["fecha"] = Convert.ToDateTime(row["fechagps"]).ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        newRow["fecha"] = DateTime.Now.ToString("yyyy-MM-dd");
                    }

                    newRow["latitud"] = row["latitud"];
                    newRow["longitud"] = row["longitud"];

                    newRow["servida"] = row["servida"];
                    newRow["racion"] = row["racion"];

                    dtsalida.Rows.Add(newRow);

                }

            }

        }

        private void consultaRecorridoCamion(DataTable dtsalida)
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            datos.p_sql = "SELECT TOP 5000 * FROM recorrido WHERE envio = 'N'";

            if (!datos.consultaSQL(ref dt, true))
            {
                //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //msgErrores(datos.p_error, Constants.MSG_ERROR);
                txtError.Text = datos.p_error;
            }
            else
            {

                dtsalida.Clear();

                foreach (DataRow row in dt.Rows)
                {

                    DataRow newRow = dtsalida.NewRow();

                    newRow["numcamion"] = numcamion;
                    newRow["hora"] = row["hora"];
                    newRow["fecha"] = Convert.ToDateTime(row["fecha"]).ToString("yyyy-MM-dd");
                    newRow["latitud"] = row["latitud"];
                    newRow["longitud"] = row["longitud"];
                    newRow["idd"] = row["idd"];

                    dtsalida.Rows.Add(newRow);

                }

            }

        }

        private void consultaRepartoCorral(DataTable dtsalida)
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();


            datos.p_sql = "SELECT corral, SUM(realidad) AS servidodia FROM servidas WHERE (day(fecha) = " + DateTime.Now.ToString("dd") + " and month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) GROUP BY corral";

            if (!datos.consultaSQL(ref dt2, true))
            {
                //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //msgErrores(datos.p_error, Constants.MSG_ERROR);
                txtError.Text = datos.p_error;
            }

            datos.p_sql = "SELECT * FROM servidas WHERE realidad > 0 AND (day(fecha) = " + DateTime.Now.ToString("dd") + " and month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ORDER BY corral, fecha, idd";

            if (!datos.consultaSQL(ref dt, true))
            {
                //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //msgErrores(datos.p_error, Constants.MSG_ERROR);
                txtError.Text = datos.p_error;
            }
            else
            {

                dtsalida.Clear();

                foreach (DataRow row in dt.Rows)
                {

                    DataRow newRow = dtsalida.NewRow();

                    newRow["corral"] = row["corral"];
                    newRow["fecha"] = Convert.ToDateTime(row["fecha"]).ToString("yyyy-MM-dd");
                    newRow["hora"] = row["hora"];
                    newRow["numcamion"] = numcamion;
                    newRow["racion"] = row["racion"];
                    newRow["servida"] = row["num"];
                    newRow["plan"] = row["planeado"];
                    newRow["servido"] = row["realidad"];
                    newRow["plandia"] = row["plandia"];
                    newRow["servidodia"] = "0";
                    newRow["corralgps"] = row["corralgps"];
                    newRow["latitudi"] = row["latitudi"];
                    newRow["longitudi"] = row["longitudi"];
                    newRow["idd"] = row["idd"];

                    foreach (DataRow row2 in dt2.Rows)
                    {
                        if (newRow["corral"].Equals(row2["corral"]))
                        {
                            newRow["servidodia"] = row2["servidodia"];
                        }
                    }

                    dtsalida.Rows.Add(newRow);

                }

            }

        }

        private void enviarWsdlLocalizaCamion(DataTable dt)
        {

            if (dt.Rows.Count != 0)
            {

                try
                {

                    Datos datos = new Datos();
                    ArrayList lista = new ArrayList();

                    //Cursor = Cursors.WaitCursor;

                    String seleccion = "LOCALIZA";

                    lista.Clear();


                    foreach (DataRow row in dt.Rows)
                    {

                        lista.Add(
                            row["numcamion"].ToString() + "|" +
                            row["fecha"].ToString().Replace("-", "") + "|" +
                            row["hora"].ToString().Replace(":", "") + "|" +
                            row["latitud"].ToString() + "|" +
                            row["longitud"].ToString() + "|" +
                            row["servida"].ToString() + "|" +
                            row["racion"].ToString()

                        );

                    }


                    //lista.Add(txtTransferir.Text+" 2");

                    Thread t = new Thread(() => { transferirWSDLSel(seleccion, lista); });
                    t.IsBackground = true;
                    t.Name = seleccion;
                    t.Start();
          

                    Console.WriteLine("WS "+t.Name + " " + t.ManagedThreadId.ToString() + " " + DateTime.Now.ToString());

                    //------------------------------------------------------------------------------------------
                    //System.Diagnostics.Process procces = System.Diagnostics.Process.GetCurrentProcess();
                    //System.Diagnostics.ProcessThreadCollection threadCollection = procces.Threads;

                    //string threads = string.Empty;

                    //foreach (System.Diagnostics.ProcessThread proccessThread in threadCollection)
                    //{
                    //    if (proccessThread.ThreadState == System.Diagnostics.ThreadState.Running)
                    //    {
                    //        threads += string.Format("Thread Id: {0}, ThreadState: {1}\r\n", proccessThread.Id, proccessThread.ThreadState);
                    //    }
                    //}

                    //Console.WriteLine(threads);
                    //------------------------------------------------------------------------------------------

                    //while (t.IsAlive)
                    //{
                    //    Thread.Sleep(50);
                    //    //Application.DoEvents();
                    //}

                    ////t.Abort();

                    //String errormsg = connectar.getError();
                    //String emsg = connectar.getEmsg();
                    //String url = connectar.getUrl();


                    //string[] lurl = url.Split('/');
                    //string[] serverl = lurl[2].Trim().Split('.');
                    //lblServer.Text = serverl[0].Trim();

                    //lblMandante.Text = lurl[9].Trim();

                    ////Cursor = Cursors.Default;

                    //if (!errormsg.Equals(""))
                    //{
                    //    //MessageBox.Show(errormsg, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    txtError.Text = "Localiza: " + errormsg;


                    //}
                    //if (!emsg.Equals(""))
                    //{
                    //    //MessageBox.Show(emsg, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //    if (emsg.Equals("OK"))
                    //    {

                    //        ////msgErrores("Envio Ws", Constants.MSG_OK);

                    //        //List<string> dbOperations = new List<string>();

                    //        //foreach (DataRow row in dt.Rows)
                    //        //{
                    //        //    dbOperations.Add("UPDATE operacion SET envio = 'S'");
                    //        //}

                    //        //datos.p_sql_list = dbOperations;

                    //        //if (!datos.consultaSQLs(ref dt))
                    //        //{
                    //        //    //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //        //    //msgErrores(datos.p_error, Constants.MSG_ERROR);
                    //        //}
                    //        //else
                    //        //{

                    //        //}


                    //    }

                    //}

                }
                catch (Exception e)
                {
                    txtError.Text = "Localiza: " + e.Message;
                    //ErrorLogging(e);
                }

            }

        }

        private void enviarWsdlRecorridoCamion(DataTable dt)
        {

            if (dt.Rows.Count != 0)
            {

                Datos datos = new Datos();
                ArrayList lista = new ArrayList();

                //Cursor = Cursors.WaitCursor;

                String seleccion = "RECORRIDO";

                lista.Clear();


                foreach (DataRow row in dt.Rows)
                {

                    lista.Add(
                        row["fecha"].ToString().Replace("-", "") + "|" +
                        row["hora"].ToString().Replace(":", "") + "|" +
                        row["numcamion"].ToString() + "|" +
                        row["latitud"].ToString() + "|" +
                        row["longitud"].ToString()

                    );

                }


                //lista.Add(txtTransferir.Text+" 2");

                Thread t = new Thread(() => { transferirWSDLSel(seleccion, lista); });
                t.IsBackground = true;
                t.Name = seleccion;
                t.Start();

                while (t.IsAlive)
                {
                    Thread.Sleep(50);
                    //Application.DoEvents();
                }

                //t.Abort();

                String errormsg = connectar.getError();
                String emsg = connectar.getEmsg();
                String url = connectar.getUrl();


                string[] lurl = url.Split('/');
                string[] serverl = lurl[2].Trim().Split('.');
                lblServer.Text = serverl[0].Trim();

                lblMandante.Text = lurl[9].Trim();

                //Cursor = Cursors.Default;

                if (!errormsg.Equals(""))
                {
                    //MessageBox.Show(errormsg, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtError.Text = "Recorrido " + errormsg;

                    msgError = errormsg;

                }
                if (!emsg.Equals(""))
                {
                    //MessageBox.Show(emsg, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (emsg.Equals("OK"))
                    {

                        //msgErrores("Envio Ws", Constants.MSG_OK);

                        List<string> dbOperations = new List<string>();

                        foreach (DataRow row in dt.Rows)
                        {
                            dbOperations.Add("UPDATE recorrido SET envio = 'S' WHERE idd =" + row["idd"].ToString());
                        }

                        datos.p_sql_list = dbOperations;

                        if (!datos.consultaSQLs(ref dt))
                        {
                            //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            //msgErrores(datos.p_error, Constants.MSG_ERROR);
                        }
                        else
                        {

                        }


                        //MessageBox.Show("Listo, Total " + dt.Rows.Count.ToString() + " Reg.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);


                    }

                }

            }
            else
            {
                //MessageBox.Show("Sin Registros para enviar.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }


        }

        private void enviarWsdlRepartoCorral(DataTable dt)
        {

            if (dt.Rows.Count != 0)
            {

                try
                {

                    Datos datos = new Datos();
                    ArrayList lista = new ArrayList();

                    //Cursor = Cursors.WaitCursor;

                    String seleccion = "REPARTO";

                    lista.Clear();

                    foreach (DataRow row in dt.Rows)
                    {

                        lista.Add(
                            row["corral"].ToString() + "|" +
                            row["fecha"].ToString().Replace("-", "") + "|" +
                            row["hora"].ToString().Replace(":", "") + "|" +
                            row["numcamion"].ToString() + "|" +
                            row["racion"].ToString() + "|" +
                            row["servida"].ToString() + "|" +
                            row["plan"].ToString() + "|" +
                            row["servido"].ToString() + "|" +
                            row["plandia"].ToString() + "|" +
                            row["servidodia"].ToString() + "|" +
                            row["corralgps"].ToString() + "|" +
                            row["latitudi"].ToString() + "|" +
                            row["longitudi"].ToString()


                        );

                    }

                    //lista.Add(txtTransferir.Text+" 2");                                        

                    Thread t = new Thread(() => { transferirWSDLSel(seleccion, lista); });
                    t.IsBackground = true;
                    t.Name = seleccion;
                    t.Start();
                              

                    Console.WriteLine("WS "+t.Name + " " + t.ManagedThreadId.ToString() +" "+ DateTime.Now.ToString());
                    //showThreadActivos();

                    //while (t.IsAlive)
                    //{
                    //    Thread.Sleep(50);
                    //    //Application.DoEvents();
                    //}

                    ////t.Abort();

                    //String errormsg = connectar.getError();
                    //String emsg = connectar.getEmsg();
                    //String url = connectar.getUrl();


                    //string[] lurl = url.Split('/');
                    //string[] serverl = lurl[2].Trim().Split('.');
                    //lblServer.Text = serverl[0].Trim();

                    //lblMandante.Text = lurl[9].Trim();

                    ////Cursor = Cursors.Default;

                    //if (!errormsg.Equals(""))
                    //{
                    //    //MessageBox.Show(errormsg, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    txtError.Text = "Reparto " + errormsg;

                    //    msgError = errormsg;

                    //}
                    //if (!emsg.Equals(""))
                    //{
                    //    //MessageBox.Show(emsg, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //    //if (emsg.Equals("OK"))
                    //    //{

                    //    //    //msgErrores("Envio Ws", Constants.MSG_OK);

                    //    //    List<string> dbOperations = new List<string>();

                    //    //    foreach (DataRow row in dt.Rows)
                    //    //    {
                    //    //        dbOperations.Add("UPDATE recorrido SET envio = 'S' WHERE idd =" + row["idd"].ToString());
                    //    //    }

                    //    //    datos.p_sql_list = dbOperations;

                    //    //    if (!datos.consultaSQLs(ref dt))
                    //    //    {
                    //    //        //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //    //        //msgErrores(datos.p_error, Constants.MSG_ERROR);
                    //    //    }
                    //    //    else
                    //    //    {

                    //    //    }


                    //    //}

                    //}

                }
                catch (Exception e)
                {
                    txtError.Text = e.Message;
                    //ErrorLogging(e);
                }
            }

        }

        private void transferirWSDLSel(String catalogo, ArrayList lectura)
        {
            try
            {
                connectar = new ConnectedWs(Constants.OPERACION_TRANSFERIR, catalogo, lectura, usuario, contrasena,"_");
                connectar.setDataWs();
            }
            catch (Exception e)
            {
                txtError.Text = e.Message;
                //ErrorLogging(e);
            }
        }

        //private void enviarWs()
        //{

        //    //string latitud = txtLatitud.Text;
        //    //string longitud = txtLongitud.Text;
        //    string numcamion = camionConfig();
        //    string cargado = "0";
        //    string hora = DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss");

        //    if (!latitud.Equals("") && !longitud.Equals(""))
        //    {

        //        datosOperacion();
        //        cargado = cargadoRacion(servida, racion);
        //        //envio coordenadas
        //        try
        //        {

        //            string url = "http://" + ip + "/gusi/engorda/gps/wsregistraroperacion.php?camion=" + numcamion + "&latitud=" + latitud + "&longitud=" + longitud + "&racion=" + racion + "&carga=" + cargado + "&servida=" + servida + "&hora=" + hora;
        //            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

        //            req.Method = "POST";
        //            string Data = "message=";// + keys;
        //            byte[] postBytes = Encoding.ASCII.GetBytes(Data);
        //            req.ContentType = "application/x-www-form-urlencoded";
        //            req.ContentLength = postBytes.Length;

        //            req.Timeout = 10;

        //            Stream requestStream = req.GetRequestStream();
        //            requestStream.Write(postBytes, 0, postBytes.Length);
        //            requestStream.Close();

        //            /*
        //            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
        //            Stream resStream = response.GetResponseStream();

        //            var sr = new StreamReader(response.GetResponseStream());
        //            string responseText = sr.ReadToEnd();
        //            */

        //            lblWsEnvio.Text = "Enviado";

        //        }
        //        catch (WebException)
        //        {

        //            lblWsEnvio.Text = "No Envio";
        //            //MessageBox.Show("Please Check Your Internet Connection");
        //        }

        //    }

        //}

        private void leerGps()
        {

            try
            {

                string texto = "";
                string validos = "A";
                double coordenada = 0;

                if (chkWarning.Checked) { validos = "V"; }

                commGps.p_nmeagps = "$GPRMC";

                if (!validapuertoExiste(commGps.p_commgps))
                {
                    //MessageBox.Show("Sin puerto Gps COM disponible", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //btnReConectar.Visible = true;
                    txtError.Text = "Sin puerto Gps COM disponible";

                    statusTimer(false);
                    //btnReConectar.Visible = true;
                    limpiaGps();
                    //cerrarpuertoGps();
                }
                else
                {



                    if (!commGps.recibePuertogps())
                    {

                        //Close();
                        //MessageBox.Show(commGps.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        //msgErrores(commGps.p_error, Constants.MSG_ERROR);
                        txtError.Text = commGps.p_error;
                        //commGps.cerrarPuerto();
                        //commGps.liberarPuerto();

                        //statusTimer(false);

                        //btnReConectar.Visible = true;
                        limpiaGps();
                        //cerrarpuertoGps();
                    }
                    else
                    {

                        texto = commGps.p_datogps.ToString();

                        if (!texto.Equals(""))
                        {

                            string[] words = texto.Split(',');

                            if (words[2].Equals(validos))
                            {
                                coordenada = convertNMEAGradosdecimales(double.Parse(words[3].ToString()), words[4].ToString());
                                gLatitudgps = Math.Round(coordenada, 6);

                                coordenada = convertNMEAGradosdecimales(double.Parse(words[5].ToString()), words[6].ToString());
                                gLongitudgps = Math.Round(coordenada, 6);

                                gfechagps = DateTime.Now.ToString("dd-MM-yyyy");
                                ghoragps = DateTime.Now.ToString("HH:mm:ss");

                                //msgErrores("", Constants.MSG_OK);
                                txtError.Text = "";

                            }
                            else
                            {
                                limpiaGps();
                            }

                        }
                        else
                        {
                            limpiaGps();
                        }

                    }



                }
            }
            catch (IOException e)
            {
                //MessageBox.Show(e.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //msgErrores(e.Message, Constants.MSG_ERROR);
                txtError.Text = e.Message;
                statusTimer(false);
                //btnReConectar.Visible = true;
                limpiaGps();
                //luzStatus(false);
            }


        }


        private void limpiaGps()
        {

            gLatitudgps = 0;
            gLongitudgps = 0;
            //txtFechaGps.Text = "";
            //txtHoraGps.Text = "00:00:00";

            //msgErrores("GPS sin datos a mostrar", Constants.MSG_ERROR);

        }


        private double convertNMEAGradosdecimales(double valor, string indicador)
        {

            double coordenada = 0;
            double residuo = 0;

            try
            {

                coordenada = valor;
                coordenada = coordenada / 100;
                residuo = coordenada % 1;
                coordenada = coordenada - residuo;
                residuo = (residuo * 100) / 60;
                coordenada = coordenada + residuo;

                if (indicador.Equals("S") || indicador.Equals("W"))
                {
                    coordenada = coordenada * -1;
                }

            }
            catch (Exception e)
            {

            }

            return coordenada;
        }

        //private void Principal_Load(object sender, EventArgs e)
        //{

        //    //try
        //    //{

        //    //    Datos datos = new Datos();

        //    //    datos.p_fecha = "2018-12-21";
        //    //    datos.p_num = 1;
        //    //    datos.p_corral = "1345";
        //    //    datos.p_racion = "R2";
        //    //    datos.p_camion = 3;
        //    //    datos.p_cabezas = 110;
        //    //    datos.p_planeado = 450;
        //    //    datos.p_realidad = 340;
        //    //    datos.p_hora = "17:30";

        //    //    if (!datos.insertServidas())
        //    //    {
        //    //        MessageBox.Show(datos.p_error);
        //    //    }


        //    //}
        //    //catch (Exception exc)
        //    //{
        //    //    MessageBox.Show(exc.Message);
        //    //}

        //    formatoGrid(); 


        //}

        //private void formatoGrid() 
        //{ 

        //    DataTable table = new DataTable();

        //    table.Columns.Add("Corral", typeof(string));
        //    table.Columns.Add("Racion", typeof(string));
        //    table.Columns.Add("Plan", typeof(int));
        //    table.Columns.Add("Real", typeof(int));
        //    table.Columns.Add("Var", typeof(int));

        //    table.Rows.Add("1350","R2",230,0,0);
        //    table.Rows.Add("1352","R5",230,0,0);
        //    table.Rows.Add("1355","R3",230,0,0);
        //    table.Rows.Add("1357","R2",230,0,0);

        //    DGServidas.DataSource = table;

        //    DGServidas.Columns[0].Width = 150;
        //    DGServidas.Columns[1].Width = 130;
        //    DGServidas.Columns[2].Width = 130;
        //    DGServidas.Columns[3].Width = 130;
        //    DGServidas.Columns[4].Width = 130;

        //    DGServidas.Columns[0].HeaderCell.Style.Font = new Font("Tahoma", 15.00F, FontStyle.Bold);
        //    DGServidas.Columns[1].HeaderCell.Style.Font = new Font("Tahoma", 15.00F, FontStyle.Bold);
        //    DGServidas.Columns[2].HeaderCell.Style.Font = new Font("Tahoma", 15.00F, FontStyle.Bold);
        //    DGServidas.Columns[3].HeaderCell.Style.Font = new Font("Tahoma", 15.00F, FontStyle.Bold);
        //    DGServidas.Columns[4].HeaderCell.Style.Font = new Font("Tahoma", 15.00F, FontStyle.Bold);

        //    DGServidas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //    DGServidas.ReadOnly = true;
        //    DGServidas.AllowUserToAddRows = false;
        //    DGServidas.AllowUserToDeleteRows = false;
        //    DGServidas.AllowUserToOrderColumns = false;
        //    DGServidas.AllowUserToResizeColumns = false;
        //    DGServidas.AllowUserToResizeRows = false;

        //    DGServidas.MultiSelect = false;

        //    //DGServidas.ColumnHeadersVisible = false;
        //    DGServidas.RowHeadersVisible = false;
        //    DGServidas.CellBorderStyle = DataGridViewCellBorderStyle.None;
        //    DGServidas.DefaultCellStyle.Font = new Font("Arial", 25.00F, FontStyle.Bold);
        //    //DGServidas.RowTemplate.Height = 50;

        //}

        //private void btnLista_Click(object sender, EventArgs e)
        //{

        //    OpenFileDialog openFileDialog1 = new OpenFileDialog();
        //    openFileDialog1.Filter = "Archivos Txt|*.txt";
        //    openFileDialog1.Title = "Seleccion un Archivo Texto";

        //    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {

        //        try
        //        {
        //            StreamReader objReader = new StreamReader(openFileDialog1.OpenFile());
        //            string sLine = "";

        //            //ArrayList arrText = new ArrayList();
        //            while (sLine != null)
        //            {
        //                sLine = objReader.ReadLine();
        //                if (sLine != null)
        //                //arrText.Add(sLine);
        //                {

        //                    string[] lines = sLine.Split(',');

        //                    try
        //                    {

        //                        Datos datos = new Datos();

        //                        datos.p_fecha = DateTime.Now.ToString("dd-MM-yyyy"); //  "2018-12-21";
        //                        datos.p_num = Convert.ToInt32(lines[0]);
        //                        datos.p_corral = lines[1];
        //                        datos.p_racion = lines[2];
        //                        datos.p_cabezas = Convert.ToInt32(lines[3]);
        //                        datos.p_planeado = Convert.ToInt32(lines[4]);
        //                        datos.p_realidad = 0;
        //                        datos.p_hora = "00:00"; //"17:30";

        //                        if (!datos.insertServidas())
        //                        {
        //                            MessageBox.Show(datos.p_error);
        //                        }

        //                    }
        //                    catch (Exception exc)
        //                    {
        //                        MessageBox.Show(exc.Message);
        //                    }

        //                }
        //            }

        //            MessageBox.Show("Listo");

        //        }
        //        catch (Exception)
        //        {
        //            MessageBox.Show("El archivo no se puede leer");
        //        }

        //    } 


        //}

        //private void btnCerrar_Click(object sender, EventArgs e)
        //{
        //    Close();
        //}

        //private void DGServidas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    txtCorral.Text = (string)DGServidas.CurrentRow.Cells[0].Value;
        //    txtRacion.Text = (string)DGServidas.CurrentRow.Cells[1].Value;
        //}

        private DataRow obtieneConfig()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            datos.p_sql = "SELECT TOP 1 * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    return row;
                }
            }

            return null;

        }

        private string puertoConfigGps()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            string portname = "";

            datos.p_sql = "SELECT TOP 1 * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    portname = row["PuertoGPS"].ToString();
                }
            }

            return portname;

        }

        private int baudConfigGps()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            int baudrate = 0;

            datos.p_sql = "SELECT TOP 1 * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    baudrate = Convert.ToInt32(row["BaudRateGPS"]);
                }
            }

            return baudrate;

        }

        private int dataConfigGps()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            int databits = 0;

            datos.p_sql = "SELECT TOP 1 * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    databits = Convert.ToInt32(row["DataBitsGPS"]);
                }
            }

            return databits;

        }


        private string stopConfigGps()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            string stopbits = "";

            datos.p_sql = "SELECT TOP 1 * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    stopbits = row["StopBitsGPS"].ToString();
                }
            }

            return stopbits;

        }

        private string parityConfigGps()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            string paritybits = "";

            datos.p_sql = "SELECT TOP 1 * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    paritybits = row["ParityGPS"].ToString();
                }
            }

            return paritybits;

        }

        //private string ipConfig()
        //{

        //    Datos datos = new Datos();
        //    DataTable dt = new DataTable();

        //    string ip = "";

        //    datos.p_sql = "SELECT TOP 1 * FROM configuracion";

        //    if (!datos.consultaSQL(ref dt, true))
        //    {
        //        MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //    }
        //    else
        //    {
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            ip = row["IP"].ToString();
        //        }
        //    }

        //    return ip;

        //}

        //private string gpsConfig()
        //{

        //    Datos datos = new Datos();
        //    DataTable dt = new DataTable();

        //    string gps = "";

        //    datos.p_sql = "SELECT TOP 1 * FROM configuracion";

        //    if (!datos.consultaSQL(ref dt, true))
        //    {
        //        MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //    }
        //    else
        //    {
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            gps = row["GPS"].ToString();
        //        }
        //    }

        //    return gps;

        //}

        private string usuarioConfigWs()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            string valor = "";

            datos.p_sql = "SELECT TOP 1 * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    valor = row["Usuario"].ToString();
                }
            }

            return valor;

        }

        private string contrasenaConfigWs()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            string valor = "";

            datos.p_sql = "SELECT TOP 1 * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    valor = row["Contrasena"].ToString();
                }
            }

            return valor;

        }

        private string numcamionConfigWs()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            string valor = "";

            datos.p_sql = "SELECT TOP 1 * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    valor = row["numcamion"].ToString();
                }
            }

            return valor;

        }

        private Boolean wsactivoConfigWs()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            Boolean valor = false;

            datos.p_sql = "SELECT TOP 1 * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["ws"].ToString().Equals("S")) { valor = true; } else { valor = false; }
                }
            }

            return valor;

        }

        private void datosOperacion()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            datos.p_sql = "SELECT TOP 1 * FROM operacion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    servida = row["Servida"].ToString();
                    racion = row["Racion"].ToString();
                }
            }



        }

        private string camionConfig()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            string numcamion = "";

            datos.p_sql = "SELECT TOP 1 * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    numcamion = row["NumCamion"].ToString();
                }
            }

            return numcamion;

        }

        //private string latenciaConfig()
        //{

        //    Datos datos = new Datos();
        //    DataTable dt = new DataTable();

        //    string latencia = "";

        //    datos.p_sql = "SELECT TOP 1 * FROM configuracion";

        //    if (!datos.consultaSQL(ref dt, true))
        //    {
        //        MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //    }
        //    else
        //    {
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            latencia = row["Latencia"].ToString();
        //        }
        //    }

        //    return latencia;

        //}

        private string cargadoRacion(string num, string racion)
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            string cargado = "";

            datos.p_sql = "SELECT TOP 1 * FROM raciones where num = " + num + " and racion = '" + racion + "'";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    cargado = row["Cargado"].ToString();
                }
            }

            return cargado;

        }



        //private void btnRefresca_Click(object sender, EventArgs e)
        //{

        //    //operacionGPS();
        //    refrescaGPS();


        //}

        //private void refrescaGPS()
        //{
        //    ip = ipConfig();
        //    timer1.Interval = Convert.ToInt32(latenciaConfig()) * 1000;

        //}

        private void cerrarpuertoGps()
        {

            serialPortGps.Close();
            serialPortGps.Dispose();

            //if (commGps.p_spgps is null)
            //{

            //}
            //else
            //{

            //    //if (gpsConfig().Equals("S"))
            //    //{
            //    //    cerrarPuerto();
            //        commGps.cerrarPuertogps();
            //        commGps.liberarPuertogps();
            //    //}

            //}

        }

        private String[] obtienelistaPuertos()
        {

            string[] puertos = { };

            try
            {
                puertos = System.IO.Ports.SerialPort.GetPortNames();
            }
            catch (Exception e)
            {

            }

            return puertos;

        }

        private bool validapuertoExiste(string puerto)
        {

            string[] puertos = obtienelistaPuertos();

            bool resultado = false;
            string valor = "";

            foreach (string value in puertos)
            {

                if (puerto.Contains(valor))
                {
                    resultado = true;
                }

            }

            return resultado;

        }


        private void guardaLecturaGps()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            //if (!txtLatitud.Text.Equals("00.000000") && !txtLatitud.Text.Equals("0") && !txtLatitud.Text.Equals(""))
            //{ 
            datos.p_sql = "UPDATE operacion SET latitud = " + gLatitudgps.ToString() + ", longitud = " + gLongitudgps.ToString() + ", fechagps = '" + gfechagps + "', horagps = '" + ghoragps + "'";

            if (!datos.consultaSQL(ref dt, false))
            {
                //MessageBox.Show(datos.p_error + datos.p_sql, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtError.Text = datos.p_error;
            }
            //}

            Console.WriteLine("GUARDA GPS" + " " + DateTime.Now.ToString());

        }

        private void guardaLecturaGpsRecorrido()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();


            if (gLatitudgps != 0 && gLongitudgps != 0)
            {
                datos.p_sql = "INSERT INTO recorrido (fecha,hora,latitud,longitud,numcamion) values('" + gfechagps + "','" + ghoragps + "'," + gLatitudgps.ToString() + "," + gLongitudgps.ToString() + "," + numcamion + ")";

                if (!datos.consultaSQL(ref dt, true))
                {
                    //MessageBox.Show(datos.p_error + datos.p_sql, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //msgErrores(datos.p_error, Constants.MSG_ERROR);
                    txtError.Text = datos.p_error;

                }
                else
                {

                }

                Console.WriteLine("GUARDA RECORRIDO" + " " + DateTime.Now.ToString());

            }

        }


        private void luzStatus(Boolean estado)
        {

            if (estado)
            {
                picVerde.Visible = true;
                picRojo.Visible = !picVerde.Visible;
                //Console.Beep(900, 100);
                //Console.Beep(900, 100);
            }
            else
            {
                picVerde.Visible = false;
                picRojo.Visible = !picVerde.Visible;
                //Console.Beep(900, 100);
            }

        }

        private void usuarioiWs()
        {
            usuario = usuarioConfigWs();
            contrasena = contrasenaConfigWs();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss tt");

            //lblContWs.Text = System.Diagnostics.Process.GetCurrentProcess().Threads.Count.ToString();


            if (wsactivo && lblConectadoF.Value==1)
            {
                focoWs();
            }
            else
            {
                lblWsActivoF.Value = 0;
            }

            if (!lblLatitud.Text.Equals("0") && ! lblLongitud.Text.Equals("0"))
            {
                focoGps();
            }
            else
            {
                lblGpsActivoF.Value = 0;
            }

        }

        private void focoWs()
        {
            
            if (lblWsActivoF.Value == 1)
            {
                lblWsActivoF.Value = 0;
            }
            else
            {
                lblWsActivoF.Value = 1;
            }

        }

        private void focoGps()
        {

            if (lblGpsActivoF.Value == 1)
            {
                lblGpsActivoF.Value = 0;
            }
            else
            {
                lblGpsActivoF.Value = 1;
            }

        }


        private void SerialPortGps2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                recibidos = serialPortGps.ReadExisting();
                this.Invoke(new EventHandler(Actualizar)); 
            }
            catch 
            {

            } 

        }

        private void Actualizar(object s, EventArgs e)
        {
            txtLectura.Text = recibidos;
        }


        private void SerialPortGps_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            try
            {

                if (!pauseGps)
                {

                    //-----------------------------
                    SerialPort port = (SerialPort)sender;
                    byte[] _buffer = new byte[port.BytesToRead];
                    port.Read(_buffer, 0, _buffer.Length);

                    //Parse buffer
                    string sdata = "";
                    Encoding encoding = ASCIIEncoding.GetEncoding(1252);
                    if (null != _buffer)
                    {
                        sdata = encoding.GetString(_buffer);
                    }

                    //-----------------------------
                    //string sdata = serialPortGps.ReadExisting();
                    Thread.Sleep(50);//200
                    //-----------------------------

                    if (sdata.Contains("GPRMC"))
                    {

                        string[] string_array = sdata.Split('$');
                        string Gprmc = null;

                        for (int i = 0; i < string_array.Length; i++)
                        {
                            string stringTemp = string_array[i];//.Replace("\n","").Replace("\r","");
                            string[] line_array = stringTemp.Split(',');
                            if (line_array[0] == "GPRMC")
                            {

                                Gprmc = string.Join(",", string_array[i]);
                                //Gprmc = line_array[3] + "," + line_array[5];

                                if (Gprmc != null)
                                {

                                    //if (line_array[4].Length > 0 && line_array[6].Length > 0)
                                    if (line_array[4].Equals("N") && line_array[6].Equals("W"))
                                    {
                                        displayToWindow(Gprmc);
                                    }

                                }

                                break;
                            }
                        }

                        //if (Gprmc != null)
                        //{
                        //    displayToWindow(Gprmc);
                        //}

                        //else
                        //{
                        //    displayToWindow("");
                        //}

                    }

                }


            }
            catch //(Exception merr)
            {
                //MessageBox.Show(merr.Message);

                //ErrorLogging(merr);

                //Console.WriteLine("GPS " + merr + DateTime.Now.ToString());
            }

        }

        //private void SerialPort_ErrorReceivedHandler(object sender, SerialErrorReceivedEventArgs e)
        //{


        //    //TODO processing error message here
        //    string message = "";
        //    switch (e.EventType)
        //    {
        //        case SerialError.Frame:
        //            message = "Framing error ";
        //            break;
        //        case SerialError.Overrun:
        //            message = "character-buffer overrun ";
        //            break;
        //        case SerialError.RXOver:
        //            message = "Input buffer overflow";
        //            break;
        //        case SerialError.RXParity:
        //            message = "parity error pada hardware";
        //            break;
        //        case SerialError.TXFull:
        //            message = "transmit data, namun output buffer sedang penuh";
        //            break;
        //    }

        //    Invoke(new Action(() =>
        //    {
        //        lblErrorGPS.Text += message.ToString() + '\n' + '\r';
        //    }));

        //    //lblErrorGPS.Invoke(new EventHandler(delegate { lblErrorGPS.Text += e.ToString() + '\n' + '\r'; }));

        //}

        private void displayToWindow(string inData)
        {
            BeginInvoke(new EventHandler(delegate
            {

                string validos = "A";
                if (chkWarning.Checked) { validos = "V"; }

                Console.WriteLine("GPS " + inData + DateTime.Now.ToString());

                gLatitudgps = separaNMEA(inData, validos,3);
                gLongitudgps = separaNMEA(inData, validos,5);

                lblLatitud.Text = gLatitudgps.ToString();
                lblLongitud.Text = gLongitudgps.ToString();

                gfechagps = DateTime.Now.ToString("dd-MM-yyyy");
                ghoragps = DateTime.Now.ToString("HH:mm:ss");

                txtLectura.Text = inData;

            }));
        }

        private double separaNMEA(String valor, string validos, int col)
        {

            double coordenada = 0;
            double resultado = 0;

            try
            {

                if (!valor.Equals(""))
                {
                    string[] words = valor.Split(',');

                    if (words[2].Equals(validos))
                    {
                        if (!words[col].ToString().Equals(""))
                        {
                            coordenada = convertNMEAGradosdecimales(double.Parse(words[col].ToString()), words[col + 1].ToString());
                            resultado = Math.Round(coordenada, 6);
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }

            return resultado;

        }

        private void btnRecorrido_Click(object sender, EventArgs e)
        {

            
            listBoxRecorridos.Visible = !listBoxRecorridos.Visible;
            

            Cursor = Cursors.WaitCursor;

            consultaRecorridoCamion(dtCamiontmp);

            if (dtCamiontmp.Rows.Count == 0)
            {
                listBoxRecorridos.Visible = false;
                //MessageBox.Show("Sin registros para enviar.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                Comun.mensajeVer("Sin registros para enviar.", Constants.MSG_BOTON_OK, Constants.MSG_IMG_WARNING);
            }
            else
            {
                //listBoxRecorridos.Visible = true;

                listBoxRecorridos.Items.Clear();

                foreach (DataRow row in dtCamiontmp.Rows)
                {
                    String valor = row["fecha"].ToString();

                    if (!listBoxRecorridos.Items.Contains(valor))
                    {
                        listBoxRecorridos.Items.Add(valor);
                    }
                }

            }

            Cursor = Cursors.Default;


            //if (MessageBox.Show("¿ Desea descargar recorridos ?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{

            //    Cursor = Cursors.WaitCursor;

            //    consultaRecorridoCamion(dtCamiontmp);

            //    if (dtCamiontmp.Rows.Count == 0)
            //    {
            //        MessageBox.Show("Sin registros para enviar.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            //    }
            //    else
            //    {
            //        msgError = "";

            //        enviarWsdlRecorridoCamion(dtCamiontmp);

            //        limpiarRecorridoCamion();                    

            //        if (msgError.Equals(""))
            //        {
            //            MessageBox.Show("Listo, Total " + dtCamiontmp.Rows.Count.ToString() + " Reg.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //        }
            //        else
            //        {
            //            MessageBox.Show(msgError, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            //        }
            //    }

            //    Cursor = Cursors.Default;

            //}



        }

        private void limpiarRecorridoCamion()
        {
            Datos datos = new Datos();
            DataTable dt = new DataTable();

            datos.p_sql = "DELETE FROM recorrido WHERE envio = 'S'";

            if (!datos.consultaSQL(ref dt, false))
            {
                //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //filas = dt.Rows.Count;
            }

        }

        //public void ErrorLogging(Exception ex)
        //{

        //    String fechahoy = DateTime.Now.ToString("dd-MM-yyyy");


        //    string strPath = Application.StartupPath + "\\log\\log_" + fechahoy + ".txt";
        //    if (!File.Exists(strPath))
        //    {
        //        File.Create(strPath).Dispose();
        //    }
        //    using (StreamWriter sw = File.AppendText(strPath))
        //    {
        //        sw.WriteLine("=============Error Logging ===========");
        //        sw.WriteLine("===========Start============= " + DateTime.Now);
        //        sw.WriteLine("Error Message: " + ex.Message);
        //        sw.WriteLine("Stack Trace: " + ex.StackTrace);
        //        sw.WriteLine("===========End============= " + DateTime.Now);

        //    }
        //}

        private void statusTimer(Boolean valor)
        {

            timerWs.Enabled = valor;
            timerRecorrido.Enabled = timerWs.Enabled;
            timerBateria.Enabled = timerWs.Enabled;

        }

        private void carpeta_existe_local()
        {

            if (!System.IO.Directory.Exists(Application.StartupPath + "\\log"))
            {
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\\log");
            }

        }

        private void btnReparto_Click(object sender, EventArgs e)
        {

            //if (!wsactivo)
            //{
            //    MessageBox.Show("Ws apagado.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

            //}
            //else
            //{

            if (Comun.mensajeVer("¿ Desea descargar repartos de hoy ?", Constants.MSG_BOTON_SINO, Constants.MSG_IMG_QUESTION) == Constants.MSG_BOTON_SI)
            //if (MessageBox.Show("¿ Desea descargar repartos de hoy ?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                Cursor = Cursors.WaitCursor;

                consultaRepartoCorral(dtRepartotmp);

                if (dtRepartotmp.Rows.Count == 0)
                {
                    //MessageBox.Show("Sin registros para enviar.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    Comun.mensajeVer("Sin registros para enviar.", Constants.MSG_BOTON_OK, Constants.MSG_IMG_WARNING);
                }
                else
                {
                    msgError = "";

                    enviarWsdlRepartoCorral(dtRepartotmp);

                    if (msgError.Equals(""))
                    {
                        //MessageBox.Show("Listo, Total " + dtRepartotmp.Rows.Count.ToString() + " Reg.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        Comun.mensajeVer("Listo, Total " + dtRepartotmp.Rows.Count.ToString() + " Reg.", Constants.MSG_BOTON_OK, Constants.MSG_IMG_OK);
                    }
                    else
                    {
                        MessageBox.Show(msgError, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                Cursor = Cursors.Default;

            }
            //}                       

        }

        private void timerError_Tick(object sender, EventArgs e)
        {
            txtError.Text = "";
        }

        private void listBoxRecorridos_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (!redConectado())
            {
                //MessageBox.Show("Sin red disponible.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                Comun.mensajeVer("Sin red disponible.", Constants.MSG_BOTON_OK, Constants.MSG_IMG_WARNING);
            }
            else
            {

                msgError = "";

                DataView dv = new DataView(dtCamiontmp);
                dv.RowFilter = "fecha = '" + listBoxRecorridos.Text + "'";

                //dtCamiontmp.Rows.Clear();

                dtCamiontmp = dv.ToTable();

                enviarWsdlRecorridoCamion(dtCamiontmp);

                limpiarRecorridoCamion();

                if (msgError.Equals(""))
                {
                    //MessageBox.Show("Listo, Total " + dtCamiontmp.Rows.Count.ToString() + " Reg.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    Comun.mensajeVer("Listo, Total " + dtCamiontmp.Rows.Count.ToString() + " Reg.", Constants.MSG_BOTON_OK, Constants.MSG_IMG_OK);
                }
                else
                {
                    MessageBox.Show(msgError, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }

            }

            listBoxRecorridos.Visible = false;

        }


        //private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        //{
        //    MessageBox.Show(e.Exception.Message + " " + e.Exception.StackTrace, "Unhandled Thread Exception");
        //    //ErrorLogging(e.Exception);
        //    // here you can log the exception ...
        //}

        //private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        //{
        //    MessageBox.Show((e.ExceptionObject as Exception).Message + " " + (e.ExceptionObject as Exception).StackTrace, "Unhandled UI Exception");
        //    // here you can log the exception ...
        //    ErrorLogging((e.ExceptionObject as Exception));
        //}

        private void btnIniciaGps_Click(object sender, EventArgs e)
        {            
            iniciaGps();       
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

            if (!redConectado())
            {
                //MessageBox.Show("Sin red disponible.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                Comun.mensajeVer("Sin red disponible.", Constants.MSG_BOTON_OK, Constants.MSG_IMG_WARNING);
            }
            else
            {

                //if (Comun.mensajeVer("¿ Desea actualizar version ?", Constants.MSG_BOTON_SINO, Constants.MSG_IMG_QUESTION) == Constants.MSG_BOTON_SI)
                //{

                    try
                    {                        

                        vrutared = rutared();

                        string sourcePath = vrutared + nameruta + nameapp + nameappnom + "\\Senga.exe";
                        string actualPath = Directory.GetCurrentDirectory() + "\\Senga.exe"; ;
                        string targetPath = Directory.GetCurrentDirectory() + "\\Senga_nva_"+ DateTime.Now.ToString("ddMMyyHHmmss") + ".exe"; 

                        FileVersionInfo sourcePathFileVersionInfo = FileVersionInfo.GetVersionInfo(sourcePath);
                        DateTime sourcedt = File.GetLastWriteTime(sourcePath);
                        FileVersionInfo actualPathFileVersionInfo = FileVersionInfo.GetVersionInfo(actualPath);
                        DateTime actualdt = File.GetLastWriteTime(actualPath);

                        string vsourcePath = sourcePathFileVersionInfo.FileVersion;
                        string vactualPath = actualPathFileVersionInfo.FileVersion;


                        //foreach (String srcPath in Directory.GetFiles(Directory.GetCurrentDirectory()))
                        //{
                        //    if (srcPath.Contains("Senga_nva")) 
                        //    {
                        //       string a = "";
                        //    }
                        //}

                        //if (sourcedt == actualdt)
                        if (vsourcePath == vactualPath)
                        {
                            Comun.mensajeVer("No hay actualizaciones a una nueva version de la App", Constants.MSG_BOTON_OK, Constants.MSG_IMG_WARNING);
                        }
                        else
                        {
                            if (Comun.mensajeVer("¿ Desea actualizar nueva version " + vsourcePath + " ?", Constants.MSG_BOTON_SINO, Constants.MSG_IMG_QUESTION) == Constants.MSG_BOTON_SI)
                            {

                                pictureBox1.Visible = false;
                                this.Refresh();

                                

                                File.Copy(sourcePath, targetPath, true);
                                
                                Rename(actualPath, "Senga_"+ vactualPath + "_" + DateTime.Now.ToString("ddMMyyHHmmss"));
                                
                                Rename(targetPath, "Senga");

                                File.Copy(sourcePath, targetPath, true);

                                Application.Restart();

                                //Comun.mensajeVer("Listo reinciar aplicacion", Constants.MSG_BOTON_OK, Constants.MSG_IMG_OK);

                        }
                        } 
                        
          
                    }
                    catch (Exception ec)
                    {
                        Comun.mensajeVer(ec.Message, Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
                    }

                //}

            }

        }

        private void cambiosSQL(String version) 
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();
            
            lblSQL.Text = "";
            Boolean activa = false;

            string resource_data = Properties.Resources.SQL;
            string[] words = resource_data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string lines in words)
            {

                if (lines.Contains("--v"))
                {

                    version = version.Replace("--v","").Replace(";", "").Replace(".","");
                    string ver = lines.Replace("--v","").Replace(";", "").Replace(".","");

                    long v1 = long.Parse(version);
                    long v2 = long.Parse(ver);
                
                    if (v1 <= v2)
                    {
                        activa = true;
                    }                    

                }

                if (activa)
                {
                    lblSQL.Text += lines + '\r' + '\n';
                    if (!lines.Substring(0, 2).ToLower().Equals("--"))
                    {
                        datos.p_sql = lines;
                        datos.consultaSQL(ref dt, false);
                    }
                }

            }



        }

        public void Rename(string filePath, string newFileName)
        {
            
            var newFilePath = Path.Combine(Path.GetDirectoryName(filePath), newFileName + Path.GetExtension(filePath));
            
            if (!File.Exists(newFilePath))
            {
                File.Move(filePath, newFilePath);
            }

        }

        private string rutared()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            string valor = "";

            datos.p_sql = "SELECT TOP 1 * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    valor = row["RutaRed"].ToString();
                }
            }

            return valor;

        }

        private void ShowPowerStatus()
        {
        
            PowerStatus status = SystemInformation.PowerStatus;
            
            //cargando
            txtChargeStatus.Text = status.BatteryChargeStatus.ToString();
            
            if (status.BatteryFullLifetime == -1)
                txtFullLifetime.Text = "Unknown";
            else
                txtFullLifetime.Text = status.BatteryFullLifetime.ToString();

            //porcentaje
            txtCharge.Text = status.BatteryLifePercent.ToString("P0");
            pbBateria.Maximum = 100;
            float porc = status.BatteryLifePercent;
            porc *= 100;
            pbBateria.Value = Convert.ToInt32(porc);
            lblBateria.Text = pbBateria.Value.ToString() + " %";
            
            Console.WriteLine("Pila " + lblBateria.Text + "% " + DateTime.Now.ToString());

            //horas
            if (status.BatteryLifeRemaining == -1)
            {
                txtLifeRemaining.Text = "Unknown";
                lblBateriaTiempo.Text = "";
            }
            else
            {
                txtLifeRemaining.Text = status.BatteryLifeRemaining.ToString();
                int tsegundos = Convert.ToInt32(txtLifeRemaining.Text);
                lblBateriaTiempo.Text = CalcularTiempo(tsegundos);            
            }


            //status cable
            txtLineStatus.Text = status.PowerLineStatus.ToString();

            if (status.PowerLineStatus.ToString().ToLower().Equals("offline"))
            {
                lblBateriaActivoF.Value = 0;
            }
            else if (status.PowerLineStatus.ToString().ToLower().Equals("online"))
            {
                lblBateriaActivoF.Value = 1;
                lblBateriaTiempo.Text = "~";
            }

        }

        private String CalcularTiempo(Int32 tsegundos)
        {
            Int32 horas = (tsegundos / 3600);
            Int32 minutos = ((tsegundos - horas * 3600) / 60);
            Int32 segundos = tsegundos - (horas * 3600 + minutos * 60);
            return horas.ToString() + "h " + minutos.ToString() + "m";// + segundos.ToString();
        }

        private void btnBateria_Click(object sender, EventArgs e)
        {
            ShowPowerStatus();
        }

        private void timerBateria_Tick(object sender, EventArgs e)
        {
            ShowPowerStatus();
        }

        private void btnTemperatura_Click(object sender, EventArgs e)
        {


            Double temperature = 0;
            String instanceName = "";


            // Consultar la API MSAcpi_ThermalZoneTemperature
            // Nota: ejecute su aplicación o Visual Studio (mientras programa) o obtendrá "Acceso denegado"
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"root\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature");

            foreach (ManagementObject obj in searcher.Get())
            {
                temperature = Convert.ToDouble(obj["CurrentTemperature"].ToString());
                // Convierta el valor a grados celsius
                temperature = (temperature - 2732) / 10.0;
                instanceName = obj["InstanceName"].ToString();
            }

            //btnTemperatura.Text = temperature.ToString();

        }


        //private static void ShowThreadInformation(String taskName)
        //{
        //    String msg = null;
        //    Thread thread = Thread.CurrentThread;
        //    //lock (lockObj)
        //    //{
        //        msg = String.Format("{0} thread information\n", taskName) +
        //              String.Format("   Background: {0}\n", thread.IsBackground) +
        //              String.Format("   Thread Pool: {0}\n", thread.IsThreadPoolThread) +
        //              String.Format("   Thread ID: {0}\n", thread.ManagedThreadId);
        //    //}
        //    Console.WriteLine(msg);
        //}

        //private void showThreadActivos() 
        //{

        //    System.Diagnostics.Process procces = System.Diagnostics.Process.GetCurrentProcess();
        //    System.Diagnostics.ProcessThreadCollection threadCollection = procces.Threads;

        //    string threads = string.Empty;

        //    foreach (System.Diagnostics.ProcessThread proccessThread in threadCollection)
        //    {
        //        if (proccessThread.ThreadState == System.Diagnostics.ThreadState.Running)
        //        {
        //            threads += string.Format("Thread Id: {0}, ThreadState: {1}\r\n", proccessThread.Id, proccessThread.ThreadState);
        //        }
        //    }

        //    Console.WriteLine(threads);

        //}


        //private void iniciaPesoOpc2()
        //{

        //    try
        //    {
        //        serialPortPeso.PortName = puertoConfig();
        //        serialPortPeso.BaudRate = baudConfig();
        //        serialPortPeso.Parity = (Parity)Enum.Parse(typeof(Parity), parityConfig(), true);
        //        serialPortPeso.DataBits = dataConfig();
        //        serialPortPeso.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopConfig(), true);
        //        serialPortPeso.DataReceived += new SerialDataReceivedEventHandler(SerialPortPeso_DataReceived);
        //        serialPortPeso.ReadTimeout = 500;
        //        serialPortPeso.WriteTimeout = 500;
        //        //serialPort1.NewLine = "\r\n";
        //        serialPortPeso.Open();

        //        RecibirPeso = true;

        //    }
        //    catch (Exception err)
        //    {
        //        Close();
        //        //MessageBox.Show(err.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //        Comun.mensajeVer(err.Message + "\n\rVerifique Cable Peso o \n\rConfiguracion del Puerto COM", Constants.MSG_BOTON_OK, Constants.MSG_IMG_WARNING);
        //        //ErrorLogging(err);
        //    }


        //}

        //private void SerialPortPeso_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{

        //    try
        //    {

        //        if (this.Enabled)
        //        {

        //            //lblRecibePeso.Visible = !lblRecibePeso.Visible;

        //            //char v = (char)serialPort1.ReadByte();
        //            //byte b = (byte)serialPort1.ReadByte();


        //            string indata = serialPortPeso.ReadExisting();
        //            Thread.Sleep(10);

        //            string stringTemp = indata;
        //            char c = (char)02;
        //            stringTemp = stringTemp.Replace(c.ToString(), "").Replace(" ", "").Trim();

        //            string[] string_array = stringTemp.Split('\r');

        //            int j = string_array.Length;
        //            for (int i = 0; i < j; i++)
        //            {
        //                stringTemp = string_array[i].Trim();
        //                long number1 = 0;
        //                if (long.TryParse(stringTemp, out number1))
        //                {
        //                    this.BeginInvoke(new DelegaAcceso(si_DataReceived), new object[] { stringTemp });
        //                    //Console.WriteLine(stringTemp);
        //                }
        //            }

        //            //this.BeginInvoke(new DelegaAcceso(si_DataReceived), new object[] { stringTemp });
        //            //Console.WriteLine(stringTemp);


        //        }

        //    }
        //    catch (Exception merr)
        //    {
        //        Console.WriteLine("Error " + merr);
        //    }
        
        //}

        //private void si_DataReceived(string accion)
        //{

        //    if (RecibirPeso)
        //    {

        //        txtBascula.Text = accion;
        //        txtBascula.Refresh();
        //        //procesaPeso(txtBascula.Text);

        //        //Console.WriteLine(accion);
        //        //Console.WriteLine(txtBascula.Text);
        //    }

        //}

        private string puertoConfig()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            string portname = "";

            datos.p_sql = "SELECT TOP 1 * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    portname = row["Puerto"].ToString();
                }
            }

            return portname;

        }

        private int baudConfig()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            int baudrate = 0;

            datos.p_sql = "SELECT TOP 1 * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    baudrate = Convert.ToInt32(row["BaudRate"]);
                }
            }

            return baudrate;

        }

        private int dataConfig()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            int databits = 0;

            datos.p_sql = "SELECT TOP 1 * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    databits = Convert.ToInt32(row["DataBits"]);
                }
            }

            return databits;

        }

        private string parityConfig()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            string paritybits = "";

            datos.p_sql = "SELECT TOP 1 * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    paritybits = row["Parity"].ToString();
                }
            }

            return paritybits;

        }

        private string stopConfig()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            string stopbits = "";

            datos.p_sql = "SELECT TOP 1 * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            } 
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    stopbits = row["StopBits"].ToString();
                }
            }

            return stopbits;

        }

        private void btnSimularGps_Click(object sender, EventArgs e)
        {


            string[] valores = txtSimularLatLng.Text.Split(',');
            if (!txtSimularLatLng.Text.Equals(""))
            {
                gLatitudgps = double.Parse(valores[0]);
                gLongitudgps = double.Parse(valores[1]);
            }
            if (!txtSimularLatitud.Text.Equals("") && !txtSimularLongitud.Text.Equals(""))
            {
                gLatitudgps = double.Parse(txtSimularLatitud.Text);
                gLongitudgps = double.Parse(txtSimularLongitud.Text);
            }

            lblLatitud.Text = gLatitudgps.ToString();
            lblLongitud.Text = gLongitudgps.ToString();

            gfechagps = DateTime.Now.ToString("dd-MM-yyyy");
            ghoragps = DateTime.Now.ToString("HH:mm:ss");

        }

        private void btnONGps_Click(object sender, EventArgs e)
        {
            pauseGps = false;
            //serialPortGps.DataReceived += new SerialDataReceivedEventHandler(SerialPortGps_DataReceived);
        }

        private void btnOFFGps_Click(object sender, EventArgs e)
        {
            pauseGps = true;
            //serialPortGps.DataReceived -= new SerialDataReceivedEventHandler(SerialPortGps_DataReceived);
        }

        private void btnConecta_Click(object sender, EventArgs e)
        {
            if (!serialPortGps.IsOpen)
                serialPortGps.Open();
        
        }

        private void btnDesconecta_Click(object sender, EventArgs e)
        {

            if (serialPortGps.IsOpen)
                serialPortGps.Close();
        
        }

        private void btnServicio_Click(object sender, EventArgs e)
        {

            ServiceBase[] servicesToRun;
            servicesToRun = new ServiceBase[]
            {
                new ServicioGPS()//,
                //new Service2()
            };
            ServiceBase.Run(servicesToRun);
          

        }

        private void timerGps_Tick(object sender, EventArgs e)
        {

            try
            {

                string sdata = serialPortGps.ReadExisting();

                string[] string_array = sdata.Split('$');
                string Gprmc = null;

                for (int i = 0; i < string_array.Length; i++)
                {
                    
                    string stringTemp = string_array[i];//.Replace("\n","").Replace("\r","");
                    string[] line_array = stringTemp.Split(',');

                    if (line_array[0] == "GPRMC")
                    {

                        Gprmc = string.Join(",", string_array[i]);

                        if (Gprmc != null)
                        {

                            if (line_array[4].Equals("N") && line_array[6].Equals("W"))
                            {

                                string inData = Gprmc;

                                string validos = "A";
                                if (chkWarning.Checked) { validos = "V"; }

                                Console.WriteLine("GPS " + inData + DateTime.Now.ToString());

                                gLatitudgps = separaNMEA(inData, validos, 3);
                                gLongitudgps = separaNMEA(inData, validos, 5);

                                lblLatitud.Text = gLatitudgps.ToString();
                                lblLongitud.Text = gLongitudgps.ToString();

                                gfechagps = DateTime.Now.ToString("dd-MM-yyyy");
                                ghoragps = DateTime.Now.ToString("HH:mm:ss");

                                txtLectura.Text = inData;

                            }

                        }

                        break;
                    }

                }

            }
            catch { }

        }






    }

}