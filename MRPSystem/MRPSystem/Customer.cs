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
using MRPSystem.Model;

namespace MRPSystem
{
    public partial class Customer : Form
    {
        public static string CompDB = ConfigurationManager.ConnectionStrings["Comp"].ConnectionString;
        TextBox changbox = new TextBox();
        public Customer()
        {
            InitializeComponent();
            button1_Click(null, null);
            //var taxlist = Common.GetTaxtype();
            //taxtype.DataSource = taxlist;
            //taxtype.DisplayMember = "Value";
            //taxtype.ValueMember = "Text";


        }
        private void ShowGridHead()
        {


            customerData.Columns[0].HeaderText = "客戶ID";
            customerData.Columns[0].Width = 120;
            customerData.Columns[1].HeaderText = "名稱";
            customerData.Columns[1].Width = 200;
            ;



        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sqlstr = string.Format("SELECT custno,custname,shortname,boss,connectman,compaddr,postid,offtel1,offtel2,fax,serno,accutday,paycode,ddrec,taxtype,tax,curcode,remark1,email,homepage  FROM aaccust with (nolock)  " +
            "WHERE 1=1 " +
            "ORDER BY custno", sysInfo.Comp);

            //Common.Common.Writetxt(sqlstr);
            var custinfo = DAOMSSQL.GetQueryList<custInfo>(sqlstr, CompDB);
            if (custinfo != null && custinfo.Count > 0)
            {
                var Olist = from d in custinfo
                            orderby d.custno
                            select new { d.custno, d.custname };
                customerData.DataSource = Olist.ToList();
                ShowGridHead();

            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string strsql = "insert into aaccust(custno,custname,shortname,boss,connectman,compaddr,postid,offtel1,offtel2,fax,serno,accutday,paycode,ddrec,taxtype,tax,curcode,remark1,email,homepage ) values('"  ;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string strsql = "UPDATE SaleCustom SET i_lastDateTime = '20221116 15:12:54.930', i_modUser = 'A00000000001', i_modDate = '20221116 15:06:28.170' WHERE i_custom = 'A00000000001' AND i_company = 'A00000000001' AND i_lastDateTime = '20221110 16:24:11.350' AND i_no = 'CS202211001' AND i_clerk IS " +
                "NULL  AND i_name = 'Test001' AND i_nameE = 'Test001' AND i_shortcut = 'Test001' AND i_shortcutE = 'Test001' AND i_active = 'T' AND i_state = 0 AND i_export = 'F' AND i_ceo IS NULL  AND i_phone1 IS NULL  AND i_phone2 IS NULL  AND i_fax IS NULL  AND i_mobile IS NULL  AND " +
                "              i_bbCall IS NULL  AND i_addr1 IS NULL  AND i_zip IS NULL  AND i_email IS NULL  AND i_websit = 'www..com' AND i_bank = '' AND i_bankAcc = '' AND i_memo IS NULL  AND i_taxid = '12345678' AND i_taxType = 0 AND i_taxRate = 5.000000 AND i_agent IS NULL  AND i_agentA IS NULL " +
                "AND i_service IS NULL  AND i_serviceA IS NULL  AND i_payto = '' AND i_billto = '' AND i_sendto = '' AND i_supplier = '' AND i_currency = 'A00000000001' AND i_limit IS NULL  AND i_dealDate IS NULL  AND i_transDate = '20221110 00:00:00.000' AND i_sellType = 'A00000000001' AND " +
                "i_areaType = '' AND i_countryType = '' AND i_cusGroup = 'A00000000001' AND i_groupType IS NULL  AND i_tradeType = 'A00000000001' AND i_shipType = 'A00000000001' AND i_term = 'A00000000001' AND i_billType = 1 AND i_doBill = 0 AND i_marker IS NULL  AND i_companyKind IS NULL  AND i_registerDept IS NULL  AND i_companyNo IS NULL  AND i_fromCmp IS NULL  AND i_parentCompany IS NULL  AND i_factoryNo IS NULL  AND i_sales IS NULL  AND i_bizContent IS NULL  AND i_level = 'E' AND i_manageCompany = 'A00000000001' AND i_introducer IS NULL  AND i_priceDiscount = 0.0 AND i_billNo IS NULL  AND i_shipToAddr IS NULL  AND i_sender = '' AND i_recDiscount = 0.0 AND i_payPoint = '0' AND i_shipDepot = '' AND i_backDepot = '' AND i_uniformNo = '89484561' AND i_lastTradeDate IS NULL  AND i_invMonth = 0 AND i_billDate IS NULL  AND i_BankAccName IS NULL  AND i_BankAccNo IS NULL  AND i_totPrincipal = 0.0 AND i_recPrincipal = 0.0 AND i_creditAmount = 0.0 AND i_discountPrinted = 0 AND i_balancebillDate IS NULL  AND i_sendBillDate IS NULL  AND i_RequestbillDate IS NULL  AND i_billName IS NULL  AND i_billAddr IS NULL  AND i_attachedGroup IS NULL  AND i_postCode IS NULL  AND i_safeInvNum IS NULL  AND i_shipToAddr2 IS NULL  AND i_shipToAddr3 IS NULL  AND i_armAcc IS NULL  AND i_armBill IS NULL  AND i_preAcc IS NULL  AND i_slipAcc IS NULL  AND i_priceLevel = 0 AND i_slipMemo IS NULL  AND i_addUser = 'A00000000001' AND i_addDate = '20221110 16:23:52.530' AND i_modUser IS NULL  AND i_modDate IS NULL  AND i_storePlace = '' AND i_storePlaceBack = '' AND i_addrE IS NULL  AND i_addrHouseE IS NULL  AND i_subBankNo IS NULL  AND i_exportTrade = 'F' AND i_saleCarNo IS NULL  AND i_assure = 0 AND i_jnPerDayAmount IS NULL  AND i_jnDayNum IS NULL  AND i_jnNotBackNum IS NULL  AND i_ceoDate IS NULL  AND i_endSaleDate IS NULL  AND i_loginName IS NULL  AND i_password = 741802802 AND i_year = 1995 AND i_month = 10 AND i_day = 10 AND i_loginLock = 'T' AND i_serSubCom = 'F' AND i_tLState = 0 ";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ShowCommon frm = new ShowCommon("sales");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                salesid.Text = ShowCommon.id ; 
                salesname.Text = ShowCommon.name; 
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ShowCommon frm = new ShowCommon("taxtype");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                taxid.Text = ShowCommon.id;
                taxName.Text = ShowCommon.name;
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            ShowCommon frm = new ShowCommon("paycode");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Paycode.Text = ShowCommon.id;
                Payname.Text = ShowCommon.name;
            }
        }
        private void Find(Control col)
        {
            foreach (Control cols in col.Controls)
            {
                if (cols is TextBox)
                {
                    //Console.WriteLine(cols.Text);
                    Console.WriteLine(cols.Name);
                    continue;
                }
                
            }
        }

        private  void GetCustomer(string no)
        {
            string sqlstr = string.Format("SELECT top 1 custno,custname,shortname,boss,connectman,compaddr,postid,offtel1,offtel2,fax,serno,accutday,paycode,ddrec,taxtype,tax,curcode,remark1,email,homepage  FROM aaccust with (nolock)  " +
            "WHERE 1=1 and custno='{0}' " 
            , no);

            //Common.Common.Writetxt(sqlstr);
            Find(this);
        }

        private void customerData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (customerData.Rows.Count > 0) 
            {
                string custno = (string)customerData.Rows[e.RowIndex].Cells[0].Value;
                GetCustomer(custno);
            }
        }
    }
}
