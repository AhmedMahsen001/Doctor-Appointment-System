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

namespace Doctor_Appointment_Booking_System
{
    public partial class Doctorp : Form
    {
         private string loggedInUsername;
        private string loggedInPassword;
        public Doctorp(string username, string password)
        {
            InitializeComponent();
            DisplayAapp();
            loggedInUsername = username;
            loggedInPassword = password;
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
                    dataGridView2.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }





        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            patientp homeForm = new patientp(loggedInUsername, loggedInPassword);
            homeForm.Show();
            this.Hide();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            
        }

        private void label11_Click(object sender, EventArgs e)
        {
            
        }

        private void label9_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Appointmentsbook homeForm = new Appointmentsbook(loggedInUsername, loggedInPassword);
            homeForm.Show();
            this.Hide();
        }

        private void label100_Click(object sender, EventArgs e)
        {
            Appointmentsbook homeForm = new Appointmentsbook(loggedInUsername, loggedInPassword);
            homeForm.Show();
            this.Hide();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Feedback homeForm = new Feedback(loggedInUsername, loggedInPassword);
            homeForm.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox1.Text.Trim(); 

            if (string.IsNullOrEmpty(searchTerm))
            {
                DisplayAapp();
                return;
            }

            
            DataTable filteredTable = ((DataTable)dataGridView2.DataSource).Clone(); 
            foreach (DataRow row in ((DataTable)dataGridView2.DataSource).Rows)
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

            
            dataGridView2.DataSource = filteredTable;
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
