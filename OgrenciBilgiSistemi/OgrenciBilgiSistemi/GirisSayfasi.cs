using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OgrenciBilgiSistemi
{
    public partial class GirisSayfasi : Form
    {
        public GirisSayfasi()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AkademisyenGiris goster = new AkademisyenGiris();
            this.Hide();
            goster.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OgrenciGiris goster = new OgrenciGiris();
            this.Hide();
            goster.Show();
        }
    }
}
