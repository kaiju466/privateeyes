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
    public partial class ProviderStatsForm : Form
    {
        #region Private Fields
        private string providerName { get; set; }
        private int aptIndex { get; set; }
        #endregion

        public ProviderStatsForm()
        {
            providerName = string.Empty;
            InitializeComponent();
        }

        //public bool LoadNextDate(string Name)
        //{
        //    // figure out which file we are currently using

        //    // parse out numer

        //    // determine next numer

        //    //
        //}

        public void LoadAptData(int counter)
        {
            string aptFileName = Path.Combine("ProviderData", providerName + String.Format("_apts{0}.csv", counter%3));
            scheduleLabel.Text = String.Format("Schedule:{0}/3",counter%3+1); 
            //aptIndex = (aptIndex+1)%3;//TODO Get the proper index dynamically
            if (File.Exists(aptFileName))
            {
                DataTable appointments = GetDataTableFromCsv(aptFileName, false);
                string aptInfoString = string.Empty;
                scheduleListBox.Items.Clear();
                foreach (DataRow appointment in appointments.Rows)
                {
                    aptInfoString = string.Format("{0},{1} - {2}", appointment[1].ToString(), appointment[0].ToString(), appointment[2].ToString());
                    scheduleListBox.Items.Add(aptInfoString);
                }
                //   scheduleListBox.SelectedIndex = Int32.Parse(providerInfo.Columns[4].ToString());

            }
        }

        public bool LoadData(string Name, int aptFileNum)
        {
            providerName = Name;
            string providerFileName = Path.Combine("ProviderData", providerName + ".csv");
            if (!File.Exists(providerFileName))
                return false;

            try
            {
                DataTable providerInfo = GetDataTableFromCsv(providerFileName, false);
                firstNameTextBox.Text = providerInfo.Columns[0].ToString();
                lastNameTextBox.Text = providerInfo.Columns[1].ToString();
                titleTextBox.Text = providerInfo.Columns[2].ToString();
                securityLevelTextBox.Text = providerInfo.Columns[3].ToString();


                // Update listbox with all appointments
                scheduleListBox.Items.Clear();
                aptIndex = 0;
                string aptFileName = Path.Combine("ProviderData", providerName+String.Format("_apts{0}.csv", aptFileNum));
                LoadAptData(aptFileNum);
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

        private void scheduleListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
