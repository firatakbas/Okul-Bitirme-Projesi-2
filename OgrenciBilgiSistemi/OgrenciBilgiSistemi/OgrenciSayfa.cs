using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OgrenciBilgiSistemi
{
    public partial class OgrenciSayfa : Form
    {
        public SqlConnection baglan = new SqlConnection("server=DESKTOP-K05KM2V;initial catalog = OBS; integrated security = SSPI");
        public DataSet ds = new DataSet();

        public OgrenciSayfa(string numara, string ad, string soyad)
        {
            InitializeComponent();
            label1.Text = "Hoş Geldiniz : " + ad + " " + soyad + " - " + "Numaranız" + " " + numara;
            textBox1.Text = numara;
        }

        void notlarİGetir()
        {
            ds.Clear();
            baglan.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT Ogrenci.numara, Ogrenci.ad, Ogrenci.soyad, OgrenciDers.dersadi, OgrenciDers.vize, OgrenciDers.final FROM Ogrenci LEFT JOIN OgrenciDers ON Ogrenci.numara = OgrenciDers.ogrenci_id WHERE OgrenciDers.ogrenci_id = '" + textBox1.Text +"' ", baglan);
            da.Fill(ds, "OgrenciDers");
            dataGridView1.DataSource = ds.Tables["OgrenciDers"];
            baglan.Close();
        }

        private void OgrenciSayfa_Load(object sender, EventArgs e)
        {
            notlarİGetir();
        }
    }
}
