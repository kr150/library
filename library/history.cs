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
    public partial class history : Form
    {
        SqlDataAdapter sda;
        DataSet ds = new DataSet();
        public history()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ds.Clear();
            try
            {
                string flag;
                string sql = "";
                flag = comboBox1.SelectedItem.ToString().Trim(' ');
                if(flag == "过去一个周")
                {
                    sql = "select * " +
                        "from borrow_book " +
                        "where  borrow_date>=DATEADD(WEEK,-1,GETDATE()) and sno = "+Form1.textBox1.Text;
                }
                else if(flag == "过去一个月")
                {
                    sql = "select * " +
                       "from borrow_book " +
                       "where  borrow_date >= dateadd(month, -1, getdate()) and sno = " + Form1.textBox1.Text; 
                }
                else if (flag == "本年")
                {
                    sql = "select * " +
                       "from borrow_book " +
                       "where YEAR(borrow_date) = YEAR(GETDATE()) and sno = " + Form1.textBox1.Text; 
                }
                else if (flag == "全部")
                {
                    sql = "select * " +
                       "from borrow_book " +
                       "where sno = " + Form1.textBox1.Text;
                }
                else
                {
                    MessageBox.Show("出错了");
                }
                sda = new SqlDataAdapter(sql, Program.connection);
                //确定执行sql

                
                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].HeaderText = "学号";
                dataGridView1.Columns[1].HeaderText = "书号";
                dataGridView1.Columns[2].HeaderText = "借阅时间";
                dataGridView1.Columns[3].HeaderText = "归还时间";
                
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

       
    }
}
