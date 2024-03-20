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
using System.Globalization;

namespace Pharmacy
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            loadgrid();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-H3TPEQS;Initial Catalog=PMS;Integrated Security=True");
        public void loadgrid()
        {
            /*
               SqlCommand cmd = new SqlCommand("Select * from Employee_Table",con);
               DataTable dt = new DataTable();
               con.Open();
               SqlDataReader sdr = cmd.ExecuteReader();
               dt.Load(sdr);
               con.Close();
               DataGrid.datasource = dt.DefaultView;
            //Different he ye   
        */
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [check]", con);
                DataSet ds = new DataSet();
                da.Fill(ds, "[check]");
                dataGridView1.DataSource = ds.Tables["[check]"].DefaultView;
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
        public void delete_emp()
        {
            if (textBox1.TextLength == 0)
            {
                MessageBox.Show("Please Provide I.D for delete operation", "Missing Item", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand sqlc = new SqlCommand("SELECT [Type] FROM [check] WHERE id = @Id", con);
                    sqlc.Parameters.AddWithValue("@Id", int.Parse(textBox1.Text));
                    string des = (string)sqlc.ExecuteScalar();
                    if (des == "Admin")
                    {
                        MessageBox.Show("You Can't Delete Admin's Data");
                    }
                    else
                    {
                        SqlCommand command2 = new SqlCommand("DELETE FROM [check] WHERE id = @Id", con);
                        command2.Parameters.AddWithValue("@Id", int.Parse(textBox1.Text));
                        int workdone = command2.ExecuteNonQuery();
                        if (workdone > 0)
                        {
                            MessageBox.Show("User deleted successfully.");
                        }
                        else
                        {
                            MessageBox.Show("User not found.");
                        }


                    }
                    con.Close();
                    loadgrid();
                    textBox1.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "Delete operation didn't perform..!!");
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public void Clear_All() 
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

        }

        public void find_emp() 
        {
            if (textBox1.Text.Length == 0 && textBox2.Text == "" && textBox4.Text == "")
            {
                MessageBox.Show("Please Enter Either I.D or Employee Name or Designation", "Alert..!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand com1 = new SqlCommand();
                    com1.Connection = con;
                    string query = "";
                    if (textBox1.Text.Trim() != "")
                    {
                        query = "SELECT * FROM [check] WHERE id = @id";
                        com1.Parameters.AddWithValue("@id", textBox1.Text.Trim());
                    }
                    else if (textBox2.Text.Trim() != "")
                    {
                        query = "SELECT * FROM [check] WHERE Name LIKE @name";
                        com1.Parameters.AddWithValue("@name", "%" + textBox2.Text.Trim() + "%");
                    }
                    else if (textBox4.Text.Trim() != "")
                    {
                        query = "SELECT * FROM [check] WHERE [Type] LIKE @type";
                        com1.Parameters.AddWithValue("@type", "%" + textBox4.Text.Trim() + "%");
                    }
                    com1.CommandText = query;

                    SqlDataAdapter adapter = new SqlDataAdapter(com1);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dataTable;
                    }
                    else
                    {
                        dataGridView1.DataSource = dataTable;
                        MessageBox.Show("No data found", "Missing Item");
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

        }


        public void update_emp() 
        {
            if (textBox1.TextLength==0 || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Please Fill the Field to Update Data...!!");
            }
            else 
            {
                try
                {
                    con.Open();
                    SqlCommand cmd_u = new SqlCommand("Update [check] SET [Name] = @Name, pass = @password, [Type] = @designation where id = @id", con);
                    cmd_u.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));
                    cmd_u.Parameters.AddWithValue("@Name", textBox2.Text);
                    cmd_u.Parameters.AddWithValue("@password", textBox3.Text);
                    cmd_u.Parameters.AddWithValue("@designation", textBox4.Text);
                    cmd_u.ExecuteNonQuery();
                    MessageBox.Show("Record has been Updated", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally 
                {
                    con.Close();
                    Clear_All();
                    loadgrid();
                
                }
            
            }
        
        
        }

        public string name = "";
        public bool isempty_Ins() 
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Password is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Designation is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;

        }

        public void insert_emp() 
        {
            
            if (isempty_Ins()) 
            {
                try
                {
                    string name1=textBox2.Text;
                    string name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name1);
                    
                    if (string.IsNullOrEmpty(textBox2.Text))
                    {
                        name = "None";
                    }
                    
                    string des1 = textBox4.Text;
                    string des = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(des1);
                    con.Open();
                    SqlCommand cmd_i = new SqlCommand("Insert into [check] (Name,Pass,[Type]) Values (@Name, @Password, @Designation)", con);
                    cmd_i.Parameters.AddWithValue("@Name", name);
                    cmd_i.Parameters.AddWithValue("@password", textBox3.Text);
                    cmd_i.Parameters.AddWithValue("@Designation", des);
                    cmd_i.ExecuteNonQuery();
                    MessageBox.Show("Insert Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally 
                {
                    con.Close();
                    loadgrid();
                    Clear_All();
                }

                }


        }
        
        
        private void Form4_Load(object sender, EventArgs e)
        {

        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            delete_emp();
            Clear_All();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row3 = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row3.Cells[0].Value.ToString();
                textBox2.Text = row3.Cells[1].Value.ToString();
                textBox3.Text = row3.Cells[2].Value.ToString();
                textBox4.Text = row3.Cells[3].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            find_emp();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            loadgrid();
            Clear_All();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            update_emp();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            insert_emp();
        }
    }
}
