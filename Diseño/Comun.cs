using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Management;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Services.Description;
using System.Windows.Forms;

namespace Senga
{
    class Comun
    {


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


        public static int mensajeVer(string mensaje, int opc, int img)
        {
            int msgResult = 0;

            switch (opc)
            {
                case Constants.MSG_BOTON_OK:
                    using (Mensaje01 msg1 = new Mensaje01(mensaje, img))
                    {
                        msg1.ShowDialog();
                    }
                    break;

                case Constants.MSG_BOTON_SINO:
                    using (Mensaje02 msg2 = new Mensaje02(mensaje, img))
                    {
                        msg2.ShowDialog();
                        msgResult = msg2.result;
                    }
                    break;

                case Constants.MSG_BOTON_SINO_P:
                    using (Mensaje03 msg3 = new Mensaje03(mensaje, img))
                    {
                        msg3.ShowDialog();
                        msgResult = msg3.result;
                    }
                    break;

            }

            return msgResult;

        }

        public static void sonidoPorc(Label color, double porc, Boolean opcColor)
        {

            int pesoAlarma = 80;
            int pesoLleno = 100;

            if (porc < pesoAlarma)
            {
                if (opcColor)
                {
                    colorCampo(color, Color.White, Color.Black);
                }
            }
            else if (porc >= pesoAlarma && porc < pesoLleno)
            {
                Console.Beep(2000, 100);
                Console.WriteLine("<<BEEP>> % " + Convert.ToInt32(porc).ToString());
                if (opcColor)
                {
                    colorCampo(color, Color.Orange, Color.White);
                }
            }
            else if (porc >= pesoLleno)
            {

                //Console.Beep(5000, 100);

                Console.Beep(1420, 25);
                Console.Beep(1400, 25);

                //Console.Beep(1000, 25);
                //Console.Beep(1300, 25);
                //Console.Beep(1600, 25);
                //Console.Beep(1900, 25);

                Console.WriteLine("<<BEEP-BEEP>> % " + Convert.ToInt32(porc).ToString());
                if (opcColor)
                {
                    colorCampo(color, Color.Red, Color.White);
                }
            }

        }

        public static void sonidoPeso(Label color, long peso, Boolean opcColor)
        {

            int pesoAlarma = -100;
            int pesoLleno = 0;

            if (peso < pesoAlarma)
            {
                if (opcColor)
                {
                    colorCampo(color, Color.White, Color.Black);
                }
            }
            else if (peso >= pesoAlarma && peso < pesoLleno)
            {
                Console.Beep(2000, 100);
                Console.WriteLine("<<BEEP>> % " + Convert.ToInt32(peso).ToString());
                if (opcColor)
                {
                    colorCampo(color, Color.Orange, Color.White);
                }
            }
            else if (peso >= pesoLleno)
            {

                //Console.Beep(5000, 100);

                Console.Beep(1420, 25);
                Console.Beep(1400, 25);

                //Console.Beep(1000, 25);
                //Console.Beep(1300, 25);
                //Console.Beep(1600, 25);
                //Console.Beep(1900, 25);

                Console.WriteLine("<<BEEP-BEEP>> % " + Convert.ToInt32(peso).ToString());
                if (opcColor)
                {
                    colorCampo(color, Color.Red, Color.White);
                }
            }

        }

        private static void colorCampo(Label campo, Color fondo, Color letra)
        {

            campo.BackColor = fondo;
            campo.ForeColor = letra;

        }

