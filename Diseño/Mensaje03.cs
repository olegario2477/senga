using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Senga
{
    public partial class Mensaje03 : Form
    {

        public int result { get; set; }

        public Mensaje03(string mensaje, int img)
        {
            InitializeComponent();
            lblMensaje.Text = mensaje;
            picImagen.Image = imagenLista.Images[img];
        }

        private void btnSi_Click(object sender, EventArgs e)
        {
            result = Constants.MSG_BOTON_SI;
            Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            result = Constants.MSG_BOTON_NO;
            Close();
        }


    }
}
