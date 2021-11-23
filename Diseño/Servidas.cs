using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Media;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

using System.Linq;
using System.Drawing.Drawing2D;
using System.Windows.Forms.DataVisualization.Charting;


namespace Senga
{
    public partial class Servidas : Form
    {
        bool activado = false;
        string vracion;
        string vnumcarga;
        //int vfilaAct = 0;
        DataTable dtUbictmp;
        DataTable dtUbictmpgrid;

        Boolean modomapa = true;
        string instancia = "2";//1 server y cache   2-cache
        double gLatitud = 21.923002;
        double gLongitud = -98.745503;
        double gmindist = 0.02;
        string gCoordenadas = "";
        private List<PointLatLng> _points;

        GMarkerGoogle markerRecorrer;
        GMapOverlay overlayRecorrer;

        GMarkerGoogle markerCamion;
        GMapOverlay overlayCamion;

        GMapOverlay overlayLinea;

        GMapOverlay overlayRuta;
        GMapOverlay overlayRutaP;

        GMapPolygon polygonCelda;
        GMapOverlay overlayCeldas;
        GMarkerGoogle markerlabelCelda;
        GMapOverlay overlaylabelCeldas;

        //public void Start()
        //{
        //    timerGPS.Start();
        //}

        //public void Stop()
        //{
        //    timerGPS.Stop();
        //}

        public static Servidas instance = null;

        public Servidas(string racion, string numcarga)
        {
            InitializeComponent();
            instance = this;
            vracion = racion;
            vnumcarga = numcarga;
            _points = new List<PointLatLng>();
        }
        
        private void Servidas_Load(object sender, EventArgs e)
        {


            propiedadesGrid();
            seleccionarFila2();

            posicionbotones();

            DGServidas.Height = 590;
            txtCaptura.Text = "";

            activado = true;
            formatoGrid();
            
            tablaUbicTemp();
            tablaUbicTempg();

            cargaMapa();

            activar();




            panelServida.Visible = true;
            DGServidas.Visible = true;
            //pbServido.Visible = true;
            btnServir.Visible = true;

            txtCorrales.Visible = true;
            panelMapa.Visible = true;

            //txtLatitud.Visible = true;
            //txtLongitud.Visible = true;
            //txtCorralUbic.Visible = true;

            colorFila();

            panelSimular.Visible = Principal.instance.panelGps.Visible;

            gMapControl1.Refresh();

        }

        private void Servidas_Activated(object sender, EventArgs e) 
        {

            activar();
            
        }

        private void posicionbotones()
        {
            btnCerrar.Top = 0;
            btnHecho.Top = 0;
            btnQuitar.Top = 0;
            btnBuscar.Top = 0;
            btnCargar.Top = 0;
            //btnAgregar.Top = 0;



            btnCerrar.Left = this.Width - btnCerrar.Width;
            btnHecho.Left = btnCerrar.Left - btnHecho.Width;
            btnQuitar.Left = btnHecho.Left - btnQuitar.Width;
            btnBuscar.Left = btnQuitar.Left - btnBuscar.Width;
            btnCargar.Left = btnBuscar.Left - btnCargar.Width;
            //btnAgregar.Left = btnQuitar.Left - btnAgregar.Width;




            //DGJornadas.Left = 0;
            //DGJornadas.Width = this.Width;

            picGpsDesconectado.Top = gMapControl1.Height - picGpsDesconectado.Height - toolStrip3.Height;
            picGpsDesconectado.Left = gMapControl1.Width - picGpsDesconectado.Width;




        }

        private void activar() 
        {

            if (activado)
            { 
            
                operacionCorral();
                
                limpiarCorral();
                refrescaGrid();
                //grafico(chartServidas);

                if (DGServidas.RowCount == 0)
                {
                    limpiarCorral();
                }
                else 
                { 
                    //txtIdD.Text = (string)DGServidas.CurrentRow.Cells["IdD"].Value;
                    //filaDatos(txtIdD.Text);   
     
                }
                
                //seleccionarFila();
                seleccionarFila2();
                barra();

                txtCorrales.Text = DGServidas.RowCount.ToString();
                
            }
        
        }

        private void fomatoCampos() 
        {


            if (txtCorrales.Text == "") 
            {
                txtCorrales.Text = "0";
            }
            if (txtPlan.Text == "")
            {
                txtPlan.Text = "0";
            }
            //if (txtFisicoV.Text == "")
            //{
            //    txtFisicoV.Text = "0";
            //}

            txtCorrales.Text = string.Format("{0:#,##0}", long.Parse(txtCorrales.Text));
            txtPlan.Text = string.Format("{0:#,##0}", long.Parse(txtPlan.Text));
            txtCargadoV.Text = string.Format("{0:#,##0}", long.Parse(cadenaCero(txtCargado.Text)));
            txtServido.Text = string.Format("{0:#,##0}", long.Parse(txtServido.Text));
            txtFisicoV.Text = string.Format("{0:#,##0}", long.Parse(txtFisico.Text));
            txtFaltaV.Text = string.Format("{0:#,##0}", long.Parse(txtFalta.Text));
            txtBasculaV.Text = string.Format("{0:#,##0}", long.Parse(txtBascula.Text));
            txtServidoExtraV.Text = string.Format("{0:#,##0}", long.Parse(txtServidoExtra.Text));
        
        }

        private void cargaMapa()
        {

            GMaps.Instance.Mode = modoAcceso(instancia);
            gMapControl1.CacheLocation = @"mapa";

            gMapControl1.Position = new PointLatLng(gLatitud, gLongitud);
            //gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.MapProvider = GMapProviders.GoogleChinaSatelliteMap;

            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;

            gMapControl1.MinZoom = 15;//15
            gMapControl1.MaxZoom = 19;//17
            gMapControl1.Zoom = 15;
            gMapControl1.Bearing = -50;
            gMapControl1.AutoScroll = true;

            //trackBar1.Value = Convert.ToInt32(gMapControl1.Zoom);
            //lblCoordenadas.Text = "lat= " + Convert.ToString(LatInicial) + "   long= " + Convert.ToString(LngInicial);

            overlayCeldas = new GMapOverlay("celdas");
            gMapControl1.Overlays.Add(overlayCeldas);

            overlaylabelCeldas = new GMapOverlay("labelceldas");
            gMapControl1.Overlays.Add(overlaylabelCeldas);

            overlayRuta = new GMapOverlay("Ruta");
            gMapControl1.Overlays.Add(overlayRuta);

            overlayRutaP = new GMapOverlay("RutaP");
            gMapControl1.Overlays.Add(overlayRutaP);

            overlayLinea = new GMapOverlay("Ruta");
            gMapControl1.Overlays.Add(overlayLinea);

            overlayCamion = new GMapOverlay("camion");
            gMapControl1.Overlays.Add(overlayCamion);

            overlayRecorrer = new GMapOverlay("recorrer");
            gMapControl1.Overlays.Add(overlayRecorrer);


            //ubicServir(dtUbictmp);


            //gMapControl1.Refresh();


        }

        private AccessMode modoAcceso(string instancia)
        {

            switch (instancia)
            {
                case "0":
                    //lblInstancia.Text = "Internet";
                    return AccessMode.ServerOnly;
                case "1":
                    //lblInstancia.Text = "Internet y Local";
                    return AccessMode.ServerAndCache;
                case "2":
                    //lblInstancia.Text = "Local";
                    return AccessMode.CacheOnly;
                default:
                    return AccessMode.ServerOnly;
            }

        }

        private void tablaUbicTemp()
        {

            dtUbictmp = new DataTable();

            dtUbictmp.Columns.Add("corral");
            dtUbictmp.Columns.Add("lat");
            dtUbictmp.Columns.Add("lng");
            dtUbictmp.Columns.Add("lat1");
            dtUbictmp.Columns.Add("lng1");
            dtUbictmp.Columns.Add("lat2");
            dtUbictmp.Columns.Add("lng2");
            dtUbictmp.Columns.Add("lat3");
            dtUbictmp.Columns.Add("lng3");
            dtUbictmp.Columns.Add("lat4");
            dtUbictmp.Columns.Add("lng4");


            dtUbictmp.Columns.Add("lat1m");
            dtUbictmp.Columns.Add("lng1m");
            dtUbictmp.Columns.Add("lat2m");
            dtUbictmp.Columns.Add("lng2m");
            dtUbictmp.Columns.Add("lat3m");
            dtUbictmp.Columns.Add("lng3m");
            dtUbictmp.Columns.Add("lat4m");
            dtUbictmp.Columns.Add("lng4m");

            dtUbictmp.Columns.Add("lat1a");
            dtUbictmp.Columns.Add("lng1a");
            dtUbictmp.Columns.Add("lat1b");
            dtUbictmp.Columns.Add("lng1b");
            dtUbictmp.Columns.Add("lat2a");
            dtUbictmp.Columns.Add("lng2a");
            dtUbictmp.Columns.Add("lat2b");
            dtUbictmp.Columns.Add("lng2b");
            dtUbictmp.Columns.Add("lat3a");
            dtUbictmp.Columns.Add("lng3a");
            dtUbictmp.Columns.Add("lat3b");
            dtUbictmp.Columns.Add("lng3b");
            dtUbictmp.Columns.Add("lat4a");
            dtUbictmp.Columns.Add("lng4a");
            dtUbictmp.Columns.Add("lat4b");
            dtUbictmp.Columns.Add("lng4b");


            consultaUbicaciones();


        }

        private void tablaUbicTempg()
        {

            dtUbictmpgrid = new DataTable();

            dtUbictmpgrid.Columns.Add("corral");
            dtUbictmpgrid.Columns.Add("lat");
            dtUbictmpgrid.Columns.Add("lng");
            dtUbictmpgrid.Columns.Add("lat1");
            dtUbictmpgrid.Columns.Add("lng1");
            dtUbictmpgrid.Columns.Add("lat2");
            dtUbictmpgrid.Columns.Add("lng2");
            dtUbictmpgrid.Columns.Add("lat3");
            dtUbictmpgrid.Columns.Add("lng3");
            dtUbictmpgrid.Columns.Add("lat4");
            dtUbictmpgrid.Columns.Add("lng4");


            dtUbictmpgrid.Columns.Add("lat1m");
            dtUbictmpgrid.Columns.Add("lng1m");
            dtUbictmpgrid.Columns.Add("lat2m");
            dtUbictmpgrid.Columns.Add("lng2m");
            dtUbictmpgrid.Columns.Add("lat3m");
            dtUbictmpgrid.Columns.Add("lng3m");
            dtUbictmpgrid.Columns.Add("lat4m");
            dtUbictmpgrid.Columns.Add("lng4m");


        }

