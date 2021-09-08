using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Xml;
using System.Management;

namespace Senga
{
    public partial class Configurar : Form
    {

        private string namerutas = "\\\\tamggerpsig\\senga";
        private string nameruta = "\\senga_archivos";
        private string namea = "\\planes";
        private string nameb = "\\servido";
        private string namec = "\\procesados";
        private string nameu = "\\ubicaciones";

        ConnectedWs connectar;
        DataTable dtUbictmp;

        String usuario = "";
        String contrasena = "";
        

        public Configurar()
        {
            InitializeComponent();
        }

        private void Configurar_Load(object sender, EventArgs e)
        {

            //inicializa();
            propiedadesGrid();
            propiedadesGrid2();
            mostrar();
            tablaUbicTemp();
            posicionbotones();

            comboPuertos();
            comboPuertosGps();
            comboBaud();

            usuarioiWs();

            lbInfoArchivo.Items.Add(Application.ExecutablePath);
            lbInfoArchivo.Items.Add(File.GetLastWriteTime(Application.ExecutablePath));

            Acercade();
            verConfig();


            lblProcesadores.Text = Environment.ProcessorCount.ToString();
            lblNomMaq.Text = Environment.MachineName.ToString();


        }

        private void posicionbotones()
        {
         
            btnBasic.Top = 0;
            btnWs.Top = 0;
            btnCom.Top = 0;
            btnZone.Top = 0;
            btnSql.Top = 0;
            btnManual.Top = 0;
            btnMtto.Top = 0;
            btnGrabar.Top = 0;
            btnCerrar.Top = 0;



            btnCerrar.Left = this.Width - btnCerrar.Width;
            btnGrabar.Left = btnCerrar.Left - btnGrabar.Width;

            btnMtto.Left = btnGrabar.Left - btnMtto.Width;
            btnManual.Left = btnMtto.Left - btnManual.Width;
            btnSql.Left = btnManual.Left - btnSql.Width;
            btnZone.Left = btnSql.Left - btnZone.Width;
            btnCom.Left = btnZone.Left - btnCom.Width;
            btnWs.Left = btnCom.Left - btnWs.Width;
            btnBasic.Left = btnWs.Left - btnBasic.Width;







        }

        private void comboPuertos() 
        {

            portDescripcion();

            string[] puertos = System.IO.Ports.SerialPort.GetPortNames();
            
            cmbPuertos.Items.Clear();
            cmbParity.Items.Clear();
            cmbStop.Items.Clear();

            
            foreach (string value in puertos)
            {

                cmbPuertos.Items.Add(value);

            }

            cmbParity.Items.Add(System.IO.Ports.Parity.Even);
            cmbParity.Items.Add(System.IO.Ports.Parity.Mark);
            cmbParity.Items.Add(System.IO.Ports.Parity.None);
            cmbParity.Items.Add(System.IO.Ports.Parity.Odd);
            cmbParity.Items.Add(System.IO.Ports.Parity.Space);


            cmbStop.Items.Add(System.IO.Ports.StopBits.None);
            cmbStop.Items.Add(System.IO.Ports.StopBits.One);
            cmbStop.Items.Add(System.IO.Ports.StopBits.OnePointFive);
            cmbStop.Items.Add(System.IO.Ports.StopBits.Two);
            
        }


        private void portDescripcion() 
        {

            //ManagementObjectCollection ManObjReturn;
            //ManagementObjectSearcher ManObjSearch;
            //ManObjSearch = new ManagementObjectSearcher("Select * from Win32_SerialPort");
            //ManObjReturn = ManObjSearch.Get();

            //foreach (ManagementObject ManObj in ManObjReturn)
            //{
            //    //int s = ManObj.Properties.Count;
            //    //foreach (PropertyData d in ManObj.Properties)
            //    //{
            //    //    MessageBox.Show(d.Name);
            //    //}
            //    Console.WriteLine(ManObj["DeviceID"].ToString());
            //    Console.WriteLine(ManObj["PNPDeviceID"].ToString());
            //    Console.WriteLine(ManObj["Name"].ToString());
            //    Console.WriteLine(ManObj["Caption"].ToString());
            //    Console.WriteLine(ManObj["Description"].ToString());
            //    Console.WriteLine(ManObj["ProviderType"].ToString());
            //    Console.WriteLine(ManObj["Status"].ToString());

            //}


        }

        private void comboBaud()
        {


            int[] valores = new[] { 300, 600, 1200, 2400, 4800, 9600, 14400, 19200, 28800, 38400, 57600, 115200, 128000, 256000 };

            cmbBaud.Items.Clear();
            cmbBaudGps.Items.Clear();

            foreach (int value in valores)
            {

                cmbBaud.Items.Add(value.ToString());
                cmbBaudGps.Items.Add(value.ToString());

            }



        }

