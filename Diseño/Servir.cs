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
        
        private SoundPlayer player = new SoundPlayer(Properties.Resources.beep_censor);

        string vidd;
        string viddr;
        int servido = 0;
        string vjornada;
        string vcorralgps;

        int lv_pos = 0;
        int lv_posr = 0;
        string[] estable = new string[] { "0", "0", "0" };
        string[] establer = new string[] { "0", "0" };
        int estr = 0;
        bool vestable = false;
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

        //private string RxString;

        //public delegate void AddDataDelegate(String myString);
        //public AddDataDelegate myDelegate;

        public Servir(string idd, string iddr, string id, string corralgps, double lat, double lng, string numcarga)
        {
            InitializeComponent();
            vidd = idd;
            viddr = iddr;
            vjornada = id;
            vcorralgps = corralgps;
            vlat = lat;
            vlng = lng;
            vnumcarga = numcarga;
        }

        private void Servir_Load(object sender, EventArgs e)
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();
                     

            manejoTimerDePrincipal(false);
            manejoTimerDeServidas(false);

            //iniciaPesoOpc1();
            iniciaPesoOpc2();
            colorPantalla();


            txtCarga.Text = vnumcarga;
            
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
                    servido = Convert.ToInt32(row["Realidad"].ToString());
                    txtPlanIni.Text = row["Planeado"].ToString();
                    int planeado = Convert.ToInt32(row["Planeado"].ToString()) - servido;
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

                if (txtCorral.Text.Equals(vcorralgps))
                {
                    panel1.BackColor = Color.Green;
                }
                else
                {
                    panel1.BackColor = Color.Red;
                }          

                minvariacion = minimaVariacion();

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

                datos.p_sql = "SELECT cargado as Cargado FROM raciones WHERE idd = " + viddr;

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

            manejoTimerDePrincipal(true);
            manejoTimerDeServidas(true);

            //cerrarPuerto();

            //comm.cerrarPuerto();
            //comm.liberarPuerto();

            serialPort1.Close();
            serialPort1.Dispose();

            swithSonido(false);

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
                serialPort1.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
                serialPort1.ReadTimeout = 500;
                serialPort1.WriteTimeout = 500;
                //serialPort1.NewLine = "\r\n";
                serialPort1.Open();

                RecibirPeso = true;

            }
            catch (Exception err)
            {
                Close();
                MessageBox.Show(err.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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


        private void barrar(TextBox maximo, TextBox valor)
        {
            
            pic1.Visible = false;
            pic2.Visible = false;
            pic3.Visible = false;
            pic4.Visible = false;
            pic5.Visible = false;
            pic6.Visible = false;
            pic7.Visible = false;
            pic8.Visible = false;
            pic9.Visible = false;
            pic10.Visible = false;

            int vmaximo = Convert.ToInt32(maximo.Text);
            int vvalor = Convert.ToInt32(valor.Text);

            double porc = Convert.ToDouble(vvalor) / Convert.ToDouble(vmaximo) * 100;

            if (porc > 10 && porc <= 20)
            {                
                pic1.Visible = true;
            }
            
            if (porc > 20 && porc <= 30) 
            {
                pic1.Visible = true;
                pic2.Visible = true;
            }

            if (porc > 30 && porc <= 40) 
            {
                pic1.Visible = true;
                pic2.Visible = true;
                pic3.Visible = true;
            }

            if (porc > 40 && porc <= 50)
            {
                pic1.Visible = true;
                pic2.Visible = true;
                pic3.Visible = true;
                pic4.Visible = true;
            }

            if (porc > 50 && porc <= 60) 
            {
                pic1.Visible = true;
                pic2.Visible = true;
                pic3.Visible = true;
                pic4.Visible = true;
                pic5.Visible = true;
            }

            if (porc > 60 && porc <= 70) 
            {
                pic1.Visible = true;
                pic2.Visible = true;
                pic3.Visible = true;
                pic4.Visible = true;
                pic5.Visible = true;
                pic6.Visible = true;
            }

            if (porc > 70 && porc <= 80) 
            {
                pic1.Visible = true;
                pic2.Visible = true;
                pic3.Visible = true;
                pic4.Visible = true;
                pic5.Visible = true;
                pic6.Visible = true;
                pic7.Visible = true;
            }

            if (porc > 80 && porc <= 90) 
            {
                pic1.Visible = true;
                pic2.Visible = true;
                pic3.Visible = true;
                pic4.Visible = true;
                pic5.Visible = true;
                pic6.Visible = true;
                pic7.Visible = true;
                pic8.Visible = true;
            }

            if (porc > 90 && porc <= 100) 
            {
                pic1.Visible = true;
                pic2.Visible = true;
                pic3.Visible = true;
                pic4.Visible = true;
                pic5.Visible = true;
                pic6.Visible = true;
                pic7.Visible = true;
                pic8.Visible = true;
                pic9.Visible = true;
            }

            if (porc > 100)
            {
                pic1.Visible = true;
                pic2.Visible = true;
                pic3.Visible = true;
                pic4.Visible = true;
                pic5.Visible = true;
                pic6.Visible = true;
                pic7.Visible = true;
                pic8.Visible = true;
                pic9.Visible = true;
                pic10.Visible = true;
            }

        }
        
        private double barra(ProgressBar barra, TextBox maximo, TextBox valor, TextBox color)
        {

            int vmaximo = Convert.ToInt32(maximo.Text);
            int vvalor = Convert.ToInt32(valor.Text);

            barra.Maximum = vmaximo;

            if (vvalor < 0)
            {
                vvalor = 0;
            }

            if (vmaximo <= vvalor)
            {
                vvalor = vmaximo;
            }

            barra.Value = vvalor;

            double porc = Convert.ToDouble(vvalor) / Convert.ToDouble(vmaximo) * 100;

            Comun.sonidoPorc(color, porc, false);

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

            btnHecho.Text = "HECHO";

            bool guardar = true;

            if (!txtServirIni.Text.Equals("") && !txtServirFin.Text.Equals(""))
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
 
                //ajustar
                if (Convert.ToInt32(txtServirIni.Text) < Convert.ToInt32(txtServirFin.Text))
                {

                    txtServirIni.Text = txtServirFin.Text;
                    guardar = false;

                }

                calcularPeso();

                double porc1 = Convert.ToDouble(txtServir.Text) / Convert.ToDouble(txtPlan.Text) * 100;

                if (porc1 > 100)
                {
                    //timerS.Enabled = false;
                    timerBeep.Enabled = false;
                    RecibirPeso = false;

                    porc1 = 100 - porc1;

                    if (Math.Abs(Math.Round(porc1, 0)) > minvariacion)
                    {
                        //string mensaje = "Peso Servido Excedio al Planeado\r\n¿ Confirma como Hecho ?";
                        string mensaje = "¿ Confirma Hecho ?";

                        if (Comun.mensajeVer(mensaje, Constants.MSG_BOTON_SINO_P, Constants.MSG_IMG_WARNING) == Constants.MSG_BOTON_NO)
                        //if (MessageBox.Show("Peso Servido Excedio " + minvariacion.ToString() + " % del Planeado, ¿ Confirma como Hecho ?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        {
                            guardar = false;
                            //timerS.Enabled = true;
                            timerBeep.Enabled = true;
                            RecibirPeso = true;
                        }

                    }

                }

                if (porc1 > 0 && porc1 <= 100)
                {
                    //timerS.Enabled = false;
                    timerBeep.Enabled = false;
                    RecibirPeso = false;

                    porc1 = 100 - porc1;

                    if (Math.Abs(Math.Round(porc1, 0)) > minvariacion)
                    {
                        //string mensaje = "Peso Servido " + txtServir.Text + " Kg\r\nFaltaron " + txtVariacion.Text + " Kg\r\n es Inferior al Planeado\r\n¿ Confirma como Hecho ?";
                        //string mensaje = "Peso Servido Inferior al Planeado\r\n¿ Confirma como Hecho ?";
                        string mensaje = "¿ Confirma Hecho ?";

                        if (Comun.mensajeVer(mensaje, Constants.MSG_BOTON_SINO_P, Constants.MSG_IMG_WARNING) == Constants.MSG_BOTON_NO)
                        //if (MessageBox.Show("Peso Servido Inferior al Planeado, ¿ Confirma como Hecho ?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        {
                            guardar = false;
                            //timerS.Enabled = true;
                            timerBeep.Enabled = true;
                            RecibirPeso = true;
                        }

                    }

                }

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

                    servido = servido + Convert.ToInt32(txtServir.Text);                    

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

                    if (resultado <= Convert.ToInt32(minvariacion))
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

                    datos.p_sql = "UPDATE servidas SET corralgps = '" + vcorralgps + "', latitudi = "+ vlat + ", longitudi = " + vlng + ", realidad = '" + servido.ToString() + "', hecho = '" + hecho.ToString() + "', fecha = '" + DateTime.Now.ToString("dd-MM-yyyy") + "', hora = '" + DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss") + "', numcarga = " + txtCarga.Text + " WHERE idd = " + vidd;

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


                    int falta = 0;

                    datos.p_sql = "SELECT sum(planeado) as Planeado, sum(realidad) as Realidad FROM servidas WHERE racion = '" + txtRacion.Text + "' and num = " + txtServida.Text + " and id = " + vjornada; //" and  ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ";

                    if (!datos.consultaSQL(ref dt, true))
                    {
                        MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (dt.Rows.Count != 0)
                            {
                                falta = Convert.ToInt32(cadenaCero(row["Planeado"].ToString())) - Convert.ToInt32(cadenaCero(row["Realidad"].ToString()));
                            }
                        }
                    }

                    datos.p_sql = "UPDATE raciones SET falta = " + falta.ToString() + ", servido = '" + servidot.ToString() + "', hechoc = " + cantidadt.ToString() + ", cargado = " + txtServirFin.Text + " WHERE racion = '" + txtRacion.Text + "' and num = " + txtServida.Text + " and id = " + vjornada; //" and  ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ";

                    if (!datos.consultaSQL(ref dt, true))
                    {
                        MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                    Close();

                }

            }

        }

        private string cadenaCero(string valor)
        {

            if (valor.Equals(""))
            {
                valor = "0";
            }

            return valor;

        }

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

            //---------------------------------------------------------------------------------
            //estable servirfin

            lv_pos = lv_pos + 1;

            if (lv_pos == 3)
            {
                lv_pos = 0;
            }

            estable[lv_pos] = txtServirFin.Text; //txtServir.Text;
            //estable[lv_pos] = dator; //txtServir.Text;

            if (estable[0] == estable[1] && estable[1] == estable[2])
            {
                lblEst.ForeColor = Color.Green;
                lblEst.Text = "Estable";
                vestable = true;
                lblEstable.Visible = true;
            }
            else
            {
                lblEst.ForeColor = Color.Red;
                lblEst.Text = "Variable";
                vestable = false;
                lblEstable.Visible = false;
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

            if (txtServirIni.Text == "" || txtServirIni.Text == "0")
            {
                txtServirIni.Text = txtServirFin.Text; //comm.p_dato;                
            }

            //---------------------------------------------------------------------------------
            //servir

            resultado = Convert.ToInt32(txtServirIni.Text) - Convert.ToInt32(txtServirFin.Text);

            if (txtServirFin.Text == "0")
            {
                //resultado = 0;
            }

            if (resultado < 0)
            {
                if (vestable) //(resultado > -100)
                {
                    if (cantAjustes <= 2)
                    {
                        txtServirIni.Text = txtServirFin.Text; //AJUSTAR
                        cantAjustes++;
                        Console.WriteLine("AUTO AJUSTE CARGA INICIO < FINAL" + " " + DateTime.Now.ToString());
                    }
                }
                resultado = 0;
            }

            txtServir.Text = resultado.ToString();

            //---------------------------------------------------------------------------------

            calcularPeso();
            //barra(pbServido, txtPlan, txtServir, txtVariacion);

            barrar(txtPlan, txtServir);

            //calcularEscala();
            //barra(pbServido2, txtEscalaIni, txtServir);
            //barra2();

            if (txtPlan.Text != "0")
            {
                double porc1 = Convert.ToDouble(txtServir.Text) / Convert.ToDouble(txtPlan.Text) * 100;

                if (porc1 > 100)
                {
                    porc1 = 100 - porc1;
                }

                txtPorc.Text = Math.Round(porc1, 0).ToString(); //+ " %";
            }
            else
            {
                txtPorc.Text = "0"; //"0 %";
            }

            fomatoCampos();

            pesoant = pesoact;


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

            resultado = Convert.ToInt32(txtServirIni.Text) - Convert.ToInt32(txtServirFin.Text);

            if (resultado < 0)
            {
                if (MessageBox.Show("¿ Desea Actualizar Carga Inicio ?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    txtServirIni.Text = txtServirFin.Text;
                    //btnServirIniV.Text = txtServirFinV.Text;
                }
            }


        }

        private void timerBotonAlerta_Tick(object sender, EventArgs e)
        {
            int resultado = 0;

            btnHecho.Text = "HECHO";

            if (txtServirIni.Text != "" && txtServirFin.Text != "")
            {
                resultado = Convert.ToInt32(txtServirIni.Text) - Convert.ToInt32(txtServirFin.Text);

                if (resultado < 0)
                {

                    //btnHecho.Text = "AJUSTAR";

                    if (btnHecho.BackColor == SystemColors.Control)
                    {
                        //btnHecho.BackColor = Color.Yellow;
                        //btnHecho.ForeColor = Color.Black;
                        txtServirIniV.BackColor = Color.Yellow;
                    }
                    else
                    {
                        //btnServirIniV.BackColor = SystemColors.Control;

                        //btnHecho.BackColor = SystemColors.Control;
                        //btnHecho.ForeColor = Color.Black;
                        txtServirIniV.BackColor = Color.Black;
                    }

                    //Console.Beep(900, 100);
                    Console.WriteLine("CARGA INICIO < FINAL" + " " + DateTime.Now.ToString());

                }
                else
                {
                    //btnHecho.BackColor = SystemColors.Control;
                    //btnHecho.ForeColor = Color.Black;
                    txtServirIniV.BackColor = Color.Black;
                }

            }



        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿ Desea Cerrar, No Se Guardaran Los Datos ?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Close();
            }

        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            try
            {

                if (this.Enabled)
                {


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
                Principal.instance.timerRecorrido.Start();
            }
            else
            {
                Principal.instance.timerWs.Stop();
                Principal.instance.timerRecorrido.Stop();
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

            double porc = barra(pbServido, txtPlan, txtServir, txtVariacion);

            if (porc >= 100)
            {

                txtVariacionV.ForeColor = Color.Orange;

                if (beepOn)
                {
                    //InitializeSound();
                    beepOn = false;
                }
            }
            else 
            {
                txtVariacionV.ForeColor = Color.Lime;
            }        

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
            Color colorFondoP = Color.Blue;
            Color colorLetra = Color.Lime;
            Color colorLetraH = Color.Yellow;
            Color colorLetraT = Color.White;

            this.BackColor = colorFondoW;

            txtCorral.BackColor = colorFondo;
            txtCorral.ForeColor = colorLetraH;

            txtRacion.BackColor = colorFondo;
            txtRacion.ForeColor = colorLetraH;

            txtCarga.BackColor = colorFondo;
            txtCarga.ForeColor = colorLetraH;


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

            lblCorral.ForeColor = colorLetraT;
            lblRacion.ForeColor = colorLetraT;
            lblNumCarga.ForeColor = colorLetraT;

            lblPlan.ForeColor = colorLetraT;
            lblVarCab.ForeColor = colorLetraT;
            lblCargaIni.ForeColor = colorLetraT;
            lblServido.ForeColor = colorLetraT;
            lblServP.ForeColor = colorLetraT;
            lblBascula.ForeColor = colorLetraT;


        }


    }

}