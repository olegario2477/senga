using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Senga
{
    public partial class Timer : Form
    {
        Stopwatch oSW = new Stopwatch();

        int min;
        int seg;

        public Timer()
        {
            InitializeComponent();
        }

        private void Timer_Load(object sender, EventArgs e)
        {

            min = 8;//9:59   -> 10min
            seg = 59;

            lblMinutos.Text = min.ToString().Length < 2 ? "0" + min.ToString() : min.ToString();
            lblSegundos.Text = seg.ToString().Length < 2 ? "0" + seg.ToString() : seg.ToString();

            //oSW.Start();
            timer1.Enabled = true;
            timer1.Interval = 1000;

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;  
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            seg -= 1;

            lblMinutos.Text = min.ToString().Length < 2 ? "0" + min.ToString() : min.ToString();
            lblSegundos.Text = seg.ToString().Length < 2 ? "0" + seg.ToString() : seg.ToString();
            lblPuntos01.Visible = !lblPuntos01.Visible;

            if (seg == 0 && min > 0)
            {
                min -= 1;
                seg = 60;
            }

            if(min == 0 && seg == 0)
            {
                timer1.Enabled = false;
                btnCerrar.Visible = true;
                timer2.Enabled = true;
            }


            //TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)oSW.ElapsedMilliseconds);

            //lblMinutos.Text = ts.Minutes.ToString().Length < 2 ? "0" + ts.Minutes.ToString() : ts.Minutes.ToString();
            //lblSegundos.Text = ts.Seconds.ToString().Length < 2 ? "0" + ts.Seconds.ToString() : ts.Seconds.ToString(); 
            //lblMilisegundos.Text = ts.Milliseconds.ToString();

            //if (lblSegundos.Text == "04")
            //{
            //    timer1.Enabled = false;
            //    btnCerrar.Visible = true;
            //    timer2.Enabled = true;
            //}

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Console.Beep(2000, 100);

        }
    }
}
