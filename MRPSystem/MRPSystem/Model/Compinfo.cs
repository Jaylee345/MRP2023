using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRPSystem.Model
{
    public class Compinfo
    {
        public string i_company { set; get; }
        public string i_name { set; get; }
        public string i_employee { set; get; }
        public string i_lock { set; get; }
    }
    public class sysInfo
    {
        public static string SFB01;
        public static string Comp;
        public static string CompName;
        public static string Admin;
        public static string acc;
        public static string Code; //Barcode n9
        public static string area; //barcode area
        public static string cref;  // account 綁認證 只能讀特定工單(CN,CT)
    }
    public class AccInfo
    {
        //public static int count;

        public string account { set; get; }
        public string password { set; get; }
        public string status { set; get; }
        public string comm { set; get; }

        public string Name { set; get; }
        public static string CAS { set; get; }
        public static string CAS1 { set; get; }
        public string code { set; get; }
        public string area { set; get; }
        public string Cref { set; get; }
    }
    public class custInfo
    {
        public string i_custom { get; set; }
        public string CusName { get; set; }
        public string i_company { get; set; }
        public string i_no { get; set; }
        public string i_clerk { get; set; }
        public string i_name { get; set; }
        public string i_nameE { get; set; }
        public string i_shortcut { get; set; }
        public string i_shortcutE { get; set; }
        public string i_active { get; set; }
        public int i_state { get; set; }
        public string i_export { get; set; }
        public string i_ceo { get; set; }
        public string i_phone1 { get; set; }
        public string i_phone2 { get; set; }
        public string i_fax { get; set; }
        public string i_mobile { get; set; }
        public string i_bbCall { get; set; }
        public string i_addr1 { get; set; }
        public string i_zip { get; set; }
        public string i_email { get; set; }
        public string i_websit { get; set; }
        public string i_bank { get; set; }
        public string i_bankAcc { get; set; }
        public string i_memo { get; set; }
        public string i_taxid { get; set; }
        public int i_taxType { get; set; }
        public decimal i_taxRate { get; set; }
        public string i_agent { get; set; }
        public string i_agentA { get; set; }
        public string i_service { get; set; }
        public string i_serviceA { get; set; }
        public string i_payto { get; set; }
        public string i_billto { get; set; }
        public string i_sendto { get; set; }
        public string i_supplier { get; set; }
        public string i_currency { get; set; }
        public decimal i_limit { get; set; }

        public string i_sellType { get; set; }
        public string i_areaType { get; set; }
        public string i_countryType { get; set; }
        public string i_cusGroup { get; set; }
        public string i_groupType { get; set; }
        public string i_tradeType { get; set; }
        public string i_shipType { get; set; }
        public string i_term { get; set; }
        public int i_billType { get; set; }
        public int i_doBill { get; set; }
        public string i_marker { get; set; }
        public string i_companyKind { get; set; }
        public string i_registerDept { get; set; }
        public string i_companyNo { get; set; }
        public string i_fromCmp { get; set; }
        public string i_parentCompany { get; set; }
        public string i_factoryNo { get; set; }
        public string i_sales { get; set; }
        public string i_bizContent { get; set; }
        public string i_level { get; set; }
        public string i_manageCompany { get; set; }
        public string i_introducer { get; set; }
        public double i_priceDiscount { get; set; }
        public string i_billNo { get; set; }
        public string i_shipToAddr { get; set; }
        public string i_sender { get; set; }
        public double i_recDiscount { get; set; }
        public string i_payPoint { get; set; }
        public string i_shipDepot { get; set; }
        public string i_backDepot { get; set; }
        public string i_uniformNo { get; set; }

        public int i_invMonth { get; set; }
        public int i_billDate { get; set; }
        public string i_BankAccName { get; set; }
        public string i_BankAccNo { get; set; }
        public double i_totPrincipal { get; set; }
        public double i_recPrincipal { get; set; }
        public double i_creditAmount { get; set; }
        public int i_discountPrinted { get; set; }
        public int i_balancebillDate { get; set; }
        public int i_sendBillDate { get; set; }
        public int i_RequestbillDate { get; set; }
        public string i_billName { get; set; }
        public string i_billAddr { get; set; }
        public string i_attachedGroup { get; set; }
        public string i_postCode { get; set; }
        public double i_safeInvNum { get; set; }
        public string i_shipToAddr2 { get; set; }
        public string i_shipToAddr3 { get; set; }
        public string i_armAcc { get; set; }
        public string i_armBill { get; set; }
        public string i_preAcc { get; set; }
        public string i_slipAcc { get; set; }
        public int i_priceLevel { get; set; }
        public string i_slipMemo { get; set; }
        public string i_addUser { get; set; }
        public string i_modUser { get; set; }
        public string i_storePlace { get; set; }
        public string i_storePlaceBack { get; set; }
        public string i_addrE { get; set; }
        public string i_addrHouseE { get; set; }
        public string i_subBankNo { get; set; }
        public string i_exportTrade { get; set; }
        public string i_saleCarNo { get; set; }
        public int i_assure { get; set; }
        public decimal i_jnPerDayAmount { get; set; }
        public decimal i_jnDayNum { get; set; }
        public decimal i_jnNotBackNum { get; set; }
        public string i_loginName { get; set; }
        public int i_password { get; set; }
        public int i_year { get; set; }
        public int i_month { get; set; }
        public int i_day { get; set; }
        public string i_loginLock { get; set; }
        public string i_serSubCom { get; set; }
        public int i_tLState { get; set; }

    }
}
