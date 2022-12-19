using MRPSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MRPSystem
{
    public partial class ShowCommon : Form
    {
        string CompDB = ConfigurationManager.ConnectionStrings["Comp"].ConnectionString;
        public static string id="",name="";
        public ShowCommon()
        {
            InitializeComponent();
        }
        public ShowCommon(string type) //taxtype  paycode  //sales
        {
            InitializeComponent();  
            string sqlstr = "";
            if (type == "taxtype") 
            {
                //稅別 taxtypeinfo
                sqlstr = "select taxtype, taxdesc from abataxtype ";
                var taxs = DAOMSSQL.GetQueryList<taxtypeinfo>(sqlstr, CompDB);
                if (taxs != null) 
                {
                    CommonInfo.DataSource = taxs;
                    CommonInfo.Columns[0].HeaderText = "代碼";
                    CommonInfo.Columns[1].HeaderText = "稅別";
                    CommonInfo.Columns[0].Width = 90;
                    CommonInfo.Columns[1].Width = 200;

                }
            }
            else if (type == "paycode") 
            {
                //付款方式  paycodeinfo
                sqlstr = "select paycode,paydesc from aodpayh with (nolock) order by paycode";
                var pays = DAOMSSQL.GetQueryList<paycodeinfo>(sqlstr, CompDB);
                if(pays != null)
                {
                    CommonInfo.DataSource = pays;
                    CommonInfo.Columns[0].HeaderText = "代碼";
                    CommonInfo.Columns[1].HeaderText = "付款方式";
                    CommonInfo.Columns[0].Width = 90;
                    CommonInfo.Columns[1].Width = 200;

                }
            }
            else if (type == "sales") 
            {
                //業務   userinfo
                sqlstr = "select userno,fname from asyuser";
                var salepers = DAOMSSQL.GetQueryList<userinfo>(sqlstr, CompDB);
                if (salepers != null)
                {
                    CommonInfo.DataSource = salepers;
                    CommonInfo.Columns[0].HeaderText = "代碼";
                    CommonInfo.Columns[1].HeaderText = "業務";
                    CommonInfo.Columns[0].Width = 90;
                    CommonInfo.Columns[1].Width = 200;

                }
            }

        }
        private void ShowPaycode()
        {
            

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (CommonInfo.Rows.Count > 0)
            {
                id = (string)CommonInfo.Rows[e.RowIndex].Cells[0].Value;
                name = (string)CommonInfo.Rows[e.RowIndex].Cells[1].Value;
                DialogResult = DialogResult.OK;//這一定要設
                Close();
            }
        }
    }
}
