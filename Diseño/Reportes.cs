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
            llenaCombo();

        



        }

        void llenaCombo()
        {
            comboBoxOpciones.Items.Clear();
            comboBoxOpciones.Items.Add("Servidas Detalle ");
            comboBoxOpciones.Items.Add("Servidas Resumen Cargas");
            comboBoxOpciones.Items.Add("Servidas Resumen Racion");
            comboBoxOpciones.Items.Add("Servidas Detalle Cargas");
            comboBoxOpciones.Items.Add("Cargas Detalle");
            comboBoxOpciones.SelectedIndex = 0;

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

            txtReporte.Text = "Jornada " + vjornada + '\r' + '\n';

            datos.p_sql = "SELECT * FROM Servidas WHERE (Hecho = 'S' OR Hecho = 'X') AND Id = " + vjornada + " ORDER BY NumCarga, Hora";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                txtReporte.Text += "Hora" + '\t' + "Servida" + '\t' + "Carga" + '\t' + "Corral" + '\t' + "Racion" + '\t' + "Planeado" + '\t' + "Realidad" + '\t' + "Dif" + '\t' + "BasIni" + '\t' + "BasFin" + '\t' + "BasDif" + '\t' + "CargaIni" + '\t' + "Carga" + '\t' + "BasFCarg" + '\r' + '\n'; ;

                foreach (DataRow row in dt.Rows)
                {
                    int difvalor = Int32.Parse(row["Realidad"].ToString()) - Int32.Parse(row["Planeado"].ToString());
                    int difvalor2 = Int32.Parse(cadenaCero(row["BasculaIni"].ToString())) - Int32.Parse(cadenaCero(row["BasculaFin"].ToString()));
                    int difvalor3 = Int32.Parse(cadenaCero(row["CargaIni"].ToString())) - difvalor2;
                    int difvalor4 =  Int32.Parse(cadenaCero(row["BasculaFin"].ToString())) - difvalor3;
                    txtReporte.Text += row["Hora"].ToString() + '\t' + row["Num"].ToString() + '\t' + row["NumCarga"].ToString() + '\t' + row["Corral"].ToString() + '\t' + row["Racion"].ToString() + '\t' + row["Planeado"].ToString() + '\t' + row["Realidad"].ToString() + '\t' + difvalor.ToString() + '\t' + row["BasculaIni"].ToString() + '\t' + row["BasculaFin"].ToString() + '\t' + difvalor2.ToString() + '\t' + row["CargaIni"].ToString() + '\t' + difvalor3.ToString() + '\t' + difvalor4.ToString() + '\r' + '\n';
                }
            }

        }

        private void consulta2()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            txtReporte.Text = "Jornada " + vjornada + '\r' + '\n';

            datos.p_sql = "SELECT Num, NumCarga,COUNT(*) as Corral,Racion,SUM(Planeado) as Planeado,SUM(Realidad) as Realidad FROM Servidas WHERE (Hecho = 'S' OR Hecho = 'X') AND Id = " + vjornada + " GROUP BY Num,NumCarga,Racion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                txtReporte.Text += "Servida" + '\t' + "Carga" + '\t' + "Corral" + '\t' + "Racion" + '\t' + "Planeado" + '\t' + "Realidad" + '\t' + "Dif" + '\r' + '\n'; ;

                foreach (DataRow row in dt.Rows)
                {
                    int difvalor = Int32.Parse(row["Realidad"].ToString()) - Int32.Parse(row["Planeado"].ToString());
                    txtReporte.Text += row["Num"].ToString() + '\t' + row["NumCarga"].ToString() + '\t' + row["Corral"].ToString() + '\t' + row["Racion"].ToString() + '\t' + row["Planeado"].ToString() + '\t' + row["Realidad"].ToString() + '\t' + difvalor.ToString() + '\r' + '\n';
                }
            }

        }

        private void consulta3()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            txtReporte.Text = "Jornada " + vjornada + '\r' + '\n';

            datos.p_sql = "SELECT Num, Racion,COUNT(*) as Corral,SUM(Planeado) as Planeado,SUM(Realidad) as Realidad FROM Servidas WHERE (Hecho = 'S' OR Hecho = 'X') AND Id = " + vjornada + " GROUP BY Num,Racion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                txtReporte.Text += "Servida" + '\t' + "Racion" + '\t' + "Corral" + '\t' + "Planeado" + '\t' + "Realidad" + '\t' + "Dif" + '\r' + '\n'; ;

                foreach (DataRow row in dt.Rows)
                {
                    int difvalor = Int32.Parse(row["Realidad"].ToString()) - Int32.Parse(row["Planeado"].ToString());
                    txtReporte.Text += row["Num"].ToString() + '\t' + row["Racion"].ToString() + '\t' + row["Corral"].ToString() + '\t' + row["Planeado"].ToString() + '\t' + row["Realidad"].ToString() + '\t' + difvalor.ToString() + '\r' + '\n';
                }
            }

        }

        private void consulta4()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            txtReporte.Text = "Jornada " + vjornada + '\r' + '\n';

            datos.p_sql = "SELECT * FROM Servidas WHERE (Hecho = 'S' OR Hecho = 'X') AND Id = " + vjornada + " ORDER BY NumCarga, Hora";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {

                long acumcarga = 0;
                long acumrac = 0;
                string antServ = "";
                string antRac = "";
                string antCarga = "";


                txtReporte.Text += "Accion" + '\t' + "Hora" + '\t' + "Servida" + '\t' + "Racion" + '\t' + "Carga" + '\t' + "Corral" + '\t' + "Servido" + '\t' + "SumCarga" + '\t' + "SumRac" + '\r' + '\n';
                
                foreach (DataRow row in dt.Rows)
                {

                    //if (!antServ.Equals(row["Num"].ToString())) { acumcarga = 0; acumrac = 0; }

                    if (!antRac.Equals(row["Racion"].ToString())) { acumrac = 0; }
                    acumrac = acumrac + long.Parse(row["Realidad"].ToString());


                    if (!antCarga.Equals(row["NumCarga"].ToString()) || !antRac.Equals(row["Racion"].ToString())) { linea(); }

                    if (!antCarga.Equals(row["NumCarga"].ToString())) 
                    { 
                        acumcarga = 0;

                        sqlConsultaTxt(txtReporte,"Carga","SELECT Hora,Num,Racion,IdD,Carga FROM Cargas WHERE Id = " + vjornada + " AND IdD = " + row["NumCarga"].ToString());

                        linea();
                       
                    }
                  
                    acumcarga = acumcarga + long.Parse(row["Realidad"].ToString());
                  
                    txtReporte.Text += "Serv" + '\t' + row["Hora"].ToString() + '\t' + row["Num"].ToString() + '\t' + row["Racion"].ToString() + '\t' + row["NumCarga"].ToString() + '\t' + row["Corral"].ToString()  + '\t' + row["Realidad"].ToString() + '\t' + acumcarga.ToString() + '\t' + acumrac.ToString() + '\r' + '\n';




                    antServ = row["Num"].ToString();
                    antRac = row["Racion"].ToString();
                    antCarga = row["NumCarga"].ToString();
                
                }
            }

        }

        private void consulta5()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            txtReporte.Text = "Jornada " + vjornada + '\r' + '\n';

            datos.p_sql = "SELECT * FROM Cargas WHERE Id = " + vjornada;

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                txtReporte.Text += "Hora" + '\t' + "Servida" + '\t' + "Carga" + '\t' + "Racion" + '\t' + "Solicito" + '\t' + "Carga" + '\t' + "Dif" + '\t' + "BasIni" + '\t' + "BasFin" + '\t' + "BasDif" + '\t' + "CargaIni" + '\t' + "Carga" + '\t' + "BasFCarg" + '\r' + '\n'; ;

                foreach (DataRow row in dt.Rows)
                {
                    int difvalor = Int32.Parse(row["Carga"].ToString()) - Int32.Parse(row["Solicito"].ToString());
                    int difvalor2 = Int32.Parse(cadenaCero(row["BasculaIni"].ToString())) - Int32.Parse(cadenaCero(row["BasculaFin"].ToString()));
                    int difvalor3 = Int32.Parse(cadenaCero(row["CargaIni"].ToString())) - difvalor2;
                    int difvalor4 = Int32.Parse(cadenaCero(row["BasculaFin"].ToString())) - difvalor3;
                    txtReporte.Text += row["Hora"].ToString() + '\t' + row["Num"].ToString() + '\t' + row["IdD"].ToString() + '\t' + row["Racion"].ToString() + '\t' + row["Solicito"].ToString() + '\t' + row["Carga"].ToString() + '\t' + difvalor.ToString() + '\t' + row["BasculaIni"].ToString() + '\t' + row["BasculaFin"].ToString() + '\t' + difvalor2.ToString() + '\t' + row["CargaIni"].ToString() + '\t' + difvalor3.ToString() + '\t' + difvalor4.ToString() + '\r' + '\n';
                }
            }

        }

        void linea() 
        { 
            txtReporte.Text += "------------------------------------------------------------------------------------------------------------" + '\r' + '\n';
        }

        private void sqlConsultaTxt(TextBox txt, string accion, string sql)
        {
            var valores = new List<string>();
            valores.Clear();

            valores = sqlConsulta(sql);
            valores.ForEach(delegate (string valor) { txt.Text += accion + '\t' + valor + '\r' + '\n'; });
        }
        
        private List<string> sqlConsulta(string sql) 
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            var valores = new List<string>();
            valores.Clear();

            string cadena = "";
            
            datos.p_sql = sql;

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    cadena = "";

                    for (int i=0;i<row.ItemArray.Length ;i++) 
                    { 
                        cadena = cadena + row[i].ToString() + '\t';
                    }

                    valores.Add(cadena);
                }            
            }

            return valores;

        }


        private void comboBoxOpciones_SelectedIndexChanged(object sender, EventArgs e)
        {


            switch (comboBoxOpciones.SelectedIndex) 
            {
                case 0:
                    consulta1();
                    break;
                case 1:
                    consulta2();
                    break;
                case 2:
                    consulta3();
                    break;
                case 3:
                    consulta4();
                    break;
                case 4:
                    consulta5();
                    break;

            }


        }

        private void btnCalculadora_Click(object sender, EventArgs e)
        {
            Process calc = new Process { StartInfo = { FileName = @"calc.exe" } };
            calc.Start();

            //calc.WaitForExit();
        }

        private string cadenaCero(string valor)
        {

            if (valor.Equals(""))
            {
                valor = "0";
            }

            return valor;

        }

    }
}