        private void comboPuertosGps()
        {

            string[] puertos = System.IO.Ports.SerialPort.GetPortNames();

            cmbPuertosGps.Items.Clear();
            cmbParityGps.Items.Clear();
            cmbStopGps.Items.Clear();


            foreach (string value in puertos)
            {

                cmbPuertosGps.Items.Add(value);

            }
                       

            cmbParityGps.Items.Add(System.IO.Ports.Parity.Even);
            cmbParityGps.Items.Add(System.IO.Ports.Parity.Mark);
            cmbParityGps.Items.Add(System.IO.Ports.Parity.None);
            cmbParityGps.Items.Add(System.IO.Ports.Parity.Odd);
            cmbParityGps.Items.Add(System.IO.Ports.Parity.Space);


            cmbStopGps.Items.Add(System.IO.Ports.StopBits.None);
            cmbStopGps.Items.Add(System.IO.Ports.StopBits.One);
            cmbStopGps.Items.Add(System.IO.Ports.StopBits.OnePointFive);
            cmbStopGps.Items.Add(System.IO.Ports.StopBits.Two);



        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        //private void inicializa() 
        //{
        //    Datos datos = new Datos();
        //    DataTable dt = new DataTable();

        //    int filas = 0;

        //    datos.p_sql = "SELECT * FROM configuracion";

        //    if (!datos.consultaSQL(ref dt, true))
        //    {
        //        MessageBox.Show(datos.p_error);
        //    }
        //    else
        //    {
        //        filas = dt.Rows.Count;
        //    }

        //    if (filas == 0)
        //    {
        //        datos.p_sql = "INSERT INTO configuracion (numcamion) VALUES ('0')";

        //        if (!datos.consultaSQL(ref dt, true))
        //        {
        //            MessageBox.Show(datos.p_error);
        //        }
        //    }
        
        //}

        private void btnGrabar_Click(object sender, EventArgs e)
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();


            if(txtCapacidad.Text == "")
            {
                txtCapacidad.Text = "0";
            }
            if (txtNumCamion.Text == "")
            {
                txtNumCamion.Text = "999";
            }

            if (txtRutaLocal.Text == "")
            {
                txtRutaLocal.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            
            if (txtRutaRed.Text == "")
            {
                txtRutaRed.Text = namerutas;
            }

            String activarws = "N";
            if (chkWs.Checked) { activarws = "S"; } else { activarws = "N"; }

            datos.p_sql = "UPDATE configuracion SET "
                + " numcamion = '" + txtNumCamion.Text
                + "', minvariacion = '" + txtMinVariacion.Text
                + "', diaselim = " + txtDiasElim.Text
                + ", capacidad = " + txtCapacidad.Text

                + ", puerto = '" + cmbPuertos.Text
                + "', baudrate = " + cmbBaud.Text
                + ", databits = " + cmbData.Text
                + ", parity = '" + cmbParity.Text
                + "', stopbits = '" + cmbStop.Text

                + "', puertogps = '" + cmbPuertosGps.Text
                + "', baudrategps = " + cmbBaudGps.Text
                + ", databitsgps = " + cmbDataGps.Text
                + ", paritygps = '" + cmbParityGps.Text
                + "', stopbitsgps = '" + cmbStopGps.Text

                + "', rutalocal = '" + txtRutaLocal.Text
                + "', rutared = '" + txtRutaRed.Text
                + "', minutostolerancia =" + txtMinutosTolerancia.Text
                
                + ", usuario = '" + txtUsuario.Text.Trim()
                + "', contrasena = '" + txtContrasena.Text.Trim()                 
                + "', ws = '" + activarws
                + "' ";



            if (!datos.consultaSQL(ref dt, true))
            {
                //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Comun.mensajeVer(datos.p_error, Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
            }
            else
            {
                //MessageBox.Show("Listo, cerrar y abrir aplicacion para que tome los cambios.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                Comun.mensajeVer("Listo, cerrar y abrir aplicacion para que tome los cambios.", Constants.MSG_BOTON_OK,Constants.MSG_IMG_OK);
                
            }

            //carpeta_existe_local();

            //guardaConfig();

            //Close();

        }

        private string activoGPS(bool valor)
        {
            if (valor)
            {
                return "S";
            }
            else
            {
                return "N";
            }

        }

        private void guardaConfig()
        {

            string rutadestino = "";
                        
            rutadestino =  Application.StartupPath + "\\Config.txt";
                    
            this.Cursor = Cursors.WaitCursor;

            try
            {
                //Open the File
                //StreamWriter objWriter = new StreamWriter(saveFileDialog1.FileName);
                StreamWriter objWriter = new StreamWriter(rutadestino);

                //objWriter.WriteLine(txtNumCamion.Text);
                //objWriter.WriteLine(txtCapacidad.Text);
                //objWriter.WriteLine(txtMinVariacion.Text);
                //objWriter.WriteLine(txtMinutosTolerancia.Text);
                //objWriter.WriteLine(txtRutaLocal.Text);
                //objWriter.WriteLine(txtRutaRed.Text);
                //objWriter.WriteLine(cmbPuertos.Text);
                //objWriter.WriteLine(cmbBaud.Text);
                //objWriter.WriteLine(cmbData.Text);
                //objWriter.WriteLine(cmbParity.Text);
                //objWriter.WriteLine(cmbStop.Text);

                string lv_cadena =

                txtNumCamion.Text + "|" +
                txtCapacidad.Text + "|" +
                txtMinVariacion.Text + "|" +
                txtMinutosTolerancia.Text + "|" +
                txtRutaLocal.Text + "|" +
                txtRutaRed.Text + "|" +

                cmbPuertos.Text + "|" +
                cmbBaud.Text + "|" +
                cmbData.Text + "|" +
                cmbParity.Text + "|" +
                cmbStop.Text + "|" +

                cmbPuertosGps.Text + "|" +
                cmbBaudGps.Text + "|" +
                cmbDataGps.Text + "|" +
                cmbParityGps.Text + "|" +
                cmbStopGps.Text + "|" +

                txtUsuario.Text + "|" +
                txtContrasena.Text;



                objWriter.WriteLine(lv_cadena);

                objWriter.Close();

                //MessageBox.Show("Listo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            }
            catch (Exception)
            {
                //MessageBox.Show("El archivo no se puede guardar");
            }

            //timer1.Interval = Convert.ToInt32(txtLatencia.Value) * 1000;

            this.Cursor = Cursors.Default;
            

        }

        private void txtNumCamion_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void txtNumCamion_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            e.Handled = !char.IsNumber(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back);
        }

        private void txtMinVariacion_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e) 
        {
            e.Handled = !char.IsNumber(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back);// && !(e.KeyChar == '-');
        }
        
