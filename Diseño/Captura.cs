using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Senga
{

    public partial class Captura : Form
    {

        int vventana;
        int vjornada;

        public int result { get; set; }

        public Captura(int jornada, int ventana)
        {
            InitializeComponent();
            vventana = ventana;
            vjornada = jornada;
        }

        private void Captura_Load(object sender, EventArgs e)
        {
            lblTexto.Text = "";
            limpiaEtiquetaBoton();

            tableLayoutPanel2.Height = 0;
            tableLayoutPanel1.Height = 400;

            switch (vventana)
            {
                case 0:

                    break;
                case 1:
                    this.Text = "NUMERO SERVIDA";
                    etiquetaBotonServidas();
                    tableLayoutPanel2.Height = 180;
                    tableLayoutPanel1.Height = 220;
                    break;
                case 2:
                    this.Text = "BUSCAR CORRAL";
                    break;
                case 3:
                    this.Text = "NUMERO RACION";
                    break;
                case 4:
                    this.Text = "NUMERO PLANTA";
                    break;
                case 5:
                    this.Text = "NUMERO TOLVA";
                    break;
                case 6:
                    this.Text = "CONTRASEÑA";
                    //txtCaptura.PasswordChar = '*';
                    txtCaptura.ForeColor = Color.FromArgb(28,28,28);
                    lblContraseña.Visible = true;

                    //for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
                    //    tableLayoutPanel1.RowStyles[i] = new RowStyle(SizeType.Percent, 33.33F);

                    //for (int i = 0; i < tableLayoutPanel1.ColumnCount; i++)
                    //    tableLayoutPanel1.ColumnStyles[i] = new ColumnStyle(SizeType.Percent, 25F);

                    //tableLayoutPanel1.PerformLayout();

                    break;
                default:

                    break;
            }


        }
        
        private void etiquetaBotonServidas()
        {

            visibleEtiquetaBoton(true);

            lbl1.Text = "AM";
            lbl2.Text = "PM";

            lbl3.Text = "AM2";
            lbl4.Text = "PM2";
            
            lbl5.Text = "AM0";
            lbl6.Text = "PM0";
            
            lbl7.Text = "AM3";
            lbl8.Text = "PM3";
            
            lbl9.Text = "";
            lbl0.Text = "";

        }

        private void limpiaEtiquetaBoton()
        {
            visibleEtiquetaBoton(false);

            lbl1.Text = "";
            lbl2.Text = "";

            lbl3.Text = "";
            lbl4.Text = "";

            lbl5.Text = "";
            lbl6.Text = "";

            lbl7.Text = "";
            lbl8.Text = "";

            lbl9.Text = "";
            lbl0.Text = "";

        }

        private void visibleEtiquetaBoton(Boolean valor)
        {

            lbl1.Visible = valor;
            lbl2.Visible = valor;

            lbl3.Visible = valor;
            lbl4.Visible = valor;

            lbl5.Visible = valor;
            lbl6.Visible = valor;

            lbl7.Visible = valor;
            lbl8.Visible = valor;

            lbl9.Visible = valor;
            lbl0.Visible = valor;

        }

        private void agregaDigito(string digito) 
        {
            string valor = txtCaptura.Text;

            if (valor.Length < 10)//txtCaptura.MaxLength)
            {
                txtCaptura.Text = valor + digito;


                if (vventana == 6)
                {
					lblContraseña.Text = lblContraseña.Text + "*";
				}

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            lblTexto.Text = "";
            agregaDigito("1");
            if (txtCaptura.Text.Length == 1)
            {
                lblTexto.Text = lbl1.Text;
            }
        }

        private void btnDos_Click(object sender, EventArgs e)
        {
            lblTexto.Text = "";
            agregaDigito("2");
            if (txtCaptura.Text.Length == 1)
            {
                lblTexto.Text = lbl2.Text;
            }
        }

        private void btnTres_Click(object sender, EventArgs e)
        {
            lblTexto.Text = "";
            agregaDigito("3");
            if (txtCaptura.Text.Length == 1)
            {
                lblTexto.Text = lbl3.Text;
            }
        }

        private void btnCuatro_Click(object sender, EventArgs e)
        {
            lblTexto.Text = "";
            agregaDigito("4");
            if (txtCaptura.Text.Length == 1)
            {
                lblTexto.Text = lbl4.Text;
            }
        }

        private void btnCinco_Click(object sender, EventArgs e)
        {
            lblTexto.Text = "";
            agregaDigito("5");
            if (txtCaptura.Text.Length == 1)
            {
                lblTexto.Text = lbl5.Text;
            }
        }

        private void btnSeis_Click(object sender, EventArgs e)
        {
            lblTexto.Text = "";
            agregaDigito("6");
            if (txtCaptura.Text.Length == 1)
            {
                lblTexto.Text = lbl6.Text;
            }
        }

        private void btnSiete_Click(object sender, EventArgs e)
        {
            lblTexto.Text = "";
            agregaDigito("7");
            if (txtCaptura.Text.Length==1) 
            { 
                lblTexto.Text = lbl7.Text; 
            }

        }

        private void btnOcho_Click(object sender, EventArgs e)
        {
            lblTexto.Text = "";
            agregaDigito("8");
            if (txtCaptura.Text.Length == 1)
            {
                lblTexto.Text = lbl8.Text;
            }
        }

        private void btnNueve_Click(object sender, EventArgs e)
        {
            lblTexto.Text = "";
            agregaDigito("9");
            if (txtCaptura.Text.Length == 1)
            {
                lblTexto.Text = lbl9.Text;
            }
        }

        private void btnCero_Click(object sender, EventArgs e)
        {
            lblTexto.Text = "";
            if (txtCaptura.Text.Length > 0)
            {
                agregaDigito("0");
                if (txtCaptura.Text.Length == 1)
                {
                    lblTexto.Text = lbl0.Text;
                }
            }
        }

        private void btn3Ceros_Click(object sender, EventArgs e)
        {

            if (txtCaptura.Text.Length > 0)
            {
                agregaDigito("000");
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            switch (vventana)
            {
                case 0:
                    if (txtCaptura.Text != "")
                    {
                        result = Int32.Parse(txtCaptura.Text);
                    }
                    break;
                case 1:
                    guardarServida();
                    break;
                case 2:
                    buscarCorral(); 
                    break;
                case 3:
                    elejirRacion();
                    break;
                case 4:
                    guardarPlanta();
                    break;
                case 5:
                    guardarTolva();
                    break;
                case 6:
                    if (txtCaptura.Text != "")
                    {
                        result = Int32.Parse(txtCaptura.Text);
                    }
                    break;
                default:
                    
                    break;
            }

            Close();

        }

        private void elejirRacion() 
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            string valor = "";

            valor = txtCaptura.Text;

            if (valor.Length > 0)
            {
                datos.p_sql = "UPDATE operacion SET racion = 'R" + txtCaptura.Text + "'";

                if (!datos.consultaSQL(ref dt, false))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        
        }

        private void guardarServida() 
        { 
            
            Datos datos = new Datos();
            DataTable dt = new DataTable();

            string valor = "";

            valor = txtCaptura.Text;

            if (valor.Length > 0)
            {
                datos.p_sql = "UPDATE operacion SET servida = '" + txtCaptura.Text + "'";

                if (!datos.consultaSQL(ref dt, false))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void guardarPlanta()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            string valor = "";

            valor = txtCaptura.Text;

            if (valor.Length > 0)
            {
                datos.p_sql = "UPDATE operacion SET planta = '" + txtCaptura.Text + "'";

                if (!datos.consultaSQL(ref dt, false))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void guardarTolva()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            string valor = "";

            valor = txtCaptura.Text;

            if (valor.Length > 0)
            {
                datos.p_sql = "UPDATE operacion SET tolva = '" + txtCaptura.Text + "'";

                if (!datos.consultaSQL(ref dt, false))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void buscarCorral() 
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            int servida = 0;
            int idd = 0;
            //int iddr = 0;

            datos.p_sql = "SELECT TOP 1 servida FROM operacion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    servida = Convert.ToInt32(row["Servida"]);
                }
            }
            
            datos.p_sql = "SELECT idd FROM servidas WHERE corral = '" + txtCaptura.Text + "' and hecho = 'N' AND num = " + servida + " and ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " )";
            
            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    idd = Convert.ToInt32(row["IdD"]);


                }

                if (dt.Rows.Count != 0)
                {
                    
                    datos.p_sql = "UPDATE operacion SET idd = " + idd.ToString() + ", accion = 'B'";

                    if (!datos.consultaSQL(ref dt, true))
                    {
                        MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                    //Senga.Servir frm = new Senga.Servir(idd.ToString(),iddr.ToString());
                    //frm.Show();
                }
                else 
                {
                    //MessageBox.Show("No se Encontro Corral", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                //datos.p_sql = "UPDATE operacion SET idd = " + idd;

                //if (!datos.consultaSQL(ref dt, false))
                //{
                //    MessageBox.Show(datos.p_error);
                //}
            }

        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            lblTexto.Text = "";
            txtCaptura.Text = cortarCadena(1,txtCaptura.Text);
        }


        public string cortarCadena(int cantQuitar, String cadena)
        {

            string resultado = "";

            if (cadena.Length > 0) 
            {
                resultado = cadena.Substring(0, cadena.Length - cantQuitar);
            }

            return resultado; 

        }

        private void lbl1_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void lbl2_Click(object sender, EventArgs e)
        {
            btnDos_Click(sender, e);
        }

        private void lbl3_Click(object sender, EventArgs e)
        {
            btnTres_Click(sender, e);
        }

        private void lbl4_Click(object sender, EventArgs e)
        {
            btnCuatro_Click(sender, e);
        }

        private void lbl5_Click(object sender, EventArgs e)
        {
            btnCinco_Click(sender, e);
        }

        private void lbl6_Click(object sender, EventArgs e)
        {
            btnSeis_Click(sender, e);
        }

        private void lbl7_Click(object sender, EventArgs e)
        {
            btnSiete_Click(sender, e);
        }

        private void lbl8_Click(object sender, EventArgs e)
        {
            btnOcho_Click(sender, e);
        }

        private void lbl9_Click(object sender, EventArgs e)
        {
            btnNueve_Click(sender, e);
        }

        private void lbl0_Click(object sender, EventArgs e)
        {
            btnCero_Click(sender, e);
        }

        private void lblBorrar_Click(object sender, EventArgs e)
        {
            btnBorrar_Click(sender, e);
        }

        private void lblOK_Click(object sender, EventArgs e)
        {
            btnOK_Click(sender, e);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lblCerrar_Click(object sender, EventArgs e)
        {
            btnCerrar_Click(sender, e);
        }

		private void picCerrar_Click(object sender, EventArgs e)
		{
            btnCerrar_Click(sender, e);
		}

		private void picCero_Click(object sender, EventArgs e)
		{
			btnCero_Click(sender, e);
		}

		private void picOK_Click(object sender, EventArgs e)
		{
			btnOK_Click(sender, e);
		}

		private void picUno_Click(object sender, EventArgs e)
		{
			button1_Click(sender, e);
		}

		private void picDos_Click(object sender, EventArgs e)
		{
			btnDos_Click(sender, e);
		}

		private void picTres_Click(object sender, EventArgs e)
		{
			btnTres_Click(sender, e);
		}

		private void picCuatro_Click(object sender, EventArgs e)
		{
			btnCuatro_Click(sender, e);
		}

		private void picCinco_Click(object sender, EventArgs e)
		{
			btnCinco_Click(sender, e);
		}

		private void picSeis_Click(object sender, EventArgs e)
		{
			btnSeis_Click(sender, e);
		}

		private void picSiete_Click(object sender, EventArgs e)
		{
			btnSiete_Click(sender, e);
		}

		private void picOcho_Click(object sender, EventArgs e)
		{
			btnOcho_Click(sender, e);
		}

		private void picNueve_Click(object sender, EventArgs e)
		{
			btnNueve_Click(sender, e);
		}

        private void picAm_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
            btnOK_Click(sender, e);
        }

        private void picPm_Click(object sender, EventArgs e)
        {
            btnDos_Click(sender, e);
            btnOK_Click(sender, e);
        }
    }
}