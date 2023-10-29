using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace gpa_calculator
{
    public partial class Year1 : Form
    {

        private string connectionString = "Data Source= C:\\Users\\Savinda\\source\\repos\\gpa_calculator\\gpa_calculator\\gpaCalDB.db";
        private SQLiteConnection connection;
        private SQLiteCommand command;

        public Year1()
        {
            InitializeComponent();
            connection = new SQLiteConnection(connectionString);
            command = new SQLiteCommand(connection);
        }

        double totalGpa, credit, weight, totCredit;
        string selectedVal;


        private void year1CalBtn_Click(object sender, EventArgs e)
        {

            if (PhysicsLaboratory11Box.Text.Length == 0)
            {
                //not selected valude, do nothing
            }
            else
            {
                weight = 1;
                selectedVal = PhysicsLaboratory11Box.Text.ToString();
                totalGpa += calculate(selectedVal, weight);
            }

            if (IntroductiontoElectricityandMagnetismBox.Text.Length == 0)
            {
                //not selected valude, do nothing
            }
            else
            {
                weight = 1;
                selectedVal = IntroductiontoElectricityandMagnetismBox.Text.ToString();
                totalGpa += calculate(selectedVal, weight);
            }

            totGpaLbl.Text = totalGpa.ToString();

            totalGpa = 0;
            credit = 0;
            weight = 0;

        }

        public double calculate(string sVal, double wght)
        {

            if (sVal == "A+" || sVal == "A")
            {
                credit = 4;
            }
            else if (sVal == "A-")
            {
                credit = 3.75;
            }
            else if (sVal == "B+")
            {
                credit = 3.5;
            }
            else if (sVal == "B")
            {
                credit = 3.25;
            }
            else if (sVal == "B-")
            {
                credit = 3.0;
            }
            else if (sVal == "C+")
            {
                credit = 2.75;
            }
            else if (sVal == "C")
            {
                credit = 2.0;
            }
            else if (sVal == "C-")
            {
                credit = 1.75;
            }
            else if (sVal == "D+")
            {
                credit = 1.5;
            }
            else if (sVal == "D")
            {
                credit = 1.25;
            }
            else if (sVal == "E")
            {
                credit = 0.0;
            }

            return credit;

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private Dictionary<string, double> gradeToCreditMap = new Dictionary<string, double>
{
    { "A+", 4.0 },
    { "A", 4.0 },
    { "A-", 3.75 },
    { "B+", 3.5 },
    { "B", 3.25 },
    { "B-", 3.0 },
    { "C+", 2.75 },
    { "C", 2.0 },
    { "C-", 1.75 },
    { "D+", 1.5 },
    { "D", 1.25 },
    { "E", 0.0 }
};

        private void button1_Click(object sender, EventArgs e)
        {

            InsertData();

            double totalGpa = 0;
            double totalCredits = 0;

            ComboBox[] gradeBoxes = { sub21grdBox, sub22grdBox, sub23grdBox, sub24grdBox, sub25grdBox, sub26grdBox, sub27grdBox, sub28grdBox, sub29grdBox, sub210grdBox, sub211grdBox, sub212grdBox };
            ComboBox[] creditBoxes = { sub21CreditBox, sub22CreditBox, sub23CreditBox, sub24CreditBox, sub25CreditBox, sub26CreditBox, sub27CreditBox, sub28CreditBox, sub29CreditBox, sub210CreditBox, sub211CreditBox, sub212CreditBox };

            for (int i = 0; i < gradeBoxes.Length; i++)
            {
                if (creditBoxes[i].Text.Length > 0 && gradeBoxes[i].Text.Length > 0)
                {
                    totalGpa += CalculateGrade(gradeBoxes[i].Text, double.Parse(creditBoxes[i].Text));
                    totalCredits += double.Parse(creditBoxes[i].Text);
                }
            }

            if (totalCredits > 0)
            {
                label16.Text = (totalGpa / totalCredits).ToString();
            }
            else
            {
                label16.Text = "0"; // Handle the case where totalCredits is 0 to avoid division by zero.
            }

            totalGpa = 0;
            totalCredits = 0;
        }

        public double CalculateGrade(string selectedVal, double weight)
        {
            if (gradeToCreditMap.TryGetValue(selectedVal, out double credit))
            {
                return credit * weight;
            }

            return 0; // Grade not found in the dictionary, return a default value.
        }

        private void Year1_Load(object sender, EventArgs e)
        {
            connection.Open();
            Console.WriteLine("Database opened");
            connection.Close();

            RetrieveData(); //load data to form 

        }

        private void InsertData()
        {
            connection.Open();
            string insertQuery = "INSERT INTO Year1sem1 (subject,weight, grade) VALUES ( '" + subject21Box.Text + "' ,'" + sub21CreditBox.Text + "', '" + sub21grdBox.Text + "')";
            command.CommandText = insertQuery;
            //command.Parameters.AddWithValue("@Value1", "Data1");
            //command.Parameters.AddWithValue("@Value2", "Data2");
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void RetrieveData()
        {
            connection.Open();
            string selectQuery = "SELECT * FROM Year1sem1";
            command.CommandText = selectQuery;
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                // Process and display data
                string data1 = reader["weight"].ToString();
                string data2 = reader["grade"].ToString();
                string data3 = reader["subject"].ToString();

                string data4 = reader["weight"].ToString();
                string data5 = reader["grade"].ToString();
                string data6 = reader["subject"].ToString();

                // Display data in the form or do whatever you need

                sub21CreditBox.Text = data1;
                sub21grdBox.Text = data2;
                subject21Box.Text = data3;

                sub22CreditBox.Text = data4;
                sub22grdBox.Text = data5;
                subject22Box.Text = data6;

            }
            reader.Close();
            connection.Close();
        }

    }
}