        private void llenaListagrid()
        {

            double vlat = 0;
            double vlng = 0;
            double vlat1 = 0;
            double vlng1 = 0;
            double vlat2 = 0;
            double vlng2 = 0;
            double vlat3 = 0;
            double vlng3 = 0;
            double vlat4 = 0;
            double vlng4 = 0;

            double vlat1m = 0;
            double vlng1m = 0;
            double vlat2m = 0;
            double vlng2m = 0;
            double vlat3m = 0;
            double vlng3m = 0;
            double vlat4m = 0;
            double vlng4m = 0;

            dtUbictmpgrid.Clear();

            foreach (DataGridViewRow row in DGServidas.Rows)
            {
                                             
                DataRow newRow = dtUbictmpgrid.NewRow();

                newRow["corral"] = row.Cells["corral"].Value.ToString();

                DataRow[] rows = dtUbictmp.Select("corral = '" + newRow["Corral"] + "'");

                foreach (DataRow row2 in rows)
                {
                    vlat = Convert.ToDouble(row2["lat"]);
                    vlng = Convert.ToDouble(row2["lng"]);
                    vlat1 = Convert.ToDouble(row2["lat1"]);
                    vlng1 = Convert.ToDouble(row2["lng1"]);
                    vlat2 = Convert.ToDouble(row2["lat2"]);
                    vlng2 = Convert.ToDouble(row2["lng2"]);
                    vlat3 = Convert.ToDouble(row2["lat3"]);
                    vlng3 = Convert.ToDouble(row2["lng3"]);
                    vlat4 = Convert.ToDouble(row2["lat4"]);
                    vlng4 = Convert.ToDouble(row2["lng4"]);

                    vlat1m = (vlat1 + vlat2) / 2;
                    vlng1m = (vlng1 + vlng2) / 2;
                    vlat2m = (vlat2 + vlat3) / 2;
                    vlng2m = (vlng2 + vlng3) / 2;
                    vlat3m = (vlat3 + vlat4) / 2;
                    vlng3m = (vlng3 + vlng4) / 2;
                    vlat4m = (vlat4 + vlat1) / 2;
                    vlng4m = (vlng4 + vlng1) / 2;

                }
                               
                newRow["lat"] = vlat;
                newRow["lng"] = vlng;
                newRow["lat1"] = vlat1;
                newRow["lng1"] = vlng1;
                newRow["lat2"] = vlat2;
                newRow["lng2"] = vlng2;
                newRow["lat3"] = vlat3;
                newRow["lng3"] = vlng3;
                newRow["lat4"] = vlat4;
                newRow["lng4"] = vlng4;

                newRow["lat1m"] = vlat1m;
                newRow["lng1m"] = vlng1m;
                newRow["lat2m"] = vlat2m;
                newRow["lng2m"] = vlng2m;
                newRow["lat3m"] = vlat3m;
                newRow["lng3m"] = vlng3m;
                newRow["lat4m"] = vlat4m;
                newRow["lng4m"] = vlng4m;

                dtUbictmpgrid.Rows.Add(newRow);

            }

        }

        private void seleccionarFila() 
        {
     
            if (DGServidas.RowCount != 0)
            {
                Datos datos = new Datos();
                DataTable dt = new DataTable();

                string searchValue = "0";
                //int filas = 0;
                int rowIndex = -1;

                datos.p_sql = "SELECT idd FROM operacion";

                if (!datos.consultaSQL(ref dt, true))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        searchValue = row["IdD"].ToString();
                    }
                }
                