        public static void reproducirRecursoWav(int opc)
        {
            //switch (opc)
            //{

            //    case 0:
            //        break;
            //    case 10:
            //        new SoundPlayer(Properties.Resources.diez).Play();
            //        break;
            //    case 20:
            //        new SoundPlayer(Properties.Resources.veinte).Play();
            //        break;
            //    case 30:
            //        new SoundPlayer(Properties.Resources.treinta).Play();
            //        break;
            //    case 40:
            //        new SoundPlayer(Properties.Resources.cuarenta).Play();
            //        break;
            //    case 50:
            //        new SoundPlayer(Properties.Resources.cincuenta).Play();
            //        break;
            //    case 60:
            //        new SoundPlayer(Properties.Resources.sesenta).Play();
            //        break;
            //    case 70:
            //        new SoundPlayer(Properties.Resources.setenta).Play();
            //        break;
            //    case 80:
            //        new SoundPlayer(Properties.Resources.ochenta).Play();
            //        break;
            //    case 90:
            //        new SoundPlayer(Properties.Resources.noventa).Play();
            //        break;
            //    case 100:
            //        new SoundPlayer(Properties.Resources.cien).Play();
            //        break;
            //}

        }

        public static long valorFalta(string racion, string servida, string jornada)
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            long falta = 0;

