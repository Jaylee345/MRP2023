using MRPSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MRPSystem
{
    public partial class Login : Form
    {
        private TextBox lastTB = null;
        public static string Account;
        MenuForm mainForm = null;
        string CompDB = ConfigurationManager.ConnectionStrings["Comp"].ConnectionString;
        public Login()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

        }
        public Login(Form mform)
        {
            InitializeComponent();
            mainForm = mform as MenuForm;
           
            string sqlstr = " select i_company,i_name from Company ";

            var Compdata = DAOMSSQL.GetQueryList<Compinfo>(sqlstr, CompDB);
            if (Compdata != null && Compdata.Count > 0)
            {

                Comps.DataSource = Compdata;
                Comps.DisplayMember = "i_name";
                Comps.ValueMember = "i_company";
                Comps.SelectedIndex = 0;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {

            label3.Text = "";

            //Common.Common.Writetxt(dbconnect);
            if (string.IsNullOrEmpty(account.Text))
            {
                label3.Text = "帳號空白!";
                account.Focus();

            }
            //if (string.IsNullOrEmpty(pwd.Text))
            //{
            //    label3.Text = "密碼空白!";
            //    pwd.Focus();
            //}
            if (label3.Text != "")
            {
                return;
            }




            var citem = (Compinfo)Comps.SelectedItem;
            string sqlstr = string.Format("select Name=i_name,cref=i_lock from _Login A " +
                   " where i_company='{0}' and i_name='{1}' ", citem.i_company, account.Text);


            //Common.Common.Writetxt(sqlstr);
            var accinfo = DAOMSSQL.GetQueryList<AccInfo>(sqlstr, CompDB).FirstOrDefault();
            if (accinfo != null)
            {
                if (accinfo.Cref == "T")
                {
                    msg.Text = "帳號已被鎖住!!!";
                    return;
                }
                //if (accinfo.password.ToUpper() != pwd.Text.ToUpper())
                //{
                //    msg.Text = "密碼資料不正確.";
                //    return;
                //}

                sysInfo.Comp = citem.i_company;
                sysInfo.CompName = citem.i_name;
                sysInfo.SFB01 = accinfo.comm;
                sysInfo.Admin = accinfo.Name; //(account.Text.Substring(2)=="6")
                sysInfo.acc = account.Text;
                sysInfo.Code = accinfo.code;
                sysInfo.area = accinfo.area;
                sysInfo.cref = accinfo.Cref;

                //AccInfo.count += 1;
                Account = account.Text.ToUpper();

                //toolStripMenuItem3.Enabled = false;
                //toolStripMenuItem4.Enabled = false;
                //入庫管理ToolStripMenuItem.Enabled = false;
                //工單管理ToolStripMenuItem.Enabled = false;

                //MenuForm f = new MenuForm();
                this.mainForm.itemN = true;


                 if (sysInfo.Admin.IndexOf("P") >= 0) //工單管理
                {
                    //    工單管理ToolStripMenuItem.Enabled = true; //工單管理
                    this.mainForm.itemP = true;
                    //    //this.WnoForm.Location = new System.Drawing.Point(112, 376);
                }
                else if (sysInfo.Admin.IndexOf("T") >= 0) //入庫
                {
                    //    入庫管理ToolStripMenuItem.Enabled = true; //入庫
                    this.mainForm.itemT = true;

                    //this.InStock.Location = new System.Drawing.Point(112, 518);
                }

                this.mainForm.setformstatus();
                this.mainForm.openworkForm();
                this.Close();
                //Home newMDIChild = new Home();
                //// Set the parent form of the child window.  
                //newMDIChild.MdiParent = this;
                //// Display the new form.  
                //newMDIChild.Show();

            }
            else
            {
                msg.Text = "無帳號資料!";
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            this.Close();
        }

        private void DBName_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (File.Exists(filename))
                File.Delete(filename);

            string db = Comps.SelectedItem.ToString();
            string path = System.Windows.Forms.Application.StartupPath;

            using (StreamWriter sw = File.AppendText(path + "\\" + filename))
            {
                //WriteLine為換行 
                sw.Write(db);

            }
        }
        protected string filename = "db.txt";
        protected string path = System.Windows.Forms.Application.StartupPath;
        private void Login_Load(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("" + System.Environment.SystemDirectory + "/osk.exe");
            if (File.Exists(filename))
            {
                string item = "";
                using (StreamReader file = new StreamReader(path + "\\" + filename))
                {
                    int counter = 0;
                    string ln;
                    if ((ln = file.ReadLine()) != null)
                    {
                        item = ln;
                    }
                }
                if (!string.IsNullOrEmpty(item))
                    Comps.SelectedItem = item;
            }
            account.ImeMode = ImeMode.Off;
            account.Select();
        }

        private void account_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pwd.Focus();
            }
        }

        private void pwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(pwd.Text))
                return;
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            account.Text = "admin";
            pwd.Text = "admin";
            button1_Click(null, null);
        }


        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }
        private void btn_Click(object sender, EventArgs e)
        {
            string text = ((Button)sender).Text;
            if (!string.IsNullOrEmpty(text))
            {


            }
        }

        private void TB_Enter(object sender, EventArgs e)
        {
            lastTB = (TextBox)sender;
        }

        private void account_TextChanged(object sender, EventArgs e)
        {

        }

        private void button42_Click(object sender, EventArgs e)
        {

        }

        private void pwd_TextChanged(object sender, EventArgs e)
        {
            if (pwd.Text == account.Text)
            {
                button1_Click(null, null);
            }
        }

        private void account_TextChanged_1(object sender, EventArgs e)
        {
            if (account.Text.Length == 3 && (account.Text.ToLower().IndexOf("d6") > 0 || account.Text.IndexOf("06") > 0))
            {
                pwd.Focus();
            }
        }
    }
}