                if (searchValue != "0")
                { 

                    foreach (DataGridViewRow row in DGServidas.Rows)
                    {
                        if (row.Cells["IdD"].Value.ToString() == searchValue)
                        {
                            rowIndex = row.Index;
                            break;
                        }
                    }

                    if (rowIndex != -1)
                    {
                        DGServidas.Rows[rowIndex].Selected = true;

                        txtIdD.Text = searchValue;

                        DGServidas.FirstDisplayedScrollingRowIndex = rowIndex;

                        filaDatos(txtIdD.Text);

                        ubicaCorralMapa();

                    }

                }
                


            }
        }

        private void seleccionarFila2()
        {

            txtFilaAct.Text = cadenaCero(txtFilaAct.Text); 

            int vfilaAct = Convert.ToInt32(txtFilaAct.Text.Replace(",",""));

            if (DGServidas.RowCount != 0)
            {

                if (DGServidas.RowCount == vfilaAct)
                {
                    vfilaAct -= 1;
                }

                DGServidas.CurrentCell = DGServidas.Rows[vfilaAct].Cells[0];

                //DGServidas.Rows[vfilaAct].Selected = true;
                
                if (vfilaAct > 10) 
                { 
                    DGServidas.FirstDisplayedScrollingRowIndex = vfilaAct;                
                }

                DGServidas.Refresh();

                txtIdD.Text = DGServidas.Rows[vfilaAct].Cells["IdD"].Value.ToString();

                filaDatos(txtIdD.Text);

                ubicaCorralMapa();

                filaAct();
                //txtFilaAct.Text = vfilaAct.ToString();

            }

        }
        
        private void refrescaGrid()
        {
            //formatoGrid();
            datosGrid();
            colorFila();

            llenaListagrid();
            ubicServir(dtUbictmpgrid);
            //DGServidas.FirstDisplayedCell = DGServidas.CurrentCell;

        }

        private void limpiarCorral() 
        { 
        
            txtCorral.Text = "";
            txtRacion.Text = "";
            txtCabezas.Text = "";
            txtIdD.Text = "";
            txtIdDr.Text = "";

            txtCorrales.Text = "";
            txtPlan.Text = "";
            txtCargado.Text = "";
            txtBascula.Text = "";
            txtServido.Text = "";
            txtFisico.Text = "";
            txtPlanC.Text = "";
            txtFalta.Text = "";
            txtFaltaV.Text = "";
            txtServidoExtra.Text = "";
            txtServidoExtraV.Text = "";
            
            txtBasculaV.Text = "";
            txtCargadoV.Text = "";
            txtPlan.Text = "";
            txtCorrales.Text = "";

            botonesHabilita(false);
        
        }

        private void botonesHabilita(bool habilita) 
        {
            
            //if(habilitado != habilita)
            //{
            //    if (habilita)
            //    {
            //        btnServir.Enabled = true;
            //        btnHecho.Enabled = true;
            //        btnQuitar.Enabled = true;
            //        btnBuscar.Enabled = true;
            //    }
            //    else
            //    {
            //        btnServir.Enabled = false;
            //        btnHecho.Enabled = false;
            //        btnQuitar.Enabled = false;
            //        btnBuscar.Enabled = false;
            //    }
            

            //    habilitado = habilita;
            //}   

        }

        private void barra() 
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();
            DataTable table = (DataTable)(DGServidas.DataSource);

            int maximo = 0;
            int valor = 0;
            int corrales = 0;

            datos.p_sql = "SELECT count(corral) as Corrales , sum(planeado) as Planeado, sum(realidad) as Realidad FROM servidas WHERE racion = '" + vracion.ToString() + "' and num = " + txtServida.Text + " and id = " + txtJornada.Text; //" and  ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {

                    if (row["Corrales"].ToString() != "0")
                    {
                        maximo = Convert.ToInt32(row["Planeado"].ToString());
                        valor = Convert.ToInt32(row["Realidad"].ToString());
                        corrales = Convert.ToInt32(row["Corrales"].ToString());
                    }
                }
            }

            if (txtPlan.Text == "") {
                txtPlan.Text = "0";
            }
            if (txtFalta.Text == "")
            {
                txtFalta.Text = "0";
            }

            maximo = Convert.ToInt32(txtPlan.Text.Replace(",",""));
            valor = maximo - Convert.ToInt32(txtFalta.Text.Replace(",", "")); ;



            pbServido.Maximum = maximo;

            //txtPlan.Text = maximo.ToString();
            //txtCorrales.Text = corrales.ToString();

            if (maximo <= valor)
            {
                valor = maximo;
            }
            //pbServido.Value = valor;

           


        }
        
        private void propiedadesGrid() 
        { 
        
            DGServidas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGServidas.ReadOnly = true;
            DGServidas.AllowUserToAddRows = false;
            DGServidas.AllowUserToDeleteRows = false;
            DGServidas.AllowUserToOrderColumns = false;
            DGServidas.AllowUserToResizeColumns = false;
            DGServidas.AllowUserToResizeRows = false;
            DGServidas.MultiSelect = false;
            //DGServidas.ColumnHeadersVisible = false;
            DGServidas.RowHeadersVisible = false;
            DGServidas.CellBorderStyle = DataGridViewCellBorderStyle.None;
            DGServidas.DefaultCellStyle.Font = new Font("Arial", 25.00F, FontStyle.Bold);
            //DGServidas.RowTemplate.Height = 50;




        }
        
        private void formatoGrid() 
        { 
        
            DataTable table = new DataTable();

            table.Columns.Add("Corral", typeof(string));
            table.Columns.Add("Racion", typeof(string));
            table.Columns.Add("Plan", typeof(long));
            table.Columns.Add("Servido", typeof(long));
            table.Columns.Add("Variacion", typeof(long));
            table.Columns.Add("Hora", typeof(string));
            table.Columns.Add("IdD", typeof(string));

            DGServidas.DataSource = table;

            for (int i = 0; i < table.Columns.Count; i++)
            {
                DGServidas.Columns[i].Width = 130;
                DGServidas.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGServidas.Columns[i].HeaderCell.Style.Font = new Font("Tahoma", 15.00F, FontStyle.Bold);
                DGServidas.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                DGServidas.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
            }
            
            DGServidas.Columns["Corral"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DGServidas.Columns["Racion"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DGServidas.Columns["Plan"].DefaultCellStyle.Format = "#,0.###";
            DGServidas.Columns["Servido"].DefaultCellStyle.Format = "#,0.###";
            DGServidas.Columns["Variacion"].DefaultCellStyle.Format = "#,0.###";

            DGServidas.Columns["Hora"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            DGServidas.Columns["IdD"].Width = 0;
            DGServidas.Columns["Racion"].Width = 0;
            DGServidas.Columns["Hora"].Width = 0;

        }

        private void colorFila() 
        {

           string horaMin = DateTime.Now.AddMinutes(-30).ToString("HHmm");
           string horaAct = DateTime.Now.ToString("HHmm");
           string horaMax = DateTime.Now.AddMinutes(30).ToString("HHmm");

           int nHoraMin = Convert.ToInt32(horaMin);
           int nHoraActual = Convert.ToInt32(horaAct);
           int nHoraMax = Convert.ToInt32(horaMax);

             

            for (int i = 0; i < DGServidas.Rows.Count; i++)
            {
                if (DGServidas.Rows[i].Cells["Hora"].Value.ToString() != "00:00")
                {
                     
                    string horaSel = DGServidas.Rows[i].Cells["Hora"].Value.ToString();
                    horaSel = horaSel.Replace(":", "");
                    int mHoraSel = Convert.ToInt32(horaSel);

                    if (mHoraSel >= nHoraMin && mHoraSel <= nHoraMax)
                    {
                        DGServidas.Rows[i].Cells["Hora"].Style.BackColor = Color.Green;
                        DGServidas.Rows[i].Cells["Hora"].Style.ForeColor = Color.White;
                        //DGServidas.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else
                    {
                        DGServidas.Rows[i].Cells["Hora"].Style.BackColor = Color.White;
                        DGServidas.Rows[i].Cells["Hora"].Style.ForeColor = Color.Black;
                        //DGServidas.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
            
        
        }

        private void datosGrid()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();
            DataTable table = (DataTable)(DGServidas.DataSource);

            table.Rows.Clear();

            string idd = "0";
            bool primero = true;
            string accion = "";


            datos.p_sql = "SELECT accion FROM operacion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    accion = row["Accion"].ToString();
                }
            }




            datos.p_sql = "SELECT Corral,Racion,Planeado,Realidad,HoraPlan,IdD FROM servidas WHERE hecho = 'N' and racion = '" + vracion.ToString() + "' AND num = " + txtServida.Text + " and id = " + txtJornada.Text + " order by idd"; //" and  ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    table.Rows.Add(row["Corral"].ToString(), row["Racion"].ToString(), row["Planeado"].ToString(), row["Realidad"].ToString(), Convert.ToInt32(row["Realidad"]) - Convert.ToInt32(row["Planeado"]), row["HoraPlan"].ToString(), row["IdD"].ToString());

                    if (row["Realidad"].ToString() == "0" && primero)
                    {
                        idd = row["IdD"].ToString();
                        primero = false;
                    }
                }

                if(accion.ToString() == "S")
                {
                    datos.p_sql = "UPDATE operacion SET idd = " + idd.ToString();  

                    if (!datos.consultaSQL(ref dt, false))
                    {
                        MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
   
            }





        }
                
        //private void btnLista_Click(object sender, EventArgs e)
        //{

            //Datos datos = new Datos();
            //DataTable dt = new DataTable();
            //DataTable dt2 = new DataTable();

            //OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //openFileDialog1.Filter = "Archivos Txt|*.txt";
            //openFileDialog1.Title = "Seleccion un Archivo Texto";

            //int registros_sel = 0;
            //int registros_ins = 0;
            //int registrost = 0;
            
            //if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{

            //    pbAgregar.Value = 0;
            //    pbAgregar.Visible = true;
            //    this.Cursor = Cursors.WaitCursor;
                
            //    try
            //    {
            //        StreamReader objReader = new StreamReader(openFileDialog1.OpenFile());
            //        string sLine = "";


            //        while (sLine != null)
            //        {
            //            sLine = objReader.ReadLine();
            //            if (sLine != null)
            //            {
            //                registrost = registrost + 1;
            //            }
            //        }

            //        pbAgregar.Maximum = registrost;

            //        objReader.Close();


            //        objReader = new StreamReader(openFileDialog1.OpenFile());
            //        sLine = "";

            //        //ArrayList arrText = new ArrayList();
            //        while (sLine != null)
            //        {
            //            sLine = objReader.ReadLine();
            //            if (sLine != null)
            //            //arrText.Add(sLine);
            //            {

            //                string[] lines = sLine.Split(',');

            //                try
            //                {
                                                                
            //                    //Datos datos = new Datos();
            //                    //DataTable dt = new DataTable();

            //                    //datos.p_fecha = DateTime.Now.ToString("dd-MM-yyyy"); //  "2018-12-21";
            //                    //datos.p_num = Convert.ToInt32(lines[0]);
            //                    //datos.p_corral = lines[1];
            //                    //datos.p_racion = lines[2];
            //                    //datos.p_cabezas = Convert.ToInt32(lines[3]);
            //                    //datos.p_planeado = Convert.ToInt32(lines[4]);
            //                    //datos.p_realidad = 0;
            //                    //datos.p_hora = "00:00"; //"17:30";

            //                    registros_sel = registros_sel + 1;
            //                    pbAgregar.Value = registros_sel;
            //                    pbAgregar.PerformStep();
            //                    pbAgregar.Focus();

            //                    datos.p_sql = "SELECT idd FROM servidas WHERE num = " + lines[0].Trim() + " and corral = '" + lines[1].Trim() + "' and racion = '" + lines[2].Trim() + "' and ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " )";

            //                    if (!datos.consultaSQL(ref dt, true))
            //                    {
            //                        MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //                    }

            //                    if (dt.Rows.Count == 0)
            //                    {
                                    
            //                        datos.p_sql = "insert into servidas (fecha,num,corral,racion,cabezas,planeado,realidad,hora) values('" + DateTime.Now.ToString("dd-MM-yyyy") + "'," + Convert.ToInt32(lines[0].Trim()) + ",'" + lines[1].Trim() + "','" + lines[2].Trim() + "'," + Convert.ToInt32(lines[3].Trim()) + "," + Convert.ToInt32(lines[4].Trim()) + "," + "0" + ",'" + "00:00" + "')";

            //                        if (!datos.consultaSQL(ref dt, false))
            //                        {
            //                            MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //                        }
            //                        else 
            //                        {
            //                            registros_ins = registros_ins + 1;
            //                        }

            //                    }

            //                }
            //                catch (Exception exc)
            //                {
            //                    MessageBox.Show(exc.Message);
            //                }

            //            }
            //        }





            //        datos.p_sql = "SELECT num as Num, racion as Racion, count(corral) as Corrales , sum(planeado) as Planeado, sum(realidad) as Realidad FROM servidas WHERE ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) group by num, racion";

            //        if (!datos.consultaSQL(ref dt, true))
            //        {
            //            MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //        }
            //        else
            //        {
            //            foreach (DataRow row in dt.Rows)
            //            {

            //                if (row["Corrales"].ToString() != "0")
            //                {

            //                    //datos.p_sql = "SELECT idd FROM raciones WHERE num = " + row["Num"].ToString() + " and racion = '" + row["Racion"].ToString() + "' and ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " )";

            //                    //if (!datos.consultaSQL(ref dt2, true))
            //                    //{
            //                    //    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //                    //}

            //                    //if (dt2.Rows.Count == 0)
            //                    //{
            //                        datos.p_sql = "insert into raciones (fecha,num,racion,cantidad,planeado,cargado,hora) values('" + DateTime.Now.ToString("dd-MM-yyyy") + "'," + row["Num"].ToString() + ",'" + row["Racion"].ToString() + "'," + row["Corrales"].ToString() + "," + row["Planeado"].ToString() + ",0,'00:00')";
            //                    //}
            //                    //else
            //                    //{
            //                    //    datos.p_sql = "UPDATE raciones SET planeado = " + row["Planeado"].ToString() + " WHERE num = " + row["Num"].ToString() + " and racion = '" + row["Racion"].ToString() + "' and ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " )";
            //                    //}

            //                    if (!datos.consultaSQL(ref dt2, false))
            //                    {
            //                        MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //                    }


            //                }

                        


                        
            //            }

            //        }

            //        MessageBox.Show("Listo, Regitros : " + registros_ins.ToString(), "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //        pbAgregar.Visible = false;
            //        DGServidas.Focus();

            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("El archivo no se puede leer");
            //    }
                            
            //    this.Cursor = Cursors.Default;
            
            //}


        //}

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            //Close();
            string vjornada;
            vjornada = txtJornada.Text;



            Raciones frm = new Raciones(vjornada);
            //frm.MdiParent = this;
            frm.Show();

            Close();


            //eliminarOperacion();

        }

        private void DGServidas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGServidas.CurrentRow != null)
            {
                txtIdD.Text = (string)DGServidas.CurrentRow.Cells["IdD"].Value;

                filaDatos(txtIdD.Text);

                ubicaCorralMapa();

            }
        }

        private void DGServidas_SelectionChanged(object sender, EventArgs e)
        {
            if (DGServidas.CurrentRow != null)
            {
                txtIdD.Text = (string)DGServidas.CurrentRow.Cells["IdD"].Value;

                filaDatos(txtIdD.Text);

                ubicaCorralMapa();

            }
        }

        private void filaAct() 
        {
            txtFilaAct.Text = DGServidas.CurrentRow.Index.ToString();
        }

        private void ubicaCorralMapa() 
        {

            if (lblLatitud.Text.Equals("0"))
            {

                DataRow row = consultaUbicacion(txtCorral.Text);

                if (row != null)
                {
                    gMapControl1.Position = new PointLatLng(double.Parse(row["lat1"].ToString()), double.Parse(row["lng1"].ToString()));
                }
            }

        }

        private void filaDatos(string idd) 
        {
                                  
            Datos datos = new Datos();
            DataTable dt = new DataTable();
            int resultado = 0;

            datos.p_sql = "SELECT * FROM servidas WHERE idd =" + idd.ToString();

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {

                foreach (DataRow row in dt.Rows)
                {
                    txtCorral.Text = row["Corral"].ToString();
                    txtRacion.Text = row["Racion"].ToString();
                    txtCabezas.Text = row["Cabezas"].ToString();
                    txtPlanC.Text = row["Planeado"].ToString();
                    txtRealidadC.Text = row["Realidad"].ToString();

                    long variacionC = long.Parse(row["Realidad"].ToString()) - long.Parse(row["Planeado"].ToString()) ;
                    txtVariacionC.Text = variacionC.ToString();
                }

                //datos.p_sql = "UPDATE operacion SET idd =" + idd.ToString();

                //if (!datos.consultaSQL(ref dt, false))
                //{
                //    MessageBox.Show(datos.p_error);
                //}



                //botonesHabilita(true);

                if (txtIdDr.Text == "")
                {
                    txtIdDr.Text = "0";
                }

                if (txtCargadoV.Text == "")
                {
                    txtCargadoV.Text = "0";
                }


                if (txtBasculaV.Text == "")
                {
                    txtBasculaV.Text = "0";
                }

                datos.p_sql = "SELECT Racion,Cantidad,Planeado,Carga,Servido,Falta,Bascula,IdD FROM raciones WHERE num = " + txtServida.Text + " and racion ='" + txtRacion.Text + "' and id = " + txtJornada.Text; //"' and ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ";

                if (!datos.consultaSQL(ref dt, true))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                else
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        //txtCorrales.Text = row["Cantidad"].ToString();
                        txtPlan.Text = row["Planeado"].ToString();
                        txtCargado.Text = row["Carga"].ToString();
                        txtBascula.Text = row["Bascula"].ToString();
                        txtServido.Text = row["Servido"].ToString();
                        txtIdDr.Text = row["IdD"].ToString();

                        resultado = Convert.ToInt32(cadenaCero(row["Carga"].ToString())) - Convert.ToInt32(row["Servido"].ToString());
                        txtFisico.Text = resultado.ToString();

                        //resultado = Convert.ToInt32(row["Planeado"].ToString()) - Convert.ToInt32(row["Servido"].ToString());
                        resultado = Convert.ToInt32(row["Falta"].ToString());
                        txtFalta.Text = resultado.ToString();


                        resultado = Convert.ToInt32(row["Falta"].ToString()) - ( Convert.ToInt32(row["Planeado"].ToString()) - Convert.ToInt32(row["Servido"].ToString()) );
                        txtServidoExtra.Text = resultado.ToString();

                    }

                }
            
            }

            fomatoCampos();
        }
        
        private void btnServir_Click(object sender, EventArgs e)
        {
    
            //if (long.Parse(txtVariacionC.Text.Replace("-","").Trim()) > long.Parse(txtCargado.Text))
            //{             
            //    Comun.mensajeVer("Insuficiente Alimento en Carga Para Servir", Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
            //}
            //else
            //{ 

            //txtcorral  y  lblcorral

                if (txtIdD.Text != "")
                {
                    txtSirviendo.Text = txtCorral.Text;

                    filaAct();

                    vnumcarga = cargaServida();

                    Senga.Servir frm = new Senga.Servir(txtIdD.Text,txtIdDr.Text,txtJornada.Text,lblCorral.Text, gLatitud, gLongitud, vnumcarga, txtCorrales.Text);
                    frm.Show();
                }
                
            //}

            //guardarOperacion();

        }

        private string cargaServida() 
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            string valor = "0";

            datos.p_sql = "SELECT * FROM raciones WHERE idd =" + txtIdDr.Text;

            if (!datos.consultaSQL(ref dt, true))
            {
                //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {

                foreach (DataRow row in dt.Rows)
                {
                    valor = row["NumCarga"].ToString();
                }
            }

            return valor;

        }

        private void eliminarOperacion()
        {

            string ruta = "";

            ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\OperacionSenga.txt";
                       
            if (System.IO.File.Exists(ruta))
            {
                try
                {
                    System.IO.File.Delete(ruta);
                }
                catch (System.IO.IOException e)
                {
                    return;
                }
                
            }
        }

        private void guardarOperaciontxt()
        {

            string rutadestino = "";
            
            rutadestino = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\OperacionSenga.txt";

            string vplan = txtPlan.Text;
            vplan = vplan.Replace(",", "");

            string vservido = txtServido.Text;
            vservido = vservido.Replace(",", "");

            if (vservido.Equals(""))
            {
                vservido = "0";
            }

            string vcargado = txtCargadoV.Text;
            vcargado = vcargado.Replace(",", "");

            if (vcargado.Equals(""))
            {
                vcargado = "0";
            }

            string vfalta = txtFaltaV.Text;
            vfalta = vfalta.Replace(",", "");

            if (vfalta.Equals(""))
            {
                vfalta = "0";
            }

            this.Cursor = Cursors.WaitCursor;

            try
            {
                
                StreamWriter objWriter = new StreamWriter(rutadestino);

                string lv_cadena =

                "S" + "|" +
                txtCamion.Text + "|" +
                txtServida.Text + "|" +
                txtCorral.Text + "|" +
                txtRacion.Text + "|" +
                vplan + "|" +
                vservido + "|" +
                vcargado + "|" +
                vfalta + "|" +
                txtCorrales.Text + "|" +
                String.Format("{0:HH:mm:ss}", DateTime.Now) + "|" +
                String.Format("{0:yyyy-MM-dd}",DateTime.Today);

                objWriter.WriteLine(lv_cadena);

                objWriter.Close();
                            
                //MessageBox.Show("Listo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            }
            catch (Exception)
            {
                //MessageBox.Show("El archivo no se puede guardar");
            }

            this.Cursor = Cursors.Default;
            
        }


        private void guardarRepartotxt()
        {
            
            string rutadestino = "";
            string cantServido = "0";

            rutadestino = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\RepartoSenga.txt";
                      
            try
            {
                               
                Datos datos = new Datos();
                DataTable dt = new DataTable();
                DataTable dt2 = new DataTable();


                //datos.p_sql = "SELECT SUM( Realidad ) AS Realidad FROM servidas WHERE corral = '" + txtSirviendo.Text + "' and  ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " )";
                datos.p_sql = "SELECT corral, SUM( Realidad ) AS Realidad FROM servidas WHERE ( day(fecha) = " + DateTime.Now.ToString("dd") + " and month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) GROUP BY corral";

                if (!datos.consultaSQL(ref dt, true))
                {

                }
                else
                {

                    //if (dt.Rows.Count > 0)
                    //{
                        
                    //    foreach (DataRow row in dt.Rows)
                    //    {
                    //        cantServido = row["Realidad"].ToString();
                    //    }

                    //}

                }
                    
                ////////////////////////////////////////////////////////////////////////////////////////

                //un corral
                //datos.p_sql = "SELECT * FROM servidas WHERE corral = '" + txtSirviendo.Text + "' and num = " + txtServida.Text + " and id = " + txtJornada.Text + " and realidad > 0";
                
                //corrales un servida
                datos.p_sql = "SELECT TOP 10 * FROM servidas WHERE num = " + txtServida.Text + " and id = " + txtJornada.Text + " and realidad > 0 and ( day(fecha) = " + DateTime.Now.ToString("dd") + " and month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) order by fecha desc, hora desc";
                
                //corrales de la jornada
                //datos.p_sql = "SELECT * FROM servidas WHERE id = " + txtJornada.Text + " and realidad > 0 order by corral, hora desc";
             
                if (!datos.consultaSQL(ref dt2, true))
                {

                }
                else
                {
                    //dt2.Columns.Add("cantServido");
                    
                    if (dt2.Rows.Count > 0)
                    {                                            
                        
                        StreamWriter objWriter = new StreamWriter(rutadestino);

                        foreach (DataRow row in dt2.Rows)
                        {

                            DataRow[] rows = dt.Select("corral = '"+ row["Corral"].ToString()+"'");
                                                        
                            cantServido = "0";

                            foreach (DataRow row2 in rows)
                            {
                                cantServido = row2["Realidad"].ToString();
                            }

                            //if (!corralAnt.Equals(row["Corral"].ToString()))
                            //{
                            //servido por corral mas actual
                            objWriter.WriteLine(txtCamion.Text + "|" + row["Num"].ToString() + "|" + row["Corral"].ToString() + "|" + row["Racion"].ToString() + "|" + row["Planeado"].ToString() + "|" + row["Realidad"].ToString() + "|" + row["Hora"].ToString() + "|" + String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(row["Fecha"])) + "|" + row["PlanDia"].ToString() + "|" + cantServido);

                            //    corralAnt = row["Corral"].ToString();
                            //}

                        }
                    
                        objWriter.Close();

                    }

                }
                               
            }
            catch (Exception)
            {

            }                            

        }

        private void operacionCorral() 
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            datos.p_sql = "SELECT TOP 1 * FROM configuracion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    txtCamion.Text = row["NumCamion"].ToString();
                }
            }

            datos.p_sql = "SELECT TOP 1 * FROM operacion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    txtServida.Text = row["Servida"].ToString();
                    txtJornada.Text = row["Jornada"].ToString();
                }
            }

            //datos.p_sql = "SELECT TOP 1 * FROM operacion";

            //if (!datos.consultaSQL(ref dt, true))
            //{
            //    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //}
            //else
            //{
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        txtJornada.Text = row["Jornada"].ToString();
            //    }
            //}
        
        }

        private void btnServida_Click(object sender, EventArgs e)
        {
            Senga.Captura frm = new Senga.Captura(Convert.ToInt32(txtJornada.Text),1);
            frm.Show();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Senga.Captura frm = new Senga.Captura(Convert.ToInt32(txtJornada.Text),2);
            //frm.Show();

            if (DGServidas.Height == 320)
            {
                DGServidas.Height = 590;
                panelCaptura.Visible = false;
            }
            else 
            { 
                DGServidas.Height = 320;
                panelCaptura.Visible = true;
            }



        }

        //private void btnArchivos_Click(object sender, EventArgs e)
        //{
            
        //    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        //    saveFileDialog1.Filter = "Archivos Txt|*.txt";
        //    saveFileDialog1.Title = "Guardar Como ...";
        //    saveFileDialog1.FileName = "T_SERVIDO";

        //    if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {

        //        this.Cursor = Cursors.WaitCursor;

        //        try
        //        {
        //            //Open the File
        //            StreamWriter objWriter = new StreamWriter(saveFileDialog1.FileName);

        //            Datos datos = new Datos();
        //            DataTable dt = new DataTable();

        //            datos.p_sql = "SELECT * FROM servidas WHERE hecho = 'S' AND num = " + txtServida.Text + " and id = " + txtJornada.Text; //" and  ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " )";

        //            if (!datos.consultaSQL(ref dt, true))
        //            {
        //                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //            }
        //            else
        //            {
        //                foreach (DataRow row in dt.Rows)
        //                {
        //                    objWriter.WriteLine(txtCamion.Text + "," + /*row["Hecho"].ToString() + "," + "C" + "," +*/ row["Num"].ToString() + "," + row["Corral"].ToString() + "," + row["Racion"].ToString() + "," + row["Realidad"].ToString() + "," + row["Hora"].ToString() + "," + String.Format("{0:dd-MM-yy}", Convert.ToDateTime(row["Fecha"])));
        //                }
        //            }
                    
        //            objWriter.Close();

        //            MessageBox.Show("Listo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

        //        }
        //        catch (Exception)
        //        {
        //            MessageBox.Show("El archivo no se puede guardar");
        //        }

        //     this.Cursor = Cursors.Default;

        //    }


        //}

        private void sonido() 
        {

            //SoundPlayer simpleSound = new SoundPlayer("beep-07.wav");
            //simpleSound.Play();  
        
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {

            //if (DGServidas.RowCount != 0)
            if (txtIdD.Text != "")
            {
                Datos datos = new Datos();
                DataTable dt = new DataTable();
                long idd = Convert.ToInt64(txtIdD.Text);
                int realidad = 0;
                int cantidadt = 0;
                string corral = "";


                this.Cursor = Cursors.WaitCursor;

                filaAct();

                datos.p_sql = "SELECT corral, realidad FROM servidas WHERE idd = " + idd.ToString();

                if (!datos.consultaSQL(ref dt, true))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        realidad = Convert.ToInt32(row["Realidad"].ToString());
                        corral = row["Corral"].ToString();
                    }
                }
                
                if (realidad == 0)
                {

                    if (Comun.mensajeVer("¿ Desea Quitar Corral " + corral.ToString() + " ?", Constants.MSG_BOTON_SINO, Constants.MSG_IMG_QUESTION) == Constants.MSG_BOTON_SI)
                    //if (MessageBox.Show("¿ Desea Quitar Corral " + corral.ToString() + " ?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        datos.p_sql = "UPDATE servidas SET hecho = 'X' WHERE idd = " + idd.ToString();

                        if (!datos.consultaSQL(ref dt, false))
                        {
                            MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        else
                        {
                            
                          
                            refrescaGrid();

                            if (DGServidas.RowCount == 0)
                            {
                                limpiarCorral();
                            }


                            seleccionarFila2();


                            cantidadTotal();
                            cantidadt = Convert.ToInt32(txtCantidadT.Text);

                            long falta = 0;

                            datos.p_sql = "SELECT sum(planeado) as Planeado, sum(realidad) as Realidad FROM servidas WHERE hecho = 'N' and racion = '" + vracion.ToString() + "' and num = " + txtServida.Text + " and id = " + txtJornada.Text; //" and  ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ";

                            if (!datos.consultaSQL(ref dt, true))
                            {
                                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            else
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    if (dt.Rows.Count != 0)
                                    {
                                        falta = /*long.Parse(cadenaCero(row["Realidad"].ToString())) -*/ long.Parse(cadenaCero(row["Planeado"].ToString()));
                                    }
                                }
                            }

                            datos.p_sql = "UPDATE raciones SET falta = " + falta.ToString() + ", hechoc = " + cantidadt.ToString() + " WHERE racion = '" + vracion.ToString() + "' and num = " + txtServida.Text + " and id = " + txtJornada.Text; //" and  ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ";

                            if (!datos.consultaSQL(ref dt, false))
                            {
                                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }

                            txtFalta.Text = falta.ToString();
                            txtFaltaV.Text = string.Format("{0:#,##0}", long.Parse(txtFalta.Text));
                            txtCorrales.Text = DGServidas.RowCount.ToString();

                            //Comun.mensajeVer("Listo", Constants.MSG_BOTON_OK, Constants.MSG_IMG_OK);
                        }
                    }

                }
                else
                {
                    //MessageBox.Show("Solo Corrales Cero Servido", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    Comun.mensajeVer("Solo Corrales Cero Servido", Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
                }

                this.Cursor = Cursors.Default;

            }

        }

        private void btnHecho_Click(object sender, EventArgs e)
        {

            //if (DGServidas.RowCount != 0)
            if (txtIdD.Text != "")
            {
                Datos datos = new Datos();
                DataTable dt = new DataTable();
                long idd = Convert.ToInt64(txtIdD.Text);
                int realidad = 0;
                int cantidadt = 0;
                string corral = "";

                this.Cursor = Cursors.WaitCursor;

                filaAct();

                datos.p_sql = "SELECT corral, realidad FROM servidas WHERE idd = " + idd.ToString();

                if (!datos.consultaSQL(ref dt, true))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        realidad = Convert.ToInt32(row["Realidad"].ToString());
                        corral = row["Corral"].ToString();
                    }
                }

                if (realidad != 0)
                {

                    if (Comun.mensajeVer("¿ Desea Terminar Corral " + corral.ToString() + " ?", Constants.MSG_BOTON_SINO, Constants.MSG_IMG_QUESTION) == Constants.MSG_BOTON_SI)
                    //if (MessageBox.Show("¿ Desea Terminar Corral " + corral.ToString() + " ?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        
                        datos.p_sql = "UPDATE servidas SET hecho = 'S' WHERE idd = " + idd.ToString();

                        if (!datos.consultaSQL(ref dt, false))
                        {
                            MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        else
                        {
                            
                            refrescaGrid();
                         
                            if (DGServidas.RowCount == 0)
                            {
                                  limpiarCorral();
                            }
                            
                            seleccionarFila2();

                            cantidadTotal();
                            cantidadt = Convert.ToInt32(txtCantidadT.Text);



                            long falta = 0;

                            datos.p_sql = "SELECT sum(planeado) as Planeado, sum(realidad) as Realidad FROM servidas WHERE hecho = 'N' and racion = '" + vracion.ToString() + "' and num = " + txtServida.Text + " and id = " + txtJornada.Text; //" and  ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ";

                            if (!datos.consultaSQL(ref dt, true))
                            {
                                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            else
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    if (dt.Rows.Count != 0)
                                    {
                                        falta = /*long.Parse(cadenaCero(row["Realidad"].ToString())) -*/ long.Parse(cadenaCero(row["Planeado"].ToString()));
                                    }
                                }
                            }


                            datos.p_sql = "UPDATE raciones SET falta = " + falta.ToString() + ", hechoc = " + cantidadt.ToString() + " WHERE racion = '" + vracion.ToString() + "' and num = " + txtServida.Text + " and id = " + txtJornada.Text; //" and  ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ";

                            if (!datos.consultaSQL(ref dt, false))
                            {
                                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }


                            txtFalta.Text = falta.ToString();
                            txtFaltaV.Text = string.Format("{0:#,##0}", long.Parse(txtFalta.Text));
                            txtCorrales.Text = DGServidas.RowCount.ToString();

                            //Comun.mensajeVer("Listo", Constants.MSG_BOTON_OK, Constants.MSG_IMG_OK);


                        }
                    }
                }
                else
                {
                    //MessageBox.Show("Solo Corrales con Alimento Servido", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    Comun.mensajeVer("Solo Corrales con Alimento Servido", Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
                }

                this.Cursor = Cursors.Default;

            }

        }

        private string cadenaCero(string valor) { 
                
            if(valor.Equals("")){
                valor = "0";
            }

            return valor;

        }

        private void cantidadTotal()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            txtCantidadT.Text = "0";

            datos.p_sql = "SELECT count(corral) as Corrales , sum(planeado) as Planeado, sum(realidad) as Realidad FROM servidas WHERE (hecho = 'S' or hecho = 'X') and racion = '" + vracion.ToString() + "' and num = " + txtServida.Text + " and id = " + txtJornada.Text; //" and  ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {

                    if (row["Corrales"].ToString() != "0")
                    {
                        txtCantidadT.Text = row["Corrales"].ToString();
                    }
                }
            }

        }

        private void timerOperacion_Tick(object sender, EventArgs e)
        {
            
            //guardarRepartotxt();

            //guardarOperaciontxt();

        }

        private void timerGPS_Tick(object sender, EventArgs e)
        {

            //leerGPSArchivo();
            //leerGPSBD();
            leerGPSPrincipal();

            if (gLatitud != 0)
            {
                ubicacionActual(gLatitud, gLongitud);
                indicaDistancia();
                picGpsDesconectado.Visible = false;
            }
            else 
            {             
                overlayCamion.Markers.Clear();
                picGpsDesconectado.Visible = true;

            }
            

        }
        private void ubicacionActual(double lat, double lng)
        {

            try
            {

                if (modomapa)
                {
                    gMapControl1.Position = new PointLatLng(lat, lng);
                }

                overlayCamion.Markers.Clear();
                agregaractualizarMarcadorLocalizaCamion(lat, lng);
            }
            catch (Exception e)
            {

            }

        }

        private void leerGPSArchivo()
        {

            double lat = 0;
            double lng = 0;

            string rutaorigen = "";

            rutaorigen = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\OperacionGPS.txt";

            //this.Cursor = Cursors.WaitCursor;

            try
            {
                if (File.Exists(rutaorigen))
                {

                    StreamReader objReader = new StreamReader(rutaorigen);
                    string sLine = "";

                    sLine = objReader.ReadLine();

                    if (sLine != null)
                    {
                        string[] lines = sLine.Split('|');

                        txtLatitud.Text = lines[0].Trim();
                        txtLongitud.Text = lines[1].Trim();

                        lat = Convert.ToDouble(txtLatitud.Text);
                        lng = Convert.ToDouble(txtLongitud.Text);
                        txtCorralUbic.Text =  corralUbicacion(dtUbictmp, lat, lng, gmindist); //lines[2].Trim();
                        //selCorral(txtCorralUbic.Text);

                    }

                    objReader.Close();

                }

                //MessageBox.Show("Listo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            }
            catch (Exception)
            {
                //MessageBox.Show("El archivo no se puede leer");
            }

            //this.Cursor = Cursors.Default;

        }


        private void leerGPSPrincipal()
        {

            double lat = Principal.instance.gLatitudgps;
            double lng = Principal.instance.gLongitudgps;

            lblLatitud.Text = lat.ToString();
            lblLongitud.Text = lng.ToString();

            lblCorral.Text = corralUbicacion(dtUbictmp, lat, lng, gmindist);
            //lblCorral.Text = corralUbicacionm(dtUbictmp, lat, lng, gmindist);

            //AUTO-DETECTA-CORRAL--------------------------------------
            //buscarCorral(lblCorral.Text);
            //--------------------------------------

            gLatitud = lat;
            gLongitud = lng;

            Console.WriteLine("LEE GPS Principal" + " " + DateTime.Now.ToString());

        }

        public string leeCorralUbicacion(double lat, double lng) 
        {
            return corralUbicacion(dtUbictmp, lat, lng, gmindist);
        }

        private void leerGPSBD()
        {
            
            Datos datos = new Datos();
            DataTable dt = new DataTable();

            double lat = 0;
            double lng = 0;

            datos.p_sql = "SELECT * FROM operacion";

            if (!datos.consultaSQL(ref dt, true))
            {
                //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {

                    //txtLatitud.Text = row["latitud"].ToString();
                    //txtLongitud.Text = row["longitud"].ToString();

                    //lat = Convert.ToDouble(txtLatitud.Text);
                    //lng = Convert.ToDouble(txtLongitud.Text);

                    //txtCorralUbic.Text = corralUbicacion(lat, lng);
                                       
                    lblLatitud.Text = row["latitud"].ToString();
                    lblLongitud.Text = row["longitud"].ToString();

                    //
                    //lblLatitud.Text = "21.920647";
                    //lblLongitud.Text = "-98.746304";
                    //

                    lat = Convert.ToDouble(lblLatitud.Text);
                    lng = Convert.ToDouble(lblLongitud.Text);

                    //lblCorral.Text = corralUbicacion(dtUbictmp, lat, lng, gmindist);
                    lblCorral.Text = corralUbicacionm(dtUbictmp, lat, lng, gmindist);

                    gLatitud = lat;
                    gLongitud = lng;

                }

                Console.WriteLine("LEE GPS BD" + " " + DateTime.Now.ToString());
            }

        }

        //private void btnRacion_Click(object sender, EventArgs e)
        //{
        //    string vjornada;
        //    vjornada = txtJornada.Text;

        //    Close();

        //    Raciones frm = new Raciones(vjornada);
        //    //frm.MdiParent = this;
        //    frm.Show();
        //}


        private string corralUbicacion(DataTable dt, double lat, double lng, double distancia)
        {

            double clat = 0;
            double clng = 0;

            double clatmin = 0;
            double clngmin = 0;

            double dist = 0;
            double distmin = 9999999;

            string corral = "0000";
            string corralmin = "0";

            try
            {

                foreach (DataRow row in dt.Rows)
                {

                    clat = Convert.ToDouble(row["lat"].ToString());
                    clng = Convert.ToDouble(row["lng"].ToString());

                    dist = getDistancia(lat, lng, clat, clng);

                    if (dist < distmin)
                    {
                        distmin = dist;
                        corralmin = row["corral"].ToString();

                        clatmin = clat;
                        clngmin = clng;

                    }

                }


                //if (dist >= 0.5 && dist <= 1)
                if (distmin <= distancia)//0.05)
                {
                    corral = corralmin;

                    mostrarLinea(lat, lng, clatmin, clngmin);

                }
                else
                {
                    corral = "0000";
                }

            }
            catch (Exception e)
            {

            }


            return corral;

        }

        private void mostrarLinea(double lat1, double lng1, double lat2, double lng2)
        {

            try
            {

                List<PointLatLng> puntos = new List<PointLatLng>();

                overlayLinea.Routes.Clear();

                puntos.Add(new PointLatLng(lat1, lng1));
                puntos.Add(new PointLatLng(lat2, lng2));

                GMapRoute PuntoLinea = new GMapRoute(puntos, "Linea");
                overlayLinea.Routes.Add(PuntoLinea);

                PuntoLinea.Stroke.Color = Color.Yellow;
                //PuntoRuta.Stroke.Color = Color.Transparent; //Color.Blue;

                //double distance = PuntoLinea.Distance * 1000;

                //distance = Math.Round(distance, 2);

                //lblDistancia.Text = distance.ToString();
                //lblDistancia.Visible = true;

            }
            catch (Exception e)
            {

            }


        }

        private string corralUbicacionm(DataTable dt, double lat, double lng, double distancia)
        {

            double clat = 0;
            double clng = 0;

            double clatmin = 0;
            double clngmin = 0;

            double dist = 0;
            double distmin = 9999999;

            string corral = "0000";
            string corralmin = "0";

            string[][] valores = new string[][]
                {

                    //new string[] { "lat1m", "lng1m"},
                    //new string[] { "lat3m", "lng3m"},

                    //new string[] { "lat1a", "lng1a"},
                    //new string[] { "lat1b", "lng1b"},
                    //new string[] { "lat3a", "lng3a"},
                    //new string[] { "lat3b", "lng3b"},

                    //------------------------------------------

                    //new string[] { "lat2", "lng2"},
                    //new string[] { "lat4", "lng4"},

                    new string[] { "lat1m", "lng1m"},
                    new string[] { "lat3m", "lng3m"},

                    new string[] { "lat1b", "lng1b"},
                    new string[] { "lat3b", "lng3b"},

                    new string[] { "lat1a", "lng1a"},
                    new string[] { "lat3a", "lng3a"},

                    //------------------------------------------

                    //new string[] { "lat", "lng"},

                };

            try
            {

                foreach (DataRow row in dt.Rows)
                {

                    foreach (string[] valor in valores)
                    {


                        clat = Convert.ToDouble(row[valor[0]].ToString());
                        clng = Convert.ToDouble(row[valor[1]].ToString());

                        dist = getDistancia(lat, lng, clat, clng);

                        if (dist < distmin)
                        {
                            distmin = dist;
                            corralmin = row["corral"].ToString();

                            clatmin = clat;
                            clngmin = clng;

                        }

                    }

                }

                if (distmin <= distancia)
                {
                    corral = corralmin;

                    //mostrarLinea(lat, lng, clatmin, clngmin);

                }
                else
                {
                    //overlayLinea.Routes.Clear();

                    corral = "0000";
                }

            }
            catch (Exception e)
            {

            }

            return corral;

        }

        public double getDistancia(double lat, double lng, double clat, double clng)
        {

            double EarthRadius = 6371;
            double distance = 0;
            double Lat = (clat - lat) * (Math.PI / 180);
            double Lon = (clng - lng) * (Math.PI / 180);
            double a = Math.Sin(Lat / 2) * Math.Sin(Lat / 2) + Math.Cos(lat * (Math.PI / 180)) * Math.Cos(clat * (Math.PI / 180)) * Math.Sin(Lon / 2) * Math.Sin(Lon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            distance = EarthRadius * c;

            return distance;

        }


        private DataRow consultaUbicacion(String corral)
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            //this.Cursor = Cursors.WaitCursor;

            datos.p_sql = "SELECT TOP 1 * FROM ubicacion WHERE corral ='" + corral + "'";

            if (!datos.consultaSQL(ref dt, true))
            {
                //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //msgErrores(datos.p_error, Constants.MSG_ERROR);
            }
            else
            {

                //dtUbictmp.Clear();

                foreach (DataRow row in dt.Rows)
                {

                    DataRow newRow = dtUbictmp.NewRow();

                    newRow["corral"] = row["corral"];
                    newRow["lat"] = row["latitud"];
                    newRow["lng"] = row["longitud"];
                    newRow["lat1"] = row["latitud1"];
                    newRow["lng1"] = row["longitud1"];
                    newRow["lat2"] = row["latitud2"];
                    newRow["lng2"] = row["longitud2"];
                    newRow["lat3"] = row["latitud3"];
                    newRow["lng3"] = row["longitud3"];
                    newRow["lat4"] = row["latitud4"];
                    newRow["lng4"] = row["longitud4"];


                    newRow["lat1m"] = (Convert.ToDouble(row["latitud1"]) + Convert.ToDouble(row["latitud2"])) / 2;
                    newRow["lng1m"] = (Convert.ToDouble(row["longitud1"]) + Convert.ToDouble(row["longitud2"])) / 2;
                    newRow["lat2m"] = (Convert.ToDouble(row["latitud2"]) + Convert.ToDouble(row["latitud3"])) / 2;
                    newRow["lng2m"] = (Convert.ToDouble(row["longitud2"]) + Convert.ToDouble(row["longitud3"])) / 2;
                    newRow["lat3m"] = (Convert.ToDouble(row["latitud3"]) + Convert.ToDouble(row["latitud4"])) / 2;
                    newRow["lng3m"] = (Convert.ToDouble(row["longitud3"]) + Convert.ToDouble(row["longitud4"])) / 2;
                    newRow["lat4m"] = (Convert.ToDouble(row["latitud4"]) + Convert.ToDouble(row["latitud1"])) / 2;
                    newRow["lng4m"] = (Convert.ToDouble(row["longitud4"]) + Convert.ToDouble(row["longitud1"])) / 2;

                    newRow["lat1a"] = (Convert.ToDouble(row["latitud1"]) + Convert.ToDouble(newRow["lat1m"])) / 2;
                    newRow["lng1a"] = (Convert.ToDouble(row["longitud1"]) + Convert.ToDouble(newRow["lng1m"])) / 2;
                    newRow["lat1b"] = (Convert.ToDouble(newRow["lat1m"]) + Convert.ToDouble(row["latitud2"])) / 2;
                    newRow["lng1b"] = (Convert.ToDouble(newRow["lng1m"]) + Convert.ToDouble(row["longitud2"])) / 2;
                    newRow["lat2a"] = (Convert.ToDouble(row["latitud2"]) + Convert.ToDouble(newRow["lat2m"])) / 2;
                    newRow["lng2a"] = (Convert.ToDouble(row["longitud2"]) + Convert.ToDouble(newRow["lng2m"])) / 2;
                    newRow["lat2b"] = (Convert.ToDouble(newRow["lat2m"]) + Convert.ToDouble(row["latitud3"])) / 2;
                    newRow["lng2b"] = (Convert.ToDouble(newRow["lng2m"]) + Convert.ToDouble(row["longitud3"])) / 2;
                    newRow["lat3a"] = (Convert.ToDouble(row["latitud3"]) + Convert.ToDouble(newRow["lat3m"])) / 2;
                    newRow["lng3a"] = (Convert.ToDouble(row["longitud3"]) + Convert.ToDouble(newRow["lng3m"])) / 2;
                    newRow["lat3b"] = (Convert.ToDouble(newRow["lat3m"]) + Convert.ToDouble(row["latitud4"])) / 2;
                    newRow["lng3b"] = (Convert.ToDouble(newRow["lng3m"]) + Convert.ToDouble(row["longitud4"])) / 2;
                    newRow["lat4a"] = (Convert.ToDouble(row["latitud4"]) + Convert.ToDouble(newRow["lat4m"])) / 2;
                    newRow["lng4a"] = (Convert.ToDouble(row["longitud4"]) + Convert.ToDouble(newRow["lng4m"])) / 2;
                    newRow["lat4b"] = (Convert.ToDouble(newRow["lat4m"]) + Convert.ToDouble(row["latitud1"])) / 2;
                    newRow["lng4b"] = (Convert.ToDouble(newRow["lng4m"]) + Convert.ToDouble(row["longitud1"])) / 2;



                    //dtUbictmp.Rows.Add(newRow);
                    return newRow;

                }

                //dataGridUbicaciones.DataSource = dtUbictmp;
                //formatoubicacionesGrid();
                //lblCantidad.Text = dataGridUbicaciones.RowCount.ToString();

            }

            //this.Cursor = Cursors.Default;

            return null;

        }

        private void consultaUbicaciones()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            operacionCorral();

            this.Cursor = Cursors.WaitCursor;

            datos.p_sql = "SELECT " +
                " ubicacion.corral ,latitud ,longitud ,latitud1 ,longitud1 ,latitud2 ,longitud2 ,latitud3 ,longitud3 ,latitud4 ,longitud4 " +
                " FROM Ubicacion " +
                " LEFT JOIN Servidas ON Ubicacion.corral = Servidas.corral " +
                " WHERE hecho = 'N' and racion = '" + vracion.ToString() + "' AND num = " + txtServida.Text + " and id = " + txtJornada.Text;
                

            if (!datos.consultaSQL(ref dt, true))
            {
                //MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //msgErrores(datos.p_error, Constants.MSG_ERROR);
            }
            else
            {

                dtUbictmp.Clear();

                foreach (DataRow row in dt.Rows)
                {

                    DataRow newRow = dtUbictmp.NewRow();

                    newRow["corral"] = row["corral"];
                    newRow["lat"] = row["latitud"];
                    newRow["lng"] = row["longitud"];
                    newRow["lat1"] = row["latitud1"];
                    newRow["lng1"] = row["longitud1"];
                    newRow["lat2"] = row["latitud2"];
                    newRow["lng2"] = row["longitud2"];
                    newRow["lat3"] = row["latitud3"];
                    newRow["lng3"] = row["longitud3"];
                    newRow["lat4"] = row["latitud4"];
                    newRow["lng4"] = row["longitud4"];


                    newRow["lat1m"] = (Convert.ToDouble(row["latitud1"]) + Convert.ToDouble(row["latitud2"])) / 2;
                    newRow["lng1m"] = (Convert.ToDouble(row["longitud1"]) + Convert.ToDouble(row["longitud2"])) / 2;
                    newRow["lat2m"] = (Convert.ToDouble(row["latitud2"]) + Convert.ToDouble(row["latitud3"])) / 2;
                    newRow["lng2m"] = (Convert.ToDouble(row["longitud2"]) + Convert.ToDouble(row["longitud3"])) / 2;
                    newRow["lat3m"] = (Convert.ToDouble(row["latitud3"]) + Convert.ToDouble(row["latitud4"])) / 2;
                    newRow["lng3m"] = (Convert.ToDouble(row["longitud3"]) + Convert.ToDouble(row["longitud4"])) / 2;
                    newRow["lat4m"] = (Convert.ToDouble(row["latitud4"]) + Convert.ToDouble(row["latitud1"])) / 2;
                    newRow["lng4m"] = (Convert.ToDouble(row["longitud4"]) + Convert.ToDouble(row["longitud1"])) / 2;

                    newRow["lat1a"] = (Convert.ToDouble(row["latitud1"]) + Convert.ToDouble(newRow["lat1m"])) / 2;
                    newRow["lng1a"] = (Convert.ToDouble(row["longitud1"]) + Convert.ToDouble(newRow["lng1m"])) / 2;
                    newRow["lat1b"] = (Convert.ToDouble(newRow["lat1m"]) + Convert.ToDouble(row["latitud2"])) / 2;
                    newRow["lng1b"] = (Convert.ToDouble(newRow["lng1m"]) + Convert.ToDouble(row["longitud2"])) / 2;
                    newRow["lat2a"] = (Convert.ToDouble(row["latitud2"]) + Convert.ToDouble(newRow["lat2m"])) / 2;
                    newRow["lng2a"] = (Convert.ToDouble(row["longitud2"]) + Convert.ToDouble(newRow["lng2m"])) / 2;
                    newRow["lat2b"] = (Convert.ToDouble(newRow["lat2m"]) + Convert.ToDouble(row["latitud3"])) / 2;
                    newRow["lng2b"] = (Convert.ToDouble(newRow["lng2m"]) + Convert.ToDouble(row["longitud3"])) / 2;
                    newRow["lat3a"] = (Convert.ToDouble(row["latitud3"]) + Convert.ToDouble(newRow["lat3m"])) / 2;
                    newRow["lng3a"] = (Convert.ToDouble(row["longitud3"]) + Convert.ToDouble(newRow["lng3m"])) / 2;
                    newRow["lat3b"] = (Convert.ToDouble(newRow["lat3m"]) + Convert.ToDouble(row["latitud4"])) / 2;
                    newRow["lng3b"] = (Convert.ToDouble(newRow["lng3m"]) + Convert.ToDouble(row["longitud4"])) / 2;
                    newRow["lat4a"] = (Convert.ToDouble(row["latitud4"]) + Convert.ToDouble(newRow["lat4m"])) / 2;
                    newRow["lng4a"] = (Convert.ToDouble(row["longitud4"]) + Convert.ToDouble(newRow["lng4m"])) / 2;
                    newRow["lat4b"] = (Convert.ToDouble(newRow["lat4m"]) + Convert.ToDouble(row["latitud1"])) / 2;
                    newRow["lng4b"] = (Convert.ToDouble(newRow["lng4m"]) + Convert.ToDouble(row["longitud1"])) / 2;







                    dtUbictmp.Rows.Add(newRow);

                }

                //dataGridUbicaciones.DataSource = dtUbictmp;
                //formatoubicacionesGrid();
                //lblCantidad.Text = dataGridUbicaciones.RowCount.ToString();

            }

            this.Cursor = Cursors.Default;

        }

        private void selCorral(String searchValue)
        {

            if (DGServidas.RowCount != 0)
            {
                Datos datos = new Datos();
                DataTable dt = new DataTable();

                 //int filas = 0;
                int rowIndex = -1;

                //datos.p_sql = "SELECT idd FROM operacion";

                //if (!datos.consultaSQL(ref dt, true))
                //{
                //    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //}
                //else
                //{
                //    foreach (DataRow row in dt.Rows)
                //    {
                //        searchValue = row["IdD"].ToString();
                //    }
                //}

                if (searchValue != "0")
                {

                    foreach (DataGridViewRow row in DGServidas.Rows)
                    {
                        if (row.Cells["Corral"].Value.ToString() == searchValue)
                        {
                            rowIndex = row.Index;
                            break;
                        }
                    }

                    if (rowIndex != -1)
                    {
                        DGServidas.Rows[rowIndex].Selected = true;

                        txtIdD.Text = (string)DGServidas.CurrentRow.Cells["IdD"].Value;
           
                        DGServidas.FirstDisplayedScrollingRowIndex = rowIndex;

                        filaDatos(txtIdD.Text);
                    }

                }



            }

        }

        private void timerH_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss tt");

            lblBateria.Text = Principal.instance.lblBateria.Text;
            pbBateria.Value = Principal.instance.pbBateria.Value;
            lblBateriaTiempo.Text = Principal.instance.lblBateriaTiempo.Text;

            btnConectar.Visible = Principal.instance.btnConectarGps.Visible;

        }
        
        private void ubicServir(DataTable dt)
        {

            Boolean vencontro = false;
            List<PointLatLng> puntos = new List<PointLatLng>();

            //List<PointLatLng> puntos = new List<PointLatLng>();

            //Datos datos = new Datos();
            //DataTable dt = new DataTable();

            //this.Cursor = Cursors.WaitCursor;

            //datos.p_sql = "SELECT * FROM ubicacion";

            //if (!datos.consultaSQL(ref dt, true))
            //{

            //}
            //else
            //{

            overlayCeldas.Polygons.Clear();
            overlayRutaP.Routes.Clear();
            //overlaylabelCeldas.Markers.Clear();

            foreach (DataRow row in dt.Rows)
            {

                vencontro = false;

                foreach (DataGridViewRow rowg in DGServidas.Rows)
                {
                    if (rowg.Cells["Corral"].Value.ToString().Equals (row["corral"].ToString()))
                    {
                        vencontro = true;
                        break;
                    }

                }

                if (vencontro)
                {

                    //agregaractualizarMarcadorRecorrer(
                    //    row["corral"].ToString(),
                    //    Convert.ToDouble(row["lat"]),
                    //    Convert.ToDouble(row["lng"]));


                    agregaractualizarMarcadorCelda(
                          row["corral"].ToString(),
                          Convert.ToDouble(row["lat"]),
                          Convert.ToDouble(row["lng"]),
                          Convert.ToDouble(row["lat1"]),
                          Convert.ToDouble(row["lng1"]),
                          Convert.ToDouble(row["lat2"]),
                          Convert.ToDouble(row["lng2"]),
                          Convert.ToDouble(row["lat3"]),
                          Convert.ToDouble(row["lng3"]),
                          Convert.ToDouble(row["lat4"]),
                          Convert.ToDouble(row["lng4"]));

                    if (!row["lat"].Equals("0")) 
                    { 
                        puntos.Add(new PointLatLng(Convert.ToDouble(row["lat"]), Convert.ToDouble(row["lng"])));                    
                    }



                }

                corralActividadNombre();

                GMapRoute PuntoRutaP = new GMapRoute(puntos, "Ruta");
                overlayRutaP.Routes.Add(PuntoRutaP);
                PuntoRutaP.Stroke.Color = Color.Yellow;
                PuntoRutaP.Stroke.Width = 1;
                //PuntoRuta.Stroke.Color = Color.Transparent;

            }

            //}

        }

        private void corralActividadNombre()
        {
            if (overlayCeldas != null)
            {
                overlaylabelCeldas.Markers.Clear();

                for (int i = 0; i < overlayCeldas.Polygons.Count; i++)
                {
                    corralActividadNombres(overlayCeldas.Polygons.ElementAt(i).Name, overlayCeldas.Polygons.ElementAt(i).Points, Color.White);
                }

            }

        }

        private void corralActividadNombres(string nombre, List<PointLatLng> points, Color color)
        {

            int tamano;
            int ancho = 40;
            int alto = 23;

            switch (gMapControl1.Zoom)//(Convert.ToInt32(trackBar1.Value))
            {
                //case 19:
                //    tamano = 20;
                //    ancho += 32;
                //    alto += 27;
                //    break;
                //case 18:
                //    tamano = 14;
                //    ancho += 12;
                //    alto += 11;
                //    break;
                case 17:
                    tamano = 10;
                    //ancho = 40;
                    //alto = 23;
                    break;
                case 16:
                    tamano = 10;
                    //ancho = 27;
                    //alto = 15;
                    break;
                //case 15:
                //    tamano = 1;
                //    ancho = 40;
                //    alto = 23;
                //    break;
                default:
                    tamano = 10;
                    //ancho = 40;
                    //alto = 23;
                    break;
            }

            Bitmap bmpmarker = new Bitmap(ancho, alto);
            Graphics graphicImage = Graphics.FromImage(bmpmarker);
            graphicImage.SmoothingMode = SmoothingMode.AntiAlias;
            graphicImage.DrawString(nombre, new Font("Cambria", tamano, FontStyle.Bold), SystemBrushes.WindowText, new Point(3, 10));
            graphicImage.Dispose();

            PointLatLng centerPoint = GetZoneCenter(points);

            centerPoint.Lat -= 0.000000;
            centerPoint.Lng += 0.000000;

            markerlabelCelda = new GMarkerGoogle(centerPoint, bmpmarker);
            overlaylabelCeldas.Markers.Add(markerlabelCelda);

        }

        private PointLatLng GetZoneCenter(List<PointLatLng> vertexes)
        {

            PointLatLng centerPoint = new PointLatLng();
            int sum = 0;
            double lat = 0;
            double lng = 0;

            foreach (var point in vertexes)
            {
                sum += 1;
                lat += point.Lat;
                lng += point.Lng;
            }

            lat = lat / sum;
            lng = lng / sum;

            centerPoint.Lat = lat;
            centerPoint.Lng = lng;

            return centerPoint;

        }

        private void agregaractualizarMarcadorRecorrer(string corral, double lat, double lng)
        {

            Bitmap bmpmarker = new Bitmap(this.imageList1.Images[3]);

            markerRecorrer = new GMarkerGoogle(new PointLatLng(lat, lng), bmpmarker); // GMarkerGoogleType.blue);// .purple_small);

            //markerRecorrer = new GMarkerGoogle(new PointLatLng(lat, lng), GMarkerGoogleType.blue);// .purple_small);
            markerRecorrer.ToolTipMode = MarkerTooltipMode.Always; //MarkerTooltipMode.OnMouseOver;
            markerRecorrer.ToolTipText = string.Format("{0}", corral);
            overlayRecorrer.Markers.Add(markerRecorrer);

        }

        private void agregaractualizarMarcadorLocalizaCamion(double lat, double lng)
        {

            //string horaAct = DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss");
            //DateTime hora1 = Convert.ToDateTime(hora);
            //DateTime hora2 = Convert.ToDateTime(horaAct);
            //TimeSpan demora = hora2.Subtract(hora1);
            Image vimg;

            //if (demora.TotalMinutes <= Convert.ToDouble(inactividad))
            //{
             vimg = this.imageList1.Images[0];
            //}
            //else
            //{
                //vimg = this.imageList1.Images[2];
            //}

            Bitmap bmpmarker = new Bitmap(vimg); //imagen camion

            //SolidBrush drawBrush = new SolidBrush(Color.Black); //color numero camion

            //Graphics graphicImage = Graphics.FromImage(bmpmarker);
            //graphicImage.SmoothingMode = SmoothingMode.AntiAlias;
            //Point punto = new Point(2, 6);
            //graphicImage.DrawString(id, new Font("Arial", 11, FontStyle.Bold), drawBrush, punto);
            //graphicImage.Dispose();

            markerCamion = new GMarkerGoogle(new PointLatLng(lat, lng), bmpmarker);
            markerCamion.ToolTipMode = MarkerTooltipMode.OnMouseOver;//.Always;

            //markerCamion.ToolTipText = string.Format("{0} \n HORA:{2}", name, velocidad, hora);
            //markerCamion.ToolTipText = string.Format("{0} \n VELOCIDAD: {1} \n HORA:{2}", name, velocidad, hora);

            overlayCamion.Markers.Add(markerCamion);
        }

        private void indicaDistancia()
        {

            //llenaListagrid();

            try
            {

                double vLatitud = 0;
                double vLongitud = 0;

                List<PointLatLng> puntos = new List<PointLatLng>();

                overlayRuta.Routes.Clear();

                if (dtUbictmpgrid.Rows.Count == 0)
                {
                    vLatitud = gLatitud;
                    vLongitud = gLongitud;

                    lblCorrarCercano.Text = "0";
                }
                else
                {

                    String vcorral = corralUbicacion(dtUbictmpgrid, gLatitud, gLongitud, 99999);

                    DataRow[] rows = dtUbictmp.Select("corral = '" + vcorral + "'");

                    lblCorrarCercano.Text = vcorral;

                    foreach (DataRow row2 in rows)
                    {
                        vLatitud = Convert.ToDouble(row2["lat"]);
                        vLongitud = Convert.ToDouble(row2["lng"]);
                    }

                }

                puntos.Add(new PointLatLng(vLatitud, vLongitud));
                puntos.Add(new PointLatLng(gLatitud, gLongitud));//camion


                GMapRoute PuntoRuta = new GMapRoute(puntos, "Ruta");
                overlayRuta.Routes.Add(PuntoRuta);

                PuntoRuta.Stroke.Color = Color.Yellow;
                //PuntoRuta.Stroke.Color = Color.Transparent; //Color.Blue;
                PuntoRuta.Stroke.Width = 1;

                double distance = PuntoRuta.Distance * 1000;

                distance = Math.Round(distance, 2);

                lblDistancia.Text = distance.ToString();
                //lblDistancia.Visible = true;

            }
            catch (Exception e)
            {

            }


        }

        private void agregaractualizarMarcadorCelda(string corral, double lat, double lng, double lat1, double lng1, double lat2, double lng2, double lat3, double lng3, double lat4, double lng4)
        {

            _points.Clear();
            _points.Add(new PointLatLng(lat1, lng1));
            _points.Add(new PointLatLng(lat2, lng2));
            _points.Add(new PointLatLng(lat3, lng3));
            _points.Add(new PointLatLng(lat4, lng4));

            corralActividad(corral, _points, Color.White);
            //corralActividadNombre(id, _points, Color.White);

        }

        private void corralActividad(string nombre, List<PointLatLng> points, Color color)
        {

            polygonCelda = new GMapPolygon(points, nombre)
            {
                Stroke = new Pen(Color.Black, 2),
                Fill = new SolidBrush(color), //Brushes.Transparent,
                Tag = nombre,
            };

            overlayCeldas.Polygons.Add(polygonCelda);


        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            modomapa = !modomapa;

            if (modomapa)
            {
                toolStripButton1.Text = "NAVEGADOR";
            }
            else
            {
                toolStripButton1.Text = "EXPLORADOR";
            }

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (gMapControl1.Zoom <= 19)
            {
                gMapControl1.Zoom += 1;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (gMapControl1.Zoom >= 15)
            {
                gMapControl1.Zoom -= 1;
            }
        }


        private void grafico(Chart chart)
        {

            chart.Series[0].IsValueShownAsLabel = true;
            chart.Series[0].IsVisibleInLegend = false;
            chart.Series[0].BorderColor = Color.Black;
            chart.Series[0].LabelBackColor = Color.Transparent;
            chart.Series[0].LabelBorderColor = Color.Transparent;
            chart.Series[0].LabelForeColor = Color.Black;
            chart.Series[0].Points.Clear();

            Color[] colores = {
                     Color.Green,
                     Color.Yellow,

                     Color.Red,
                     Color.Orange,

                     Color.White,
                     Color.Gray,
                     Color.Blue, 
                     Color.Purple,
    
            };

            int vPlan = Int32.Parse(txtPlan.Text.Replace(",",""));
            int vServido = Int32.Parse(txtServido.Text.Replace(",", ""));
            int vFalta = Int32.Parse(txtFalta.Text.Replace(",", ""));
            
            //int vfalta = vplan - vServido;

            double[] valores = { vPlan, vFalta, 0, 0, 0, 0, 0, 0 };

            //int totalcapas = overlayCeldas.Polygons.Count;

            //for (int i = 0; i < totalcapas; i++)
            //{
            //    for (int j = 0; j < valores.Count(); j++)
            //    {
            //        if (overlayCeldas.Polygons.ElementAt(i).Tag.Equals(colores[j].Name)) { valores[j]++; }
            //    }
            //}


            //for (int i = 0; i < 2; i++)
            //{
            //    valores[i] = Math.Round(valores[i] / totalcapas * 100, 1);
            //    chart.Series[0].Points.AddXY(i.ToString(), valores[i].ToString());
            //    chart.Series[0].Points[i].Color = colores[i];
            //}


            

            //valores[0] = Math.Round(valores[0] / vplan * 100, 1);
            valores[0] = Math.Round(valores[0], 1);
            chart.Series[0].Points.AddXY(0.ToString(), valores[0].ToString());
            chart.Series[0].Points[0].Color = colores[0];

            //valores[1] = Math.Round(valores[1] / vplan * 100, 1);
            valores[1] = Math.Round(valores[1], 1);
            chart.Series[0].Points.AddXY(1.ToString(), valores[1].ToString());
            chart.Series[0].Points[1].Color = colores[1];


        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            gMapControl1.Bearing += 90;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Senga.Captura frm = new Senga.Captura(Convert.ToInt32(txtJornada.Text),2);
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


        //private void trackBar1_Scroll(object sender, EventArgs e)
        //{
        //    //gMapControl1.Zoom = trackBar1.Value;
        //}


        private void agregaDigito(string digito)
        {
            string valor = txtCaptura.Text;

            if (valor.Length < txtCaptura.MaxLength)
            {
                txtCaptura.Text = valor + digito;
            }

            sonidoBoton();

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            buscarCorral(txtCaptura.Text);
            txtCaptura.Text = "";
        }

        private void buscarCorral(string captura) 
        {

            String searchValue = captura;
            int rowIndex = -1;
            foreach (DataGridViewRow row in DGServidas.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(searchValue))
                {
                    rowIndex = row.Index;
                    break;
                }
            }

            if (rowIndex != -1)
            {
                DGServidas.CurrentCell = DGServidas.Rows[rowIndex].Cells[0];

                if (rowIndex > 5)
                {
                    DGServidas.FirstDisplayedScrollingRowIndex = rowIndex;
                }

                txtIdD.Text = DGServidas.Rows[rowIndex].Cells["IdD"].Value.ToString();

                filaDatos(txtIdD.Text);

                ubicaCorralMapa();

                filaAct();
                
            }

        }

        private void btnUno_Click(object sender, EventArgs e)
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

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            txtCaptura.Text = cortarCadena(1, txtCaptura.Text);
        }

        private void sonidoBoton() 
        {
            //Console.Beep(440, 50);
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

        private void btnCargar_Click(object sender, EventArgs e)
        {

            if (DGServidas.RowCount != 0)
            {

                //rowIndex = DGServidas.CurrentRow.Index;

                //if (txtCorrales.Text == txtHechos.Text)
                //{
                //    //MessageBox.Show("Racion Servida Completada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //    Comun.mensajeVer("Racion Servida Completada", Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);

                //}
                //else
                //{

                    Datos datos = new Datos();
                    DataTable dt = new DataTable();

                    filaAct();

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

                //}


            }

        }

        private void btnSimularGps_Click(object sender, EventArgs e)
        {

            if (!txtSimularLatLng.Text.Equals(""))
            {
                Principal.instance.txtSimularLatLng.Text = txtSimularLatLng.Text;
                Principal.instance.btnSimularGps.PerformClick();
            }

        }


        private void gMapControl1_MouseMove(object sender, MouseEventArgs e)
        {

            base.OnMouseMove(e);

            double lat = Math.Round(gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat, 6);
            double lng = Math.Round(gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng, 6);

            gCoordenadas = Convert.ToString(lat) + "," + Convert.ToString(lng);

            corralUbicacion(dtUbictmp, lat, lng, gmindist);





        }

        private void copiarCoordenadaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Clipboard.SetText(gCoordenadas); //To copy your text to your clipboard
            Clipboard.GetText(); //To get your text from your clipboard
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            Principal.instance.btnConectarGps.PerformClick();
        }
    }

}