            datos.p_sql = "SELECT sum(planeado) as Planeado, sum(realidad) as Realidad FROM servidas WHERE hecho = 'N' and racion = '" + racion + "' and num = " + servida + " and id = " + jornada;

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
                        falta = long.Parse(cadenaCero(row["Planeado"].ToString())) - long.Parse(cadenaCero(row["Realidad"].ToString()));
                    }
                }
            }

            if (falta < 0)
            {
                falta = 0;
            }

            return falta;

        }

        public static string cadenaCero(string valor)
        {

            if (valor.Equals(""))
            {
                valor = "0";
            }

            return valor;

        }

        public static string valorBoton(PictureBox control, PaintEventArgs e, string valor)
        {

            //using (Font fuente = new Font("Arial", 27, FontStyle.Bold))
            using (Brush pincel = new SolidBrush(Color.White))
            {
                float emSize = 27;
                Point posicion = new Point(8, 12);

                switch (valor.Length)
                {
                    case 1:
                        posicion = new Point(18, 12);
                        break;
                    case 2:
                        posicion = new Point(8, 12);
                        break;
                    case 3:
                        posicion = new Point(0, 14);
                        emSize = 24;
                        break;
                    case 4:
                        posicion = new Point(0, 18);
                        emSize = 19;
                        break;
                    case 5:
                        posicion = new Point(0, 22);
                        emSize = 15;
                        break;
                    case 6:
                        posicion = new Point(0, 23);
                        emSize = 13;
                        break;

                }

                Font fuente = new Font("Courier", emSize, FontStyle.Bold);
                e.Graphics.DrawString(valor, fuente, pincel, posicion);
            }

            return valor;

        }


        public static void mostrarReloj2(PictureBox pictureBox, object sender, PaintEventArgs e)
        {

            double anguloH;
            int magnitudH;
            int hora, minuto, segundo, XH, YH;
            double xH, yH;

            var g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);//FromArgb(28, 28, 28));
            var r1 = pictureBox.ClientRectangle;
            r1.Inflate(-3, -3);
            g.DrawEllipse(Pens.Black, r1);
            var r2 = r1;
            r2.Inflate(-5, -5);


            Font font = new Font("Tahoma", 10.00F, FontStyle.Bold);
            TextRenderer.DrawText(g, DateTime.Now.ToString("HH:mm:ss"), font,//this.Font,
                new Rectangle(r1.Left, r1.Top + 2 * r1.Height / 3 - 20, r1.Width, r1.Height / 3),
                Color.Black);

            int espacio = 10; //numeros
            int espacio2 = 5; //puntos

            for (int i = 1; i <= 12; i++)
            {

                anguloH = i * 30;
                anguloH = (anguloH - 90);
                anguloH = (anguloH * Math.PI) / 180;

                magnitudH = r2.Height / 2 - espacio;

                xH = magnitudH * Math.Cos(anguloH);
                yH = magnitudH * Math.Sin(anguloH);

                XH = Convert.ToInt32(xH);
                YH = Convert.ToInt32(yH);

                g.DrawString(i.ToString(), new Font("Ariel", 13, FontStyle.Bold), Brushes.Black, new PointF(XH + magnitudH + espacio, YH + magnitudH + espacio));

            }

            espacio = 2;
            espacio2 = 6;

            for (int i = 1; i <= 60; i++)
            {

                //if (i % 10 == 0 && i % 5 == 0)
                //{
                //    continue;
                //}

                anguloH = i * 6;
                anguloH = (anguloH - 90);
                anguloH = (anguloH * Math.PI) / 180;

                magnitudH = r2.Height / 2 - espacio;

                xH = magnitudH * Math.Cos(anguloH);
                yH = magnitudH * Math.Sin(anguloH);

                XH = Convert.ToInt32(xH);
                YH = Convert.ToInt32(yH);

                g.DrawString("°", new Font("Ariel", 5, FontStyle.Bold), Brushes.Black, new PointF(XH + magnitudH + espacio + espacio2, YH + magnitudH + espacio + espacio2));

            }


            e.Graphics.TranslateTransform(r2.Left + r2.Width / 2, r2.Top + r2.Height / 2);

            var c = g.BeginContainer();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.RotateTransform(DateTime.Now.Hour * 30f + DateTime.Now.Minute / 2f);
            g.FillEllipse(Brushes.Black, -5, -5, 10, 10);
            using (var p = new Pen(Color.Black, 4))
                g.DrawLine(p, 0, 0, 0, -r2.Height / 2 + 30);
            g.EndContainer(c);

            c = g.BeginContainer();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.RotateTransform(DateTime.Now.Minute * 6);
            using (var p = new Pen(Color.Black, 2))
                g.DrawLine(p, 0, 0, 0, -r2.Height / 2 + 10);
            g.EndContainer(c);

            c = g.BeginContainer();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.RotateTransform(DateTime.Now.Second * 6);
            using (var p = new Pen(Color.Red, 2))
                g.DrawLine(p, 0, 10, 0, -r2.Height / 2 + 15);
            g.EndContainer(c);

        }

        public static void actualizaHoraPlan(DataTable dt, string columna)
        {

            DateTime date1 = DateTime.Now;

            foreach (DataRow row in dt.Rows)
            {

                DateTime date2 = date1.AddSeconds(30);//.AddMinutes(1);
                string currentTime = date2.ToString("HH:mm");

                row[columna] = currentTime;

                date1 = date2;

            }

        }

        public static string fportDescripcion(string nombre)
        {

            string valor = "";

            try
            {
                ManagementObjectCollection ManObjReturn;
                ManagementObjectSearcher ManObjSearch;
                ManObjSearch = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'");
                //ManObjSearch = new ManagementObjectSearcher("Select * from Win32_SerialPort");
                ManObjReturn = ManObjSearch.Get();

                foreach (ManagementObject ManObj in ManObjReturn)
                {
                    string port = ManObj["Caption"].ToString();

                    if (port.ToLower().Contains(nombre))
                    {
                        valor = port;
                        break;
                    }
                }
            }
            catch { }

            return valor;
            
        }

        public static string ExtraerEntreParentesis(string input)
        {
            // Usamos una expresión regular para encontrar el texto entre paréntesis
            Match match = Regex.Match(input, @"\(([^)]*)\)");

            if (match.Success)
            {
                // Retornamos el contenido entre los paréntesis
                return match.Groups[1].Value;
            }
            else
            {
                return string.Empty; // Si no se encuentra nada, retornamos una cadena vacía
            }
        }

        public static double archivoTamanoDB()
        {

            string rutaArchivo = AppDomain.CurrentDomain.BaseDirectory + "datos.accdb";

            FileInfo fileInfo = new FileInfo(rutaArchivo);

            long fileSizeInBytes = fileInfo.Length;

            // Convertir el tamaño a megabytes (1 MB = 1,048,576 bytes)
            return Math.Round(fileSizeInBytes / (1024.0 * 1024.0), 1);

        }

        public static Boolean redConectado()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }

        public static void Rename(string filePath, string newFileName)
        {

            var newFilePath = Path.Combine(Path.GetDirectoryName(filePath), newFileName + Path.GetExtension(filePath));

            if (!File.Exists(newFilePath))
            {
                File.Move(filePath, newFilePath);
            }

        }

        public static string rutared()
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

        public static void graficoRosca(PictureBox pic, Boolean increment, Color color, Color fondo, float grosor, double valor, double maximo, string unidad, string textodescrip)
        {

            try
            {

                pic.Image = new Bitmap(pic.Width, pic.Height);
                pic.BackColor = fondo;

                Bitmap bmpmarker = new Bitmap(pic.Image); 

                Graphics graphicImage = Graphics.FromImage(bmpmarker);
                graphicImage.SmoothingMode = SmoothingMode.AntiAlias;

                float anguloIni = 270;

                graphicImage.FillPie(
                    new SolidBrush(Color.FromArgb(100, Color.White)), // Pen to draw the pie
                    new Rectangle(0, 0, pic.Width, pic.Height), // Rectangle defining the bounds of the pie
                    anguloIni, // Start angle in degrees
                    360 // Sweep angle in degrees
                );

                double percentComplete = 0;

                if (increment)
                {
                    double percentCompleteA = maximo > 0 ? (double)(maximo - valor) / (maximo) : 0;
                    double percentCompleteB = 1 - percentCompleteA;
                    percentComplete = percentCompleteB;
                }
                else
                {
                    double percentCompleteA = maximo > 0 ? (double)(maximo - valor) / (maximo) : 0;
                    percentComplete = percentCompleteA;
                }

                double degreesComplete = percentComplete * 360 - 360;

                if (increment) 
                { 
                    degreesComplete = degreesComplete + 360; 
                    percentComplete = 1 - percentComplete;
                }

                //valores
                graphicImage.DrawPie(
                    new Pen(color, grosor), // Pen to draw the pie
                    new Rectangle((int)(grosor/2) , (int)(grosor / 2) , pic.Width - (int)grosor, pic.Height - (int)grosor), // Rectangle defining the bounds of the pie
                    anguloIni, // Start angle in degrees
                    (float)degreesComplete  // Sweep angle in degrees
                );

                var colorCentro = fondo; //Color.FromArgb(28, 28, 28);

                graphicImage.FillPie(
                    new SolidBrush(colorCentro), // Pen to draw the pie
                    new Rectangle((int)grosor, (int)grosor, pic.Width - (int)grosor * 2 , pic.Height - (int)grosor * 2), // Rectangle defining the bounds of the pie
                    anguloIni, // Start angle in degrees
                    360 // Sweep angle in degrees
                );


                //////porcentaje texto
                String textoc = Math.Round(100 - (percentComplete * 100),0).ToString() + "";
             
                Font font = new Font("Arial", pic.Width / 4, FontStyle.Regular);
                SizeF textSize = graphicImage.MeasureString(textoc, font);

                float x = (pic.Width - textSize.Width) / 2;
                float y = (pic.Height - textSize.Height) / 2;

                graphicImage.DrawString(textoc,
                    font,
                    new SolidBrush(Color.White),
                    new PointF(x, y));



                //////descripcion
                Font font2 = new Font("Arial", pic.Width / 8 / 2, FontStyle.Bold);
                textodescrip += "";
                SizeF textSize2 = graphicImage.MeasureString(textodescrip, font2);
                float x2 = (pic.Width - textSize2.Width) / 2;
                float y2 = (pic.Height - textSize2.Height) / 2;

                graphicImage.DrawString(textodescrip,
                    font2,
                    new SolidBrush(Color.White),
                    new PointF(x2, y2 + textSize.Height/2));


                //////descripcion unidad
                Font font3 = new Font("Arial", pic.Width / 10 , FontStyle.Bold);
                SizeF textSize3 = graphicImage.MeasureString(unidad, font2);
                float x3 = (pic.Width - textSize3.Width) / 2;
                float y3 = (pic.Height - textSize3.Height) / 2;

                graphicImage.DrawString(unidad,
                    font3,
                    new SolidBrush(Color.White),
                    new PointF(x + textSize.Width-15, y3));



                pic.Image = bmpmarker;
                graphicImage.Dispose();

            }
            catch { }

        }

        public static void actualizarApp(Boolean msg, int opc)
        {

            string nameruta = "\\senga_archivos";
            string nameapp = "\\app";
            string nameappnom = "\\senga";
            string vrutared;

            if (!redConectado())
            {
                //MessageBox.Show("Sin red disponible.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (msg)
                {
                    Comun.mensajeVer("Sin red disponible.", Constants.MSG_BOTON_OK, Constants.MSG_IMG_WARNING);
                }
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
                    string targetPath = Directory.GetCurrentDirectory() + "\\Senga_nva_" + DateTime.Now.ToString("ddMMyyHHmmss") + ".exe";

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
                        if (msg)
                        {
                            Comun.mensajeVer("No hay actualizaciones a una nueva version de la App", Constants.MSG_BOTON_OK, Constants.MSG_IMG_WARNING);
                        }
                    }
                    else
                    {
                        if (

                            (msg) ?

                            Comun.mensajeVer(
                            "¿ Desea actualizar nueva version " + vsourcePath + " ?",
                            Constants.MSG_BOTON_SINO,
                            Constants.MSG_IMG_QUESTION
                            ) == Constants.MSG_BOTON_SI

                            : true

                            )
                        {

                            Principal.instance.Greloj.Visible = false;
                            Principal.instance.pictureBox1.Visible = false;
                            Principal.instance.lblIniciandoApp.Visible = true;
                            Principal.instance.panelHoral.Visible = false;
                            Principal.instance.panelNombre.Visible = false;
                            Principal.instance.picSalir.Visible = false;
                            Principal.instance.picPlanes.Visible = false;

                            Principal.instance.picConfigurar.Visible = false;
                            Principal.instance.picOpciones.Visible = false;

                            Principal.instance.picPuertos.Visible = false;
                            Principal.instance.picNumero.Visible = false;
                            Principal.instance.picPlanta.Visible = false;
                            Principal.instance.picConfigurarT.Visible = false;
                            Principal.instance.picRecorrido.Visible = false;
                            Principal.instance.lblNumCamion.Visible = false;

                            Principal.instance.picConectGps.Visible = false;
                 

                            Principal.instance.lblIniciandoApp.Text = "Actualizando...";

                            Principal.instance.Refresh();
                            //this.Visible = false;



                            File.Copy(sourcePath, targetPath, true);

                            Rename(actualPath, "Senga_" + vactualPath + "_" + DateTime.Now.ToString("ddMMyyHHmmss"));

                            Rename(targetPath, "Senga");

                            File.Copy(sourcePath, targetPath, true);


                            switch (opc)
                            {
                                case Constants.OPC_NADA:

                                    break;
                                case Constants.OPC_REINICIO:
                                    Application.Restart();
                                    break;
                                case Constants.OPC_SALIR:
                                    Application.Exit();
                                    break;

                            }

                            //if (msg)
                            //{
                            //    Application.Restart();
                            //}
                            //else
                            //{
                            //    //Application.Exit();
                            //}

                            //Comun.mensajeVer("Listo reinciar aplicacion", Constants.MSG_BOTON_OK, Constants.MSG_IMG_OK);

                        }
                    }


                }
                catch (Exception ec)
                {
                    if (msg)
                    {
                        Comun.mensajeVer(ec.Message, Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
                    }
                }

                //}

            }


        }

    }
}
