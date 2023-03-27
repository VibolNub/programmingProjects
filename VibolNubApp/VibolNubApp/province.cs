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

namespace VibolNubApp
{
    public partial class province : Form
    {
        public province()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("",Program.cnn);
            da.SelectCommand.CommandText = "select * from tblprovince";
            da.Fill(tbl);

            dataGridView1.DataSource= tbl;
        }
    }
}
