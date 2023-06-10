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

namespace Stok_Takip
{
    public partial class formsatiscs : Form
    {
        public formsatiscs()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-MHL953C;Initial Catalog=STOKTAKIP;Integrated Security=True");
        DataSet ds = new DataSet();
        SqlCommand komut;
        SqlDataReader read;

        private void button1_Click(object sender, EventArgs e)
        {
            stokkayit ekle = new stokkayit();
            ekle.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            stoklist ekle = new stoklist();
            ekle.ShowDialog();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            stokhareket ekle = new stokhareket();
            ekle.ShowDialog();
        }









        private void button2_Click(object sender, EventArgs e)
        {
            kullanıcıpaneli ekle = new kullanıcıpaneli();
            ekle.ShowDialog();

        }

        private void textid_TextChanged(object sender, EventArgs e)
        {

            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from ürünler where stok_id like '" + textid.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {

                textad.Text = read["urun_adi"].ToString();
                textsatıs.Text = read["satıs_fiyatı"].ToString();

            }
            baglanti.Close();
            temizle();
        }




        private void textadet_TextChanged(object sender, EventArgs e)
        {
            try
            {
                texttoplam.Text = (decimal.Parse(textadet.Text) * decimal.Parse(textsatıs.Text)).ToString();
            }
            catch (Exception)
            {

                ;
            }
        }

        private void textsatıs_TextChanged(object sender, EventArgs e)
        {
            try
            {
                texttoplam.Text = (decimal.Parse(textadet.Text) * decimal.Parse(textsatıs.Text)).ToString();
            }
            catch (Exception)
            {

                ;
            }
        }







        private void sptlistele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from sepet", baglanti);
            adtr.Fill(ds, "sepet");
            dataGridView2.DataSource = ds.Tables["sepet"];

            baglanti.Close();
        }









        private void temizle()
        {
            if (textid.Text == "")
            {
                foreach (Control item in groupBox1.Controls)
                {
                    if (item is TextBox)
                    {
                        if (item != textadet)
                        {
                            item.Text = "";
                        }
                    }
                }


            }
        }



        private void btnekle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into sepet(musteri,stok_id,urun_adi,satıs_fiyatı,adet,toplam_fiyatı,siparis_tarih) values(@musteri,@stok_id,@urun_adi,@satıs_fiyatı,@adet,@toplam_fiyatı,@siparis_tarih)", baglanti);
            komut.Parameters.AddWithValue("@musteri", textalici.Text);
            komut.Parameters.AddWithValue("@stok_id", textid.Text);
            komut.Parameters.AddWithValue("@urun_adi", textad.Text);
            komut.Parameters.AddWithValue("@satıs_fiyatı", decimal.Parse(textsatıs.Text));
            komut.Parameters.AddWithValue("@adet", int.Parse(textadet.Text));
            komut.Parameters.AddWithValue("@toplam_fiyatı", decimal.Parse(texttoplam.Text));
            komut.Parameters.AddWithValue("@siparis_tarih", DateTime.Now);
            komut.ExecuteNonQuery();
            baglanti.Close();
            ds.Tables["sepet"].Clear();
            textadet.Text = "1";
            sptlistele();
            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                {
                    if (item != textadet)
                    {
                        item.Text = "";
                    }
                }
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from sepet where musteri='" + dataGridView2.CurrentRow.Cells["musteri"].Value.ToString() + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            ds.Tables["sepet"].Clear();
            MessageBox.Show("Ürün Sepetten Çıkarıldı");
            sptlistele();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from sepet ", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            ds.Tables["sepet"].Clear();
            MessageBox.Show("Tüm Satışlar İptal Edildi");
            sptlistele();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into satıs(musteri,stok_id,urun_adi,satıs_fiyatı,adet,toplam_fiyatı,siparis_tarih) values(@musteri,@stok_id,@urun_adi,@satıs_fiyatı,@adet,@toplam_fiyatı,@siparis_tarih)", baglanti);
                komut.Parameters.AddWithValue("@musteri", dataGridView2.Rows[i].Cells["musteri"].Value.ToString());
                komut.Parameters.AddWithValue("@stok_id", dataGridView2.Rows[i].Cells["stok_id"].Value.ToString());
                komut.Parameters.AddWithValue("@urun_adi", dataGridView2.Rows[i].Cells["urun_adi"].Value.ToString());
                komut.Parameters.AddWithValue("@satıs_fiyatı", decimal.Parse(dataGridView2.Rows[i].Cells["satıs_fiyatı"].Value.ToString()));
                komut.Parameters.AddWithValue("@adet", int.Parse(dataGridView2.Rows[i].Cells["adet"].Value.ToString()));
                komut.Parameters.AddWithValue("@toplam_fiyatı", decimal.Parse(dataGridView2.Rows[i].Cells["toplam_fiyatı"].Value.ToString()));
                komut.Parameters.AddWithValue("@siparis_tarih", DateTime.Now);
                komut.ExecuteNonQuery();
                SqlCommand komut2 = new SqlCommand("update ürünler set adet=adet-'" + int.Parse(dataGridView2.Rows[i].Cells["adet"].Value.ToString()) + "'where stok_id='" + dataGridView2.Rows[i].Cells["stok_id"].Value.ToString() + "'", baglanti);
                komut2.ExecuteNonQuery();
                baglanti.Close();
            }
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("delete from sepet", baglanti); ;
            komut3.ExecuteNonQuery();
            baglanti.Close();
            ds.Tables["sepet"].Clear();
            sptlistele();
            MessageBox.Show("Satışlar Başarıyla Gerçekleştirildi");

        }

        private void formsatiscs_Load(object sender, EventArgs e)
        {
       

                sptlistele();
             }
    }

}

/*/private void formsatiscs_Load(object sender, EventArgs e,int roll)
{

    baglanti.Open();
    komut = new SqlCommand();
    komut.Connection = baglanti;
    komut.CommandText = "select *from KULLANICILAR where roll='" + roll + "'";
    read = komut.ExecuteReader();
    if (read.Read() == true)
    {
        button2.Enabled = true;
    }
    else
    {
        button2.Enabled = false;
        button2_Click(sender, e);
        {
            MessageBox.Show("Erişim Yetkiniz Bulunmuyor");
        }
    }

    sptlistele();
}
*/