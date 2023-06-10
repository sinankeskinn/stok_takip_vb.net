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
    public partial class girişekranı : Form
    {
        public girişekranı()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-MHL953C;Initial Catalog=STOKTAKIP;Integrated Security=True");
        SqlCommand komut;
        SqlDataReader read;
        private void label1_Click(object sender, EventArgs e)
        {
          

        }
        giris a =new giris();

        private void button1_Click(object sender, EventArgs e)
        {
            a.kullanicigiris(txtkullanıcıadı, txtsifre);
              formsatiscs ekle = new formsatiscs();
            
        }

     
    }  
    
}
