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
namespace Stok_Takip
{
    public partial class kullanıcıpaneli : Form
    {
        public kullanıcıpaneli()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
        SqlConnection baglanti=new SqlConnection("Data Source=DESKTOP-MHL953C;Initial Catalog=STOKTAKIP;Integrated Security=True");

        
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void kullanıcıpaneli_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            
            SqlCommand komut = new SqlCommand("insert into KULLANICILAR(ad,soyad,kullanici_türü,kullanici_adi,email,sifre,cinsiyet) values(@ad,@soyad,@kullanici_türü,@kullanici_adi,@email,@sifre,@cinsiyet)", baglanti) ;
            #region
            try
            {
                komut.Parameters.AddWithValue("@ad", txtad.Text);
                komut.Parameters.AddWithValue("@soyad", txtsoyad.Text);
                komut.Parameters.AddWithValue("@kullanici_türü", txtkullanici_türü.Text);
                komut.Parameters.AddWithValue("@kullanici_adi", txtkullanici_adi.Text);
                komut.Parameters.AddWithValue("@email", txtemail.Text);
                komut.Parameters.AddWithValue("@sifre", txtsifre.Text);
                if (checkBox3.Checked == true)
                {
                    komut.Parameters.AddWithValue("@cinsiyet", "Erkek");
                }
                if (checkBox1.Checked == true)
                {
                    komut.Parameters.AddWithValue("@cinsiyet", "Kadın");
                }
                komut.ExecuteNonQuery();

                
                MessageBox.Show("Kullanıcı kaydı başarıyla eklendi...");
                foreach (Control item in this.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
                
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
            baglanti.Close();
            #endregion
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
           
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update kullanıcılar set ad=@ad, soyad=@soyad,kullanici_türü=@kullanici_türü, kullanici_adi=@kullanici_adi,email=@email,sifre=@sifre,cinsiyet=@cinsiyet where kullanici_adi='"+txtkullanici_adi+"'", baglanti);
            komut.Parameters.AddWithValue("@ad", txtad.Text);
            komut.Parameters.AddWithValue("@soyad", txtsoyad.Text);
            komut.Parameters.AddWithValue("@kullanici_türü", txtkullanici_türü.Text);
            komut.Parameters.AddWithValue("@kullanici_adi", txtkullanici_adi.Text);
            komut.Parameters.AddWithValue("@email", txtemail.Text);
            komut.Parameters.AddWithValue("@sifre", txtsifre.Text);
            if (checkBox3.Checked == true)
            {
                komut.Parameters.AddWithValue("@cinsiyet", "Erkek");
            }
            if (checkBox1.Checked == true)
            {
                komut.Parameters.AddWithValue("@cinsiyet", "Kadın");
            }
            komut.ExecuteNonQuery();

            baglanti.Close();

           
          
          
            MessageBox.Show("Güncelleme Başarıyla Gerçekleşti");
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void txtkullanici_adi_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from kullanıcılar where kullanici_adi like '" + txtkullanici_adi.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {

                txtad.Text = read["ad"].ToString();
                txtsoyad.Text = read["soyad"].ToString();
                txtkullanici_türü.Text = read["kullanici_türü"].ToString();
                txtemail.Text = read["email"].ToString();


            }
            baglanti.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from kullanıcılar where kullanici_adi='" + txtkullanici_adi + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Başarıyla Silindi");
        }
    }
}
