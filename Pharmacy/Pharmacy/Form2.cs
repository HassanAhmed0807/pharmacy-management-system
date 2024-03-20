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
using System.Data.SqlTypes;
using System.Globalization;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;


namespace Pharmacy
{
    public partial class Form2 : Form
    {
        //Bill Vreation from here to
        //private SqlConnection con;
        private SqlCommand command;
        private SqlDataReader reader1;
        private string query;
        DataTable billData;
        DataTable customerData;
        DataTable empData;
        DataTable productData;
        private static int flag = 0;
        // here


        public Form2()
        {
            InitializeComponent();
            E_loadgrid();
            P_loadgrid();
            Co_loadgrid();
            Cu_loadgrid();
            O_loadgrid();
            panel2.Visible = false;
            Name_Label();

            //For Bill Creation from here to
            flag = 0;
            this.billData = new DataTable();
            this.customerData = new DataTable();
            this.empData = new DataTable();
            this.productData = new DataTable();
            //string connectionString = "Data Source = DESKTOP - H3TPEQS; Initial Catalog = Pharmacy; Integrated Security = True";
            //con = new SqlConnection(connectionString);
            //SqlConnection con = new SqlConnection("Data Source=DESKTOP-H3TPEQS;Initial Catalog=Pharmacy;Integrated Security=True");
            command = new SqlCommand();
            command.Connection = con;
            //loadCustandEmp();
            comboShow();
            comboShowEmp();
            comboShowCus();

            //here


        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-H3TPEQS;Initial Catalog=Pharmacy;Integrated Security=True");
        public string TextBoxValue { get; set; }
        public int value { get; set; }

        public class Product 
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Stock { get; set; }
            public string Category { get; set; }
        }

