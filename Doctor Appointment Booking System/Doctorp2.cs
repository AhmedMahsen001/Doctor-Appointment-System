using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Doctor_Appointment_Booking_System
{
    public partial class Doctorp2 : Form
    {
        public Doctorp2()
        {
            InitializeComponent();
            DisplayAapp();
            dataGridView1.CellClick += dataGridView1_CellClick;
            
            DisplayBapp();
            
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\DoctorAppointmentDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayAapp()
        {
            try
            {
                using (SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\DoctorAppointmentDb.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    Con.Open();
                    string Query = "Select * From AddappointmentTb1";
                    SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                    SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                    var ds = new DataSet();
                    sda.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }





        }
        private void DisplayBapp()
        {

            try
            {
                using (SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\DoctorAppointmentDb.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    Con.Open();
                    string Query = "Select * From BookappointmentTb1";
                    SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                    SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                    var ds = new DataSet();
                    sda.Fill(ds);
                    dataGridView2.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void Addingbtn_Click(object sender, EventArgs e)
        {
            if (AddDoc.Text == "" || AddSpec.Text == "" || dateTimePicker1.Text == "" || AddStart.Text == "")
            {
                MessageBox.Show("Missing Info");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into AddappointmentTb1(AappDoc,AappSpec,AappDate,StartTime)values(@AddDoc,@AddSpec,@dateTimePicker1,@AddStart)", Con);
                    cmd.Parameters.AddWithValue("@AddDoc", AddDoc.Text);
                    cmd.Parameters.AddWithValue("@AddSpec", AddSpec.Text);
                    cmd.Parameters.AddWithValue("@dateTimePicker1", dateTimePicker1.Text);
                    cmd.Parameters.AddWithValue("@AddStart", AddStart.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Appointment Created");
                    Con.Close();
                    DisplayAapp();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
            
        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select Appointment");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from AddappointmentTb1 where AappID=@Aappkey", Con);

                    cmd.Parameters.AddWithValue("@Aappkey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Doctor Deleted");
                    Con.Close();
                    DisplayAapp();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void Updatebtn_Click(object sender, EventArgs e)
        {
            if (AddDoc.Text == "" || AddSpec.Text == "" || dateTimePicker1.Text == "" || AddStart.Text == "")
            {
                MessageBox.Show("Missing Info");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update AddappointmentTb1 set AappDoc=@AddDoc,AappSpec=@AddSpec,AappDate=@dateTimePicker1,StartTime=@AddStart where AappID=@Aappkey", Con);
                    cmd.Parameters.AddWithValue("@AddDoc", AddDoc.Text);
                    cmd.Parameters.AddWithValue("@AddSpec", AddSpec.Text);
                    cmd.Parameters.AddWithValue("@dateTimePicker1", dateTimePicker1.Text);
                    cmd.Parameters.AddWithValue("@AddStart", AddStart.Text);
                    cmd.Parameters.AddWithValue("@Aappkey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Updated");
                    Con.Close();
                    DisplayAapp();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                AddDoc.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                AddSpec.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

                TimeSpan startTime = (TimeSpan)dataGridView1.Rows[e.RowIndex].Cells[4].Value;

                // Convert TimeSpan to DateTime
                DateTime startDateTime = DateTime.Today.Add(startTime);

                // Format the DateTime to display only time
                AddStart.Text = startDateTime.ToString("HH:mm");


                if (AddDoc.Text == "")
                {
                    Key = 0;
                }
                else
                {
                    Key = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            if (comboBoxdate.SelectedItem == null || comboBoxdoc.SelectedItem == null || textBoxpat.Text == "" || comboBoxtime.SelectedItem == null || textBoxtype.Text == "")
            {
                MessageBox.Show("Missing Info");
            }
            else
            {
                try
                {
                    Con.Open();

                    // Check if the selected date, doctor, and time slot already exist
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM BookappointmentTb1 WHERE BappDate = @BappDate AND BappDoc = @BappDoc AND BookTime = @BookTime", Con);
                    checkCmd.Parameters.AddWithValue("@BappDate", comboBoxdate.Text);
                    checkCmd.Parameters.AddWithValue("@BappDoc", comboBoxdoc.Text);
                    checkCmd.Parameters.AddWithValue("@BookTime", comboBoxtime.Text);

                    int existingAppointmentsCount = (int)checkCmd.ExecuteScalar();

                    if (existingAppointmentsCount > 0)
                    {
                        MessageBox.Show($"The selected time slot ({comboBoxtime.Text}) for Dr. {comboBoxdoc.Text} on {comboBoxdate.Text} is already booked.");

                    }
                    else
                    {
                        // Insert the appointment into the database
                        SqlCommand cmd = new SqlCommand("INSERT INTO BookappointmentTb1(BappDate, BappDoc, BappPat, BookTime, BappType, BappStatus) VALUES (@BappDate, @BappDoc, @BappPat, @BookTime, @BappType, 'booked')", Con);
                        cmd.Parameters.AddWithValue("@BappDate", comboBoxdate.Text);
                        cmd.Parameters.AddWithValue("@BappDoc", comboBoxdoc.Text);
                        cmd.Parameters.AddWithValue("@BappPat", textBoxpat.Text);
                        cmd.Parameters.AddWithValue("@BookTime", comboBoxtime.Text);
                        cmd.Parameters.AddWithValue("@BappType", textBoxtype.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Appointment Booked");

                        comboBoxdate.SelectedItem = null;
                        comboBoxdoc.SelectedItem = null;
                        textBoxpat.Text = "";
                        comboBoxtime.SelectedItem = null;
                        textBoxtype.Text = "";

                    }
                    DisplayBapp();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                finally
                {

                    if (Con.State == ConnectionState.Open)
                    {
                        Con.Close();
                    }

                }
            }
        }

        private void LoadUniqueDates()
        {
            comboBoxdate.Items.Clear(); // Clear existing items
            try
            {
                Con.Open();
                string Query = "SELECT DISTINCT CONVERT(date, AappDate) AS AappDate FROM AddappointmentTb1";
                SqlCommand cmd = new SqlCommand(Query, Con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBoxdate.Items.Add(((DateTime)reader["AappDate"]).ToString("yyyy-MM-dd"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();
                }

            }

        }

        private void LoadUniqueDoctors()
        {
            comboBoxdoc.Items.Clear(); // Clear existing items
            try
            {
                Con.Open();
                string Query = "SELECT DISTINCT AappDoc FROM AddappointmentTb1";
                SqlCommand cmd = new SqlCommand(Query, Con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBoxdoc.Items.Add(reader["AappDoc"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();
                }

            }

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
       
        private void comboBoxdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }

        private void comboBoxdoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
            {
                LoadUniqueDates();
                LoadUniqueDoctors();
            }
            else if (tabControl1.SelectedTab == tabPage3)
            {
                // Refresh dataGridView2 when tabPage3 is selected
                DisplayBapp();
            }
        }
       

        private void Doctorp2_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Homes obj = new Homes();
            obj.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Homes obj = new Homes();
            obj.Show();
            this.Hide();
        }

        private void Approvedbtn_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridView2.SelectedRows[0].Index;
                int bappId = Convert.ToInt32(dataGridView2.Rows[rowIndex].Cells["BappID"].Value);

                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE BookappointmentTb1 SET BappStatus = 'approved' WHERE BappID = @BappId", Con);
                    cmd.Parameters.AddWithValue("@BappId", bappId);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Appointment approved successfully.");
                        DisplayBapp(); // Refresh the data grid view after updating the status
                    }
                    else
                    {
                        MessageBox.Show("No appointment found with the given ID.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating status: " + ex.Message);
                }
                finally
                {
                    Con.Close();
                }
            }
            else
            {
                MessageBox.Show("Please select an appointment to approve.");
            }
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridView2.SelectedRows[0].Index;
                int bappId = Convert.ToInt32(dataGridView2.Rows[rowIndex].Cells["BappID"].Value);

                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE BookappointmentTb1 SET BappStatus = 'canceled' WHERE BappID = @BappId", Con);
                    cmd.Parameters.AddWithValue("@BappId", bappId);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Appointment canceled successfully.");
                        DisplayBapp(); // Refresh the data grid view after updating the status
                    }
                    else
                    {
                        MessageBox.Show("No appointment found with the given ID.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating status: " + ex.Message);
                }
                finally
                {
                    Con.Close();
                }
            }
            else
            {
                MessageBox.Show("Please select an appointment to cancel.");
            }
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Logout Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox3.Text.Trim();

            if (string.IsNullOrEmpty(searchTerm))
            {
                DisplayAapp();
                return;
            }


            DataTable filteredTable = ((DataTable)dataGridView1.DataSource).Clone();
            foreach (DataRow row in ((DataTable)dataGridView1.DataSource).Rows)
            {

                if (row["AappDoc"].ToString().IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    filteredTable.ImportRow(row);
                }
                else if (row["AappDate"].ToString().IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    filteredTable.ImportRow(row);
                }
            }


            dataGridView1.DataSource = filteredTable;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(searchTerm))
            {
                DisplayBapp();
                return;
            }


            DataTable filteredTable = ((DataTable)dataGridView2.DataSource).Clone();
            foreach (DataRow row in ((DataTable)dataGridView2.DataSource).Rows)
            {

                if (row["BappDoc"].ToString().IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    filteredTable.ImportRow(row);
                }
                else if (row["BappDate"].ToString().IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    filteredTable.ImportRow(row);
                }
            }


            dataGridView2.DataSource = filteredTable;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            patients obj = new  patients();
            obj.Show();
            this.Hide();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            doctors obj = new doctors();
            obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ListViewForm obj = new ListViewForm();
            obj.Show();
            this.Hide();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Reports obj = new Reports();
            obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

    

}
