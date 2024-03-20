using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Pharmacy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            System.Drawing.Drawing2D.GraphicsPath obj = new System.Drawing.Drawing2D.GraphicsPath();
            obj.AddEllipse(0, 0, flowLayoutPanel1.Width, flowLayoutPanel1.Height);
            Region rg = new Region(obj);
            flowLayoutPanel1.Region = rg;
            string hexcolor = "#B7EAF7";
            Color myColor = System.Drawing.ColorTranslator.FromHtml(hexcolor);
            panel1.BackColor = myColor;
            login_button.BackColor = myColor;
            textBox2.UseSystemPasswordChar = true;
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-H3TPEQS;Initial Catalog=Pharmacy;Integrated Security=True");
        public string emp_Name;
        public void Log_Button()
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Kindly Provide Username and Password  !!!", "Missing Item..!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (int.TryParse(textBox1.Text, out int number))
            {

                try
                {
                    con.Open();
                    SqlCommand cmd_Login = new SqlCommand("Select Designation from Employee where [Employee I.D]=@id and [Password]=@pass collate Latin1_General_CS_AS ", con);
                    cmd_Login.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));
                    cmd_Login.Parameters.AddWithValue("@pass", textBox2.Text);
                    string des = (string)cmd_Login.ExecuteScalar();
                    if (des == null)
                    {
                        MessageBox.Show("Wrong Credentials", "Error");
                    }
                    else
                    {
                        if (des == "Admin")
                        {
                            Form2 f2 = new Form2();
                            f2.Show();
                            this.Hide();
                            MessageBox.Show("Login Successful", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (des == "Pharmacist")
                        {
                            Form3 f3 = new Form3();
                            f3.Show();
                            this.Hide();
                            MessageBox.Show("Login Successful", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("You don't have Access", "Access Denied");
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Please Input Correct I.D Format...!!", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void name_login()
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("Select [Employee Name] from Employee where [Employee I.D]=@id", con);
                cmd.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));
                emp_Name = (string)cmd.ExecuteScalar();
                Form2 fr2 = new Form2();
                fr2.TextBoxValue = emp_Name;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            con.Close();

        }


        private void label1_Click_1(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        
        
        private void Login_button_Click(object sender, EventArgs e)
        {
            Log_Button();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Form4 tu = new Form4();
            tu.Show();
            this.Hide();
            /*
            Form2 hu = new Form2();
            hu.Show();
            this.Hide();
            




            Form3 t = new Form3();
            t.Show();
            this.Hide();
            */

            /*
            Form5 f5 = new Form5();
            f5.Show();
            this.Hide();
            */
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        
    }
}
