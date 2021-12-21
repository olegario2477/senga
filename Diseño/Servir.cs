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

    public partial class Servir : Form
    {

        private delegate void DelegaAcceso(string accion);

        private Comm comm = new Comm();
        private SerialPort serialPort1 = new SerialPort();
        
        //private SoundPlayer player = new SoundPlayer(Properties.Resources.beep_censor);

        string vidd;
        string viddr;
        long servido = 0;
        string vjornada;
        string vcorralgps;

        int lv_pos = 0;
        int lv_posr = 0;
        string[] seriePesos = new string[] { "0", "0", "0", "0", "0", "0" };
        string[] establer = new string[] { "0", "0" };
        int estr = 0;
        bool vestable = false;
        bool vtolerable = false;
        int pesoact = 0;
        int pesoant = 0;
        string vzeros = "";
        double minvariacion = 0;
        double vlat;
        double vlng;
        string vnumcarga;
        bool RecibirPeso = false;
        bool beepOn = true;
        int cantAjustes = 1;
        string vcorrales = "";
        char c = (char)02;
        char r = '\r';
        char n = '\n';
        string recibeData = "";
        int vcontLeerGps = 0;
        //private string RxString;
        string bascula = "0";
        int cantPesos = 1;

        //public delegate void AddDataDelegate(String myString);
        //public AddDataDelegate myDelegate;

        //public static Servir instance = null;

        public Servir(string idd, string iddr, string id, string corralgps, double lat, double lng, string numcarga, string corrales)
        {
            InitializeComponent();
            //instance = this;

            vidd = idd;
            viddr = iddr;
            vjornada = id;
            //----------------------------------------
            vcorralgps = "0000";//corralgps;
            vlat = 0;// lat;
            vlng = 0;// lng;
            //----------------------------------------
            vnumcarga = numcarga;
            vcorrales = corrales;
        }

        private void Servir_Load(object sender, EventArgs e)
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();
                     

            txtBasculaIniV.Text = "0";
            txtServirFinV.Text = "0";
            txtServirV.Text = "0";
            txtVariacionV.Text = "0";
            txtVariacion.Text = "0";
            txtPlanV.Text = "0";
            txtPlan.Text = "0";
            txtPorc.Text = "0";
            txtVarCab.Text = "0";
            txtNumCarga.Text = "0";



            //manejoDePrincipal(false);
            
            manejoTimerDePrincipal(false);
            manejoTimerDeServidas(false);

            //iniciaPesoOpc1();
            iniciaPesoOpc2();
            colorPantalla();

            txtNumCarga.Text = vnumcarga;
            
            datos.p_sql = "SELECT Id,Num,Corral,Racion,Planeado,Realidad,Cabezas FROM servidas WHERE idd = " + vidd;

            if (!datos.consultaSQL(ref dt, true))
            {
                //MessageBox.Show(datos.p_error);
                this.Close();
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    txtCorral.Text = row["Corral"].ToString();
                    txtRacion.Text = row["Racion"].ToString();
                    servido = long.Parse(row["Realidad"].ToString());
                    txtPlanIni.Text = row["Planeado"].ToString();
                    long planeado = long.Parse(row["Planeado"].ToString()) - servido;
                    txtPlan.Text = planeado.ToString();
                    txtServir.Text = "0";
                    txtCabezas.Text = row["Cabezas"].ToString();
                    txtServida.Text = row["Num"].ToString();
                    txtJornada.Text = row["Id"].ToString();
                 
                    //long variacion = Convert.ToInt32(row["Realidad"]) - Convert.ToInt32(row["Planeado"]);
                    //txtVariacion.Text = variacion.ToString();

                    //double porcentaje = Convert.ToDouble(row["Realidad"]) / Convert.ToDouble(row["Planeado"]);
                    //txtPorcentaje.Text = Math.Round(porcentaje * 100, 1).ToString() + " %";

                    calcularPeso();
                    //barra(pbServido,txtPlan,txtServir);

                }

                llenaSigCorral();

                long resultado = long.Parse(vcorrales); //- 1;
                txtFaltaCorral.Text = resultado.ToString();

                //abrirPuerto();
                //string portname = "";

                //datos.p_sql = "SELECT TOP 1 * FROM configuracion";

                //if (!datos.consultaSQL(ref dt, true))
                //{
                //    MessageBox.Show(datos.p_error);
                //}
                //else
                //{
                //    foreach (DataRow row in dt.Rows)
                //    {
                //        portname = row["Puerto"].ToString();
                //    }
                //}

                //if (txtCorral.Text.Equals(vcorralgps))
                //{
                //    panel1.BackColor = Color.Green;
                //}
                //else
                //{
                //    panel1.BackColor = Color.Red;
                //}          

                minvariacion = minimaVariacion();

                txtBasculaIni.Text = "0";
                txtServirFin.Text = "0";

                txtServidoT.Text = "0";

                datos.p_sql = "SELECT count(corral) as Corrales , sum(planeado) as Planeado, sum(realidad) as Realidad FROM servidas WHERE racion = '" + txtRacion.Text + "' and num = " + txtServida.Text + " and id = " + vjornada; //" and  ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ";

                if (!datos.consultaSQL(ref dt, true))
                {
                    MessageBox.Show(datos.p_error);
                    //this.Close();
                }
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {

                        if (row["Corrales"].ToString() != "0")
                        {
                            //maximo = Convert.ToInt32(row["Planeado"].ToString());
                            txtServidoT.Text = row["Realidad"].ToString();
                            //corrales = Convert.ToInt32(row["Corrales"].ToString());
                        }
                    }
                }

                //txtCantidadT.Text = "0";

                //datos.p_sql = "SELECT count(corral) as Corrales , sum(planeado) as Planeado, sum(realidad) as Realidad FROM servidas WHERE hecho = 'S' and racion = '" + txtRacion.Text + "' and num = " + txtServida.Text + " and id = " + vjornada; //" and  ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ";

                //if (!datos.consultaSQL(ref dt, true))
                //{
                //    MessageBox.Show(datos.p_error);
                //    //this.Close();
                //}
                //else
                //{
                //    foreach (DataRow row in dt.Rows)
                //    {

                //        if (row["Corrales"].ToString() != "0")
                //        {
                //            //maximo = Convert.ToInt32(row["Planeado"].ToString());
                //            //txtServidoT.Text = row["Realidad"].ToString();
                //            txtCantidadT.Text = row["Corrales"].ToString();
                //            //corrales = Convert.ToInt32(row["Corrales"].ToString());
                //        }
                //    }
                //}
                
                txtEscalaIni.Text = "0";

                datos.p_sql = "SELECT cargado as Cargado, Carga, Bascula FROM raciones WHERE idd = " + viddr;

                if (!datos.consultaSQL(ref dt, true))
                {
                    MessageBox.Show(datos.p_error);
                    //this.Close();
                }
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        //txtCorral.Text = row["Corral"].ToString();
                        //txtRacion.Text = row["Racion"].ToString();
                        //servido = Convert.ToInt32(row["Realidad"].ToString());

                        //int planeado = Convert.ToInt32(row["Planeado"].ToString()) - servido;
                        //txtPlan.Text = planeado.ToString();
                        //txtServir.Text = "0";
                        //txtCabezas.Text = row["Cabezas"].ToString();

                        //txtEscala.Text = row["Cargado"].ToString();
                        txtEscalaIni.Text = row["Cargado"].ToString();
                        txtServirIni.Text = Comun.cadenaCero(row["Carga"].ToString());
                        //-----------------
                        txtBasculaIni.Text = Comun.cadenaCero(row["Bascula"].ToString());
                        //-----------------

                        calcularEscala();
                        //barra2();

                    }
                }

                string dator = "";


                ////iniciaPesoOpc1();
                //iniciaPesoOpc2();

            }


            //InitializeSound();

        }

        private void Servir_FormClosed(object sender, EventArgs e)
        {

            manejoDePrincipal(true);

            manejoTimerDePrincipal(true);
            manejoTimerDeServidas(true);

            //cerrarPuerto();

            //comm.cerrarPuerto();
            //comm.liberarPuerto();

            serialPort1.Close();
            serialPort1.Dispose();

            //swithSonido(false);

        }

        private void iniciaPesoOpc1()
        {

            comm.p_comm = puertoConfig();
            comm.p_baudrate = baudConfig();
            comm.p_databits = dataConfig();
            comm.p_parity = parityConfig();
            comm.p_stopbits = stopConfig();
            comm.p_sp = serialPort1;

            if (!comm.abrirPuerto())
            {
                Close();
                MessageBox.Show(comm.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                timerS.Enabled = true;
            }

        }

        private void iniciaPesoOpc2()
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
                //serialPort1.NewLine = "\r\n";
                serialPort1.Open();

                RecibirPeso = true;

            }
            catch (Exception err)
            {
                Close();
                //MessageBox.Show(err.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Comun.mensajeVer(err.Message + "\n\rVerifique Cable Peso o \n\rConfiguracion del Puerto COM", Constants.MSG_BOTON_OK, Constants.MSG_IMG_WARNING);
                //ErrorLogging(err);
            }


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
        //private void Servir_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        //{

        //    if (e.KeyCode == Keys.Escape) 

        //        this.Close();

        //}

        //public void AddDataMethod(String myString)
        //{
        //    txtServir.AppendText(myString.Trim());
        //}

        //private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{

        //    SerialPort sp = (SerialPort)sender;
        //    string s = sp.ReadExisting(); //+ "\n";

        //    s = cadenaPeso(s);

        //    txtServir.Invoke(this.myDelegate, new Object[] { s });

        //}



        //private string cadenaPeso(string peso)
        //{

        //    string resultado = "0";
        //    //peso = serialPort1.ReadExisting();

        //    string[] lines = peso.Split('\r'); 
        //    char c = (char)02;

        //    peso = lines[0].ToString();
        //    peso = peso.Replace(c.ToString(), "");
        //    peso = peso.Replace("$", "");

        //    while (peso.Contains(" "))
        //    {
        //        peso = peso.Replace(" ", "");
        //    }

        //    if (IsNumbersOnly(peso))
        //    {
        //        resultado = peso.ToString();
        //    }

        //    //calcularPeso();
        //    //barra();

        //    return resultado;

        //}


        //private void abrirPuerto() 
        //{
        //    Datos datos = new Datos();
        //    DataTable dt = new DataTable();

        //    string portname = "";

        //    datos.p_sql = "SELECT TOP 1 * FROM configuracion";

        //    if (!datos.consultaSQL(ref dt, true))
        //    {
        //        MessageBox.Show(datos.p_error);
        //    }
        //    else
        //    {
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            portname = row["Puerto"].ToString();
        //        }
        //    }

        //    if (portname.ToString() != "")
        //    {
        //        serialPort1.PortName = portname;
        //        serialPort1.BaudRate = 9600;
        //        serialPort1.DataBits = 6; //7
        //        serialPort1.Parity = Parity.None;
        //        serialPort1.StopBits = StopBits.One;
        //        serialPort1.Handshake = Handshake.None;

        //        try
        //        {
        //            serialPort1.Open();
        //            //serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);

        //            timer1.Enabled = true;
        //        }
        //        catch
        //        {

        //        }
        //    }
        //}

        //private void cerrarPuerto() 
        //{
        //    try
        //    {
        //        if (serialPort1.IsOpen)
        //        {
        //            //serialPort1.DataReceived -= new SerialDataReceivedEventHandler(serialPort1_DataReceived);
        //            serialPort1.Close();
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}

        //private void barra()
        //{

        //    int maximo = Convert.ToInt32(txtPlan.Text);
        //    int valor = Convert.ToInt32(txtServir.Text);

        //    pbServido.Maximum = maximo;

        //    if (maximo <= valor)
        //    {
        //    valor = maximo;
        //    }
        //    pbServido.Value = valor;

        //    double porc = Convert.ToDouble(valor) / Convert.ToDouble(maximo) * 100;

        //    if (porc < 60)
        //    {
        //        // Beep at 5000 Hz for 1 second
        //        //Console.Beep(1000, 500);
        //        colorCampo(3);
        //    }
        //    else if (porc >= 80 && porc < 90)
        //    {
        //        // Beep at 5000 Hz for 1 second
        //        Console.Beep(900, 100);
        //        colorCampo(2);
        //    }
        //    else if (porc >= 90 && porc < 100)
        //    {
        //        // Beep at 5000 Hz for 1 second
        //        Console.Beep(1000, 100);
        //        colorCampo(2);

        //    }
        //    else if (porc >= 100)
        //    {
        //        // Beep at 5000 Hz for 1 second
        //        //Console.Beep(1000, 2000);
        //        colorCampo(1);
        //    }   

        //}

        //private void barra2() 
        //{


        //    int maximo = Convert.ToInt32(txtEscalaIni.Text);
        //    int valor = (Convert.ToInt32(txtServidoT.Text) + Convert.ToInt32(txtServir.Text));

        //    pbServido2.Maximum = maximo;

        //    if (maximo <= valor)
        //    {
        //        valor = maximo;
        //    }
        //    pbServido2.Value = valor;


        //}


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

        }

        private double barra(ProgressBar barra, TextBox maximo, Label valor, Label color)
        {

            long vmaximo = long.Parse(maximo.Text.Replace(",", ""));
            long vvalor = long.Parse(valor.Text.Replace(",",""));

            //barra.Maximum = vmaximo;

            if (vvalor < 0)
            {
                vvalor = 0;
            }

            if (vmaximo <= vvalor)
            {
                vvalor = vmaximo;
            }

            //barra.Value = vvalor;

            double porc = Convert.ToDouble(vvalor) / Convert.ToDouble(vmaximo) * 100;

            //Comun.sonidoPorc(color, porc, false);
            Comun.sonidoPeso(color, long.Parse(Comun.cadenaCero(txtVariacion.Text)), false);

            return porc;

            //------------------------------------------------------------------------------
            //if (porc > 0 && porc <= 60)
            //{
            //    //swithSonido(false);
            //    colorCampo(color, Color.White, Color.Black);
            //} 
            //else if (porc > 60 && porc <= 90)
            //{
            //    // Beep at 5000 Hz for 1 second
            //    Console.Beep((int)porc * 10, 50);
            //    //SystemSounds.Beep.Play();
            //    //player.LoadAsync();
            //    //Console.Beep(2000, 80);
            //    colorCampo(color, Color.Yellow, Color.Black);
            //}
            //else if (porc > 90 && porc < 100)
            //{
            //    Console.Beep(2000, 100);
            //    colorCampo(color, Color.Orange, Color.White);
            //}
            //else if (porc >= 100)
            //{
            //    Console.Beep(3000, 100);
            //    colorCampo(color, Color.Red, Color.White);
            //}
            //------------------------------------------------------------------------------

            //if (porc > 0 && porc <= 10)
            //    Comun.reproducirRecursoWav(0);
            //else if (porc > 10 && porc <= 20)
            //    Comun.reproducirRecursoWav(10);
            //else if (porc > 20 && porc <= 30)
            //    Comun.reproducirRecursoWav(20);
            //else if (porc > 30 && porc <= 40)
            //    Comun.reproducirRecursoWav(30);
            //else if (porc > 40 && porc <= 50)
            //    Comun.reproducirRecursoWav(40);
            //else if (porc > 50 && porc <= 60)
            //    Comun.reproducirRecursoWav(50);
            //else if (porc > 60 && porc <= 70)
            //    Comun.reproducirRecursoWav(60);
            //else if (porc > 70 && porc <= 80)
            //    Comun.reproducirRecursoWav(70);
            //else if (porc > 80 && porc <= 90)
            //    Comun.reproducirRecursoWav(80);
            //else if (porc > 90 && porc < 100)
            //    Comun.reproducirRecursoWav(90);
            //else if (porc >= 100)
            //    Comun.reproducirRecursoWav(100);

        }

        //private void colorCampo(TextBox campo, Color fondo, Color letra)
        //{

        //    campo.BackColor = fondo;
        //    campo.ForeColor = letra;

        //}
        private void btnHecho_Click(object sender, EventArgs e)
        {
            //true - individual
            //false - fast
            btnGrabar(true);
        }

        private void btnGrabar(Boolean grabacierra) 
        {                      

            btnHecho.Text = "HECHO";

            bool guardar = true;

            //if (!txtServirIni.Text.Equals("") && !txtServirFin.Text.Equals(""))
            if (!txtBasculaIni.Text.Equals("") && !txtServirFin.Text.Equals(""))
            {

                //if (!lblEst.Visible)
                //{
                //if (!txtServir.Text.Equals("0"))
                //{
                //    if (MessageBox.Show("Peso No Estable, ¿ Desea Guardar ?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                //    {
                //        guardar = false;
                //    }
                //}
                //}


                if (Convert.ToInt32(txtBasculaIni.Text) <= Convert.ToInt32(txtServirFin.Text))
                {
                    //Close();
                    return;
                }

                //ajustar
                //if (Convert.ToInt32(txtServirIni.Text) < Convert.ToInt32(txtServirFin.Text))
                //-------------------------------
                //if (Convert.ToInt32(txtBasculaIni.Text) < Convert.ToInt32(txtServirFin.Text))
                //{
                //    //txtServirIni.Text = txtServirFin.Text;

                //    txtBasculaIni.Text = txtServirFin.Text;
                    //guardar = false;                   

                //}
                //-------------------------------

                calcularPeso();

                //-------------------------------------------------------------------------------------------------
                //double porc1 = Convert.ToDouble(txtServir.Text) / Convert.ToDouble(txtPlan.Text) * 100;
                                
                //if (porc1 == 0) 
                //{                    
                //    guardar = false;
                    
                //}
                //else if (porc1 > 100)
                //{
                //    //timerS.Enabled = false;
                //    timerBeep.Enabled = false;
                //    RecibirPeso = false;

                //    porc1 = 100 - porc1;

                //    //if (Math.Abs(Math.Round(porc1, 0)) > minvariacion)
                //    if (Math.Abs(Convert.ToDouble(cadenaCero(txtVariacion.Text))) > minvariacion)
                //    {
                //        //string mensaje = "Peso Servido Excedio al Planeado\r\n¿ Confirma como Hecho ?";
                //        string mensaje = "¿ Confirma Hecho ?";

                //        if (Comun.mensajeVer(mensaje, Constants.MSG_BOTON_SINO_P, Constants.MSG_IMG_WARNING) == Constants.MSG_BOTON_NO)
                //        //if (MessageBox.Show("Peso Servido Excedio " + minvariacion.ToString() + " % del Planeado, ¿ Confirma como Hecho ?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                //        {
                //            guardar = false;
                //            //timerS.Enabled = true;
                //            timerBeep.Enabled = true;
                //            RecibirPeso = true;
                //        }

                //    }

                //}
                //else if (porc1 > 0 && porc1 <= 100)
                //{
                //    //timerS.Enabled = false;
                //    timerBeep.Enabled = false;
                //    RecibirPeso = false;

                //    porc1 = 100 - porc1;

                //    //if (Math.Abs(Math.Round(porc1, 0)) > minvariacion)
                //    if (Math.Abs(Convert.ToDouble(cadenaCero(txtVariacion.Text))) > minvariacion)
                //    {
                //        //string mensaje = "Peso Servido " + txtServir.Text + " Kg\r\nFaltaron " + txtVariacion.Text + " Kg\r\n es Inferior al Planeado\r\n¿ Confirma como Hecho ?";
                //        //string mensaje = "Peso Servido Inferior al Planeado\r\n¿ Confirma como Hecho ?";
                //        string mensaje = "¿ Confirma Hecho ?";

                //        if (Comun.mensajeVer(mensaje, Constants.MSG_BOTON_SINO_P, Constants.MSG_IMG_WARNING) == Constants.MSG_BOTON_NO)
                //        //if (MessageBox.Show("Peso Servido Inferior al Planeado, ¿ Confirma como Hecho ?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                //        {
                //            guardar = false;
                //            //timerS.Enabled = true;
                //            timerBeep.Enabled = true;
                //            RecibirPeso = true;
                //        }

                //    }

                //}

                //--------------------

                double variacion = Convert.ToDouble(Comun.cadenaCero(txtVariacion.Text));

                if (Math.Abs(Convert.ToDouble(Comun.cadenaCero(txtVariacion.Text))) > minvariacion) 
                {

                    timerBeep.Enabled = false;
                    RecibirPeso = false;

                    string mensaje = "¿ Confirma Hecho ?";
                    if (Comun.mensajeVer(mensaje, Constants.MSG_BOTON_SINO_P, Constants.MSG_IMG_WARNING) == Constants.MSG_BOTON_NO)
                    {
                        guardar = false;
                        timerBeep.Enabled = true;
                        RecibirPeso = true;
                    }

                }

                //-------------------------------------------------------------------------------------------------


                if (guardar)
                {
                    Datos datos = new Datos();
                    DataTable dt = new DataTable();

                    int servidot = 0;
                    int cantidadt = 0;
                    string hecho;
                    //double minvariacion = 0;
                    double division = 0;
                    int resultado = 0;

                    //timerS.Enabled = false;
                    RecibirPeso = false;

                    servido = servido + long.Parse(txtServir.Text);                    

                    //long variacion = Convert.ToInt32(txtServir.Text) - Convert.ToInt32(txtPlan.Text);
                    //txtVariacion.Text = variacion.ToString();
                                       
                    //datos.p_sql = "SELECT TOP 1 * FROM configuracion";

                    //if (!datos.consultaSQL(ref dt, true))
                    //{
                    //    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //}
                    //else
                    //{
                    //    foreach (DataRow row in dt.Rows)
                    //    {
                    //        minvariacion = Convert.ToDouble(row["MinVariacion"]);
                    //    }
                    //}

                    //if (Convert.ToDouble(txtVariacion.Text) > -10 && Convert.ToDouble(txtVariacion.Text) < 10)
                    //if (Convert.ToDouble(txtVariacion.Text) >= minvariacion)
                    division = Convert.ToDouble(servido.ToString()) / Convert.ToDouble(txtPlanIni.Text);
                    division = division * 100;
                    resultado = 100 - Convert.ToInt32(division);

                    //if (resultado <= Convert.ToInt32(minvariacion))
                    if ( variacion >= 0 || variacion >= (Convert.ToDouble(minvariacion) * -1))
                    {
                        hecho = "S";
                    }
                    else
                    {
                        hecho = "N";
                    }

                    //if (Convert.ToInt32(servido.ToString()) < Convert.ToInt32(txtPlan.Text))
                    //{
                    //    hecho = "M";
                    //}

                    datos.p_sql = "UPDATE servidas SET corralgps = '" + vcorralgps + "', latitudi = "+ vlat + ", longitudi = " + vlng + ", realidad = '" + servido.ToString() + "', hecho = '" + hecho.ToString() + "', fecha = '" + DateTime.Now.ToString("dd-MM-yyyy") + "', hora = '" + DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss") + "', numcarga = " + txtNumCarga.Text + " , BasculaIni = " + txtBasculaIni.Text + " , BasculaFin = " + txtServirFin.Text + " , CargaIni = " + txtServirIni.Text + " , bascula = " + bascula + " WHERE idd = " + vidd;

                    if (!datos.consultaSQL(ref dt, false))
                    {
                        MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                    datos.p_sql = "UPDATE operacion SET accion = 'S'";

                    if (!datos.consultaSQL(ref dt, true))
                    {
                        MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

  
                    servidot = Convert.ToInt32(txtServir.Text) + Convert.ToInt32(txtServidoT.Text);
                    //servidot = servido + Convert.ToInt32(txtServidoT.Text);

                    cantidadTotal();
                    cantidadt = Convert.ToInt32(txtCantidadT.Text);


                    //FALTA---------------------------------------------------------------------------

                    //long falta = 0;

                    //datos.p_sql = "SELECT sum(planeado) as Planeado, sum(realidad) as Realidad FROM servidas WHERE hecho = 'N' and racion = '" + txtRacion.Text + "' and num = " + txtServida.Text + " and id = " + vjornada; //" and  ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ";
                    ////datos.p_sql = "SELECT sum(planeado) as Planeado, sum(realidad) as Realidad FROM servidas WHERE racion = '" + txtRacion.Text + "' and num = " + txtServida.Text + " and id = " + vjornada; //" and  ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ";

                    //if (!datos.consultaSQL(ref dt, true))
                    //{
                    //    //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //}
                    //else
                    //{
                    //    foreach (DataRow row in dt.Rows)
                    //    {
                    //        if (dt.Rows.Count != 0)
                    //        {
                    //            //falta = long.Parse(cadenaCero(row["Planeado"].ToString()));
                    //            falta = long.Parse(cadenaCero(row["Planeado"].ToString())) - long.Parse(cadenaCero(row["Realidad"].ToString()));
                    //        }
                    //    }
                    //}

                    //if (falta < 0) 
                    //{
                    //    falta = 0;
                    //}

                    long falta = Comun.valorFalta(txtRacion.Text, txtServida.Text, vjornada);

                    //--------------------------------------------------------------------------------

                    long carga = long.Parse(txtServirIni.Text) - long.Parse(txtServir.Text);// long.Parse(servido.ToString());
                    txtCarga.Text = carga.ToString();

                    //datos.p_sql = "UPDATE raciones SET falta = " + falta.ToString() + ", servido = '" + servidot.ToString() + "', hechoc = " + cantidadt.ToString() + ", cargado = " + txtServirFin.Text + " WHERE racion = '" + txtRacion.Text + "' and num = " + txtServida.Text + " and id = " + vjornada; //" and  ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ";
                    //datos.p_sql = "UPDATE raciones SET falta = " + falta.ToString() + ", servido = '" + servidot.ToString() + "', hechoc = " + cantidadt.ToString() + ", carga = " + txtServirFin.Text + " WHERE racion = '" + txtRacion.Text + "' and num = " + txtServida.Text + " and id = " + vjornada; //" and  ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ";
                    datos.p_sql = "UPDATE raciones SET falta = " + falta.ToString() + ", servido = '" + servidot.ToString() + "', hechoc = " + cantidadt.ToString() + ", carga = " + carga.ToString() + ", bascula = "  + txtServirFin.Text + " WHERE racion = '" + txtRacion.Text + "' and num = " + txtServida.Text + " and id = " + vjornada; //" and  ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ";

                    if (!datos.consultaSQL(ref dt, true))
                    {
                        MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                    if (grabacierra)
                    { 
                        Close(); 
                    }                    
                    else 
                    { 
                        nuevoCorral();                        
                    }

                }

            }

        }

        private void nuevoCorral() 
        {


            DataRow row = sigCorral(txtCorral.Text);

            if (row != null) 
            {

                string corral = row["Corral"].ToString();
                string plan = row["Planeado"].ToString();
     
                long vcarga = long.Parse(txtCarga.Text);
                long vplan = long.Parse(plan);

                
                txtCabezas.Text = row["Cabezas"].ToString();
                vidd = row["IdD"].ToString();

                txtCorral.Text = corral;
                txtBasculaIni.Text = txtServirFin.Text;
                txtPlan.Text = plan;
                txtPlanIni.Text = txtPlan.Text;
                txtServir.Text = "0";

                DataRow rowr = regRacion(viddr);
                txtServirIni.Text = rowr["Carga"].ToString();
                txtServidoT.Text = rowr["Servido"].ToString();

                llenaSigCorral();
                long resultado = long.Parse(txtFaltaCorral.Text);//-1;
                txtFaltaCorral.Text = resultado.ToString();

                fomatoCampos();
  
                if (vcarga < vplan) 
                {
                    Close(); 
                    Comun.mensajeVer("Insuficiente Alimento", Constants.MSG_BOTON_OK, Constants.MSG_IMG_WARNING);                    
                } 

            }

            servido = 0;
            RecibirPeso = true;
            cantAjustes = 1;


        }

        private void llenaSigCorral() 
        {

            DataRow row = sigCorral(txtCorral.Text);
            if (row != null)
            {
                txtSigCorral.Text = row["Corral"].ToString();
            }
            else
            {
                txtSigCorral.Text = "";
            }

            

        }

        private DataRow sigCorral(string corral) 
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            datos.p_sql = "SELECT TOP 1 corral,Planeado,Realidad,Cabezas,IdD FROM servidas WHERE hecho = 'N' and racion = '" + txtRacion.Text + "' and num = " + txtServida.Text + " and id = " + vjornada + " and  corral <> '" + corral + "' and Realidad = 0 ORDER BY IdD"; 

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error);
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

        private DataRow regRacion(string idd)
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            datos.p_sql = "SELECT cargado as Cargado, Carga, Servido FROM raciones WHERE idd = " + idd;

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error);
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

        //private string cadenaCero(string valor)
        //{

        //    if (valor.Equals(""))
        //    {
        //        valor = "0";
        //    }

        //    return valor;

        //}

        private void cantidadTotal()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            txtCantidadT.Text = "0";

            datos.p_sql = "SELECT count(corral) as Corrales , sum(planeado) as Planeado, sum(realidad) as Realidad FROM servidas WHERE (hecho = 'S' or hecho = 'X') and racion = '" + txtRacion.Text + "' and num = " + txtServida.Text + " and id = " + vjornada; //" and  ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {

                    if (row["Corrales"].ToString() != "0")
                    {
                        txtCantidadT.Text = row["Corrales"].ToString();
                    }
                }
            }

        }

        private void timerS_Tick(object sender, EventArgs e)
        {

            if (!comm.recibePuerto())
            {
                timerS.Enabled = false;
                Close();
                MessageBox.Show(comm.p_error + " No recibe datos del puerto COMM", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                procesaPeso(comm.p_dato);
            }

        }

        public void procesaPeso(string dato)
        {

            int resultado = 0;
            //int pesoant;
            int difpeso;
            string dator = "";
            //int maxzeros = 10;

            dator = dato; //comm.p_dato;
            pesoact = Int32.Parse(dator);

            difpeso = pesoact - pesoant;

            if (difpeso == 0)
            {
                Console.WriteLine(pesoact.ToString());
            }
            else
            {
                Console.WriteLine(pesoact.ToString() + " ant " + pesoant.ToString() + " dif " + difpeso.ToString());
            }



            //if (difpeso != 0 && (difpeso < -50 || difpeso > 50))
            //{
            //    Console.WriteLine(pesoact.ToString() + " Ignorado " + difpeso.ToString());
            //    return;
            //}

            //if (dator.Equals("0") && pesoant == 0) { vzeros += dator; }

            lblZero.Visible = dator.Equals("0");

            //txtPesoT.Text = vzeros;

            //if (dator.Equals("0"))
            //{
            //    dator = pesoant.ToString();
            //}

            //if (!dator.Equals("0"))
            //{
            //    txtServirFin.Text = dator;
            //}

            //if (dator.Equals("0") && vzeros.Length == maxzeros)
            //{
            //    txtServirFin.Text = dator;
            //}

            //if (pesoant != 0 || vzeros.Length > maxzeros)
            //{
            //    vzeros = "";
            //}

            txtServirFin.Text = dator;
            lblRecibePeso.Visible = !lblRecibePeso.Visible;

            //---------------------------------------------------------------------------------
            //estable servirfin

            lv_pos = lv_pos + 1;

            if (lv_pos == 6) //3)
            {
                lv_pos = 0;
            }

            seriePesos[lv_pos] = txtServirFin.Text; //txtServir.Text;
            //estable[lv_pos] = dator; //txtServir.Text;

            if (seriePesos[0] == seriePesos[1] 
                && seriePesos[0] == seriePesos[2]
                && seriePesos[0] == seriePesos[3]
                && seriePesos[0] == seriePesos[4]
                && seriePesos[0] == seriePesos[5])            
            {
                lblEst.ForeColor = Color.Green;
                lblEst.Text = "Estable";
                vestable = true;
                //lblEstable.Visible = true;
                lblEstable.ForeColor = Color.Lime;
            }
            else
            {
                lblEst.ForeColor = Color.Red;
                lblEst.Text = "Variable";
                vestable = false;
                //lblEstable.Visible = false;
                lblEstable.ForeColor = Color.Gray;
            }

            long p1 = long.Parse(Comun.cadenaCero(seriePesos[0])) - long.Parse(Comun.cadenaCero(seriePesos[1]));
            long p2 = long.Parse(Comun.cadenaCero(seriePesos[1])) - long.Parse(Comun.cadenaCero(seriePesos[2]));
            long p3 = long.Parse(Comun.cadenaCero(seriePesos[2])) - long.Parse(Comun.cadenaCero(seriePesos[3]));
            long p4 = long.Parse(Comun.cadenaCero(seriePesos[3])) - long.Parse(Comun.cadenaCero(seriePesos[4]));
            long p5 = long.Parse(Comun.cadenaCero(seriePesos[4])) - long.Parse(Comun.cadenaCero(seriePesos[5]));

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



            if (vestable && txtServir.Text != "0")
            {
                ////btnHecho.BackColor = Color.Green;
                ////btnHecho.ForeColor = Color.White;       
            }
            else
            {

                //btnHecho.BackColor = Color.Red;
                //btnHecho.ForeColor = Color.White;
                //btnHecho.ForeColor = Control.DefaultForeColor;

                btnHecho.BackColor = Control.DefaultBackColor;
                btnHecho.ForeColor = Color.Black;

            }

            //lblEstable.Visible = comm.p_estable;
            //if (vestable) { txtServirFin.Text = dator; }       


            //---------------------------------------------------------------------------------
            
            if (bascula== "0" && cantPesos > 3) 
            {
                bascula = txtServirFin.Text;
            }
            cantPesos++;

            //if (txtServirIni.Text == "" || txtServirIni.Text == "0")
            if (txtBasculaIni.Text == "" || txtBasculaIni.Text == "0")
            {
                //txtServirIni.Text = txtServirFin.Text; //comm.p_dato;
                //-----------------
                //txtBasculaIni.Text = txtServirFin.Text;
                //-----------------
            }

            //---------------------------------------------------------------------------------
            //servir


            //resultado = Convert.ToInt32(txtServirIni.Text) - Convert.ToInt32(txtServirFin.Text);
            resultado = Convert.ToInt32(txtBasculaIni.Text) - Convert.ToInt32(txtServirFin.Text);

            if (txtServirFin.Text == "0")
            {
                //resultado = 0;
            }

            //-----------------------------------------------------
            //if (resultado < 0)
            //{
            //    if (vestable && cantAjustes <= 1) //(resultado > -100)
            //    {

            //        //txtServirIni.Text = txtServirFin.Text; //AJUSTAR
            //        //--------------
            //        txtBasculaIni.Text = txtServirFin.Text; //AJUSTAR
            //        cantAjustes++;
            //        Console.WriteLine("AUTO AJUSTE CARGA INICIO < FINAL" + " " + DateTime.Now.ToString());
            //        //--------------                    
                    
            //    }

            //    //resultado = 0;
            //}
            //-----------------------------------------------------

       

            txtServir.Text = resultado.ToString();

            //---------------------------------------------------------------------------------

            calcularPeso();
            //barra(pbServido, txtPlan, txtServir, txtVariacion);

            //barrar(txtPlan, txtServir);

            //calcularEscala();
            //barra(pbServido2, txtEscalaIni, txtServir);
            //barra2();

            if (txtPlan.Text != "0")
            {
                double porc1 = Convert.ToDouble(txtServir.Text) / Convert.ToDouble(txtPlan.Text) * 100;

                resultado = Convert.ToInt32(Math.Round(porc1));
                barrar(resultado);
                
                //-------------------------------------------------------------------

                tomaCoordenada(resultado, false);

                if (resultado > 90 && Int32.Parse(txtPlan.Text) < 50)
                {
                    tomaCoordenada(0, true);
                }

                //-------------------------------------------------------------------

                //if (porc1 > 100)
                //{
                //    porc1 = 100 - porc1;
                //}

                txtPorc.Text = Math.Round(porc1, 0).ToString(); //+ " %";

           
            }
            else
            {
                txtPorc.Text = "0"; //"0 %";
            }

            fomatoCampos();

            pesoant = pesoact;



        }

        public void tomaCoordenada(int porc, Boolean tome) 
        {

            if ((porc >= 40 && porc < 70) || tome)
            {

                if ((vlat == 0 && vlng == 0) || (vlat != 0 && vlng != 0 && vcontLeerGps < 3))
                {
                    //Principal.instance.limpaGPS();
                    Principal.instance.leerGPS();
                    vlat = Principal.instance.gLatitudgps;
                    vlng = Principal.instance.gLongitudgps;
                    
                    vcontLeerGps++;
                    lblDifGps.Text = Principal.instance.lblDifGps.Text + ", "+lblDifGps.Text;
                    //vlat = 21.920879; vlng = -98.741526;

                }

                else if (vcorralgps.Equals("0000"))
                {
                    //lblGps.ForeColor = Color.Lime;
                    vcorralgps = Servidas.instance.leeCorralUbicacion(vlat, vlng);

                    lblCorralGps.Text = vcorralgps;
                    lblCorralGps.ForeColor = Color.Lime;

                    if (txtCorral.Text.Equals(vcorralgps))
                    {
                        //panel1.BackColor = Color.Green;
                        lblCorralGps.ForeColor = Color.Lime;
                    }
                    else
                    {
                        //panel1.BackColor = Color.Red;
                        lblCorralGps.ForeColor = Color.Orange;
                    }

                }
                
            }

        }

        //private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //private void DataReceivedEventHandler(object sender, SerialDataReceivedEventArgs e)
        //{

        //    SerialPort serialPuerto = (SerialPort)sender;

        //    try
        //    {
        //        txtLeePuerto.Text = serialPuerto.ReadLine();

        //    }
        //    catch (Exception er)
        //    {
        //        String msg = er.ToString();
        //    }

        //}

        //public static bool IsNumbersOnly(string s)
        //{
        //    if (s == null || s == string.Empty)
        //        return false;

        //    foreach (char c in s)
        //    {
        //        if (c < '0' || c > '9') // Avoid using .IsDigit or .IsNumeric as they will return true for other characters
        //            return false;
        //    }

        //    return true;
        //}

        private void calcularPeso() 
        {
            
            long variacion = Convert.ToInt32(txtServir.Text) - Convert.ToInt32(txtPlan.Text);
            txtVariacion.Text = variacion.ToString();

            double varcab = Convert.ToDouble(txtVariacion.Text) / Convert.ToDouble(txtCabezas.Text);
            txtVarCab.Text = Math.Round(varcab, 1).ToString();

            //colorCampo(1);

            if (variacion > 0)
            {
                txtVariacionV.ForeColor = Color.Orange;
            }
            else
            {
                txtVariacionV.ForeColor = Color.Lime;
            }

        }

        private void calcularEscala() 
        {
            long escala = Convert.ToInt32(txtEscalaIni.Text) - ( Convert.ToInt32(txtServidoT.Text) + Convert.ToInt32(txtServir.Text));
            txtEscala.Text = escala.ToString();
        }

        //private void colorCampo(int opc) 
        //{

        //    if (opc == 1)//(Convert.ToInt32(txtVariacion.Text) < 0)
        //    {
        //        txtVariacionV.BackColor = Color.Red;
        //        txtVariacionV.ForeColor = Color.White;
        //        txtVarCab.BackColor = Color.Red;
        //        txtVarCab.ForeColor = Color.White;
        //    }
            
        //    else if (opc == 2)
        //    {
        //        txtVariacionV.BackColor = Color.Yellow;
        //        txtVariacionV.ForeColor = Color.Black;
        //        txtVarCab.BackColor = Color.Yellow;
        //        txtVarCab.ForeColor = Color.Black;
        //    }

        //    else if (opc == 3)
        //    {
        //        txtVariacionV.BackColor = Color.White;
        //        txtVariacionV.ForeColor = Color.Black;
        //        txtVarCab.BackColor = Color.White;
        //        txtVarCab.ForeColor = Color.Black;
        //    }

        //}

        private void fomatoCampos()
        {
            
            txtPlanV.Text = string.Format("{0:#,##0}", long.Parse(txtPlan.Text));
            txtVariacionV.Text = string.Format("{0:#,##0}", long.Parse(txtVariacion.Text));
            txtServirV.Text = string.Format("{0:#,##0}", long.Parse(txtServir.Text));

            txtBasculaIniV.Text = string.Format("{0:#,##0}", long.Parse(txtBasculaIni.Text));
            txtServirIniV.Text = string.Format("{0:#,##0}", long.Parse(txtServirIni.Text));
            btnServirIniV.Text = txtServirIniV.Text;
            txtServirFinV.Text = string.Format("{0:#,##0}", long.Parse(txtServirFin.Text));

            //txtVariacionV.ForeColor = txtVariacion.ForeColor;
            //txtVariacionV.BackColor = txtVariacion.BackColor;




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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnServirIniV_Click(object sender, EventArgs e)
        {

            int resultado = 0;

            //resultado = Convert.ToInt32(txtServirIni.Text) - Convert.ToInt32(txtServirFin.Text);
            resultado = Convert.ToInt32(txtBasculaIni.Text) - Convert.ToInt32(txtServirFin.Text);

            if (resultado < 0)
            {
                if (MessageBox.Show("¿ Desea Actualizar Carga Inicio ?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    //txtServirIni.Text = txtServirFin.Text;
                    //btnServirIniV.Text = txtServirFinV.Text;
                }
            }


        }

        private void timerBotonAlerta_Tick(object sender, EventArgs e)
        {
            int resultado = 0;

            

            //btnHecho.Text = "HECHO";

            //if (txtServirIni.Text != "" && txtServirFin.Text != "")
            if (txtBasculaIni.Text != "" && txtServirFin.Text != "")
            {
                //resultado = Convert.ToInt32(txtServirIni.Text) - Convert.ToInt32(txtServirFin.Text);
                resultado = Convert.ToInt32(txtBasculaIni.Text) - Convert.ToInt32(txtServirFin.Text);

                if (resultado < 0)
                {

                    //btnHecho.Text = "AJUSTAR";

                    //if (btnHecho.BackColor == SystemColors.Control)
                    //if (btnAjustar.BackColor == SystemColors.Control)
                    //if (txtBasculaIniV.BackColor == Color.Black)
                    if (txtServirV.BackColor == Color.Black)
                    {
                        //btnHecho.BackColor = Color.Yellow;
                        //btnHecho.ForeColor = Color.Black;
                        //txtServirIniV.BackColor = Color.Yellow;
                        //txtBasculaIniV.BackColor = Color.Yellow;
                        txtServirV.BackColor = Color.Yellow;
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
                        txtServirV.BackColor = Color.Black;
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
                    txtServirV.BackColor = Color.Black;
                    //btnAjustar.BackColor = SystemColors.Control;
                    //btnAjustar.ForeColor = Color.Black;
                }

            }



        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (Comun.mensajeVer("¿ Desea Cerrar ?", Constants.MSG_BOTON_SINO, Constants.MSG_IMG_WARNING) == Constants.MSG_BOTON_SI)
            {
                Close();
            }

        }

        private void SerialPort_DataReceived2(object sender, SerialDataReceivedEventArgs e)
        {

            try
            {
                
                if (this.Enabled)
                {

                    recibeData += serialPort1.ReadExisting();                    

                    if (recibeData.Contains("\r"))
                    {

                        string stringTemp = recibeData;

                        string[] string_array = stringTemp.Split('\r');

                        for (int i = 0; i < string_array.Length; i++)
                        {

                            stringTemp = string_array[i].Replace(c.ToString(), "").Replace(" ", "").Trim();

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
            catch (Exception merr)
            {
                Console.WriteLine("Error " + merr);
            }


            //try
            //{

            //    //if (serialPort1.IsOpen)
            //    if (this.Enabled)
            //    {

            //        string indata = serialPort1.ReadExisting();
            //        //Thread.Sleep(10);
            //        Thread.Sleep(100);
            //        string stringTemp = indata;
            //        //stringTemp = stringTemp.Replace(c.ToString(), "").Replace(" ", "").Trim();
            //        stringTemp = stringTemp.Replace(r.ToString(), "]\r").Trim();


            //        string[] string_array = stringTemp.Split('\r');

            //        int j = string_array.Length;
            //        //for (int i = 1; i < j-1; i++)
            //        for (int i = 0; i < j; i++)
            //        {

            //            stringTemp = string_array[i].Trim();
            //            //Console.WriteLine(">" + stringTemp);

            //            if (stringTemp.Contains(c.ToString()) && stringTemp.Contains("]"))
            //            {
            //                stringTemp = stringTemp.Replace(c.ToString(), "").Replace(" ", "").Trim();
            //                stringTemp = stringTemp.Replace("]", "").Trim();


            //                //if (stringTemp.Length >= 1 && stringTemp.Length <= 2)
            //                //{
            //                //    string a = "";
            //                //}

            //                long number1 = 0;
            //                if (long.TryParse(stringTemp, out number1))
            //                {
            //                    this.BeginInvoke(new DelegaAcceso(si_DataReceived), new object[] { stringTemp });

            //                }
            //            }

            //        }

            //    }

            //}
            //catch (Exception merr)
            //{
            //    Console.WriteLine("Error " + merr);
            //}


        }
        
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            try
            {

                if (this.Enabled)
                {

                    //lblRecibePeso.Visible = !lblRecibePeso.Visible;

                    //char v = (char)serialPort1.ReadByte();
                    //byte b = (byte)serialPort1.ReadByte();


                    string indata = serialPort1.ReadExisting();
                    Thread.Sleep(10);

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

                    //this.BeginInvoke(new DelegaAcceso(si_DataReceived), new object[] { stringTemp });
                    //Console.WriteLine(stringTemp);


                }

            }
            catch (Exception merr)
            {
                Console.WriteLine("Error "+merr);
            }


            //SerialPort port = (SerialPort)sender;

            ////port.DtrEnable = true;
            ////port.RtsEnable = true;

            //byte[] _buffer = new byte[port.BytesToRead];
            //port.Read(_buffer, 0, _buffer.Length);

            ////Parse buffer
            //string sdata = "";
            //Encoding encoding = ASCIIEncoding.GetEncoding(1252);
            //if (null != _buffer)
            //{
            //    sdata = encoding.GetString(_buffer);
            //}

            //string[] string_array = sdata.Split('\r');
            //string Peso = null;
            //string stringTemp = null;
            //for (int i = 0; i < string_array.Length; i++)
            //{
            //    stringTemp = string_array[i];

            //    char c = (char)02; //\u0002
            //    stringTemp = stringTemp.Replace(c.ToString(), "");

            //    stringTemp = stringTemp.Replace("$", "");
            //    stringTemp = stringTemp.Trim();
            //    //stringTemp += '\r';
            //    //stringTemp += '\n';

            //    //    string[] line_array = stringTemp.Split(',');
            //    //    if (line_array[0] == "GPRMC")
            //    //    {
            //          //stringTemp = string.Join(",",stringTemp);
            //    //    }

            //    if (!stringTemp.Equals(""))
            //    {
            //        Peso = stringTemp;
            //        break;
            //    }

            //    //int peso = int.Parse(Peso);
            //    //if ( Peso.Length>0 ) { break; }

            //}

            //if (Peso != null)
            //{
            //    displayToWindow(Peso.ToString());
            //}

        }

        private void si_DataReceived(string accion)
        {

            if (RecibirPeso) 
            { 
            
                txtBascula.Text = accion;
                txtBascula.Refresh();               
                procesaPeso(txtBascula.Text);

                //Console.WriteLine(accion);
                //Console.WriteLine(txtBascula.Text);
            }

        }

        //private void displayToWindow(string inData)
        //{
        //    BeginInvoke(new EventHandler(delegate
        //    {

        //        //richTextBox1.AppendText(inData);
        //        //richTextBox1.ScrollToCaret();

        //        //txtLectura.AppendText(inData);

        //        txtServirFinV.Text = inData;

        //        //string validos = "A";
        //        //if (chkWarning.Checked) { validos = "V"; }

        //        //gLatitudgps = separaNMEA(inData, validos, 3);
        //        //gLongitudgps = separaNMEA(inData, validos, 5);

        //        //lblLatitud.Text = gLatitudgps.ToString();
        //        //lblLongitud.Text = gLongitudgps.ToString();

        //        //gfechagps = DateTime.Now.ToString("dd-MM-yyyy");
        //        //ghoragps = DateTime.Now.ToString("HH:mm:ss");


        //    }));
        //}


        private double minimaVariacion()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            double minvar = 0;

            datos.p_sql = "SELECT TOP 1 * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    minvar = Convert.ToDouble(row["MinVariacion"]);
                }
            }

            return minvar;

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

        private void manejoTimerDeServidas(Boolean activar)
        {

            if (activar)
            {
                Servidas.instance.timerGPS.Start();
            }
            else
            {
                Servidas.instance.timerGPS.Stop();
            }

        }

        private void timerBeep_Tick(object sender, EventArgs e)
        {


                 

            double porc = barra(pbServido, txtPlan, txtServirV, txtVariacionV);

            //long variacion = long.Parse(txtVariacion.Text);

            //if (variacion > 0)
            //{

            //    txtVariacionV.ForeColor = Color.Orange;

            //    if (beepOn)
            //    {
            //        //InitializeSound();
            //        beepOn = false;
            //    }
            //}
            //else 
            //{
            //    txtVariacionV.ForeColor = Color.Lime;
            //}        

        }

        //private void InitializeSound()
        //{
        //    player.LoadCompleted += delegate (object sender, AsyncCompletedEventArgs e)
        //    {
        //        playSound(false);
        //    };
        //    player.LoadAsync();

        //}

        //private void swithSonido(Boolean valor)
        //{
        //    if (valor)
        //    {
        //        playSound(true);
        //    }
        //    else
        //    {
        //        if (player != null)
        //        {
        //            player.Stop();
        //        }
        //    }
        //}

        //public void playSound(bool looping)
        //{
        //    if (looping)
        //    {
        //        player.PlayLooping();
        //    }
        //    else
        //    {
        //        player.Play();
        //    }
        //}

        private void colorPantalla() 
        {

            Color colorFondo = Color.Black;
            Color colorFondoW = Color.White;
            Color colorFondoP = Color.Blue;
            Color colorLetra = Color.Lime;
            Color colorLetraH = Color.Yellow;
            Color colorLetraT = Color.White;

            this.BackColor = colorFondoW;

            txtCorral.BackColor = colorFondo;
            txtCorral.ForeColor = colorLetraH;

            txtRacion.BackColor = colorFondo;
            txtRacion.ForeColor = colorLetraH;

            txtNumCarga.BackColor = colorFondo;
            txtNumCarga.ForeColor = colorLetraH;


            txtVariacionV.BackColor = colorFondo;
            txtVariacionV.ForeColor = colorLetra;

            txtPlanV.BackColor = colorFondo;
            txtPlanV.ForeColor = colorLetra;

            txtServirV.BackColor = colorFondo;
            txtServirV.ForeColor = colorLetra;

            txtVarCab.BackColor = colorFondo;
            txtVarCab.ForeColor = colorLetra;

            txtServirIniV.BackColor = colorFondo;
            txtServirIniV.ForeColor = colorLetra;

            txtServirFinV.BackColor = colorFondo;
            txtServirFinV.ForeColor = colorLetra;

            txtPorc.BackColor = colorFondo;
            txtPorc.ForeColor = colorLetra;

            txtBasculaIniV.BackColor = colorFondo;
            txtBasculaIniV.ForeColor = colorLetra;

            txtSigCorral.BackColor = colorFondo;
            txtSigCorral.ForeColor = colorLetraH;

            txtFaltaCorral.BackColor = colorFondo;
            txtFaltaCorral.ForeColor = colorLetraH;

            panelBarra.BackColor = colorFondo;

            panelCorral.BackColor = colorFondoP;
            panelRacion.BackColor = colorFondoP;
            panelNumCarga.BackColor = colorFondoP;

            panelPlan.BackColor = colorFondoP;
            panelVarCab.BackColor = colorFondoP;
            panelCargaIni.BackColor = colorFondoP;
            panelServido.BackColor = colorFondoP;
            panelServP.BackColor = colorFondoP;
            panelBascula.BackColor = colorFondoP;
            panelBasculaIni.BackColor = colorFondoP;
            panelSigCorral.BackColor = colorFondoP;
            panelFaltaCorral.BackColor = colorFondoP;

            lblCorral.ForeColor = colorLetraT;
            lblRacion.ForeColor = colorLetraT;
            lblNumCarga.ForeColor = colorLetraT;

            lblPlan.ForeColor = colorLetraT;
            lblVarCab.ForeColor = colorLetraT;
            lblCargaIni.ForeColor = colorLetraT;
            lblServido.ForeColor = colorLetraT;
            lblServP.ForeColor = colorLetraT;
            lblBascula.ForeColor = colorLetraT;
            lblBasculaIni.ForeColor = colorLetraT;
            lblSigCorral.ForeColor = colorLetraT;
            lblFaltaCorral.ForeColor = colorLetraT;



            



        }

        private void btnAjustar_Click(object sender, EventArgs e)
        {
            funcionAjustar(1,false);
            funcionAjustar(2,true);

            Console.Beep(1400, 25);

        }

        void funcionAjustar(int opc, Boolean msg) 
        {

            switch (opc) 
            {
                case 1:
                    if (Convert.ToInt32(txtBasculaIni.Text) < Convert.ToInt32(txtServirFin.Text))
                    {
                        //txtServirIni.Text = txtServirFin.Text;
                        txtBasculaIni.Text = txtServirFin.Text;
                    }                    
                    break;
                case 2:
                    if (Convert.ToInt32(txtBasculaIni.Text) > Convert.ToInt32(txtServirFin.Text))
                    {
                        if (msg)
                        {
                            if (Comun.mensajeVer("¿ Desea Ajustar ?", Constants.MSG_BOTON_SINO, Constants.MSG_IMG_WARNING) == Constants.MSG_BOTON_SI)
                            {
                                //txtServirIni.Text = txtServirFin.Text;
                                txtBasculaIni.Text = txtServirFin.Text;
                            }                            
                        }
                        else
                        {
                            txtBasculaIni.Text = txtServirFin.Text;
                        }
                    }
                    break;
            }

        
        }

 






    }

}