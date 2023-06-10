using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace Stok_Takip
{
    internal class giris
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-MHL953C;Initial Catalog=STOKTAKIP;Integrated Security=True");
        SqlCommand komut;
        SqlDataReader read;
        formsatiscs satis = new formsatiscs();
        public  SqlDataReader kullanicigiris(TextBox kullanıcıadı,TextBox şifre)
        {
            baglanti.Open();
            komut = new SqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = "select *from KULLANICILAR where kullanici_adi='"+kullanıcıadı.Text+"'";
            read = komut.ExecuteReader();

            if(read.Read()==true)
            {
                if (şifre.Text==read["sifre"].ToString())
                {
                    MessageBox.Show("Giriş Başarılı");
                    satis.ShowDialog();
                    baglanti.Close();

                }
                else
                {
                    MessageBox.Show("Şifrenizi hatalı girdiniz!!! \n Lütfen Tekrar Deneyin.");
                    
                }
            }
            else
            {
                MessageBox.Show("Bilgilerinizi hatalı girdiniz \n Lütfen Tekrar Deneyin.");
            }
            baglanti.Close();
            return read;
        }
    }

}
