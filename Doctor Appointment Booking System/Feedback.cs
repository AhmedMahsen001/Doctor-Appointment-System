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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Doctor_Appointment_Booking_System
{
    public partial class Feedback : Form
    {
        private string loggedInUsername;
        private string loggedInPassword;
        public Feedback(string username, string password)
        {
            InitializeComponent();
            loggedInUsername = username;
            loggedInPassword = password;
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            patientp homeForm = new patientp(loggedInUsername, loggedInPassword);
            homeForm.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void submitfeed_Click(object sender, EventArgs e)
        {
            // Retrieve data from form controls
            string satisfactionLevel = "";
            if (radioButton1.Checked)
                satisfactionLevel = "Excellent";
            else if (radioButton2.Checked)
                satisfactionLevel = "Good";
            else if (radioButton3.Checked)
                satisfactionLevel = "Neutral";
            else if (radioButton4.Checked)
                satisfactionLevel = "Poor";

            string additionalInfo = txtAdditionalInfo.Text;
            string name = txtName.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;

            // Insert data into database
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\DoctorAppointmentDb.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    connection.Open();
                    string query = "INSERT INTO FeedbackTb1 (FdSatisfaction, FdAdditionalInfo, FdName, FdEmail, FdPhone) " +
                                   "VALUES (@SatisfactionLevel, @AdditionalInfo, @Name, @Email, @Phone)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SatisfactionLevel", satisfactionLevel);
                    command.Parameters.AddWithValue("@AdditionalInfo", additionalInfo);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Feedback submitted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting feedback: " + ex.Message);
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Doctorp homeForm = new Doctorp(loggedInUsername, loggedInPassword);
            homeForm.Show();
            this.Hide();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Appointmentsbook homeForm = new Appointmentsbook(loggedInUsername, loggedInPassword);
            homeForm.Show();
            this.Hide();
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

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            patpresc homeForm = new patpresc(loggedInUsername, loggedInPassword);
            homeForm.Show();
            this.Hide();
        }
    }
}
    

