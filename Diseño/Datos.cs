using System;
using System.Collections.Generic;
using System.Text;

using System.Configuration;
using System.Data.OleDb;
using System.Data;
using Microsoft.Office.Interop.Access.Dao;
using System.IO; 



namespace Senga
{
    class Datos
    {
        
        #region "Atributos"

        private string perror;
        private string psql;
        private List<string> psqllist;
        private long pidd;

        //private string pfecha;
        //private int pnum;
        //private string pcorral;
        //private string pracion;
        //private int pcamion;
        //private int pcabezas;
        //private int pplaneado;
        //private int prealidad;
        //private string phora;
        

        #endregion

        #region "Metodos Publicos"

        public Datos()
        {

            perror = "";
            psql = "";
            psqllist = null;
            pidd = 0;
            
            //pfecha = "";
            //pnum = 0;
            //pcamion = 0;
            //pcorral = "";
            //pracion = "";
            //pcabezas = 0;
            //pplaneado = 0;
            //prealidad = 0;
            //phora = "";
            

        }

        public string coneccion()
        {
            Senga.Properties.Settings setting = Senga.Properties.Settings.Default;
            return setting.datosConnectionStringAccessaccdb;
        }

        //public bool insertServidas()
        //{
        //    DataTable dt = new DataTable();
        //    return iniReg(ref dt, "insert into servidas (fecha,num,corral,racion,cabezas,planeado,realidad,hora) values('" + pfecha + "'," + pnum + ",'" + pcorral + "','" + pracion + "'," + pcabezas + "," + pplaneado + "," + prealidad + ",'" + phora + "')", false);
        //}
        //"UPDATE servidas SET " + columna + " = " + dato + "  WHERE idd = " + idd
        //public bool actualizaServidasIdD()
        //{
        //    DataTable dt = new DataTable();
        //    return iniReg(ref dt, psql, false);
        //}

        public bool consultaSQL(ref DataTable dt, bool val) 
        {
            return iniReg(ref dt, psql, val);
        }

        public bool consultaSQLs(ref DataTable dt)
        {
            return iniRegs(ref dt, psqllist);
        }

        public bool iniReg(ref DataTable dtx, string sql, bool exe) 
        {

            try
            {      
                
                OleDbConnection con = new OleDbConnection(coneccion());
                con.Open();
                
                OleDbCommand cmd = new OleDbCommand(sql, con);

                if (exe)
                {

                    OleDbDataAdapter da = new OleDbDataAdapter(sql, con);
                    DataTable dt = new DataTable();

                    da.Fill(dt);
                    dtx = dt;
                    
                    dt.Dispose();
                    da.Dispose();

                }
                else 
                { 
                    cmd.ExecuteNonQuery();           
                }
                        
                cmd.Dispose();
                con.Close();
                return true;

            }
            catch (Exception ex)
            {
                perror = ex.Message;
                return false;
            }

        }

        public bool iniRegs(ref DataTable dtx, List<string> sqls)
        {

            try
            {

                OleDbConnection con = new OleDbConnection(coneccion());
                con.Open();

                foreach (string sql in sqls)
                {
                    OleDbCommand cmd = new OleDbCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }

                con.Close();
                return true;

            }
            catch (OleDbException ex)
            {
                perror = ex.Message;
                return false;
            }

        }

        public List<string> ObtenerTablas()
        {

            DataTable userTables = null;
            using (OleDbConnection con = new OleDbConnection(coneccion()))
            {

                string[] restrictions = new string[4];
                restrictions[3] = "Table";

                con.Open();

                userTables = con.GetSchema("Tables", restrictions);

            }

            List<string> tableNames = new List<string>();
            for (int i = 0; i < userTables.Rows.Count; i++)
                tableNames.Add(userTables.Rows[i][2].ToString());

            return tableNames;

        }

        public bool compact() 
        {
                       
             try
             {
                 string File_Path;
                 string compact_file;
                 File_Path = AppDomain.CurrentDomain.BaseDirectory + "datos.accdb";
                 compact_file = AppDomain.CurrentDomain.BaseDirectory + "datos1.accdb";

                 if (File.Exists(File_Path))
                 {
                     
                     DBEngine db = new DBEngine();                    

                     db.CompactDatabase(File_Path, compact_file, null, null, null); // ";pwd=''"
                 }
                 if (File.Exists(compact_file))
                 {
                     File.Delete(File_Path);
                     File.Move(compact_file, File_Path);
                 }

                 return true;   
             }
             catch (Exception ex)
             {
                 perror = ex.Message;
                 return false;
             }

        }
        
        #endregion


        #region "Propiedades"

        public string p_error
        {
            get { return perror; }
            set { perror = value; }
        }
        public string p_sql
        {
            get { return psql; }
            set { psql = value; }
        }
        public List<string> p_sql_list
        {
            get { return psqllist; }
            set { psqllist = value; }
        }
        public long p_idd
        {
            get { return pidd; }
            set { pidd = value; }
        }



        //public string p_fecha
        //{
        //    get { return pfecha; }
        //    set { pfecha = value; }
        //}
        //public int p_num
        //{
        //    get { return pnum; }
        //    set { pnum = value; }
        //}
        //public string p_corral
        //{
        //    get { return pcorral; }
        //    set { pcorral = value; }
        //}
        //public string p_racion
        //{
        //    get { return pracion; }
        //    set { pracion = value; }
        //}
        //public int p_camion
        //{
        //    get { return pcamion; }
        //    set { pcamion = value; }
        //}
        //public int p_cabezas
        //{
        //    get { return pcabezas; }
        //    set { pcabezas = value; }
        //}
        //public int p_planeado
        //{
        //    get { return pplaneado; }
        //    set { pplaneado = value; }
        //}
        //public int p_realidad
        //{
        //    get { return prealidad; }
        //    set { prealidad = value; }
        //}
        //public string p_hora
        //{
        //    get { return phora; }
        //    set { phora = value; }
        //}

        #endregion

    }
}
