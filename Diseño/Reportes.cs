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
            propiedadesGrid();





        }

        void llenaCombo()
        {
            comboBoxOpciones.Items.Clear();
            comboBoxOpciones.Items.Add("Servidas Detalle");
            comboBoxOpciones.Items.Add("Servidas Resumen Por Cargas");
            comboBoxOpciones.Items.Add("Servidas Resumen Por Racion");
            comboBoxOpciones.Items.Add("Cargas y Servidas Detalle");
            comboBoxOpciones.Items.Add("Cargas Detalle");
            comboBoxOpciones.Items.Add("Saldos");
            comboBoxOpciones.SelectedIndex = 0;

        }

        private void posicionbotones()
        {
            btnCerrar.Top = 0;
            btnCalculadora.Top = 0;
            btnCerrar.Left = this.Width - btnCerrar.Width;

            btnCalculadora.Left = btnCerrar.Left - btnCalculadora.Width;

            btnInfo.Top = 0;
            btnInfo.Left = btnCalculadora.Left - btnInfo.Width;




            panel1.Height = this.Height - btnCerrar.Height - 20;

        }

        private void consulta1() 
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            //txtReporte.Text = "Jornada " + vjornada + '\r' + '\n';
            txtReporte.Text = "";

            datos.p_sql = "SELECT Servidas.Num, Servidas.Racion, Servidas.Corral, Servidas.NumCarga, Servidas.Planeado, Servidas.Realidad, Servidas.BasculaIni, Servidas.BasculaFin, Servidas.CargaIni, Servidas.Hora, Cargas.BasculaFin AS BasculaFinCarga, Servidas.Bascula FROM Servidas LEFT Join Cargas ON Servidas.NumCarga = Cargas.IdD WHERE (Servidas.Hecho = 'S' OR Servidas.Hecho = 'X') AND Servidas.Id = " + vjornada + " ORDER BY Servidas.NumCarga, Servidas.Hora";

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

                txtReporte.Text += "Corral" + '\t' + "Racion" + '\t' + "#Srv" + '\t' + "#Carga" + '\t' + "HoraServ" + '\t' + "TiempoS" + '\t' + "Planeado" + '\t' + "Servido" + '\t' + "Realidad" + '\t' + "Dif" + '\t' + "Porcen%" + '\t' + "BasApe" + '\t' + "BasIni" + '\t' + "BasFin" /*+ '\t' + "BasDif" *//*+ '\t' + "CargaIni"*/ + '\t' + "Carga" + '\t' + "BasFCarg" + '\t' + "Ajuste"  + '\t' + "BApeBIni" + '\r' + '\n'; ;
                
                int i = 0;
                long total1 = 0;
                long total2 = 0;
                long total3 = 0;
                long total4 = 0;
                long total1a = 0;
                long total2a = 0;
                long total3a = 0;
                long total4a = 0;

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

                    int difvalor6 = Int32.Parse(cadenaCero(row["Bascula"].ToString())) - Int32.Parse(cadenaCero(row["BasculaIni"].ToString()));
                    int difvalor7 = Int32.Parse(cadenaCero(row["Bascula"].ToString())) - Int32.Parse(cadenaCero(row["BasculaFin"].ToString()));


                    if (!antCarga.Equals(row["NumCarga"].ToString()))
                    {
                        txtReporte.Text += "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + formatoDecimal(total4a.ToString()) + '\t' + formatoDecimal(total3a.ToString()) + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + formatoDecimal(total2a.ToString()) + '\t' + formatoDecimal(total1a.ToString()) + '\r' + '\n';
                        total1a = 0;
                        total2a = 0;
                        total3a = 0;
                        total4a = 0;
                    }


                    double porc = ( Double.Parse(row["Realidad"].ToString()) / Double.Parse(row["Planeado"].ToString()) * 100 ) - 100;
                    porc = Math.Round(porc,1);

                    txtReporte.Text += row["Corral"].ToString().Trim() + '\t' + row["Racion"].ToString() + '\t' + row["Num"].ToString() + '\t' + row["NumCarga"].ToString() + '\t' + row["Hora"].ToString() + '\t' + duracion.ToString() + '\t' + formatoDecimal(row["Planeado"].ToString()) + '\t' + formatoDecimal(row["Realidad"].ToString()) + '\t' + formatoDecimal(difvalor7.ToString()) + '\t' + difvalor.ToString() + '\t' + porc.ToString() + '\t' + formatoDecimal(row["Bascula"].ToString()) + '\t' + formatoDecimal(row["BasculaIni"].ToString()) + '\t' + formatoDecimal(row["BasculaFin"].ToString()) /*+ '\t' + difvalor2.ToString()*//* + '\t' + row["CargaIni"].ToString()*/ + '\t' + formatoDecimal(difvalor3.ToString()) + '\t' + formatoDecimal(difvalor4.ToString()) + '\t' + formatoDecimal(difvalor5.ToString()) + '\t' + formatoDecimal(difvalor6.ToString()) + '\r' + '\n';


                    total1a = total1a + difvalor6;
                    total2a = total2a + difvalor5;
                    total3a = total3a + difvalor7;
                    total4a = total4a + long.Parse(row["Realidad"].ToString());



                    total1 = total1 + difvalor6;
                    total2 = total2 + difvalor5;
                    total3 = total3 + difvalor7;
                    total4 = total4 + long.Parse(row["Realidad"].ToString());


                    basfinAnt = Int32.Parse(cadenaCero(row["BasculaFin"].ToString()));
                    horaAnt = DateTime.Parse(row["Hora"].ToString());
                    antCarga = row["NumCarga"].ToString();
                    i++;

                }

                txtReporte.Text += "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + formatoDecimal(total4a.ToString()) + '\t' + formatoDecimal(total3a.ToString()) + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + formatoDecimal(total2a.ToString()) + '\t' + formatoDecimal(total1a.ToString()) + '\r' + '\n';

                txtReporte.Text += "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + formatoDecimal(total4.ToString()) + '\t' + formatoDecimal(total3.ToString()) + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + formatoDecimal(total2.ToString()) + '\t' + formatoDecimal(total1.ToString()) + '\r' + '\n';

            }

            ////--------------------------------------------------------------
            //DataTable dt2 = new DataTable();
            //txtDt(txtReporte, ref dt2);
            //dataGridSQLResultados.DataSource = dt2;
            ////--------------------------------------------------------------

        }


        private void txtDt(TextBox txt, ref DataTable dt) 
        {
            //TextReader reader = new StringReader(csv);
            var table = new DataTable();


            string[] lines = txt.Text.Split('\n');

            string[] cols = lines[0].Replace("\r", "").Split('\t');

            foreach (string col in cols)
                table.Columns.Add(col);


            for(int i = 1; i < lines.Length; i++) 
            {

                var row = table.NewRow();
                string[] filas = lines[i].Replace("\r", "").Split('\t');
                int k = 0;
                foreach (var fila in filas)
                {
                    row[cols[k]] = fila.Replace("\r", "");
                    k++;
                }
                table.Rows.Add(row);

            }

            //foreach (string line in lines) 
            //{

            //    table.Rows.Add(row);


            //}

            //using (var it = reader.ReadCsvWithHeader().GetEnumerator())
            //{

            //if (!it.MoveNext()) return;

            //foreach (var k in it.Current.Keys)
            //    table.Columns.Add(k);

            //do
            //{
            //    var row = table.NewRow();
            //    foreach (var k in it.Current.Keys)
            //        row[k] = it.Current[k];

            //    table.Rows.Add(row);

            //} while (it.MoveNext());
            //}

            dt = table;



        }

        private void consulta2()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            //txtReporte.Text = "Jornada " + vjornada + '\r' + '\n';
            txtReporte.Text = "";

            datos.p_sql = "SELECT Num, NumCarga,COUNT(*) as Corral,Racion,SUM(Planeado) as Planeado,SUM(Realidad) as Realidad, SUM(Bascula-BasculaIni) as BasApeBasIni FROM Servidas WHERE (Hecho = 'S' OR Hecho = 'X') AND Id = " + vjornada + " GROUP BY Num,NumCarga,Racion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                txtReporte.Text += "Servida" + '\t' + "#Carga" + '\t' + "Corral" + '\t' + "Racion" + '\t' + "Planeado" + '\t' + "Realidad" + '\t' + "Dif" + '\t' + "BApeBIni" + '\r' + '\n'; ;

                foreach (DataRow row in dt.Rows)
                {
                    int difvalor = Int32.Parse(row["Realidad"].ToString()) - Int32.Parse(row["Planeado"].ToString());
                    txtReporte.Text += "Serv " + row["Num"].ToString() + '\t' + row["NumCarga"].ToString() + '\t' + row["Corral"].ToString() + '\t' + row["Racion"].ToString() + '\t' + formatoDecimal(row["Planeado"].ToString()) + '\t' + formatoDecimal(row["Realidad"].ToString()) + '\t' + difvalor.ToString() + '\t' + formatoDecimal(row["BasApeBasIni"].ToString()) + '\r' + '\n';
                }
            }

        }

        private void consulta3()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            //txtReporte.Text = "Jornada " + vjornada + '\r' + '\n';
            txtReporte.Text = "";

            datos.p_sql = "SELECT Num, Racion,COUNT(*) as Corral,SUM(Planeado) as Planeado,SUM(Realidad) as Realidad, SUM(Bascula-BasculaIni) as BasApeBasIni FROM Servidas WHERE (Hecho = 'S' OR Hecho = 'X') AND Id = " + vjornada + " GROUP BY Num,Racion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                txtReporte.Text += "Servida" + '\t' + "Racion" + '\t' + "Corral" + '\t' + "Planeado" + '\t' + "Realidad" + '\t' + "Dif" + '\t' + "BApeBIni" + '\r' + '\n'; ;

                foreach (DataRow row in dt.Rows)
                {
                    int difvalor = Int32.Parse(row["Realidad"].ToString()) - Int32.Parse(row["Planeado"].ToString());
                    txtReporte.Text += "Serv "+row["Num"].ToString() + '\t' + row["Racion"].ToString() + '\t' + row["Corral"].ToString() + '\t' + formatoDecimal(row["Planeado"].ToString()) + '\t' + formatoDecimal(row["Realidad"].ToString()) + '\t' + difvalor.ToString() + '\t' + formatoDecimal(row["BasApeBasIni"].ToString()) + '\r' + '\n';
                }
            }

        }

        private void consulta4()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            //txtReporte.Text = "Jornada " + vjornada + '\r' + '\n';
            txtReporte.Text = "";

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


                txtReporte.Text += "Accion" + '\t' + "HoraServ" + '\t' + "Servida" + '\t' + "Racion" + '\t' + "#Carga" + '\t' + "Corral" + '\t' + "Cantidad" + /*'\t' + "SumCarga" + '\t' + "SumRac" +*/ '\r' + '\n';
                
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
                            txtReporte.Text += "Total" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + formatoDecimal(acumcarga.ToString()) + /*'\t' + acumrac.ToString() +*/ '\r' + '\n';
                        }

                        linea(); 
                    }

                    if (!antCarga.Equals(row["NumCarga"].ToString())) 
                    { 

                        acumcarga = 0;

                        sqlConsultaTxt(txtReporte,"Carga", "SELECT Hora,Num,Racion,IdD,'' as Vacio,Carga FROM Cargas WHERE Id = " + vjornada + " AND IdD = " + row["NumCarga"].ToString());

                        linea();
                       
                    }
                  
                    acumcarga = acumcarga + long.Parse(row["Realidad"].ToString());
                  
                    txtReporte.Text += "Serv" + '\t' + row["Hora"].ToString() + '\t' + row["Num"].ToString() + '\t' + row["Racion"].ToString() + '\t' + row["NumCarga"].ToString() + '\t' + row["Corral"].ToString()  + '\t' + formatoDecimal(row["Realidad"].ToString()) + /*'\t' + acumcarga.ToString() + '\t' + acumrac.ToString() +*/ '\r' + '\n';




                    antServ = row["Num"].ToString();
                    antRac = row["Racion"].ToString();
                    antCarga = row["NumCarga"].ToString();
                
                }

                linea();
                txtReporte.Text += "Total" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + "" + '\t' + formatoDecimal(acumcarga.ToString()) + /*'\t' + acumrac.ToString() +*/ '\r' + '\n';
                linea();



            }

        }

        private void consulta5()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            //txtReporte.Text = "Jornada " + vjornada + '\r' + '\n';
            txtReporte.Text = "";

            datos.p_sql = "SELECT * FROM Cargas WHERE Id = " + vjornada;

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                txtReporte.Text += "#Carga" + '\t' + "Servida" + '\t' + "Hora" + '\t' + "Racion" + '\t' + "Solicito" + '\t' + "Anterior" + '\t' + "Recibio"  + '\t' + "Carga"+ '\t' + "SolCarg" + '\t' + "BasIni" + '\t' + "BasFin" + '\t' + "BasDif" + '\t' + "CargaIni" + '\t' + "BasFCarg" + '\r' + '\n'; 

                foreach (DataRow row in dt.Rows)
                {
                    int difvalor = Int32.Parse(row["Solicito"].ToString()) - Int32.Parse(row["Carga"].ToString());
                    int difvalor2 = Int32.Parse(cadenaCero(row["BasculaFin"].ToString())) - Int32.Parse(cadenaCero(row["BasculaIni"].ToString()));
                    //int difvalor3 = difvalor2 + Int32.Parse(cadenaCero(row["CargaIni"].ToString())) ;
                    int carga = Int32.Parse(cadenaCero(row["CargaIni"].ToString())) + Int32.Parse(cadenaCero(row["Carga"].ToString()));

                    int difvalor4 = Int32.Parse(cadenaCero(row["BasculaFin"].ToString())) - carga;//difvalor2;
                    txtReporte.Text += row["IdD"].ToString() + '\t' + row["Num"].ToString() + '\t' + row["Hora"].ToString() + '\t' + row["Racion"].ToString() + '\t' + formatoDecimal(row["Solicito"].ToString()) + '\t' + formatoDecimal(row["CargaIni"].ToString()) + '\t' + formatoDecimal(row["Carga"].ToString()) + '\t' + formatoDecimal(carga.ToString())  + '\t' + formatoDecimal(difvalor.ToString()) + '\t' + formatoDecimal(row["BasculaIni"].ToString()) + '\t' + formatoDecimal(row["BasculaFin"].ToString()) + '\t' + formatoDecimal(difvalor2.ToString()) + '\t' + formatoDecimal(row["CargaIni"].ToString()) + '\t' + formatoDecimal(difvalor4.ToString()) + '\r' + '\n';
                }
            }

        }

        private void consulta6()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            txtReporte.Text = "";

            datos.p_sql = "SELECT * FROM Raciones WHERE Id = " + vjornada;

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                txtReporte.Text += "Concepto" + '\t' + "Valor" + '\r' + '\n';

                foreach (DataRow row in dt.Rows)
                {


                    txtReporte.Text += "Racion" + '\t' + row["Racion"].ToString() + '\r' + '\n';
                    txtReporte.Text += "Servida" + '\t' + row["Num"].ToString() + '\r' + '\n';
                    txtReporte.Text += "Corrales" + '\t' + row["Cantidad"].ToString() + '\r' + '\n';
                    txtReporte.Text += "Hechos" + '\t' + row["HechoC"].ToString() + '\r' + '\n';
                    txtReporte.Text += "Planeado" + '\t' + formatoDecimal(row["Planeado"].ToString()) + '\r' + '\n';
                    txtReporte.Text += "Cargado" + '\t' + formatoDecimal(row["Cargado"].ToString()) + '\r' + '\n';
                    txtReporte.Text += "Servido" + '\t' + formatoDecimal(row["Servido"].ToString()) + '\r' + '\n';
                    txtReporte.Text += "Falta" + '\t' + formatoDecimal(row["Falta"].ToString()) + '\r' + '\n';

                    long neto = long.Parse(row["Cargado"].ToString()) - long.Parse(row["Servido"].ToString());

                    txtReporte.Text += "Neto" + '\t' + formatoDecimal(neto.ToString()) + '\r' + '\n';


                    txtReporte.Text += "" + '\t' + "" +'\r' + '\n';




                }


            }



        }

        private String formatoDecimal(String valor) 
        {

            //double number = 1234.56;
            //string.Format("{0:0.000}", number)      // 1234.560
            //string.Format("{0:#,0.00}", number)     // 1,234.56 
            //string.Format("{0:#,0.####}", number)   // 1,234.56
            //// Thousand separator and number scaling    
            //string.Format("{0:#,0}", 123000000)     // 123,000,000
            //string.Format("{0:#,0, K}", 123000000)  // 123,000 K
            //string.Format("{0:#,0,, M}", 123000000) // 123 M
            //double number = 1234.56;
            //double numberNeg = -1234.56;
            //double numberZero = 0;
            //string.Format("{0:0.00}", number)               // 1234.56
            //string.Format("{0:0.00}", numberNeg)            // -1234.56
            //string.Format("{0:0.00}", numberZero)           // 0.00
            //string.Format("{0:0.00;(0.00);0}", number)      // 1234.56
            //string.Format("{0:0.00;(0.00);0}", numberNeg)   // (1234.56)
            //string.Format("{0:0.00;(0.00);0}", numberZero)  // 0
            //string.Format("{0:0.00;'neg: '-0.00;zero}", numberNeg)   // neg: -1234.56
            //string.Format("{0:0.00;'neg: '-0.00;zero}", numberZero)  // zero
            //double numberNeg = -1234.56;
            //// unescaped text
            //string.Format("{0:0.00 Number}", number)         // 1234.56 Number
            //string.Format("{0:0.00 #Num.}", number)          // 1234.56 Num
            //// escaped text
            //string.Format("{0:0.00 '#Num.'}", number)        // 1234.56 #Num.


            return String.Format("{0:#,0}", long.Parse(Comun.cadenaCero(valor))); 

        }

        void linea() 
        { 
            //txtReporte.Text += "------------------------------------------------------------------------------------------------------------" + '\r' + '\n';
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

            //string cadena = "";
            
            datos.p_sql = sql;

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    //cadena = "";

                    //for (int i=0;i<row.ItemArray.Length ;i++) 
                    //{ 
                    //    cadena = cadena + row[i].ToString() + '\t';
                    //}
                    
                    valores.Add(string.Join("\t", row.ItemArray));
                }            
            }

            return valores;

        }


        private void comboBoxOpciones_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;

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
                case 5:
                    consulta6();
                    break;

            }

            //--------------------------------------------------------------
            DataTable dt2 = new DataTable();
            txtDt(txtReporte, ref dt2);
            dataGridSQLResultados.DataSource = null;
            dataGridSQLResultados.DataSource = dt2;


            for (int i = 0; i < dt2.Columns.Count; i++)
            {
                dataGridSQLResultados.Columns[i].Width = dataGridSQLResultados.Columns[i].HeaderText.Length*11;
            }


            switch (comboBoxOpciones.SelectedIndex)
            {
                case 0:                    
                    dataGridSQLResultados.Columns["dif"].ToolTipText = "Servido-Planeado";
                    dataGridSQLResultados.Columns["basfcarg"].ToolTipText = "BasFin-Carga";
                    dataGridSQLResultados.Columns["carga"].ToolTipText = "Carga(Anterior)-Servido";
                    dataGridSQLResultados.Columns["ajuste"].ToolTipText = "BasIni-BasFin(Anterior)";
                    dataGridSQLResultados.Columns["realidad"].ToolTipText = "BasApe-BasFin";
                    dataGridSQLResultados.Columns["bapebini"].ToolTipText = "BasApe-BasIni";
                    dataGridSQLResultados.Columns["servido"].ToolTipText = "BasIni-BasFin";
                    break;

            }

            foreach (DataGridViewColumn item in dataGridSQLResultados.Columns)
            {
                if (item.Index == 0)
                {
                    item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
                if (item.Index != 0)
                {
                    item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }                

            }

            //--------------------------------------------------------------



            this.Cursor = Cursors.Default;

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

        private void propiedadesGrid()
        {

            dataGridSQLResultados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridSQLResultados.ReadOnly = true;
            dataGridSQLResultados.AllowUserToAddRows = false;
            dataGridSQLResultados.AllowUserToDeleteRows = false;
            dataGridSQLResultados.AllowUserToOrderColumns = false;
            dataGridSQLResultados.AllowUserToResizeColumns = false;
            dataGridSQLResultados.AllowUserToResizeRows = false;
            dataGridSQLResultados.MultiSelect = false;
                        
            //DGServidas.ColumnHeadersVisible = false;
            dataGridSQLResultados.RowHeadersVisible = false;
            //dataGridSQLResultados.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridSQLResultados.DefaultCellStyle.Font = new Font("Arial", 20.00F, FontStyle.Bold);
            dataGridSQLResultados.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 20.00F, FontStyle.Bold);
            
            //dataGridSQLResultados.AutoResizeColumns();
            dataGridSQLResultados.AutoResizeRows();


            dataGridSQLResultados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridSQLResultados.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //DGServidas.RowTemplate.Height = 50;



        }

        private void btnInfo_Click(object sender, EventArgs e)
        {

            var mensaje =
            "servido  " + "BasIni-BasFin" + "\r\n" +
            "realidad " + "BasApe-BasFin" + "\r\n" +
            "dif      " + "Servido-Planeado" + "\r\n" +
            "basfcarg " + "BasFin-Carga" + "\r\n" +
            "carga    " + "Carga(Anterior)-Servido" + "\r\n" +
            "ajuste   " + "BasIni-BasFin(Anterior)" + "\r\n" +
            "bapebini " + "BasApe-BasIni" + "\r\n" +
 
            "\r\n" +
            "BasApe   " + "Bascula (tabla)" + "\r\n" +
            "BasIni   " + "BasculaIni (tabla)" + "\r\n" +
            "BasFin   " + "BasculaFin (tabla)"; 

    MessageBox.Show(mensaje);






        }
    }
}