        private void txtMinVariacion_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCapacidad_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            e.Handled = !char.IsNumber(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back);// && !(e.KeyChar == '-');
        }

        private void tablaUbicTemp()
        {

            dtUbictmp = new DataTable();

            dtUbictmp.Columns.Add("corral");
            dtUbictmp.Columns.Add("lat");
            dtUbictmp.Columns.Add("lng");

            consultaUbicaciones();
            //cargarUbicaciones();

        }

        private void mostrar() 
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
                    txtNumCamion.Text = row["NumCamion"].ToString();
                    txtMinVariacion.Text = row["MinVariacion"].ToString();
                    txtDiasElim.Text = row["DiasElim"].ToString();
                    txtCapacidad.Text = row["Capacidad"].ToString();

                    cmbPuertos.Text = row["Puerto"].ToString();
                    cmbBaud.Text = row["BaudRate"].ToString();
                    cmbData.Text = row["DataBits"].ToString();
                    cmbParity.Text = row["Parity"].ToString();
                    cmbStop.Text = row["StopBits"].ToString();

                    cmbPuertosGps.Text = row["PuertoGps"].ToString();
                    cmbBaudGps.Text = row["BaudRateGps"].ToString();
                    cmbDataGps.Text = row["DataBitsGps"].ToString();
                    cmbParityGps.Text = row["ParityGps"].ToString();
                    cmbStopGps.Text = row["StopBitsGps"].ToString();

                    txtRutaLocal.Text = row["RutaLocal"].ToString();
                    //txtRutaLocal.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    txtRutaRed.Text = row["RutaRed"].ToString();
                    txtMinutosTolerancia.Text = row["MinutosTolerancia"].ToString();

                    txtUsuario.Text = row["Usuario"].ToString();
                    txtContrasena.Text = row["Contrasena"].ToString();
                    
                    if (row["Ws"].ToString().Equals("S")) { chkWs.Checked = true; } else { chkWs.Checked = false; };

