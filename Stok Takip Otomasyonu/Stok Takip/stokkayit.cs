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
    public partial class stokkayit : Form
    {
        public stokkayit()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-MHL953C;Initial Catalog=STOKTAKIP;Integrated Security=True");

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into ÜRÜNLER(stok_id,donanim_turu,urun_adi,marka,model,alıs_fiyatı,satıs_fiyatı,adet) values(@stok_id,@donanim_turu,@urun_adi,@marka,@model,@alıs_fiyatı,@satıs_fiyatı,@adet)", baglanti);
            try
            {
                komut.Parameters.AddWithValue("@stok_id", stok.Text);
                komut.Parameters.AddWithValue("@donanim_turu", donanim.Text);
                komut.Parameters.AddWithValue("@urun_adi", urunadi.Text);
                komut.Parameters.AddWithValue("@marka", marka.Text);
                komut.Parameters.AddWithValue("@model", model.Text);
                komut.Parameters.AddWithValue("@alıs_fiyatı", decimal.Parse(textalıs.Text));
                komut.Parameters.AddWithValue("@satıs_fiyatı", decimal.Parse(textsatıs.Text));
                komut.Parameters.AddWithValue("@adet", int.Parse(adet.Text));

                komut.ExecuteNonQuery();

                
                MessageBox.Show("Ürün kaydı başarıyla eklendi...");
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

        }
    }
}
