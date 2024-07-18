using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Data.SqlClient;

namespace OgrenciBilgiSistemi
{
    public partial class AkademisyenGiris : Form
    {
        public SqlConnection baglan = new SqlConnection("server=DESKTOP-K05KM2V;initial catalog = OBS; integrated security = SSPI");
        public DataSet ds = new DataSet();

        public AkademisyenGiris()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Boş Alan Bırakmayınız..");
            }
            else
            {
                SqlCommand AkademisyenGiris = new SqlCommand("SELECT * FROM Akademisyen WHERE ad=@ad and sifre=@sifre", baglan);
                AkademisyenGiris.Parameters.AddWithValue("@ad", textBox1.Text);
                AkademisyenGiris.Parameters.AddWithValue("@sifre", textBox2.Text);
                baglan.Open();
                SqlDataReader oku = AkademisyenGiris.ExecuteReader();
                if (oku.Read())
                {
                    string ad, soyad, yetki;
                    
                    ad = oku["ad"].ToString();
                    soyad = oku["soyad"].ToString();
                    yetki = oku["yetki"].ToString();
                    OgrenciYonetim goster = new OgrenciYonetim(ad, soyad, yetki);
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GirisSayfasi goster = new GirisSayfasi();
            this.Hide();
            goster.Show();
        }
    }
}
