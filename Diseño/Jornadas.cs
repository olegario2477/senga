using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Senga
{
    public partial class Jornadas : Form
    {
        bool activado = false;
        private int rowIndex = 0;

        public Jornadas()
        {
            InitializeComponent();
 
        }
        
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Jornadas_Load(object sender, EventArgs e)
        {
            propiedadesGrid();
            seleccionarFila();

            posicionbotones();

            activado = true;
            formatoGrid();
            activar();
        }

        private void Jornadas_Activated(object sender, EventArgs e) 
        {

            activar();
         
        }

        private void activar() 
        { 

           if (activado)
            {
                operacionJornada();

                limpiarJornada();
                refrescaGrid();


                if (DGJornadas.RowCount == 0)
                {
                    limpiarJornada();
                }
                else
                {
                    //txtIdD.Text = (string)DGServidas.CurrentRow.Cells["IdD"].Value;
                    //filaDatos(txtIdD.Text);   

                }

                //seleccionarFila();
                //barra();
            
            }
        
        }

        private void posicionbotones()
        {
            btnCerrar.Top = 0;
            btnAbrir.Top = 0;
            btnEliminar.Top = 0;
            btnNuevo.Top = 0;
            btnReportes.Top = 0;

            btnCerrar.Left = this.Width - btnCerrar.Width;
            btnAbrir.Left = btnCerrar.Left - btnAbrir.Width;
            btnEliminar.Left = btnAbrir.Left - btnEliminar.Width;
            btnNuevo.Left = btnEliminar.Left - btnNuevo.Width;
            btnReportes.Left = btnNuevo.Left - btnReportes.Width;


            DGJornadas.Left = 0;
            DGJornadas.Width = this.Width;


        }

        private void limpiarJornada()
        {

            //txtCorral.Text = "";
            //txtRacion.Text = "";
            //txtCabezas.Text = "";
            txtId.Text = "";

            //botonesHabilita(false);

        }

        private void propiedadesGrid()
        {

            DGJornadas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGJornadas.ReadOnly = true;
            DGJornadas.AllowUserToAddRows = false;
            DGJornadas.AllowUserToDeleteRows = false;
            DGJornadas.AllowUserToOrderColumns = false;
            DGJornadas.AllowUserToResizeColumns = false;
            DGJornadas.AllowUserToResizeRows = false;
            DGJornadas.MultiSelect = false;
            //DGRaciones.ColumnHeadersVisible = false;
            DGJornadas.RowHeadersVisible = false;
            DGJornadas.CellBorderStyle = DataGridViewCellBorderStyle.None;
            DGJornadas.DefaultCellStyle.Font = new Font("Arial", 20.00F, FontStyle.Bold);
            //DGRaciones.RowTemplate.Height = 50;




        }

        private void operacionJornada()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            datos.p_sql = "SELECT TOP 1 * FROM operacion";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    txtId.Text = row["Jornada"].ToString();
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
            //    }
            //}

            //datos.p_sql = "SELECT TOP 1 * FROM operacion";

            //if (!datos.consultaSQL(ref dt, true))
            //{
            //    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //}
            //else
            //{
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        txtServida.Text = row["Servida"].ToString();
            //    }
            //}

        }

        private void refrescaGrid()
        {
            //formatoGrid();
            datosGrid();

            if (DGJornadas.RowCount > 0)
            {
                DGJornadas.CurrentCell = DGJornadas.Rows[rowIndex].Cells[0];
                DGJornadas.Rows[rowIndex].Selected = true;
            }

            refreshJornadas();

        }



        private void formatoGrid()
        {

            DataTable table = new DataTable();


            table.Columns.Add("Jornada", typeof(string));
            table.Columns.Add("Fecha", typeof(string));
            table.Columns.Add("Hora", typeof(string));

            DGJornadas.DataSource = table;

            for (int i = 0; i < table.Columns.Count; i++)
            {
                DGJornadas.Columns[i].Width = 200;
                DGJornadas.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGJornadas.Columns[i].HeaderCell.Style.Font = new Font("Tahoma", 15.00F, FontStyle.Bold);
                DGJornadas.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                DGJornadas.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }





        }

        private void datosGrid()
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();
            DataTable table = (DataTable)(DGJornadas.DataSource);

            table.Rows.Clear();

            string id = "0";
            bool primero = true;

            datos.p_sql = "SELECT Id, Fecha, Hora FROM jornadas";

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    table.Rows.Add(row["Id"].ToString(), row["Fecha"].ToString().Substring(0, 10), row["Hora"].ToString());

                    if (row["Id"].ToString() == "0" && primero)
                        if (primero)
                        {
                            id = row["IdD"].ToString();
                            primero = false;
                        }
                }


                datos.p_sql = "UPDATE operacion SET jornada = " + id.ToString();

                if (!datos.consultaSQL(ref dt, false))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

            }


        }

        private void seleccionarFila()
        {

            if (DGJornadas.RowCount != 0)
            {
                Datos datos = new Datos();
                DataTable dt = new DataTable();

                string searchValue = "0";
                //int filas = 0;
                int rowIndex = -1;

                datos.p_sql = "SELECT Jornada FROM operacion";

                if (!datos.consultaSQL(ref dt, true))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        searchValue = row["Jornada"].ToString();
                    }
                }

                if (searchValue != "0")
                {

                    foreach (DataGridViewRow row in DGJornadas.Rows)
                    {
                        if (row.Cells["Jornada"].Value.ToString() == searchValue)
                        {
                            rowIndex = row.Index;
                            break;
                        }
                    }

                    if (rowIndex != -1)
                    {
                        DGJornadas.Rows[rowIndex].Selected = true;

                        txtId.Text = searchValue;

                        filaDatos(txtId.Text);
                    }

                }



            }
        }

        private void filaDatos(string id)
        {

            Datos datos = new Datos();
            DataTable dt = new DataTable();

            datos.p_sql = "SELECT * FROM jornadas WHERE id =" + id.ToString();

            if (!datos.consultaSQL(ref dt, true))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {

                foreach (DataRow row in dt.Rows)
                {
                    //txtCorral.Text = row["Corral"].ToString();
                    txtFecha.Text = row["Fecha"].ToString();
                    //txtCabezas.Text = row["Cabezas"].ToString();
                }

                datos.p_sql = "UPDATE operacion SET jornada =" + id.ToString();

                if (!datos.consultaSQL(ref dt, false))
                {
                    MessageBox.Show(datos.p_error);
                }



                //botonesHabilita(true);
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            if (DGJornadas.RowCount == 0)
            {
                //MessageBox.Show("No hay Jornadas", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Comun.mensajeVer("No hay Jornadas", Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
            }
            else
            {

                Datos datos = new Datos();
                DataTable dt = new DataTable();

                int valorf = 0;

                datos.p_sql = "SELECT count(*) as Jornada FROM jornadas WHERE id = " + txtId.Text + " and ( day(fecha) = " + DateTime.Now.ToString("dd") + " and  month(fecha) = " + DateTime.Now.ToString("MM") + " and year(fecha) = " + DateTime.Now.ToString("yyyy") + " ) ";

                if (!datos.consultaSQL(ref dt, true))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {

                        if (row["Jornada"].ToString() != "0")
                        {
                            valorf = Convert.ToInt32(row["Jornada"].ToString());
                        }
                    }
                }


                if (valorf != 0)
                {

                    //MessageBox.Show("No se permite con fecha de Hoy", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    Comun.mensajeVer("No se permite con fecha de Hoy", Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
                }

                else
                {
                    
                    int valor = 0;

                    datos.p_sql = "SELECT count(*) as Raciones FROM raciones WHERE guardar = 'N' and id = " + txtId.Text;

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
                                valor = Convert.ToInt32(row["Raciones"].ToString());
                            }
                        }
                    }

                    if (valor != 0)
                    {

                        //MessageBox.Show("Existen Movimientos Sin Guardar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        Comun.mensajeVer("Existen Movimientos Sin Guardar", Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
                    }

                    else
                    {

                        string id = txtId.Text;

                        if (Comun.mensajeVer("¿ Desea Quitar Toda Informacion Raciones y Corrales Permanentemente ?", Constants.MSG_BOTON_SINO, Constants.MSG_IMG_WARNING) == Constants.MSG_BOTON_SI)
                        //if (MessageBox.Show("¿ Desea Quitar Toda Informacion Raciones y Corrales Permanentemente ?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        { 
                            
                            datos.p_sql = "DELETE FROM jornadas WHERE Id = " + id.ToString();

                            if (!datos.consultaSQL(ref dt, false))
                            {
                                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }

                            datos.p_sql = "DELETE FROM servidas WHERE Id = " + id.ToString();

                            if (!datos.consultaSQL(ref dt, false))
                            {
                                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }

                            datos.p_sql = "DELETE FROM cargas WHERE Id = " + id.ToString();

                            if (!datos.consultaSQL(ref dt, false))
                            {
                                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }

                            datos.p_sql = "DELETE FROM raciones WHERE Id = " + id.ToString();

                            if (!datos.consultaSQL(ref dt, false))
                            {
                                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            else
                            {
                                //MessageBox.Show("Listo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Comun.mensajeVer("Listo", Constants.MSG_BOTON_OK, Constants.MSG_IMG_OK);
                            }

                        }
                    }
                }
            }

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            
            Datos datos = new Datos();
            DataTable dt = new DataTable();

            //string searchValue = "0";
            //int filas = 0;
            int rowIndex = -1;

            datos.p_sql = "insert into jornadas (fecha,Hora) values('" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss") + "')";

            if (!datos.consultaSQL(ref dt, false))
            {
                MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else 
            {
                //MessageBox.Show("Listo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refrescaGrid();
                
                rowIndex = DGJornadas.RowCount - 1;
                DGJornadas.Rows[rowIndex].Selected = true;
                DGJornadas.FirstDisplayedScrollingRowIndex = rowIndex;

                txtId.Text = DGJornadas.Rows[rowIndex].Cells["Jornada"].Value.ToString(); ;
                //searchValue = DGJornadas.Rows.Cells["Jornada"].Value.ToString();
                //txtId.Text = searchValue;
                filaDatos(txtId.Text);
                //searchValue ="20";
                
                //if (searchValue != "0")
                //{

                //    foreach (DataGridViewRow row in DGJornadas.Rows)
                //    {
                //        if (row.Cells["Jornada"].Value.ToString() == searchValue)
                //        {
                //            rowIndex = row.Index;
                //            break;
                //        }
                //    }

                //    if (rowIndex != -1)
                //    {
                //        DGJornadas.Rows[rowIndex].Selected = true;
                //        txtId.Text = searchValue;
                //        DGJornadas.FirstDisplayedScrollingRowIndex = rowIndex;
                //        filaDatos(txtId.Text);
                //    }

                //}



            }
        }

        private void DGJornadas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (DGJornadas.CurrentRow != null)
            //{
            //    txtId.Text = (string)DGJornadas.CurrentRow.Cells["Jornada"].Value;

            //    filaDatos(txtId.Text);
            //}
        }

        private void DGJornadas_SelectionChanged(object sender, EventArgs e)
        {

            refreshJornadas();

        }

        void refreshJornadas() 
        { 

            if (DGJornadas.CurrentRow != null)
            {
                txtId.Text = (string)DGJornadas.CurrentRow.Cells["Jornada"].Value;

                filaDatos(txtId.Text);
            }        
        
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {

            if (DGJornadas.RowCount == 0)
            {
                //MessageBox.Show("No hay Jornadas", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Comun.mensajeVer("No hay Jornadas", Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
            }
            else
            {
                Datos datos = new Datos();
                DataTable dt = new DataTable();

                datos.p_sql = "UPDATE operacion SET jornada = '" + txtId.Text + "'";

                if (!datos.consultaSQL(ref dt, true))
                {
                    MessageBox.Show(datos.p_error, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                string vid;
                vid = txtId.Text;

            

                Raciones frm = new Raciones(vid);
                //frm.MdiParent = this;
                frm.Show();

                Close();

            }

        }

        private void btnReportes_Click(object sender, EventArgs e)
        {

            string vid;


            if (DGJornadas.RowCount == 0)
            {
                Comun.mensajeVer("No hay Jornadas", Constants.MSG_BOTON_OK, Constants.MSG_IMG_STOP);
            }
            else 
            {
                rowIndex = DGJornadas.CurrentRow.Index;

                vid = txtId.Text;
                
                Reportes frm = new Reportes(vid);
                frm.Show();            
            }
                    
        }





    }
}