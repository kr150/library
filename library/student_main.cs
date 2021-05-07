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
    public partial class student_main : Form
    {
        public student_main()
        {
            InitializeComponent();
            Program.command.CommandText = "select * from student where sno = " + Form1.textBox1.Text;
            Program.thisSqlDataReader = Program.command.ExecuteReader();
            Program.thisSqlDataReader.Read();
            textBox3.Text = Program.thisSqlDataReader["sno"].ToString();
            textBox4.Text = Program.thisSqlDataReader["sname"].ToString();
            textBox5.Text = Program.thisSqlDataReader["balance"].ToString();
            textBox6.Text = Program.thisSqlDataReader["major"].ToString();
            textBox7.Text = Program.thisSqlDataReader["phone"].ToString();
            textBox8.Text = Program.thisSqlDataReader["sex"].ToString();
            textBox9.Text = Program.thisSqlDataReader["limitation"].ToString();
            Program.command.Dispose();
            Program.thisSqlDataReader.Close();
            textBox2.Enabled = false;
        }

        private void student_main_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)//asdfasdfasdfasdfasdfsd
        {
            history h = new history();
            h.ShowDialog();
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        #region 还书按钮
        private void button2_Click(object sender, EventArgs e)
        {
            Program.command.CommandText = $"exec gettime {Form1.textBox1.Text},{textBox1.Text}";
            Program.thisSqlDataReader = Program.command.ExecuteReader();

            Program.thisSqlDataReader.Read();
            if (Program.thisSqlDataReader.IsDBNull(Program.thisSqlDataReader.GetOrdinal("time")))
            {
                MessageBox.Show("未借阅过这本书！");
                Program.command.Dispose();
                Program.thisSqlDataReader.Close();
                button6.Enabled = true;
                button2.Enabled = false;
                return;
            }
            int fi = Convert.ToInt32(Program.thisSqlDataReader["time"]);

            if (fi > 30)
            {
                if((Convert.ToDouble(textBox5.Text) - 0.1 * (fi - 30.0)) > 0)
                {
                    textBox5.Text = (Convert.ToDouble(textBox5.Text) - 0.1 * (Convert.ToDouble(Program.thisSqlDataReader["time"]) - 30.0)).ToString();
                    Program.command.Dispose();
                    Program.command.CommandText = $"exec chaoshi '{Form1.textBox1.Text}','{textBox1.Text}'";
                    Program.command.ExecuteNonQuery();
                    MessageBox.Show($"已扣款{0.1 * (fi - 30.0)}元");
                }
                else
                {
                    MessageBox.Show("余额不足，请充值");
                    return;
                }

            }
            Program.command.Dispose();
            Program.thisSqlDataReader.Close();


            Program.command.CommandText = $"exec weichaoshi '{Form1.textBox1.Text}','{textBox1.Text}'";
            Program.command.ExecuteNonQuery();
            MessageBox.Show("还书成功");
            



            Program.command.Dispose();
            Program.thisSqlDataReader.Close();
            button6.Enabled = true;
            button2.Enabled = false;
        }
        #endregion

        #region 借书按钮
        private void button1_Click(object sender, EventArgs e)
        {
            //判断是否还有借书名额
            Program.command.CommandText = $"select * from student where sno = '{Form1.textBox1.Text}'";
            Program.thisSqlDataReader = Program.command.ExecuteReader();
            Program.thisSqlDataReader.Read();
            if(Convert.ToInt32(Program.thisSqlDataReader["limitation"].ToString()) < 0)
            {
                MessageBox.Show("可借数量不足，请先看完手中的书！");
                Program.command.Dispose();
                Program.thisSqlDataReader.Close();
                return;
            }
            //判断余额是否充足，保证有余额才能借阅
            else if(Convert.ToDouble(Program.thisSqlDataReader["balance"].ToString()) < 0)
            {
                MessageBox.Show("余额不足，请先充值！");
                Program.command.Dispose();
                Program.thisSqlDataReader.Close();
                return;
            }
            Program.command.Dispose();
            Program.thisSqlDataReader.Close();

            //判断图书库存是否足够
            Program.command.CommandText = $"select * from book where bno = '{textBox10.Text}'" ;
            Program.thisSqlDataReader = Program.command.ExecuteReader();
            Program.thisSqlDataReader.Read();
            if (Convert.ToInt32(Program.thisSqlDataReader["quantity"].ToString()) <= 0)
            {
                MessageBox.Show("该书在册数量不足，借阅失败！");
                Program.command.Dispose();
                Program.thisSqlDataReader.Close();
                return;
            }
            Program.command.Dispose();
            Program.thisSqlDataReader.Close();
            //执行借书
            Program.command.CommandText = $"exec browbook '{Form1.textBox1.Text}','{textBox10.Text}'";
            Program.command.ExecuteNonQuery();
            MessageBox.Show("借阅成功！");
            Program.command.Dispose();
            Program.thisSqlDataReader.Close();
        }

        #endregion

        #region 更新按钮
        private void button6_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            Program.command.CommandText = $"select * from borrow_book where sno = {Form1.textBox1.Text} " +
                "and return_date = '1900-01-01 00:00:00.000'" ;
            Program.thisSqlDataReader = Program.command.ExecuteReader();
            
            while (Program.thisSqlDataReader.Read())
            {
                textBox2.Text += Program.thisSqlDataReader["sno"].ToString() + Program.thisSqlDataReader["bno"].ToString() + Program.thisSqlDataReader["borrow_date"].ToString () + "\r\n";
            } 
            Program.command.Dispose();
            Program.thisSqlDataReader.Close();
            button2.Enabled = true;
            button6.Enabled = false;
        }


        #endregion

        #region 充值按钮
        private void button5_Click(object sender, EventArgs e)
        {
            charge c = new charge();
            c.ShowDialog();
            if(c.DialogResult == DialogResult.OK)
            {
                Program.command.CommandText = $"exec recharge {Form1.textBox1.Text},{c.textBox1.Text}";
                Program.command.ExecuteNonQuery();
                textBox5.Text = (Convert.ToDouble(textBox5.Text) + Convert.ToDouble(c.textBox1.Text)).ToString();
                Program.command.Dispose();
                Program.thisSqlDataReader.Close();
            }
            else if(c.DialogResult == DialogResult.Cancel)
            {
                MessageBox.Show("充值取消！");
            }
            
        }

        #endregion

        #region 罚款信息
        private void button4_Click(object sender, EventArgs e)
        {
            fine f = new fine();
            f.ShowDialog();
        }
        #endregion

        #region 密码修改
        private void button7_Click(object sender, EventArgs e)
        {
            newpassword n = new newpassword();
            n.ShowDialog();
            if (n.DialogResult == DialogResult.OK)
            {
                Program.command.CommandText = $"update student set spassword = {n.textBox1.Text} where sno = {Form1.textBox1.Text}";
                Program.command.ExecuteNonQuery();
                Program.command.Dispose();
            }
            else if (n.DialogResult == DialogResult.Cancel)
            {
                MessageBox.Show("修改取消！");
            }
        }
        #endregion
    }
}
