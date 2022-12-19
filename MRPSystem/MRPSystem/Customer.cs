using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MRPSystem.Model;

namespace MRPSystem
{
    public partial class Customer : Form
    {
        public static string CompDB = ConfigurationManager.ConnectionStrings["Comp"].ConnectionString;
        custInfo custinfo = new custInfo() { };
        private static string mode = "";
        private static string[] excol = { "taxdesc", "PayName","sellerName" };
        public Customer()
        {
            InitializeComponent();
            button1_Click(null, null);
            //var taxlist = Common.GetTaxtype();
            //taxtype.DataSource = taxlist;
            //taxtype.DisplayMember = "Value";
            //taxtype.ValueMember = "Text";
            foreach (Control cols in this.Controls)
            {
                if (cols is TextBox)
                        cols.Enabled = false;

            }


        }
        private void ShowGridHead()
        {


            customerData.Columns[0].HeaderText = "客戶ID";
            customerData.Columns[0].Width = 120;
            customerData.Columns[1].HeaderText = "名稱";
            customerData.Columns[1].Width = 200;
            



        }

        private string gettype(string value) 
        {
            string ValueString = "";
            switch (value)
            {
                case "String":
                    ValueString = "String";
                    break;
                case "Int32":
                    ValueString = "Int32";
                    break;
                case "Boolean":
                    ValueString = "Boolean";
                    break;
                case "Decimal":
                    ValueString = "Decimal";
                    break;
                case "Double":
                    ValueString = "Double";
                    break;
                default:
                    ValueString="型別不符";
                    break;
            }
            return ValueString;
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
            if (string.IsNullOrEmpty(custno.Text))
            {
                msg.Text = "請先叫出客戶資料再執行作業!!!";
                return;
            }
            string Colname = "", colsql = "", ssql = "", esql = "",cno="";
            if (mode == "Add")
            {
                foreach (Control cols in this.Controls)
                {
                    if (cols is TextBox)
                    {
                        if (!string.IsNullOrEmpty(cols.Text))
                        {
                            

                            Colname = cols.Name;
                            colsql += string.Format("{0},", Colname);
                            var getItemInfo = custinfo.GetType().GetProperty(Colname);
                            if (getItemInfo != null)
                            {
                                Type ctype = getItemInfo.GetType();
                                if ((ctype.Name.IndexOf("int") >= 0 || ctype.Name.IndexOf("decimal") >= 0 || ctype.Name.IndexOf("double") >= 0))
                                    ssql += string.Format("{0},", cols.Text);
                                else
                                    ssql += string.Format("'{0}',", cols.Text);

                            }
                            cols.Enabled = false;
                        }

                    }

                }
                if (!string.IsNullOrEmpty(colsql))
                    esql = string.Format("insert into aaccust({0}) values({1}) ", colsql.Substring(0, colsql.Length - 1), ssql.Substring(0, ssql.Length - 1));


            }
            else 
            {
                var k1= Type.GetType("MRPSystem.Model.custInfo.custno");
                foreach (Control cols in this.Controls)
                {
                    if (cols is TextBox)
                    {
                        Colname = cols.Name;
                        if (Colname != "custno" && !excol.Contains(Colname))
                        {
                            
                            if (!string.IsNullOrEmpty(cols.Text))
                            {
                                //colsql += string.Format("{0},", Colname);
                                var getItemInfo = custinfo.GetType().GetProperty(Colname);
                                if (getItemInfo != null)
                                {
                                    
                                    
                                    var ctype = gettype(getItemInfo.PropertyType.Name);
                                    if ((ctype.IndexOf("Int")>=0  || ctype.IndexOf("Decimal")>=0  || ctype.IndexOf("Double") >= 0))
                                        ssql += string.Format("{0} = {1},", Colname, cols.Text);
                                    else
                                        ssql += string.Format("{0}='{1}',", Colname, cols.Text);

                                }
                            }
                            cols.Enabled = false;
                        }
                        else 
                        {
                            cno = cols.Text;
                        }
                    }

                }
                if (!string.IsNullOrEmpty(ssql))
                    esql = string.Format("update aaccust set {0} where custno='{1}' ", ssql.Substring(0, ssql.Length - 1), cno);

            }
            Console.WriteLine(esql);
            Updatebutton.ForeColor = Color.LawnGreen;
            Delbutton.ForeColor = Color.LawnGreen;
            Addbutton.ForeColor = Color.LawnGreen;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(custno.Text))
            {
                msg.Text = "請先叫出客戶資料再執行作業!!!";
                return;
            }
            mode = "Updated";
            Addbutton.Enabled = false;
            Updatebutton.Enabled = false;
            Delbutton.Enabled = false;
            Updatebutton.ForeColor = Color.LightGray;
            Delbutton.ForeColor = Color.LightGray;
            Addbutton.ForeColor = Color.LightGray;
            string Colname = "";
            foreach (Control cols in this.Controls)
            {
                if (cols is TextBox)
                {
                    if (cols.Name != "custno")
                    {
                        cols.Enabled = true;
                        

                    }

                }

            }
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
                sellerno.Text = ShowCommon.id ; 
                sellerName.Text = ShowCommon.name; 
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ShowCommon frm = new ShowCommon("taxtype");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                taxtype.Text = ShowCommon.id;
                taxdesc.Text = ShowCommon.name;
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            ShowCommon frm = new ShowCommon("paycode");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                paycode.Text = ShowCommon.id;
                PayName.Text = ShowCommon.name;
            }
        }
        private string NewCustno(string para)
        {
            string sql = "SELECT max(custno) as custno  FROM aaccust with(nolock) ";
            string no = "";
            int num = 0;
            //stCount = GetMaxNO().ToString("0000");
            using (SqlConnection conn = new SqlConnection(CompDB))
            {
                conn.Open();

                
                SqlCommand command = new SqlCommand(sql, conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        no = reader[0].ToString() ;
                        num = Convert.ToInt32(no.Substring(1, 3));
                    }
                }
                num += 1;
            }
            return string.Format("{0}{1}", para, num.ToString("000"));

        }
        private void Find(string custno)
        {
            string Colname = "";
            string sqlstr = string.Format("SELECT top 1 custno,custname,shortname,boss,connectman,compaddr,postid,offtel1,offtel2,a.fax,serno,accutday,a.paycode,PayName = b.paydesc,ddrec,a.taxtype,taxdesc,tax,curcode,remark1,a.email,a.homepage,sellerno,sellerName=d.lname FROM aaccust a with (nolock) " +
            "left join  aodpayh b with (nolock) on b.paycode=a.paycode " +
            "left join  abataxtype c with (nolock) on c.taxtype=a.taxtype " +
            "left join  asyuser d with (nolock) on d.userno=a.sellerno " +
                "WHERE 1=1 and custno='{0}' "
            , custno); 
            custinfo = DAOMSSQL.GetQuery<custInfo>(sqlstr, CompDB);

            

           var alldata  = custinfo.GetType();
            //PropertyInfo myPropInfo = alldata.GetProperty("custno");
            //var barProperty = custinfo.GetType().GetProperty("custno");
            //string val = barProperty.GetValue(custinfo, null) as string;
            //Console.WriteLine(val);


            foreach (Control cols in this.Controls)
            {
                if (cols is TextBox)
                {
                    Colname = cols.Name;


                    var getItemInfo = custinfo.GetType().GetProperty(Colname);
                    if (getItemInfo != null) 
                    { 
                        string val = getItemInfo.GetValue(custinfo, null) as string;
                        if (!string.IsNullOrEmpty(val))
                            cols.Text = val;
                    }

                }
                
            }
            Updatebutton.Enabled = true;
        }


        private void customerData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (customerData.Rows.Count > 0) 
            {
                string custno = (string)customerData.Rows[e.RowIndex].Cells[0].Value;
                Find(custno);
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {

        }

        private void Addbutton_Click(object sender, EventArgs e)
        {
           
            mode = "Add";
            foreach (Control cols in this.Controls)
            {
                if (cols is TextBox)
                {
                    cols.Enabled = true;

                }
            }
            custno.Text = NewCustno("V");
            Addbutton.Enabled = false;
            Updatebutton.Enabled = false;
            Delbutton.Enabled = false;

        }

        private void Delbutton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(custno.Text)) 
            {
                msg.Text = "請先叫出客戶資料再執行作業!!!";
                return;
            }
            InputBox frm = new InputBox("確定要刪除此筆資料?");
            string s = "";
            //frm.ShowDialog();
            if (frm.ShowDialog() == DialogResult.OK)
            {

                s = InputBox.value.ToString();
            }

        }
    }
}
