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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            //this.tabControl1.ItemSize = new Size(100, 30);
            panel2.Visible = false;
            Name_Label();
            E_loadgrid();
            Co_loadgrid();
            P_loadgrid();
            Cu_loadgrid();
            O_loadgrid();
            B_loadgrid();



        }
        
        
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-H3TPEQS;Initial Catalog=Pharmacy;Integrated Security=True");

        public void Name_Label()
        {
            try
            {
                con.Open();
                SqlCommand cm2 = new SqlCommand("SELECT [Employee Name] FROM Employee WHERE Designation = 'Pharmacist'", con);
                string rt = (string)cm2.ExecuteScalar();
                con.Close();
                label2.Text = rt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void showmenu()
        {
            if (panel2.Visible == false)
            {
                panel2.Visible = true;
            }
            else
            {
                panel2.Visible = false;
            }
        }

        //All Loadgrids
        public void E_loadgrid()
        {
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT [Employee I.D],[Employee Name],Designation,Gender FROM Employee_View", con);
                DataSet ds = new DataSet();
                da.Fill(ds, "Employee_View");
                dataGridView1.DataSource = ds.Tables["Employee_View"].DefaultView;
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
        public void Co_loadgrid()
        {
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Company_View_Simple", con);
                DataSet ds = new DataSet();
                da.Fill(ds, "Company_View_Simple");
                dataGridView3.DataSource = ds.Tables["Company_View_Simple"].DefaultView;
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
        public void P_loadgrid()
        {
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("select A.[Product I.D],A.[Product Name],A.[Generic Name],A.Category,A.Pack,A.[Expiry Date],A.[M.R.P] as [Price],A.Stock,A.[Mfg. Date],B.[Company I.D],B.[Company Name] from Product A inner join Company B on A.[Company I.D]=B.[Company I.D]", con);
                DataSet ds = new DataSet();
                da.Fill(ds, "Product");
                dataGridView2.DataSource = ds.Tables["Product"].DefaultView;
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
        public void Cu_loadgrid()
        {
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Customer_View", con);
                DataSet ds = new DataSet();
                da.Fill(ds, "Customer_View");
                dataGridView4.DataSource = ds.Tables["Customer_View"].DefaultView;
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
        public void O_loadgrid()
        {
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [Order]", con);
                DataSet ds = new DataSet();
                da.Fill(ds, "Order");
                dataGridView5.DataSource = ds.Tables["Order"].DefaultView;
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


        public void B_loadgrid()
        {
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [Bill]", con);
                DataSet ds = new DataSet();
                da.Fill(ds, "Bill");
                dataGridView6.DataSource = ds.Tables["Bill"].DefaultView;
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


        //All Find Functions

        public void E_find()
        {


            if (textBox1.Text.Length == 0  && textBox2.Text == "" && textBox7.Text == "" && textBox9.Text == "")
            {
                MessageBox.Show("Please Enter Either I.D or Employee Name or Designation or Gender", "Alert..!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand com1 = new SqlCommand();
                    com1.Connection = con;
                    string query = "";
                    if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && textBox7.Text.Trim() != "")
                    {
                        query = "SELECT [Employee I.D],[Employee Name],Designation,Gender FROM Employee_View WHERE [Employee I.D] = @id or [Employee Name] LIKE @name or Designation LIKE @des or Gender LIKE @gen";
                        com1.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));
                        com1.Parameters.AddWithValue("@name", "%" + textBox2.Text.Trim() + "%");
                        com1.Parameters.AddWithValue("@des", "%" + textBox7.Text.Trim() + "%");
                        com1.Parameters.AddWithValue("@gen", "%" + textBox9.Text.Trim() + "%");
                    }
                    else if (textBox1.Text.Trim() != "")
                    {
                        query = "SELECT [Employee I.D],[Employee Name],Designation,Gender FROM Employee_View WHERE [Employee I.D] = @id";
                        com1.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));
                    }
                    else if (textBox2.Text.Trim() != "")
                    {
                        query = "SELECT [Employee I.D],[Employee Name],Designation,Gender FROM Employee_View WHERE [Employee Name] LIKE @name";
                        com1.Parameters.AddWithValue("@name", "%" + textBox2.Text.Trim() + "%");
                    }
                    else if (textBox7.Text.Trim() != "")
                    {
                        query = "SELECT [Employee I.D],[Employee Name],Designation,Gender FROM Employee_View WHERE Designation LIKE @des";
                        com1.Parameters.AddWithValue("@des", "%" + textBox7.Text.Trim() + "%");
                    }
                    else if (textBox9.Text.Trim() != "")
                    {
                        query = "SELECT [Employee I.D],[Employee Name],Designation,Gender FROM Employee_View WHERE Gender LIKE @gen";
                        com1.Parameters.AddWithValue("@gen", "%" + textBox9.Text.Trim() + "%");
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
                        MessageBox.Show("No data found..", "Missing Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    E_ClearAll();
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
        public void Co_find()
        {
            if (textBox25.Text.Length == 0 && textBox24.Text == "" )
            {
                MessageBox.Show("Please Enter Either Company I.D or Company Name or Ntn No. to find Company Detials", "Alert..!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand com2 = new SqlCommand();
                    com2.Connection = con;
                    string query2 = "";
                    if (textBox25.Text.Trim() != "" && textBox24.Text.Trim() != "" )
                    {
                        query2 = "SELECT * FROM Company_View_Simple WHERE [Company I.D] = @co_id or [Company Name] LIKE @co_name ";
                        com2.Parameters.AddWithValue("@co_id", int.Parse(textBox25.Text));
                        com2.Parameters.AddWithValue("@co_name", "%" + textBox24.Text.Trim() + "%");
                        
                    }
                    else if (textBox25.Text.Trim() != "")
                    {
                        query2 = "SELECT * FROM Company_View_Simple WHERE [Company I.D] = @co_id";
                        com2.Parameters.AddWithValue("@co_id", int.Parse(textBox25.Text));
                    }
                    else if (textBox24.Text.Trim() != "")
                    {
                        query2 = "SELECT * FROM Company_View_Simple WHERE [Company Name] LIKE @co_name";
                        com2.Parameters.AddWithValue("@co_name", "%" + textBox24.Text.Trim() + "%");
                    }
                    
                    com2.CommandText = query2;

                    SqlDataAdapter adapter2 = new SqlDataAdapter(com2);
                    DataTable dataTable2 = new DataTable();
                    adapter2.Fill(dataTable2);


                    if (dataTable2.Rows.Count > 0)
                    {
                        dataGridView3.DataSource = dataTable2;
                    }
                    else
                    {
                        dataGridView3.DataSource = dataTable2;
                        MessageBox.Show("No data found..", "Missing Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    Co_ClearAll();
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
        public void P_find()
        {
            if (textBox14.Text.Length == 0 && textBox12.Text == "" && textBox13.Text == "" && textBox17.Text == "" && textBox16.Text == "")
            {
                MessageBox.Show("Please Enter Either Product I.D or Product Name or Generic Name or Company I.D or Company Name to Find Products", "Alert..!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand com3 = new SqlCommand();
                    com3.Connection = con;
                    string query1 = "";
                    if (textBox14.Text.Trim() != "")
                    {
                        query1 = "select Product.[Product I.D],Product.[Product Name],Product.[Generic Name],Product.Category,Product.Pack,Product.[Expiry Date],Product.[M.R.P] as [Price],Product.Stock,Product.[Company I.D],Company.[Company Name] from product inner join Company on Product.[Company I.D] = Company.[Company I.D] where [Product I.D] = @pid";
                        com3.Parameters.AddWithValue("@pid", int.Parse(textBox14.Text));
                    }

                    else if (textBox13.Text.Trim() != "")
                    {
                        query1 = "select Product.[Product I.D],Product.[Product Name],Product.[Generic Name],Product.Category,Product.Pack,Product.[Expiry Date],Product.[M.R.P] as [Price],Product.Stock,Product.[Company I.D],Company.[Company Name] from product inner join Company on Product.[Company I.D] = Company.[Company I.D] where [Product Name] like @pname";
                        com3.Parameters.AddWithValue("@pname", "%" + textBox13.Text.Trim() + "%");
                    }
                    else if (textBox12.Text.Trim() != "")
                    {
                        query1 = "select Product.[Product I.D],Product.[Product Name],Product.[Generic Name],Product.Category,Product.Pack,Product.[Expiry Date],Product.[M.R.P] as [Price],Product.Stock,Product.[Company I.D],Company.[Company Name] from product inner join Company on Product.[Company I.D] = Company.[Company I.D] where [Generic Name] like @gname";
                        com3.Parameters.AddWithValue("@gname", "%" + textBox12.Text.Trim() + "%");
                    }
                    else if (textBox17.Text.Trim() != "")
                    {
                        query1 = "select Product.[Product I.D],Product.[Product Name],Product.[Generic Name],Product.Category,Product.Pack,Product.[Expiry Date],Product.[M.R.P] as [Price],Product.Stock,Product.[Company I.D],Company.[Company Name] from product inner join Company on Product.[Company I.D] = Company.[Company I.D] where Product.[Company I.D] = @cid";
                        com3.Parameters.AddWithValue("@cid", int.Parse(textBox17.Text));
                    }
                    else if (textBox16.Text.Trim() != "")
                    {
                        query1 = "select Product.[Product I.D],Product.[Product Name],Product.[Generic Name],Product.Category,Product.Pack,Product.[Expiry Date],Product.[M.R.P] as [Price],Product.Stock,Product.[Company I.D],Company.[Company Name] from product inner join Company on Product.[Company I.D]=Company.[Company I.D] where [Company Name] like @cname";
                        com3.Parameters.AddWithValue("@cname", "%" + textBox16.Text.Trim() + "%");
                    }
                    com3.CommandText = query1;

                    SqlDataAdapter adapter1 = new SqlDataAdapter(com3);
                    DataTable dataTable1 = new DataTable();
                    adapter1.Fill(dataTable1);


                    if (dataTable1.Rows.Count > 0)
                    {
                        dataGridView2.DataSource = dataTable1;
                    }
                    else
                    {
                        dataGridView2.DataSource = dataTable1;
                        MessageBox.Show("No data found..", "Missing Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    P_ClearAll();
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
        public void Cu_find()
        {
            if (textBox31.Text.Length == 0 && textBox33.Text.Length == 0 && textBox30.Text == "" && textBox29.Text == "" && textBox26.Text == "")
            {
                MessageBox.Show("Please Enter Either Customer I.D or Customer Name or Contact No. or Gender to find Customer Details", "Alert..!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand com4 = new SqlCommand();
                    com4.Connection = con;
                    string query3 = "";
                    if (textBox31.Text.Trim() != "")
                    {
                        query3 = "SELECT * FROM Customer_View WHERE [Customer I.D] = @cu_id";
                        com4.Parameters.AddWithValue("@cu_id", int.Parse(textBox31.Text));
                    }
                    else if (textBox30.Text.Trim() != "")
                    {
                        query3 = "SELECT * FROM Customer_View WHERE [Customer Name] like @cu_name";
                        com4.Parameters.AddWithValue("@cu_name", textBox30.Text);
                    }
                    else if (textBox29.Text.Trim() != "")
                    {
                        query3 = "SELECT * FROM Customer_View WHERE [Contact No.] = @cno";
                        com4.Parameters.AddWithValue("@cno", textBox29.Text);
                    }
                    else if (textBox26.Text.Trim() != "")
                    {
                        query3 = "SELECT * FROM Customer_View WHERE [Gender] like @gen";
                        com4.Parameters.AddWithValue("@gen", textBox26.Text);
                    }
                    if (textBox33.Text.Trim() != "")
                    {
                        query3 = "SELECT * FROM Customer WHERE [Age] = @age";
                        com4.Parameters.AddWithValue("@age", int.Parse(textBox33.Text));
                    }
                    com4.CommandText = query3;

                    SqlDataAdapter adapter3 = new SqlDataAdapter(com4);
                    DataTable dataTable3 = new DataTable();
                    adapter3.Fill(dataTable3);


                    if (dataTable3.Rows.Count > 0)
                    {
                        dataGridView4.DataSource = dataTable3;
                    }
                    else
                    {
                        dataGridView4.DataSource = dataTable3;
                        MessageBox.Show("No data found..", "Missing Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    Cu_ClearAll();
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
        public void O_find()
        {


            if (textBox38.Text.Length == 0 && textBox37.Text.Length == 0 && textBox3.Text.Length == 0 && textBox36.Text == "" && dateTimePicker1.Text.Length == 0)
            {
                MessageBox.Show("Please Enter Either Order I.D or Company I.D or Employee I.D or Status or Order Date  to find Order", "Alert..!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand com5 = new SqlCommand();
                    com5.Connection = con;
                    string query = "";
                    if (textBox38.Text.Trim() != "")
                    {
                        query = "SELECT * FROM [Order] WHERE [Order I.D] = @o_id";
                        com5.Parameters.AddWithValue("@o_id", int.Parse(textBox38.Text));
                    }
                    else if (textBox37.Text.Trim() != "")
                    {
                        query = "SELECT * FROM [Order] WHERE [Company I.D] = @co_id";
                        com5.Parameters.AddWithValue("@co_id", int.Parse(textBox37.Text));
                    }
                    else if (textBox3.Text.Trim() != "")
                    {
                        query = "SELECT * FROM [Order] WHERE [Employee I.D] = @e_id";
                        com5.Parameters.AddWithValue("@e_id", int.Parse(textBox3.Text));
                    }
                    else if (textBox36.Text.Trim() != "")
                    {
                        query = "SELECT * FROM [Order] WHERE [Status] like @status";
                        com5.Parameters.AddWithValue("@status", textBox36.Text);
                    }
                    else if (dateTimePicker1.Text.Trim() != "")
                    {
                        query = "SELECT * FROM [Order] WHERE [Order Date] = @date";
                        com5.Parameters.AddWithValue("@date", dateTimePicker1.Value.Date);
                    }

                    com5.CommandText = query;

                    SqlDataAdapter adapter3 = new SqlDataAdapter(com5);
                    DataTable dataTable3 = new DataTable();
                    adapter3.Fill(dataTable3);


                    if (dataTable3.Rows.Count > 0)
                    {
                        dataGridView5.DataSource = dataTable3;
                    }
                    else
                    {
                        dataGridView5.DataSource = dataTable3;
                        MessageBox.Show("No data found..", "Missing Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    O_ClearAll();
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




        public void B_find()
        {


            if (textBox4.Text.Length == 0 )
            {
                MessageBox.Show("Please Enter  Bill I.D to find Bill", "Alert..!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand com5 = new SqlCommand();
                    com5.Connection = con;
                    string query = "";
                    
                    query = "SELECT * FROM [Bill] WHERE [Bill I.D] = @o_id";
                    com5.Parameters.AddWithValue("@o_id", int.Parse(textBox4.Text));
                    com5.CommandText = query;

                    SqlDataAdapter adapter3 = new SqlDataAdapter(com5);
                    DataTable dataTable3 = new DataTable();
                    adapter3.Fill(dataTable3);


                    if (dataTable3.Rows.Count > 0)
                    {
                        dataGridView6.DataSource = dataTable3;
                    }
                    else
                    {
                        dataGridView6.DataSource = dataTable3;
                        MessageBox.Show("No data found..", "Missing Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //O_ClearAll();
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

        //All Clear_all Functions
        public void E_ClearAll()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox7.Clear();
            textBox9.Clear();
        }
        public void Co_ClearAll()
        {
            textBox25.Clear();
            textBox24.Clear();
            
        }
        public void P_ClearAll()
        {
            textBox12.Clear();
            textBox13.Clear();
            textBox14.Clear();
            textBox17.Clear();
            textBox16.Clear();
        }
        public void Cu_ClearAll()
        {
            textBox31.Clear();
            textBox30.Clear();
            textBox29.Clear();
            textBox27.Clear();
            textBox26.Clear();
            textBox28.Clear();
            textBox33.Clear();
        }
        public void O_ClearAll() 
        {
            textBox3.Clear();
            textBox38.Clear();
            textBox36.Clear();
            textBox37.Clear();
        }

        //All Customer Functions
        public void Cu_delete()
        {
            if (textBox31.TextLength == 0)
            {
                MessageBox.Show("Please Provide I.D for Delete operation", "Missing Item", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand command1 = new SqlCommand("DELETE FROM Customer WHERE [Customer I.D] = @cu_id", con);
                    command1.Parameters.AddWithValue("@cu_id", int.Parse(textBox31.Text));
                    int workdone = command1.ExecuteNonQuery();
                    if (workdone > 0)
                    {
                        MessageBox.Show("User deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("User not found.");
                    }
                    con.Close();
                    Cu_loadgrid();
                    Cu_ClearAll();
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
        public void Cu_Insert() 
        {
            if (textBox30.Text == "" && textBox26.Text == "" && textBox27.Text == "" && textBox29.Text == "" && textBox28.Text == "")
            {
                MessageBox.Show("Please Provide Name and Gender and Contact and Addeess for Insert operation", "Missing Item", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    string name1 = textBox30.Text;
                    string name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name1);
                    string gen1 = textBox26.Text;
                    string gen = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(gen1);
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("INSERT INTO Customer ([Customer Name], [Date of Birth], Gender) VALUES (@Name, @dob, @gen)", con);
                    cmd1.Parameters.AddWithValue("@Name", name);
                    cmd1.Parameters.AddWithValue("@dob", textBox27.Text);
                    cmd1.Parameters.AddWithValue("@gen", gen);
                    int rowsaffected = cmd1.ExecuteNonQuery();
                    if (rowsaffected > 0)
                    {
                        SqlCommand getIdCommand = new SqlCommand("SELECT @@IDENTITY", con);
                        int cu_Id = Convert.ToInt32(getIdCommand.ExecuteScalar());
                        
                        SqlCommand cmd2 = new SqlCommand("INSERT INTO Customer_Address ([Customer I.D], [Address]) VALUES (@cuid, @Address)", con);
                        cmd2.Parameters.AddWithValue("@cuid", cu_Id);
                        string[] addresses = textBox28.Text.Split(':');
                        foreach (string address in addresses)
                        {
                            cmd2.Parameters.Clear();
                            cmd2.Parameters.AddWithValue("@cuid", cu_Id);
                            cmd2.Parameters.AddWithValue("@Address", address.Trim());
                            cmd2.ExecuteNonQuery();
                        }
                        SqlCommand cmd3 = new SqlCommand("INSERT INTO Customer_Phone ([Customer I.D], [Contact No.]) VALUES (@cuid, @phone)", con);
                        cmd2.Parameters.AddWithValue("@cuid", cu_Id);
                        string[] phone = textBox29.Text.Split(':');
                        foreach (string number in phone)
                        {
                            cmd3.Parameters.Clear();
                            cmd3.Parameters.AddWithValue("@cuid", cu_Id);
                            cmd3.Parameters.AddWithValue("@phone", number.Trim());
                            cmd3.ExecuteNonQuery();
                        }
                        MessageBox.Show("Insert Successfully");
                    }
                    
                    else 
                    {
                        MessageBox.Show("Didn't Insert", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    Cu_loadgrid();
                    Cu_ClearAll();
                }
            }
        }
        public void Cu_Update() 
        {
            if (textBox31.TextLength == 0 && textBox30.Text == "" && textBox26.Text == "" && textBox27.Text == "" && textBox29.Text == "" && textBox28.Text == "")
            {
                MessageBox.Show("Please Provide I.D and Name and Gender and Contact and Address for Update operation", "Missing Item", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else 
            {
                try
                {
                    con.Open();
                    string sql = "UPDATE Customer SET [Customer Name] = @Name, [Date of Birth] = @dob, [Gender]=@gen WHERE [Customer I.D] = @cuid";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@Name", textBox30.Text);
                    cmd.Parameters.AddWithValue("@dob", textBox27.Text);
                    cmd.Parameters.AddWithValue("@gen", textBox26.Text);
                    cmd.Parameters.AddWithValue("@cuid", int.Parse(textBox31.Text));
                    cmd.ExecuteNonQuery();

                    string sql1 = "DELETE FROM Customer_Address WHERE [Customer I.D] = @cuid";
                    SqlCommand cmd1 = new SqlCommand(sql1, con);
                    cmd1.Parameters.AddWithValue("@cuid", int.Parse(textBox31.Text));
                    cmd1.ExecuteNonQuery();

                    string sql2 = "INSERT INTO Customer_Address ([Customer I.D], [Address]) VALUES (@cuid, @Address)";
                    SqlCommand cmd2 = new SqlCommand(sql2, con);
                    string[] addresses = textBox28.Text.Split(':');
                    foreach (string address in addresses)
                    {
                        cmd2.Parameters.Clear();
                        cmd2.Parameters.AddWithValue("@cuid", int.Parse(textBox31.Text));
                        cmd2.Parameters.AddWithValue("@Address", address);
                        cmd2.ExecuteNonQuery();
                    }

                    string sql3 = "DELETE FROM Customer_Phone WHERE [Customer I.D] = @cuid";
                    SqlCommand cmd3 = new SqlCommand(sql3, con);
                    cmd3.Parameters.AddWithValue("@cuid", int.Parse(textBox31.Text));
                    cmd3.ExecuteNonQuery();

                    string sql4 = "INSERT INTO Customer_Phone ([Customer I.D], [Contact No.]) VALUES (@cuid, @ph)";
                    SqlCommand cmd4 = new SqlCommand(sql4, con);
                    string[] Contacts = textBox29.Text.Split(':');
                    foreach (string phone in Contacts)
                    {
                        cmd4.Parameters.Clear();
                        cmd4.Parameters.AddWithValue("@cuid", int.Parse(textBox31.Text));
                        cmd4.Parameters.AddWithValue("@ph", phone);
                        cmd4.ExecuteNonQuery();
                    }
                    con.Close();
                    MessageBox.Show("Update Successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                    Cu_loadgrid();
                    Cu_ClearAll();
                }
            }

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            timer1.Start();
            this.tabControl1.ItemSize = new Size(178, 30);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label1.Text = DateTime.Now.ToString();
        }

        private void Menu_Click(object sender, EventArgs e)
        {
            showmenu();
        }

        private void logout_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.logout_Popup();
            this.Hide();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Bill_Details_Simple bd = new Bill_Details_Simple();
            bd.Show();
            this.Hide();
        }

        private void label35_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.lowStock();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            O_find();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Cu_find();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Cu_ClearAll();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Cu_delete();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Cu_Insert();
        }

        private void dataGridView4_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView4.Rows[e.RowIndex];
                textBox31.Text = row.Cells[0].Value.ToString();
                textBox30.Text = row.Cells[1].Value.ToString();
                textBox27.Text = row.Cells[2].Value.ToString();
                textBox26.Text = row.Cells[3].Value.ToString();
                textBox33.Text = row.Cells[4].Value.ToString();
                textBox28.Text = row.Cells[5].Value.ToString();
                textBox29.Text = row.Cells[6].Value.ToString();
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Cu_Update();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            E_find();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            E_ClearAll();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Co_find();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Co_ClearAll();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            P_find();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            P_ClearAll();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            E_loadgrid();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Co_loadgrid();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            P_loadgrid();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Cu_loadgrid();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            O_loadgrid();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            B_find();
            textBox4.Clear();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            B_loadgrid();
            textBox4.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bill_Detail_User bdu = new Bill_Detail_User();
            bdu.Show();
            this.Hide();
        }
    }
}
        
    

