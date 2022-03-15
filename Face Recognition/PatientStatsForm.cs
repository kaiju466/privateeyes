using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using System.Globalization;
/*---------------------------------------------------------------------------------------------------------------------------------
 * Name: PrivateEyes Facial Recognition Security Software
 * Written by: InYoface: Steven Garcia, William Chang and Josh Emrick
 * Date: 9/11/2012
 * Revision:1.2.0
 * --------------------------------------------------------------------------------------------------------------------------------
 */
namespace Face_Recognition
{
    public partial class PatientStatsForm : Form
    {
        #region Private Fields
        private string patientName { get; set; }
        #endregion

        public PatientStatsForm()
        {
            patientName = string.Empty;
            InitializeComponent();
        }

        
        /// <summary>
        /// LoadData function returns true if patient found
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
 
        public bool LoadData(string Name)
        {            
            patientName = Name;
            string patientFileName = Path.Combine("PatientData",patientName+".csv");
            if (!File.Exists(patientFileName))
                return false;

            try
            {
                DataTable patientInfo = GetDataTableFromCsv(patientFileName, false);
                firstNameTextBox.Text = patientInfo.Columns[0].ToString();
                lastNameTextBox.Text = patientInfo.Columns[1].ToString();
                aptDateTextBox.Text = patientInfo.Columns[2].ToString();
                providerNameTextBox.Text = patientInfo.Columns[3].ToString();
                aptDescriptionTextBox.Text = patientInfo.Columns[4].ToString();
            }
            catch
            {
                return false;
            }

            return true;
        }

        static DataTable GetDataTableFromCsv(string path, bool isFirstRowHeader)
        {
            string[] Lines = File.ReadAllLines(path);
            string[] Fields;
            Fields = Lines[0].Split(new char[] { ',' });
            int Cols = Fields.GetLength(0);
            DataTable dt = new DataTable();
            //1st row must be column names; force lower case to ensure matching later on.
            for (int i = 0; i < Cols; i++)
                dt.Columns.Add(Fields[i].ToLower(), typeof(string));
            DataRow Row;
            for (int i = 1; i < Lines.GetLength(0); i++)
            {
                Fields = Lines[i].Split(new char[] { ',' });
                Row = dt.NewRow();
                for (int f = 0; f < Cols; f++)
                    Row[f] = Fields[f];
                dt.Rows.Add(Row);
            }

            return dt;
        }

        private void firstNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void PatientStatsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
