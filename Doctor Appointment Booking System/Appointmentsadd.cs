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
using System.Xml.Linq;
namespace Doctor_Appointment_Booking_System
{
    public partial class Appointmentsadd : Form
    {
        private string loggedInUsername;
        private string loggedInPassword;
        public Appointmentsadd(string username, string password)
        {
            InitializeComponent();
            loggedInUsername = username;
            loggedInPassword = password;

            DisplayAapp();
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
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Appointmentsadd_Load(object sender, EventArgs e)
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

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Doctord doctorForm = new Doctord(loggedInUsername, loggedInPassword);
            doctorForm.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            patientd doctorForm = new patientd(loggedInUsername, loggedInPassword);
            doctorForm.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Doctord doctorForm = new Doctord(loggedInUsername, loggedInPassword);
            doctorForm.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Doctord doctorForm = new Doctord(loggedInUsername, loggedInPassword);
            doctorForm.Show();
            this.Hide();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Presc doctorForm = new Presc(loggedInUsername, loggedInPassword);
            doctorForm.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox3.Text.Trim(); // Get the search term from the TextBox and remove leading/trailing spaces

            // If the search term is empty, display all rows in the DataGridView
            if (string.IsNullOrEmpty(searchTerm))
            {
                DisplayAapp();
                return;
            }

            // Filter the DataTable based on the search term
            DataTable filteredTable = ((DataTable)dataGridView1.DataSource).Clone(); // Clone the DataTable structure
            foreach (DataRow row in ((DataTable)dataGridView1.DataSource).Rows)
            {
                // Check if the Doctor's Name column contains the search term (case-insensitive)
                if (row["AappDoc"].ToString().IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    filteredTable.ImportRow(row); // Add the row to the filtered DataTable
                }
            }

            // Update the DataGridView with the filtered data
            dataGridView1.DataSource = filteredTable;
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Logout Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide(); // Hide 
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
