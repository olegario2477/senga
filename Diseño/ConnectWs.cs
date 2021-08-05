using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.Serialization;
using System.Net;
using System.IO;
using System.Windows.Forms;
using Senga.getSAPData;
using System.Web.Services.Protocols;
using System.Management;




namespace Senga
{
    class ConnectWs
    {

        private String operacion;
        private String catalogo;
        private ArrayList lectura = new ArrayList();
        private String usuario;
        private String contrasena;
        private String numcamion;
        private String fechai;
        private String fechaf;
        private ArrayList mList = new ArrayList();
        private String errormsg;
        private String emsg;
        private String url;

        String SEPARATOR = ",";



        public ConnectWs(String operacion, String catalogo, String usuario, String contrasena, String numcamion)
        {
            this.operacion = operacion;
            this.catalogo = catalogo;
            this.usuario = usuario;
            this.contrasena = contrasena;
            this.numcamion = numcamion;

        }

        public ConnectWs(String operacion, String catalogo, ArrayList lectura, String usuario, String contrasena, String numcamion)
        {
            this.operacion = operacion;
            this.catalogo = catalogo;
            this.lectura = lectura;
            this.usuario = usuario;
            this.contrasena = contrasena;
            this.numcamion = numcamion;
  
        }

        public void connect()
        {

            NetworkCredential cred = new NetworkCredential();
            
            zwsdl_gps zwsdl = new zwsdl_gps();

            url = zwsdl.Url;

            ZWsGps ZWs_Input = new ZWsGps();
            ZWsGpsResponse ZWs_Response = new ZWsGpsResponse();
            
            cred.UserName = usuario;
            cred.Password = contrasena;
            zwsdl.PreAuthenticate = true;
            zwsdl.Credentials = cred;

            zwsdl.Timeout = 10 * 1000;

            ZstLineasGps[] lineasI = { };
            ZstLineasGps[] lineasE = { };

            errormsg = "";
            emsg = "";

            switch (operacion) {
                case Constants.OPERACION_CATALOGO:

                    ZWs_Input.ICatalogo = catalogo;
                    /*
                    ZWs_Input.IFechai = fechai; //"2020-07-22";
                    ZWs_Input.IFechaf = fechaf; //"2020-07-22";
                    ZWs_Input.ICentroi = "2000";
                    ZWs_Input.ICentrof = "2000";
                    ZWs_Input.IAlmaceni = "1000";
                    ZWs_Input.IAlmacenf = "1000";
                    */
                    ZWs_Input.IEquipo = numcamion;                   
                    ZWs_Input.IOperacion = operacion;
                    ZWs_Input.IImei = getIMEIInfo();     
                    ZWs_Input.ITabla = lineasI;

                    try
                    {

                        ZWs_Response = zwsdl.ZWsGps(ZWs_Input);
                        lineasE = ZWs_Response.ETabla;

                        mList = obtenerLista(lineasE);
                        emsg = ZWs_Response.EMsg;

                    }
                    catch (SoapHeaderException)
                    {
                        errormsg = "soap fault in the header e.g. auth failed";
                    }
                    catch (SoapException ex)
                    {
                        errormsg = ex.Message;
                    }
                    catch (WebException)
                    {
                        errormsg = "e.g. internet esta caido";
                    }
                    catch (Exception ex2)
                    {
                        errormsg = ex2.Message;
                    }


                    break;

                case Constants.OPERACION_TRANSFERIR:

                    ZWs_Input.ICatalogo = catalogo;
                    ZWs_Input.IEquipo = numcamion;
                    ZWs_Input.IOperacion = operacion;
                    ZWs_Input.IImei = getIMEIInfo();
                    ZWs_Input.ITabla = tableData(lectura);

                    try
                    {

                        ZWs_Response = zwsdl.ZWsGps(ZWs_Input);
                        //lineasE = ZWsMedicamentos_Response.ETabla;

                        //mList = obtenerLista(lineasE);
                        emsg = ZWs_Response.EMsg;

                    }
                    catch (SoapHeaderException)
                    {
                        errormsg = "soap fault in the header e.g. auth failed";
                    }
                    catch (SoapException ex)
                    {
                        errormsg = ex.Message;
                    }
                    catch (WebException)
                    {
                        errormsg = "e.g. internet esta caido";
                    }
                    catch (Exception ex2)
                    {
                        errormsg = ex2.Message;
                    }

                    break;

            }


            Console.WriteLine("WS connect " + catalogo + " " + operacion + " // " + errormsg + " " + DateTime.Now.ToString());


        }        

        private ArrayList obtenerLista(ZstLineasGps[] lineasE)
        {

            ArrayList lista = new ArrayList();
            
            foreach (ZstLineasGps linease in lineasE)
            {
                lista.Add(linease.Linea);
            }
            
            return lista;

        }

        public ZstLineasGps[] tableData(ArrayList lectura)
        {

            ZstLineasGps[] lineasI2 = new ZstLineasGps[lectura.Count];
           
            int index = 0;

            foreach (String cadena in lectura)
            {

                String cad = cadena;
                String[] sa1 = cadena.Split('|');
                int cantidad = 20 - sa1.Length;

                for (int i=0; i<cantidad; i++) {
                    cad = cad + '|';
                }


                String[] sa = cad.Split('|');
                               
                ZstLineasGps lineasI3 = new ZstLineasGps();
                lineasI3.Linea = cadena;
                lineasI3.Linea2 = "";
                lineasI3.Col01 = sa[0];
                lineasI3.Col02 = sa[1];
                lineasI3.Col03 = sa[2];
                lineasI3.Col04 = sa[3];
                lineasI3.Col05 = sa[4];
                lineasI3.Col06 = sa[5];
                lineasI3.Col07 = sa[6];
                lineasI3.Col08 = sa[7];
                lineasI3.Col09 = sa[8];
                lineasI3.Col10 = sa[9];

                lineasI3.Col11 = sa[10];
                lineasI3.Col12 = sa[11];
                lineasI3.Col13 = sa[12];
                lineasI3.Col14 = sa[13];
                lineasI3.Col15 = sa[14];
                lineasI3.Col16 = sa[15];
                lineasI3.Col17 = sa[16];
                lineasI3.Col18 = sa[17];
                lineasI3.Col19 = sa[18];
                lineasI3.Col20 = sa[19];

                lineasI2[index] = lineasI3;
                index++;
            }
                     
            return lineasI2;

        }


        private string getIMEIInfo()
        {
            string IMEI="_";

         


            return (IMEI);
        }

        public ArrayList getList() { return mList; }
        public String getError() { return errormsg; }
        public String getEmsg() { return emsg; }
        public String getUrl() { return url; }

    }

}

    
