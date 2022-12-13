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
    public class taxtypeinfo
    {
        public string taxtype { set; get; }
        public string taxdesc { set; get; }
        
    }
    public class userinfo
    {
        public string userno { set; get; }
        public string fname { set; get; }

    }
    public class paycodeinfo
    {
        public string paycode { set; get; }
        public string paydesc { set; get; }

    }
    public class schemainfo
    {
        public string COLUMN_NAME { set; get; }
        public string DATA_TYPE { set; get; }
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
    //vendno,vendname,shortname,boss,connectman,compaddr,postid,offtel1,offtel2,
    public class Vendinfo
    {
        public string vendno { set; get; }
        public string vendname { set; get; }
        public string shortname { set; get; }
        public string boss { set; get; }
        public string connectman { set; get; }
        public string postid { set; get; }
        public string offtel1 { set; get; }
        public string offtel2 { set; get; }
        public string fax { set; get; }
        public string serno { set; get; }
        public int accutday { set; get; }
        public string paycode { set; get; }
        public int ddrec { set; get; }
        public string taxtype { set; get; }
        public Double tax { set; get; }
        public string curcode { set; get; }
        public string remark1 { set; get; }
        

        //fax,serno,accutday,paycode,ddrec,taxtype,tax,curcode,remark
    }
    public class custInfo
    {
        public string custno { set; get; }
        public string custname { set; get; }
        public string shortname { set; get; }
        public string boss { set; get; }
        public string connectman { set; get; }
        public string postid { set; get; }
        public string offtel1 { set; get; }
        public string offtel2 { set; get; }
        public string fax { set; get; }
        public string serno { set; get; }
        public int accutday { set; get; }
        public string paycode { set; get; }
        public int ddrec { set; get; }
        public string taxtype { set; get; }
        public Double tax { set; get; }
        public string curcode { set; get; }
        public string remark1 { set; get; } //email,homepage 
        public string email { set; get; }
        public string homepage { set; get; }
    }
}
