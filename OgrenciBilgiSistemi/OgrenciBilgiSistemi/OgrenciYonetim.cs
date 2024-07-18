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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OgrenciBilgiSistemi
{
    public partial class OgrenciYonetim : Form
    {

        public SqlConnection baglan = new SqlConnection("server=DESKTOP-K05KM2V;initial catalog = OBS; integrated security = SSPI");
        public DataSet ds = new DataSet();

        void OgrenciListele()
        {
            ds.Clear();
            baglan.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT numara, ad, soyad, cinsiyet, sinif, email, sifre, adres FROM Ogrenci", baglan);
            da.Fill(ds, "Ogrenci");
            dataGridView1.DataSource = ds.Tables["Ogrenci"];
            baglan.Close();
        }

        void OgrenciEkle()
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || comboBox1.Text == "" || comboBox2.Text == "")
            {
                //MessageBox.Show("Boş Alan bırakma");
                MessageBox.Show("Boş Alan bırakmayınız", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                SqlCommand OgrenciNumaraSorgulama = new SqlCommand("SELECT COUNT(numara) FROM Ogrenci WHERE numara = @numara", baglan);
                OgrenciNumaraSorgulama.Parameters.AddWithValue("@numara", textBox1.Text);
                
                baglan.Open();
                int saydir = (int)OgrenciNumaraSorgulama.ExecuteScalar();
                baglan.Close();

                if (saydir > 0)
                {
                    //MessageBox.Show("Bu numarada kayıtlı öğrenci zaten var..");
                    MessageBox.Show("Bu numarada kayıtlı öğrenci zaten var..", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    baglan.Open();
                    SqlCommand OgrenciKaydet = new SqlCommand("INSERT INTO Ogrenci(ad, soyad, cinsiyet, sinif, email, sifre, adres) values(@ad, @soyad, @cinsiyet, @sinif, @email, @sifre, @adres)", baglan);
                    OgrenciKaydet.Parameters.AddWithValue("@ad", textBox2.Text);
                    OgrenciKaydet.Parameters.AddWithValue("@soyad", textBox3.Text);
                    OgrenciKaydet.Parameters.AddWithValue("@cinsiyet", comboBox1.Text);
                    OgrenciKaydet.Parameters.AddWithValue("@sinif", comboBox2.Text);
                    OgrenciKaydet.Parameters.AddWithValue("@email", textBox4.Text);
                    OgrenciKaydet.Parameters.AddWithValue("@sifre", textBox5.Text);
                    OgrenciKaydet.Parameters.AddWithValue("@adres", textBox6.Text);
                    OgrenciKaydet.ExecuteNonQuery();
                    ds.Clear();
                    baglan.Close();
                    OgrenciListele();
                    temizle();
                    MessageBox.Show("Öğrenci kaydedildi..", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        void OgrenciGuncelle()
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || comboBox1.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("Boş Alan bırakmayınız..", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                SqlCommand OgrenciGuncelle = new SqlCommand("UPDATE Ogrenci SET ad=@ad, soyad=@soyad, cinsiyet=@cinsiyet, sinif=@sinif, email=@email, sifre=@sifre, adres=@adres WHERE numara=@numara", baglan);
                OgrenciGuncelle.Parameters.AddWithValue("@numara", textBox1.Text);
                OgrenciGuncelle.Parameters.AddWithValue("@ad", textBox2.Text);
                OgrenciGuncelle.Parameters.AddWithValue("@soyad", textBox3.Text);
                OgrenciGuncelle.Parameters.AddWithValue("@cinsiyet", comboBox1.Text);
                OgrenciGuncelle.Parameters.AddWithValue("@sinif", comboBox2.Text);
                OgrenciGuncelle.Parameters.AddWithValue("@email", textBox4.Text);
                OgrenciGuncelle.Parameters.AddWithValue("@sifre", textBox5.Text);
                OgrenciGuncelle.Parameters.AddWithValue("@adres", textBox6.Text);
                baglan.Open();
                OgrenciGuncelle.ExecuteNonQuery();
                baglan.Close();
                OgrenciListele();
                temizle();
                MessageBox.Show("Öğrenci Güncellenmiştir..", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        void OgrenciSil()
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Numara alanı dolu olmalıdır..", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                SqlCommand OgrenciSil = new SqlCommand("DELETE FROM Ogrenci WHERE numara=@numara", baglan);
                OgrenciSil.Parameters.AddWithValue("@numara", textBox1.Text);
                baglan.Open();
                OgrenciSil.ExecuteNonQuery();
                baglan.Close();
                OgrenciListele();
                temizle();
                MessageBox.Show("Kayıt Silindi..", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        void temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            /*comboBox1.Text = string.Empty;
            comboBox2.Text = string.Empty;*/
        }

        public OgrenciYonetim(string ad, string soyad, string yetki)
        {
            InitializeComponent();
            label10.Text = "Hoş Geldiniz: " + ad +" "+ soyad;
            //int a = 0;
            if (yetki == "1")
            {
                MessageBox.Show("Yönetici Girişi, Bütün Yetkilere Sahipsiniz..");
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button7.Enabled = true;
            }
            else
            {
                MessageBox.Show("Akademisyen Girişi, Yetki Kısıtlaması var..");
                button3.BackColor = Color.White;
                button4.BackColor = Color.White;
                button5.BackColor = Color.White;
                button7.BackColor = Color.White;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button7.Enabled = false;
            }
        }

        private void OgrenciYonetim_Load(object sender, EventArgs e)
        {
            OgrenciListele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OgrenciEkle();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OgrenciGuncelle();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OgrenciSil();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DersListele goster = new DersListele();
            goster.Show();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            NotEkleme goster = new NotEkleme();
            goster.Show();
        }

        void AdaGoreListele()
        {
            if (textBox7.Text == "")
            {
                MessageBox.Show("Lütfen aramak istediğiniz öğrenci adını girin.");
            }
            else
            {
                ds.Clear();
                baglan.Open();
                SqlCommand adListele = new SqlCommand("SELECT * FROM Ogrenci WHERE ad LIKE @ad   ", baglan);
                adListele.Parameters.AddWithValue("@ad", textBox7.Text);

                SqlDataAdapter adapter = new SqlDataAdapter(adListele);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                baglan.Close();
            }
        }

        void SoyadaGoreListele()
        {
            if (textBox9.Text == "")
            {
                MessageBox.Show("Lütfen aramak istediğiniz öğrenci soyadını girin.");
            }
            else
            {
                ds.Clear();
                baglan.Open();
                SqlCommand SoyadaListele = new SqlCommand("SELECT * FROM Ogrenci WHERE soyad LIKE @soyad   ", baglan);
                SoyadaListele.Parameters.AddWithValue("@soyad", textBox9.Text);

                SqlDataAdapter adapter = new SqlDataAdapter(SoyadaListele);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                baglan.Close();
            }
        }

        void NumarayaGoreListele()
        {
            if (textBox8.Text == "")
            {
                MessageBox.Show("Lütfen aramak istediğiniz öğrenci soyadını girin.");
            }
            else
            {
                ds.Clear();
                baglan.Open();
                SqlCommand numaraListele = new SqlCommand("SELECT * FROM Ogrenci WHERE numara LIKE @numara", baglan);
                numaraListele.Parameters.AddWithValue("@numara", textBox8.Text);

                SqlDataAdapter adapter = new SqlDataAdapter(numaraListele);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                baglan.Close();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AdaGoreListele();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SoyadaGoreListele();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button11_Click(object sender, EventArgs e)
        {
            OgrenciListele();
            temizle();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            NumarayaGoreListele();
        }
    }
}
