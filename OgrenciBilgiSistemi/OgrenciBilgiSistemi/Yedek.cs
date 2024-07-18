using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OgrenciBilgiSistemi
{
    public partial class Yedek : Form
    {
        public SqlConnection baglan = new SqlConnection("server=DESKTOP-K05KM2V;initial catalog = OBS; integrated security = SSPI");
        public DataSet ds = new DataSet();

        public Yedek()
        {
            InitializeComponent();
        }

        private void Ogrenci_Load(object sender, EventArgs e)
        {
            OgrenciListele();
        }

        void OgrenciListele()
        {
            ds.Clear();
            baglan.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT numara, ad, soyad, email, sifre, sinif, cinsiyet, adres FROM Ogrenci", baglan);
            da.Fill(ds, "Ogrenci");
            dataGridView1.DataSource = ds.Tables["Ogrenci"];
            baglan.Close();
        }
        void OgrenciKaydet()
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || comboBox1.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("Boş Alan bırakma");
            }
            else
            {
                MessageBox.Show(textBox2.Text);
                SqlCommand OgrenciNumaraSorgulama = new SqlCommand("SELECT COUNT(numara) FROM Ogrenci WHERE numara = @numara", baglan);
                OgrenciNumaraSorgulama.Parameters.AddWithValue("@numara", textBox2.Text);
                baglan.Open();
                int saydir = (int)OgrenciNumaraSorgulama.ExecuteScalar();
                baglan.Close();
                if (saydir > 0)
                {
                    MessageBox.Show("Bu numarada kayıtlı öğrenci zaten var..");
                }
                else
                {
                    baglan.Open();
                    SqlCommand OgrenciKaydet = new SqlCommand("INSERT INTO Ogrenci(numara, ad, soyad, email, sifre, sinif, cinsiyet, adres) values(@numara, @ad, @soyad, @email, @sifre, @sinif, @cinsiyet, @adres)", baglan);
                    OgrenciKaydet.Parameters.AddWithValue("@numara", textBox2.Text);
                    OgrenciKaydet.Parameters.AddWithValue("@ad", textBox3.Text);
                    OgrenciKaydet.Parameters.AddWithValue("@soyad", textBox4.Text);
                    OgrenciKaydet.Parameters.AddWithValue("@email", textBox5.Text);
                    OgrenciKaydet.Parameters.AddWithValue("@sifre", textBox6.Text);
                    OgrenciKaydet.Parameters.AddWithValue("@adres", textBox7.Text);
                    OgrenciKaydet.Parameters.AddWithValue("@sinif", comboBox1.Text);
                    OgrenciKaydet.Parameters.AddWithValue("@cinsiyet", comboBox2.Text);
                    OgrenciKaydet.ExecuteNonQuery();
                    ds.Clear();
                    baglan.Close();
                    OgrenciListele();
                    temizle();
                    MessageBox.Show("Öğrenci kaydedildi");
                }
            }
        }
        void OgrenciGuncelle()
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || comboBox1.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("Boş Alan bırakma");
            }
            else
            {  
                SqlCommand OgrenciGuncelle = new SqlCommand("UPDATE Ogrenci SET ad=@ad, soyad=@soyad, email=@email, sifre=@sifre, sinif=@sinif, cinsiyet=@cinsiyet, adres=@adres WHERE numara=@numara", baglan);
                OgrenciGuncelle.Parameters.AddWithValue("@numara", textBox2.Text);
                OgrenciGuncelle.Parameters.AddWithValue("@ad", textBox3.Text);
                OgrenciGuncelle.Parameters.AddWithValue("@soyad", textBox4.Text);
                OgrenciGuncelle.Parameters.AddWithValue("@email", textBox5.Text);
                OgrenciGuncelle.Parameters.AddWithValue("@sifre", textBox6.Text);
                OgrenciGuncelle.Parameters.AddWithValue("@adres", textBox7.Text);
                OgrenciGuncelle.Parameters.AddWithValue("@sinif", comboBox1.Text);
                OgrenciGuncelle.Parameters.AddWithValue("@cinsiyet", comboBox2.Text);
                baglan.Open();
                OgrenciGuncelle.ExecuteNonQuery();
                baglan.Close();
                OgrenciListele();
                temizle();
                MessageBox.Show("Öğrenci Güncellenmiştir");
            }
        }
        void OgrenciSil()
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Numara alanı dolu olmalıdır");
            }
            else
            { 
                SqlCommand OgrenciSil = new SqlCommand("DELETE FROM Ogrenci WHERE numara=@numara", baglan);
                OgrenciSil.Parameters.AddWithValue("@numara", textBox2.Text);
                baglan.Open();
                OgrenciSil.ExecuteNonQuery();
                baglan.Close();
                OgrenciListele();
                temizle();
            }
        }
        void temizle()
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }

        //Öğrenci Kaydet
        private void button1_Click(object sender, EventArgs e)
        {
            OgrenciKaydet();
        }
        //Öğrenci Listele
        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
        }
        //TextBox temizle
        private void button3_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OgrenciGuncelle();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OgrenciSil();
        }
    }
}
