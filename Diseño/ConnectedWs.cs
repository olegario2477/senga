using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Senga
{
    class ConnectedWs
    {

        private String operacion;
        private String catalogo;
        private ArrayList lectura = new ArrayList();
        private String equipo;
        private String usuario;
        private String contrasena;
        private String numcamion;
        private String fechai;
        private String fechaf;
        private ArrayList mList = new ArrayList();
        private String errormsg;
        private String emsg;
        private String url;

        ConnectWs connectar;

        String SEPARATOR = ",";


        public ConnectedWs(String operacion, String catalogo, String usuario, String contrasena, String numcamion)
        {
            this.operacion = operacion;
            this.catalogo = catalogo;
            this.usuario = usuario;
            this.contrasena = contrasena;
            this.numcamion = numcamion;

        }

        public ConnectedWs(String operacion, String catalogo, ArrayList lectura, String usuario, String contrasena, String numcamion)
        {
            this.operacion = operacion;
            this.catalogo = catalogo;
            this.lectura = lectura;
            this.usuario = usuario;
            this.contrasena = contrasena;
            this.numcamion = numcamion;

        }

        public void setDataWs()
        {

            connectar = new ConnectWs(operacion, catalogo, lectura, usuario, contrasena, numcamion);
            connectar.connect();

            errormsg = connectar.getError();
            emsg = connectar.getEmsg();
            url = connectar.getUrl();
            //mList = connectar.getList();

        }

        public void getDataWs()
        {
            connectar = new ConnectWs(operacion, catalogo, usuario, contrasena, numcamion);
            connectar.connect();

            errormsg = connectar.getError();
            emsg = connectar.getEmsg();
            url = connectar.getUrl();
            mList = connectar.getList();

        }

        public ArrayList getList() { return mList; }
        public String getError() { return errormsg; }
        public String getEmsg() { return emsg; }
        public String getUrl() { return url; }

    }


}
