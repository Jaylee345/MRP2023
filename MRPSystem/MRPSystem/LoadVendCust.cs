using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security;
using System.Windows.Forms;
using System.Text;
using System.Collections.Generic;

namespace MRPSystem
{
    public partial class LoadVendCust : Form
    {
        public LoadVendCust()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    filePath.Text = openFileDialog1.FileName; 
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var e1= Encoding.Default;
            string txtfile = openFileDialog1.FileName;
            List<string> inssqls = new List<string>();
            if (File.Exists(txtfile))
            {
                using (StreamReader sr = new StreamReader( txtfile, Encoding.Default))
                {
                    string line;
                    int i1 = 0,i2=0;
                    string ColumnN ="";
                    int[] colindex = new int[50];
                    string[] data = new string[50];
                    line = sr.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] ChtTitle = line.Split(',');
                        if (ChtTitle != null) 
                        {
                            foreach (var item in ChtTitle) 
                            {
                                string[] EngTitle = item.Split('-');
                                if (EngTitle != null && EngTitle.Length >1) 
                                {
                                    ColumnN += EngTitle[1] + ",";
                                    colindex[i2] = i1;
                                    Console.WriteLine(EngTitle[1] +"," );
                                    i2 += 1;
                                }
                                i1 +=1;
                            }
                        }


                    }
                    textBox1.Text = ColumnN.Substring(0, ColumnN.Length-1);
                    for(int c1=0;c1<i2;c1++)
                        textBox2.Text +=  colindex[c1] + "_";

                    string sql1 = "", sql = "";
                    sql = "insert into aacvned(" + ColumnN.Substring(0, ColumnN.Length - 1) + ") values({0})";
                    while ((line = sr.ReadLine()) != null)
                    {
                        sql1 = "";
                        if (!string.IsNullOrEmpty(line)) 
                        {
                            data = line.Split(',');
                            if (data[0].Substring(0, 1) == "C") 
                            {
                                for (int c1 = 0; c1 < i2; c1++)
                                    sql1 += string.Format("'{0}',", data[c1]);
                            }
                            if(!string.IsNullOrEmpty(sql1))
                                inssqls.Add(string.Format(sql, sql1));
                        }
                        //    string id = line.Substring(0, 6);
                        

                    }
                }
                foreach (var t1 in inssqls)
                    textBox2.Text += t1 + Environment.NewLine;
            }
        }
    }
}
