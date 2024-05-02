using DGVPrinterHelper;
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
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
           
            DisplayAappData();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\DoctorAppointmentDb.mdf;Integrated Security=True;Connect Timeout=30");


       
        
        private void DisplayAappData()
        {
            try
            {
                string query = "SELECT * FROM BookappointmentTb1"; // Adjust this query according to your database schema
                SqlDataAdapter sda = new SqlDataAdapter(query, Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
       
        
        private void printaap_Click(object sender, EventArgs e)
        {

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            // Increase font size for rows
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Regular);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // Print using DGVPrinter
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "\r\n\r\n DOCTOR APPOINTMENT SYSTEM";
            printer.SubTitle = "APPOINTMENTS LIST\r\nFOR MORE INFO \r\n CONTACT: 0746533010 - AHMED\r\n";
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.Footer = "*Ahmed's Design* ";
            printer.FooterSpacing = 15;

            printer.PrintDataGridView(dataGridView1);

            // Reset font size after printing
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Regular);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;


            MessageBox.Show("Printed Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        
    }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {

           
        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {



        }

        private void pictureBox10_Click(object sender, EventArgs e)
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

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Doctorp2 obj = new Doctorp2();
            obj.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
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
    }
}
