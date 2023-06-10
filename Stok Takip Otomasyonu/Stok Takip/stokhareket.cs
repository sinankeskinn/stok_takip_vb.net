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
    public partial class stokhareket : Form
    {
        public stokhareket()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-MHL953C;Initial Catalog=STOKTAKIP;Integrated Security=True");
        DataSet ds = new DataSet();

        private void satıslistele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from satıs", baglanti);
            adtr.Fill(ds, "satıs");
            dataGridView3.DataSource = ds.Tables["satıs"];

            baglanti.Close();
        }

        private void stokhareket_Load(object sender, EventArgs e)
        {
            satıslistele();
        }
    }


}
