using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OgrenciBilgiSistemi
{
    public partial class OgrenciGiris : Form
    {
        public SqlConnection baglan = new SqlConnection("server=DESKTOP-K05KM2V;initial catalog = OBS; integrated security = SSPI");
        public DataSet ds = new DataSet();

        public OgrenciGiris()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Boş alan bırakmayınız");
            }
            else
            {
                SqlCommand OgrenciGiris = new SqlCommand("SELECT * FROM Ogrenci WHERE numara=@numara and sifre=@sifre", baglan);
                OgrenciGiris.Parameters.AddWithValue("@numara", textBox1.Text);
                OgrenciGiris.Parameters.AddWithValue("@sifre", textBox2.Text);
                baglan.Open();
                SqlDataReader oku = OgrenciGiris.ExecuteReader();
                if (oku.Read())
                {
                    string numara, ad, soyad;

                    numara = oku["numara"].ToString();
                    ad = oku["ad"].ToString();
                    soyad = oku["soyad"].ToString();
                    OgrenciSayfa goster = new OgrenciSayfa(numara, ad, soyad);
                    this.Hide();
                    goster.Show();
                }
                else
                {
                    MessageBox.Show("Bilgileriniz yanlış");
                    textBox1.Clear();
                    textBox2.Clear();
                }
                oku.Close();
                baglan.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GirisSayfasi goster = new GirisSayfasi();
            this.Hide();
            goster.Show();
        }
    }
}
