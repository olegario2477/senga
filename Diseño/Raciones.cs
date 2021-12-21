using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Net.NetworkInformation;

namespace Senga
{
    public partial class Raciones : Form
    {
        private string nameruta = "\\senga_archivos";
        private string namea = "\\planes";
        private string nameb = "\\servido";
        private string namec = "\\procesados";

        ConnectedWs connectar;
        DataTable dtUbictmp;

        String usuario = "";
        String contrasena = "";
        String camion = "";
        //Boolean wsactivo = false;
        String msgProceso = "Espere ...";

        private string vrutared;
        private string vrutaarchivos;

        bool activado = false;
        string vjornada;
        private int rowIndex = 0;

        public static Raciones instance = null;

        public Raciones(string id)
        {
            InitializeComponent();
            instance = this;
            vjornada = id;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            //Close();


            //Jornadas frm = new Jornadas();
            ////frm.MdiParent = this;
            //frm.Show();

            Close();

        }

        private void Raciones_Load(object sender, EventArgs e)
        {
                       

            posicionbotones();

            this.Refresh();
            
            configuracionBasica();

            propiedadesGrid();
            propiedadesGrid2();

            seleccionarFila();
            
            //carpeta_existe_local();
            activado = true;

            //usuarioiWs();

            formatoGrid();
            formatoGrid2();
       
            //activar();

            //wsactivo = wsactivoConfigWs();
            //redEstado();
            //if (wsactivo)
            //{
            //    lblWsActivo.Text = "ON";
            //    //lblWsActivoF.Value = 1;
            //    btnReparto.Width = 0;
            //}
            //else
            //{
            //    lblWsActivo.Text = "OFF";
            //    //lblWsActivoF.Value = 0;
            //}

            panelRacion.Visible = true;
            this.Refresh();

            //pbServido.Visible = true;

            //vrutared = rutared();
            //vrutaarchivos = rutaarchivos();

            DGRaciones.Visible = true;
            DGCargas.Visible = true;
            lblCantidad.Visible = true;
            lblCantidad.Text = msgProceso;

            this.Refresh();

            activar();

            lblCantidad.Visible = false;//Text = "";

            tiempoServido();

            this.Refresh();

            carpeta_existe_local();
            usuarioiWs();
            vrutared = rutared();
            vrutaarchivos = rutaarchivos();

            
            this.Refresh();

        }

        private void Raciones_Activated(object sender, EventArgs e)
        {

            activar();

        }

        private void activar()
        {

            if (activado)
            {
                operacionRacion();
                limpiarRacion();
                
                refrescaGrid();
                
                //tiempoServido();


                //var th = new Thread(refrescaGrid);
                //th.Start();


                if (DGRaciones.RowCount == 0)
                {
                    limpiarRacion();
                }
                //else
                //{
                //    //txtIdD.Text = (string)DGServidas.CurrentRow.Cells["IdD"].Value;
                //    //filaDatos(txtIdD.Text);   

                //}

                //seleccionarFila();

                //DGRaciones.Rows.IndexOf

                //DGRaciones.FirstDisplayedScrollingRowIndex = rowIndex;
                //DGRaciones.Rows[rowIndex].Selected = true;



                //barra();

                //cantplanes();
                //cantservidos();

            }

        }

        private void posicionbotones()
        {
            btnCerrar.Top = 0;
            btnServir.Top = 0;
            btnArchivos.Top = 0;
            btnLista.Top = 0;
            btnSincroniza.Top = 0;
            btnExplorer.Top = 0;
            btnUSB.Top = 0;
            btnNube.Top = 0;
            btnWsRecibir.Top = 0;
            btnWsEnviar.Top = 0;
            btnServida.Top = 0;
            btnPlanta.Top = 0;
            btnJornadas.Top = 0;
            btnReportes.Top = 0;

            //oculta boton
            btnExplorer.Width = 0;
            //btnSincroniza.Width = 0;
            btnWsEnviar.Width = 0;
            btnLista.Width = 0;
            btnArchivos.Width = 0;
            btnSincroniza.Width = 0;

            btnCerrar.Left = this.Width - btnCerrar.Width;
            btnServir.Left = btnCerrar.Left - btnServir.Width;
            btnArchivos.Left = btnServir.Left - btnArchivos.Width;
            btnLista.Left = btnArchivos.Left - btnLista.Width;
            btnSincroniza.Left = btnLista.Left - btnSincroniza.Width;
            btnExplorer.Left = btnSincroniza.Left - btnExplorer.Width;
            btnUSB.Left = btnExplorer.Left - btnUSB.Width;
            btnNube.Left = btnUSB.Left - btnNube.Width;

            btnWsEnviar.Left = btnNube.Left - btnWsEnviar.Width;
            btnWsRecibir.Left = btnWsEnviar.Left - btnWsRecibir.Width;

            btnServida.Left = btnWsRecibir.Left - btnServida.Width;
            btnPlanta.Left = btnServida.Left - btnPlanta.Width;

            btnReportes.Left = btnPlanta.Left - btnReportes.Width;
            btnJornadas.Left = btnReportes.Left - btnJornadas.Width;



            lblPlanes.Top = btnLista.Top + 5;
            lblPlanes.Left = btnLista.Left + 5;

            lblServidos.Top = btnArchivos.Top + 5;
            lblServidos.Left = btnArchivos.Left + 5;

            lblPlanes.Visible = false;
            lblServidos.Visible = false;

            //DGJornadas.Left = 0;
            //DGJornadas.Width = this.Width;


        }
        private void propiedadesGrid()
        {

            DGRaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGRaciones.ReadOnly = true;
            DGRaciones.AllowUserToAddRows = false;
            DGRaciones.AllowUserToDeleteRows = false;
            DGRaciones.AllowUserToOrderColumns = false;
            DGRaciones.AllowUserToResizeColumns = false;
            DGRaciones.AllowUserToResizeRows = false;
            DGRaciones.MultiSelect = false;
            //DGRaciones.ColumnHeadersVisible = false;
            DGRaciones.RowHeadersVisible = false;
            DGRaciones.CellBorderStyle = DataGridViewCellBorderStyle.None;
            DGRaciones.DefaultCellStyle.Font = new Font("Arial", 20.00F, FontStyle.Bold);
            //DGRaciones.RowTemplate.Height = 50;


        }

        private void propiedadesGrid2()
        {

            DGCargas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGCargas.ReadOnly = true;
            DGCargas.AllowUserToAddRows = false;
            DGCargas.AllowUserToDeleteRows = false;
            DGCargas.AllowUserToOrderColumns = false;
            DGCargas.AllowUserToResizeColumns = false;
            DGCargas.AllowUserToResizeRows = false;
            DGCargas.MultiSelect = false;
            //DGRaciones.ColumnHeadersVisible = false;
            DGCargas.RowHeadersVisible = false;
            DGCargas.CellBorderStyle = DataGridViewCellBorderStyle.None;
            DGCargas.DefaultCellStyle.Font = new Font("Arial", 20.00F, FontStyle.Bold);
            //DGRaciones.RowTemplate.Height = 50;

            DGCargas.RowsDefaultCellStyle.SelectionBackColor = Color.White;
            DGCargas.DefaultCellStyle.SelectionForeColor = Color.Black;

        }

        private Boolean wsactivoConfigWs()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            Boolean valor = false;

