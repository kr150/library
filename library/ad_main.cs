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
    public partial class ad_main : Form
    {
        SqlDataAdapter sda;
        DataSet ds = new DataSet();

        public ad_main()
        {
            InitializeComponent();
            this.FormClosing += Ad_main_FormClosing;
           
        }

        

        private void Ad_main_FormClosing(object sender, FormClosingEventArgs e)
        {
            return;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.command.CommandText = "update student set spassword = 0 where sno = '"+textBox1.Text+"'";
            Program.command.ExecuteNonQuery();
            Program.command.Dispose();
            MessageBox.Show("修改成功，密码为0");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            book_management b = new book_management();
            b.ShowDialog();

        }

        private void ad_main_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ds.Clear();

            string table;
            if (radioButton1.Checked)
                table = "borrow_book";
            else if (radioButton2.Checked)
                table = "fine";
            else
            {
                MessageBox.Show("未选定查询方式");
                return;
            }
            try
            {
                string flag;
                string sql = "";
                flag = comboBox1.SelectedItem.ToString().Trim(' ');
                if (flag == "学号")
                {
                    sql = "select * " +
                        "from " +table +
                        " where sno = '" + textBox2.Text + "'";
                }
                else if (flag == "书号")
                {
                    sql = "select * " +
                       "from " +table+
                       " where bno = '" + textBox2.Text + "'";
                }
                else if (flag == "借阅时间")
                {
                    if (radioButton1.Checked)
                    {
                        sql = "exec ad_query_b " + "'"+comboBox3.Text + "'" + ","+"'"+comboBox2.Text + "'" + "," + "'" + comboBox4.Text + "'";
                    }
                    else
                    {
                        sql = "exec ad_query_f " + "'" + comboBox3.Text + "'" + "," + "'" + comboBox2.Text + "'" + "," + "'" + comboBox4.Text + "'";
                    }
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
                dataGridView1.Columns[3].HeaderText = "还书时间";
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
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0 || comboBox1.SelectedIndex == 1)
            {
                comboBox3.Enabled = false;
                comboBox2.Enabled = false;
                comboBox4.Enabled = false;
                textBox2.Enabled = true;
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
                comboBox4.Enabled = true;
                textBox2.Enabled = false;
            }
            else { }
            
        }
    }
}
