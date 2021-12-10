using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.Media;

namespace Senga
{
    public partial class Racion : Form
    {
        //delegate void UpdateTextEventHandler(string text);

        //private delegate void displayDelegate(string x);
        //displayDelegate displayFunction;

        private delegate void DelegaAcceso(string accion);

        //private Comm comm = new Comm();
        private SerialPort serialPort1 = new SerialPort();

        private SoundPlayer player = new SoundPlayer(Properties.Resources.beep_censor);

        string vidd;
        string vjornada;
        int pesoact = 0;
        int pesostop = 0;
        int pesomargen = 0;
        string[] seriePesos = new string[] { "0", "0", "0", "0", "0", "0" };
        bool vestable = false;
        bool vtolerable = false;
        int lv_pos = 0;
        string vzeros = "";
        string vpeso = "";
        int cantAjustes = 1;
        bool beepOn = true;
        char c = (char)02;
        char r = '\r';
        char n = '\n';
        string recibeData = "";

        public Racion(string idd, string id)
        {
            InitializeComponent();
            vidd = idd;
            vjornada = id;

            //displayFunction = displayText;

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

            if (Comun.mensajeVer("¿ Desea Cancelar ?", Constants.MSG_BOTON_SINO, Constants.MSG_IMG_WARNING) == Constants.MSG_BOTON_SI)
            {
                Close();
            }
        }

        private void Raciones_Load(object sender, EventArgs e)
        {

            //this.Visible = false;
            //manejoDePrincipal(false);

            manejoTimerDePrincipal(false);
            manejoTimerDeRaciones(false);
                       

            //posicionBotones();
            
            //datosRacion();
            //datosServida(); 

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            int fisico = 0;
            int tamanoact = 0;
            int capacidad = 0;
            int resultado = 0;

            txtCargaV.Text = "0";
            txtBasculaIniV.Text = "0";
            txtBrutoV.Text = "0";
            txtSolicitarV.Text = "0";
            txtCargadoV.Text = "0";
            txtFaltaV.Text = "0";
            txtCapacidadV.Text = "0";
            txtCargaPorc.Text = "0";

            txtBruto.Text = "0";
            txtTamano.Text = "0";
            txtFalta.Text = "0";
            txtFaltaV.Text = "0";
            txtCargado.Text = "0";
            txtCarga.Text = "0";
            txtCargadoV.Text = "0";
            txtStop.Text = "0";
            txtBasculaIni.Text = "0";
            txtCargaIni.Text = "0";
            txtSolicitar.Text = "0";
            txtTamanoAct.Text = "0";

            //IniciaPesoOpc1();
            IniciaPesoOpc2();
            colorPantalla();

            datos.p_sql = "SELECT racion, planeado, cargado, servido, falta, carga, bascula FROM raciones WHERE idd = " + vidd;

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {

                    btnRacion.Text = row["Racion"].ToString();
                    txtRacion.Text = row["Racion"].ToString();

                    txtTamano.Text = row["Planeado"].ToString();
                    txtServido.Text = row["Servido"].ToString();
                    txtCargaIni.Text = cadenaCero(row["Carga"].ToString());
                    txtBasculaIni.Text = cadenaCero(row["Bascula"].ToString());

                    //txtTamanoAct.Text = Convert.ToString(Convert.ToInt32(txtTamano.Text) - Convert.ToInt32(txtServido.Text));

                    long falta = long.Parse(row["Falta"].ToString());

                    txtTamanoAct.Text = falta.ToString();
                 
                    tamanoact = Convert.ToInt32(txtTamanoAct.Text);
                    capacidad = Convert.ToInt32(txtCapacidad.Text);

                    //if (tamanoact < 0)
                    //{
                    //    txtTamanoAct.Text = "0";
                    //}

                    if (tamanoact > capacidad || tamanoact < 0)
                    {
                        txtTamanoAct.Text = txtCapacidad.Text;
                    }

                    //resultado = Convert.ToInt32(txtTamanoAct.Text) - Convert.ToInt32(txtBasculaIni.Text);
                    //txtSolicitar.Text = resultado.ToString();



                    txtCargadoIni.Text = "0"; //row["Cargado"].ToString();
                    //txtCargado.Text = row["Cargado"].ToString();

                    fisico = Convert.ToInt32(row["Cargado"]) - Convert.ToInt32(row["Servido"]);
                    txtFisico.Text = fisico.ToString();
                    txtFisicoIni.Text = fisico.ToString();

                    llenaSolicitar();
                    //resultado = Convert.ToInt32(txtTamanoAct.Text) - Convert.ToInt32(txtBasculaIni.Text);
                    //txtSolicitar.Text = resultado.ToString();

                }

                ////IniciaPesoOpc1();
                //IniciaPesoOpc2();

                //----------------------------------------------------------------------------------------------------------------
                //----------------------------------------------------------------------------------------------------------------
                //comm.p_comm = puertoConfig();
                //comm.p_baudrate = baudConfig();
                //comm.p_databits = dataConfig();
                //comm.p_parity = parityConfig();
                //comm.p_stopbits = stopConfig();
                //comm.p_sp = serialPort1;

                //if (!comm.abrirPuerto())
                //{
                //    Close();
                //    MessageBox.Show(comm.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                //}
                //else
                //{
                //    timer1.Enabled = true;
                //}
                //----------------------------------------------------------------------------------------------------------------
                //try
                //{
                //    serialPort1.PortName = puertoConfig();
                //    serialPort1.BaudRate = baudConfig();
                //    serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), parityConfig(), true);
                //    serialPort1.DataBits = dataConfig();
                //    serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopConfig(), true);
                //    serialPort1.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
                //    serialPort1.Open();

                //}
                //catch (Exception err)
                //{
                //    Close();
                //    MessageBox.Show(err.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //}
                //----------------------------------------------------------------------------------------------------------------
                //----------------------------------------------------------------------------------------------------------------
            }



            //panelAnimacion.Visible = true;
            
            //this.Visible = true;

        }


        private void IniciaPesoOpc1() 
        {

            //comm.p_comm = puertoConfig();
            //comm.p_baudrate = baudConfig();
            //comm.p_databits = dataConfig();
            //comm.p_parity = parityConfig();
            //comm.p_stopbits = stopConfig();
            //comm.p_sp = serialPort1;

            //if (!comm.abrirPuerto())
            //{
            //    Close();
            //    MessageBox.Show(comm.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            //}
            //else
            //{
            //    timerR.Enabled = true;
            //}

        }

