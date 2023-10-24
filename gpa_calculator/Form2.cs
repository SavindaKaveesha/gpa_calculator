using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gpa_calculator
{
    public partial class Year1 : Form
    {
        public Year1()
        {
            InitializeComponent();
        }

        double totalGpa, credit, weight,totCredit;
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
            double totalGpa = 0;
            double totalCredits = 0;

            ComboBox[] gradeBoxes = { sub21grdBox, sub22grdBox };
            ComboBox[] creditBoxes = { sub21CreditBox, sub22CreditBox };

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
        }

        public double CalculateGrade(string selectedVal, double weight)
        {
            if (gradeToCreditMap.TryGetValue(selectedVal, out double credit))
            {
                return credit * weight;
            }

            return 0; // Grade not found in the dictionary, return a default value.
        }


    }
}