                    checkValores(chkWs);

                }
            }
        }

        private void txtCompact_Click(object sender, EventArgs e)
        {

            Datos datos = new Datos();

            if (!datos.compact())
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else 
            {
                //MessageBox.Show("Listo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Comun.mensajeVer("Listo", Constants.MSG_BOTON_OK, Constants.MSG_IMG_OK);
            }


        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            cmbBaud.Text = "9600";
            cmbData.Text = "7";

            cmbParity.Text = "Even";
            cmbStop.Text = "One";
        }

        private void btnR_Click(object sender, EventArgs e)
        {
            comboPuertos();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

            if (Comun.mensajeVer("¿ Desea Quitar Toda Informacion Jornadas, Raciones, Corrales y Operaciones Permanentemente ?", Constants.MSG_BOTON_SINO, Constants.MSG_IMG_WARNING) == Constants.MSG_BOTON_SI)

            //if (MessageBox.Show("¿ Desea Quitar Toda Informacion Jornadas, Raciones, Corrales y Operaciones Permanentemente?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Datos datos = new Datos();
                DataTable dt = new DataTable();

                this.Cursor = Cursors.WaitCursor;

                //datos.p_sql = "DELETE FROM servidas WHERE ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ";
                datos.p_sql = "DELETE FROM servidas";

                if (!datos.consultaSQL(ref dt, false))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                //datos.p_sql = "DELETE FROM raciones WHERE ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ";
                datos.p_sql = "DELETE FROM raciones";

                if (!datos.consultaSQL(ref dt, false))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                //else
                //{
                //    MessageBox.Show("Listo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}

                datos.p_sql = "DELETE FROM jornadas";

                if (!datos.consultaSQL(ref dt, false))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                //else
                //{
                //    MessageBox.Show("Listo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}


                datos.p_sql = "DELETE FROM recorrido WHERE envio = 'S'"; 

                if (!datos.consultaSQL(ref dt, false))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }


                datos.p_sql = "DELETE FROM cargas";

                if (!datos.consultaSQL(ref dt, false))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                datos.p_sql = "DELETE FROM operacion";

                if (!datos.consultaSQL(ref dt, false))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }


                datos.p_sql = "INSERT INTO operacion (servida) VALUES ('1')";

                if (!datos.consultaSQL(ref dt, true))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    //MessageBox.Show("Listo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Comun.mensajeVer("Listo", Constants.MSG_BOTON_OK, Constants.MSG_IMG_OK);
                }

                this.Cursor = Cursors.Default;

            }
        }


        private void carpeta_existe_local()
        {


            
            if (!System.IO.Directory.Exists(rutaarchivos() + nameruta))
            {
                System.IO.Directory.CreateDirectory(rutaarchivos() + nameruta);
            }

            if (!System.IO.Directory.Exists(rutaarchivos() + nameruta + namea))
            {
                System.IO.Directory.CreateDirectory(rutaarchivos() + nameruta + namea);
            }

            if (!System.IO.Directory.Exists(rutaarchivos() + nameruta + namea + namec))
            {
                System.IO.Directory.CreateDirectory(rutaarchivos() + nameruta + namea + namec);
            }

            if (!System.IO.Directory.Exists(rutaarchivos() + nameruta + nameb))
            {
                System.IO.Directory.CreateDirectory(rutaarchivos() + nameruta + nameb);
            }

            if (!System.IO.Directory.Exists(rutaarchivos() + nameruta + nameb + namec))
            {
                System.IO.Directory.CreateDirectory(rutaarchivos() + nameruta + nameb + namec);
            }




            //if (!System.IO.Directory.Exists(rutaarchivosred() + nameruta))
            //{
            //    System.IO.Directory.CreateDirectory(rutaarchivosred() + nameruta);
            //}

            //if (!System.IO.Directory.Exists(rutaarchivosred() + nameruta + namea))
            //{
            //    System.IO.Directory.CreateDirectory(rutaarchivosred() + nameruta + namea);
            //}

            ////if (!System.IO.Directory.Exists(rutaarchivosred() + nameruta + namea + namec))
            ////{
            ////    System.IO.Directory.CreateDirectory(rutaarchivosred() + nameruta + namea + namec);
            ////}

            //if (!System.IO.Directory.Exists(rutaarchivosred() + nameruta + nameb))
            //{
            //    System.IO.Directory.CreateDirectory(rutaarchivosred() + nameruta + nameb);
            //}

            ////if (!System.IO.Directory.Exists(rutaarchivosred() + nameruta + nameb + namec))
            ////{
            ////    System.IO.Directory.CreateDirectory(rutaarchivosred() + nameruta + nameb + namec);
            ////}



        }




        private string rutaarchivos()
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
                    valor = row["RutaLocal"].ToString();
                }
            }

            return valor;

        }

        private string rutaarchivosred()
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

        private void btnDefaultRuta_Click(object sender, EventArgs e)
        {
            txtRutaLocal.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        private void btnDefaultRutaRed_Click(object sender, EventArgs e)
        {
            txtRutaRed.Text = namerutas;
        }
 
        private void btnImportar_Click(object sender, EventArgs e)
        {

            string rutaorigen = "";

            rutaorigen = Application.StartupPath + "\\Config.txt";

            this.Cursor = Cursors.WaitCursor;
            
            try
            {

                StreamReader objReader = new StreamReader(rutaorigen);
                string sLine = "";
                
                sLine = objReader.ReadLine();

                string[] lines = sLine.Split('|');
       
                txtNumCamion.Text = lines[0].Trim();
                txtCapacidad.Text = lines[1].Trim();
                txtMinVariacion.Text = lines[2].Trim();
                txtMinutosTolerancia.Text = lines[3].Trim();
                txtRutaLocal.Text = lines[4].Trim();
                txtRutaRed.Text = lines[5].Trim();

                cmbPuertos.Text = lines[6].Trim();
                cmbBaud.Text = lines[7].Trim();
                cmbData.Text = lines[8].Trim();
                cmbParity.Text = lines[9].Trim();
                cmbStop.Text = lines[10].Trim();


                cmbPuertosGps.Text = lines[11].Trim();
                cmbBaudGps.Text = lines[12].Trim();
                cmbDataGps.Text = lines[13].Trim();
                cmbParityGps.Text = lines[14].Trim();
                cmbStopGps.Text = lines[15].Trim();

                txtUsuario.Text = lines[16].Trim();
                txtContrasena.Text = lines[17].Trim();

                objReader.Close();

                MessageBox.Show("Listo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                
            }
            catch (Exception)
            {
                MessageBox.Show("El archivo no se puede leer");
            }

            this.Cursor = Cursors.Default;

        }

        private void btnRGPS_Click(object sender, EventArgs e)
        {
            comboPuertos();
        }

        #region Descriptores de acceso de atributos de ensamblado

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        public void Acercade()
        {
            //InitializeComponent();
            //this.Text = String.Format("Acerca de {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Versión {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.textBoxDescription.Text = AssemblyDescription;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;


            if (Comun.mensajeVer("¿ Desea sincronizar ubicaciones ?", Constants.MSG_BOTON_SINO, Constants.MSG_IMG_QUESTION) == Constants.MSG_BOTON_SI)
            
            //if (MessageBox.Show("¿ Desea actualizar ubicaciones ?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                //string targetproc = namerutas + nameruta + nameu + "\\ubicaciones.txt";

                //listaUbicaciones(targetproc, 1);
                listaUbicaciones("", 2);

            }

            this.Cursor = Cursors.Default;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;

            //if (MessageBox.Show("¿ Desea eliminar ubicaciones ?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            if (Comun.mensajeVer("¿ Desea eliminar ubicaciones ?", Constants.MSG_BOTON_SINO, Constants.MSG_IMG_WARNING) == Constants.MSG_BOTON_SI)
            {

                    Datos datos = new Datos();
                DataTable dt = new DataTable();

                datos.p_sql = "DELETE FROM ubicacion";

                if (!datos.consultaSQL(ref dt, true))
                {
                    //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    MessageBox.Show(datos.p_error, "Mensaje");
                }
                else
                {
                    consultaUbicaciones();

                    //MessageBox.Show("Listo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    Comun.mensajeVer("Listo", Constants.MSG_BOTON_OK, Constants.MSG_IMG_OK);

                }

            }

            this.Cursor = Cursors.Default;

        }

        private void consultaUbicaciones()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            this.Cursor = Cursors.WaitCursor;

            datos.p_sql = "SELECT * FROM ubicacion";

            if (!datos.consultaSQL(ref dt, true))
            {
                //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                MessageBox.Show(datos.p_error, "Mensaje");
            }
            else
            {

                dtUbictmp.Clear();

                foreach (DataRow row in dt.Rows)
                {

                    DataRow newRow = dtUbictmp.NewRow();

                    newRow["corral"] = row["corral"];
                    newRow["lat"] = row["latitud"];
                    newRow["lng"] = row["longitud"];

                    dtUbictmp.Rows.Add(newRow);

                }

                dataGridUbicaciones.DataSource = dtUbictmp;
                formatoubicacionesGrid();
                lblCantidad.Text = dataGridUbicaciones.RowCount.ToString();

            }

            this.Cursor = Cursors.Default;

        }

        private void formatoubicacionesGrid()
        {

            for (int i = 0; i < dataGridUbicaciones.Columns.Count; i++)
            {
                dataGridUbicaciones.Columns[i].Width = 190;
                dataGridUbicaciones.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridUbicaciones.Columns[i].HeaderCell.Style.Font = new Font("Tahoma", 15.00F, FontStyle.Bold);
                dataGridUbicaciones.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridUbicaciones.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }


            dataGridUbicaciones.Columns[0].HeaderText = "Corral";
            dataGridUbicaciones.Columns[1].HeaderText = "Latitud";
            dataGridUbicaciones.Columns[2].HeaderText = "Longitud";

        }

        private void listaUbicaciones(string ruta, int opc)
        {

            DataTable dt = new DataTable();
            String msgerr = "";

            switch (opc)
            {
                case 1:

                    leerTxtUbicacion(ruta, ref dt);

                    break;

                case 2:

                    //leerWsUbicacion(ref dt);
                    leerWsdlUbicacion(ref dt, ref msgerr);

                    break;

            }


            if (dt.Rows.Count > 0)
            {

                guardaUbicaciones(dt);

                consultaUbicaciones();
                //cargarUbicaciones();

                //MessageBox.Show("Listo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                Comun.mensajeVer("Listo", Constants.MSG_BOTON_OK, Constants.MSG_IMG_OK);

            }
            else
            {

                //MessageBox.Show("No actualizo ninguna ubicacion " + msgerr, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Comun.mensajeVer("No actualizo ninguna ubicacion " + msgerr, Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);

            }

        }


        private void guardaUbicaciones(DataTable dtentrada)
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            this.Cursor = Cursors.WaitCursor;

            datos.p_sql = "DELETE FROM ubicacion";

            if (!datos.consultaSQL(ref dt, true))
            {
                //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                MessageBox.Show(datos.p_error, "Mensaje");
            }


            List<string> dbOperations = new List<string>();

            foreach (DataRow row in dtentrada.Rows)
            {
                dbOperations.Add("insert into ubicacion (corral,latitud,longitud,latitud1,longitud1,latitud2,longitud2,latitud3,longitud3,latitud4,longitud4) values('" + row["corral"].ToString() + "', " + row["lat"].ToString() + "," + row["lng"].ToString() + "," + row["lat1"].ToString() + "," + row["lng1"].ToString() + "," + row["lat2"].ToString() + "," + row["lng2"].ToString() + "," + row["lat3"].ToString() + "," + row["lng3"].ToString() + "," + row["lat4"].ToString() + "," + row["lng4"].ToString() + ")");
            }


            datos.p_sql_list = dbOperations;

            if (!datos.consultaSQLs(ref dt))
            {
                //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                MessageBox.Show(datos.p_error, "Mensaje");
            }
            else
            {

            }

            this.Cursor = Cursors.Default;

        }


        private void leerTxtUbicacion(string ruta, ref DataTable dt)
        {

            try
            {

                //StreamReader objReader = new StreamReader(openFileDialog1.OpenFile());
                StreamReader objReader = new StreamReader(ruta);
                string sLine = "";

                DataTable dttmp = new DataTable();
                dttmp.Columns.Add("corral");
                //dttmp.Columns.Add("name");
                dttmp.Columns.Add("lat");
                dttmp.Columns.Add("lng");
                //dttmp.Columns.Add("type");


                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null)
                    {

                        string[] lines = sLine.Split(',');


                        DataRow newRow = dttmp.NewRow();
                        newRow["corral"] = lines[0].Trim();
                        //newRow["name"] = "";
                        newRow["lat"] = lines[1].Trim();
                        newRow["lng"] = lines[2].Trim();
                        //newRow["type"] = "";

                        dttmp.Rows.Add(newRow);

                    }
                }

                dt = dttmp;

                objReader.Close();

            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message, "Mensaje");
            }

        }

        private void btnDefaultGps_Click(object sender, EventArgs e)
        {
            cmbBaudGps.Text = "38400";
            cmbDataGps.Text = "8";

            cmbParityGps.Text = "None";
            cmbStopGps.Text = "One";
        }

        private void btnRGps_Click_1(object sender, EventArgs e)
        {
            comboPuertosGps();
        }


        private void leerWsdlUbicacion(ref DataTable dt, ref String msgerr)
        {
            ArrayList lista = new ArrayList();

            //Cursor = Cursors.WaitCursor;

            String seleccion = "ALMACENES";

            Thread t = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                cargarWSDLSel(seleccion);
            });
            t.Start();

            while (t.IsAlive)
            {
                Thread.Sleep(50);
                Application.DoEvents();
            }

            lista = connectar.getList();
            String errormsg = connectar.getError();
            String emsg = connectar.getEmsg();



            DataTable dttmp = new DataTable();
            dttmp.Columns.Add("corral");
            dttmp.Columns.Add("lat");
            dttmp.Columns.Add("lng");
            dttmp.Columns.Add("lat1");
            dttmp.Columns.Add("lng1");
            dttmp.Columns.Add("lat2");
            dttmp.Columns.Add("lng2");
            dttmp.Columns.Add("lat3");
            dttmp.Columns.Add("lng3");
            dttmp.Columns.Add("lat4");
            dttmp.Columns.Add("lng4");

            foreach (String listar in lista)
            {

                string[] lines = listar.Split('|');

                DataRow newRow = dttmp.NewRow();
                newRow["corral"] = lines[0].Trim();
                newRow["lat"] = lines[1].Trim();
                newRow["lng"] = lines[2].Trim();
                newRow["lat1"] = lines[3].Trim();
                newRow["lng1"] = lines[4].Trim();
                newRow["lat2"] = lines[5].Trim();
                newRow["lng2"] = lines[6].Trim();
                newRow["lat3"] = lines[7].Trim();
                newRow["lng3"] = lines[8].Trim();
                newRow["lat4"] = lines[9].Trim();
                newRow["lng4"] = lines[10].Trim();



                dttmp.Rows.Add(newRow);

            }

            dt = dttmp;

            //Cursor = Cursors.Default;

            if (!errormsg.Equals(""))
            {
                //MessageBox.Show(errormsg, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                msgerr = errormsg;
            }
            //if (!errormsg.Equals("")) { lblErrores.Text = errormsg; }


        }

        private void cargarWSDLSel(String catalogo)
        {

            connectar = new ConnectedWs(Constants.OPERACION_CATALOGO, catalogo, usuario, contrasena, "_");
            connectar.getDataWs();

        }

        private void usuarioiWs()
        {
            usuario = txtUsuario.Text;
            contrasena = txtContrasena.Text;
        }

        private void btnTeclado_Click(object sender, EventArgs e)
        {
            String folderPath = @"C:\Program Files\Common Files\Microsoft Shared\ink";
            String keyBPath = Path.Combine(folderPath, "TabTip.exe");
            Process.Start(keyBPath);
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
                       
            Datos datos = new Datos();
            DataTable dt = new DataTable();

            
            string valor = txtSQL.Text;


            if (txtSQL.SelectionLength > 0)
            {
                valor = txtSQL.SelectedText;
            }


            //-----------------------------------------------------
            int vcont = 0;
            Boolean verror = false;
            string[] lines = valor.Split(';');

            foreach (String line in lines)
            {
                vcont = vcont + 1;
                string linea = line.Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty);

                if (!linea.Equals(""))
                {

                    datos.p_sql = linea;

                    if (!linea.Substring(0, 2).ToLower().Equals("--"))
                    {

                        if (linea.ToLower().Contains("select"))
                        {
                            if (!datos.consultaSQL(ref dt, true))
                            {
                                //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                Comun.mensajeVer(datos.p_error + " Linea : " + vcont.ToString(), Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
                                verror = true;
                                break;
                            }
                            else
                            {
                                dataGridSQLResultados.DataSource = dt;

                                lblSQLStatus.Text = "Correcto" + " Total " + dt.Rows.Count.ToString() + " Reg.";
                            }
                        }
                        else
                        {

                            if (!datos.consultaSQL(ref dt, false))
                            {
                                //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                Comun.mensajeVer(datos.p_error + " Linea : " + vcont.ToString(), Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
                                verror = true;
                                break;
                            }
                            else
                            {
                                lblSQLStatus.Text = "Correcto";
                            }

                        }
                
                    }
                
                }

            }

            //if (!verror)
            //{
            //    Comun.mensajeVer("Listo", Constants.MSG_BOTON_OK, Constants.MSG_IMG_OK);
            //}

            //-----------------------------------------------------

            //datos.p_sql = valor;

            //if (!datos.consultaSQL(ref dt, false))
            //{
            //    //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    Comun.mensajeVer(datos.p_error, Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
            //}
            //else
            //{
            //    Comun.mensajeVer("Listo", Constants.MSG_BOTON_OK, Constants.MSG_IMG_OK);
            //}

            //-----------------------------------------------------


        }

        private void txtSQL_DragDrop(object sender, DragEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            //if (MessageBox.Show("¿ Desea actualizar sql ?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            //{


                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                foreach (string file in files)
                //MessageBox.Show(file);

                try
                {
                    StreamReader objReader = new StreamReader(file);
                    string sLine = "";
                    
                    while (sLine != null)
                    {
                        sLine = objReader.ReadLine();
                        if (sLine != null)
                        {

                            txtSQL.Text = txtSQL.Text + sLine + "\r\n";
                            //string[] lines = sLine.Split(',');


                            //DataRow newRow = dttmp.NewRow();
                            //newRow["id"] = lines[0].Trim();
                            //newRow["name"] = "";
                            //newRow["lat"] = lines[1].Trim();
                            //newRow["lng"] = lines[2].Trim();
                            //newRow["type"] = "";

                            //dttmp.Rows.Add(newRow);

                        }
                    }

                }
                catch (IOException)
                {

                }

            //}

            this.Cursor = Cursors.Default;
        }

        private void txtSQL_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void dataGridUbicaciones_DragDrop(object sender, DragEventArgs e)
        {
            
            this.Cursor = Cursors.WaitCursor;

            if (Comun.mensajeVer("¿ Desea actualizar ubicaciones ?", Constants.MSG_BOTON_SINO, Constants.MSG_IMG_QUESTION) == Constants.MSG_BOTON_SI)
            {

                DataTable dttmp = new DataTable();
                dttmp.Columns.Add("corral");
                dttmp.Columns.Add("lat");
                dttmp.Columns.Add("lng");
                dttmp.Columns.Add("lat1");
                dttmp.Columns.Add("lng1");
                dttmp.Columns.Add("lat2");
                dttmp.Columns.Add("lng2");
                dttmp.Columns.Add("lat3");
                dttmp.Columns.Add("lng3");
                dttmp.Columns.Add("lat4");
                dttmp.Columns.Add("lng4");

                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                foreach (string file in files)
                    //MessageBox.Show(file);

                    try
                    {
                        StreamReader objReader = new StreamReader(file);
                        string sLine = "";
                        int cont1;

                        while (sLine != null)
                        {
                            sLine = objReader.ReadLine();

                            if (sLine != null)
                            {

                                cont1 = 0;

                                for (int i = 0; i < sLine.Length; i++)
                                {                       
                                    if ((sLine.Substring(i, 1) == ",") )
                                    {
                                        cont1++;
                                    }                               
                                }

                                if (cont1 == 10)
                                {

                                    //txtSQL.Text = txtSQL.Text + sLine + "\r\n";
                                    string[] lines = sLine.Split(',');


                                    DataRow newRow = dttmp.NewRow();
                                    newRow["corral"] = lines[0].Trim();
                                    newRow["lat"] = lines[1].Trim();
                                    newRow["lng"] = lines[2].Trim();
                                    newRow["lat1"] = lines[3].Trim();
                                    newRow["lng1"] = lines[4].Trim();
                                    newRow["lat2"] = lines[5].Trim();
                                    newRow["lng2"] = lines[6].Trim();
                                    newRow["lat3"] = lines[7].Trim();
                                    newRow["lng3"] = lines[8].Trim();
                                    newRow["lat4"] = lines[9].Trim();
                                    newRow["lng4"] = lines[10].Trim();

                                    dttmp.Rows.Add(newRow);
                                }

                            }
                        }

                        if (dttmp.Rows.Count > 0)
                        {

                            guardaUbicaciones(dttmp);
                            consultaUbicaciones();

                        }

                        Comun.mensajeVer("Listo", Constants.MSG_BOTON_OK, Constants.MSG_IMG_OK);

                    }
                    catch (IOException)
                    {

                    }

                //}
            }

            this.Cursor = Cursors.Default;
            
        }

        private void dataGridUbicaciones_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            consultaUbicaciones();

            //MessageBox.Show("Listo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            Comun.mensajeVer("Listo", Constants.MSG_BOTON_OK, Constants.MSG_IMG_OK);

        }

        private void tabPage8_Click(object sender, EventArgs e)
        {

        }

        private void btnTestPeso_Click(object sender, EventArgs e)
        {

            Comm comm = new Comm();
            SerialPort serialPort1 = new SerialPort();
            string dator = "";

            this.Cursor = Cursors.WaitCursor;
            btnTestPeso.Enabled = false;

            comm.p_comm = cmbPuertos.Text;
            comm.p_baudrate = Int32.Parse(cmbBaud.Text);
            comm.p_databits = Int32.Parse(cmbData.Text);
            comm.p_parity = cmbParity.Text;
            comm.p_stopbits = cmbStop.Text;
            comm.p_sp = serialPort1;

            if (!comm.abrirPuerto())
            {
                MessageBox.Show(comm.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {

                for (int i = 0; i < 10; i++)
                {

                    if (!comm.recibePuerto())
                    {
                        //break;
                    }
                    else
                    {
                        Thread.Sleep(50);
                        dator = comm.p_dato;
                        
                        if (!dator.Equals("0"))
                        {
                            break;
                        }
                    }

                    

                }

                lblTestPeso.Text = dator;
                comm.cerrarPuerto();
                

            }

            this.Cursor = Cursors.Default;
            btnTestPeso.Enabled = true;

        }

        //private int mensajeVer(string mensaje, int opc)
        //{
        //    int msgResult = 0;

        //    switch (opc)
        //    {
        //        case Constants.MSG_BOTON_OK:
        //            Mensaje01 msg1 = new Mensaje01(mensaje);
        //            msg1.ShowDialog ();                    
        //            break;

        //        case Constants.MSG_BOTON_SINO:

        //            using (Mensaje02 msg2 = new Mensaje02(mensaje))
        //            {
        //                msg2.ShowDialog();
        //                msgResult = msg2.result;                
        //            }

        //            break;

        //    }

        //    return msgResult;

        //}

            
        private void verConfig()
        {

            Senga.Properties.Settings setting = Senga.Properties.Settings.Default;

            string[] cadena = setting.Senga_getSAPData_zwsdl_gps.Split('/');            
            string[] cadena2 = cadena[2].ToString().Split('.');

            txtMandante.Text = cadena[9].ToString();
            txtServer.Text = cadena2[0].ToString();

            //Properties.Settings.Default.Save();
            //Properties.Settings.Default.Reload();

        }

        private void btnRecursos_Click(object sender, EventArgs e)
        {


            string resource_data = Properties.Resources.SQL;
            string[] words = resource_data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string lines in words)
            {
                txtSQL.Text += lines+'\r'+'\n'; 
            }

        }

        private void btnLimpia_Click(object sender, EventArgs e)
        {

            if (Comun.mensajeVer("¿ Desea limpiar ventana ?", Constants.MSG_BOTON_SINO, Constants.MSG_IMG_QUESTION) == Constants.MSG_BOTON_SI)
            {
                txtSQL.Text = "";
            }
        }

        private void btnConfigPeso_Click(object sender, EventArgs e)
        {

            listBoxConfigDisplay.Items.Clear();

            string resource_data = Properties.Resources.PESO;
            string[] words = resource_data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string lines in words)
            {
                listBoxConfigDisplay.Items.Add(lines);
            }
        }


        private void propiedadesGrid()
        {

            dataGridUbicaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridUbicaciones.ReadOnly = true;
            dataGridUbicaciones.AllowUserToAddRows = false;
            dataGridUbicaciones.AllowUserToDeleteRows = false;
            dataGridUbicaciones.AllowUserToOrderColumns = false;
            dataGridUbicaciones.AllowUserToResizeColumns = false;
            dataGridUbicaciones.AllowUserToResizeRows = false;
            dataGridUbicaciones.MultiSelect = false;
            //DGRaciones.ColumnHeadersVisible = false;
            dataGridUbicaciones.RowHeadersVisible = false;
            dataGridUbicaciones.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridUbicaciones.DefaultCellStyle.Font = new Font("Arial", 20.00F, FontStyle.Bold);
            //DGRaciones.RowTemplate.Height = 50;


        }

        private void propiedadesGrid2()
        {

            dataGridSQLResultados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridSQLResultados.ReadOnly = true;
            dataGridSQLResultados.AllowUserToAddRows = false;
            dataGridSQLResultados.AllowUserToDeleteRows = false;
            dataGridSQLResultados.AllowUserToOrderColumns = false;
            dataGridSQLResultados.AllowUserToResizeColumns = true;
            dataGridSQLResultados.AllowUserToResizeRows = false;
            dataGridSQLResultados.MultiSelect = false;
            //DGRaciones.ColumnHeadersVisible = false;
            dataGridSQLResultados.RowHeadersVisible = false;
            dataGridSQLResultados.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridSQLResultados.DefaultCellStyle.Font = new Font("Arial", 20.00F, FontStyle.Bold);
            //DGRaciones.RowTemplate.Height = 50;


        }

        private void btnBasic_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["basic"];
        }

        private void btnWs_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["ws"];
        }

        private void btnCom_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["com"];
        }

        private void btnZone_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["zone"];
        }

        private void btnSql_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["sql"];
        }

        private void btnManual_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["manual"];
        }

        private void btnMtto_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["mtto"];
        }

        private void btnTablas_Click(object sender, EventArgs e) 
        {
            Datos datos = new Datos();

            List<string> tablas = new List<string>();
            tablas = datos.ObtenerTablas();

            foreach (string value in tablas)
            {
                txtSQL.Text += value + '\r' + '\n';
            }

        }

        private void chkWs_CheckedChanged(object sender, EventArgs e)
        {
            checkValores(chkWs);
        }


        void checkValores(CheckBox control) 
        {

            if (control.Checked)
            {
                control.ImageIndex = 1;
            }
            else 
            {
                control.ImageIndex = 0;
            }        
        }

    }

}