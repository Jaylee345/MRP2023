using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security;
using System.Windows.Forms;
using System.Text;
using System.Collections.Generic;
using MRPSystem.Model;
using System.Linq;
using System.Configuration;

namespace MRPSystem
{
    public partial class LoadVendCust : Form
    {
        string CompDB = ConfigurationManager.ConnectionStrings["Comp"].ConnectionString;
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
            string ssql = " SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME in ( 'aacvend') AND TABLE_CATALOG = 'compdata' ";
            var dataTypes = DAOMSSQL.GetQueryList<schemainfo>(ssql, CompDB);

            List<string> inssqls = new List<string>();
            if (File.Exists(txtfile))
            {
                using (StreamReader sr = new StreamReader( txtfile, Encoding.Default))
                {
                    string line;
                    int i1 = 0,i2=0;
                    string ColumnN ="";
                    List<int> colindex = new List<int>();
                    string[] data = new string[80] ,datatype = new string[80];
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
                                    colindex.Add(i1);
                                    Console.WriteLine(EngTitle[1] +"," );
                                    i2 += 1;
                                    var d1 = dataTypes.FirstOrDefault(t => t.COLUMN_NAME==EngTitle[1] );
                                    if (d1 != null) 
                                    {
                                        datatype[i1] = d1.DATA_TYPE;
                                    }

                                }
                                i1 +=1;
                            }
                        }


                    }
                    textBox1.Text = ColumnN.Substring(0, ColumnN.Length-1);
                    for(int c1=0;c1<i2;c1++)
                        textBox2.Text +=  colindex[c1] + "_";

                    string sql1 = "", sql = "",sql2;
                    sql = "insert into aacvend(" + ColumnN.Substring(0, ColumnN.Length - 1) + ",remark) values({0}{1})";
                    while ((line = sr.ReadLine()) != null)
                    {
                        sql1 = "";
                        sql2 = "";
                        if (!string.IsNullOrEmpty(line)) 
                        {
                            data = line.Split(',');
                            if (data[0].Substring(0, 1) == "C") 
                            {
                                foreach (var c1 in colindex)
                                {
                                    if (datatype[c1] == "char" || datatype[c1] == "varchar" || datatype[c1] == "nvarchar")
                                        sql1 += string.Format("'{0}',", data[c1]);
                                    else
                                    {
                                        if(string.IsNullOrEmpty( data[c1]) )
                                            sql1 += string.Format("0,");
                                        else
                                            sql1 += string.Format("{0},", data[c1]);
                                    }
                                }
                                sql2 = ",'" + data[26] + " " + data[57] + " " + data[58] + " " + data[59] + " " + data[60] + " " + data[61] + "'";
                            }
                            if(!string.IsNullOrEmpty(sql1))
                                inssqls.Add(string.Format(sql, sql1.Substring(0, sql1.Length-1), sql2));
                            
                        }
                        //    string id = line.Substring(0, 6);
                        

                    }
                }
                textBox2.Text += Environment.NewLine;
                foreach (var t1 in inssqls)
                    textBox2.Text +=  t1 + Environment.NewLine;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var e1 = Encoding.Default;
            string txtfile = openFileDialog1.FileName;
            string ssql = " SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME in ('aaccust') AND TABLE_CATALOG = 'compdata' ";
            var dataTypes = DAOMSSQL.GetQueryList<schemainfo>(ssql, CompDB);

            List<string> inssqls = new List<string>();
            if (File.Exists(txtfile))
            {
                using (StreamReader sr = new StreamReader(txtfile, Encoding.Default))
                {
                    string line;
                    int i1 = 0, i2 = 0;
                    string ColumnN = "";
                    List<int> colindex = new List<int>();
                    string[] data = new string[80], datatype = new string[80];
                    line = sr.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] ChtTitle = line.Split(',');
                        if (ChtTitle != null)
                        {
                            foreach (var item in ChtTitle)
                            {
                                string[] EngTitle = item.Split('-');
                                if (EngTitle != null && EngTitle.Length > 1)
                                {
                                    ColumnN += EngTitle[1] + ",";
                                    colindex.Add(i1);
                                    Console.WriteLine(EngTitle[1] + ",");
                                    i2 += 1;
                                    var d1 = dataTypes.FirstOrDefault(t => t.COLUMN_NAME == EngTitle[1]);
                                    if (d1 != null)
                                    {
                                        datatype[i1] = d1.DATA_TYPE;
                                    }

                                }
                                i1 += 1;
                            }
                        }


                    }
                    textBox1.Text = ColumnN.Substring(0, ColumnN.Length - 1);
                    for (int c1 = 0; c1 < i2; c1++)
                        textBox2.Text += colindex[c1] + "_";

                    string sql1 = "", sql = "", sql2="";
                    sql = "insert into aaccust(" + ColumnN.Substring(0, ColumnN.Length - 1) + ",remark1) values({0}{1})";
                    while ((line = sr.ReadLine()) != null)
                    {
                        sql1 = "";
                        if (!string.IsNullOrEmpty(line))
                        {
                            data = line.Split(',');
                            //////-------------------////
                            if (data[0].Substring(0, 1) == "V")
                            {
                                foreach (var c1 in colindex)
                                {
                                    if (datatype[c1] == "char" || datatype[c1] == "varchar" || datatype[c1] == "nvarchar")
                                        sql1 += string.Format("'{0}',", data[c1]);
                                    else
                                    {
                                        if (string.IsNullOrEmpty(data[c1]))
                                            sql1 += string.Format("0,");
                                        else
                                            sql1 += string.Format("{0},", data[c1]);
                                    }
                                }
                                sql2 = ",'" + data[26] + " " + data[57] + " " + data[58] + " " + data[59] + " " + data[60] + " " + data[61] + "'";
                            }
                            if (!string.IsNullOrEmpty(sql1))
                                inssqls.Add(string.Format(sql, sql1.Substring(0, sql1.Length - 1), sql2));
                        }
                        //    string id = line.Substring(0, 6);


                    }
                }
                textBox2.Text += Environment.NewLine;
                foreach (var t1 in inssqls)
                    textBox2.Text += t1 + Environment.NewLine;
            }
        }
    }
}