        private void IniciaPesoOpc2()
        {

            try
            {
                serialPort1.PortName = puertoConfig();
                serialPort1.BaudRate = baudConfig();
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), parityConfig(), true);
                serialPort1.DataBits = dataConfig();
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopConfig(), true);
                //serialPort1.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
                serialPort1.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived2);
                serialPort1.ReadTimeout = 500;
                serialPort1.WriteTimeout = 500;
                serialPort1.Open();
            }
            catch (Exception err)
            {
                Close();
                //MessageBox.Show(err.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Comun.mensajeVer(err.Message + "\n\rVerifique Cable Peso o \n\rConfiguracion del Puerto COM", Constants.MSG_BOTON_OK, Constants.MSG_IMG_WARNING);
                
            }

        }

        private void Raciones_Activated(object sender, EventArgs e)
        {
            operacionCorral();


        }

        //private void datosRacion() 
        //{
        //    Datos datos = new Datos();
        //    DataTable dt = new DataTable();

        //    datos.p_sql = "SELECT planeado, cargado FROM raciones WHERE num = " + txtServida.Text + " and racion = '" + btnRacion.Text + "' and ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " )";

        //    if (!datos.consultaSQL(ref dt, true))
        //    {
        //        MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //    }
        //    else
        //    {
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            txtCargadoIni.Text = row["Cargado"].ToString();
        //            txtCargado.Text = row["Cargado"].ToString();
        //            txtTamano.Text = row["Planeado"].ToString();

        //        }
        //    }
        //}

        //private void datosServida() 
        //{

        //    Datos datos = new Datos();
        //    DataTable dt = new DataTable();
        //    //DataTable table = (DataTable)(DGServidas.DataSource);

        //    int maximo = 0;
        //    int valor = 0;
        //    int corrales = 0;

        //    datos.p_sql = "SELECT count(corral) as Corrales , sum(planeado) as Planeado, sum(realidad) as Realidad FROM servidas WHERE num = " + txtServida.Text + " and  ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ";

        //    if (!datos.consultaSQL(ref dt, true))
        //    {
        //        MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //    }
        //    else
        //    {
        //        foreach (DataRow row in dt.Rows)
        //        {

        //            if (row["Corrales"].ToString() != "0")
        //            {
        //                maximo = Convert.ToInt32(row["Planeado"].ToString());
        //                valor = Convert.ToInt32(row["Realidad"].ToString());
        //                corrales = Convert.ToInt32(row["Corrales"].ToString());
        //            }
        //        }

        //        txtTamano.Text = maximo.ToString();

        //    }


        //}

        private void operacionCorral()
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
                    txtCapacidad.Text = row["Capacidad"].ToString();
                    
                }
            }

            datos.p_sql = "SELECT TOP 1 * FROM operacion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    //btnRacion.Text = row["Racion"].ToString();
                    txtServida.Text = row["Servida"].ToString();
                    btnTolva.Text = row["Tolva"].ToString();
                    txtPlanta.Text = row["Planta"].ToString();
                }
            }

        }

        private void Raciones_FormClosed(object sender, EventArgs e)
        {
            //if (!comm.cerrarPuerto()) 
            //{
            //    MessageBox.Show(comm.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //}

            //------------------------------------
            //comm.cerrarPuerto();
            //comm.liberarPuerto();
            //------------------------------------
            manejoDePrincipal(true);

            manejoTimerDePrincipal(true);
            manejoTimerDeRaciones(true);
            
            serialPort1.Close();
            serialPort1.Dispose();

            swithSonido(false);

        }

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

        private void posicionBotones()
        {

            btnHecho.Width = this.Width / 2 - 20;
            btnHecho.Height = this.Height / 4 - 30 + 4;
            btnHecho.Top = 0;
            btnHecho.Left = this.Width / 2;

            btnCancelar.Width = btnHecho.Width;
            btnCancelar.Height = btnHecho.Height;
            btnCancelar.Top = btnHecho.Top + btnHecho.Height;
            btnCancelar.Left = btnHecho.Left;

            //btnTara.Width = btnCancelar.Width;
            //btnTara.Height = btnCancelar.Height;
            //btnTara.Top = btnCancelar.Top + btnCancelar.Height;
            //btnTara.Left = btnCancelar.Left;

            panelAnimacion.Width = btnCancelar.Width;
            panelAnimacion.Height = btnCancelar.Height;
            panelAnimacion.Top = btnCancelar.Top + btnCancelar.Height;
            panelAnimacion.Left = btnCancelar.Left;

            panelValores.Width = panelAnimacion.Width;
            panelValores.Height = panelAnimacion.Height;
            panelValores.Top = panelAnimacion.Top + panelAnimacion.Height;
            panelValores.Left = panelAnimacion.Left;

            btnSolicitar.Width = panelAnimacion.Width;
            btnSolicitar.Height = panelAnimacion.Height;
            btnSolicitar.Top = panelAnimacion.Top + panelAnimacion.Height;
            btnSolicitar.Left = panelAnimacion.Left;



            pbFisico.Width = btnCancelar.Width;
            //pbFisico.Height = btnCancelar.Height;
            //btnTara.Top = btnCancelar.Top + btnCancelar.Height;
            pbFisico.Left = btnCancelar.Left;

            pbCargado.Width = btnCancelar.Width;
            //pbCargado.Height = btnCancelar.Height;
            //btnTara.Top = btnCancelar.Top + btnCancelar.Height;
            pbCargado.Left = btnCancelar.Left;

            int ancho = this.Width / 2 - 15;
            int izq = 10;

            picLogo.Width = ancho;
            //picLogo.Height = this.Height / 3 - 30;
            picLogo.Top = 5;
            picLogo.Left = izq;

            //txtRacion.Width = picLogo.Width;
            //txtRacion.Left = picLogo.Left;
            //lblRacion.Left = txtRacion.Left + txtRacion.Width / 2 - lblRacion.Width / 2;

            btnTolva.Width = ancho;
            btnTolva.Left = izq;
            lblTolva.Left = btnTolva.Left + btnTolva.Width / 2 - lblTolva.Width / 2;

            btnRacion.Width = ancho;
            btnRacion.Left = izq;
            lblRacion.Left = btnRacion.Left + btnRacion.Width / 2 - lblRacion.Width / 2;

            txtTamanoV.Width = ancho;
            txtTamanoV.Left = izq;
            lblTamaño.Left = txtTamanoV.Left + txtTamanoV.Width / 2 - lblTamaño.Width / 2;

            txtCargadoV.Width = ancho;
            txtCargadoV.Left = izq;
            lblCargado.Left = txtCargadoV.Left + txtCargadoV.Width / 2 - lblCargado.Width / 2;

            txtFisicoV.Width = ancho;
            txtFisicoV.Left = izq;
            lblFisico.Left = txtFisicoV.Left + txtFisicoV.Width / 2 - lblFisico.Width / 2;

            txtBrutoV.Width = ancho;
            txtBrutoV.Left = izq;
            lblBruto.Left = txtBrutoV.Left + txtBrutoV.Width / 2 - lblBruto.Width / 2;

            txtFaltaV.Width = ancho;
            txtFaltaV.Left = izq;
            lblBruto.Left = txtFaltaV.Left + txtFaltaV.Width / 2 - lblBruto.Width / 2;


            txtStopV.Top = txtBrutoV.Top + 10;
            txtStopV.Left = (txtBrutoV.Left + txtBrutoV.Width) - txtStopV.Width - 10;



        }

        private void btnHecho_Click(object sender, EventArgs e)
        {
            btnGrabar();
        }

        private void btnGrabar()
        {

            bool guardar = true;

            if (Convert.ToInt32(txtBasculaIni.Text) >= Convert.ToInt32(txtBruto.Text))
            {
                return;
            }

            if (guardar)
            {

                Datos datos = new Datos();
                DataTable dt = new DataTable();

                long cargado = 0;
                long cargadoAct = 0;

                datos.p_sql = "insert into cargas (id,fecha,num,racion,carga,hora,tolva,solicito,planta,basculaini,basculafin,cargaini) values(" + vjornada + ",'" + DateTime.Now.ToString("dd-MM-yyyy") + "'," + txtServida.Text + ",'" + btnRacion.Text + "'," + txtCargado.Text + ",'" + DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss") + "','" + btnTolva.Text + "','" + txtSolicitarV.Text + "'," + txtPlanta.Text + "," + txtBasculaIni.Text + "," + txtBruto.Text + "," + txtCargaIni.Text + ")";

                if (!datos.consultaSQL(ref dt, false))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                string maxid = maxId("cargas");

                //datos.p_sql = "SELECT idd FROM raciones WHERE num = " + txtServida.Text + " and racion = '" + btnRacion.Text + "' and id = " + vjornada; //"' and ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " )";

                //if (!datos.consultaSQL(ref dt, true))
                //{
                //    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //}

                //if (dt.Rows.Count == 0)
                //{
                //    datos.p_sql = "insert into raciones (fecha,num,racion,cargado,hora) values('" + DateTime.Now.ToString("dd-MM-yyyy") + "'," + txtServida.Text + ",'" + btnRacion.Text + "'," + txtCargado.Text + ",'" + DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss") + "')";
                //}
                //else
                //{

                datos.p_sql = "SELECT SUM(Carga) as Carga, SUM(BasculaIni) as BasculaIni, SUM(CargaIni) as CargaIni FROM Cargas WHERE num = " + txtServida.Text + " and id = " + vjornada + " and racion = '" + txtRacion.Text + "'";

                if (!datos.consultaSQL(ref dt, true))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        cargado = long.Parse(row["Carga"].ToString()) + long.Parse(row["CargaIni"].ToString()); //+long.Parse(row["BasculaIni"].ToString());
                    }

                }

                cargadoAct = long.Parse(txtCargado.Text) + long.Parse(txtCargaIni.Text);

                datos.p_sql = "UPDATE raciones SET cargado = " + cargado.ToString() + ", carga = " + cargadoAct.ToString() + ", Bascula = " + txtBruto.Text + " , fecha = '" + DateTime.Now.ToString("dd-MM-yyyy") + "', hora = '" + DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss") + "', NumCarga = " + maxid.ToString() + " WHERE num = " + txtServida.Text + " and racion = '" + btnRacion.Text + "' and id = " + vjornada; //"' and ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " )";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    //datos.p_sql = "UPDATE raciones SET cargado = " + txtCargado.Text + ", fecha = '" + DateTime.Now.ToString("dd-MM-yyyy") + "', hora = '" + DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss") + "', NumCarga = " + maxid.ToString() + " WHERE num = " + txtServida.Text + " and racion = '" + btnRacion.Text + "' and id = " + vjornada; //"' and ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " )";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    //}

                if (!datos.consultaSQL(ref dt, false))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                Close();

            }
        
        }

        private string maxId(string tabla)
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            //datos.p_sql = "SELECT AUTO_INCREMENT AS id FROM information_schema.Tables WHERE TABLE_SCHEMA='numcarga' AND table_name='"+tabla+"'";
            datos.p_sql = "SELECT max(idd) as id from " + tabla;

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    return row["id"].ToString();
                }

            }

            return "";
        
        }

        private void timerR_Tick(object sender, EventArgs e)
        {

            //if (!comm.recibePuerto())
            //{
            //    timerR.Enabled = false;
            //    Close();
            //    MessageBox.Show(comm.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //}
            //else
            //{
            //    procesaPeso(comm.p_dato);
            //}

        }

        public void procesaPeso(string dato) 
        {

            int capacidad = 0;
            //int bascula = 0;
            int cargado = 0;
            int fisico = 0;
            int bruto = 0;
            int falta = 0;
            int tamano = 0;
            int tamanoact = 0;
            int basculaini = 0;
            int resultado = 0;
            int entero = 0;
            double decimales = 0;
            double division = 0;
            //int pesoant;
            string dator = "";
            //int maxzeros = 10;

            //pesoant = pesoact;

            //if (dato.Equals("")) { return; }


            dator = dato; //comm.p_dato;

            //if (dator.Equals("0") && pesoant == 0) { vzeros += dator; }
            
            //pesoact = Int32.Parse(dator);

            //---------------------------------------------------------------
            //bascula = Convert.ToInt32(txtBascula.Text);

            //if (pesoact > pesostop) 
            //{ 
            //    pesostop = pesoact;
            //    pesomargen = pesostop - 99999; //50; margen direccion contraria
            //    txtStop.Text = pesostop.ToString();
            //    //if (txtStop.Text == "") { txtStop.Text = "0"; }
            //}
            
            //if (pesoact < pesomargen) 
            //{
            //    dator = pesomargen.ToString();
            //    //lblEstable.Visible = true; 
            //    //return; 
            //}
            //---------------------------------------------------------------

            lblZero.Visible = dator.Equals("0");

            //if (dator.Equals("0"))
            //{
            //    lblZero.Visible = true;
            //}
            //else
            //{
            //    lblZero.Visible = false;
            //}

            //if (!dator.Equals("0")) { txtBruto.Text = dator; }
            txtBruto.Text = dator;

            lblRecibePeso.Visible = ! lblRecibePeso.Visible;

            //if (dator.Equals("0") && vzeros.Length == maxzeros) { txtBruto.Text = dator; }            
            //if (pesoant != 0 || vzeros.Length > maxzeros) { vzeros = ""; }

            if (txtBruto.Text != "") { bruto = Convert.ToInt32(txtBruto.Text); }  
            if (txtFalta.Text != "") { falta = Convert.ToInt32(txtFalta.Text); }
            if (txtCargadoIni.Text != "") { cargado = Convert.ToInt32(txtCargadoIni.Text); }
            if (txtFisicoIni.Text != "")  { fisico = Convert.ToInt32(txtFisicoIni.Text); }
            if (txtCapacidad.Text != "")  { capacidad = Convert.ToInt32(txtCapacidad.Text); }
            if (txtTamano.Text != "")     { tamano = Convert.ToInt32(txtTamano.Text); }
            if (txtTamanoAct.Text != "")  { tamanoact = Convert.ToInt32(txtTamanoAct.Text); }
            if (txtBasculaIni.Text != "")  { basculaini = Convert.ToInt32(txtBasculaIni.Text); }


            resultado = ( cargado + bruto ) - basculaini;
            txtCargado.Text = resultado.ToString();

            resultado = resultado + basculaini;
            txtCarga.Text = resultado.ToString();

            resultado = fisico + bruto;
            txtFisico.Text = resultado.ToString();

            resultado = bruto - tamanoact;
            if (resultado >= 0)
            {
                txtFalta.Text = resultado.ToString();
            }
            else
            {
                txtFalta.Text = resultado.ToString(); //"0";
            }

            long variacion = long.Parse(txtFalta.Text);

            if (variacion > 0)
            {
                txtCargaV.ForeColor = Color.Orange;
            }
            else
            {
                txtCargaV.ForeColor = Color.Lime;
            }


            fomatoCampos();

            txtTamanoV.Refresh();
            txtCargadoV.Refresh();

            //lblEstable.Visible = comm.p_estable;

            lv_pos = lv_pos + 1;

            if (lv_pos == 6)//3)
            {
                lv_pos = 0;
            }

            seriePesos[lv_pos] = txtBruto.Text; //txtServir.Text;

            if (seriePesos[0] == seriePesos[1] 
                && seriePesos[0] == seriePesos[2]
                && seriePesos[0] == seriePesos[3]
                && seriePesos[0] == seriePesos[4]
                && seriePesos[0] == seriePesos[5])          
            {
                //lblEst.ForeColor = Color.Green;
                //lblEst.Text = "Estable";
                vestable = true;
                //lblEstable.Visible = true;
                lblEstable.ForeColor = Color.Lime;
            }
            else
            {
                //lblEst.ForeColor = Color.Red;
                //lblEst.Text = "Variable";
                vestable = false;
                //lblEstable.Visible = false;
                lblEstable.ForeColor = Color.Gray;
            }

            long p1 = long.Parse(cadenaCero(seriePesos[0])) - long.Parse(cadenaCero(seriePesos[1]));
            long p2 = long.Parse(cadenaCero(seriePesos[1])) - long.Parse(cadenaCero(seriePesos[2]));
            long p3 = long.Parse(cadenaCero(seriePesos[2])) - long.Parse(cadenaCero(seriePesos[3]));
            long p4 = long.Parse(cadenaCero(seriePesos[3])) - long.Parse(cadenaCero(seriePesos[4]));
            long p5 = long.Parse(cadenaCero(seriePesos[4])) - long.Parse(cadenaCero(seriePesos[5]));

            int pt = 15;

            if ((p1 >= -pt && p1 <= pt)
                && (p2 >= -pt && p2 <= pt)
                && (p3 >= -pt && p3 <= pt)
                && (p4 >= -pt && p4 <= pt)
                && (p5 >= -pt && p5 <= pt))
            {
                //lblEst.ForeColor = Color.Green;
                //lblEst.Text = "Estable";
                vtolerable = true;
                //lblTolerable.Visible = true;
                lblTolerable.ForeColor = Color.Lime;
                Console.WriteLine("TOLERABLE" + " " + p1.ToString() + "," + p2.ToString() + "," + p3.ToString() + "," + p4.ToString() + "," + p5.ToString());
            }
            else
            {
                //lblEst.ForeColor = Color.Red;
                //lblEst.Text = "Variable";
                vtolerable = false;
                //lblTolerable.Visible = false;
                lblTolerable.ForeColor = Color.Gray;
            }



            //----------------------------------------------------------------------------------------
            resultado = Convert.ToInt32(txtBruto.Text) - Convert.ToInt32(txtBasculaIni.Text);
            if (resultado < 0)
            {
                if (vestable && cantAjustes <= 1) //(resultado > -100)
                {

                    //txtServirIni.Text = txtServirFin.Text; //AJUSTAR
                    //--------------
                    txtBasculaIni.Text = txtBruto.Text; //AJUSTAR
                    cantAjustes++;
                    llenaSolicitar();
                    Console.WriteLine("AUTO AJUSTE CARGA INICIO < FINAL" + " " + DateTime.Now.ToString());
                    //--------------
                    
                }

                //resultado = 0;
            }
            //----------------------------------------------------------------------------------------

            //if (txtBasculaIni.Text == "" || txtBasculaIni.Text == "0")
            //{
            //txtBasculaIni.Text = txtBruto.Text;

            //resultado = Convert.ToInt32(txtTamanoAct.Text) - Convert.ToInt32(txtBasculaIni.Text);
            //if (resultado < 0)
            //{ 
            //    resultado = 0; 
            //}
            //txtSolicitar.Text = resultado.ToString();


            //}

            //calcularPeso();
            //barra(pbFisico, txtCapacidad, txtFisico, txtFisico);


            if (Convert.ToDouble(txtCapacidad.Text) != 0)
            {

                //division = Convert.ToDouble(txtCargado.Text) / Convert.ToDouble(txtTamanoAct.Text);//Convert.ToDouble(txtCapacidad.Text);
                division = Convert.ToDouble(txtCarga.Text) / Convert.ToDouble(txtTamanoAct.Text);//Convert.ToDouble(txtCapacidad.Text);
                division = division * 100;
                resultado = 100 - Convert.ToInt32(division);
                picFisicoF.Height = resultado;
                resultado = 100 - resultado;

                barrar(resultado);

                lblPorcentaje.Text = resultado.ToString() + " %";
                txtCargaPorc.Text = resultado.ToString();


                //calcularEscala();
                //lblVueltas.Text = txtTamano.Text - txtCargadoIni.Text / txtCapacidad.Text;
                division = Convert.ToDouble(txtTamano.Text) / Convert.ToDouble(txtCapacidad.Text);
                //division = division * 100;
                entero = (int)Math.Truncate(division); //Convert.ToInt32(division);
                decimales = division - entero;

                if (decimales > 0)
                {
                    entero = entero + 1;
                }

                resultado = entero;

                lblVueltas.Text = resultado.ToString() + " Cargas"; 

            }

            //barrar(txtCapacidad, txtCargado);
            //barrar(txtTamanoAct, txtCarga);


            //barra(pbCargado, txtTamano, txtServido, txtServido);
            ////barra2();
            ////barra(pbCarga, txtTamanoV, txtCargadoV, txtCargadoV);
            //barra(pbVariacion, txtTamanoV, txtCargadoV, txtFaltaV);

            //fomatoCampos();

        }

        private void btnRacion_Click(object sender, EventArgs e)
        {
            Senga.Captura frm = new Senga.Captura(Convert.ToInt32(vjornada),3);
            frm.Show();
        }

        private void fomatoCampos()
        {

            txtTamanoV.Text = string.Format("{0:#,##0}", long.Parse(txtTamanoAct.Text));
            txtCargadoV.Text = string.Format("{0:#,##0}", long.Parse(txtCargado.Text));
            txtBrutoV.Text = string.Format("{0:#,##0}", long.Parse(txtBruto.Text));
            txtFisicoV.Text = string.Format("{0:#,##0}", long.Parse(txtFisico.Text));
            txtFaltaV.Text = string.Format("{0:#,##0}", long.Parse(txtFalta.Text));
            txtStopV.Text = string.Format("{0:#,##0}", long.Parse(txtStop.Text));
            txtCapacidadV.Text = string.Format("{0:#,##0}", long.Parse(txtCapacidad.Text));
            txtBasculaIniV.Text = string.Format("{0:#,##0}", long.Parse(txtBasculaIni.Text));
            txtCargaIniV.Text = string.Format("{0:#,##0}", long.Parse(txtCargaIni.Text));
            txtCargaV.Text = string.Format("{0:#,##0}", long.Parse(txtCarga.Text));
            txtSolicitarV.Text = string.Format("{0:#,##0}", long.Parse(txtSolicitar.Text));


            //txtCargadoV.BackColor = txtCargado.BackColor;
            //txtCargadoV.ForeColor = txtCargado.ForeColor;

            //txtFisicoV.BackColor = txtFisico.BackColor;
            //txtFisicoV.ForeColor = txtFisico.ForeColor;

        }


        private double barra(ProgressBar barra, TextBox maximo, Label valor, Label campo)
        {

            if (maximo.Text != "")
            {

                String max = maximo.Text;
                String val = valor.Text;

                max = max.Replace(",", "");
                val = val.Replace(",", "");

                int vmaximo = Convert.ToInt32(max);
                int vvalor = Convert.ToInt32(val);

                if (vvalor < 0)
                {
                    vvalor = 0;
                }

                //if (vmaximo < 0) {
                //    vmaximo = 0;
                //}

                barra.Maximum = vmaximo;

                if (vmaximo <= vvalor)
                {
                    vvalor = vmaximo;
                }

                barra.Value = vvalor;

                double porc = Convert.ToDouble(vvalor) / Convert.ToDouble(vmaximo) * 100;

                Comun.sonidoPorc(campo, porc, false);

                return porc;

            }
            else
            {
                return 0;
            }

        }

        //private void colorCampo(TextBox campo, Color fondo, Color letra)
        //{

        //    campo.BackColor = fondo;
        //    campo.ForeColor = letra;

        //}

        private void btnTolva_Click(object sender, EventArgs e)
        {
            Senga.Captura frm = new Senga.Captura(Convert.ToInt32(vjornada),5);
            frm.Show();
        }

        void SerialPort_DataReceived2(object sender, SerialDataReceivedEventArgs e)
        {

            try
            {

                 if (this.Enabled)
                 {

                    if (serialPort1.BytesToRead > 0)
                    {

                        //if (serialPort1.BytesToRead > 0) 
                        //{
                        recibeData += serialPort1.ReadExisting();
                        //    serialPort1.DiscardInBuffer();
                        //}

                        //recibeData = serialPort1.ReadLine();                    

                        if (recibeData.Contains("\r"))
                        {

                            string stringTemp = recibeData;

                            string[] string_array = stringTemp.Split('\r');

                            //for (int i = 0; i < string_array.Length; i++)
                            for (int i = string_array.Length - 1; i >= 0; i--) //desc
                            {

                                stringTemp = string_array[i].Replace(c.ToString(), "").Replace(" ", "").Trim();

                                Console.WriteLine(string_array.Length.ToString() + "-" + i.ToString() + "-" + stringTemp);

                                long number1 = 0;
                                if (long.TryParse(stringTemp, out number1))
                                {
                                    this.BeginInvoke(new DelegaAcceso(si_DataReceived), new object[] { stringTemp });
                                    break;
                                }

                            }

                            recibeData = "";

                        }
                    }

                }

            }
            catch (Exception merr)
            {
                Console.WriteLine("Error " + merr);
            }

        }

        void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            try
            {

                //if (serialPort1.IsOpen)
                if (this.Enabled)
                {
                
                    string indata = serialPort1.ReadExisting();
                    Thread.Sleep(10);
                    //Thread.Sleep(1000);
                    string stringTemp = indata;
                    char c = (char)02;
                    stringTemp = stringTemp.Replace(c.ToString(), "").Replace(" ", "").Trim();

                    string[] string_array = stringTemp.Split('\r');

                    int j = string_array.Length;
                    for (int i = 0; i < j; i++)
                    {
                        stringTemp = string_array[i].Trim();
                        long number1 = 0;
                        if (long.TryParse(stringTemp, out number1))
                        {
                            this.BeginInvoke(new DelegaAcceso(si_DataReceived), new object[] { stringTemp });
                            //Console.WriteLine(stringTemp);
                        }
                    }
                
                }

            }
            catch (Exception merr)
            {
                Console.WriteLine("Error "+merr);
            }


            //if (this.Enabled)
            //{

            //    //try
            //    //{

            //    Thread.Sleep(500);

            //    //SerialPort sp = (SerialPort)sender;
            //    //string indata = sp.ReadExisting();
            //    string indata = serialPort1.ReadExisting();

            //    //indata = indata.Replace("   25", "18005");


            //    string stringTemp = indata;
            //    char c = (char)02;
            //    stringTemp = stringTemp.Replace(c.ToString(), "");
            //    stringTemp = stringTemp.TrimStart();
            //    stringTemp = stringTemp.TrimEnd();

            //    string[] string_array = stringTemp.Split('\r');
            //    int j = string_array.Length;
            //    for (int i = 0; i < j; i++)
            //    {

            //        stringTemp = string_array[i].Trim();

            //        long number1 = 0;
            //        if (long.TryParse(stringTemp, out number1))
            //        {
            //            displayFunction(stringTemp);
            //            Console.WriteLine(stringTemp);

            //        }

            //    }

            //    //}
            //    //catch (Exception merr)
            //    //{
            //    //    Console.WriteLine("Error "+merr);
            //    //}

            //}



            //this.Invoke(new UpdateTextEventHandler(UpdateTextBox), new string[] { indata });

            //////rtxtBruto.Text += sp.ReadExisting() + "\n";
            //////Console.Write("Data Received:");
            //////Console.WriteLine(indata);
            ///


            //try
            //{

            //    SerialPort port = (SerialPort)sender;

            //    //-----------------------------------------------------
            //    //byte[] _buffer = new byte[port.BytesToRead];
            //    //port.Read(_buffer, 0, _buffer.Length);

            //    ////Parse buffer
            //    //string sdata = "";
            //    //Encoding encoding = ASCIIEncoding.GetEncoding(1252);
            //    //if (null != _buffer)
            //    //{
            //    //    sdata = encoding.GetString(_buffer);
            //    //}
            //    //-----------------------------------------------------

            //    string sdata = port.ReadExisting();

            //    //-----------------------------------------------------

            //    string[] string_array = sdata.Split('\r');
            //    string Peso = null;
            //    for (int i = 0; i < string_array.Length; i++)
            //    {
            //        string stringTemp = string_array[i];

            //        char c = (char)02;
            //        stringTemp = stringTemp.Replace(c.ToString(), "");
            //        stringTemp = stringTemp.TrimStart();
            //        stringTemp = stringTemp.TrimEnd();

            //        Peso = stringTemp;
            //        vpeso = stringTemp;

            //        if (Peso != null && Peso != "")
            //        {
            //            //Invoke(new UpdateTextEventHandler(UpdateTextBox), new string[] { Peso });
            //            Invoke(new EventHandler(ShowData));
            //        }

            //        //break;

            //    }


            //}
            //catch (Exception merr)
            //{

            //}

        }

        //private void ShowData(object sender, EventArgs e)
        //{
        //    rtxtBruto.AppendText(vpeso);
        //}

        //private void UpdateTextBox(string text)
        //{
        //    if (!string.IsNullOrEmpty(text))
        //    {
        //        //rtxtBruto.Text += text;
        //        rtxtBruto.Text += text;
        //        rtxtBruto.Text += "\n";
        //    }
        //}

        private void si_DataReceived(string accion) 
        {

            txtBascula.Text = accion;
            txtBascula.Refresh();
            procesaPeso(txtBascula.Text);

            Console.WriteLine(accion);

        }

        //void displayText(string x)
        //{
        //    if (txtBascula.InvokeRequired)
        //        txtBascula.Invoke(displayFunction, new object[] { x });
        //    else
        //        txtBascula.Text = x;

        //    //////procesaPeso(txtBascula.Text);
            
            
        //    //procesaPeso(txtBascula.Text.Replace(",", "").Trim());

        //}

        private void manejoTimerDePrincipal(Boolean activar)
        {
            //ITimer miInterfaz = this.Owner as ITimer;
            //if (miInterfaz != null)
            //{
            //    if (activar)
            //    {
            //        miInterfaz.Start();

            //    }
            //    else
            //    {
            //        miInterfaz.Stop();
            //    }
            //}
            //this.Dispose();

            if (activar)
            {
                Principal.instance.timerWs.Start();
                Principal.instance.timerGps.Start();
                Principal.instance.timerRecorrido.Start();
                Principal.instance.timerBateria.Start();
            }
            else
            {
                Principal.instance.timerWs.Stop();
                Principal.instance.timerGps.Stop();
                Principal.instance.timerRecorrido.Stop();
                Principal.instance.timerBateria.Stop();
            }

        }

        private void manejoTimerDeRaciones(Boolean activar)
        {

            if (activar)
            {
                Raciones.instance.timerGPS.Start();
            }
            else
            {
                Raciones.instance.timerGPS.Stop();
            }

        }

        private void txtBrutoV_TextChanged(object sender, EventArgs e)
        {
            //procesaPeso(txtBrutoV.Text.Replace(",","").Trim());
        }

        private void timerBeep_Tick(object sender, EventArgs e)
        {

            //barra(pbCargado, txtTamano, txtServido, txtServido);
            
            //fomatoCampos();

            double porc = barra(pbVariacion, txtTamanoV, txtCargaV, txtFaltaV);
            //long variacion = long.Parse(txtFalta.Text);

            //if (variacion > 0)
            //{

            //    txtCargaV.ForeColor = Color.Orange;

            //    if (beepOn)
            //    {
            //        //InitializeSound();
            //        beepOn = false;
            //    }
            //}
            //else
            //{
            //    txtCargaV.ForeColor = Color.Lime;
            //}

        }

        private void InitializeSound()
        {
            player.LoadCompleted += delegate (object sender, AsyncCompletedEventArgs e)
            {
                playSound(false);
            };
            player.LoadAsync();

        }

        private void swithSonido(Boolean valor)
        {
            if (valor)
            {
                playSound(true);
            }
            else
            {
                if (player != null)
                {
                    player.Stop();
                }
            }
        }

        public void playSound(bool looping)
        {
            if (looping)
            {
                player.PlayLooping();
            }
            else
            {
                player.Play();
            }
        }

        private void colorPantalla()
        {

            Color colorFondo = Color.Black;
            Color colorFondoW = Color.White;
            Color colorFondoP = Color.Brown;
            Color colorLetra = Color.Lime;
            Color colorLetraH = Color.Yellow;
            Color colorLetraT = Color.White;

            this.BackColor = colorFondoW;

            panelBarra.BackColor = colorFondo;

            txtRacion.BackColor = colorFondo;
            txtRacion.ForeColor = colorLetraH;

            txtTamanoV.BackColor = colorFondo;
            txtTamanoV.ForeColor = colorLetra;

            txtCargadoV.BackColor = colorFondo;
            txtCargadoV.ForeColor = colorLetra;

            txtFaltaV.BackColor = colorFondo;
            txtFaltaV.ForeColor = colorLetra;

            txtCargaPorc.BackColor = colorFondo;
            txtCargaPorc.ForeColor = colorLetra;

            txtCapacidadV.BackColor = colorFondo;
            txtCapacidadV.ForeColor = colorLetra;

            txtBasculaIniV.BackColor = colorFondo;
            txtBasculaIniV.ForeColor = colorLetra;

            txtCargaIniV.BackColor = colorFondo;
            txtCargaIniV.ForeColor = colorLetra;

            txtCargaV.BackColor = colorFondo;
            txtCargaV.ForeColor = colorLetra;

            txtSolicitarV.BackColor = colorFondo;
            txtSolicitarV.ForeColor = colorLetra;

            txtBrutoV.BackColor = colorFondo;
            txtBrutoV.ForeColor = colorLetra;

            panelSolicitar.BackColor = colorFondoP;
            panelVariacion.BackColor = colorFondoP;
            panelBascula.BackColor = colorFondoP;
            panelRacion.BackColor = colorFondoP;
            panelTolva.BackColor = colorFondoP;
            panelCargaPorc.BackColor = colorFondoP;
            panelCapacidad.BackColor = colorFondoP;
            panelBasculaIni.BackColor = colorFondoP;
            panelCargaIni.BackColor = colorFondoP;
            panelRecibio.BackColor = colorFondoP;

            lblTamaño.ForeColor = colorLetraT;
            lblFisico.ForeColor = colorLetraT;
            lblBruto.ForeColor = colorLetraT;
            lblRacion.ForeColor = colorLetraT;
            lblTolva.ForeColor = colorLetraT;
            lblCargaPorc.ForeColor = colorLetraT;
            lblCapacidad.ForeColor = colorLetraT;
            lblBasculaIni.ForeColor = colorLetraT;
            lblCargaIni.ForeColor = colorLetraT;
            lblRecibio.ForeColor = colorLetraT;

        }

        //private void barrar(TextBox maximo, TextBox valor)
        private void barrar(int porc)
        {

            if (porc <= 0)
            {

                pic1.BackColor = Color.Gray;
                pic2.BackColor = Color.Gray;
                pic3.BackColor = Color.Gray;
                pic4.BackColor = Color.Gray;
                pic5.BackColor = Color.Gray;
                pic6.BackColor = Color.Gray;
                pic7.BackColor = Color.Gray;
                pic8.BackColor = Color.Gray;
                pic9.BackColor = Color.Gray;
                pic10.BackColor = Color.Gray;

                //pic1.Visible = false;
                //pic2.Visible = false;
                //pic3.Visible = false;
                //pic4.Visible = false;
                //pic5.Visible = false;
                //pic6.Visible = false;
                //pic7.Visible = false;
                //pic8.Visible = false;
                //pic9.Visible = false;
                //pic10.Visible = false;
            }

            if (porc > 0 && porc < 20)
            {
                pic1.BackColor = Color.Lime;
                pic2.BackColor = Color.Gray;
                pic3.BackColor = Color.Gray;
                pic4.BackColor = Color.Gray;
                pic5.BackColor = Color.Gray;
                pic6.BackColor = Color.Gray;
                pic7.BackColor = Color.Gray;
                pic8.BackColor = Color.Gray;
                pic9.BackColor = Color.Gray;
                pic10.BackColor = Color.Gray;

                //pic1.Visible = true;
                //pic2.Visible = false;
                //pic3.Visible = false;
                //pic4.Visible = false;
                //pic5.Visible = false;
                //pic6.Visible = false;
                //pic7.Visible = false;
                //pic8.Visible = false;
                //pic9.Visible = false;
                //pic10.Visible = false;
            }

            if (porc >= 20 && porc < 30)
            {

                pic1.BackColor = Color.Lime;
                pic2.BackColor = Color.Lime;
                pic3.BackColor = Color.Gray;
                pic4.BackColor = Color.Gray;
                pic5.BackColor = Color.Gray;
                pic6.BackColor = Color.Gray;
                pic7.BackColor = Color.Gray;
                pic8.BackColor = Color.Gray;
                pic9.BackColor = Color.Gray;
                pic10.BackColor = Color.Gray;

                //pic1.Visible = true;
                //pic2.Visible = true;
                //pic3.Visible = false;
                //pic4.Visible = false;
                //pic5.Visible = false;
                //pic6.Visible = false;
                //pic7.Visible = false;
                //pic8.Visible = false;
                //pic9.Visible = false;
                //pic10.Visible = false;
            }

            if (porc >= 30 && porc < 40)
            {
                pic1.BackColor = Color.Lime;
                pic2.BackColor = Color.Lime;
                pic3.BackColor = Color.Lime;
                pic4.BackColor = Color.Gray;
                pic5.BackColor = Color.Gray;
                pic6.BackColor = Color.Gray;
                pic7.BackColor = Color.Gray;
                pic8.BackColor = Color.Gray;
                pic9.BackColor = Color.Gray;
                pic10.BackColor = Color.Gray;

                //pic1.Visible = true;
                //pic2.Visible = true;
                //pic3.Visible = true;
                //pic4.Visible = false;
                //pic5.Visible = false;
                //pic6.Visible = false;
                //pic7.Visible = false;
                //pic8.Visible = false;
                //pic9.Visible = false;
                //pic10.Visible = false;
            }

            if (porc >= 40 && porc < 50)
            {
                pic1.BackColor = Color.Lime;
                pic2.BackColor = Color.Lime;
                pic3.BackColor = Color.Lime;
                pic4.BackColor = Color.Lime;
                pic5.BackColor = Color.Gray;
                pic6.BackColor = Color.Gray;
                pic7.BackColor = Color.Gray;
                pic8.BackColor = Color.Gray;
                pic9.BackColor = Color.Gray;
                pic10.BackColor = Color.Gray;

                //pic1.Visible = true;
                //pic2.Visible = true;
                //pic3.Visible = true;
                //pic4.Visible = true;
                //pic5.Visible = false;
                //pic6.Visible = false;
                //pic7.Visible = false;
                //pic8.Visible = false;
                //pic9.Visible = false;
                //pic10.Visible = false;
            }

            if (porc >= 50 && porc < 60)
            {
                pic1.BackColor = Color.Lime;
                pic2.BackColor = Color.Lime;
                pic3.BackColor = Color.Lime;
                pic4.BackColor = Color.Lime;
                pic5.BackColor = Color.Lime;
                pic6.BackColor = Color.Gray;
                pic7.BackColor = Color.Gray;
                pic8.BackColor = Color.Gray;
                pic9.BackColor = Color.Gray;
                pic10.BackColor = Color.Gray;
                //pic1.Visible = true;
                //pic2.Visible = true;
                //pic3.Visible = true;
                //pic4.Visible = true;
                //pic5.Visible = true;
                //pic6.Visible = false;
                //pic7.Visible = false;
                //pic8.Visible = false;
                //pic9.Visible = false;
                //pic10.Visible = false;
            }

            if (porc >= 60 && porc < 70)
            {
                pic1.BackColor = Color.Lime;
                pic2.BackColor = Color.Lime;
                pic3.BackColor = Color.Lime;
                pic4.BackColor = Color.Lime;
                pic5.BackColor = Color.Lime;
                pic6.BackColor = Color.Lime;
                pic7.BackColor = Color.Gray;
                pic8.BackColor = Color.Gray;
                pic9.BackColor = Color.Gray;
                pic10.BackColor = Color.Gray;
                //pic1.Visible = true;
                //pic2.Visible = true;
                //pic3.Visible = true;
                //pic4.Visible = true;
                //pic5.Visible = true;
                //pic6.Visible = true;
                //pic7.Visible = false;
                //pic8.Visible = false;
                //pic9.Visible = false;
                //pic10.Visible = false;
            }

            if (porc >= 70 && porc < 80)
            {
                pic1.BackColor = Color.Lime;
                pic2.BackColor = Color.Lime;
                pic3.BackColor = Color.Lime;
                pic4.BackColor = Color.Lime;
                pic5.BackColor = Color.Lime;
                pic6.BackColor = Color.Lime;
                pic7.BackColor = Color.Lime;
                pic8.BackColor = Color.Gray;
                pic9.BackColor = Color.Gray;
                pic10.BackColor = Color.Gray;
                //pic1.Visible = true;
                //pic2.Visible = true;
                //pic3.Visible = true;
                //pic4.Visible = true;
                //pic5.Visible = true;
                //pic6.Visible = true;
                //pic7.Visible = true;
                //pic8.Visible = false;
                //pic9.Visible = false;
                //pic10.Visible = false;
            }

            if (porc >= 80 && porc < 90)
            {
                pic1.BackColor = Color.Lime;
                pic2.BackColor = Color.Lime;
                pic3.BackColor = Color.Lime;
                pic4.BackColor = Color.Lime;
                pic5.BackColor = Color.Lime;
                pic6.BackColor = Color.Lime;
                pic7.BackColor = Color.Lime;
                pic8.BackColor = Color.Lime;
                pic9.BackColor = Color.Gray;
                pic10.BackColor = Color.Gray;
                //pic1.Visible = true;
                //pic2.Visible = true;
                //pic3.Visible = true;
                //pic4.Visible = true;
                //pic5.Visible = true;
                //pic6.Visible = true;
                //pic7.Visible = true;
                //pic8.Visible = true;
                //pic9.Visible = false;
                //pic10.Visible = false;
            }

            if (porc >= 90 && porc < 100)
            {
                pic1.BackColor = Color.Lime;
                pic2.BackColor = Color.Lime;
                pic3.BackColor = Color.Lime;
                pic4.BackColor = Color.Lime;
                pic5.BackColor = Color.Lime;
                pic6.BackColor = Color.Lime;
                pic7.BackColor = Color.Lime;
                pic8.BackColor = Color.Lime;
                pic9.BackColor = Color.Lime;
                pic10.BackColor = Color.Gray;
                //pic1.Visible = true;
                //pic2.Visible = true;
                //pic3.Visible = true;
                //pic4.Visible = true;
                //pic5.Visible = true;
                //pic6.Visible = true;
                //pic7.Visible = true;
                //pic8.Visible = true;
                //pic9.Visible = true;
                //pic10.Visible = false;
            }

            if (porc >= 100)
            {
                pic1.BackColor = Color.Lime;
                pic2.BackColor = Color.Lime;
                pic3.BackColor = Color.Lime;
                pic4.BackColor = Color.Lime;
                pic5.BackColor = Color.Lime;
                pic6.BackColor = Color.Lime;
                pic7.BackColor = Color.Lime;
                pic8.BackColor = Color.Lime;
                pic9.BackColor = Color.Lime;
                pic10.BackColor = Color.Lime;
                //pic1.Visible = true;
                //pic2.Visible = true;
                //pic3.Visible = true;
                //pic4.Visible = true;
                //pic5.Visible = true;
                //pic6.Visible = true;
                //pic7.Visible = true;
                //pic8.Visible = true;
                //pic9.Visible = true;
                //pic10.Visible = true;
            }

            //if (porc <= 0)
            //{
            //    pic1.Visible = false;
            //    pic2.Visible = false;
            //    pic3.Visible = false;
            //    pic4.Visible = false;
            //    pic5.Visible = false;
            //    pic6.Visible = false;
            //    pic7.Visible = false;
            //    pic8.Visible = false;
            //    pic9.Visible = false;
            //    pic10.Visible = false;
            //}

            //if (porc > 0 && porc < 20)
            //{
            //    pic1.Visible = true;
            //    pic2.Visible = false;
            //    pic3.Visible = false;
            //    pic4.Visible = false;
            //    pic5.Visible = false;
            //    pic6.Visible = false;
            //    pic7.Visible = false;
            //    pic8.Visible = false;
            //    pic9.Visible = false;
            //    pic10.Visible = false;
            //}

            //if (porc >= 20 && porc < 30)
            //{
            //    pic1.Visible = true;
            //    pic2.Visible = true;
            //    pic3.Visible = false;
            //    pic4.Visible = false;
            //    pic5.Visible = false;
            //    pic6.Visible = false;
            //    pic7.Visible = false;
            //    pic8.Visible = false;
            //    pic9.Visible = false;
            //    pic10.Visible = false;
            //}

            //if (porc >= 30 && porc < 40)
            //{
            //    pic1.Visible = true;
            //    pic2.Visible = true;
            //    pic3.Visible = true;
            //    pic4.Visible = false;
            //    pic5.Visible = false;
            //    pic6.Visible = false;
            //    pic7.Visible = false;
            //    pic8.Visible = false;
            //    pic9.Visible = false;
            //    pic10.Visible = false;
            //}

            //if (porc >= 40 && porc < 50)
            //{
            //    pic1.Visible = true;
            //    pic2.Visible = true;
            //    pic3.Visible = true;
            //    pic4.Visible = true;
            //    pic5.Visible = false;
            //    pic6.Visible = false;
            //    pic7.Visible = false;
            //    pic8.Visible = false;
            //    pic9.Visible = false;
            //    pic10.Visible = false;
            //}

            //if (porc >= 50 && porc < 60)
            //{
            //    pic1.Visible = true;
            //    pic2.Visible = true;
            //    pic3.Visible = true;
            //    pic4.Visible = true;
            //    pic5.Visible = true;
            //    pic6.Visible = false;
            //    pic7.Visible = false;
            //    pic8.Visible = false;
            //    pic9.Visible = false;
            //    pic10.Visible = false;
            //}

            //if (porc >= 60 && porc < 70)
            //{
            //    pic1.Visible = true;
            //    pic2.Visible = true;
            //    pic3.Visible = true;
            //    pic4.Visible = true;
            //    pic5.Visible = true;
            //    pic6.Visible = true;
            //    pic7.Visible = false;
            //    pic8.Visible = false;
            //    pic9.Visible = false;
            //    pic10.Visible = false;
            //}

            //if (porc >= 70 && porc < 80)
            //{
            //    pic1.Visible = true;
            //    pic2.Visible = true;
            //    pic3.Visible = true;
            //    pic4.Visible = true;
            //    pic5.Visible = true;
            //    pic6.Visible = true;
            //    pic7.Visible = true;
            //    pic8.Visible = false;
            //    pic9.Visible = false;
            //    pic10.Visible = false;
            //}

            //if (porc >= 80 && porc < 90)
            //{
            //    pic1.Visible = true;
            //    pic2.Visible = true;
            //    pic3.Visible = true;
            //    pic4.Visible = true;
            //    pic5.Visible = true;
            //    pic6.Visible = true;
            //    pic7.Visible = true;
            //    pic8.Visible = true;
            //    pic9.Visible = false;
            //    pic10.Visible = false;
            //}

            //if (porc >= 90 && porc < 100)
            //{
            //    pic1.Visible = true;
            //    pic2.Visible = true;
            //    pic3.Visible = true;
            //    pic4.Visible = true;
            //    pic5.Visible = true;
            //    pic6.Visible = true;
            //    pic7.Visible = true;
            //    pic8.Visible = true;
            //    pic9.Visible = true;
            //    pic10.Visible = false;
            //}

            //if (porc >= 100)
            //{
            //    pic1.Visible = true;
            //    pic2.Visible = true;
            //    pic3.Visible = true;
            //    pic4.Visible = true;
            //    pic5.Visible = true;
            //    pic6.Visible = true;
            //    pic7.Visible = true;
            //    pic8.Visible = true;
            //    pic9.Visible = true;
            //    pic10.Visible = true;
            //}

        }

        private string cadenaCero(string valor)
        {

            if (valor.Equals(""))
            {
                valor = "0";
            }

            return valor;

        }

        private void btnSolicitar_Click(object sender, EventArgs e)
        {

        }

        private void btnAjustar_Click(object sender, EventArgs e)
        {
            Console.Beep(1400, 25);

            if (Convert.ToInt32(txtBasculaIni.Text) < Convert.ToInt32(txtBruto.Text))
            {

                if (Comun.mensajeVer("¿ Desea Ajustar ?", Constants.MSG_BOTON_SINO, Constants.MSG_IMG_WARNING) == Constants.MSG_BOTON_SI)
                {
                    txtBasculaIni.Text = txtBruto.Text;
                    llenaSolicitar();
                }
            
            }
            
            if (Convert.ToInt32(txtBasculaIni.Text) > Convert.ToInt32(txtBruto.Text))
            {
                //txtServirIni.Text = txtServirFin.Text;
                txtBasculaIni.Text = txtBruto.Text;
                llenaSolicitar();

            }

        }
        private void llenaSolicitar() 
        {

            int resultado;

            resultado = Convert.ToInt32(txtTamanoAct.Text) - Convert.ToInt32(txtBasculaIni.Text);
            txtSolicitar.Text = resultado.ToString();

        }

        private void timerBotonAlerta_Tick(object sender, EventArgs e)
        {

            int resultado = 0;

            //btnHecho.Text = "HECHO";

            //if (txtServirIni.Text != "" && txtServirFin.Text != "")
            if (txtBasculaIni.Text != "" && txtBruto.Text != "")
            {
                //resultado = Convert.ToInt32(txtServirIni.Text) - Convert.ToInt32(txtServirFin.Text);
                resultado = Convert.ToInt32(txtBasculaIni.Text) - Convert.ToInt32(txtBruto.Text);

                if (resultado > 0)
                {

                    //btnHecho.Text = "AJUSTAR";

                    //if (btnHecho.BackColor == SystemColors.Control)
                    //if (btnAjustar.BackColor == SystemColors.Control)
                    //if (txtBasculaIniV.BackColor == Color.Black)
                    if (txtCargadoV.BackColor == Color.Black)
                    {
                        //btnHecho.BackColor = Color.Yellow;
                        //btnHecho.ForeColor = Color.Black;
                        //txtServirIniV.BackColor = Color.Yellow;
                        //txtBasculaIniV.BackColor = Color.Yellow;
                        txtCargadoV.BackColor = Color.Yellow;
                        //btnAjustar.BackColor = Color.Yellow;
                        //btnAjustar.ForeColor = Color.Black;

                    }
                    else
                    {
                        //btnServirIniV.BackColor = SystemColors.Control;

                        //btnHecho.BackColor = SystemColors.Control;
                        //btnHecho.ForeColor = Color.Black;
                        //txtServirIniV.BackColor = Color.Black;
                        //txtBasculaIniV.BackColor = Color.Black;
                        txtCargadoV.BackColor = Color.Black;
                        //btnAjustar.BackColor = SystemColors.Control;
                        //btnAjustar.ForeColor = Color.Black;

                    }

                    //Console.Beep(900, 100);
                    //Console.Beep(1420, 25);
                    Console.WriteLine("CARGA INICIO < FINAL" + " " + DateTime.Now.ToString());

                }
                else
                {
                    //btnHecho.BackColor = SystemColors.Control;
                    //btnHecho.ForeColor = Color.Black;
                    //txtServirIniV.BackColor = Color.Black;
                    //txtBasculaIniV.BackColor = Color.Black;
                    txtCargadoV.BackColor = Color.Black;
                    //btnAjustar.BackColor = SystemColors.Control;
                    //btnAjustar.ForeColor = Color.Black;
                }

            }


        }

        private void manejoDePrincipal(Boolean activar)
        {

            if (activar)
            {

                try
                {
                    Principal.instance.serialPortGps.Open();
                    //Principal.instance.pauseGps = true;
                }
                catch { }

            }
            else
            {
                try
                {
                    Principal.instance.serialPortGps.Close();
                    //Principal.instance.pauseGps = false;
                }
                catch { }

            }

        }



    }

}