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

            switch (vventana)
            {
                case 1:
                    this.Text = "NUMERO SERVIDA";
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
                    txtCaptura.PasswordChar = '*';
                    break;
                default:

                    break;
            }


        }
        
        private void agregaDigito(string digito) 
        {
            string valor = txtCaptura.Text;

            if (valor.Length < txtCaptura.MaxLength)
            {
                txtCaptura.Text = valor + digito;
            }
                        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            agregaDigito("1");
        }

        private void btnDos_Click(object sender, EventArgs e)
        {
            agregaDigito("2");
        }

        private void btnTres_Click(object sender, EventArgs e)
        {
            agregaDigito("3");
        }

        private void btnCuatro_Click(object sender, EventArgs e)
        {
            agregaDigito("4");
        }

        private void btnCinco_Click(object sender, EventArgs e)
        {
            agregaDigito("5");
        }

        private void btnSeis_Click(object sender, EventArgs e)
        {
            agregaDigito("6");
        }

        private void btnSiete_Click(object sender, EventArgs e)
        {
            agregaDigito("7");
        }

        private void btnOcho_Click(object sender, EventArgs e)
        {
            agregaDigito("8");
        }

        private void btnNueve_Click(object sender, EventArgs e)
        {
            agregaDigito("9");
        }

        private void btnCero_Click(object sender, EventArgs e)
        {
            agregaDigito("0");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            switch (vventana)
            {
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
                    if (txtCaptura.Text!="") 
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



    }
}