            datos.p_sql = "SELECT TOP 1 * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["ws"].ToString().Equals("S")) { valor = true; } else { valor = false; }
                }
            }

            return valor;

        }

        private void refrescaGrid()
        {
            //formatoGrid();

            datosGrid();

            if (DGRaciones.RowCount > 0)
            {
                DGRaciones.CurrentCell = DGRaciones.Rows[rowIndex].Cells[0];
                DGRaciones.Rows[rowIndex].Selected = true;
            }

            refreshCargas();

        }

        private void formatoGrid()
        {

            DataTable table = new DataTable();

            table.Columns.Add("Racion", typeof(string));
            table.Columns.Add("Corrales", typeof(string));

            //table.Columns.Add("Hechos", typeof(long));
            table.Columns.Add("Plan", typeof(long));
            //table.Columns.Add("Variacion", typeof(string));
            table.Columns.Add("Cargas", typeof(long));
            table.Columns.Add("Servido", typeof(long));
            //table.Columns.Add("Carga", typeof(long));
            table.Columns.Add("Falta", typeof(long));
            table.Columns.Add("Neto", typeof(long));
            table.Columns.Add("IdD", typeof(string));

            DGRaciones.DataSource = table;

            for (int i = 0; i < table.Columns.Count; i++)
            {
                DGRaciones.Columns[i].Width = 125;
                DGRaciones.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGRaciones.Columns[i].HeaderCell.Style.Font = new Font("Tahoma", 15.00F, FontStyle.Bold);
                DGRaciones.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                DGRaciones.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            DGRaciones.Columns["IdD"].Width = 0;
            //DGRaciones.Columns["Bruto"].Width = 0;

            DGRaciones.Columns["Racion"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DGRaciones.Columns["Corrales"].DefaultCellStyle.Format = "#,0.###";

            //DGRaciones.Columns["Hechos"].DefaultCellStyle.Format = "#,0.###";
            DGRaciones.Columns["Plan"].DefaultCellStyle.Format = "#,0.###";
            DGRaciones.Columns["Cargas"].DefaultCellStyle.Format = "#,0.###";
            DGRaciones.Columns["Servido"].DefaultCellStyle.Format = "#,0.###";
            //DGRaciones.Columns["Carga"].DefaultCellStyle.Format = "#,0.###";
            DGRaciones.Columns["Falta"].DefaultCellStyle.Format = "#,0.###";
            DGRaciones.Columns["Neto"].DefaultCellStyle.Format = "#,0.###";

            //DGRaciones.DataSource = table;

        }

        private void formatoGrid2()
        {

            DataTable table = new DataTable();

            table.Columns.Add("IdD", typeof(string));
            //table.Columns.Add("Hora", typeof(string));
            //table.Columns.Add("Tolva", typeof(string));
            table.Columns.Add("Solicito", typeof(long));
            //table.Columns.Add("Anterior", typeof(long));
            table.Columns.Add("Recibio", typeof(long));
            //table.Columns.Add("Servido", typeof(long));
            //table.Columns.Add("Carga", typeof(long));


            DGCargas.DataSource = table;

            for (int i = 0; i < table.Columns.Count; i++)
            {
                DGCargas.Columns[i].Width = 125;
                DGCargas.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGCargas.Columns[i].HeaderCell.Style.Font = new Font("Tahoma", 15.00F, FontStyle.Bold);
                DGCargas.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                DGCargas.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            //DGCargas.Columns["IdD"].Width = 0;
            //DGRaciones.Columns["Bruto"].Width = 0;


            //DGCargas.Columns["Hora"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //DGCargas.Columns["Tolva"].DefaultCellStyle.Format = "#,0.###";
            DGCargas.Columns["Solicito"].DefaultCellStyle.Format = "#,0.###";
            //DGCargas.Columns["Anterior"].DefaultCellStyle.Format = "#,0.###";
            DGCargas.Columns["Recibio"].DefaultCellStyle.Format = "#,0.###";
            //DGCargas.Columns["Servido"].DefaultCellStyle.Format = "#,0.###";
            //DGCargas.Columns["Carga"].DefaultCellStyle.Format = "#,0.###";

            //DGRaciones.DataSource = table;

            DGCargas.Columns["IdD"].HeaderText = "# Carga";

        }

        private void datosGrid()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();
            DataTable table = (DataTable)(DGRaciones.DataSource);

            table.Rows.Clear();

            string idd = "0";
            bool primero = true;


            //datos.p_sql = "SELECT Racion,Cantidad,HechoC,Planeado,Cargado,Servido,Falta,Carga,IdD FROM raciones WHERE num = " + txtServida.Text + " and id = " + vjornada; //" " and ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ";
            datos.p_sql = "SELECT * FROM raciones WHERE num = " + txtServida.Text + " and id = " + vjornada; //" " and ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {


                foreach (DataRow row in dt.Rows)
                {                

                    long neto = long.Parse(row["Cargado"].ToString()) - long.Parse(row["Servido"].ToString());

                    table.Rows.Add(row["Racion"], (row["HechoC"].ToString() + "/" + row["Cantidad"].ToString()), row["Planeado"].ToString(), row["Cargado"].ToString(), row["Servido"].ToString(), Convert.ToInt32(row["Falta"]), neto, row["IdD"].ToString());

                    if (row["Cargado"].ToString() == "0" && primero)
                    //if (primero)
                    {
                        idd = row["IdD"].ToString();
                        primero = false;
                    }
                }


                datos.p_sql = "UPDATE operacion SET iddr = " + idd.ToString();

                if (!datos.consultaSQL(ref dt, false))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

            }


        }

        private void datosGrid2()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable table = (DataTable)(DGCargas.DataSource);

            table.Rows.Clear();

            string idd = "0";
            bool primero = true;

            datos.p_sql = "SELECT NumCarga,Sum(Realidad) as Realidad FROM Servidas WHERE num = " + txtServida.Text + " and id = " + vjornada + " and racion = '" + txtRacion.Text + "' group by NumCarga";

            if (!datos.consultaSQL(ref dt2, true))
            {
                //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            //datos.p_sql = "SELECT Hora,Racion,Solicito,Carga,Tolva,BasculaIni,CargaIni,IdD FROM Cargas WHERE num = " + txtServida.Text + " and id = " + vjornada + " and racion = '" + txtRacion.Text + "' order by IdD asc";
            datos.p_sql = "SELECT * FROM Cargas WHERE num = " + txtServida.Text + " and id = " + vjornada + " and racion = '" + txtRacion.Text + "' order by IdD asc";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {

                long difant = 0;

                foreach (DataRow row in dt.Rows)
                {

                    string realidad = "0";

                    foreach (DataRow row2 in dt2.Rows)
                    {
                        if (row2["NumCarga"].ToString().Equals(row["IdD"].ToString()))
                        {
                            realidad = row2["Realidad"].ToString();
                            break;
                        }

                    }

                    //long anterior = Convert.ToInt32(cadenaCero(row["BasculaIni"].ToString()));
                    long anterior = Convert.ToInt32(cadenaCero(row["CargaIni"].ToString()));
                    long dif = anterior + (Convert.ToInt32(cadenaCero(row["Carga"].ToString())));//- Convert.ToInt32(cadenaCero(realidad)) );

                    long carga = dif;
                    //------------------------------------
                    //long res = difant - anterior;
                    //long carga = dif + res;
                    //------------------------------------

                    //Convert.ToInt32(row["Cargado"]) - Convert.ToInt32(row["Planeado"])
                    table.Rows.Add(row["IdD"].ToString(),/* row["Hora"].ToString(), row["Tolva"].ToString(),*/ row["Solicito"].ToString(), /*anterior.ToString(),*/ row["Carga"].ToString()/*, carga.ToString()*//*realidad.ToString(),*/ );

                    difant = dif;

                    //if (row["Cargado"].ToString() == "0" && primero)
                    ////if (primero)
                    //{
                    //    idd = row["IdD"].ToString();
                    //    primero = false;
                    //}
                }


                //datos.p_sql = "UPDATE operacion SET iddr = " + idd.ToString();

                //if (!datos.consultaSQL(ref dt, false))
                //{
                //    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //}

            }


        }

        private void DGRaciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (DGRaciones.CurrentRow != null)
            //{
            //    txtIdDr.Text = (string)DGRaciones.CurrentRow.Cells["IdD"].Value;

            //    filaDatos(txtIdDr.Text);
            //}         

        }


        private String cant_vueltas(String plan, String capacidad)
        {

            int resultado = 0;
            int entero = 0;
            double decimales = 0;
            double division = 0;

            division = Convert.ToDouble(plan) / Convert.ToDouble(capacidad);
            entero = (int)Math.Truncate(division);
            decimales = division - entero;

            if (decimales > 0)
            {
                entero = entero + 1;
            }

            resultado = entero;
            return resultado.ToString();

        }

        private void DGRaciones_SelectionChanged(object sender, EventArgs e)
        {

            refreshCargas();

        }

        void refreshCargas()
        {

            if (DGRaciones.CurrentRow != null)
            {
                txtIdDr.Text = (string)DGRaciones.CurrentRow.Cells["IdD"].Value;

                filaDatos(txtIdDr.Text);
            }

            datosGrid2();
        }

        private void btnServida_Click(object sender, EventArgs e)
        {
            Senga.Captura frm = new Senga.Captura(Convert.ToInt32(txtJornada.Text), 1);
            frm.Show();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {


            if (DGRaciones.RowCount != 0)
            {

                rowIndex = DGRaciones.CurrentRow.Index;

                if (txtCorrales.Text == txtHechos.Text)
                {
                    //MessageBox.Show("Racion Servida Completada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    Comun.mensajeVer("Racion Servida Completada", Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);

                }
                else
                {


                    Datos datos = new Datos();
                    DataTable dt = new DataTable();

                    datos.p_sql = "UPDATE operacion SET accion = 'C'" + ", racion = '" + txtRacion.Text + "'";

                    if (!datos.consultaSQL(ref dt, true))
                    {
                        MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }



                    if (txtIdDr.Text != "")
                    {

                        Senga.Racion frm = new Senga.Racion(txtIdDr.Text, txtJornada.Text);
                        frm.Show();
                    }
                }

            }

        }

        private void filaDatos(string idd)
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            datos.p_sql = "SELECT * FROM raciones WHERE idd =" + idd.ToString();

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {

                foreach (DataRow row in dt.Rows)
                {
                    //txtCorral.Text = row["Corral"].ToString();
                    txtRacion.Text = row["Racion"].ToString();
                    txtServido.Text = row["Servido"].ToString();

                    txtCargado.Text = row["Cargado"].ToString();
                    txtCarga.Text = row["Carga"].ToString();
                    txtCorrales.Text = row["Cantidad"].ToString();
                    txtHechos.Text = row["HechoC"].ToString();

                    txtPlaneado.Text = row["Planeado"].ToString();
                    txtNumCarga.Text = row["NumCarga"].ToString();

                    //txtCabezas.Text = row["Cabezas"].ToString();
                }

                //datos.p_sql = "UPDATE operacion SET idd =" + idd.ToString();

                //if (!datos.consultaSQL(ref dt, false))
                //{
                //    MessageBox.Show(datos.p_error);
                //}



                //botonesHabilita(true);

                //txtVueltas.Text = cant_vueltas(txtPlaneado.Text, txtCapacidad.Text);
                //barra();
            }

        }

        private void seleccionarFila()
        {

            if (DGRaciones.RowCount != 0)
            {
                Datos datos = new Datos();
                DataTable dt = new DataTable();

                string searchValue = "0";
                //int filas = 0;
                int rowIndex = -1;

                //datos.p_sql = "SELECT iddr FROM operacion";
                datos.p_sql = "SELECT * FROM operacion";

                if (!datos.consultaSQL(ref dt, true))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        searchValue = row["IdDr"].ToString();
                    }
                }

                if (searchValue != "0")
                {

                    foreach (DataGridViewRow row in DGRaciones.Rows)
                    {
                        if (row.Cells["IdD"].Value.ToString() == searchValue)
                        {
                            rowIndex = row.Index;
                            break;
                        }
                    }

                    if (rowIndex != -1)
                    {
                        DGRaciones.Rows[rowIndex].Selected = true;

                        txtIdDr.Text = searchValue;

                        filaDatos(txtIdDr.Text);
                    }

                }



            }
        }

        private void operacionRacion()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            datos.p_sql = "SELECT * FROM operacion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    txtJornada.Text = row["Jornada"].ToString();
                    txtServida.Text = row["Servida"].ToString();
                    txtPlanta.Text = row["Planta"].ToString();
                    txtTolva.Text = row["Tolva"].ToString();
                    //txtRacion.Text = row["Racion"].ToString();
                }
            }

            datos.p_sql = "SELECT * FROM jornadas WHERE id = "+ txtJornada.Text;

            if (!datos.consultaSQL(ref dt, true))
            {
                //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    txtFechaJornada.Text = row["Fecha"].ToString().Substring(0,10);
                }
            }

            //datos.p_sql = "SELECT TOP 1 * FROM configuracion";

            //if (!datos.consultaSQL(ref dt, true))
            //{
            //    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //}
            //else
            //{
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        txtCamion.Text = row["NumCamion"].ToString();
            //        txtCapacidad.Text = row["Capacidad"].ToString();
            //    }
            //}


        }

        private void configuracionBasica() 
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            datos.p_sql = "SELECT * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    txtCamion.Text = row["NumCamion"].ToString();
                    txtCapacidad.Text = row["Capacidad"].ToString();
                }
            }

        }

        private void limpiarRacion()
        {

            //txtCorral.Text = "";
            txtRacion.Text = "";
            //txtCabezas.Text = "";
            txtIdDr.Text = "";

            txtServido.Text = "";
            txtCargado.Text = "";
            txtCarga.Text = "";
            txtCorrales.Text = "";
            txtHechos.Text = "";

            txtVueltas.Text = "";

            //botonesHabilita(false);

        }

        private void barra()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();
            DataTable table = (DataTable)(DGRaciones.DataSource);

            int maximo = 0;
            int valor = 0;
            int raciones = 0;

            datos.p_sql = "SELECT count(racion) as Raciones , sum(planeado) as Planeado, sum(servido) as Realidad, sum(falta) as Falta FROM raciones WHERE num = " + txtServida.Text + " and id = " + vjornada + " and racion = '" + txtRacion.Text + "'"; // " and  ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {

                    if (row["Raciones"].ToString() != "0")
                    {
                        maximo = Convert.ToInt32(row["Planeado"].ToString());
                        valor = Convert.ToInt32(row["Planeado"].ToString()) - Convert.ToInt32(row["Falta"].ToString());
                        raciones = Convert.ToInt32(row["Raciones"].ToString());
                    }
                }
            }

            pbServido.Maximum = maximo;

            //txtPlan.Text = maximo.ToString();
            //txtCorrales.Text = raciones.ToString();

            if (maximo <= valor)
            {
                valor = maximo;
            }
            //pbServido.Value = valor;


        }

        private void quitadifdiaplan()
        {

            string sourcePath = rutaarchivos() + nameruta + namea;
            string targetproc = rutaarchivos() + nameruta + namea + namec;

            foreach (String srcPath in Directory.GetFiles(sourcePath))
            {

                string namefile = Path.GetFileName(srcPath);

                string[] lines = namefile.Split('_');
                string tipo = lines[0].Trim();
                string line = lines[1].Trim();
                string fecha = lines[2].Substring(0, 6);

                if (!(tipo.Equals("P") && line.Equals(txtCamion.Text) && fecha.Equals(DateTime.Now.ToString("ddMMyy"))))
                {

                    moverArchivo(srcPath, targetproc, true);

                    //namefile = Path.GetFileName(srcPath);
                    //namefile = namefile.Replace(".txt", "_" + DateTime.Now.ToString("ddMMyyHHmmss") + ".txt")
                    //                   .Replace(".TXT", "_" + DateTime.Now.ToString("ddMMyyHHmmss") + ".TXT");

                    //string newname = targetproc + "\\" + namefile;

                    //try
                    //{
                    //    File.Move(srcPath, newname);
                    //}
                    //catch (Exception)
                    //{

                    //}

                }


            }

        }

        private void quitaArchivosde(String valor)
        {

            string sourcePath = "";
            string targetproc = "";


            if (valor.Equals("P"))
            {
                sourcePath = rutaarchivos() + nameruta + namea;
                targetproc = rutaarchivos() + nameruta + namea + namec;
            }
            else if (valor.Equals("S"))
            {
                sourcePath = rutaarchivos() + nameruta + nameb;
                targetproc = rutaarchivos() + nameruta + nameb + namec;
            }

            foreach (String srcPath in Directory.GetFiles(sourcePath))
            {

                string namefile = Path.GetFileName(srcPath);

                string[] lines = namefile.Split('_');
                string tipo = lines[0].Trim();
                string line = lines[1].Trim();
                string fecha = lines[2].Substring(0, 6);

                //if (!(tipo.Equals("P")))
                //{
                moverArchivo(srcPath, targetproc, true);
                //}


            }

        }

        private void lista(Boolean vmsg)
        {

            string vservida = "1";
            int resp = Constants.MSG_BOTON_SI;

            //quitadifdiaplan();
            cantplanes();


            if (lblPlanes.Text.Equals("0"))
            {
                if (vmsg)
                { Comun.mensajeVer("Sin Planes de Servidas Para Cargar", Constants.MSG_BOTON_OK, Constants.MSG_IMG_WARNING); }
                return;
            }

            if (vmsg)
            {
                resp = Comun.mensajeVer("¿ Desea Cargar Plan de Servidas ?", Constants.MSG_BOTON_SINO, Constants.MSG_IMG_QUESTION);
            }

            if (resp == Constants.MSG_BOTON_SI)
            {

                Datos datos = new Datos();
                DataTable dt = new DataTable();
                DataTable dt2 = new DataTable();

                List<string> dbOperations = new List<string>();

                string sourcePath = rutaarchivos() + nameruta + namea;
                string targetproc = rutaarchivos() + nameruta + namea + namec;

                lblCantidad.Visible = true;
                lblCantidad.Text = msgProceso;

                int registros_ins = 0;
                int tipof = 0;
                string linea = "";

                foreach (String srcPath in Directory.GetFiles(sourcePath))
                {

                    string namefile = Path.GetFileName(srcPath);

                    if (namefile.Substring(0, 2).Equals("P_"))
                    {
                        tipof = 1;
                    }

                    if (namefile.Contains("DS_UNDON"))
                    {
                        tipof = 2;
                    }

                    if (tipof != 0)
                    {

                        //string[] lineas = namefile.Split('_');
                        //string line = lineas[1].Trim();
                        //string fecha = lineas[2].Substring(0, 6);

                        //if (fecha.Equals(DateTime.Now.ToString("ddMMyy")))
                        //{

                        //OpenFileDialog openFileDialog1 = new OpenFileDialog();
                        //openFileDialog1.Filter = "Archivos Txt|*.txt";
                        //openFileDialog1.Title = "Seleccion un Archivo Texto";

                        //openFileDialog1.InitialDirectory = rutaarchivos() + nameruta + namea;//@"C:\";

                        int registros_sel = 0;

                        int registrost = 0;

                        //if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        //{

                        //this.Refresh();
                        //pbAgregar.Value = 0;
                        //pbAgregar.Visible = true;

                        this.Cursor = Cursors.WaitCursor;

                        try
                        {
                            //StreamReader objReader = new StreamReader(openFileDialog1.OpenFile());
                            StreamReader objReader = new StreamReader(srcPath);
                            string sLine = "";


                            while (sLine != null)
                            {
                                sLine = objReader.ReadLine();
                                if (sLine != null)
                                {
                                    registrost = registrost + 1;
                                }
                            }

                            //pbAgregar.Maximum = registrost;

                            objReader.Close();

                            //objReader = new StreamReader(openFileDialog1.OpenFile());
                            objReader = new StreamReader(srcPath);
                            sLine = "";

                            //activado = false;
                            int registrosact = 0;
                            //ArrayList arrText = new ArrayList();

                            while (sLine != null)
                            {

                                sLine = objReader.ReadLine();

                                if (sLine != null)
                                //arrText.Add(sLine);
                                {

                                    registrosact = registrosact + 1;

                                    string[] lines = { };

                                    if (tipof == 1)
                                    {
                                        lines = sLine.Split(',');
                                    }
                                    //

                                    if (tipof == 2)
                                    {

                                        if (registrosact == 1)
                                        {
                                            continue;
                                        }
                                        else
                                        {

                                            linea = sLine + " " + registrosact.ToString();
                                            string[] lines2 = sLine.Split(',');
                                            lines = new string[lines2.Length];

                                            if (lines2.Length < 2)
                                            {
                                                continue;
                                            }
                                            else
                                            {

                                                if (!lines2[2].Trim().Equals("P"))
                                                {
                                                    continue;
                                                }
                                                else
                                                {

                                                    lines[0] = txtCamion.Text;
                                                    lines[1] = lines2[4].Substring(0, 1).Trim();
                                                    lines[2] = lines2[5].Trim();
                                                    lines[3] = lines2[6].Trim();
                                                    lines[4] = lines2[13].Trim();
                                                    lines[5] = lines2[7].Trim();
                                                    lines[6] = "00:00";
                                                    lines[7] = "0";

                                                }

                                            }

                                        }

                                    }


                                    //

                                    try
                                    {

                                        registros_sel = registros_sel + 1;
                                        //pbAgregar.Value = registros_sel;

                                        //lblCantidad.Text = registros_sel.ToString() + " - " + registrost.ToString();
                                        lblCantidad.Refresh();

                                        //if (txtCamion.Text.Equals(lines[0].Trim()))
                                        //{

                                        //datos.p_sql = "SELECT idd FROM servidas WHERE num = " + lines[1].Trim() + " and corral = '" + lines[2].Trim() + "' and racion = '" + lines[3].Trim() + "' and id = " + vjornada; //"' and ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " )";

                                        //if (!datos.consultaSQL(ref dt, true))
                                        //{
                                        //    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                        //}

                                        //if (dt.Rows.Count == 0)
                                        //{

                                        //datos.p_sql = "insert into servidas (id,fecha,num,corral,racion,cabezas,planeado,realidad,hora,horaplan,plandia) values(" + vjornada.ToString() + ",'" + DateTime.Now.ToString("dd-MM-yyyy") + "'," + Convert.ToInt32(lines[1].Trim()) + ",'" + lines[2].Trim() + "','" + lines[3].Trim() + "'," + Convert.ToInt32(lines[4].Trim()) + "," + Convert.ToInt32(lines[5].Trim()) + "," + "0" + ",'" + "00:00:00" + "','" + lines[6].Trim() + "'," + Convert.ToInt32(lines[7].Trim()) + ")";


                                        datos.p_sql = "insert into servidas (id,fecha,num,corral,racion,cabezas,planeado,realidad,hora,horaplan,plandia) select top 1 " + vjornada.ToString() + ",'" + DateTime.Now.ToString("dd-MM-yyyy") + "'," + Convert.ToInt32(lines[1].Trim()) + ",'" + lines[2].Trim() + "','" + lines[3].Trim() + "'," + Convert.ToInt32(lines[4].Trim()) + "," + Convert.ToInt32(lines[5].Trim()) + "," + "0" + ",'" + "00:00:00" + "','" + lines[6].Trim() + "'," + Convert.ToInt32(lines[7].Trim()) + " from jornadas where not exists ( select * from servidas where num = " + lines[1].Trim() + " and corral = '" + lines[2].Trim() + "' and racion = '" + lines[3].Trim() + "' and id = " + vjornada + ")";

                                        vservida = lines[1].Trim();
                                        dbOperations.Add(datos.p_sql);
                                        registros_ins = registros_ins + 1;

                                        //if (!datos.consultaSQL(ref dt, false))
                                        //{
                                        //    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                        //}
                                        //else
                                        //{
                                        //    registros_ins = registros_ins + 1;
                                        //}

                                        //}


                                        //}

                                    }
                                    catch (Exception exc)
                                    {
                                        if (vmsg) { MessageBox.Show(exc.Message); }
                                        lblCantidad.Visible = false;
                                    }
                                    finally
                                    {
                                        //pbAgregar.PerformStep();
                                        //pbAgregar.Refresh();

                                        //lblCantidad.Text = registros_sel.ToString() + " - " + registrost.ToString();
                                        //lblCantidad.Refresh();

                                        //this.Refresh();
                                        //this.Focus();
                                    }

                                }
                            }

                            objReader.Close();
                            //activado = true;

                            datos.p_sql_list = dbOperations;
                            if (!datos.consultaSQLs(ref dt))
                            {
                                if (vmsg) { MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                            }
                            else
                            {
                                //registros_ins = registros_ins + 1;
                            }

                            //----------------------------------------------------------------

                            datos.p_sql = "SELECT num as Num, racion as Racion, count(corral) as Corrales , sum(planeado) as Planeado, sum(realidad) as Realidad FROM servidas WHERE id = " + vjornada + " group by num, racion"; //( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) group by num, racion";

                            if (!datos.consultaSQL(ref dt, true))
                            {
                                if (vmsg) { MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                            }
                            else
                            {
                                foreach (DataRow row in dt.Rows)
                                {

                                    if (row["Corrales"].ToString() != "0")
                                    {

                                        datos.p_sql = "SELECT idd FROM raciones WHERE num = " + row["Num"].ToString() + " and racion = '" + row["Racion"].ToString() + "' and id = " + vjornada; // "' and ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " )";

                                        if (!datos.consultaSQL(ref dt2, true))
                                        {
                                            if (vmsg)
                                            { MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                                        }

                                        if (dt2.Rows.Count == 0)
                                        {
                                            long falta = long.Parse(row["Planeado"].ToString());
                                            datos.p_sql = "insert into raciones (id,fecha,num,racion,cantidad,planeado,falta,cargado,servido,hechoc,carga,hora) values(" + vjornada.ToString() + ",'" + DateTime.Now.ToString("dd-MM-yyyy") + "'," + row["Num"].ToString() + ",'" + row["Racion"].ToString() + "'," + row["Corrales"].ToString() + "," + row["Planeado"].ToString() + "," + falta.ToString() + ",0,0,0,0,'00:00:00')";
                                        }
                                        else
                                        {
                                            long falta = long.Parse(row["Planeado"].ToString());
                                            datos.p_sql = "UPDATE raciones SET cantidad = " + row["Corrales"].ToString() + " , planeado = " + row["Planeado"].ToString() + " , falta = " + falta.ToString() + " WHERE num = " + row["Num"].ToString() + " and racion = '" + row["Racion"].ToString() + "' and ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " )";
                                        }

                                        if (!datos.consultaSQL(ref dt2, false))
                                        {
                                            if (vmsg)
                                            { MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                                        }


                                    }

                                }

                            }




                        }
                        catch (Exception msgerror)
                        {
                            //MessageBox.Show("El archivo no se puede leer");
                            if (vmsg)
                            { Comun.mensajeVer("El archivo no se puede leer " + msgerror + " " + linea, Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP); }
                            this.Cursor = Cursors.Default;
                            lblCantidad.Visible = false;
                            return;
                        }

                        //}

                        //}
                        //else
                        //{

                        moverArchivo(srcPath, targetproc, true);

                        //namefile = Path.GetFileName(srcPath);
                        //namefile = namefile.Replace(".txt", "_" + DateTime.Now.ToString("ddMMyyHHmmss") + ".txt")
                        //                   .Replace(".TXT", "_" + DateTime.Now.ToString("ddMMyyHHmmss") + ".TXT");

                        //string newname = targetproc + "\\" + namefile;

                        //try
                        //{
                        //    File.Move(srcPath, newname);
                        //}
                        //catch (Exception)
                        //{

                        //}                        

                        //}


                        //if (File.Exists(srcPath))
                        //{

                        //    string newname = srcPath.Replace("P_", "X_");
                        //    //File.Copy(srcPath, newname, true);
                        //    //File.Delete(srcPath);

                        //    if (File.Exists(newname))
                        //    {
                        //        File.Delete(newname);
                        //    }

                        //    //namefile = Path.GetFileName(newname);
                        //    //newname = targetproc + "\\" + namefile;

                        //    File.Move(srcPath, newname);

                        //}

                        break;

                    }

                    //if (!fecha.Equals(DateTime.Now.ToString("ddMMyy")))
                    //{

                    //    namefile = Path.GetFileName(srcPath);
                    //    string newname = targetproc + "\\" + namefile;

                    //    File.Move(srcPath, newname);

                    //}

                }

                this.Cursor = Cursors.Default;

                cantplanes();

                lblCantidad.Visible = false;

                //MessageBox.Show("Listo, Regitros : " + registros_ins.ToString(), "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                elejirServida(vservida);

                activar();

                //MessageBox.Show("Listo, Cargo Plan : " + registros_ins.ToString() + " Registros", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                if (vmsg) { Comun.mensajeVer("Cargo Plan : " + registros_ins.ToString() + " Registros", Constants.MSG_BOTON_OK, Constants.MSG_IMG_OK); }

                pbAgregar.Visible = false;
                DGRaciones.Focus();

            }

        }

        private void btnLista_Click(object sender, EventArgs e)
        {

            lista(true);

        }

        private void btnServir_Click(object sender, EventArgs e)
        {

            if (DGRaciones.RowCount != 0)
            {
                //string a = txtCarga.Text;
                //if (txtCargado.Text == "0")
                //{
                //    Comun.mensajeVer("Requiere Carga Para Servidas", Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
                //}
                //else
                //{

                    if (txtCorrales.Text == txtHechos.Text)
                    {
                        //MessageBox.Show("Racion Servida Completada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        Comun.mensajeVer("Racion Servida Completada", Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);

                    }
                    else
                    {

                        if (DGRaciones.RowCount == 0)
                        {
                            //MessageBox.Show("No hay Raciones", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            Comun.mensajeVer("No hay Raciones", Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
                        }
                        else
                        {
                            Datos datos = new Datos();
                            DataTable dt = new DataTable();

                            datos.p_sql = "UPDATE operacion SET accion = 'S'";

                            if (!datos.consultaSQL(ref dt, true))
                            {
                                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }

                            datos.p_sql = "UPDATE operacion SET racion = '" + txtRacion.Text + "'";

                            if (!datos.consultaSQL(ref dt, true))
                            {
                                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }


                            string vracion = txtRacion.Text;
                            string vnumcarga = txtNumCarga.Text;



                            Servidas frm = new Servidas(vracion, vnumcarga);
                            //frm.MdiParent = this;
                            frm.Show();


                            Close();


                        }

                    }
                //}
            }
        }

        //private void btnJornadas_Click(object sender, EventArgs e)
        //{

        //    Close();

        //    Jornadas frm = new Jornadas();
        //    //frm.MdiParent = this;
        //    frm.Show();
        //}


        private void grabar(Boolean vmsg, ref String file)
        {

            string rutadestino = "";
            int resp = Constants.MSG_BOTON_SI;
            file = "";

            if (DGRaciones.RowCount != 0)
            {

                //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                //saveFileDialog1.Filter = "Archivos Txt|*.txt";
                //saveFileDialog1.Title = "Guardar Como ...";
                //saveFileDialog1.FileName = "T_SERVIDO_" + numcamion() + "_" + txtServida.Text + "_" + DateTime.Now.ToString("ddMMyyHHmm");

                //saveFileDialog1.InitialDirectory = rutaarchivos() + "\\archivos_servidas\\servido";//@"C:\";
                if (!hayServidos())
                //if (txtServido.Text.Equals("0"))
                {
                    //MessageBox.Show("Debe haber Servidas", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    if (vmsg) { Comun.mensajeVer("Debe haber Servidas", Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP); }
                }
                else
                {

                    if (vmsg)
                    {
                        resp = Comun.mensajeVer("¿ Desea Guardar Servida " + txtServida.Text + " ?", Constants.MSG_BOTON_SINO, Constants.MSG_IMG_QUESTION);
                    }

                    if (resp == Constants.MSG_BOTON_SI)
                    {

                        rutadestino = rutaarchivos() + nameruta + nameb + "\\S_" + numcamion() + "_" + DateTime.Now.ToString("ddMMyyHHmmss") + ".txt";
                        file = Path.GetFileName(rutadestino);
                        //if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        //{

                        this.Cursor = Cursors.WaitCursor;

                        try
                        {
                            //Open the File
                            //StreamWriter objWriter = new StreamWriter(saveFileDialog1.FileName);

                            Datos datos = new Datos();
                            DataTable dt = new DataTable();

                            datos.p_sql = "SELECT * FROM servidas WHERE ( hecho = 'S' OR hecho = 'X' ) AND num = " + txtServida.Text + " and id = " + txtJornada.Text + " order by fecha, hora"; //" and  ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " )";

                            if (!datos.consultaSQL(ref dt, true))
                            {
                                if (vmsg)
                                { MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                            }
                            else
                            {

                                if (dt.Rows.Count > 0)
                                {

                                    StreamWriter objWriter = new StreamWriter(rutadestino);

                                    foreach (DataRow row in dt.Rows)
                                    {
                                        objWriter.WriteLine(txtCamion.Text + "," + /*row["Hecho"].ToString() + "," + "C" + "," +*/ row["Num"].ToString() + "," + row["Corral"].ToString() + "," + row["Racion"].ToString() + "," + row["Planeado"].ToString() + "," + row["Realidad"].ToString() + "," + row["Hora"].ToString().Substring(0, 5) + "," + String.Format("{0:dd-MM-yy}", Convert.ToDateTime(row["Fecha"])));
                                    }

                                    objWriter.Close();


                                    datos.p_sql = "UPDATE raciones SET guardar = 'S' WHERE num = " + txtServida.Text + " and id = " + txtJornada.Text;

                                    if (!datos.consultaSQL(ref dt, true))
                                    {
                                        if (vmsg)
                                        { MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                                    }

                                    cantservidos();

                                    //MessageBox.Show("Listo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                    if (vmsg)
                                    { Comun.mensajeVer("Listo, Guardo Servida " + txtServida.Text, Constants.MSG_BOTON_OK, Constants.MSG_IMG_OK); }


                                }
                                else
                                {
                                    if (vmsg)
                                    { Comun.mensajeVer("No Guardo Servida " + txtServida.Text, Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP); }
                                }

                            }



                        }
                        catch (Exception)
                        {
                            if (vmsg)
                            { MessageBox.Show("El archivo no se puede guardar"); }
                        }

                        this.Cursor = Cursors.Default;

                        //}

                    }

                }

            }

        }

        private void btnArchivos_Click(object sender, EventArgs e)
        {
            String file = "";
            grabar(true, ref file);

        }

        private string rutaarchivos()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            string valor = "";

            datos.p_sql = "SELECT TOP 1 * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    valor = row["RutaLocal"].ToString();
                }
            }

            return valor;

        }

        private string rutared()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            string valor = "";

            datos.p_sql = "SELECT TOP 1 * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    valor = row["RutaRed"].ToString();
                }
            }

            return valor;

        }


        private string numcamion()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            string valor = "";

            datos.p_sql = "SELECT TOP 1 * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    valor = row["NumCamion"].ToString();
                }
            }

            return valor;

        }

        private string nomusuario()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            string valor = "";

            datos.p_sql = "SELECT TOP 1 * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    valor = row["Usuario"].ToString();
                }
            }

            return valor;

        }

        private string nomcontrasena()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            string valor = "";

            datos.p_sql = "SELECT TOP 1 * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    valor = row["Contrasena"].ToString();
                }
            }

            return valor;

        }

        private Boolean redConectado()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }

        private Boolean usbConectado(ref string unidad)
        {

            DriveInfo[] allDrives = DriveInfo.GetDrives();
            Boolean listo = false;

            foreach (DriveInfo d in allDrives)
            {

                if (d.IsReady == true && d.DriveType == DriveType.Removable)
                {
                    unidad = d.Name;
                    listo = true;
                }

            }

            return listo;

        }

        private void btnUSB_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();

            string vunidad = "";
            String file = "";
            String pmsgerr = "";
            String pmsge = "";
            //String smsgerr = "";
            String smsge = "";
            String textomsg = "";
            int vcantidad = 0;

            String merror = "";
            //String mok = "";

            if (!usbConectado(ref vunidad))
            {
                //MessageBox.Show("Sin red disponible.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                Comun.mensajeVer("Sin USB disponible.", Constants.MSG_BOTON_OK, Constants.MSG_IMG_WARNING);
            }
            else
            {
                //sincronizar(vrutaarchivos + nameruta, vunidad, Constants.FILE_USB, true, true);

                if (DGRaciones.RowCount == 0) //USB>>Tablet
                {
                    textomsg = "Recibio";
                    quitaArchivosde("P");

                    moverArchivos(vunidad, vrutaarchivos + nameruta + namea, ref vcantidad, false, false);
                    //leerWsdlPlan(ref dt, ref pmsgerr, ref pmsge);
                    //merror = pmsgerr;
                    //mok = pmsge;
                    if (vcantidad != 0)
                    {
                        lista(false);
                    }
                    else
                    {
                        merror = "Ningun Registro";
                    }
                }
                else  //USB<<Tablet
                {
                    textomsg = "Envio";

                    if (!hayServidos())
                    {
                        Comun.mensajeVer("Debe haber Servidas", Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
                        return;
                    }
                    else
                    {
                        quitaArchivosde("S");
                        grabar(false, ref file);

                        if (!file.Equals(""))
                        {
                            moverArchivo(vrutaarchivos + nameruta + nameb + "\\" + file, vunidad, false);
                            cantservidos();

                        }
                        else
                        {
                            //merror = "Error generar informacion";
                        }

                    }

                }

                if (merror.Equals(""))
                {
                    Comun.mensajeVer(textomsg + " Informacion Dispositivo USB", Constants.MSG_BOTON_OK, Constants.MSG_IMG_OK);
                }
                else
                {
                    Comun.mensajeVer(textomsg + " " + merror, Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
                }

            }

        }

        private void btnSincroniza_Click(object sender, EventArgs e)
        {
            //sincronizarWS();
            //return;
            String file = "";
            String textomsg = "";
            int vcantidad = 0;
            String merror = "";

            if (!redConectado())
            {
                //MessageBox.Show("Sin red disponible.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                Comun.mensajeVer("Sin red disponible.", Constants.MSG_BOTON_OK, Constants.MSG_IMG_WARNING);
            }
            else
            {
                //sincronizar(vrutaarchivos + nameruta, vrutared + nameruta, Constants.FILE_RED, true, true);


                if (DGRaciones.RowCount == 0) //Wifi>>Tablet
                {
                    textomsg = "Recibio";
                    quitaArchivosde("P");

                    moverArchivos(vrutared + nameruta + namea, vrutaarchivos + nameruta + namea, ref vcantidad, true, false);
                    //leerWsdlPlan(ref dt, ref pmsgerr, ref pmsge);
                    //merror = pmsgerr;
                    //mok = pmsge;
                    if (vcantidad != 0)
                    {
                        lista(false);
                    }
                    else
                    {
                        merror = "Ningun Registro";
                    }
                }
                else  //Wifi<<Tablet
                {
                    textomsg = "Envio";

                    if (!hayServidos())
                    {
                        Comun.mensajeVer("Debe haber Servidas", Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
                        return;
                    }
                    else
                    {
                        quitaArchivosde("S");
                        grabar(false, ref file);

                        if (!file.Equals(""))
                        {
                            moverArchivo(vrutaarchivos + nameruta + nameb + "\\" + file, vrutared + nameruta + nameb, false);
                            cantservidos();

                        }
                        else
                        {
                            //merror = "Error generar informacion";
                        }

                    }

                }

                if (merror.Equals(""))
                {
                    Comun.mensajeVer(textomsg + " Informacion Por Wifi", Constants.MSG_BOTON_OK, Constants.MSG_IMG_OK);
                }
                else
                {
                    Comun.mensajeVer(textomsg + " " + merror, Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
                }

            }

        }

        private void sincronizar(string rutalocal, string rutadisp, int opc, Boolean valcamion, Boolean valfechahoy)
        {

            string nameopc = "";
            //string namefilei = "";
            //string namefileo = "";

            try
            {

                this.Cursor = Cursors.WaitCursor;

                string vnameb = nameb;
                string vnamea = namea;

                quitadifdiaplan();

                switch (opc)
                {
                    case Constants.FILE_RED:
                        nameopc = "WIFI";
                        break;

                    case Constants.FILE_USB:
                        nameopc = "USB";
                        vnameb = "";
                        vnamea = "";
                        break;
                }

                // (servido) server/USB <<< tablet  

                string sourcePath = rutalocal + nameb; //origen
                string targetPath = rutadisp + vnameb; //destino
                string targetproc = sourcePath + namec; //proc

                foreach (String srcPath in Directory.GetFiles(sourcePath))
                {

                    string namefile = Path.GetFileName(srcPath);

                    if (namefile.Contains("S_"))
                    {

                        string[] lines = namefile.Split('_');
                        string line = lines[1].Trim();
                        string fecha = lines[2].Substring(0, 6);

                        //if (namefile.Substring(0, 1).Equals("S"))
                        //{

                        //if (fecha.Equals(DateTime.Now.ToString("ddMMyy")))
                        //{

                        //File.Copy(srcPath, srcPath.Replace(sourcePath, targetPath), true);
                        File.Move(srcPath, srcPath.Replace(sourcePath, targetPath));
                        //namefileo = namefile;
                        //}

                        //if (File.Exists(srcPath))
                        //{

                        //    string newname = srcPath.Replace("S_", "X_");
                        //    //File.Copy(srcPath, newname, true);
                        //    //File.Delete(srcPath);

                        //    if (File.Exists(newname))
                        //    {
                        //        File.Delete(newname);
                        //    }

                        //    //namefile = Path.GetFileName(newname);
                        //    //newname = targetproc + "\\" + namefile;

                        //    File.Move(srcPath, newname);

                        //}

                        //if (!fecha.Equals(DateTime.Now.ToString("ddMMyy")))
                        //{

                        //--procesados----------------------------------------------------
                        ///
                        //namefile = Path.GetFileName(srcPath);
                        //namefile = namefile.Replace(".txt", "_" + DateTime.Now.ToString("ddMMyyHHmmss") + ".txt")
                        //                   .Replace(".TXT", "_" + DateTime.Now.ToString("ddMMyyHHmmss") + ".TXT");

                        //string newname = targetproc + "\\" + namefile;

                        //File.Move(srcPath, newname);
                        //---------------------------------------------------------------

                        //}

                    }

                }




                // (planes) server/USB >>> tablet 

                sourcePath = rutadisp + vnamea; //origen
                targetPath = rutalocal + namea; //destino
                targetproc = targetPath + namec; //proc

                //Boolean encontrof = false;

                //if (lblPlanes.Text.Equals("0"))
                //{

                foreach (String srcPath in Directory.GetFiles(sourcePath))
                {

                    string namefile = Path.GetFileName(srcPath);

                    if (namefile.Contains("P_"))
                    {

                        string[] lines = namefile.Split('_');
                        string tipo = lines[0].Trim();
                        string line = lines[1].Trim();
                        string fecha = lines[2].Substring(0, 6);


                        if (((String.Equals(txtCamion.Text, line) && valcamion)) || !valcamion)
                        {
                            if (((String.Equals(DateTime.Now.ToString("ddMMyy"), fecha) && valfechahoy)) || !valfechahoy)
                            {
                                File.Move(srcPath, srcPath.Replace(sourcePath, targetPath + "\\"));
                            }

                        }

                        //if (valcamion)
                        //{
                        //    if (String.Equals(txtCamion.Text, line))
                        //    {
                        //        File.Move(srcPath, srcPath.Replace(sourcePath, targetPath + "\\"));
                        //    }
                        //}
                        //else
                        //{
                        //    File.Move(srcPath, srcPath.Replace(sourcePath, targetPath + "\\"));
                        //}

                        //if (String.Equals(txtCamion.Text, line))
                        //{

                        //if (fecha.Equals(DateTime.Now.ToString("ddMMyy")))
                        //{
                        //namefile = namefile.Replace("P_", "X_");

                        //if (!File.Exists(targetPath + "\\" + namefile))
                        //{
                        //File.Copy(srcPath, srcPath.Replace(sourcePath, targetPath + "\\"), true);                                    
                        //File.Move(srcPath, srcPath.Replace(sourcePath, targetPath + "\\"));                                    
                        //}

                        //}

                        //}
                        //namefilei = namefile;
                        //encontrof = true;
                        //break;

                    }

                }

                //if (!encontrof)
                //{

                //    foreach (String srcPath in Directory.GetFiles(sourcePath))
                //    {

                //        string namefile = Path.GetFileName(srcPath);

                //        if (namefile.Contains("DS_UNDON"))
                //        {
                //            //File.Copy(srcPath, srcPath.Replace(sourcePath, targetPath + "\\"), true);
                //            File.Move(srcPath, srcPath.Replace(sourcePath, targetPath + "\\"));
                //            namefilei = namefile;
                //            break;
                //        }

                //    }

                //}

                //}

                this.Cursor = Cursors.Default;

                cantplanes();
                cantservidos();

                //Console.Beep(1000, 100);

                //MessageBox.Show("Listo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                Comun.mensajeVer("Listo, Envio/Recibio Archivos por " + nameopc /*+ " " + namefileo + " " + namefilei*/, Constants.MSG_BOTON_OK, Constants.MSG_IMG_OK);

            }
            catch (Exception err)
            {

                //MessageBox.Show("Error al sincronizar, " + err.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                Comun.mensajeVer("Error al sincronizar, " + err.Message, Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
                this.Cursor = Cursors.Default;
            }

        }

        private void cantplanes()
        {

            int cantp = 0;
            string sourcePath = rutaarchivos() + nameruta + namea;

            foreach (String srcPath in Directory.GetFiles(sourcePath))
            {

                string namefile = Path.GetFileName(srcPath);

                string[] lines = namefile.Split('_');
                string tipo = lines[0].Trim();
                string line = lines[1].Trim();
                string fecha = lines[2].Substring(0, 6);

                if ((tipo.Equals("P")))// && line.Equals(txtCamion.Text) && fecha.Equals(DateTime.Now.ToString("ddMMyy"))))
                //if (tipo.Equals("P") )//|| namefile.Contains("DS_UNDON"))
                {
                    cantp = cantp + 1;
                }

            }

            lblPlanes.Text = cantp.ToString();

        }

        private void cantservidos()
        {

            int cantp = 0;
            string sourcePath = rutaarchivos() + nameruta + nameb;

            foreach (String srcPath in Directory.GetFiles(sourcePath))
            {

                string namefile = Path.GetFileName(srcPath);

                if (namefile.Substring(0, 1).Equals("S"))
                {

                    cantp = cantp + 1;
                }

            }

            lblServidos.Text = cantp.ToString();


        }

        private void btnExplorer_Click(object sender, EventArgs e)
        {

            Process.Start("explorer.exe", rutaarchivos() + nameruta);

        }

        private void carpeta_existe_local()
        {

            try
            {

                if (!System.IO.Directory.Exists(rutaarchivos() + nameruta))
                {
                    System.IO.Directory.CreateDirectory(rutaarchivos() + nameruta);
                }

                if (!System.IO.Directory.Exists(rutaarchivos() + nameruta + namea))
                {
                    System.IO.Directory.CreateDirectory(rutaarchivos() + nameruta + namea);
                }

                if (!System.IO.Directory.Exists(rutaarchivos() + nameruta + namea + namec))
                {
                    System.IO.Directory.CreateDirectory(rutaarchivos() + nameruta + namea + namec);
                }

                if (!System.IO.Directory.Exists(rutaarchivos() + nameruta + nameb))
                {
                    System.IO.Directory.CreateDirectory(rutaarchivos() + nameruta + nameb);
                }

                if (!System.IO.Directory.Exists(rutaarchivos() + nameruta + nameb + namec))
                {
                    System.IO.Directory.CreateDirectory(rutaarchivos() + nameruta + nameb + namec);
                }

            }
            catch (Exception e)
            {

            }
            //if (!System.IO.Directory.Exists(rutaarchivosred() + nameruta))
            //{
            //    System.IO.Directory.CreateDirectory(rutaarchivosred() + nameruta);
            //}

            //if (!System.IO.Directory.Exists(rutaarchivosred() + nameruta + namea))
            //{
            //    System.IO.Directory.CreateDirectory(rutaarchivosred() + nameruta + namea);
            //}

            ////if (!System.IO.Directory.Exists(rutaarchivosred() + nameruta + namea + namec))
            ////{
            ////    System.IO.Directory.CreateDirectory(rutaarchivosred() + nameruta + namea + namec);
            ////}

            //if (!System.IO.Directory.Exists(rutaarchivosred() + nameruta + nameb))
            //{
            //    System.IO.Directory.CreateDirectory(rutaarchivosred() + nameruta + nameb);
            //}

            ////if (!System.IO.Directory.Exists(rutaarchivosred() + nameruta + nameb + namec))
            ////{
            ////    System.IO.Directory.CreateDirectory(rutaarchivosred() + nameruta + nameb + namec);
            ////}

        }

        private void btnplan_Click(object sender, EventArgs e)
        {

        }

        private void btnPlanta_Click(object sender, EventArgs e)
        {
            Senga.Captura frm = new Senga.Captura(Convert.ToInt32(txtJornada.Text), 4);
            frm.Show();
        }

        private void sincronizarWS()
        {

            MessageBox.Show("Listo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void elejirServida(string valor)
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            if (valor.Length > 0)
            {
                datos.p_sql = "UPDATE operacion SET servida = '" + valor + "'";

                if (!datos.consultaSQL(ref dt, false))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void btnNube_Click(object sender, EventArgs e)
        {

            nubeTransferencia();
        
        }

        private void btnWsRecibir_Click(object sender, EventArgs e)
        {
            nubeTransferenciaRecibe();
        }

        private void btnWsEnviar_Click(object sender, EventArgs e)
        {
            nubeTransferenciaEnvia();
        }

        private void nubeTransferencia()
        {



            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();

            String file = "";
            String pmsgerr = "";
            String pmsge = "";
            String smsgerr = "";
            String smsge = "";
            String textomsg = "";

            String merror = "";
            String mok = "";

            if (!redConectado())
            {
                Comun.mensajeVer("Sin red disponible.", Constants.MSG_BOTON_OK, Constants.MSG_IMG_WARNING);
            }
            else
            {
                //Comun.mensajeVer("No disponible.", Constants.MSG_BOTON_OK, Constants.MSG_IMG_WARNING);

                //DGRaciones.Visible = false;
                //DGCargas.Visible = false;
                //this.Refresh();

                if (DGRaciones.RowCount == 0) //SAP>>Tablet
                {
                    textomsg = "Recibio";

                    quitaArchivosde("P");
                    leerWsdlPlan(ref dt, ref pmsgerr, ref pmsge);
                    merror = pmsgerr;
                    mok = pmsge;

                    lista(false);
                }
                else  //SAP<<Tablet
                {
                    textomsg = "Envio";

                    if (!hayServidos())
                    //if (txtServido.Text.Equals("0"))
                    {
                        Comun.mensajeVer("Debe haber Servidas", Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
                        return;
                    }
                    else
                    {
                        quitaArchivosde("S");
                        grabar(false, ref file);

                        if (!file.Equals(""))
                        {
                            leerArchivoServ(ref file, ref dt2, ref smsgerr, ref smsge);
                            //if (smsge.Equals("REP")) { smsge = "Envio repetido"; }

                            merror = smsgerr;
                            mok = smsge;
                        }
                        else
                        {
                            merror = "Falla Obtener Informacion";
                        }

                    }

                }

                //DGRaciones.Visible = true;
                //DGCargas.Visible = true;
                //this.Refresh();

                if (mok == "OK")
                {
                    Comun.mensajeVer("Correcto " + textomsg + " Informacion por Ws" + "\n\r", Constants.MSG_BOTON_OK, Constants.MSG_IMG_OK);
                }
                else
                {
                    Comun.mensajeVer("Error " + textomsg + " Informacion por Ws" + "\n\r" + merror, Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
                }

                //if (merror.Equals(""))
                //{
                //    if (mok == "OK")
                //    {
                //        Comun.mensajeVer(textomsg + " Informacion por Ws\n\r" + mok, Constants.MSG_BOTON_OK, Constants.MSG_IMG_OK);
                //    }
                //    else
                //    {
                //        Comun.mensajeVer("Error " + textomsg + " Informacion por Ws\n\r" + mok, Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
                //    }
                //}
                //else
                //{ 
                //    Comun.mensajeVer(textomsg + " " + merror, Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);         
                //}               

            }

        }

        private void nubeTransferenciaRecibe()
        {

            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();

            String file = "";
            String pmsgerr = "";
            String pmsge = "";
            String smsgerr = "";
            String smsge = "";
            String textomsg = "";

            String merror = "";
            String mok = "";

            if (!redConectado())
            {
                Comun.mensajeVer("Sin red disponible.", Constants.MSG_BOTON_OK, Constants.MSG_IMG_WARNING);
            }
            else
            {

                //DGRaciones.Visible = false;
                //DGCargas.Visible = false;
                //this.Refresh();

                //Comun.mensajeVer("No disponible.", Constants.MSG_BOTON_OK, Constants.MSG_IMG_WARNING);

                //if (DGRaciones.RowCount != 0) //SAP>>Tablet
                //{
                textomsg = "Recibio";

                    quitaArchivosde("P");
                    leerWsdlPlan(ref dt, ref pmsgerr, ref pmsge);
                    merror = pmsgerr;
                    mok = pmsge;

                    lista(false);
                //}
                //else 
                //{

                //    Comun.mensajeVer("Debe haber Registros", Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
                //    return;

                //}
                //else  //SAP<<Tablet
                //{
                //textomsg = "Envio";

                //if (!hayServidos())
                //{
                //    Comun.mensajeVer("Debe haber Servidas", Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
                //    return;
                //}
                //else
                //{
                //quitaArchivosde("S");
                //grabar(false, ref file);

                //if (!file.Equals(""))
                //{
                //    leerArchivoServ(ref file, ref dt2, ref smsgerr, ref smsge);
                //    merror = smsgerr;
                //    mok = smsge;
                //}
                //else
                //{
                //    merror = "Falla Obtener Informacion";
                //}

                //}

                //}

                //DGRaciones.Visible = true;
                //DGCargas.Visible = true;
                //this.Refresh();

                if (mok == "OK")
                {
                    Comun.mensajeVer("Correcto " + textomsg + " Informacion por Ws" + "\n\r", Constants.MSG_BOTON_OK, Constants.MSG_IMG_OK);
                }
                else
                {
                    Comun.mensajeVer("Error " + textomsg + " Informacion por Ws" + "\n\r" + merror, Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
                }

            }

        }

        private void nubeTransferenciaEnvia()
        {

            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();

            String file = "";
            String pmsgerr = "";
            String pmsge = "";
            String smsgerr = "";
            String smsge = "";
            String textomsg = "";

            String merror = "";
            String mok = "";

            if (!redConectado())
            {
                Comun.mensajeVer("Sin red disponible.", Constants.MSG_BOTON_OK, Constants.MSG_IMG_WARNING);
            }
            else
            {
                //Comun.mensajeVer("No disponible.", Constants.MSG_BOTON_OK, Constants.MSG_IMG_WARNING);

                //if (DGRaciones.RowCount == 0) //SAP>>Tablet
                //{
                //textomsg = "Recibio";

                //quitaArchivosde("P");
                //leerWsdlPlan(ref dt, ref pmsgerr, ref pmsge);
                //merror = pmsgerr;
                //mok = pmsge;

                //lista(false);
                //}
                //else  //SAP<<Tablet
                //{
                textomsg = "Envio";

                if (!hayServidos())
                {
                    Comun.mensajeVer("Debe haber Servidas", Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
                    return;
                }
                else
                {
                    quitaArchivosde("S");
                    grabar(false, ref file);

                    if (!file.Equals(""))
                    {
                        leerArchivoServ(ref file, ref dt2, ref smsgerr, ref smsge);
                        merror = smsgerr;
                        mok = smsge;
                    }
                    else
                    {
                        merror = "Falla Obtener Informacion";
                    }

                }

                //}

                if (mok == "OK")
                {
                    Comun.mensajeVer("Correcto " + textomsg + " Informacion por Ws" + "\n\r", Constants.MSG_BOTON_OK, Constants.MSG_IMG_OK);
                }
                else
                {
                    Comun.mensajeVer("Error " + textomsg + " Informacion por Ws" + "\n\r" + merror, Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
                }

            }

        }

        private Boolean hayServidos()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();
            Boolean resp = false;

            datos.p_sql = "SELECT SUM(Servido) AS ServidoT FROM raciones WHERE num = " + txtServida.Text + " and id = " + vjornada;

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {

                foreach (DataRow row in dt.Rows)
                {

                    if (!row["ServidoT"].ToString().Equals("0"))
                    {
                        resp = true;
                    }
                    else
                    {
                        resp = false;
                    }


                }

            }

            return resp;

        }

        private void leerWsdlPlan(ref DataTable dt, ref String msgerr, ref String msge)
        {
            ArrayList lista = new ArrayList();

            //Cursor = Cursors.WaitCursor;

            //String seleccion = "";

            //switch (txtServida.Text)
            //{
            //    case "1":
            //        seleccion = "PLAN01";
            //        break;
            //    case "2":
            //        seleccion = "PLAN02";
            //        break;
            //    case "3":
            //        seleccion = "PLAN03";
            //        break;
            //}

            String seleccion = "PLAN" + txtServida.Text.PadLeft(2, '0');


            Thread t = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                cargarWSDLSel(seleccion);
            });
            t.Start();

            //while (t.IsAlive)
            //{
            //    Thread.Sleep(50);
            //    Console.WriteLine("Estado Hilo: " + t.ThreadState + " " + t.IsAlive+" "+t.Name);
            //    //Application.DoEvents();
            //}           

            t.Join();

            lista = connectar.getList();
            String errormsg = connectar.getError();
            String emsg = connectar.getEmsg();


            DataTable dttmp = new DataTable();
            dttmp.Columns.Add("Camion");
            dttmp.Columns.Add("Numservida");
            dttmp.Columns.Add("Corral");
            dttmp.Columns.Add("Racion");
            dttmp.Columns.Add("Cabezas");
            dttmp.Columns.Add("Planeado");
            dttmp.Columns.Add("Hora");
            dttmp.Columns.Add("Plandia");


            foreach (String listar in lista)
            {

                string[] lines = listar.Split('|');

                DataRow newRow = dttmp.NewRow();
                newRow["Camion"] = lines[0].Trim();
                newRow["Numservida"] = lines[1].Trim();
                newRow["Corral"] = lines[2].Trim();
                newRow["Racion"] = lines[3].Trim();
                newRow["Cabezas"] = lines[4].Trim();
                newRow["Planeado"] = lines[5].Trim();
                newRow["Hora"] = lines[6].Trim();
                newRow["Plandia"] = lines[7].Trim();


                dttmp.Rows.Add(newRow);

            }

            dt = dttmp;

            //Cursor = Cursors.Default;
            msgerr = errormsg;
            msge = emsg;

            //if (!errormsg.Equals(""))
            //{
            //MessageBox.Show(errormsg, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //}
            //if (!errormsg.Equals("")) { lblErrores.Text = errormsg; }

            if (emsg.Equals("OK"))
            {
                crearArchivoPlan(dt);
            }


            if (dt.Rows.Count == 0)
            {
                msgerr = "Ningun Registro";
            }


        }

        private void enviarWsdlServido(DataTable dt, ref String msgerr, ref String msge)
        {

            if (dt.Rows.Count != 0)
            {

                try
                {

                    Datos datos = new Datos();
                    ArrayList lista = new ArrayList();

                    //Cursor = Cursors.WaitCursor;

                    String seleccion = "SERVIDO";

                    lista.Clear();

                    foreach (DataRow row in dt.Rows)
                    {

                        lista.Add(
                            row["Numcamion"].ToString() + "|" +
                            row["Jornada"].ToString() + "|" +
                            row["Fecha"].ToString().Substring(0, 2) + "." + row["Fecha"].ToString().Substring(3, 2) + ".20" + row["Fecha"].ToString().Substring(6, 2) + "|" +
                            row["Hora"].ToString() + ":00" + "|" +
                            row["Corral"].ToString() + "|" +
                            row["Servida"].ToString() + "|" +
                            row["Racion"].ToString() + "|" +
                            row["planeado"].ToString() + "|" +
                            row["Realidad"].ToString()

                        );

                    }

                    //lista.Add(txtTransferir.Text+" 2");

                    Thread t = new Thread(() =>
                    {
                        Thread.CurrentThread.IsBackground = true;
                        transferirWSDLSel(seleccion, lista);
                    });
                    t.Start();

                    //while (t.IsAlive)
                    //{
                    //    Thread.Sleep(50);
                    //    //Application.DoEvents();
                    //}
                    t.Join();
                    ////t.Abort();

                    String errormsg = connectar.getError();
                    String emsg = connectar.getEmsg();
                    //String url = connectar.getUrl();

                    msgerr = errormsg;
                    msge = emsg;

                    //string[] lurl = url.Split('/');
                    //string[] serverl = lurl[2].Trim().Split('.');
                    //lblServer.Text = serverl[0].Trim();

                    //lblMandante.Text = lurl[9].Trim();

                    ////Cursor = Cursors.Default;

                    //if (!errormsg.Equals(""))
                    //{
                    //    //MessageBox.Show(errormsg, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    txtError.Text = "Localiza: " + errormsg;
                    //}

                    //if (!emsg.Equals(""))
                    //{
                    //    //MessageBox.Show(emsg, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);                              

                    //if (emsg.Equals("OK"))
                    //{

                    //        ////msgErrores("Envio Ws", Constants.MSG_OK);

                    //        //List<string> dbOperations = new List<string>();

                    //        //foreach (DataRow row in dt.Rows)
                    //        //{
                    //        //    dbOperations.Add("UPDATE operacion SET envio = 'S'");
                    //        //}

                    //        //datos.p_sql_list = dbOperations;

                    //        //if (!datos.consultaSQLs(ref dt))
                    //        //{
                    //        //    //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //        //    //msgErrores(datos.p_error, Constants.MSG_ERROR);
                    //        //}
                    //        //else
                    //        //{

                    //        //}


                    //    }

                    //}

                }
                catch (Exception e)
                {
                    //txtError.Text = "Localiza: " + e.Message;
                    //ErrorLogging(e);
                }

            }

        }

        private void cargarWSDLSel(String catalogo)
        {

            connectar = new ConnectedWs(Constants.OPERACION_CATALOGO, catalogo, usuario, contrasena, numcamion());
            connectar.getDataWs();

        }

        private void transferirWSDLSel(String catalogo, ArrayList lectura)
        {
            try
            {
                connectar = new ConnectedWs(Constants.OPERACION_TRANSFERIR, catalogo, lectura, usuario, contrasena, numcamion());
                connectar.setDataWs();
            }
            catch (Exception e)
            {
                //txtError.Text = e.Message;
                //ErrorLogging(e);
            }
        }

        private void usuarioiWs()
        {
            usuario = nomusuario();
            contrasena = nomcontrasena();
        }

        private void crearArchivoPlan(DataTable dt)
        {

            string rutadestino = rutaarchivos() + nameruta + namea + "\\P_" + numcamion() + "_" + DateTime.Now.ToString("ddMMyyHHmmss") + ".txt";

            if (dt.Rows.Count > 0)
            {
                StreamWriter objWriter = new StreamWriter(rutadestino);

                foreach (DataRow row in dt.Rows)
                {
                    objWriter.WriteLine(row["Camion"].ToString() + "," + row["Numservida"].ToString() + "," + row["Corral"].ToString() + "," + row["Racion"].ToString() + "," + int.Parse(row["Cabezas"].ToString()).ToString() + "," + int.Parse(row["Planeado"].ToString()).ToString() + ",00:00," + int.Parse(row["Plandia"].ToString()).ToString());
                }

                objWriter.Close();

            }

        }

        private void leerArchivoServ(ref String file, ref DataTable dt, ref String msgerr, ref String msge)
        {

            string sourcePath = rutaarchivos() + nameruta + nameb;
            string targetproc = rutaarchivos() + nameruta + nameb + namec;

            //string linea = "";

            DataTable dttmp = new DataTable();
            dttmp.Columns.Add("Jornada");
            dttmp.Columns.Add("Numcamion");
            dttmp.Columns.Add("Servida");
            dttmp.Columns.Add("Corral");
            dttmp.Columns.Add("Racion");
            dttmp.Columns.Add("Planeado");
            dttmp.Columns.Add("Realidad");
            dttmp.Columns.Add("Hora");
            dttmp.Columns.Add("Fecha");


            //foreach (String srcPath in Directory.GetFiles(sourcePath))
            //{

            String srcPath = sourcePath + "\\" + file;

            string namefile = Path.GetFileName(srcPath);

            if (namefile.Substring(0, 2).Equals("S_"))
            {

                StreamReader objReader;

                try
                {

                    objReader = new StreamReader(srcPath);
                    string sLine = "";

                    while (sLine != null)
                    {

                        sLine = objReader.ReadLine();

                        if (sLine != null)
                        {

                            string[] lines = sLine.Split(',');

                            DataRow newRow = dttmp.NewRow();
                            newRow["Jornada"] = vjornada.Trim();
                            newRow["Numcamion"] = lines[0].Trim();
                            newRow["Servida"] = lines[1].Trim();
                            newRow["Corral"] = lines[2].Trim();
                            newRow["Racion"] = lines[3].Trim();
                            newRow["Planeado"] = lines[4].Trim();
                            newRow["Realidad"] = lines[5].Trim();
                            newRow["Hora"] = lines[6].Replace("-", "").Trim();
                            newRow["Fecha"] = lines[7].Replace(":", "").Trim();

                            dttmp.Rows.Add(newRow);

                        }

                    }

                    dt = dttmp;

                    objReader.Close();

                    enviarWsdlServido(dt, ref msgerr, ref msge);

                    //if (msge.Equals("OK") || msge.Equals("REP"))
                    //{

                    //string namefile2 = Path.GetFileName(srcPath);
                    //namefile2 = namefile2.Replace(".txt", "_" + DateTime.Now.ToString("ddMMyyHHmmss") + ".txt")
                    //                   .Replace(".TXT", "_" + DateTime.Now.ToString("ddMMyyHHmmss") + ".TXT");

                    //string newname2 = targetproc + "\\" + namefile2;

                    //try
                    //{
                    //    File.Move(srcPath, newname2);
                    //}
                    //catch (Exception)
                    //{

                    //}

                    moverArchivo(srcPath, targetproc, true);

                    //}

                    return;

                }
                catch (Exception msgerror)
                {
                    //MessageBox.Show("El archivo no se puede leer");
                    //Comun.mensajeVer("El archivo no se puede leer " + msgerror + " " + linea, Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
                    //this.Cursor = Cursors.Default;
                    //lblCantidad.Visible = false;
                    return;
                }


            }


            //}


        }


        private void moverArchivo(String origen, String destino, Boolean fechahora)
        {

            string namefile = Path.GetFileName(origen);
            string newname = "";

            if (fechahora)
            {
                namefile = namefile.Replace(".txt", "_" + DateTime.Now.ToString("ddMMyyHHmmss") + ".txt")
                                    .Replace(".TXT", "_" + DateTime.Now.ToString("ddMMyyHHmmss") + ".TXT");
            }
            newname = destino + "\\" + namefile;

            try
            {
                File.Move(origen, newname);
            }
            catch (Exception em)
            {

            }

        }

        private void moverArchivos(String origen, String destino, ref int cantidad, Boolean valcamion, Boolean valfechahoy)
        {

            int vcantidad = 0;

            foreach (String srcPath in Directory.GetFiles(origen))
            {

                string namefile = Path.GetFileName(srcPath);

                if (namefile.Contains("P_"))
                {

                    string[] lines = namefile.Split('_');
                    string tipo = lines[0].Trim();
                    string line = lines[1].Trim();
                    string fecha = lines[2].Substring(0, 6);


                    if (((String.Equals(txtCamion.Text, line) && valcamion)) || !valcamion)
                    {
                        if (((String.Equals(DateTime.Now.ToString("ddMMyy"), fecha) && valfechahoy)) || !valfechahoy)
                        {
                            //File.Move(srcPath, srcPath.Replace(sourcePath, targetPath + "\\"));
                            moverArchivo(srcPath, destino, false);
                            vcantidad = vcantidad + 1;
                        }

                    }


                }


            }

            cantidad = vcantidad;

        }

        private void timerReloj_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss tt");

            lblWsActivoF.Value = Principal.instance.lblWsActivoF.Value;
            lblConectadoF.Value = Principal.instance.lblConectadoF.Value;
            lblGpsActivoF.Value = Principal.instance.lblGpsActivoF.Value;
            lblBateriaActivoF.Value = Principal.instance.lblBateriaActivoF.Value;
            lblBateria.Text = Principal.instance.lblBateria.Text;
            pbBateria.Value = Principal.instance.pbBateria.Value;
            lblBateriaTiempo.Text = Principal.instance.lblBateriaTiempo.Text;

            btnConectar.Visible = Principal.instance.btnConectarGps.Visible;
            //if (wsactivo && lblConectadoF.Value == 1)
            //{
            //    focoWs();
            //}
            //else
            //{
            //    lblWsActivoF.Value = 0;
            //}

            //if (!lblLatitud.Text.Equals("0"))
            //{
            //    focoGps();
            //}
            //else
            //{
            //    lblGpsActivoF.Value = 0;
            //}

        }


        //private void focoWs()
        //{

        //    if (lblWsActivoF.Value == 1)
        //    {
        //        lblWsActivoF.Value = 0;
        //    }
        //    else
        //    {
        //        lblWsActivoF.Value = 1;
        //    }

        //}

        //private void focoGps()
        //{

        //    if (lblGpsActivoF.Value == 1)
        //    {
        //        lblGpsActivoF.Value = 0;
        //    }
        //    else
        //    {
        //        lblGpsActivoF.Value = 1;
        //    }

        //}

        private void leerGPSPrincipal()
        {

            double lat = Principal.instance.gLatitudgps;
            double lng = Principal.instance.gLongitudgps;

            lblLatitud.Text = lat.ToString();
            lblLongitud.Text = lng.ToString();

            //lblCorral.Text = corralUbicacionm(dtUbictmp, lat, lng, gmindist);

            //gLatitud = lat;
            //gLongitud = lng;

            Console.WriteLine("LEE GPS Principal" + " " + DateTime.Now.ToString());

        }

        private void timerGPS_Tick(object sender, EventArgs e)
        {
            leerGPSPrincipal();
        }

        //private void redEstado()
        //{

        //    if (redConectado())
        //    {
        //        //lblConectado.Text = "ON";
        //        lblConectadoF.Value = 1;
        //        //enviarWsdl();
        //    }
        //    else
        //    {
        //        //lblConectado.Text = "OFF";
        //        lblConectadoF.Value = 0;
        //    }

        //}

        private string cadenaCero(string valor)
        {

            if (valor.Equals(""))
            {
                valor = "0";
            }

            return valor;

        }

        private void tiempoServido()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            TimeSpan ts;
            string duracion = "00:00:00";

            datos.p_sql = "SELECT MIN(Hora) AS MinHora, MAX(Hora) AS MaxHora FROM servidas WHERE hora <> '00:00:00' AND num = " + txtServida.Text + " and id = " + txtJornada.Text;

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {

                foreach (DataRow row in dt.Rows)
                {
                    if (!row["MaxHora"].ToString().Equals("") && !row["MinHora"].ToString().Equals(""))
                    {
                        ts = DateTime.Parse(row["MaxHora"].ToString()) - DateTime.Parse(row["MinHora"].ToString());
                        duracion = ts.ToString();
                    }


                }

            }

            txtTiempo.Text = duracion.ToString();

        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            Principal.instance.btnConectarGps.PerformClick();
        }

        private void btnJornadas_Click(object sender, EventArgs e)
        {


            //Datos datos = new Datos();
            //DataTable dt = new DataTable();

            //datos.p_sql = "UPDATE operacion SET racion = '" + txtRacion.Text + "'";

            //if (!datos.consultaSQL(ref dt, true))
            //{
            //    //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //}

            Jornadas frm = new Jornadas(txtJornada.Text);
            frm.Show();

            Close();

        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            Reportes frm = new Reportes(txtJornada.Text);
            frm.Show();
        }
    }
    
}