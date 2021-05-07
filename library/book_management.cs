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
    public partial class book_management : Form
    {
        SqlDataAdapter sda;
        DataSet ds = new DataSet();
        public book_management()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string id = textBox8.Text;
            try
            {
                Program.command.CommandText = "exec delete_book '" + id.Trim(' ') + "'";
                Program.command.ExecuteNonQuery();
                Program.command.Dispose();
                MessageBox.Show("删除成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除错误！" + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ds.Clear();
            try
            {
                string flag;
                string sql = "";
                flag = comboBox1.SelectedItem.ToString().Trim(' ');
                if (flag == "作者")
                {
                    sql = "select * " +
                        "from book " +
                        "where author = '"+textBox9.Text+"'";
                }
                else if (flag == "书名")
                {
                    sql = "select * " +
                       "from book " +
                       "where bname = '" + textBox9.Text + "'";
                }
                else if (flag == "出版商")
                {
                    sql = "select * " +
                       "from book " +
                       "where publisher = '" + textBox9.Text + "'";
                }
                else if (flag == "类别")
                {
                    sql = "select * " +
                       "from book " +
                       "where category = '" + textBox9.Text + "'";
                }
                else
                {
                    MessageBox.Show("出错了");
                }
                sda = new SqlDataAdapter(sql, Program.connection);
                //确定执行sql


                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].HeaderText = "书号";
                dataGridView1.Columns[1].HeaderText = "书名";
                dataGridView1.Columns[2].HeaderText = "作者";
                dataGridView1.Columns[3].HeaderText = "价格";
                dataGridView1.Columns[4].HeaderText = "出版商";
                dataGridView1.Columns[5].HeaderText = "数量";
                dataGridView1.Columns[6].HeaderText = "类别";
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

        private void button4_Click(object sender, EventArgs e)
        {
            string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            try
            {
                Program.command.CommandText = "exec delete_book '" + id.Trim(' ') + "'";
                Program.command.ExecuteNonQuery();
                Program.command.Dispose();
                MessageBox.Show("删除成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除错误！" + ex.Message);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.command.CommandText = "exec add_book '" + textBox1.Text+"','"+textBox2.Text+
                "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text +
                "','" + textBox6.Text+"','" + comboBox2.SelectedItem.ToString() +"'";
            Program.command.ExecuteNonQuery();
            Program.command.Dispose();
            MessageBox.Show("添加成功");
        }
    }
}
