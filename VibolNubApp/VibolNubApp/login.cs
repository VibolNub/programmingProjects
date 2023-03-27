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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private string randomConfirmCode()
        {
            SqlCommand cmd = new SqlCommand("", Program.cnn);

            Random rnd = new Random();
            string confirm_pwd = rnd.Next(0, 1000000).ToString("000000");
            cmd.CommandText = "update tblusers set confirm_code ='" + confirm_pwd + "', exp_datetime=dateadd(mi,5,getdate()) where usr_name='" + txtName.Text + "'";
            //MessageBox.Show(cmd.CommandText + "");
            cmd.ExecuteNonQuery();
            return confirm_pwd;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("", Program.cnn);
            if (btnLogin.Text == "Login")
            {

                cmd.CommandText = "select count(*) from tblusers where usr_name = '" + txtName.Text + "' and usr_pwd='" + txtPwd.Text + "'";
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 0)
                {
                    MessageBox.Show("Invalid User or Password!");
                    return;
                }
                else
                {
                    string confirm_pwd = this.randomConfirmCode();

                    txtName.Enabled = false;
                    txtPwd.Enabled = false;
                    btnResent.Enabled = true;
                    txtConfirm.Text = confirm_pwd;
                    btnLogin.Text = "Verify";
                }
            }
            else
            {
                cmd.CommandText = "select count(*) from tblusers where usr_name='" + txtName.Text + "' and usr_pwd = '" + txtPwd.Text + "' and confirm_code ='" + txtConfirm.Text + "' and getdate()<=exp_datetime";
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 0)
                {
                    MessageBox.Show("Invalid Confirm Code! /Expired ");
                    txtConfirm.Clear();
                    txtConfirm.Focus();

                }
                else
                {
                    this.Hide();
                    Program.active_user = txtName.Text;
                    //MessageBox.Show("active user in program is: "+Program.active_user);
                    cmd.CommandText = "update tblusers set log_status = 1 where usr_name = '" + txtName.Text + "'";
                    cmd.ExecuteNonQuery();
                    main frm = new main(); 
                    frm.Show();
                    frm.lblActiveUser.Text = txtName.Text;
                    
                   // MessageBox.Show("active user in program is: " + Program.active_user);

                }
            }
        }

        private void btnResent_Click(object sender, EventArgs e)
        {
            string confirm_pwd = this.randomConfirmCode();
            txtConfirm.Text= confirm_pwd;
        }
    }
}
