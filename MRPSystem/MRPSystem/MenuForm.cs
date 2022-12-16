
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MRPSystem.Model;

namespace MRPSystem
{
    public partial class MenuForm : Form
    {
        string openform = "";
        public Form newMDIChild = null;
        public MenuForm()
        {
            InitializeComponent();
            //this.Text = string.Format("金豐盛 - 工單報工系統 {0}", ip[0]);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void MenuForm_Load(object sender, EventArgs e)
        {

            this.Text = string.Format("進銷存系統 ");
            WindowState = FormWindowState.Maximized;

            newMDIChild = new Login(this);
            // Set the parent form of the child window.  
            newMDIChild.MdiParent = this;
            // Display the new form.  
            newMDIChild.Show();
            if (sysInfo.Admin != null && sysInfo.Admin.IndexOf("S") >= 0) //管理者 1
            {

                //揀貨出ToolStripMenuItem.Enabled = true;
            }

            if (sysInfo.Admin != null && sysInfo.Admin.IndexOf("P") >= 0) //工單管理
            {

                //this.WnoForm.Location = new System.Drawing.Point(112, 376);
            }
            if (sysInfo.Admin != null && sysInfo.Admin.IndexOf("I") >= 0) //入庫
            {

                //this.InStock.Location = new System.Drawing.Point(112, 518);
            }


            //if (AccInfo.Admin != null)
            //        this.Exit();

        }

        private void 參數設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 一般模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 派工模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 工單管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }



        public bool itemS
        {
            set
            {
                //分派工單ToolStripMenuItem.Enabled = true;
                //參數設定ToolStripMenuItem.Enabled = true;
                //入庫管理ToolStripMenuItem.Enabled = true;
                //入庫退回ToolStripMenuItem.Enabled = true;
                //掃標揀貨ToolStripMenuItem.Enabled = true;
                //零散退料ToolStripMenuItem.Enabled = true;
                //整件退料ToolStripMenuItem.Enabled = true;
                //發料作業ToolStripMenuItem.Enabled = true;
                //完工復原ToolStripMenuItem.Enabled = true;
                //條碼資料刪除ToolStripMenuItem.Enabled = true;
            }
        }
        public bool itemI
        {
            set
            {
                //入庫管理ToolStripMenuItem.Enabled = true;
                //入庫退回ToolStripMenuItem.Enabled = true;
                //掃標揀貨ToolStripMenuItem.Enabled = true;
                //零散退料ToolStripMenuItem.Enabled = true;
                //整件退料ToolStripMenuItem.Enabled = true;
                //發料作業ToolStripMenuItem.Enabled = true;
                //完工復原ToolStripMenuItem.Enabled = true;
                //條碼資料刪除ToolStripMenuItem.Enabled = true;
            }
        }
        public void openworkForm()
        {
            string AutoOpenWork = ConfigurationManager.AppSettings["AutoOpenWork"];
            if (string.IsNullOrEmpty(AutoOpenWork) && sysInfo.acc != null)
                if (sysInfo.acc.Length == 3 && (sysInfo.acc.IndexOf("06") > 0 || sysInfo.acc.ToLower().IndexOf("d6") > 0))
                    派工模式ToolStripMenuItem_Click(null, null);
        }
        public bool itemP
        {
            set
            {
                //分派工單ToolStripMenuItem.Enabled = true;
                //發料作業ToolStripMenuItem.Enabled = true;
                //零散退料ToolStripMenuItem.Enabled = true;
                //整件退料ToolStripMenuItem.Enabled = true;
                //完工復原ToolStripMenuItem.Enabled = true;
                //條碼資料刪除ToolStripMenuItem.Enabled = true;
            }
        }
        public bool itemN
        {
            set
            {
                //一般模式ToolStripMenuItem.Enabled = true;
                //派工模式ToolStripMenuItem.Enabled = true;
                //發料作業ToolStripMenuItem.Enabled = true;
                //零散退料ToolStripMenuItem.Enabled = true;
                //整件退料ToolStripMenuItem.Enabled = true;
                //整件退料ToolStripMenuItem.Enabled = true;
                //標籤併件ToolStripMenuItem.Enabled = true;
                //完工復原ToolStripMenuItem.Enabled = true;
                //條碼資料刪除ToolStripMenuItem.Enabled = true;
            }
        }
        public bool itemT
        {
            set
            {
                //入庫管理ToolStripMenuItem.Enabled = true;
                //入庫退回ToolStripMenuItem.Enabled = true;
                //發料作業ToolStripMenuItem.Enabled = true;
                //零散退料ToolStripMenuItem.Enabled = true;
                //整件退料ToolStripMenuItem.Enabled = true;
                //完工復原ToolStripMenuItem.Enabled = true;
                //條碼資料刪除ToolStripMenuItem.Enabled = true;
            }
        }

        private void 結束ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private bool checkForm()
        {
            if (newMDIChild != null && newMDIChild.Name == "Login")
            {
                newMDIChild.Close();
                return true;
            }
            else if (newMDIChild != null)
            {
                DialogResult myResult = MessageBox.Show("是否要結束已開啟的表單", "開啟功能"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (myResult == DialogResult.Yes)
                {
                    newMDIChild.Close();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            return true;
        }
        public void setformstatus()
        {
            newMDIChild = null;

        }

        private void 登出ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (newMDIChild != null)
                newMDIChild.Close();

            sysInfo.Admin = "";
            sysInfo.SFB01 = "";
            MenuForm_Load(null, null);
        }

        private void 報價單ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 客戶設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newMDIChild = new Customer();
            newMDIChild.MdiParent = this;
            // Display the new form.  
            newMDIChild.Show();
            //入庫管理ToolStripMenuItem.Checked = true;
            openform = "Customer";

        }
    }
}
