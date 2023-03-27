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
    public partial class userInfoFrm : Form
    {
        public userInfoFrm()
        {
            InitializeComponent();
        }

        private void importExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            importFrm frm = new importFrm();
            frm.ShowDialog();

            frm.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void userInfoFrm_Load(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("", Program.cnn);
            da.SelectCommand.CommandText = "select pro_id,(pro_name_kh+'('+ pro_name+')') pro_name from tblprovince where is_active = 1";
            da.Fill(tbl);
            cboProvince.ValueMember = "pro_id";
            cboProvince.DisplayMember = "pro_name";
            cboProvince.DataSource = tbl;

            DataTable tbl2 = new DataTable();
            da.SelectCommand.CommandText = "select u.*,p.pro_name_kh from tblusers u\r\nleft join tblprovince p on p.pro_id = u.pro_id";
            da.Fill(tbl2);
            //dataGridView1.DataSource = tbl2;
            foreach (DataRow r in tbl2.Rows)
            {
                int idx = dataGridView1.Rows.Add(r[0]);
                for (int i = 1; i<=dataGridView1.Columns.Count-1; i++) {
                    dataGridView1.Rows[idx].Cells[i].Value= r[i];
                }
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
