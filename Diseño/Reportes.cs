using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Senga
{
    public partial class Reportes : Form
    {

        string vjornada;

        public Reportes(string id)
        {
            InitializeComponent();
            vjornada = id;

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Reportes_Load(object sender, EventArgs e)
        {
            posicionbotones();


            txtReporte.Text += "Jornada "+ vjornada + '\r'+'\n';
            consulta1();


        }

        private void posicionbotones()
        {
            btnCerrar.Top = 0;
            btnCerrar.Left = this.Width - btnCerrar.Width;
        }

        private void consulta1() 
        {
            Datos datos = new Datos();
            DataTable dt = new DataTable();

            string valor = "";

            datos.p_sql = "SELECT * FROM Servidas WHERE (Hecho = 'S' OR Hecho = 'X') AND Id = " + vjornada + " ORDER BY Hora";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    valor = row["Corral"].ToString();
                    txtReporte.Text += row["Hora"].ToString() + '\t' + row["NumCarga"].ToString() + '\t' + row["Corral"].ToString() + '\t' + row["Racion"].ToString() + '\t' + row["Realidad"].ToString() + '\r' + '\n';
                }
            }
        }



    }
}