        private List<Product> getStock()
        {
            
                List<Product> lowStockProducts = new List<Product>();

            // connect to the database and retrieve products with stock less than 2
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Product WHERE Stock < 3", con);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product();
                    product.Id = (int)reader["Product I.D"];
                    product.Name = reader["Product Name"].ToString();
                    product.Stock = (int)reader["Stock"];
                    product.Category = reader["Category"].ToString();
                    // add the product to the list
                    lowStockProducts.Add(product);
                }
                reader.Close();
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
            return lowStockProducts;
            
            
        }
        public void lowStock()
        {
            List<Product> lowStockProducts = getStock();
            if (lowStockProducts.Count > 0)
            {
                string message = "The following products have stock less than 3:\n \n";
                foreach (Product product in lowStockProducts)
                {
                    message += product.Id + "\t" + product.Name + "\t" + product.Category + "\n";
                }
                MessageBox.Show(message, "Low Stock Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        

         public void Name_Label() 
        {
            try
            {
                con.Open();
                SqlCommand cm2 = new SqlCommand("SELECT [Employee Name] FROM Employee WHERE Designation = 'Admin'", con);
                string rt = (string)cm2.ExecuteScalar();
                con.Close();
                label1.Text = rt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pMSDataSet.Employee_Table' table. You can move, or remove it, as needed.
            //this.employee_TableTableAdapter.Fill(this.pMSDataSet.Employee_Table);
            timer1.Start();
            this.tabControl1.ItemSize = new Size(178, 30);
            
        }

        //All Empty_Insert Functions
        public bool E_isempty_Ins() 
        {
            if (textBox2.Text == string.Empty) 
            {
                MessageBox.Show("Name is Required.", "Missing", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            if (textBox3.Text == string.Empty)
            {
                MessageBox.Show("Password is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox4.Text == string.Empty)
            {
                MessageBox.Show("Date of Birth is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox5.Text == string.Empty)
            {
                MessageBox.Show("Contact No. is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox6.Text == string.Empty)
            {
                MessageBox.Show("Cnic is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox7.Text == string.Empty)
            {
                MessageBox.Show("Designation is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox8.Text == string.Empty)
            {
                MessageBox.Show("Date of Join is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox35.Text == string.Empty)
            {
                MessageBox.Show("Address is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox9.Text == string.Empty)
            {
                MessageBox.Show("Gender is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox10.Text.Length == 0)
            {
                MessageBox.Show("Salary is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;

        }
        public bool Co_isempty_Ins() 
        {
            if (textBox24.Text == string.Empty)
            {
                MessageBox.Show("Company Name is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox22.Text == string.Empty)
            {
                MessageBox.Show("Ntn No. is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox23.Text == string.Empty)
            {
                MessageBox.Show("Contact No. is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox40.Text == string.Empty)
            {
                MessageBox.Show("Address is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public bool P_isempty_Ins() 
        {
            if (textBox13.Text == string.Empty)
            {
                MessageBox.Show("Product Name is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox11.Text == string.Empty)
            {
                MessageBox.Show("Category is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox34.Text == string.Empty)
            {
                MessageBox.Show("Mfg. Date is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox20.Text.Length == 0)
            {
                MessageBox.Show("T.P is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox19.Text.Length == 0)
            {
                MessageBox.Show("M.R.P is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox15.Text.Length == 0)
            {
                MessageBox.Show("Stock is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox17.Text.Length == 0)
            {
                MessageBox.Show("Company I.D is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        
        //All Empty_Update Functions
        public bool E_isempty_Upd()
        {
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("Employee I.D is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox2.Text == string.Empty)
            {
                MessageBox.Show("Name is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox3.Text == string.Empty)
            {
                MessageBox.Show("Password is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox4.Text == string.Empty)
            {
                MessageBox.Show("Date of Birth is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox5.Text == string.Empty)
            {
                MessageBox.Show("Contact No. is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox35.Text == string.Empty)
            {
                MessageBox.Show("Address is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox6.Text == string.Empty)
            {
                MessageBox.Show("Cnic is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox7.Text == string.Empty)
            {
                MessageBox.Show("Designation is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox8.Text == string.Empty)
            {
                MessageBox.Show("Date of Join is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox9.Text == string.Empty)
            {
                MessageBox.Show("Gender is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox10.Text.Length == 0)
            {
                MessageBox.Show("Salary is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;

        }
        public bool Co_isempty_Upd()
        {
            if (textBox25.Text.Length == 0)
            {
                MessageBox.Show("Company I.D is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox24.Text == string.Empty)
            {
                MessageBox.Show("Company Name is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox22.Text == string.Empty)
            {
                MessageBox.Show("Ntn No. is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox40.Text == string.Empty)
            {
                MessageBox.Show("Address is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox23.Text == string.Empty)
            {
                MessageBox.Show("Contact No. is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public bool P_isempty_Upd()
        {
            if (textBox14.Text.Length == 0)
            {
                MessageBox.Show("Product I.D is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox13.Text == string.Empty)
            {
                MessageBox.Show("Product Name is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox34.Text == string.Empty)
            {
                MessageBox.Show("Mfg Date is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox20.Text.Length == 0)
            {
                MessageBox.Show("T.P is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox19.Text.Length == 0)
            {
                MessageBox.Show("M.R.P is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox15.Text.Length == 0)
            {
                MessageBox.Show("Stock is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox17.Text.Length == 0)
            {
                MessageBox.Show("Company I.D is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public bool Cu_isempty_Upd()
        {
            if (textBox2.Text.Length == 0)
            {
                MessageBox.Show("Customer I.D is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox30.Text == string.Empty)
            {
                MessageBox.Show("Customer Name is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox26.Text == string.Empty)
            {
                MessageBox.Show("Gender is Required.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        
        //All Loadgrid Functions
        public void E_loadgrid() 
        {
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Employee_View", con);
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
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Company_View", con);
                DataSet ds = new DataSet();
                da.Fill(ds, "Company_View");
                dataGridView3.DataSource = ds.Tables["Company_View"].DefaultView;
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
                SqlDataAdapter da = new SqlDataAdapter("select A.[Product I.D],A.[Product Name],A.[Generic Name],A.Category,A.Pack,A.[Expiry Date],A.[T.P],A.[M.R.P],A.Stock,A.[Mfg. Date],B.[Company I.D],B.[Company Name] from Product A inner join Company B on A.[Company I.D]=B.[Company I.D]", con);
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

        //All Clear Functions
        public void E_ClearAll()
        {

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox32.Clear();
            textBox35.Clear();


        }
        public void Co_ClearAll()
        {
            textBox25.Clear();
            textBox24.Clear();
            textBox23.Clear();
            textBox22.Clear();
            textBox40.Clear();
        }
        public void P_ClearAll()
        {

            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();
            textBox14.Clear();
            textBox15.Clear();
            textBox16.Clear();
            textBox17.Clear();
            textBox18.Clear();
            textBox19.Clear();
            textBox20.Clear();
            textBox21.Clear();
            textBox34.Clear();
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
            textBox38.Clear();
            textBox36.Clear();
            textBox37.Clear();
            textBox39.Clear();
            
        }


        //All Find Functions
        public void E_find()
        {


            if (textBox1.Text.Length == 0 && textBox32.Text.Length == 0 && textBox2.Text == "" && textBox7.Text == "" && textBox9.Text == "")
            {
                MessageBox.Show("Please Enter Either I.D or Employee Name or Designation or Age or Gender", "Alert..!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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
                        query = "SELECT * FROM Employee_View WHERE [Employee I.D] = @id or [Employee Name] LIKE @name or Designation LIKE @des";
                        com1.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));
                        com1.Parameters.AddWithValue("@name", "%" + textBox2.Text.Trim() + "%");
                        com1.Parameters.AddWithValue("@des", "%" + textBox7.Text.Trim() + "%");
                    }
                    else if (textBox1.Text.Trim() != "")
                    {
                        query = "SELECT * FROM Employee_View WHERE [Employee I.D] = @id";
                        com1.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));
                    }
                    else if (textBox2.Text.Trim() != "")
                    {
                        query = "SELECT * FROM Employee_View WHERE [Employee Name] LIKE @name";
                        com1.Parameters.AddWithValue("@name", "%" + textBox2.Text.Trim() + "%");
                    }
                    else if (textBox7.Text.Trim() != "")
                    {
                        query = "SELECT * FROM Employee_View WHERE Designation LIKE @des";
                        com1.Parameters.AddWithValue("@des", "%" + textBox7.Text.Trim() + "%");
                    }
                    else if (textBox9.Text.Trim() != "")
                    {
                        query = "SELECT * FROM Employee_View WHERE Gender LIKE @gen";
                        com1.Parameters.AddWithValue("@gen", "%" + textBox9.Text.Trim() + "%");
                    }
                    else if (textBox32.Text.Trim() != "")
                    {
                        query = "SELECT * FROM Employee_View WHERE [Age] = @age";
                        com1.Parameters.AddWithValue("@age", int.Parse(textBox32.Text));
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
            if (textBox25.Text.Length == 0 && textBox24.Text == "" && textBox22.Text == "")
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
                    if (textBox25.Text.Trim() != "" && textBox24.Text.Trim() != "" && textBox22.Text.Trim() != "")
                    {
                        query2 = "SELECT * FROM Company_View WHERE [Company I.D] = @co_id or [Company Name] LIKE @co_name or [Company Ntn No.] LIKE @ntn";
                        com2.Parameters.AddWithValue("@co_id", int.Parse(textBox25.Text));
                        com2.Parameters.AddWithValue("@co_name", "%" + textBox24.Text.Trim() + "%");
                        com2.Parameters.AddWithValue("@ntn", "%" + textBox22.Text.Trim() + "%");
                    }
                    else if (textBox25.Text.Trim() != "")
                    {
                        query2 = "SELECT * FROM Company_View WHERE [Company I.D] = @co_id";
                        com2.Parameters.AddWithValue("@co_id", int.Parse(textBox25.Text));
                    }
                    else if (textBox24.Text.Trim() != "")
                    {
                        query2 = "SELECT * FROM Company_View WHERE [Company Name] LIKE @co_name";
                        com2.Parameters.AddWithValue("@co_name", "%" + textBox24.Text.Trim() + "%");
                    }
                    else if (textBox22.Text.Trim() != "")
                    {
                        query2 = "SELECT * FROM Company_View WHERE [Company Ntn No.] LIKE @ntn";
                        com2.Parameters.AddWithValue("@ntn", "%" + textBox22.Text.Trim() + "%");
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
                        query1 = "select Product.[Product I.D],Product.[Product Name],Product.[Generic Name],Product.Category,Product.Pack,Product.[Expiry Date],Product.[T.P],Product.[M.R.P],Product.Stock,Product.[Company I.D],Company.[Company Name] from product inner join Company on Product.[Company I.D] = Company.[Company I.D] where [Product I.D] = @pid";
                        com3.Parameters.AddWithValue("@pid",int.Parse(textBox14.Text));
                    }

                    else if (textBox13.Text.Trim() != "")
                    {
                        query1 = "select Product.[Product I.D],Product.[Product Name],Product.[Generic Name],Product.Category,Product.Pack,Product.[Expiry Date],Product.[T.P],Product.[M.R.P],Product.Stock,Product.[Company I.D],Company.[Company Name] from product inner join Company on Product.[Company I.D] = Company.[Company I.D] where [Product Name] like @pname";
                        com3.Parameters.AddWithValue("@pname","%" + textBox13.Text.Trim() + "%");
                    }
                    else if (textBox12.Text.Trim() != "")
                    {
                        query1 = "select Product.[Product I.D],Product.[Product Name],Product.[Generic Name],Product.Category,Product.Pack,Product.[Expiry Date],Product.[T.P],Product.[M.R.P],Product.Stock,Product.[Company I.D],Company.[Company Name] from product inner join Company on Product.[Company I.D] = Company.[Company I.D] where [Generic Name] like @gname";
                        com3.Parameters.AddWithValue("@gname","%" + textBox12.Text.Trim() + "%");
                    }
                    else if (textBox17.Text.Trim() != "")
                    {
                        query1 = "select Product.[Product I.D],Product.[Product Name],Product.[Generic Name],Product.Category,Product.Pack,Product.[Expiry Date],Product.[T.P],Product.[M.R.P],Product.Stock,Product.[Company I.D],Company.[Company Name] from product inner join Company on Product.[Company I.D] = Company.[Company I.D] where Product.[Company I.D] = @cid";
                        com3.Parameters.AddWithValue("@cid", int.Parse(textBox17.Text));
                    }
                    else if (textBox16.Text.Trim() != "")
                    {
                        query1 = "select Product.[Product I.D],Product.[Product Name],Product.[Generic Name],Product.Category,Product.Pack,Product.[Expiry Date],Product.[T.P],Product.[M.R.P],Product.Stock,Product.[Company I.D],Company.[Company Name] from product inner join Company on Product.[Company I.D]=Company.[Company I.D] where [Company Name] like @cname";
                        com3.Parameters.AddWithValue("@cname","%" + textBox16.Text.Trim() + "%");
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
            
            
                if (textBox38.Text.Length == 0 && textBox37.Text.Length == 0 && textBox39.Text.Length == 0 && textBox36.Text == "" && dateTimePicker1.Text.Length==0)
                {
                    MessageBox.Show("Please Enter Either Order I.D or Company I.D or Employee I.D or Status or Order Date to find Order", "Alert..!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        else if (textBox39.Text.Trim() != "")
                        {
                            query = "SELECT * FROM [Order] WHERE [Employee I.D] = @e_id";
                            com5.Parameters.AddWithValue("@e_id", int.Parse(textBox39.Text));
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
                        textBox38.Clear();
                        textBox36.Clear();
                        textBox37.Clear();
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

        //All Delete Functions
        public void E_delete() 
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
                    SqlCommand sqlc = new SqlCommand("SELECT [Designation] FROM Employee WHERE [Employee I.D] = @e_id", con);
                    sqlc.Parameters.AddWithValue("@e_id", int.Parse(textBox1.Text));
                    string des = (string)sqlc.ExecuteScalar();
                    if (des == "Admin")
                    {
                        MessageBox.Show("You Can't Delete Admin's Data");
                    }
                    else
                    {
                        SqlCommand command4 = new SqlCommand("DELETE FROM Employee WHERE [Employee I.D] = @id", con);
                        command4.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));
                        int workdone = command4.ExecuteNonQuery();
                        if (workdone > 0)
                        {
                            MessageBox.Show("Employee deleted successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Employee not found.");
                        }


                    }
                    con.Close();
                    E_loadgrid();
                    E_ClearAll();
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
        public void Co_delete() 
        {
            if (textBox25.TextLength == 0)
            {
                MessageBox.Show("Please Provide I.D for Delete operation", "Missing Item", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand command3 = new SqlCommand("DELETE FROM Company WHERE [Company I.D] = @co_id", con);
                    command3.Parameters.AddWithValue("@co_id", int.Parse(textBox25.Text));
                    int workdone = command3.ExecuteNonQuery();
                    if (workdone > 0)
                    {
                        MessageBox.Show("Company deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Company not found.");
                    }
                    con.Close();
                    Co_loadgrid();
                    Co_ClearAll();
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
        public void P_delete() 
        {
            if (textBox14.TextLength == 0)
            {
                MessageBox.Show("Please Provide I.D for Delete operation", "Missing Item", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand command2 = new SqlCommand("DELETE FROM Product WHERE [Product I.D] = @p_id", con);
                    command2.Parameters.AddWithValue("@p_id", int.Parse(textBox14.Text));
                    int workdone = command2.ExecuteNonQuery();
                    if (workdone > 0)
                    {
                        MessageBox.Show("Product deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Product not found.");
                    }
                    con.Close();
                    P_loadgrid();
                    P_ClearAll();
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
        public void O_delete() 
        {
            if (textBox38.TextLength == 0)
            {
                MessageBox.Show("Please Provide I.D for Delete operation", "Missing Item", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand command1 = new SqlCommand("DELETE FROM [Order] WHERE [Order I.D] = @o_id", con);
                    command1.Parameters.AddWithValue("@o_id", int.Parse(textBox38.Text));
                    int workdone = command1.ExecuteNonQuery();
                    if (workdone > 0)
                    {
                        MessageBox.Show("Order deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Order not found.");
                    }
                    con.Close();
                    O_loadgrid();
                    textBox38.Clear();
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

        //All Insert Functions
        public void E_Insert() 
        {
            if (E_isempty_Ins()) 
            {
                try
                {
                    string name1 = textBox2.Text;
                    string name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name1);
                    string des1 = textBox7.Text;
                    string des = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(des1);
                    string gen1 = textBox9.Text;
                    string gen = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(gen1);
                    con.Open();
                    SqlCommand cmd_i = new SqlCommand("Insert into Employee ([Employee Name],[Password],Cnic,Designation,Gender,Salary,[Date of Birth],[Date of Join]) Values (@Name, @Password, @cnic, @des, @gen, @salary, @db, @dj)", con);
                    cmd_i.Parameters.AddWithValue("@Name", name);
                    cmd_i.Parameters.AddWithValue("@password", textBox3.Text);
                    cmd_i.Parameters.AddWithValue("@cnic", textBox6.Text);
                    cmd_i.Parameters.AddWithValue("@des", des);
                    cmd_i.Parameters.AddWithValue("@gen", gen);
                    cmd_i.Parameters.AddWithValue("@salary", decimal.Parse(textBox10.Text));
                    cmd_i.Parameters.AddWithValue("@db", textBox4.Text);
                    cmd_i.Parameters.AddWithValue("@dj", textBox8.Text);
                    int rowsaffected = cmd_i.ExecuteNonQuery();
                    if (rowsaffected > 0)
                    {
                        SqlCommand getIdCommand = new SqlCommand("SELECT @@IDENTITY", con);
                        int e_Id = Convert.ToInt32(getIdCommand.ExecuteScalar());

                        SqlCommand cmd2 = new SqlCommand("INSERT INTO Employee_Address ([Employee I.D], [Address]) VALUES (@eid, @Address)", con);
                        cmd2.Parameters.AddWithValue("@eid", e_Id);
                        string[] addresses = textBox35.Text.Split(':');
                        foreach (string address in addresses)
                        {
                            cmd2.Parameters.Clear();
                            cmd2.Parameters.AddWithValue("@eid", e_Id);
                            cmd2.Parameters.AddWithValue("@Address", address.Trim());
                            cmd2.ExecuteNonQuery();
                        }
                        SqlCommand cmd3 = new SqlCommand("INSERT INTO Employee_Phone ([Employee I.D], [Contact No.]) VALUES (@eid, @phone)", con);
                        cmd2.Parameters.AddWithValue("@eid", e_Id);
                        string[] phone = textBox5.Text.Split(':');
                        foreach (string number in phone)
                        {
                            cmd3.Parameters.Clear();
                            cmd3.Parameters.AddWithValue("@eid", e_Id);
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
                    E_loadgrid();
                    E_ClearAll();
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
        public void Co_Insert() 
        {
            if (Co_isempty_Ins()) 
            {
                try
                {
                    string name1 = textBox24.Text;
                    string name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name1);
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("INSERT INTO Company ([Company Name], [Company Ntn No.]) VALUES (@Name, @ntn)", con);
                    cmd1.Parameters.AddWithValue("@Name", name);
                    cmd1.Parameters.AddWithValue("@ntn", textBox22.Text);
                    int rowsaffected = cmd1.ExecuteNonQuery();
                    if (rowsaffected > 0)
                    {
                        SqlCommand getIdCommand = new SqlCommand("SELECT @@IDENTITY", con);
                        int co_Id = Convert.ToInt32(getIdCommand.ExecuteScalar());

                        SqlCommand cmd2 = new SqlCommand("INSERT INTO Company_Address ([Company I.D], [Address]) VALUES (@coid, @Address)", con);
                        cmd2.Parameters.AddWithValue("@coid", co_Id);
                        string[] addresses = textBox40.Text.Split(':');
                        foreach (string address in addresses)
                        {
                            cmd2.Parameters.Clear();
                            cmd2.Parameters.AddWithValue("@coid", co_Id);
                            cmd2.Parameters.AddWithValue("@Address", address.Trim());
                            cmd2.ExecuteNonQuery();
                        }
                        SqlCommand cmd3 = new SqlCommand("INSERT INTO Company_Phone ([Company I.D], [Contact No.]) VALUES (@coid, @phone)", con);
                        cmd2.Parameters.AddWithValue("@coid", co_Id);
                        string[] phone = textBox23.Text.Split(':');
                        foreach (string number in phone)
                        {
                            cmd3.Parameters.Clear();
                            cmd3.Parameters.AddWithValue("@coid", co_Id);
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
                    Co_loadgrid();
                    Co_ClearAll();
                }
                
            }
        }
        public void P_Insert() 
        {
            if (P_isempty_Ins()) 
            {
                try 
                {
                    string name1 = textBox13.Text;
                    string name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name1);
                    string gname1 = textBox12.Text;
                    string gname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(gname1);
                    string cat1 = textBox11.Text;
                    string cat = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cat1);
                    string pack1 = textBox21.Text;
                    string pack = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(pack1);
                    con.Open();
                    SqlCommand ins_com = new SqlCommand("SELECT COUNT(*) FROM Company WHERE [Company I.D] = @co_id", con);
                    ins_com.Parameters.AddWithValue("@co_id", int.Parse(textBox17.Text));
                    int count = (int)ins_com.ExecuteScalar();
                    if (count>0) 
                    {
                        SqlCommand com = new SqlCommand("Insert into Product ([Product Name],[Generic Name],Category,Pack,[Expiry Date],[Mfg. Date],[T.P],[M.R.P],Stock,[Company I.D]) Values (@pname,@gname,@cat,@pack,@ed,@mfg,@tp,@mrp,@stock,@co_id)", con);
                        com.Parameters.AddWithValue("@pname", name);
                        com.Parameters.AddWithValue("@gname", gname);
                        com.Parameters.AddWithValue("@cat", cat);
                        com.Parameters.AddWithValue("@pack", pack);
                        if (textBox18.Text == string.Empty)
                        {
                            SqlParameter param = new SqlParameter("@ed", SqlDbType.Date);
                            param.Value = DBNull.Value;
                            com.Parameters.Add(param);
                        }
                        else
                        {
                            com.Parameters.AddWithValue("@ed", DateTime.Parse(textBox18.Text));
                        }
                        com.Parameters.AddWithValue("@mfg", DateTime.Parse(textBox34.Text));
                        com.Parameters.AddWithValue("@tp", decimal.Parse(textBox20.Text));
                        com.Parameters.AddWithValue("@mrp", decimal.Parse(textBox19.Text));
                        com.Parameters.AddWithValue("@stock", int.Parse(textBox15.Text));
                        com.Parameters.AddWithValue("@co_id", int.Parse(textBox17.Text));
                        int workdone = com.ExecuteNonQuery();
                        if (workdone > 0)
                        {
                            MessageBox.Show("Insert Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else 
                        {
                            MessageBox.Show("Didn't Insert", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else 
                    {
                        MessageBox.Show("Company Is not Registered...!!", "Alert");
                        return;
                    }
                    con.Close();
                    P_loadgrid();
                    P_ClearAll();
                }
                catch(Exception ex) 
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

        //All Update Functions
        public void E_Update() 
        {
            if (E_isempty_Upd()) 
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update Employee SET [Employee Name] = @Name, [Password] = @pass, [Salary] = @sal, [Date of Birth] = @dob, [Date of Join] = @doj, [Cnic] = @cnic, [Designation] = @des, [Gender] = @gen where [Employee I.D] = @id", con);
                    cmd.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));
                    cmd.Parameters.AddWithValue("@Name", textBox2.Text);
                    cmd.Parameters.AddWithValue("@pass", textBox3.Text);
                    cmd.Parameters.AddWithValue("@sal", textBox10.Text);
                    cmd.Parameters.AddWithValue("@dob", textBox4.Text);
                    cmd.Parameters.AddWithValue("@doj", textBox8.Text);
                    cmd.Parameters.AddWithValue("@cnic", textBox6.Text);
                    cmd.Parameters.AddWithValue("@des", textBox7.Text);
                    cmd.Parameters.AddWithValue("@gen", textBox9.Text);
                    cmd.ExecuteNonQuery();
                    string sql1 = "DELETE FROM Employee_Address WHERE [Employee I.D] = @eid";
                    SqlCommand cmd1 = new SqlCommand(sql1, con);
                    cmd1.Parameters.AddWithValue("@eid", int.Parse(textBox1.Text));
                    cmd1.ExecuteNonQuery();

                    string sql2 = "INSERT INTO Employee_Address ([Employee I.D], [Address]) VALUES (@eid, @Address)";
                    SqlCommand cmd2 = new SqlCommand(sql2, con);
                    string[] addresses = textBox35.Text.Split(':');
                    foreach (string address in addresses)
                    {
                        cmd2.Parameters.Clear();
                        cmd2.Parameters.AddWithValue("@eid", int.Parse(textBox1.Text));
                        cmd2.Parameters.AddWithValue("@Address", address);
                        cmd2.ExecuteNonQuery();
                    }

                    string sql3 = "DELETE FROM Employee_Phone WHERE [Employee I.D] = @eid";
                    SqlCommand cmd3 = new SqlCommand(sql3, con);
                    cmd3.Parameters.AddWithValue("@eid", int.Parse(textBox1.Text));
                    cmd3.ExecuteNonQuery();

                    string sql4 = "INSERT INTO Employee_Phone ([Employee I.D], [Contact No.]) VALUES (@eid, @ph)";
                    SqlCommand cmd4 = new SqlCommand(sql4, con);
                    string[] Contacts = textBox5.Text.Split(':');
                    foreach (string phone in Contacts)
                    {
                        cmd4.Parameters.Clear();
                        cmd4.Parameters.AddWithValue("@eid", int.Parse(textBox1.Text));
                        cmd4.Parameters.AddWithValue("@ph", phone);
                        cmd4.ExecuteNonQuery();
                    }
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
                    E_ClearAll();
                    E_loadgrid();

                }

            }
        }
        public void P_Update() 
        {
            if (P_isempty_Upd()) 
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update Product SET [Product Name] = @Name, [Generic Name] = @gname, [Category] = @cat, [Stock] = @st, [Pack] = @pk, [T.p] = @tp, [M.R.P] = @mrp, [Mfg. Date] = @mfg, [Expiry Date] = @exp, [Company I.D] = @cid where [Product I.D] = @id", con);
                    cmd.Parameters.AddWithValue("@id", int.Parse(textBox14.Text));
                    cmd.Parameters.AddWithValue("@Name", textBox13.Text);
                    cmd.Parameters.AddWithValue("@gname", textBox12.Text);
                    cmd.Parameters.AddWithValue("@cat", textBox11.Text);
                    cmd.Parameters.AddWithValue("@st", textBox15.Text);
                    cmd.Parameters.AddWithValue("@pk", textBox21.Text);
                    cmd.Parameters.AddWithValue("@tp", textBox20.Text);
                    cmd.Parameters.AddWithValue("@mrp", textBox19.Text);
                    cmd.Parameters.AddWithValue("@mfg", DateTime.Parse(textBox34.Text));
                    if (textBox18.Text == string.Empty)
                    {
                        SqlParameter param = new SqlParameter("@exp", SqlDbType.Date);
                        param.Value = DBNull.Value;
                        cmd.Parameters.Add(param);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@exp", DateTime.Parse(textBox18.Text));
                    }
                    cmd.Parameters.AddWithValue("@cid", int.Parse(textBox17.Text));
                    cmd.ExecuteNonQuery();
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
                    P_ClearAll();
                    P_loadgrid();

                }
            }
        }
        public void Co_Update() 
        {
            if (Co_isempty_Upd()) 
            {
                try
                {
                    con.Open();
                    string sql = "UPDATE Company SET [Company Name] = @Name, [Company Ntn No.] = @ntn WHERE [Company I.D] = @coid";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@Name", textBox24.Text);
                    cmd.Parameters.AddWithValue("@ntn", textBox22.Text);
                    cmd.Parameters.AddWithValue("@coid", int.Parse(textBox25.Text));
                    cmd.ExecuteNonQuery();

                    string sql1 = "DELETE FROM Company_Address WHERE [Company I.D] = @coid";
                    SqlCommand cmd1 = new SqlCommand(sql1, con);
                    cmd1.Parameters.AddWithValue("@coid", int.Parse(textBox25.Text));
                    cmd1.ExecuteNonQuery();

                    string sql2 = "INSERT INTO Company_Address ([Company I.D], [Address]) VALUES (@coid, @Address)";
                    SqlCommand cmd2 = new SqlCommand(sql2, con);
                    string[] addresses = textBox40.Text.Split(':');
                    foreach (string address in addresses)
                    {
                        cmd2.Parameters.Clear();
                        cmd2.Parameters.AddWithValue("@coid", int.Parse(textBox25.Text));
                        cmd2.Parameters.AddWithValue("@Address", address);
                        cmd2.ExecuteNonQuery();
                    }

                    string sql3 = "DELETE FROM Company_Phone WHERE [Company I.D] = @coid";
                    SqlCommand cmd3 = new SqlCommand(sql3, con);
                    cmd3.Parameters.AddWithValue("@coid", int.Parse(textBox25.Text));
                    cmd3.ExecuteNonQuery();

                    string sql4 = "INSERT INTO Company_Phone ([Company I.D], [Contact No.]) VALUES (@coid, @ph)";
                    SqlCommand cmd4 = new SqlCommand(sql4, con);
                    string[] Contacts = textBox23.Text.Split(':');
                    foreach (string phone in Contacts)
                    {
                        cmd4.Parameters.Clear();
                        cmd4.Parameters.AddWithValue("@coid", int.Parse(textBox25.Text));
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
                    Co_loadgrid();
                    Co_ClearAll();
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
        public void O_Update()
        {
            if (textBox38.TextLength == 0 || textBox36.Text == "")
            {
                MessageBox.Show("Please Fill the Field to Update Data...!!");
            }
            else
            {
                string status1 = textBox36.Text;
                string status = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(status1);
                try
                {
                    con.Open();
                    SqlCommand cmd_u = new SqlCommand("Update [Order] SET [Status] = @status where [Order I.D] = @id", con);
                    cmd_u.Parameters.AddWithValue("@id", int.Parse(textBox38.Text));
                    cmd_u.Parameters.AddWithValue("@status", status);

                    int workdone=cmd_u.ExecuteNonQuery();
                    if (workdone > 0)
                    {
                        MessageBox.Show("Record has been Updated", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else 
                    {
                        MessageBox.Show("Record has not been Updated", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    textBox36.Clear();
                    textBox38.Clear();
                    O_loadgrid();

                }
            }
        }


        public void find_PDGV()
        {


            if (textBox1.Text.Length == 0 && textBox2.Text == "" && textBox3.Text == "")
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
                    if (textBox14.Text.Trim() != "")
                    {
                        query = "SELECT [I.D],[Employee Name],Designation,Gender FROM Employee_Table WHERE [I.D] = @id";
                        com1.Parameters.AddWithValue("@id", textBox1.Text.Trim());
                    }
                    else if (textBox13.Text.Trim() != "")
                    {
                        query = "SELECT [I.D],[Employee Name],Designation,Gender FROM Employee_Table WHERE [Employee Name] LIKE @name";
                        com1.Parameters.AddWithValue("@name", "%" + textBox2.Text.Trim() + "%");
                    }
                    else if (textBox12.Text.Trim() != "")
                    {
                        query = "SELECT [I.D],[Employee Name],Designation,Gender FROM Employee_Table WHERE Designation LIKE @designation";
                        com1.Parameters.AddWithValue("@designation", "%" + textBox3.Text.Trim() + "%");
                    }
                    else if (textBox17.Text.Trim() != "")
                    {
                        query = "SELECT [I.D],[Employee Name],Designation,Gender FROM Employee_Table WHERE Designation LIKE @designation";
                        com1.Parameters.AddWithValue("@designation", "%" + textBox3.Text.Trim() + "%");
                    }
                    else if (textBox16.Text.Trim() != "")
                    {
                        query = "SELECT [I.D],[Employee Name],Designation,Gender FROM Employee_Table WHERE Designation LIKE @designation";
                        com1.Parameters.AddWithValue("@designation", "%" + textBox3.Text.Trim() + "%");
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
        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            showmenu();
        }
        public void showmenu() 
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
        public void logout_Popup() 
        {
            DialogResult dialogResult = MessageBox.Show("Do You Want to Logout From Dashboard...", "Logout Option", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Form1 f1 = new Form1();
                f1.Show();
                this.Hide();
            }
        }
        private void Logout_Click(object sender, EventArgs e)
        {
            logout_Popup();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label3.Text = DateTime.Now.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            E_ClearAll();
        }

        private void dataGridView3_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row3 =this.dataGridView3.Rows[e.RowIndex];
                textBox25.Text = row3.Cells[0].Value.ToString();
                textBox24.Text = row3.Cells[1].Value.ToString();
                textBox22.Text = row3.Cells[2].Value.ToString();
                textBox40.Text = row3.Cells[3].Value.ToString();
                textBox23.Text = row3.Cells[4].Value.ToString();
            }
        }
        private void dataGridView1_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
                textBox5.Text = row.Cells[11].Value.ToString();
                textBox6.Text = row.Cells[3].Value.ToString();
                textBox7.Text = row.Cells[4].Value.ToString();
                textBox9.Text = row.Cells[5].Value.ToString();
                textBox10.Text = row.Cells[6].Value.ToString();
                textBox4.Text = row.Cells[7].Value.ToString();
                textBox8.Text = row.Cells[8].Value.ToString();
                textBox32.Text = row.Cells[9].Value.ToString();
                textBox35.Text = row.Cells[10].Value.ToString();
            }
        }
        private void dataGridView2_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
                textBox14.Text = row.Cells[0].Value.ToString();
                textBox13.Text = row.Cells[1].Value.ToString();
                textBox12.Text = row.Cells[2].Value.ToString();
                textBox11.Text = row.Cells[3].Value.ToString();
                textBox21.Text = row.Cells[4].Value.ToString();
                textBox18.Text = row.Cells[5].Value.ToString();
                textBox20.Text = row.Cells[6].Value.ToString();
                textBox19.Text = row.Cells[7].Value.ToString();
                textBox15.Text = row.Cells[8].Value.ToString();
                textBox34.Text = row.Cells[9].Value.ToString();
                textBox17.Text = row.Cells[10].Value.ToString();
                //textBox16.Text = row.Cells[10].Value.ToString();
            }
        }
        private void dataGridView4_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
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
        private void dataGridView5_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row3 = this.dataGridView5.Rows[e.RowIndex];
                textBox38.Text = row3.Cells[0].Value.ToString();
                textBox37.Text = row3.Cells[2].Value.ToString();
                textBox36.Text = row3.Cells[3].Value.ToString();
                textBox39.Text = row3.Cells[5].Value.ToString();

            }
        }

        private void findEmployee_Click(object sender, EventArgs e)
        {
            E_find();
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            E_loadgrid();
            E_ClearAll();
        }

        
        private void findProduct_Click(object sender, EventArgs e)
        {
            P_find();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            P_loadgrid();
            P_ClearAll();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Co_loadgrid();
            Co_ClearAll();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Co_ClearAll();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            P_ClearAll();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Cu_ClearAll();        
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Cu_loadgrid();
            Cu_ClearAll();
        }

        private void findCompany_Click(object sender, EventArgs e)
        {
            Co_find();
        }

        private void findCustomer_Click(object sender, EventArgs e)
        {
            Cu_find();
        }

        private void deleteCustomer_Click(object sender, EventArgs e)
        {
            Cu_delete();
        }

        private void deleteProduct_Click(object sender, EventArgs e)
        {
            P_delete();
        }

        private void deleteCompany_Click(object sender, EventArgs e)
        {
            Co_delete();
        }

        private void deleteEmployee_Click(object sender, EventArgs e)
        {
            E_delete();
        }

        private void insertEmployee_Click(object sender, EventArgs e)
        {
            E_Insert();
        }

        private void insertProduct_Click(object sender, EventArgs e)
        {
            P_Insert();
        }

        private void insertCompany_Click(object sender, EventArgs e)
        {
            Co_Insert();
        }

        private void insertCustomer_Click(object sender, EventArgs e)
        {
            Cu_Insert();
        }

        private void Notifications_Click(object sender, EventArgs e)
        {
            lowStock();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            P_Update();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            
            Bill_Details bd = new Bill_Details();
            bd.Show();
            this.Hide();
            
            
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Bill b = new Bill();
            b.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            O_loadgrid();
            O_ClearAll();
        }

        private void deleteOrder_Click(object sender, EventArgs e)
        {
            O_delete();
        }

        private void updateOrder_Click(object sender, EventArgs e)
        {
            O_Update();
        }

        private void findOrder_Click(object sender, EventArgs e)
        {
            O_find();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Cu_Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            E_Update();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Co_Update();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            O_ClearAll();
        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }



        // All Work for Bill Creation
        //-------------------------------------------------------------------------Bill Section---------------------------------------------------------------------------------------------//


        private void billCreation()
        {
            //con.Close();
            if (flag == 0)
            {


                //con.Open();
                command.Parameters.Clear();
                string billQuery = @"INSERT INTO Bill ([Employee I.D], [Customer I.D], [Billing Date], Amount, Discount) 
                                         VALUES (@employeeID, @customerID, @date, @totalBillAmount, @discount) ";
                command.CommandText = billQuery;
                command.Parameters.AddWithValue("@employeeID", employeeID);
                command.Parameters.AddWithValue("@customerID", custID);
                command.Parameters.AddWithValue("@date", SqlDbType.DateTime).Value = DateTime.Parse(date);
                command.Parameters.AddWithValue("@discount", Decimal.Parse(discount));
                command.Parameters.AddWithValue("@totalBillAmount", Decimal.Parse(amount.ToString()));
                command.ExecuteNonQuery();
                //con.Close();
                flag = 1;
            }

        }

        
        private void loadFormData()
        {

            try
            {

                billData.Clear();
                dataGridView6.DataSource = null;
                dataGridView6.Refresh();
                //con.Open();
                
                command.Parameters.Clear();
                string billQuery = "select * from Bill_Detail WHERE [Bill I.D] = " + maxBillID;
                command.CommandText = billQuery;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(this.billData);
                dataGridView6.DataSource = billData;
                //con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading bill data: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                //con.Close();
            }
        }
        /*
        private void onProductSelect()
        {
            //SqlConnection con = new SqlConnection("Data Source=DESKTOP-H3TPEQS;Initial Catalog=Pharmacy;Integrated Security=True");

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                con.Open();
                // Properly converting the selected item to a string
                string selectedProductID = comboBox4.SelectedItem.ToString();

                string prodselectQuery = "SELECT * FROM Product WHERE [Product I.D] = @ProductID";
                command.CommandText = prodselectQuery;
                // Use parameterized query to avoid SQL injection
                command.Parameters.AddWithValue("@ProductID", selectedProductID);

                SqlDataReader sdr = command.ExecuteReader();
                if (sdr.Read())
                {
                    textBox48.Text = sdr["Product Name"].ToString();
                    textBox47.Text = sdr["Stock"].ToString();
                    textBox43.Text = sdr["M.R.P"].ToString();
                    textBox42.Text = sdr["Expiry Date"].ToString();
                    textBox45.Text = sdr["Mfg. Date"].ToString();
                }
                discount = textBox46.Text = "0.00%";
                con.Close();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading bill data: " + ex.Message);
                con.Close();
            }
            finally
            {
                con.Close();
                
            }
        }
        */

        string productID;
        string productName;
        string employeeID;
        string custID;
        string quantity;
        string unitPrice;
        string date;
        string discount;
        private static string totalBillAmount;
        private static decimal amount;
        private static string maxBillID = string.Empty;
        string stock = string.Empty;
        int remainingStock = 0;

        private string getBillID()
        {
            try
            {
                //con.Open();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string billQuery = "select MAX([Bill I.D]) from Bill";
                command.CommandText = billQuery;
                SqlDataReader billsdr = command.ExecuteReader();
                if (billsdr.Read())
                {
                    maxBillID = billsdr[0].ToString();
                }
                //con.Close();
                return maxBillID;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
            finally{con.Close(); }
        }
        private void saveData()
        {
            try
            {
                stock = textBox47.Text;
                productID = comboBox4.Text;
                productName = textBox48.Text;
                employeeID = comboBox2.Text;
                custID = comboBox3.Text;
                quantity = textBox44.Text;
                unitPrice = textBox43.Text;
                date = dateTimePicker2.Text;
                discount = textBox46.Text;
                discount = discount.Replace("%", "");
                remainingStock = Int32.Parse(stock) - Int32.Parse(quantity);
                if (remainingStock < 0)
                {
                    remainingStock = Int32.Parse(stock);
                    MessageBox.Show("Product is Out of stock");
                    return;
                }
                if (amount == 0)
                {
                    MessageBox.Show("Amount is incorrect");
                    return;
                }
                if (productID == "" || custID == "" || employeeID == "" || amount == 0 || totalBillAmount == "" || quantity == "")
                {
                    MessageBox.Show("Please enter all values properly");
                    return;
                }
                //con.Open();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                command.Parameters.Clear();
                string quantityUpdate = @"update Product set [Stock] = @remainingStock where [Product I.D] = @productID;";
                command.CommandText = quantityUpdate;
                command.Parameters.AddWithValue("@productID", productID);
                command.Parameters.AddWithValue("@remainingStock", remainingStock);
                
                command.ExecuteNonQuery();
                

                billCreation();
                maxBillID = getBillID();

                // con.Open();
                command.Parameters.Clear();
                string billDetailquery = @"INSERT INTO Bill_Detail ([Product I.D], Quantity, Price, Total, [Bill I.D]) 
                                                            VALUES (@productID,@quantity,@UnitPrice, @TotalAmount, @billID)";
                command.CommandText = billDetailquery;
                command.Parameters.AddWithValue("@productID", productID);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@UnitPrice", unitPrice);
                command.Parameters.AddWithValue("@TotalAmount", amount.ToString());
                command.Parameters.AddWithValue("@billID", maxBillID);
                
                command.ExecuteNonQuery();
                
                MessageBox.Show("Record Added Succesfully!");

                recordUpdate();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in saving, please insert values properly!",ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                //con.Close();
            }

        }

        private void recordUpdate()
        {
            if (flag != 0)
            {
                loadFormData();
                genreateTotalAmount();
                con.Open();
                command.Parameters.Clear();
                string billQuery = @"Update Bill set Amount= " + decimal.Parse(totalBillAmount) + " where [Bill I.D] = " + maxBillID;
                command.CommandText = billQuery;
                command.ExecuteNonQuery();
                con.Close();
                flag = -100;
            }
        }
        private void genreateTotalAmount()
        {
            try
            {
                decimal totalamount = 0;
                foreach (DataGridViewRow row in dataGridView6.Rows)
                {
                    totalamount += decimal.Parse(row.Cells["Total"].Value.ToString());
                }
                totalBillAmount = totalamount.ToString();
                label58.Text = totalBillAmount.ToString() + " Rupees ";
            }
            catch (Exception e)
            {
                totalBillAmount = string.Empty;
                throw (e);
            }
        }
        private void amountCalc()
        {
            if ((textBox43.Text != "" && textBox44.Text != "") && (textBox46.Text == string.Empty || textBox46.Text == "0.00%"))
            {
                discount = textBox46.Text = "0.00%";
                decimal price = decimal.Parse(textBox43.Text);
                int quantity = Int32.Parse(textBox44.Text);
                amount = price * quantity;
                textBox41.Text = amount.ToString();
            }
            else if (textBox43.Text != "" && textBox44.Text != "" && textBox46.Text != string.Empty)
            {
                discount = textBox46.Text;
                decimal price = decimal.Parse(textBox43.Text);
                int quantity = Int32.Parse(textBox44.Text);
                decimal abc = (price * (decimal.Parse(discount.Replace("%", "")) / 100));
                price = price - abc;
                amount = price * quantity;
                textBox41.Text = amount.ToString();
            }
            else
            {
                amount = 0;
            }
        }

        private void onClear()
        {
            productID = string.Empty;
            productName = string.Empty;
            custID = string.Empty;
            employeeID = string.Empty;
            quantity = string.Empty;
            amount = 0;
            discount = string.Empty;
            //comboBox1.Text = string.Empty;
            comboBox2.Text = string.Empty;
            comboBox3.Text = string.Empty;
            comboBox4.Text = string.Empty;
            maxBillID = string.Empty;
            stock = string.Empty;
            unitPrice = textBox43.Text;
            date = string.Empty;
            remainingStock = 0;
            textBox44.Text = "";
            textBox47.Text = "";
            textBox48.Text = "";
            textBox43.Text = "";
            textBox41.Text = "";
            textBox42.Text = "";
            textBox46.Text = "";
            textBox45.Text = "";
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.onProductSelect();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [Product] WHERE [Product I.D] = @id", con);
                cmd.Parameters.AddWithValue("@id", comboBox4.SelectedItem);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    textBox48.Text = sdr["Product Name"].ToString();
                    textBox47.Text = sdr["Stock"].ToString();
                    textBox43.Text = sdr["M.R.P"].ToString();
                    textBox42.Text = sdr["Expiry Date"].ToString();
                    textBox45.Text = sdr["Mfg. Date"].ToString();
                }
                discount = textBox46.Text = "0.00%";
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

        private void button27_Click(object sender, EventArgs e)
        {
            saverecords();
            /*
            saveData();
            


            if (flag == 1)
            {
                loadFormData();
                genreateTotalAmount();
            }
            onClear();
            */
        }

        private void textBox44_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox47.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox44.Text = textBox44.Text.Remove(textBox44.Text.Length - 1);
                return;
            }
            amountCalc();
        }
        private void loadCustandEmp()
        {
            try
            {

                con.Open();
                string productQuery = "Select * from Product";
                command.CommandText = productQuery;
                SqlDataAdapter prodAdapter = new SqlDataAdapter(command);
                prodAdapter.Fill(this.productData);
                SqlDataReader prodIDreader = command.ExecuteReader();
                while (prodIDreader.Read())
                {
                    comboBox4.Items.Add(prodIDreader["Product I.D"]);
                }
                prodIDreader.Close();
                con.Close();

                con.Open();
                string custQuery = "Select * from Customer";
                command.CommandText = custQuery;
                SqlDataAdapter custdapter = new SqlDataAdapter(command);
                custdapter.Fill(this.customerData);
                SqlDataReader custReader = command.ExecuteReader();
                while (custReader.Read())
                {
                    comboBox3.Items.Add(custReader["Customer I.D"]);
                }
                custReader.Close();
                con.Close();

                con.Open();
                string empQuery = "Select * from Employee";
                command.CommandText = empQuery;
                SqlDataAdapter empdapter = new SqlDataAdapter(command);
                empdapter.Fill(this.empData);
                SqlDataReader empReader = command.ExecuteReader();
                while (empReader.Read())
                {
                    comboBox2.Items.Add(empReader["Employee I.D"]);
                }
                empReader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading bill data: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void textBox46_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox47.Text, "[^0-9].[^0-9]%"))
            {
                MessageBox.Show("Please enter in format 0.00% ");
                textBox44.Text = textBox44.Text.Remove(textBox44.Text.Length - 1);
                return;
            }
            amountCalc();
        }


        public void comboShow()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select [Product I.D] from Product", con);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    comboBox4.Items.Add(sdr["Product I.D"]);
                }
                sdr.Close();


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


        public void comboShowEmp()
        {
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("Select [Employee I.D] from Employee where Designation = 'Admin' ", con);
                SqlDataReader sdr1 = cmd1.ExecuteReader();
                while (sdr1.Read())
                {
                    comboBox2.Items.Add(sdr1["Employee I.D"]);
                }
                sdr1.Close();
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
        public void comboShowCus()
        {
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("Select [Customer I.D] from Customer", con);
                SqlDataReader sdr1 = cmd1.ExecuteReader();
                while (sdr1.Read())
                {
                    comboBox3.Items.Add(sdr1["Customer I.D"]);
                }
                sdr1.Close();
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
        public void saverecords() 
        {
            try 
            {
                con.Open();
                stock = textBox47.Text;
                productID = comboBox4.Text;
                productName = textBox48.Text;
                employeeID = comboBox2.Text;
                custID = comboBox3.Text;
                quantity = textBox44.Text;
                unitPrice = textBox43.Text;
                date = dateTimePicker2.Text;
                discount = textBox46.Text;
                discount = discount.Replace("%", "");
                remainingStock = Int32.Parse(stock) - Int32.Parse(quantity);
                if (remainingStock < 0)
                {
                    remainingStock = Int32.Parse(stock);
                    MessageBox.Show("Product is Out of stock");
                    return;
                }
                if (amount == 0)
                {
                    MessageBox.Show("Amount is incorrect");
                    return;
                }
                if (productID == "" || custID == "" || employeeID == "" || amount == 0 || totalBillAmount == "" || quantity == "")
                {
                    MessageBox.Show("Please enter all values properly");
                    return;
                }
                

                command.Parameters.Clear();
                string quantityUpdate = @"update Product set [Stock] = @remainingStock where [Product I.D] = @productID;";
                command.CommandText = quantityUpdate;
                command.Parameters.AddWithValue("@productID", productID);
                command.Parameters.AddWithValue("@remainingStock", remainingStock);

                command.ExecuteNonQuery();


                //billCreation();

                if (flag == 0)
                {


                    //con.Open();
                    command.Parameters.Clear();
                    string billQuery2 = @"INSERT INTO Bill ([Employee I.D], [Customer I.D], [Billing Date], Amount, Discount) 
                                         VALUES (@employeeID, @customerID, @date, @totalBillAmount, @discount) ";
                    command.CommandText = billQuery2;
                    command.Parameters.AddWithValue("@employeeID", employeeID);
                    command.Parameters.AddWithValue("@customerID", custID);
                    command.Parameters.AddWithValue("@date", SqlDbType.DateTime).Value = DateTime.Parse(date);
                    command.Parameters.AddWithValue("@discount", Decimal.Parse(discount));
                    command.Parameters.AddWithValue("@totalBillAmount", Decimal.Parse(amount.ToString()));
                    command.ExecuteNonQuery();
                    //con.Close();
                    flag = 1;
                }




                //maxBillID = getBillID();

                
                string billQuery = "select MAX([Bill I.D]) from Bill";
                command.CommandText = billQuery;
                SqlDataReader billsdr = command.ExecuteReader();
                if (billsdr.Read())
                {
                    maxBillID = billsdr[0].ToString();
                }
                billsdr.Close();


                command.Parameters.Clear();
                string billDetailquery = @"INSERT INTO Bill_Detail ([Product I.D], Quantity, Price, Total, [Bill I.D]) 
                                                            VALUES (@productID,@quantity,@UnitPrice, @TotalAmount, @billID)";
                command.CommandText = billDetailquery;
                command.Parameters.AddWithValue("@productID", productID);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@UnitPrice", unitPrice);
                command.Parameters.AddWithValue("@TotalAmount", amount.ToString());
                command.Parameters.AddWithValue("@billID", maxBillID);

                command.ExecuteNonQuery();

                MessageBox.Show("Record Added Succesfully!");

                //recordUpdate();

                if (flag != 0)
                {
                    loadFormData();
                    genreateTotalAmount();
                    //con.Open();
                    con.Open();
                    command.Parameters.Clear();
                    string billQuery3 = @"Update Bill set Amount= " + decimal.Parse(totalBillAmount) + " where [Bill I.D] = " + maxBillID;
                    command.CommandText = billQuery3;
                    command.ExecuteNonQuery();
                    //con.Close();
                    flag = -100;
                }

                if (flag == 1)
                {
                    //loadFormData();
                    billData.Clear();
                    dataGridView6.DataSource = null;
                    dataGridView6.Refresh();
                    //con.Open();

                    command.Parameters.Clear();
                    string billQuery5 = "select * from Bill_Detail WHERE [Bill I.D] = " + maxBillID;
                    command.CommandText = billQuery5;
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(this.billData);
                    dataGridView6.DataSource = billData;
                    //con.Close();


                    genreateTotalAmount();
                }
                onClear();


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally 
            {
                con.Close();
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            Bill_find bf = new Bill_find();
            bf.Show();
            this.Hide();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            Bill_Info bi = new Bill_Info();
            bi.Show();
            this.Hide();
        }
    }
}
