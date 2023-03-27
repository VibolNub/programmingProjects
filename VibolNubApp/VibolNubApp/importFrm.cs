using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace VibolNubApp
{
    public partial class importFrm : Form
    {
        public importFrm()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void display_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndex = 0;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Excel File|*.xlsx";
            d.Multiselect = false;
            if (d.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.Columns.Clear();
                string filePath = d.FileName;
                Microsoft.Office.Interop.Excel.Application Xa = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook Wb;
                Microsoft.Office.Interop.Excel.Worksheet Ws;
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    Wb = Xa.Workbooks.Open(filePath, false, false, true);
                    Ws = Wb.Worksheets["Sheet1"];

                    //get column header
                    int colIndex = 1;
                    int rowIndex = 1;
                    string headerLabel = Ws.Cells[rowIndex, colIndex].Text;
                    while (headerLabel.Length > 0)
                    {
                        dataGridView1.Columns.Add("col" + colIndex, headerLabel);
                        colIndex++;
                        headerLabel = Ws.Cells[rowIndex, colIndex].Text;
                    }

                    //Get Record
                    rowIndex++;
                    string userId = Ws.Cells[rowIndex, 1].Text;
                    while (userId.Length > 0)
                    {
                        //read record
                        int r = dataGridView1.Rows.Add(userId);
                        for (int c = 2; c <= dataGridView1.Columns.Count; c++)
                        {
                            dataGridView1.Rows[r].Cells[c - 1].Value = Ws.Cells[rowIndex, c].Text;
                        }
                        rowIndex++;
                        userId = Ws.Cells[rowIndex, 1].Text;
                    }

                    Wb.Close();
                    Xa.Quit();
                    Ws = null; Wb = null; Xa = null;
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    Ws = null; Wb = null; Xa = null;
                    MessageBox.Show(ex.Message.ToString(), "Error",
                                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
