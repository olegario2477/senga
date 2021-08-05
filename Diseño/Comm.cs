using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.IO;
using System.Diagnostics;

namespace Senga
{
    class Comm
    {
        
        #region "Atributos"

        private string perror;

        private string pcomm;
        private string pdato;
        private int pbaudrate;
        private int pdatabits;
        private string pparity;
        private string pstopbits;

        private string pcommgps;
        private string pdatogps;
        private int pbaudrategps;
        private int pdatabitsgps;
        private string pparitygps;
        private string pstopbitsgps;
        private string pnmeagps;

        private bool pestable;
        private SerialPort psp;

        private SerialPort pspgps;

        #endregion

        #region "Metodos Publicos"

        public Comm()
        {
            perror = "";

            pcomm = "";
            pdato = "";
            pbaudrate = 0;
            pdatabits = 0;
            pparity = "";
            pstopbits = "";

            pcommgps = "";
            pdatogps = "";
            pbaudrategps = 0;
            pdatabitsgps = 0;
            pparitygps = "";
            pstopbitsgps = "";
            pnmeagps = "";

            pestable = false;
            psp = null;

            pspgps = null;
            
        
        }

        public bool abrirPuerto()
        {

            psp.PortName = pcomm;
            psp.BaudRate = pbaudrate; // 9600;
            psp.DataBits = pdatabits; // 6; //7
            psp.Parity = (Parity)Enum.Parse(typeof(Parity), pparity, true); //Parity.None;
            psp.StopBits = (StopBits)Enum.Parse(typeof(StopBits), pstopbits, true); //StopBits.One;  //pstopbits; //
            psp.Handshake = Handshake.None;
    
            try
            {
                if (!psp.IsOpen)
                {
                    psp.Open();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                perror = ex.Message;
                return false;
            }
            
        }

        public bool cerrarPuerto()
        {
            try
            {
                if (psp.IsOpen)
                {
                    psp.Close();
                    return true;
                }
                else 
                {
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                perror = ex.Message;
                return false;
            }
        }

        public void liberarPuerto()
        {

            try
            {
                psp.Dispose();
            }
            catch (Exception)
            {

            }


        }

        public void resetPuerto()
        {

            psp.DiscardOutBuffer();
            psp.DiscardInBuffer();
           
        }

        public bool recibePuerto()
        {
            string dato = "";

            try
            {
                dato = psp.ReadExisting();
                
                string[] lines = dato.Split('\r');

                dato = lines[0].ToString();

                char c = (char)02;
                dato = dato.Replace(c.ToString(), "");

                if (dato.Contains("$"))
                {
                    pestable = true;
                }
                else
                {
                    pestable = false;
                }

                dato = dato.Replace("$", "");

                dato = dato.TrimStart();
                dato = dato.TrimEnd();

                if (IsNumbersOnly(dato))
                {
                    pdato = dato.ToString();
                }
                else 
                {
                    pdato = "0";
                }

                return true;
                
            }
            catch (Exception ex)
            {
                perror = ex.Message;
                return false;
            }
                    
        
        }


        public bool abrirPuertogps()
        {

            pspgps.PortName = pcommgps;
            pspgps.BaudRate = pbaudrategps; // 9600;
            pspgps.DataBits = pdatabitsgps; // 6; //7
            pspgps.Parity = (Parity)Enum.Parse(typeof(Parity), pparitygps, true); //Parity.None;
            pspgps.StopBits = (StopBits)Enum.Parse(typeof(StopBits), pstopbitsgps, true); //StopBits.One;  //pstopbits; //
            pspgps.Handshake = Handshake.None;

            try
            {
                if (!pspgps.IsOpen)
                {
                    pspgps.Open();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                perror = ex.Message;
                return false;
            }

        }

        public bool cerrarPuertogps()
        {
            try
            {
                if (pspgps.IsOpen)
                {
                    pspgps.Close();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                perror = ex.Message;
                return false;
            }
        }

        public void liberarPuertogps()
        {
            try
            {
                pspgps.Dispose();
            }
            catch (Exception ex)
            {
                perror = ex.Message;
            }
        }

        public bool recibePuertogps()
        {

            string dato = "";

            try
            {

                dato = pspgps.ReadExisting();

                string[] lines = dato.Split('\r');

                pdatogps = "";

                foreach (string line in lines)
                {                   

                    string[] words = line.Split(',');

                    //if (words[0].Equals(p_nmeagps))
                    if (words[0].Contains(p_nmeagps))
                    {
                        pdatogps = line.ToString();

                    }

                }

                if (!pdatogps.Equals(""))
                {
                    return true;
                }
                else
                {
                    perror = "Sin lecturas del gps";
                    return false;

                }
                

            }
            catch (Exception ex)
            {
                perror = ex.Message;
                return false;
            }

        }

        public static bool IsNumbersOnly(string s)
        {
            if (s == null || s == string.Empty)
                return false;

            foreach (char c in s)
            {
                if (c < '0' || c > '9') 
                    return false;
            }

            return true;
        }

        #endregion


        #region "Propiedades"
        public string p_error
        {
            get { return perror; }
            set { perror = value; }
        }

        public string p_comm
        {
            get { return pcomm; }
            set { pcomm = value; }
        }
        public string p_dato
        {
            get { return pdato; }
            set { pdato = value; }
        }
        public int p_baudrate
        {
            get { return pbaudrate; }
            set { pbaudrate = value; }
        }
        public int p_databits
        {
            get { return pdatabits; }
            set { pdatabits = value; }
        }
        public string p_parity
        {
            get { return pparity; }
            set { pparity = value; }
        }
        public string p_stopbits
        {
            get { return pstopbits; }
            set { pstopbits = value; }
        }


        public string p_commgps
        {
            get { return pcommgps; }
            set { pcommgps = value; }
        }
        public string p_datogps
        {
            get { return pdatogps; }
            set { pdatogps = value; }
        }
        public int p_baudrategps
        {
            get { return pbaudrategps; }
            set { pbaudrategps = value; }
        }
        public int p_databitsgps
        {
            get { return pdatabitsgps; }
            set { pdatabitsgps = value; }
        }
        public string p_paritygps
        {
            get { return pparitygps; }
            set { pparitygps = value; }
        }
        public string p_stopbitsgps
        {
            get { return pstopbitsgps; }
            set { pstopbitsgps = value; }
        }

        public string p_nmeagps
        {
            get { return pnmeagps; }
            set { pnmeagps = value; }
        }

        public bool p_estable
        {
            get { return pestable; }
            set { pestable = value; }
        }
        public SerialPort p_sp
        {
            get { return psp; }
            set { psp = value; }
        }

        public SerialPort p_spgps
        {
            get { return pspgps; }
            set { pspgps = value; }
        }


        #endregion



    }

}
