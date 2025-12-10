using Microsoft.Office.Interop.Access.Dao;
using NativeWifi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;




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

        //public string gServida = "";
        //public string gRacion = "";
        public string gPlanta = "";
        //string ip = "";
        string recibidos = "";

        public double gLatitudgps = 0;
        public double gLongitudgps = 0;
        string gfechagps = DateTime.Now.ToString("dd-MM-yyyy");
        string ghoragps = "00:00:00";
        string gTodo = "TODO";

        DateTime ultError = new DateTime();
        DataTable dtRaciontmp;
        DataTable dtRacionestmp;
         DataTable dtCamiontmp;
        DataTable dtRepartotmp;

        ConnectedWs connectar;

        String puerto = "";
        String puertogps = "";
        String usuario = "";
        String contrasena = "";
        String numcamion = "";
        Boolean wsactivo = false;
        String appgps = "GpsGateXP";
        String msgError = "";
        String versionAct = "";


        bool RecibirPeso = false;
        public bool pauseGps = false;

        long lngBytesSend = 0;
        long lngBtyesReceived = 0;
        string vcamion = "";

        string vCadenaGps = "";

        //public void Start()
        //{
        //    timerWs.Start();
        //}

        //public void Stop()
        //{
        //    timerWs.Stop();
        //}

        public static Principal instance = null;


        //Graphics H;
        //Pen hora1, min, seg;
        //Brush E;


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



            panelCristal2.BackColor = Color.Lime;//FromArgb(100, 88, 104, 105);
            panelCristal2.Dock = DockStyle.Top;
            panelCristal2.Height = 1;

            panelCristal.BackColor = Color.Lime;//FromArgb(100, 88, 104, 105);
            panelCristal.Dock = DockStyle.Bottom;
            panelCristal.Height = 1;




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

            lblIniciandoApp.Top = this.Height/4 * 3;
            lblIniciandoApp.Left = this.Width/2 - lblIniciandoApp.Width/2;

            mostrarBotones(false);
            panelNombre.Visible = true;

            this.Text = "v" + Application.ProductVersion + " - " + File.GetLastWriteTime(Application.ExecutablePath) + " - " + bitModo();

            versionAct = Application.ProductVersion;
            lblNomMaq.Text = Environment.MachineName.ToString();

            lblVersion.Text = "v" + versionAct; //+ " - " + bitModo();
                    
            //cambiosSQL(versionAct);

            string texto ="";

            //panelInicial();

            posicionBotones();
       
            btnPlanes.Focus();

            this.Refresh();

            if (validaBD(ref texto))
            {
                Comun.mensajeVer(texto, Constants.MSG_BOTON_OK, Constants.MSG_IMG_WARNING);
                Close();
            }
            else
            {
                //////////////////////////////////////////////////

                //consultasSql("alter table Configuracion add column NumCamionFis long;");
                //consultasSql("update Configuracion set NumCamionFis = NumCamion where NumCamionFis is null; ");
       






                /////////////////////////////////////////////////

                //iniciaPesoOpc2();

                DataRow row = obtieneConfig();

                if (row != null)
                {
                    usuario = row["Usuario"].ToString();
                    contrasena = row["Contrasena"].ToString();
                    numcamion = row["numcamion"].ToString();

                    wsactivo = row["ws"].ToString().Equals("S");
                    puerto = row["Puerto"].ToString();
                    puertogps = row["PuertoGps"].ToString();

                }

                carpeta_existe_local();

                tablaCamionTemp();
                tablaRacionTemp();
                tablaRacionesTemp();
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
                limpiaServidas();
                luzStatus(false);

                //operacionGPS();
                //iniciaGps();

                ShowPowerStatus();

                statusTimer(true);                    

                //ejecutaAppGPS();

                //ServicioGPS servicioGPS = new ServicioGPS();
                //servicioGPS.InitializeLifetimeService();

                txtError.Visible = true;
                btnPlanes.Enabled = true;
                picPlanes.Enabled = true;

                //lblIniciandoApp.Visible = false;
                //mostrarBotones(true);
                //etiquetasBotones();
                
                this.Refresh();

                btnPlanes.Focus();

                vcamion = camionConfig();


                lblCamion.Text = "CAM\n\r"+vcamion;
                lblNumCamion.Text = ""+vcamion;




                ///////////////////////////////////
                lblPortGPS3.Text = 
                    Comun.ExtraerEntreParentesis(Comun.fportDescripcion("nmea port")); //interno

                lblPortGPS.Text = 
                    Comun.ExtraerEntreParentesis(Comun.fportDescripcion("usb serial port")); //externo

                lblPortGPS2.Text = puertogps; //actual

                //gps ext
                lblMsgGPS.Visible = 
                    (lblPortGPS2.Text.Length>0 &&
                    lblPortGPS.Text.Length>0 &&
                    lblPortGPS2.Text != lblPortGPS.Text);
                  
                //gps int
                if (lblPortGPS3.Text.Length > 0 &&
                    lblPortGPS2.Text.Length > 0 &&
                    lblPortGPS3.Text == lblPortGPS2.Text)
                {
                    lblMsgGPS.Visible = false;
                }

                //lblMsgGPS.Visible = true;


                if (! lblMsgGPS.Visible) //inicia si no esta boton
                {
                    iniciaGps();
                }
                /////////////////////////////////



                lblPuerto.Text = puerto;
                lblPuertoGps.Text = puertogps;

                verConfigServ();
                datosOperacion();

            }

            //Greloj.Visible = true;
            //toolStrip1.Visible = true;

            //lblSenal.Text = fNet();


            lblIniciandoApp.Visible = false;
            mostrarBotones(true);
            etiquetasBotones();


            Cursor = Cursors.Default;

        }

        void mostrarBotones(bool valor)
        {

            picPlanes.Visible = valor;
            picSalir.Visible = valor;

            //panelNombre.Visible = valor;
            //panelBaterial.Visible = valor;
            //panelHoral.Visible = valor;
            panelUbic.Visible = valor;
            //panelSenal.Visible = valor;

            picConfigurar.Visible = valor;
            picPuertos.Visible = valor;

            picNumero.Visible = valor;
            picPlanta.Visible = valor;
            picConfigurarT.Visible = valor;
            picRecorrido.Visible = valor;
            //picOpciones.Visible = valor;

            picIniGps.Visible = valor;
            picConectGps.Visible = valor;
            
        }

        public String fNetFiltar(string nombre)
        {

            var process = new Process
            {
                StartInfo =
                    {
                    FileName = "netsh.exe",
                    Arguments = "wlan add filter permission=block ssid=" + nombre + " networktype=infrastructure",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    Verb = "runas" // Ejecutar como administrador
                    }
            };
            process.Start();

            return process.StandardOutput.ReadToEnd();

        }

        public String fNetQuitarFiltro(string nombre)
        {

            var process = new Process
            {
                StartInfo =
                    {
                    FileName = "netsh.exe",
                    Arguments = "wlan delete filter permission=block ssid=" + nombre + " networktype=infrastructure",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    Verb = "runas" // Ejecutar como administrador
                    }
            };
            process.Start();

            return process.StandardOutput.ReadToEnd();

        }

        public String fNetV()
        {

            var process = new Process
            {
                StartInfo =
                    {
                    FileName = "netsh.exe",
                    Arguments = "wlan show interfaces",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                    }
            };
            process.Start();

            return process.StandardOutput.ReadToEnd(); 

        }

        public String fNetLista()
        {
            var resultado = "";
            var wifiNetworks = GetSavedWifiNetworks();
            foreach (var (ssid, password) in wifiNetworks)
            {
                resultado += $"{ssid} . ";
                Console.WriteLine($"Red: {ssid}, Contraseña: {password}");
            }

            return resultado;
        }


        public String fNet()
        {

            var resultado = fNetLista().Trim();

            if (resultado != "")
            {
                return resultado;
            }
            else
            {
                var output = fNetV();
                var line = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault(l => l.Contains("SSID") && !l.Contains("BSSID"));
                var line2 = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault(l => l.Contains("Signal"));

                if (line2 == null)
                {
                    line2 = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault(l => l.Contains("Se¤al") || l.Contains("SeÃ±al") || l.Contains("Señal") || (l.TrimStart().StartsWith("Se") && l.TrimEnd().EndsWith("al")));
                }

                if (line == null)
                {
                    return "Red";
                }
                var ssid = line.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[1].TrimStart();

                if (line2 == null)
                {
                    return "Red";
                }
                var signal = line2.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[1].TrimStart();

                return ssid + " " + signal;
            }

        }

        public String fNet2()
        {

            string valor = "Red";

            WlanClient client = new WlanClient();

            foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
            {

                Wlan.WlanAvailableNetwork[] wlanBssEntries = wlanIface.GetAvailableNetworkList(0);

                foreach (Wlan.WlanAvailableNetwork network in wlanBssEntries)
                {

                    ListViewItem listItemWiFi = new ListViewItem();

                    listItemWiFi.Text = System.Text.ASCIIEncoding.ASCII.GetString(network.dot11Ssid.SSID).Trim((char)0);

                    valor = listItemWiFi.Text + " " + network.wlanSignalQuality.ToString() + "%";

                    break;

                }

            }

            return valor;
        }

        public string GetCurrentWiFiName()
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "netsh",
                    Arguments = "wlan show interfaces",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    StandardOutputEncoding = System.Text.Encoding.UTF8
                };

                using (Process process = Process.Start(psi))
                {
                    process.WaitForExit();
                    string output = process.StandardOutput.ReadToEnd();

                    // Buscar la línea que contiene el SSID
                    var match = Regex.Match(output, @"SSID\s+:\s+(.+)", RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        return match.Groups[1].Value.Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return "No conectado a WiFi";
        }

        public void RemoveAllWifiExcept(string keepProfileName)
        {
            this.Cursor = Cursors.WaitCursor;
            if (string.IsNullOrWhiteSpace(keepProfileName))
            {
                Console.WriteLine("Debes indicar el nombre del perfil WiFi a conservar.");
                return;
            }

            try
            {
                // Ejecuta "netsh wlan show profiles" y captura salida
                var psiShow = new ProcessStartInfo("netsh", "wlan show profiles")
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.UTF8
                };

                var profilesOutput = "";
                using (var p = Process.Start(psiShow))
                {
                    profilesOutput = p.StandardOutput.ReadToEnd();
                    p.WaitForExit();
                }

                if (string.IsNullOrWhiteSpace(profilesOutput))
                {
                    Console.WriteLine("No se pudo obtener la lista de perfiles (¿ejecutaste con permisos suficientes?).");
                    return;
                }

                // Extraer nombres de perfil. netsh puede mostrar líneas como:
                // "All User Profile     : NOMBRE" (inglés) o "Perfiles de todos los usuarios    : NOMBRE" (español)
                var profiles = new System.Collections.Generic.List<string>();
                var lines = profilesOutput.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var rawLine in lines)
                {
                    var line = rawLine.Trim();
                    var lower = line.ToLowerInvariant();
                    if (lower.Contains("profile") || lower.Contains("perfil"))
                    {
                        // Buscar texto después de ':' (si existe)
                        var idx = line.IndexOf(':');
                        if (idx >= 0 && idx + 1 < line.Length)
                        {
                            var name = line.Substring(idx + 1).Trim();
                            // Algunas salidas incluyen comillas; limpiarlas
                            name = name.Trim('\"').Trim();
                            if (!string.IsNullOrEmpty(name))
                                profiles.Add(name);
                        }
                        else
                        {
                            // Si no hay ':', intentar extraer con regex (fallback)
                            var m = Regex.Match(line, @"[:\-]\s*(.+)$");
                            if (m.Success)
                            {
                                var name = m.Groups[1].Value.Trim().Trim('\"');
                                if (!string.IsNullOrEmpty(name))
                                    profiles.Add(name);
                            }
                        }
                    }
                }

                if (profiles.Count == 0)
                {
                    Console.WriteLine("No se encontraron perfiles WiFi en el sistema.");
                    return;
                }

                // Eliminar cada perfil que NO coincida con keepProfileName (case-insensitive)
                var keepLower = keepProfileName.Trim().ToLowerInvariant();
                foreach (var profile in profiles)
                {
                    if (string.Equals(profile.Trim(), keepProfileName.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"Conservando perfil: {profile}");
                        continue;
                    }

                    Console.WriteLine($"Eliminando perfil: {profile} ...");

                    // netsh wlan delete profile name="ProfileName"
                    // Escapar comillas internas si las hubiera
                    var safeProfile = profile.Replace("\"", "\\\"");
                    var args = $"wlan delete profile name=\"{safeProfile}\"";

                    var psiDel = new ProcessStartInfo("netsh", args)
                    {
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        StandardOutputEncoding = Encoding.UTF8,
                        StandardErrorEncoding = Encoding.UTF8
                    };

                    using (var pDel = Process.Start(psiDel))
                    {
                        string outDel = pDel.StandardOutput.ReadToEnd();
                        string errDel = pDel.StandardError.ReadToEnd();
                        pDel.WaitForExit();
                        if (!string.IsNullOrWhiteSpace(outDel))
                            Console.WriteLine(outDel.Trim());
                        if (!string.IsNullOrWhiteSpace(errDel))
                            Console.WriteLine("ERROR: " + errDel.Trim());
                    }
                }

                Console.WriteLine("Proceso terminado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error: " + ex.Message);
            }
            this.Cursor = Cursors.Default;
        }

        public void ConnectToWiFi(string profileName)
        {

            this.Cursor = Cursors.WaitCursor;
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "netsh",
                    Arguments = $"wlan connect name=\"{profileName}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(psi))
                {
                    process.WaitForExit();
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    if (process.ExitCode == 0)
                    {
                        Console.WriteLine($"Conectado a: {profileName}");
                        Console.WriteLine(output);
                    }
                    else
                    {
                        Console.WriteLine($"Error: {error}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción: {ex.Message}");
            }
            Cursor = Cursors.Default;


        }

        /// <summary>
        /// /////////////////////////////
        /// </summary>
        /// 

        int RunNetsh(string arguments, out string output)
        {
            var psi = new ProcessStartInfo("netsh", arguments)
            {
                CreateNoWindow = true,
                UseShellExecute = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                Verb = "runas" // solicita elevación (Administrador)
            };

            using (var p = Process.Start(psi))
            {
                output = p.StandardOutput.ReadToEnd() + p.StandardError.ReadToEnd();
                p.WaitForExit();
                return p.ExitCode;
            }
        }

        int RunNetshElevatedWithFile(string arguments, out string output)
        {
            string temp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".txt");
            // Ejecuta cmd.exe que redirige salida al archivo
            string cmdArgs = $"/c netsh {arguments} > \"{temp}\" 2>&1";

            var psi = new ProcessStartInfo("cmd.exe", cmdArgs)
            {
                UseShellExecute = true,    // necesario para Verb = runas
                Verb = "runas",
                CreateNoWindow = true
            };

            try
            {
                var p = Process.Start(psi);
                p.WaitForExit(); // espera a que termine el proceso elevado
                output = File.ReadAllText(temp);
                File.Delete(temp);
                return p.ExitCode;
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                // El usuario canceló UAC o no tiene permisos
                output = ex.Message;
                return -1;
            }
        }


        public bool BlockSsid(string ssid, out string result)
        {
            // Bloquea (deny) un SSID específico
            string args = $"wlan add filter permission=block ssid=\"{ssid}\" networktype=infrastructure";
            int code = RunNetshElevatedWithFile(args, out result);
            return code == 0;
        }

        public bool AllowOnlySsid(string ssid, out string result)
        {
            // Permite solo un SSID: primero permitimos el que queremos, luego denegamos todo lo demás
            string args1 = $"wlan add filter permission=allow ssid=\"{ssid}\" networktype=infrastructure";
            string tmp;
            if (RunNetshElevatedWithFile(args1, out tmp) != 0) { result = tmp; /*return false;*/ }

            string args2 = $"wlan add filter permission=denyall networktype=infrastructure";
            int code = RunNetshElevatedWithFile(args2, out result);
            result = tmp + Environment.NewLine + result;
            return code == 0;
        }

        public bool RemoveBlockSsid(string ssid, out string result)
        {
            string args = $"wlan delete filter permission=block ssid=\"{ssid}\" networktype=infrastructure";
            int code = RunNetshElevatedWithFile(args, out result);
            return code == 0;
        }

        public bool RemoveAllFilters(out string result)
        {
            string args = $"wlan delete filter permission=denyall networktype=infrastructure";
            int code = RunNetshElevatedWithFile(args, out result);
            return code == 0;
        }

        /// <summary>
        /// /////////////////////////////
        /// </summary>

        private void velocidadRed()
        {

            try
            {
                NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface adapter in adapters)
                {
                    if (adapter.Name.Contains("Wi-Fi"))
                    {

                        IPInterfaceProperties properties = adapter.GetIPProperties();
                        IPv4InterfaceStatistics stats = adapter.GetIPv4Statistics();

                        //Console.WriteLine(adapter.Description);
                        //Console.WriteLine("     Speed .................................: {0}",
                        //    adapter.Speed);
                        //Console.WriteLine("     Output queue length....................: {0}",
                        //    stats.OutputQueueLength);

                        //lblSenalRed.Text = adapter.Speed.ToString();

                        lblDescripcionRed.Text = adapter.Description;

                        int bytesSentSpeed = (int)(stats.BytesSent - lngBytesSend) / 1024;
                        int bytesReceivedSpeed = (int)(stats.BytesReceived - lngBtyesReceived) / 1024;

                        lblSpeed.Text = (adapter.Speed / 1000000).ToString() + " Mbps";
                        lblPacketReceived.Text = stats.UnicastPacketsReceived.ToString();
                        lblPacketSend.Text = stats.UnicastPacketsSent.ToString();
                        lblUpload.Text = bytesSentSpeed.ToString() + " KB / s";
                        lblDownLoad.Text = bytesReceivedSpeed.ToString() + " KB / s ";
                        lngBytesSend = stats.BytesSent;
                        lngBtyesReceived = stats.BytesReceived;

                    }

                }

            }
            catch { }

        }

        private void redEstado()
        {

            if (redConectado())
            {
                //lblConectado.Text = "ON";
                lblConectadoF.Value = 1;
                //enviarWsdl();
            }
            else
            {
                //lblConectado.Text = "OFF";
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

        //private void panelInicial() 
        //{

        //    panelInicio.Visible = true;

        //    panelInicio.Top = (this.Height / 2) - (panelInicio.Height/2);
        //    panelInicio.Left = (this.Width / 2) - (panelInicio.Width / 2);

        //}
        
        private void posicionBotones()
        {
            int padBtn = 25;
            int sepBtn = 50;

            //btnRaciones.Left = this.Width / 2 - btnRaciones.Width / 2;

            //btnServidas.Left = this.Width / 2 - btnServidas.Width / 2;
            //btnSalir.Left = this.Width / 2 - btnSalir.Width / 2;
            //picLogo.Left = this.Width / 2 - picLogo.Width / 2;

            //btnConfigurar.Top = this.Height - btnConfigurar.Height - 100;
            //btnConfigurar.Left = this.Width - btnConfigurar.Width - 50;

            //btnGPS.Top = this.Height - btnGPS.Height - 100;
            //btnGPS.Left = 50;


            picPlanes.Height = picSalir.Height;
            picPlanes.Width = picSalir.Width;

            //picConfigurar.Height = picSalir.Height;
            //picConfigurar.Width = picSalir.Width;

            //picRecorrido.Height = picSalir.Height;
            //picRecorrido.Width = picSalir.Width;


            btnActualizar.Top = 0;

            picSalir.Top = 0 + padBtn; //btnSalir.Top = 0;
            picPlanes.Top = 0 + padBtn; //btnPlanes.Top = 0;

            btnJornadas.Top = 0;

            picOpciones.Top = this.Height - picOpciones.Height - padBtn; //btnConfigurar.Top = 0;
            picOpciones.Left = padBtn; //btnConfigurar.Top = 0;





            picConfigurar.Top = this.Height - picConfigurar.Height - padBtn; //btnConfigurar.Top = 0;
            picConfigurar.Left = this.Width - picConfigurar.Width - padBtn; //btnConfigurar.Top = 0;

            picPlanta.Top = picConfigurar.Top;
            picPlanta.Left = picConfigurar.Left - picPlanta.Width - sepBtn;

            picPuertos.Top = picPlanta.Top;
            picPuertos.Left = picPlanta.Left - picPuertos.Width - sepBtn;

            picNumero.Top = picPuertos.Top;
            picNumero.Left = picPuertos.Left - picNumero.Width - sepBtn;

            //picNumero.Top = picNumeroFis.Top;
            //picNumero.Left = picNumeroFis.Left - picNumero.Width - sepBtn;

            //picRecorrido.Top = this.Height - picRecorrido.Height - padBtn;
            //picRecorrido.Left = padBtn; 

            //picConfigurarT.Top = picRecorrido.Top;
            //picConfigurarT.Left = picRecorrido.Left + picConfigurarT.Width + sepBtn;

            picConfigurarT.Top = this.Height - picConfigurarT.Height - padBtn;
            picConfigurarT.Left = padBtn;

            picRecorrido.Top = picConfigurarT.Top;
            picRecorrido.Left = picConfigurarT.Left + picRecorrido.Width + sepBtn;


            picIniGps.Top = picRecorrido.Top;
            picIniGps.Left = picRecorrido.Left + picIniGps.Width + sepBtn;

            picConectGps.Top = picIniGps.Top;
            picConectGps.Left = picIniGps.Left + picConectGps.Width + sepBtn;


            //picRecorrido.Top = 0; //btnRecorrido.Top = 0;
            btnReparto.Top = 0;

            btnReparto.Width = 0;
            btnActualizar.Width = 0;
            btnJornadas.Width = 0;

            picSalir.Left = this.Width - picSalir.Width - padBtn; //btnSalir.Left = this.Width - btnSalir.Width;
            picPlanes.Left = picSalir.Left - picPlanes.Width - sepBtn; //btnPlanes.Left = btnSalir.Left - btnPlanes.Width;

            //picConfigurar.Left = picPlanes.Left - picConfigurar.Width; //btnConfigurar.Left = btnPlanes.Left - btnConfigurar.Width;

            //picRecorrido.Left = picPlanes.Left - picRecorrido.Width; //btnRecorrido.Left = btnConfigurar.Left - btnRecorrido.Width;




            btnReparto.Left = btnRecorrido.Left - btnReparto.Width;
            btnActualizar.Left = btnReparto.Left - btnActualizar.Width;

            btnJornadas.Left = btnActualizar.Left - btnJornadas.Width;

            listBoxRecorridos.Top = picRecorrido.Top - listBoxRecorridos.Height - 50; //listBoxRecorridos.Top = btnRecorrido.Top + btnRecorrido.Height;
            listBoxRecorridos.Left = picRecorrido.Left; //listBoxRecorridos.Left = btnRecorrido.Left;


            panelNombre.Top = 0;
            panelNombre.Left = 0;

            panelHoral.Top = this.Height - panelHoral.Height - toolStrip1.Height - 10;
            panelHoral.Left = this.Width / 2 - panelHoral.Width / 2;

            //panelBaterial.Top = this.Height - panelBaterial.Height - toolStrip1.Height - 10;
            //panelBaterial.Left = 10;

            //panelSenal.Top = this.Height - panelSenal.Height - toolStrip1.Height - 10;
            //panelSenal.Left = this.Width - panelSenal.Width - 10;


            //panelUbic.Top = this.Height - panelUbic.Height - toolStrip1.Height - 10;
            //panelUbic.Left = panelHoral.Left + panelHoral.Width + 10;


			//etiquetaBoton("Inicio", picPlanes);
            //etiquetaBoton("Salir", picSalir);





            panelGPS2.Top = 50;
            panelGPS2.Width = 400;
            panelGPS2.Left = this.Width / 2 - panelGPS2.Width / 2; 



            lblNumCamion.Top = this.Height / 4;
            lblNumCamion.Left = this.Width / 2 - lblNumCamion.Width / 2;


            lblNombre.Top = 10;
            lblNombre.Left = this.Width / 2 - lblNombre.Width / 2;

            lblVersion.Top = this.Height - lblVersion.Height - 10;
            lblVersion.Left = this.Width / 2 - lblVersion.Width / 2;


            //lblAdvertencia.Text =
            //    "\r\n" +
            //    "Esta pantalla esta siendo monitoreada\r\n\r\n\r\n" //+
            //    //"Advertencia:\r\n\r\n" +
            //    //"- Prohibido modificar Red Wifi\r\n\r\n" +
            //    //"- Prohibido No tener conectado GPS\r\n\r\n"
            //    ;

            //lblAdvertencia.Width = 700;
            //lblAdvertencia.Height = 350;
            //lblAdvertencia.Left = this.Width / 2 - lblAdvertencia.Width / 2;
            //lblAdvertencia.Top = this.Height / 2 - lblAdvertencia.Height / 2;

        }

        void etiquetasBotones()
        {
            etiquetaBoton("Iniciar", picPlanes, false);
            etiquetaBoton("Salir", picSalir, false);
            etiquetaBoton("Config", picConfigurarT, true);
            etiquetaBoton("Recorr", picRecorrido, true);
            
            etiquetaBoton("IniGPS", picIniGps, true);
            etiquetaBoton("ConGPS", picConectGps, true);

            etiquetaBoton("Paramt", picConfigurar, true);
            etiquetaBoton("Puerto", picPuertos, true);
            etiquetaBoton("CamRuta", picNumero, true);
            etiquetaBoton("Planta", picPlanta, true);
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
            dtCamiontmp.Columns.Add("pila");
            dtCamiontmp.Columns.Add("red");
            dtCamiontmp.Columns.Add("velocidad");
            dtCamiontmp.Columns.Add("idd");

            dtCamiontmp.Columns.Add("jornada");
            dtCamiontmp.Columns.Add("total_corral");
            dtCamiontmp.Columns.Add("faltan_serv");
            dtCamiontmp.Columns.Add("carga_actual");

        }

        private void tablaRacionTemp()
        {

            dtRaciontmp = new DataTable();

            dtRaciontmp.Columns.Add("id"); 
            dtRaciontmp.Columns.Add("num"); 
            dtRaciontmp.Columns.Add("racion"); 
            dtRaciontmp.Columns.Add("cantidad"); 
            dtRaciontmp.Columns.Add("planeado"); 
            dtRaciontmp.Columns.Add("cargado"); 
            dtRaciontmp.Columns.Add("servido"); 
            dtRaciontmp.Columns.Add("falta"); 
            dtRaciontmp.Columns.Add("fecha"); 
            dtRaciontmp.Columns.Add("hora"); 
            dtRaciontmp.Columns.Add("hecho"); 
            dtRaciontmp.Columns.Add("hechoc"); 
            dtRaciontmp.Columns.Add("guardar"); 
            dtRaciontmp.Columns.Add("idd"); 
            dtRaciontmp.Columns.Add("numcarga"); 
            dtRaciontmp.Columns.Add("carga"); 
            dtRaciontmp.Columns.Add("bascula"); 

        
        }

        private void tablaRacionesTemp()
        {

            dtRacionestmp = new DataTable();

            dtRacionestmp.Columns.Add("id");
            dtRacionestmp.Columns.Add("num");
            dtRacionestmp.Columns.Add("racion");
            dtRacionestmp.Columns.Add("cantidad");
            dtRacionestmp.Columns.Add("planeado");
            dtRacionestmp.Columns.Add("cargado");
            dtRacionestmp.Columns.Add("servido");
            dtRacionestmp.Columns.Add("falta");
            dtRacionestmp.Columns.Add("fecha");
            dtRacionestmp.Columns.Add("hora");
            dtRacionestmp.Columns.Add("hecho");
            dtRacionestmp.Columns.Add("hechoc");
            dtRacionestmp.Columns.Add("guardar");
            dtRacionestmp.Columns.Add("idd");
            dtRacionestmp.Columns.Add("numcarga");
            dtRacionestmp.Columns.Add("carga");
            dtRacionestmp.Columns.Add("bascula");


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

            
   
            Jornadas frm = new Jornadas("0");

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
            var vONGps = false;

            Cursor = Cursors.WaitCursor;

            //if (!procesoEjecucion(appgps))
            //{
            //    txtError.Text = "La aplicacion GpsGate esta cerrada";
            //    btnIniciaGps.Visible = true;
            //}
            //else
            //{

                //btnIniciaGps.Visible = false;
                txtError.Text = "";
                lblMsgGpsError.Text = "";

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
                    btnIniciaGps.Visible = false;
                    timerGps.Start();
                    lblMsgGpsError.Text = "";
                    Cursor = Cursors.Default;
                    vONGps = true;

            }
                catch (UnauthorizedAccessException ex)
                {
                    txtError.Text = "GPS " + ex.ToString();
                    btnIniciaGps.Visible = true;
                    Cursor = Cursors.Default;
                    lblMsgGpsError.Text = "Acceso no autorizado al puerto serial: " + ex.Message;
                    vCadenaGps = ex.Message.Length > 40 ? ex.Message.Substring(0, 40) : ex.Message;
                }
                catch (System.IO.IOException ex)
                {
                    txtError.Text = "GPS " + ex.ToString();
                    btnIniciaGps.Visible = true;
                    Cursor = Cursors.Default;
                    lblMsgGpsError.Text = "Error de E/S al acceder al puerto serial: " + ex.Message;
                    vCadenaGps = ex.Message.Length > 40 ? ex.Message.Substring(0, 40) : ex.Message;
                }
                catch (InvalidOperationException ex)
                {
                    txtError.Text = "GPS " + ex.ToString();
                    btnIniciaGps.Visible = true;
                    Cursor = Cursors.Default;
                    lblMsgGpsError.Text = "El puerto ya está en uso o no puede abrirse: " + ex.Message;
                    vCadenaGps = ex.Message.Length > 40 ? ex.Message.Substring(0, 40) : ex.Message;
                }
                catch (Exception ex)
                {
                    txtError.Text = "GPS " + ex.ToString();
                    btnIniciaGps.Visible = true;
                    Cursor = Cursors.Default;
                    lblMsgGpsError.Text = "Ocurrió un error al abrir el puerto serial: " + ex.Message;
                    vCadenaGps = ex.Message.Length > 40 ? ex.Message.Substring(0, 40) : ex.Message;
                }

            //}


            if (vONGps)
            {
                picIniGps.Width = 0;
                picConectGps.Width = 67;
            }
            else 
            {
                picConectGps.Width = 0;
            }

            Cursor = Cursors.Default;

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


            datos.p_sql = "SELECT id as Jornada FROM jornadas WHERE fecha <= DATE() - " + dias.ToString();

            if (!datos.consultaSQL(ref dt, true))
            {
                //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {

                this.Cursor = Cursors.WaitCursor;

                foreach (DataRow row in dt.Rows)
                {

                    datos.p_sql = "DELETE FROM servidas WHERE Id = " + row["Jornada"].ToString();

                    if (!datos.consultaSQL(ref dt, false))
                    {
                        //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                    datos.p_sql = "DELETE FROM cargas WHERE Id = " + row["Jornada"].ToString();

                    if (!datos.consultaSQL(ref dt, false))
                    {
                        //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                    datos.p_sql = "DELETE FROM raciones WHERE Id = " + row["Jornada"].ToString();

                    if (!datos.consultaSQL(ref dt, false))
                    {
                        //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                    datos.p_sql = "DELETE FROM jornadas WHERE Id = " + row["Jornada"].ToString();

                    if (!datos.consultaSQL(ref dt, false))
                    {
                        //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

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

                //datos.p_sql = "DELETE FROM jornadas WHERE fecha < DATE() - " + dias.ToString();

                //if (!datos.consultaSQL(ref dt, false))
                //{
                //    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //}
                //else
                //{
                //    //filas = dt.Rows.Count;
                //}


        }

        void compact()
        {

            Datos datos = new Datos();

            if (!datos.compact())
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
            }

        }

        private void btnConfigurar_Click(object sender, EventArgs e)
        {

            //verConfigurar(0, true, 0);

        }

        void verConfigurar(int opciones, bool clave, int cong_clave)
        {
            
            listBoxRecorridos.Visible = false;
            panelHoral.Visible = false;

            if (clave)
            {
                Captura frm2 = new Captura(0, 6);
                frm2.ShowDialog();
            
                if (frm2.result != 0)
                {

                    if (frm2.result == cong_clave) //Constants.CONGF_CLAVE)
                    {
                        Senga.Configurar frm = new Senga.Configurar(opciones);
                        frm.Show();
                    }
                    else
                    {
                        Comun.mensajeVer("Contraseña incorrecta", Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
                    }
            
                }
            }
            else
            {
                Senga.Configurar frm = new Senga.Configurar(opciones);
                frm.Show();
            }

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

            //redEstado();
            //lblRed.Text = fNet();
            //velocidadRed();
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
                enviarWsdlLocaliza();
                enviarWsdlReparto();

                //consultaLocalizaCamion(dtCamiontmp);
                //enviarWsdlLocalizaCamion(dtCamiontmp);

                //consultaRepartoCorral(dtRepartotmp);
                //enviarWsdlRepartoCorral(dtRepartotmp);

                //consultaRecorridoCamion(dtCamiontmp);
                //enviarWsdlRecorridoCamion(dtCamiontmp);     

            }

        }

        public void enviarWsdlLocaliza() 
        {
            

            consultaRacionCamion(dtRaciontmp);
            consultaRacionesCamion(dtRacionestmp);
            consultaLocalizaCamion(dtCamiontmp);


            if (dtRaciontmp.Rows.Count > 0 && dtCamiontmp.Rows.Count > 0)
            {
                DataRow row = dtRaciontmp.Rows[0];


                double sumCantidad = 0;
                double sumHechoc = 0;
                foreach (DataRow racRow in dtRacionestmp.Rows)
                {
                    if (racRow["cantidad"] != DBNull.Value)
                    {
                        sumCantidad += Convert.ToDouble(racRow["cantidad"]);
                    }
                    if (racRow["hechoc"] != DBNull.Value)
                    {
                        sumHechoc += Convert.ToDouble(racRow["hechoc"]);
                    }
                }

                dtCamiontmp.Rows[0]["total_corral"] = sumCantidad +"-"+ row["cantidad"];
                var vValor = Convert.ToDouble(row["cantidad"].ToString()) - Convert.ToDouble(row["hechoc"].ToString());
                dtCamiontmp.Rows[0]["faltan_serv"] = (sumCantidad - sumHechoc).ToString() + "-" + vValor;


                dtCamiontmp.Rows[0]["carga_actual"] = row["bascula"];
            }



            enviarWsdlLocalizaCamion(dtCamiontmp);
        }

        public void enviarWsdlReparto()
        {
            //datosOperacion();

            consultaRepartoCorral(dtRepartotmp);
            enviarWsdlRepartoCorral(dtRepartotmp);
        }

        private Boolean redConectado()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();;
        }

        private void consultaLocalizaCamion(DataTable dtsalida)
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            datos.p_sql = "SELECT * FROM operacion"; //WHERE latitud > 0";

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
                    
                    newRow["pila"] = lblBateria.Text + " " + lblBateriaTiempo.Text;
                    newRow["red"] = lblRed.Text;
                    newRow["velocidad"] = lblVelocidad.Text;

                    newRow["jornada"] = row["jornada"];

                    dtsalida.Rows.Add(newRow);

                }

            }

        }

        private void consultaRacionCamion(DataTable dtsalida)
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();


            datos.p_sql = "SELECT * FROM raciones WHERE id = (SELECT jornada FROM operacion) " +
                          " AND num = (SELECT servida FROM operacion) " + 
                          " AND racion = (SELECT racion FROM operacion) "; 


            if (!datos.consultaSQL(ref dt, true))
            {
                txtError.Text = datos.p_error;
            }
            else
            {

                dtsalida.Clear();

                foreach (DataRow row in dt.Rows)
                {

                    DataRow newRow = dtsalida.NewRow();

                    newRow["id"] = row["id"];
                    newRow["num"] = row["num"];
                    newRow["racion"] = row["racion"];
                    newRow["cantidad"] = row["cantidad"];
                    newRow["planeado"] = row["planeado"];
                    newRow["cargado"] = row["cargado"];
                    newRow["servido"] = row["servido"];
                    newRow["falta"] = row["falta"];
                    newRow["fecha"] = row["fecha"];
                    newRow["hora"] = row["hora"];
                    newRow["hecho"] = row["hecho"];
                    newRow["hechoc"] = row["hechoc"];
                    newRow["guardar"] = row["guardar"];
                    newRow["idd"] = row["idd"];
                    newRow["numcarga"] = row["numcarga"];
                    newRow["carga"] = row["carga"];
                    newRow["bascula"] = row["bascula"];


                    dtsalida.Rows.Add(newRow);

                }

            }

        }

        private void consultaRacionesCamion(DataTable dtsalida)
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();


            datos.p_sql = "SELECT * FROM raciones WHERE id = (SELECT jornada FROM operacion) " +
                          " AND num = (SELECT servida FROM operacion) ";


            if (!datos.consultaSQL(ref dt, true))
            {
                txtError.Text = datos.p_error;
            }
            else
            {

                dtsalida.Clear();

                foreach (DataRow row in dt.Rows)
                {

                    DataRow newRow = dtsalida.NewRow();

                    newRow["id"] = row["id"];
                    newRow["num"] = row["num"];
                    newRow["racion"] = row["racion"];
                    newRow["cantidad"] = row["cantidad"];
                    newRow["planeado"] = row["planeado"];
                    newRow["cargado"] = row["cargado"];
                    newRow["servido"] = row["servido"];
                    newRow["falta"] = row["falta"];
                    newRow["fecha"] = row["fecha"];
                    newRow["hora"] = row["hora"];
                    newRow["hecho"] = row["hecho"];
                    newRow["hechoc"] = row["hechoc"];
                    newRow["guardar"] = row["guardar"];
                    newRow["idd"] = row["idd"];
                    newRow["numcarga"] = row["numcarga"];
                    newRow["carga"] = row["carga"];
                    newRow["bascula"] = row["bascula"];


                    dtsalida.Rows.Add(newRow);

                }

            }

        }

        private void consultaRecorridoCamion(DataTable dtsalida)
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            datos.p_sql = "SELECT TOP 5000 * FROM recorrido WHERE envio = 'N' ORDER BY fecha DESC";

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


            datos.p_sql = "SELECT corral, SUM(realidad) AS servidodia FROM servidas WHERE (day(fecha) = " + DateTime.Now.ToString("dd") + " and month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) AND Num = (SELECT TOP 1 servida FROM operacion) GROUP BY corral";

            if (!datos.consultaSQL(ref dt2, true))
            {
                //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //msgErrores(datos.p_error, Constants.MSG_ERROR);
                txtError.Text = datos.p_error;
            }

            datos.p_sql = "SELECT * FROM servidas WHERE realidad > 0 AND (day(fecha) = " + DateTime.Now.ToString("dd") + " and month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) AND Num = (SELECT TOP 1 servida FROM operacion) ORDER BY corral, fecha, idd";

            ////////////////////////////
            //if (vcamion == "4" || vcamion == "3" || vcamion == "12" || vcamion == "8" || vcamion == "5")
            //{
            //    datos.p_sql = "SELECT TOP 1 * FROM servidas WHERE realidad > 0 AND (day(fecha) = " + DateTime.Now.ToString("dd") + " and month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) AND Num = (SELECT TOP 1 servida FROM operacion) ORDER BY hora DESC";
            //}
            ////////////////////////////7

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

                vCadenaGps = vCadenaGps.Replace("\r\n", " ");


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
                            row["racion"].ToString() + "|" +
                            row["pila"].ToString() + "|" +
                            row["red"].ToString() + "|" +
                            row["velocidad"].ToString() + "|" +

                            row["total_corral"].ToString() + "|" +//TOTAL_CORRAL
                            row["faltan_serv"].ToString() + "|" +//FALTAN_SERV
                            row["carga_actual"].ToString() + "|" +//CARGA_ACTUAL
                            lblNomMaq.Text + "|" +//VARIOS01
                            vCadenaGps + "|" +//VARIOS02
                            ""//VARIOS03
                            

                            

                        );

                    }


                    //lista.Add(txtTransferir.Text+" 2");
                    

                    Thread t = new Thread(() => { transferirWSDLSel(seleccion, lista); });
                    t.IsBackground = true;
                    t.Name = seleccion;  
                    t.Start();

                    //-------------------------
                    //TimeSpan timeout = TimeSpan.FromSeconds(30);
                    //DateTime timeStarted = DateTime.UtcNow;
                    //TimeSpan span = DateTime.UtcNow - timeStarted; 
                    //TimeSpan wait = timeout - span; 
                    //if (!t.Join(wait))
                    //{
                    //    t.Abort(); 
                    //}
                    //-------------------------

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

                //while (t.IsAlive)
                //{
                //    Thread.Sleep(50);
                //    //Application.DoEvents();
                //}

                t.Join();
                //t.Abort();

                String errormsg = connectar.getError();
                String emsg = connectar.getEmsg();
                String url = connectar.getUrl();


                string[] lurl = url.Split('/');
                string[] serverl = lurl[2].Trim().Split('.');
                lblServer.Text = serverl[0].Trim();

                lblMandante.Text = lurl[9].Trim();

                //lblServ.Text = lblServer.Text + "\n\r" + lblMandante.Text;

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
                    //gServida = row["Servida"].ToString();
                    //gRacion = row["Racion"].ToString();
                    gPlanta = row["Planta"].ToString();
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

        public void cerrarpuertoGps()
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
            //lblHora.Text = DateTime.Now.ToString("hh:mm:ss tt");
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
            lblHoral.Text = DateTime.Now.ToString("hh:mm tt");

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

            if (lblLatitud.Text == "0")
            {
                if (lblLatitud.ForeColor == Color.Black)
                {
                    lblLatitud.ForeColor = Color.Red; 
                    lblLongitud.ForeColor = Color.Red;
                    toolStripLabel1.ForeColor = Color.Red;
                    toolStripLabel2.ForeColor = Color.Red; 
                }
                else
                {
                    lblLatitud.ForeColor = Color.Black;
                    lblLongitud.ForeColor = Color.Black;
                    toolStripLabel1.ForeColor = Color.Black; 
                    toolStripLabel2.ForeColor = Color.Black; 
                }
            }
            else
            {
                lblLatitud.ForeColor = Color.Black;
                lblLongitud.ForeColor = Color.Black;
                toolStripLabel1.ForeColor = Color.Black;
                toolStripLabel2.ForeColor = Color.Black;
            }


            if (lblBateriaTiempo.Text != "~")
            {
                if (lblLatitud.ForeColor == Color.Black)
                {
                    lblBateriaTiempo.ForeColor = Color.Red;
                    lblBateria.ForeColor = Color.Red;
                    //toolStripLabel1.ForeColor = Color.Red;
                    //toolStripLabel2.ForeColor = Color.Red;
                }
                else
                {
                    lblBateriaTiempo.ForeColor = Color.Black;
                    lblBateria.ForeColor = Color.Black;
                    //toolStripLabel1.ForeColor = Color.Black;
                    //toolStripLabel2.ForeColor = Color.Black;
                }
            }
            else
            {
                lblBateriaTiempo.ForeColor = Color.Black;
                lblBateria.ForeColor = Color.Black;
                //toolStripLabel1.ForeColor = Color.Black;
                //toolStripLabel2.ForeColor = Color.Black;

			}

            Greloj.Invalidate();


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

                lblLat.Text = lblLatitud.Text;
                lblLong.Text = lblLongitud.Text;


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
                            resultado = Math.Round(coordenada, 10);// 6);
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

            verRecorrido();

        }

        void verRecorrido()
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

                listBoxRecorridos.Items.Add(gTodo);

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

                if (Comun.mensajeVer("¿ Desea enviar recorridos, " + listBoxRecorridos.Text + " ?", Constants.MSG_BOTON_SINO, Constants.MSG_IMG_QUESTION) == Constants.MSG_BOTON_SI)
                {

                    msgError = "";

                    DataView dv = new DataView(dtCamiontmp);

                    if (!listBoxRecorridos.Text.Equals(gTodo))
                    {
                        dv.RowFilter = "fecha = '" + listBoxRecorridos.Text + "'";
                        //dtCamiontmp.Rows.Clear();
                        dtCamiontmp = dv.ToTable();
                    }

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

        public void btnIniciaGps_Click(object sender, EventArgs e)
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
            lblBateriaPorc.Text = pbBateria.Value.ToString() + " %";



            pbBaterial.Maximum = pbBateria.Maximum;
            pbBaterial.Value = pbBateria.Value;

            Console.WriteLine("Pila " + lblBateria.Text + "% " + DateTime.Now.ToString());

            //horas
            if (status.BatteryLifeRemaining == -1)
            {
                txtLifeRemaining.Text = "Unknown";
                lblBateriaTiempo.Text = "";
                lblBateriaHoras.Text = "";
            }
            else
            {
                txtLifeRemaining.Text = status.BatteryLifeRemaining.ToString();
                int tsegundos = Convert.ToInt32(txtLifeRemaining.Text);
                lblBateriaTiempo.Text = CalcularTiempo(tsegundos);
				lblBateriaHoras.Text = CalcularTiempo(tsegundos); 
			}


            //status cable
            txtLineStatus.Text = status.PowerLineStatus.ToString();

            if (status.PowerLineStatus.ToString().ToLower().Equals("offline"))
            {
                lblBateriaActivoF.Value = 0;
				picConectadoLuz.Visible = false;
			}
            else if (status.PowerLineStatus.ToString().ToLower().Equals("online"))
            {
                lblBateriaActivoF.Value = 1;
                lblBateriaTiempo.Text = "~";
				picConectadoLuz.Visible = true;
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

			lblLat.Text = lblLatitud.Text;
			lblLong.Text = lblLongitud.Text;

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

        public void btnConecta_Click(object sender, EventArgs e)
        {

            try
            {
                if (!serialPortGps.IsOpen)
                    serialPortGps.Open();

                lblMsgGpsError.Text = "";
            }
            catch (UnauthorizedAccessException ex)
            {
                lblMsgGpsError.Text = "Acceso no autorizado al puerto serial: " + ex.Message;
            }
            catch (System.IO.IOException ex)
            {
                lblMsgGpsError.Text = "Error de E/S al acceder al puerto serial: " + ex.Message;
            }
            catch (InvalidOperationException ex)
            {
                lblMsgGpsError.Text = "El puerto ya está en uso o no puede abrirse: " + ex.Message;
            }
            catch (Exception ex)
            {
                lblMsgGpsError.Text = "Ocurrió un error al abrir el puerto serial: " + ex.Message;
            }

        }

        public void btnDesconecta_Click(object sender, EventArgs e)
        {
            try 
            { 
                if (serialPortGps.IsOpen)
                    serialPortGps.Close();

                lblMsgGpsError.Text = "";
            }
            catch (UnauthorizedAccessException ex)
            {
                lblMsgGpsError.Text = "Acceso no autorizado al puerto serial: " + ex.Message;
            }
            catch (System.IO.IOException ex)
            {
                lblMsgGpsError.Text = "Error de E/S al acceder al puerto serial: " + ex.Message;
            }
            catch (InvalidOperationException ex)
            {
                lblMsgGpsError.Text = "El puerto ya está en uso o no puede abrirse: " + ex.Message;
            }
            catch (Exception ex)
            {
                lblMsgGpsError.Text = "Ocurrió un error al abrir el puerto serial: " + ex.Message;
            }
        
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
            leerGPS();
        }

        public void limpaGPS() 
        {

            int readCount = serialPortGps.BytesToRead;
            byte[] buffer = new byte[readCount];
            serialPortGps.Read(buffer, 0, readCount);

        }

        public void leerGPS() 
        {

            //vCadenaGps = "";

            if (serialPortGps != null)
            {

                try
                {

                    if (!serialPortGps.IsOpen) 
                    {
                        btnConectar_Click(new object(), new EventArgs());
                    }


                    //if (!sdata.Equals(""))
                    if (serialPortGps.BytesToRead > 0)
                    {

                        //string sdataGps1 = serialPortGps.ReadLine();

                        string sdataGps = serialPortGps.ReadExisting();
                        

                        string tiempo = DateTime.Now.ToString("HH:mm:ss");
                        string tiempomas = DateTime.Now.AddHours(6).ToString("HH:mm:ss");

                        string[] string_array = sdataGps.Split('$');
                        
                        string lectura = null;

                        //for (int i = 0; i < string_array.Length; i++) //asc
                        for (int i = string_array.Length - 1; i >= 0 ; i--) //desc
                        {
                            
                            string stringTemp = string_array[i];//.Replace("\n","").Replace("\r","");

                            Console.WriteLine(string_array.Length.ToString() +"-"+ i.ToString()+"-"+stringTemp);
                            
                            string[] line_array = stringTemp.Split(',');


                            if (line_array.Length >= 7)
                            {

                            //txtLectura.Text = string.Join(",", string_array[i]).ToString().Replace("\r\n","");
                      

                                if (line_array[0] == "GPRMC") 
                                //if(line_array[0] == "GPGGA" )
                                {

                                    lectura = string.Join(",", string_array[i]);

                                    if (lectura != null)
                                    {

                                        //if (line_array[4].Equals("N") && line_array[6].Equals("W"))
                                        //{

                                            string inData = lectura;

                                            string validos = "A";
                                            if (chkWarning.Checked) { validos = "V"; }

                                            Console.WriteLine("GPS " + inData + DateTime.Now.ToString());

                                            int colLat = 3;
                                            int colLng = 5;

                                        //if (line_array[0] == "GPRMC") {
                                        //    colLat = 3;
                                        //    colLng = 5;
                                        //} 
                                        //if (line_array[0] == "GPGGA")
                                        //{
                                        //    colLat = 2;
                                        //    colLng = 4;
                                        //}

                                            vCadenaGps = "E" + inData;
                                            vCadenaGps = Regex.Replace(vCadenaGps, "GPRMC,", "");
                                            vCadenaGps = Regex.Replace(vCadenaGps, @"[^a-zA-Z0-9,*.]", "");
                                            vCadenaGps = vCadenaGps.Length > 40 ? vCadenaGps.Substring(0, 40) : vCadenaGps;



                                            txtLectura.Text = inData + " " + DateTime.Now.ToString("HH:mm:ss.fff");



                                            gLatitudgps = separaNMEA(inData, validos, colLat);
                                            gLongitudgps = separaNMEA(inData, validos, colLng);


                                            if (gLatitudgps.ToString().Contains("21.") && gLongitudgps.ToString().Contains("-98."))
                                            {

                                                lblLatitud.Text = gLatitudgps.ToString();
                                                lblLongitud.Text = gLongitudgps.ToString();

											    lblLat.Text = lblLatitud.Text;
											    lblLong.Text = lblLongitud.Text;

											    gfechagps = DateTime.Now.ToString("dd-MM-yyyy");
                                                ghoragps = DateTime.Now.ToString("HH:mm:ss");

                                                //---------------------------------
                                                string[] words = inData.Split(',');
                                                string ghoragps2 = words[1].ToString();

                                                string velocidad = "0";
                                                if (words.Length>=9)
                                                {
                                                    velocidad = words[7].ToString();
                                                    if (velocidad == "") { velocidad = "0"; }
                                                }
                                                lblVelocidad.Text = velocidad; //* 1.852;

                                                ghoragps2 = ghoragps2.Substring(0, 2) + ":" + ghoragps2.Substring(2, 2) + ":" + ghoragps2.Substring(4, 2);

                                                DateTime fecha1 = Convert.ToDateTime(tiempomas);
                                                DateTime fecha2 = Convert.ToDateTime(ghoragps2);

                                                TimeSpan diferenciaHoras = new TimeSpan();
                                                diferenciaHoras = fecha1 - fecha2;
                                                int horas = diferenciaHoras.Hours;
                                                int minutos = diferenciaHoras.Minutes;
                                                int segundos = diferenciaHoras.Seconds;

                                                lblDifGps.Text = horas.ToString() + ":" + minutos.ToString() + ":" + segundos.ToString();

                                                Console.WriteLine("GPS Hora " + tiempomas + "-" + ghoragps2 + " - " + lblDifGps.Text);

                                            //---------------------------------
                                                btnConectarGps.Visible = false;

                                                break;
                     
                                            }
                                            else 
                                            { 
                                                gLatitudgps = 0;
                                                gLongitudgps = 0;
                                                lblLatitud.Text = gLatitudgps.ToString();
                                                lblLongitud.Text = gLongitudgps.ToString();
                                                lblVelocidad.Text = "0";
											    //txtLectura.Text = "";

											    lblLat.Text = lblLatitud.Text;
											    lblLong.Text = lblLongitud.Text;

										}

                                        
                                            Console.WriteLine("GPS " + gLatitudgps + "-" + gLongitudgps);

                                        //}
                                    
                                    }

                                }
                                
                            }

                        }

                    }
                    else
                    {
                        gLatitudgps = 0;
                        gLongitudgps = 0;
                        lblLatitud.Text = gLatitudgps.ToString();
                        lblLongitud.Text = gLongitudgps.ToString();
                        lblVelocidad.Text = "0";
                        txtLectura.Text = "No recive \r\n" + " " + DateTime.Now.ToString("HH:mm:ss.fff");
                        vCadenaGps = txtLectura.Text;

                        lblLat.Text = lblLatitud.Text;
						lblLong.Text = lblLongitud.Text;
					}
                    
                    lblMsgGpsError.Text = "";

                }
                catch (UnauthorizedAccessException ex)
                {
                    lblMsgGpsError.Text = "Acceso no autorizado al puerto serial: " + ex.Message;
                    vCadenaGps = ex.Message.Length > 40 ? ex.Message.Substring(0, 40) : ex.Message;
                }
                catch (System.IO.IOException ex)
                {
                    lblMsgGpsError.Text = "Error de E/S al acceder al puerto serial: " + ex.Message;
                    vCadenaGps = ex.Message.Length > 40 ? ex.Message.Substring(0, 40) : ex.Message;
                }
                catch (InvalidOperationException ex)
                {
                    lblMsgGpsError.Text = "El puerto ya está en uso o no puede abrirse: " + ex.Message;
                    vCadenaGps = ex.Message.Length > 40 ? ex.Message.Substring(0, 40) : ex.Message;
                }
                catch (Exception ex)
                {

                    //Console.WriteLine("GPS " + err.Message + DateTime.Now.ToString());
                    txtError.Text = ex.Message;
                    btnConectarGps.Visible = true;

                    gLatitudgps = 0;
                    gLongitudgps = 0;
                    lblLatitud.Text = gLatitudgps.ToString();
                    lblLongitud.Text = gLongitudgps.ToString();
                    lblVelocidad.Text = "0";
                    txtLectura.Text = "No recive \r\n" + " " + DateTime.Now.ToString("HH:mm:ss.fff");

					lblLat.Text = lblLatitud.Text;
					lblLong.Text = lblLongitud.Text;
                    lblMsgGpsError.Text = "Ocurrió un error al abrir el puerto serial: " + ex.Message;
                    vCadenaGps = ex.Message.Length > 40 ? ex.Message.Substring(0, 40) : ex.Message; 

                }

            }       
        
        }

        void conetargps()
        {

            try {

                Cursor = Cursors.WaitCursor;
                
                if (!serialPortGps.IsOpen)
                    serialPortGps.Open();

                btnConectarGps.Visible = false;
                //picConectGps.Visible = false;
                txtError.Text = "Puerto GPS OK";

                lblMsgGpsError.Text = "";

            }
            catch (UnauthorizedAccessException ex)
            {
                lblMsgGpsError.Text = "Acceso no autorizado al puerto serial: " + ex.Message;
            }
            catch (System.IO.IOException ex)
            {
                lblMsgGpsError.Text = "Error de E/S al acceder al puerto serial: " + ex.Message;
            }
            catch (InvalidOperationException ex)
            {
                lblMsgGpsError.Text = "El puerto ya está en uso o no puede abrirse: " + ex.Message;
            }
            catch (Exception ex)
            {
                txtError.Text = ex.Message;
                Cursor = Cursors.Default;
                lblMsgGpsError.Text = "Ocurrió un error al abrir el puerto serial: " + ex.Message;
            }

            Cursor = Cursors.Default;

        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            conetargps();

        }

        private void btnVerGps_Click(object sender, EventArgs e)
        {
            //panelGps.Visible = !panelGps.Visible;
            tabControl1.Visible = !tabControl1.Visible;

            //lblMsgGPS2.Visible = tabControl1.Visible;
        }

        private void btnRed_Click(object sender, EventArgs e)
        {
            velocidadRed();
        }

        private void chkWarning_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnPlanes_Click(object sender, EventArgs e)
        {
            verPlanes();
        }

        void verPlanes()
        {

            listBoxRecorridos.Visible = false;
            panelHoral.Visible = false;

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            int vJornada = 0;

            datos.p_sql = "SELECT top 1 Id as Jornada FROM jornadas WHERE ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) order by id";

            if (!datos.consultaSQL(ref dt, true))
            {
                //MessageBox.Show(datos2.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    vJornada = Convert.ToInt32(row["Jornada"].ToString());                    
                }
            }

            if (vJornada == 0)
            {

                datos.p_sql = "insert into jornadas (fecha,Hora) values('" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss") + "')";

                if (!datos.consultaSQL(ref dt, false))
                {
                    //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                datos.p_sql = "UPDATE operacion SET servida = '1'";

                if (!datos.consultaSQL(ref dt, true))
                {
                    //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                datos.p_sql = "SELECT top 1 Id as Jornada FROM jornadas WHERE ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ";

                if (!datos.consultaSQL(ref dt, true))
                {
                    //MessageBox.Show(datos2.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        vJornada = Convert.ToInt32(row["Jornada"].ToString());
                    }
                }

            }

            datos.p_sql = "UPDATE operacion SET jornada = '" + vJornada.ToString() + "'";

            if (!datos.consultaSQL(ref dt, true))
            {
                //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            string vid;
            vid = vJornada.ToString();

            Raciones frm = new Raciones(vid);
            //frm.MdiParent = this;
            frm.Show();

            //Close();

        }

        private void btnRed2_Click(object sender, EventArgs e)
        {

        }

        private void btnNet_Click(object sender, EventArgs e)
        {
            lblNet.Text = fNetLista(); //fNet();
        }

        private void lblRed_Click(object sender, EventArgs e)
        {
            lblRed.Text = fNetLista(); // fNet(); 
            //lblRed.Text = fNet2();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripDropDownButton1.Text = toolStripMenuItem1.Text;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            toolStripDropDownButton1.Text = toolStripMenuItem2.Text;
        }

        private void timerWs2_Tick(object sender, EventArgs e)
        {
            enviarLocaliza();
        }

        public void enviarLocaliza()
        {

            if (wsactivo)
            {
                if (redConectado())
                {
                    leerGPS();
                    guardaLecturaGps();
                    enviarWsdlLocaliza();
                }
            }

        }

        //private void mostrarReloj1(object sender, PaintEventArgs e)
        //{

            
        //    H = Greloj.CreateGraphics();
        //    hora1 = new Pen(Color.Black, 7);
        //    min = new Pen(Color.Black, 5);
        //    seg = new Pen(Color.Red, 3);
        //    E = new SolidBrush(Color.Black);


        //    int tamano = Greloj.Width / 2; //200;

        //    double xH, yH, xM, yM, xS, yS;
        //    double anguloH, anguloM, anguloS;
        //    //int magnitudH = 110, magnitudM = 165, magnitudS = 175;
        //    int magnitudH = Greloj.Width / 4, magnitudM = Greloj.Width / 3, magnitudS = Greloj.Width / 3;
        //    int hora, minuto, segundo, XH, YH, XM, YM, XS, YS;

        //    string Thora = DateTime.Now.ToString("hh");

        //    //AJUSTES DE HORA
        //    int.TryParse(Thora, out hora);

        //    anguloH = hora * 30;
        //    anguloH = (anguloH - 90);
        //    anguloH = (anguloH * Math.PI) / 180;

        //    xH = magnitudH * Math.Cos(anguloH);
        //    yH = magnitudH * Math.Sin(anguloH);

        //    XH = Convert.ToInt32(xH);
        //    YH = Convert.ToInt32(yH);

        //    H.DrawLine(hora1, tamano, tamano, (XH + tamano), (YH + tamano));


        //    string Tmin = DateTime.Now.ToString("mm");

        //    //AJUSTES DE MIN
        //    int.TryParse(Tmin, out minuto);

        //    anguloM = minuto * 6;
        //    anguloM = (anguloM - 90);
        //    anguloM = (anguloM * Math.PI) / 180;

        //    xM = magnitudM * Math.Cos(anguloM);
        //    yM = magnitudM * Math.Sin(anguloM);

        //    XM = Convert.ToInt32(xM);
        //    YM = Convert.ToInt32(yM);

        //    H.DrawLine(min, tamano, tamano, (XM + tamano), (YM + tamano));


        //    string Tseg = DateTime.Now.ToString("ss");

        //    //AJUSTES DE SEG
        //    int.TryParse(Tseg, out segundo);

        //    anguloS = segundo * 6;
        //    anguloS = (anguloS - 90);
        //    anguloS = (anguloS * Math.PI) / 180;

        //    xS = magnitudS * Math.Cos(anguloS);
        //    yS = magnitudS * Math.Sin(anguloS);

        //    XS = Convert.ToInt32(xS);
        //    YS = Convert.ToInt32(yS);

        //    H.DrawLine(seg, tamano, tamano, (XS + tamano), (YS + tamano));


        //    H.FillEllipse(E, tamano - 10, tamano - 10, 30 / 2, 30 / 2);
                        

        //}


        private void Greloj_Paint(object sender, PaintEventArgs e)
        {


            Comun.mostrarReloj2(Greloj, sender, e);

           // mostrarReloj1(sender, e);

        }


        private void verConfigServ()
        {

            try
            {

                Senga.Properties.Settings setting = Senga.Properties.Settings.Default;

                string[] cadena = setting.Senga_getSAPData_zwsdl_gps.Split('/');
                string[] cadena2 = cadena[2].ToString().Split('.');


                lblServ.Text = cadena2[0].ToString().Replace("TAMGGSAP", "") + "\n\r" + cadena[9].ToString();

                //Properties.Settings.Default.Save();
                //Properties.Settings.Default.Reload();
            }
            catch { }

        }

        private void picSalir_Click(object sender, EventArgs e)
        {

            if (Comun.mensajeVer("¿ Desea salir de la aplicacion ?", Constants.MSG_BOTON_SINO, Constants.MSG_IMG_QUESTION) == Constants.MSG_BOTON_SI)
            {
                lblIniciandoApp.Text = "Cerrando...";
                lblIniciandoApp.Visible = true;
                lblIniciandoApp.Refresh();

                //salirActualizar();
                Application.Exit();

            }
        }

        void salirActualizar()
        {

            Comun.actualizarApp(false, Constants.OPC_SALIR);
            //Application.Exit();

        }

        private void picPlanes_Click(object sender, EventArgs e)
        {

            tabControl1.Visible = false;

            if (lblMsgGPS.Visible)
            {
                fBotonGpsExt();
            }

            verPlanes();

        }

        private void picConfigurar_Click(object sender, EventArgs e)
        {
            verConfigurar(3, true, clavePlanta(gPlanta)); 
        }

        private void picRecorrido_Click(object sender, EventArgs e)
        {
            verRecorrido();
        }

        private void picWifi_Click(object sender, EventArgs e)
        {
            lblSenal.Text = fNet();
        }

        private void picPuertos_Click(object sender, EventArgs e)
        {
            verConfigurar(1, false, 0);
        }

        void etiquetaBoton(string texto, PictureBox pic, Boolean arriba)
        {
            Label lbl = new Label();
            this.Controls.Add(lbl);
            lbl.Name = "lbl" + pic.Name;
         
            lbl.Text = texto;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Font = new Font("Arial", 10);
            lbl.ForeColor = Color.Silver;
            lbl.AutoSize = false;

            if(arriba)
                lbl.Top = pic.Top - lbl.Height;
            else
                lbl.Top = pic.Top + pic.Height;
            
            lbl.Left = pic.Left;
            lbl.Width = pic.Width;
            lbl.BringToFront();
        }

        private void picOpciones_Click(object sender, EventArgs e)
        {
            panelHoral.Visible = !panelHoral.Visible;
        }

        public void lblMsgGPS_Click(object sender, EventArgs e)
        {
            fBotonGpsExt();
        }

        public void fBotonGpsExt()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            datos.p_sql = "UPDATE configuracion SET "
                + "puertogps = '" + lblPortGPS.Text
                + "'";

            if (!datos.consultaSQL(ref dt, true))
            {
                Comun.mensajeVer(datos.p_error, Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
            }
            else
            {
                lblPortGPS2.Text = lblPortGPS.Text;

                lblMsgGPS.Visible = false; 
                serialPortGps.Close();
                iniciaGps();
                //Comun.mensajeVer("Listo, configuro GPS, se reiniciara la aplicacion.", Constants.MSG_BOTON_OK, Constants.MSG_IMG_OK);
                //Application.Restart();
            }

        }

        public void lblMsgGPS2_Click(object sender, EventArgs e)
        {

            string vPortGPS2 =
                Comun.ExtraerEntreParentesis(Comun.fportDescripcion("nmea port"));

            if (vPortGPS2 != "")
            {

                Datos datos = new Datos();
                DataTable dt = new DataTable();

                datos.p_sql = "UPDATE configuracion SET "
                    + "puertogps = '" + vPortGPS2
                    + "'";

                if (!datos.consultaSQL(ref dt, true))
                {
                    Comun.mensajeVer(datos.p_error, Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
                }
                else
                {
                    
                    lblPortGPS2.Text = vPortGPS2;

                    lblMsgGPS2.Visible = false;
                    serialPortGps.Close();
                    iniciaGps();
                    //Comun.mensajeVer("Listo, configuro GPS, se reiniciara la aplicacion.", Constants.MSG_BOTON_OK, Constants.MSG_IMG_OK);
                    //Application.Restart();
                }
            
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            lblMsgGPS2_Click(sender, e);  
        }

        private void Principal_Click(object sender, EventArgs e)
        {
            verPlanes();
        }

        private void picNumero_Click(object sender, EventArgs e)
        {
            verConfigurar(2, false, 0);
        }

        private void picConfigurarT_Click(object sender, EventArgs e)
        {
            verConfigurar(0, true, Constants.CONGF_CLAVE_ADM);
        }

        private void picNumero_Paint(object sender, PaintEventArgs e)
        {
            Comun.valorBoton(picNumero, e, "C"+lblNumCamion.Text);
        }

        private void picPlanta_Click(object sender, EventArgs e)
        {
            verConfigurar(4, true, clavePlanta(gPlanta));
        }

        private int clavePlanta(string pPlanta)
        {

            var resultado = 0;

            switch (pPlanta)
            {

                case "1":
                    resultado = Constants.CONGF_CLAVE_P1;
                    break;
                case "2":
                    resultado = Constants.CONGF_CLAVE_P2;
                    break;

            }

            return resultado;

        }

        private void picPlanta_Paint(object sender, PaintEventArgs e)
        {
            Comun.valorBoton(picPlanta, e, "P"+gPlanta);
        }

        private void picNumeroFis_Click(object sender, EventArgs e)
        {
            verConfigurar(2, false, 0);
        }

        public static List<(string SSID, string Password)> GetSavedWifiNetworks()
        {
            var networks = new List<(string, string)>();
            try
            {
                var profilesProcess = Process.Start(new ProcessStartInfo("netsh", "wlan show profiles")
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                });

                var profilesOutput = profilesProcess.StandardOutput.ReadToEnd();
                profilesProcess.WaitForExit();

                //MessageBox.Show(profilesOutput);

                var matches1 = Regex.Matches(profilesOutput, @"All User Profile\s+:\s(.+)");
                var matches2 = Regex.Matches(profilesOutput, @"Perfil de todos los usuarios\s+:\s(.+)");
                var matches = matches1.Count > 0 ? matches1 : matches2;

                foreach (Match match in matches)
                {
                    var ssid = match.Groups[1].Value.Trim();
                    var keyProcess = Process.Start(new ProcessStartInfo("netsh", $"wlan show profile \"{ssid}\" key=clear")
                    {
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    });

                    var keyOutput = keyProcess.StandardOutput.ReadToEnd();
                    keyProcess.WaitForExit();

                    var passwordMatch = Regex.Match(keyOutput, @"Contenido de la clave\s+:\s(.+)");
                    var password = passwordMatch.Success ? passwordMatch.Groups[1].Value.Trim() : "";

                    //if (!string.IsNullOrEmpty(password))
                        networks.Add((ssid, password));
                }
            }
            catch { }

            return networks;
        }

        public void picIniGps_Click(object sender, EventArgs e)
        {
            iniciaGps();
        }

        public void picConectGps_Click(object sender, EventArgs e)
        {
            conetargps();
        }

        private void picIniGps_Paint(object sender, PaintEventArgs e)
        {
            Comun.valorBoton(picIniGps, e, "INI");
        }

        private void picConectGps_Paint(object sender, PaintEventArgs e)
        {
            Comun.valorBoton(picIniGps, e, "CONN");
        }



    }

}