using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Media;
using System.Drawing;

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
                    using (Mensaje02 msg2 = new Mensaje02(mensaje,img))
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

        public static void sonidoPorc(TextBox color, double porc) 
        {

            if (porc > 0 && porc < 50)
            {
                colorCampo(color, Color.White, Color.Black);
            }
            else if (porc >= 50 && porc < 100)
            {
                Console.Beep(2000, 100);
                colorCampo(color, Color.Orange, Color.White);
            }
            else if (porc >= 100)
            {
                colorCampo(color, Color.Red, Color.White);
            }

        }

        private static void colorCampo(TextBox campo, Color fondo, Color letra)
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



    }
}
