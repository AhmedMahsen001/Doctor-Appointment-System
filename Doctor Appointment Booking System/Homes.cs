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

namespace Doctor_Appointment_Booking_System
{
    public partial class Homes : Form
    {
        public Homes()
        {
            InitializeComponent();
                       Countpatients();
            CountAppo();
                Countappr();
                Countdoc();
                Countcanc();
            Countfeed();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\DoctorAppointmentDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void Countpatients()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count (*) From PatientTb1",Con);
            DataTable dt= new DataTable();
            sda.Fill(dt);
            label17.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void Countdoc()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count (*) From DoctorTb1", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            label16.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void Countappr()
        {
            try
            {
                using (SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\DoctorAppointmentDb.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    
                    Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count (*) From BookappointmentTb1 where BappStatus ='approved'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            label2.Text = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Countcanc()
        {
            try
            {
                using (SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\DoctorAppointmentDb.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count (*) From BookappointmentTb1 where BappStatus ='canceled'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            label15.Text = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CountAppo()
        {

            try
            {
                using (SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\DoctorAppointmentDb.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count (*) From BookappointmentTb1 ", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            label14.Text = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Countfeed()
        {

            try
            {
                using (SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\DoctorAppointmentDb.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    Con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("Select count (*) From FeedbackTb1 ", Con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    label18.Text = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Homes_Load(object sender, EventArgs e)
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

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            patients obj = new patients();
            obj.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            patients obj = new patients();
            obj.Show();
            this.Hide();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            doctors obj = new doctors();
            obj.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            doctors obj = new doctors();
            obj.Show();
            this.Hide();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Doctorp2 obj = new Doctorp2();
            obj.Show();
            this.Hide();
        }

        private void label100_Click(object sender, EventArgs e)
        {
            Doctorp2 obj = new Doctorp2();
            obj.Show();
            this.Hide();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Reports obj = new Reports();
            obj.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Reports obj = new Reports();
            obj.Show();
            this.Hide();
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            ListViewForm obj = new ListViewForm();
            obj.Show();
            this.Hide();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Logout Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
