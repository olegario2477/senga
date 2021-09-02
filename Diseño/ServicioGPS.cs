using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace Senga
{
    partial class ServicioGPS : ServiceBase
    {

        Timer tmrServicio = null;

        public ServicioGPS()
        {
            InitializeComponent();

            tmrServicio = new Timer(1000);
            tmrServicio.Elapsed += new ElapsedEventHandler(tmrServicio_Elapsed);

        }

        void tmrServicio_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
            VerificarAppIniciar();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: agregar código aquí para iniciar el servicio.
            tmrServicio.Start();
        }

        protected override void OnStop()
        {
            // TODO: agregar código aquí para realizar cualquier anulación necesaria para detener el servicio.
            tmrServicio.Stop();
        }

        void VerificarAppIniciar()
        {
            //Process[] apps = Process.GetProcessesByName("sistemaventas");
            //if (apps.Length == 0)
            //{
            //    try
            //    {
            //        Process.Start(@"C:Archivos de programa\MiApp\sistemaventas.exe");
            //    }
            //    catch
            //    {
            //    }
            //}
        }

    }
}
