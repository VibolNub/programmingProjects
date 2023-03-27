using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace VibolNubApp
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            userInfoFrm form= new userInfoFrm();
            form.MdiParent = this;
            form.Show();
            
        }
        public void showOrHideMenu(Boolean val)
        {
            //file
            userInfoToolStripMenuItem.Visible = val;
            //data
            customerToolStripMenuItem.Visible = val;
            supplierToolStripMenuItem.Visible = val;
            productToolStripMenuItem.Visible = val;
            //transaction
            transactionToolStripMenuItem.Visible = val;
            //report
            reportsToolStripMenuItem.Visible = val;
        }public void checkUserPermission()
        {
            //file
            userInfoToolStripMenuItem.Visible = isValidPermission("File","User Info");
            //data
            customerToolStripMenuItem.Visible = isValidPermission("Data","Customer");
            supplierToolStripMenuItem.Visible = isValidPermission("Data","Supplier");
            productToolStripMenuItem.Visible = isValidPermission("Data","Product");
        }
        public Boolean isValidPermission(String menu, String sub_menu)
        {
            SqlCommand cmd = new SqlCommand("",Program.cnn);
            cmd.CommandText = "select count(*) from tbluserpermission p\r\ninner join tblusers u on u.usr_id = p.usr_id\r\nwhere u.usr_name = '"+Program.active_user+"' and is_active = 1\r\nand menu='"+menu+"' and sub_menu ='"+sub_menu+"'\r\n";
            if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
            {
                return true;
            }

            return false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (Program.active_user.ToLower() == "admin")
            {
                showOrHideMenu(true);
            }
            else
            {
                showOrHideMenu(false);
                checkUserPermission();
            }
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            login frm = new login();
            frm.ShowDialog();
            Program.active_user = frm.txtName.Text;
            frm.Close();
            Form1_Load(null, null);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void main_FormClosed(object sender, FormClosedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("",Program.cnn);
            cmd.CommandText = "update tblusers set log_status = 0 where usr_name = '"+Program.active_user+"'";
            cmd.ExecuteNonQuery();
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("", Program.cnn); 
            cmd.CommandText = "select count(*) from tblusers where log_status = 1 and usr_name = '" + Program.active_user + "'";
            int count = Convert.ToInt32(cmd.ExecuteScalar()); 
            if (count == 0)
            {
                Application.Exit();
            }
            
        }

        private void provinceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            province frm = new province();
            frm.MdiParent = this;
            frm.Show();
        }

        private void userInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            userInfoFrm frm = new userInfoFrm();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
