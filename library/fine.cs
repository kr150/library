using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace library
{
    public partial class fine : Form
    {
        SqlDataAdapter sda;
        DataSet ds = new DataSet();
        public fine()
        {
            InitializeComponent();
            ds.Clear();
            try
            {
                string sql = $"exec query_fine {Form1.textBox1.Text}";
                sda = new SqlDataAdapter(sql, Program.connection);


                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[1].HeaderText = "学号";
                dataGridView1.Columns[1].HeaderText = "书号";
                dataGridView1.Columns[2].HeaderText = "超期天数";
                dataGridView1.Columns[3].HeaderText = "借阅时间";

                //设置数据表格为只读
                dataGridView1.ReadOnly = true;
                //不允许添加行
                dataGridView1.AllowUserToAddRows = false;
                //背景为白色
                dataGridView1.BackgroundColor = Color.White;
                //只允许选中单行
                dataGridView1.MultiSelect = false;
                //整行选中
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询错误！" + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
