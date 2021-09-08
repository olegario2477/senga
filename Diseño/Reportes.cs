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
            comboBoxOpciones.Items.Add("Servidas Detalle");
            comboBoxOpciones.Items.Add("Servidas Resumen Por Cargas");
            comboBoxOpciones.Items.Add("Servidas Resumen Por Racion");
            comboBoxOpciones.Items.Add("Cargas y Servidas Detalle");
            comboBoxOpciones.Items.Add("Cargas Detalle");
            comboBoxOpciones.SelectedIndex = 0;

        }

        private void posicionbotones()
        {
            btnCerrar.Top = 0;
            btnCalculadora.Top = 0;
            btnCerrar.Left = this.Width - btnCerrar.Width;

            btnCalculadora.Left = btnCerrar.Left - btnCalculadora.Width;

            panel1.Height = this.Height - btnCerrar.Height - 20;

        }

        private void consulta1() 
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            txtReporte.Text = "Jornada " + vjornada + '\r' + '\n';

            datos.p_sql = "SELECT Servidas.Num, Servidas.Racion, Servidas.Corral, Servidas.NumCarga, Servidas.Planeado, Servidas.Realidad, Servidas.BasculaIni, Servidas.BasculaFin, Servidas.CargaIni, Servidas.Hora, Cargas.BasculaFin AS BasculaFinCarga FROM Servidas LEFT Join Cargas ON Servidas.NumCarga = Cargas.IdD WHERE (Servidas.Hecho = 'S' OR Servidas.Hecho = 'X') AND Servidas.Id = " + vjornada + " ORDER BY Servidas.NumCarga, Servidas.Hora";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            { 

                int basfinAnt = 0;
                string antCarga = "";
                DateTime horaAnt = new DateTime(1998, 04, 30, 0, 0, 0);

                TimeSpan ts;

                txtReporte.Text += "Hora" + '\t' + "Tiempo" + '\t' + "Servida" + '\t' + "#Carga" + '\t' + "Corral" + '\t' + "Racion" + '\t' + "Planeado" + '\t' + "Realidad" + '\t' + "Dif" + '\t' + "BasIni" + '\t' + "BasFin" /*+ '\t' + "BasDif" *//*+ '\t' + "CargaIni"*/ + '\t' + "Carga" + '\t' + "BasFCarg" + '\t' + "Ajuste" + '\r' + '\n'; ;
                
                int i = 0;

                foreach (DataRow row in dt.Rows)
                {

                    int difvalor5 = 0;
                    ts = DateTime.Parse(row["Hora"].ToString()) - horaAnt;

                    string duracion = ts.ToString();

                    if (i == 0) { duracion = "00:00:00"; }



                    int difvalor = Int32.Parse(row["Realidad"].ToString()) - Int32.Parse(row["Planeado"].ToString());
                    int difvalor2 = Int32.Parse(cadenaCero(row["BasculaIni"].ToString())) - Int32.Parse(cadenaCero(row["BasculaFin"].ToString()));
                    int difvalor3 = Int32.Parse(cadenaCero(row["CargaIni"].ToString())) - difvalor2;
                    int difvalor4 =  Int32.Parse(cadenaCero(row["BasculaFin"].ToString())) - difvalor3;

                    if (!antCarga.Equals(row["NumCarga"].ToString()))
                    {
                        difvalor5 = Int32.Parse(cadenaCero(row["BasculaIni"].ToString())) - Int32.Parse(cadenaCero(row["BasculaFinCarga"].ToString()));
                        linea();
                    }
                    else
                    {
                        difvalor5 = Int32.Parse(cadenaCero(row["BasculaIni"].ToString())) - basfinAnt;
                    }             



                    txtReporte.Text += row["Hora"].ToString() + '\t' + duracion.ToString() + '\t' + row["Num"].ToString() + '\t' + row["NumCarga"].ToString() + '\t' + row["Corral"].ToString() + '\t' + row["Racion"].ToString() + '\t' + row["Planeado"].ToString() + '\t' + row["Realidad"].ToString() + '\t' + difvalor.ToString() + '\t' + row["BasculaIni"].ToString() + '\t' + row["BasculaFin"].ToString() /*+ '\t' + difvalor2.ToString()*//* + '\t' + row["CargaIni"].ToString()*/ + '\t' + difvalor3.ToString() + '\t' + difvalor4.ToString() + '\t' + difvalor5.ToString() + '\r' + '\n';

                    basfinAnt = Int32.Parse(cadenaCero(row["BasculaFin"].ToString()));
                    horaAnt = DateTime.Parse(row["Hora"].ToString());
                    antCarga = row["NumCarga"].ToString();
                    i++;
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
                txtReporte.Text += "Servida" + '\t' + "#Carga" + '\t' + "Corral" + '\t' + "Racion" + '\t' + "Planeado" + '\t' + "Realidad" + '\t' + "Dif" + '\r' + '\n'; ;

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


                txtReporte.Text += "Accion" + '\t' + "Hora" + '\t' + "Servida" + '\t' + "Racion" + '\t' + "#Carga" + '\t' + "Corral" + '\t' + "Cantidad" + /*'\t' + "SumCarga" + '\t' + "SumRac" +*/ '\r' + '\n';
                
                foreach (DataRow row in dt.Rows)
                {

                    //if (!antServ.Equals(row["Num"].ToString())) { acumcarga = 0; acumrac = 0; }

                    if (!antRac.Equals(row["Racion"].ToString())) { acumrac = 0; }
                    acumrac = acumrac + long.Parse(row["Realidad"].ToString());


                    if (!antCarga.Equals(row["NumCarga"].ToString()) || !antRac.Equals(row["Racion"].ToString())) 
                    {

                        if (!antCarga.Equals("")) 
                        {
                            linea();
                            txtReporte.Text += "Total" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + acumcarga.ToString() + /*'\t' + acumrac.ToString() +*/ '\r' + '\n';
                        }

                        linea(); 
                    }

                    if (!antCarga.Equals(row["NumCarga"].ToString())) 
                    { 

                        acumcarga = 0;

                        sqlConsultaTxt(txtReporte,"Carga","SELECT Hora,Num,Racion,IdD,'' as Vacio,Carga FROM Cargas WHERE Id = " + vjornada + " AND IdD = " + row["NumCarga"].ToString());

                        linea();
                       
                    }
                  
                    acumcarga = acumcarga + long.Parse(row["Realidad"].ToString());
                  
                    txtReporte.Text += "Serv" + '\t' + row["Hora"].ToString() + '\t' + row["Num"].ToString() + '\t' + row["Racion"].ToString() + '\t' + row["NumCarga"].ToString() + '\t' + row["Corral"].ToString()  + '\t' + row["Realidad"].ToString() + /*'\t' + acumcarga.ToString() + '\t' + acumrac.ToString() +*/ '\r' + '\n';




                    antServ = row["Num"].ToString();
                    antRac = row["Racion"].ToString();
                    antCarga = row["NumCarga"].ToString();
                
                }

                linea();
                txtReporte.Text += "Total" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + acumcarga.ToString() + /*'\t' + acumrac.ToString() +*/ '\r' + '\n';
                linea();



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
                txtReporte.Text += "Hora" + '\t' + "Servida" + '\t' + "#Carga" + '\t' + "Racion" + '\t' + "Solicito" + '\t' + "Anterior" + '\t' + "Recibio"  + '\t' + "Carga"+ '\t' + "SolCarg" + '\t' + "BasIni" + '\t' + "BasFin" + '\t' + "BasDif" + '\t' + "CargaIni" + '\t' + "BasFCarg" + '\r' + '\n'; ;

                foreach (DataRow row in dt.Rows)
                {
                    int difvalor = Int32.Parse(row["Solicito"].ToString()) - Int32.Parse(row["Carga"].ToString());
                    int difvalor2 = Int32.Parse(cadenaCero(row["BasculaFin"].ToString())) - Int32.Parse(cadenaCero(row["BasculaIni"].ToString()));
                    //int difvalor3 = difvalor2 + Int32.Parse(cadenaCero(row["CargaIni"].ToString())) ;
                    int carga = Int32.Parse(row["CargaIni"].ToString()) + Int32.Parse(row["Carga"].ToString());

                    int difvalor4 = Int32.Parse(cadenaCero(row["BasculaFin"].ToString())) - carga;//difvalor2;
                    txtReporte.Text += row["Hora"].ToString() + '\t' + row["Num"].ToString() + '\t' + row["IdD"].ToString() + '\t' + row["Racion"].ToString() + '\t' + row["Solicito"].ToString() + '\t' + row["CargaIni"].ToString() + '\t' + row["Carga"].ToString() + '\t' + carga.ToString()  + '\t' + difvalor.ToString() + '\t' + row["BasculaIni"].ToString() + '\t' + row["BasculaFin"].ToString() + '\t' + difvalor2.ToString() + '\t' + row["CargaIni"].ToString() + '\t' + difvalor4.ToString() + '\r' + '\n';
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
