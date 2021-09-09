using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Senga
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Principal());

            //try
            //{
            //    Application.EnableVisualStyles();
            //    Application.SetCompatibleTextRenderingDefault(false);
            //    Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //    Application.ThreadException +=
            //        new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //    Application.Run(new Principal());
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("A fatal error has occurred.  Please contact your admin\n" +
            //                    "Exception type: " + ex.GetType() + "\n" +
            //                    "Exception Message: " + ex.Message + "\n" +
            //                    ex.StackTrace, "Fatal Exception");
            //    if (ex.InnerException != null)
            //    {
            //        MessageBox.Show("Inner Exception:\n" +
            //                        "Exception type: " + ex.InnerException.GetType() + "\n" +
            //                        "Exception Message: " + ex.InnerException.Message + "\n" +
            //                        ex.StackTrace, "Fatal Exception (inner)");
            //    }
            //}

        }

        //static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        //{
        //    throw new Exception(e.Exception.Message, e.Exception);
        //}




    }
}