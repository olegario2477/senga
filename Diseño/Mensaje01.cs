using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Senga
{
    public partial class Mensaje01 : Form
    {
        public Mensaje01(string mensaje, int img)
        {
            InitializeComponent();
            lblMensaje.Text = mensaje;
            picImagen.Image = imagenLista.Images[img];
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
           
        }


    }
}