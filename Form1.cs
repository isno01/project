using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StudentManagementSystem
{
    public partial class Student_Page : Form
    {
        private string connectionString;
        private DataGridView dataGridView1;
        private Button loadBtn;

        public Student_Page()
        {
            // Retrieve the connection string from the app.config
            connectionString = ConfigurationManager.ConnectionStrings["StudentDB"].ConnectionString;

            // Initialize Form Settings
            this.Text = "Student Page";
            this.Size = new System.Drawing.Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Initialize DataGridView
            dataGridView1 = new DataGridView
            {
                Size = new System.Drawing.Size(550, 250),
                Location = new System.Drawing.Point(20, 50),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false
            };

            // Initialize Load Button
            loadBtn = new Button
            {
                Text = "Load Students",
                Location = new System.Drawing.Point(20, 10),
                Size = new System.Drawing.Size(150, 30)
            };
            loadBtn.Click += new EventHandler(LoadStudentRecords);

            // Add Controls to Form
            this.Controls.Add(loadBtn);
            this.Controls.Add(dataGridView1);
        }

        private void LoadStudentRecords(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT studentId, CONCAT(firstName, ' ', middleName, ' ', lastName) AS fullName FROM studentrecordtb";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Add View Button Column (if not already added)
                    if (!dataGridView1.Columns.Contains("ViewBtn"))
                    {
                        DataGridViewButtonColumn viewButton = new DataGridViewButtonColumn
                        {
                            Name = "ViewBtn",
                            HeaderText = "Action",
                            Text = "VIEW",
                            UseColumnTextForButtonValue = true
                        };
                        dataGridView1.Columns.Add(viewButton);
                    }

                    dataGridView1.DataSource = dt;

                    // Handle Button Click
                    dataGridView1.CellClick += new DataGridViewCellEventHandler(DataGridView1_CellClick);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading student records: " + ex.Message);
                }
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["ViewBtn"].Index && e.RowIndex >= 0)
            {
                int studentId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["studentId"].Value);

                // Open StudentPage_Individual and pass the studentId
                StudentPage_Individual individualForm = new StudentPage_Individual(studentId);
                individualForm.Show();
            }
        }
    }
}