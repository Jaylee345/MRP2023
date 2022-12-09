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
}
