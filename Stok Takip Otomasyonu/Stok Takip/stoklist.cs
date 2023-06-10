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
    public partial class stoklist : Form
    {
        public stoklist()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-MHL953C;Initial Catalog=STOKTAKIP;Integrated Security=True");
        DataSet ds = new DataSet();
        
        private void groupBox2_Enter(object sender, EventArgs e)
        {
            kayıt_listele();
        }

        private void kayıt_listele()


        {

            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from ÜRÜNLER", baglanti);
            adtr.Fill(ds, "ÜRÜNLER");
            dataGridView1.DataSource = ds.Tables["ÜRÜNLER"];
            baglanti.Close();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textid.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textdon.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                texturun.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textmarka.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textmodel.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                textalıs.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                textsatıs.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                textadet.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();

            }
        }




        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update ürünler set donanim_turu=@donanim_turu, marka=@marka,model=@model, urun_adi=@urun_adi,alıs_fiyatı=@alıs_fiyatı,satıs_fiyatı=@satıs_fiyatı,adet=@adet where stok_id=@stok_id",baglanti);

            try
            {
                komut.Parameters.AddWithValue("@stok_id", textid.Text);
                komut.Parameters.AddWithValue("@donanim_turu", textid.Text);
                komut.Parameters.AddWithValue("@urun_adi", texturun.Text);
                komut.Parameters.AddWithValue("@marka", textid.Text);
                komut.Parameters.AddWithValue("@model", textid.Text);
                komut.Parameters.AddWithValue("@alıs_fiyatı", decimal.Parse(textalıs.Text));
                komut.Parameters.AddWithValue("@satıs_fiyatı", decimal.Parse(textsatıs.Text));
                komut.Parameters.AddWithValue("@adet", int.Parse(textadet.Text));
                komut.ExecuteNonQuery();
                
                ds.Tables["ürünler"].Clear();
                kayıt_listele();
                MessageBox.Show("Güncelleme Başarıyla Gerçekleşti");
                
                
                foreach (Control item in this.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }
            catch (Exception hata) 
            { MessageBox.Show(hata.Message); }
            baglanti.Close();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from ürünler where stok_id='"+dataGridView1.CurrentRow.Cells["stok_id"].Value.ToString() +"'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            ds.Tables["ürünler"].Clear();
            kayıt_listele();
            MessageBox.Show("Kayıt Başarıyla Silindi");

        }

        private void idbox_TextChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from ÜRÜNLER where stok_id like '%" + idbox.Text + "%'", baglanti);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            baglanti.Close();
        }

       
    }
        
}
    
