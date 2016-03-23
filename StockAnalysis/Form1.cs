using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net; // Needed for web client
using System.IO; // This is necessary for file streaming 

namespace StockAnalysis
{
    public partial class StockAnalysis : Form
    {
        int buttonClick;
        string[] myArr; 
        WebClient wc = new WebClient(); // needed to download file 
        public StockAnalysis()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog(); // prompts user to select folder

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                location.Text = fbd.SelectedPath;
                String websiteFirst = "http://real-chart.finance.yahoo.com/table.csv?s=";
                String websiteSecond = "&d=2&e=21&f=2016&g=d&a=5&b=29&c=2010&ignore=.csv";
                String stock = stockSymb.Text; //"TSLA";
                Uri url = new Uri(websiteFirst+stock+websiteSecond); 
                wc.DownloadFile(url, location.Text + @"\finance_" +stock +".csv"); // Can be used to download files. Saves location and FileName  

                FileStream fs = new FileStream(location.Text + @"\finance_" + stock + ".csv", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                DataSet dataset = new DataSet();
                dataset.Tables.Add("FinData");
                dataset.Tables["FinData"].Columns.Add("Date");
                dataset.Tables["FinData"].Columns.Add("Open");
                dataset.Tables["FinData"].Columns.Add("High");
                dataset.Tables["FinData"].Columns.Add("Low");
                dataset.Tables["FinData"].Columns.Add("Close");
                dataset.Tables["FinData"].Columns.Add("Volume");
                dataset.Tables["FinData"].Columns.Add("Adj Close");

                string item; 
                while((item = sr.ReadLine()) != null){
                    myArr = item.Split(',');
                    dataset.Tables["FinData"].Rows.Add(myArr); 
                }
                dataGridView1.DataSource = dataset.Tables[0].DefaultView;
                dataGridView1.Rows.Remove(dataGridView1.Rows[0]);
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);

            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void location_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            buttonClick++;
            if (buttonClick%2 == 0)
            {
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
                order.Text = "Ascending";
            }
            else if(buttonClick%2 != 0)
            {
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
                order.Text = "Descending";

            }


        }
    }
}
