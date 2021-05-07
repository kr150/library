using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace library
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

    private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("输入不完整！");
                return;
            }
            if (radioButton1.Checked == true)
            {
                //学生登录检查
                Program.command.CommandText = "select * from student where sno = " + textBox1.Text;
                Program.thisSqlDataReader = Program.command.ExecuteReader();
                //查询不到该账号
                if (!Program.thisSqlDataReader.HasRows)
                {
                    MessageBox.Show("查无此人");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    Program.command.Dispose();
                    Program.thisSqlDataReader.Close();
                    return;
                }
                //检查密码
                Program.thisSqlDataReader.Read();
                if (Program.thisSqlDataReader["spassword"].ToString().Trim(' ') == textBox2.Text)
                    {
                    Program.command.Dispose();
                    Program.thisSqlDataReader.Close();
                    student_main s = new student_main();
                    this.Hide();
                    s.ShowDialog();
                    this.Show();

                }
                else
                {
                    MessageBox.Show("学生登录有问题！");
                    Program.command.Dispose();
                    Program.thisSqlDataReader.Close();
                }
            }            
            else if(radioButton2.Checked == true)
            {
                //管理员登录检查
                Program.command.CommandText = "select * from Administrators where number = " + textBox1.Text;
                Program.thisSqlDataReader = Program.command.ExecuteReader();
                 //查找不到该账号
                if (!Program.thisSqlDataReader.HasRows)
                 {
                    MessageBox.Show("查无此人");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    Program.command.Dispose();
                    Program.thisSqlDataReader.Close();
                    return;
                }
                //检查密码
                Program.thisSqlDataReader.Read();
                if (Program.thisSqlDataReader["ad_password"].ToString().Trim(' ') == textBox2.Text)
                {
                    Program.command.Dispose();
                    Program.thisSqlDataReader.Close();
                    ad_main a = new ad_main();
                    this.Hide();
                    a.ShowDialog();
                    this.Show();

                }
                else
                 {
                    MessageBox.Show("管理员登录有问题,请联系技术处！");
                    Program.command.Dispose();
                    Program.thisSqlDataReader.Close();
                }
                //未选定角色
            }
            else
            {
                MessageBox.Show("为选定身份！");
            }
        }

        //学生账号注册
        private void button2_Click(object sender, EventArgs e)
        {
            zhuce s = new zhuce();
            s.ShowDialog();
            if (s.DialogResult == DialogResult.OK)
            {
                Program.command.CommandText = "exec create_student " + s.textBox1.Text + "," + s.textBox2.Text + ","
                    + s.textBox3.Text + "," + s.textBox4.Text + "," + s.textBox5.Text + "," + s.textBox6.Text + ","
                    + s.textBox7.Text + "," + s.textBox8.Text;
                Program.command.ExecuteNonQuery();
            }
            s.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        

        protected static void OnInfoMessage(
          object sender, SqlInfoMessageEventArgs args)
        {
            foreach (SqlError err in args.Errors)
            {
                MessageBox.Show($"The {err.Source} has received a severity {err.Class}, state {err.State} error number {err.Number}\n" +
              $"on line {err.LineNumber} of procedure {err.Procedure} on server {err.Server}:\n{err.Message}");
            }
        }
    }
}
