using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MRPSystem
{
    public partial class Customer : Form
    {
        string CompDB = ConfigurationManager.ConnectionStrings["Comp"].ConnectionString;
        public Customer()
        {
            InitializeComponent();
            string sqlstr = string.Format("SELECT customPK, CusName, i_custom, i_company, i_lastDateTime, i_no, i_clerk, i_name, i_nameE, i_shortcut, i_shortcutE, i_active, i_state, i_export, i_ceo, i_phone1, i_phone2, i_fax, i_mobile, i_bbCall, i_addr1, i_zip, i_email, i_websit, i_bank, i_bankAcc, i_memo, i_taxid, i_taxType, i_taxRate, i_agent, i_agentA, i_service, i_serviceA, i_payto, i_billto, i_sendto, i_supplier, i_currency, i_limit, i_dealDate, i_transDate, i_sellType, i_areaType, i_countryType, i_cusGroup, i_groupType, i_tradeType, i_shipType, i_term, i_billType, i_doBill, i_marker, i_companyKind, i_registerDept, i_companyNo, i_fromCmp, i_parentCompany, i_factoryNo, i_sales, i_bizContent, i_level, i_manageCompany, i_introducer, i_priceDiscount, i_billNo, i_shipToAddr, i_sender, i_recDiscount, i_payPoint, i_shipDepot, i_backDepot, i_uniformNo, i_lastTradeDate, i_invMonth, i_billDate, i_BankAccName, i_BankAccNo, i_totPrincipal, i_recPrincipal, i_creditAmount, i_discountPrinted, i_balancebillDate, i_sendBillDate, i_RequestbillDate, i_billName, i_billAddr, i_checkSeal, i_attachedGroup, i_postCode, i_safeInvNum, i_shipToAddr2, i_shipToAddr3, i_armAcc, i_armBill, i_preAcc, i_slipAcc, i_priceLevel, i_slipMemo, i_addUser, i_addDate, i_modUser, i_modDate, i_storePlace, i_storePlaceBack, i_addrE, i_addrHouseE, i_subBankNo, i_exportTrade, i_saleCarNo, i_assure, i_jnPerDayAmount, i_jnDayNum, i_jnNotBackNum, i_ceoDate, i_endSaleDate, i_loginName, i_password, i_year, i_month, i_day, i_loginLock, i_serSubCom, i_tLState FROM v_SaleCustom  " +
            "WHERE i_company> ''  and i_company = '{0}' " +
            "ORDER BY i_no", sysInfo.Comp);

            //Common.Common.Writetxt(sqlstr);
            var custinfo = DAOMSSQL.GetQueryList<custInfo>(sqlstr, CompDB);
            if (custinfo != null && custinfo.Count > 0)
            {
                var Olist = from d in custinfo
                            orderby d.i_no
                            select new { d.i_no, d.CusName };
                customerData.DataSource = Olist.ToList();
                ShowGridHead();

            }

        }
        private void ShowGridHead()
        {


            customerData.Columns[0].HeaderText = "客戶ID";
            customerData.Columns[0].Width = 120;
            customerData.Columns[1].HeaderText = "名稱";
            customerData.Columns[1].Width = 120;
            ;



        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sqlstr = string.Format("SELECT customPK, CusName, i_custom, i_company, i_lastDateTime, i_no, i_clerk, i_name, i_nameE, i_shortcut, i_shortcutE, i_active, i_state, i_export, i_ceo, i_phone1, i_phone2, i_fax, i_mobile, i_bbCall, i_addr1, i_zip, i_email, i_websit, i_bank, i_bankAcc, i_memo, i_taxid, i_taxType, i_taxRate, i_agent, i_agentA, i_service, i_serviceA, i_payto, i_billto, i_sendto, i_supplier, i_currency, i_limit, i_dealDate, i_transDate, i_sellType, i_areaType, i_countryType, i_cusGroup, i_groupType, i_tradeType, i_shipType, i_term, i_billType, i_doBill, i_marker, i_companyKind, i_registerDept, i_companyNo, i_fromCmp, i_parentCompany, i_factoryNo, i_sales, i_bizContent, i_level, i_manageCompany, i_introducer, i_priceDiscount, i_billNo, i_shipToAddr, i_sender, i_recDiscount, i_payPoint, i_shipDepot, i_backDepot, i_uniformNo, i_lastTradeDate, i_invMonth, i_billDate, i_BankAccName, i_BankAccNo, i_totPrincipal, i_recPrincipal, i_creditAmount, i_discountPrinted, i_balancebillDate, i_sendBillDate, i_RequestbillDate, i_billName, i_billAddr, i_checkSeal, i_attachedGroup, i_postCode, i_safeInvNum, i_shipToAddr2, i_shipToAddr3, i_armAcc, i_armBill, i_preAcc, i_slipAcc, i_priceLevel, i_slipMemo, i_addUser, i_addDate, i_modUser, i_modDate, i_storePlace, i_storePlaceBack, i_addrE, i_addrHouseE, i_subBankNo, i_exportTrade, i_saleCarNo, i_assure, i_jnPerDayAmount, i_jnDayNum, i_jnNotBackNum, i_ceoDate, i_endSaleDate, i_loginName, i_password, i_year, i_month, i_day, i_loginLock, i_serSubCom, i_tLState FROM v_SaleCustom  " +
            "WHERE i_company> ''  and i_company = '{0}' " +
            "ORDER BY i_no", sysInfo.Comp);

            //Common.Common.Writetxt(sqlstr);
            var custinfo = DAOMSSQL.GetQueryList<custInfo>(sqlstr, CompDB);
            if (custinfo != null && custinfo.Count > 0)
            {
                var Olist = from d in custinfo
                            orderby d.i_no
                            select new { d.i_no, d.CusName };
                customerData.DataSource = Olist.ToList();
                ShowGridHead();

            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string strsql = "INSERT INTO SaleCustom(i_custom, i_company, i_lastDateTime, i_no, i_name, i_nameE, i_shortcut, i_shortcutE, i_active, i_state, " +
                "i_export, i_ceo, i_phone1, i_phone2, i_fax, i_addr1, i_email, i_websit, i_bank, i_bankAcc, " +
                "i_taxid, i_taxType, i_taxRate, i_agent, i_agentA, i_service, i_serviceA, i_payto, i_billto, i_sendto, " +
                " i_supplier, i_currency, i_transDate, i_sellType, i_areaType, i_countryType, i_cusGroup, i_tradeType, i_shipType, i_term, " +
                " i_billType, i_doBill, i_companyNo, i_parentCompany, i_factoryNo, i_level, i_manageCompany, i_introducer, i_priceDiscount, i_shipToAddr, " +
                " i_sender, i_recDiscount, i_payPoint, i_shipDepot, i_backDepot, i_uniformNo, i_BankAccName, i_BankAccNo, i_totPrincipal, i_recPrincipal, " +
                "i_discountPrinted, i_balancebillDate, i_sendBillDate, i_RequestbillDate, i_billName, i_billAddr, i_postCode, i_safeInvNum, i_shipToAddr2, i_shipToAddr3, " +
                " i_armAcc, i_armBill, i_preAcc, i_slipAcc, i_priceLevel, i_slipMemo, i_addUser, i_addDate, i_storePlace, i_storePlaceBack, " +
                "i_addrE, i_addrHouseE, i_subBankNo, i_exportTrade, i_assure, i_ceoDate, i_serSubCom, i_tLState) " +
                " VALUES('A00000000002', 'A00000000001', '20221114 13:08:51.823', 'CS202211002', '1', '2', '11', '31', 'T', 0, 'F', '111', '4', '33', '5', '9', '22', 'www..com', 'A00000000001', 'A00000000001', '3', 0, 5.000000, 'A00000000001', 'A00000000001', 'A00000000002', 'A00000000002', 'A00000000001', 'A00000000001', '', '', 'A00000000001', '20221114 00:00:00.000', 'A00000000001', '', '', 'A00000000001', 'A00000000001', 'A00000000001', 'A00000000001', 1, 0, '44', '', '7', 'E', 'A00000000001', '6', 0.0, '1', 'A00000000002', 0.0, '0', 'A00000000101', 'A00000000094', '89484561', '1235', '12345', 200.0, 100.0, 0, 1, 3, 2, 'Test001', '12333', '8', 1.0, '2', '3', 'A00000000005', 'A00000000011', 'A00000000030', 'A00000000015', 0, '12444', 'A00000000001', '20221114 13:04:30.123', 'A00000000101', 'A00000000094', 'eng1', 'eng2', '1234', 'F', 0, '20050824 13:57:43.513', 'F', 1)";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string strsql = "UPDATE SaleCustom SET i_lastDateTime = '20221116 15:12:54.930', i_modUser = 'A00000000001', i_modDate = '20221116 15:06:28.170' WHERE i_custom = 'A00000000001' AND i_company = 'A00000000001' AND i_lastDateTime = '20221110 16:24:11.350' AND i_no = 'CS202211001' AND i_clerk IS " +
                "NULL  AND i_name = 'Test001' AND i_nameE = 'Test001' AND i_shortcut = 'Test001' AND i_shortcutE = 'Test001' AND i_active = 'T' AND i_state = 0 AND i_export = 'F' AND i_ceo IS NULL  AND i_phone1 IS NULL  AND i_phone2 IS NULL  AND i_fax IS NULL  AND i_mobile IS NULL  AND " +
                "              i_bbCall IS NULL  AND i_addr1 IS NULL  AND i_zip IS NULL  AND i_email IS NULL  AND i_websit = 'www..com' AND i_bank = '' AND i_bankAcc = '' AND i_memo IS NULL  AND i_taxid = '12345678' AND i_taxType = 0 AND i_taxRate = 5.000000 AND i_agent IS NULL  AND i_agentA IS NULL " +
                "AND i_service IS NULL  AND i_serviceA IS NULL  AND i_payto = '' AND i_billto = '' AND i_sendto = '' AND i_supplier = '' AND i_currency = 'A00000000001' AND i_limit IS NULL  AND i_dealDate IS NULL  AND i_transDate = '20221110 00:00:00.000' AND i_sellType = 'A00000000001' AND " +
                "i_areaType = '' AND i_countryType = '' AND i_cusGroup = 'A00000000001' AND i_groupType IS NULL  AND i_tradeType = 'A00000000001' AND i_shipType = 'A00000000001' AND i_term = 'A00000000001' AND i_billType = 1 AND i_doBill = 0 AND i_marker IS NULL  AND i_companyKind IS NULL  AND i_registerDept IS NULL  AND i_companyNo IS NULL  AND i_fromCmp IS NULL  AND i_parentCompany IS NULL  AND i_factoryNo IS NULL  AND i_sales IS NULL  AND i_bizContent IS NULL  AND i_level = 'E' AND i_manageCompany = 'A00000000001' AND i_introducer IS NULL  AND i_priceDiscount = 0.0 AND i_billNo IS NULL  AND i_shipToAddr IS NULL  AND i_sender = '' AND i_recDiscount = 0.0 AND i_payPoint = '0' AND i_shipDepot = '' AND i_backDepot = '' AND i_uniformNo = '89484561' AND i_lastTradeDate IS NULL  AND i_invMonth = 0 AND i_billDate IS NULL  AND i_BankAccName IS NULL  AND i_BankAccNo IS NULL  AND i_totPrincipal = 0.0 AND i_recPrincipal = 0.0 AND i_creditAmount = 0.0 AND i_discountPrinted = 0 AND i_balancebillDate IS NULL  AND i_sendBillDate IS NULL  AND i_RequestbillDate IS NULL  AND i_billName IS NULL  AND i_billAddr IS NULL  AND i_attachedGroup IS NULL  AND i_postCode IS NULL  AND i_safeInvNum IS NULL  AND i_shipToAddr2 IS NULL  AND i_shipToAddr3 IS NULL  AND i_armAcc IS NULL  AND i_armBill IS NULL  AND i_preAcc IS NULL  AND i_slipAcc IS NULL  AND i_priceLevel = 0 AND i_slipMemo IS NULL  AND i_addUser = 'A00000000001' AND i_addDate = '20221110 16:23:52.530' AND i_modUser IS NULL  AND i_modDate IS NULL  AND i_storePlace = '' AND i_storePlaceBack = '' AND i_addrE IS NULL  AND i_addrHouseE IS NULL  AND i_subBankNo IS NULL  AND i_exportTrade = 'F' AND i_saleCarNo IS NULL  AND i_assure = 0 AND i_jnPerDayAmount IS NULL  AND i_jnDayNum IS NULL  AND i_jnNotBackNum IS NULL  AND i_ceoDate IS NULL  AND i_endSaleDate IS NULL  AND i_loginName IS NULL  AND i_password = 741802802 AND i_year = 1995 AND i_month = 10 AND i_day = 10 AND i_loginLock = 'T' AND i_serSubCom = 'F' AND i_tLState = 0 ";
        }
    }
}
