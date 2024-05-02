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
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Doctor_Appointment_Booking_System
{
    
    public partial class Appointmentsbook : Form
    {
        private string loggedInUsername;
        private string loggedInPassword;
        public Appointmentsbook(string username, string password)
        {
            InitializeComponent();
            DisplayBapp();
            PopulateDateComboBox();
            loggedInUsername = username;
            loggedInPassword = password;
            PopulateDoctorComboBox();

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\DoctorAppointmentDb.mdf;Integrated Security=True;Connect Timeout=30");
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
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Appointmentsbook_Load(object sender, EventArgs e)
        {

        }

        private void panel30_Paint(object sender, PaintEventArgs e)
        {

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
        private void PopulateDateComboBox()
        {
            try
            {
                Con.Open();
                string query = "SELECT DISTINCT AappDate FROM AddappointmentTb1";
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    comboBoxdate.Items.Add(reader.GetDateTime(0));
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void PopulateDoctorComboBox()
        {
            try
            {
                Con.Open();
                string query = "SELECT DISTINCT AappDoc FROM AddappointmentTb1 ";
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    comboBoxdoc.Items.Add(reader.GetString(0));
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

       
           
        private void comboBoxdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBoxdoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBoxtime_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedRows[0].Index;
                int bappId = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["BappID"].Value);

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

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            patientp patForm = new patientp(loggedInUsername, loggedInPassword);
            patForm.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            patientp patientForm = new patientp(loggedInUsername, loggedInPassword);
            patientForm.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            patientp patientForm = new patientp(loggedInUsername, loggedInPassword);
            patientForm.Show();
            this.Hide();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Feedback patientForm = new Feedback(loggedInUsername, loggedInPassword);
            patientForm.Show();
            this.Hide();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Doctorp homeForm = new Doctorp(loggedInUsername, loggedInPassword);
            homeForm.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Doctorp homeForm = new Doctorp(loggedInUsername, loggedInPassword);
            homeForm.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Feedback patientForm = new Feedback(loggedInUsername, loggedInPassword);
            patientForm.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Logout Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide(); // Hide 
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            patpresc homeForm = new patpresc(loggedInUsername, loggedInPassword);
            homeForm.Show();
            this.Hide();
        }
    }
}
