using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Net;
using System.Net.Sockets;

using System.Threading;
using System.Windows.Forms;
using MRPSystem.Model;

namespace MRPSystem
{
    public class Common
    {
        public static string logPath = Application.StartupPath + "\\Logs";
        //public static string connectStr = ConfigurationManager.ConnectionStrings["WT"].ConnectionString;
        static string[] stDateCode = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V" };
        public static string Left(string param, int length)
        {
            string result = param.Substring(0, length);
            return result;
        }

        //public static List<Taxtype> GetTaxtype()
        //{
        //    List<Taxtype> taxtypelist = new List<Taxtype>();
        //    taxtypelist.Add(new Taxtype(){Text="1",Value= "外加稅" });
        //    taxtypelist.Add(new Taxtype() { Text = "2", Value = "內含稅" });
        //    taxtypelist.Add(new Taxtype() { Text = "3", Value = "免稅" });
        //    taxtypelist.Add(new Taxtype() { Text = "4", Value = "零稅率" });
        //    return taxtypelist;

        //}

        public static string GetPlant(string wkno)
        {
            string kv = "KV";
            if (Left(wkno, 1) == "C")
                kv = "KV1";
            else if (Left(wkno, 1) == "V")
                kv = "KV";
            else if (Left(wkno, 1) == "Y" || Left(wkno, 1) == "Q")
                kv = "KV2";
            else if (Left(wkno, 1) == "F")
                kv = "KF";
            return kv;
        }
        public static int getdatecode(string stNumber)
        {

            string[] stDateCode = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V" };
            int index = Array.FindIndex(stDateCode, val => val.Equals(stNumber)); //findIndex(stDateCode,stNumber);

            return index + 1;
        }
        public static string Right(string param, int length)
        {
            string result = param.Substring(param.Length - length, length);
            return result;
        }

        public static string GetHostIPAddress()
        {
            //List<string> lstIPAddress = new List<string>();
            //lstIPAddress.Add("192.168.66.100");
            //return lstIPAddress ;
            //Writetxt("58");
            //string hostName = Dns.GetHostName();   //取得本機名稱
            //取得所有IP，包含IPV4和IPV6
            try
            {
                IPHostEntry IpEntry = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in IpEntry.AddressList)
                {
                    Writetxt(ip.ToString());
                }
                foreach (IPAddress ip in IpEntry.AddressList)
                {
                    if (ip.ToString().IndexOf("192") >= 0)
                        return ip.ToString();
                }
            }
            catch (Exception ex)
            {
                Writetxt("ip", ex);
            }

            return "0.0.0.0"; // result: 192.168.1.17 ......
        }
        public static bool Datediff(string D1, string D2)
        {
            DateTime Date1, Date2;
            DateTime.TryParse(D1, out Date1);
            DateTime.TryParse(D2, out Date2);
            if (Date1.Year == Date2.Year && Date1.Month == Date2.Month && Date1.Day == Date2.Day)
                return true;
            else
                return false;
        }
        private string getdate(string stDate)
        {

            string[] stArDate = stDate.Split('/');
            string stYear = stArDate[0].Substring(2);
            string stMonth = getDateCode(stArDate[1]);
            string stDay = getDateCode(stArDate[2]);
            string stReDate = stYear + stMonth + stDay;
            return stReDate;
        }
        public static string getDate(string lotdate)
        {
            string stDate = lotdate; //dtimeLabelDate.Value.ToString("yyyy/MM/dd");
            string[] stArDate = stDate.Split('/');
            string stYear = stArDate[0].Substring(2);
            string stMonth = getDateCode(stArDate[1]);
            string stDay = getDateCode(stArDate[2]);
            string stReDate = stYear + stMonth + stDay;

            return stReDate;
        }
        private static string getDateCode(string stNumber)
        {
            int iNumber = int.Parse(stNumber) - 1;
            return stDateCode[iNumber];
        }
        public static string ToJson<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);

        }
        public static void Writetxt<T>(T obj)
        {
            Writetxt(ToJson<T>(obj));
        }


        /// <summary>
        /// 反序列化 JSON 為物件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonDeserializeEntity<T>(string json)
        {
            T result = JsonConvert.DeserializeObject<T>(json);
            return result;

        }

        public static List<T> JsonDeserialize2Entities<T>(string json)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(List<T>));
                List<T> result = (List<T>)js.ReadObject(ms);
                return result;
            }
        }

        public static string ToJson(object obj)
        {
            if (obj == null) return null;

            JsonSerializerSettings setting = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "MM\\/dd\\/yyyy HH:mm:ss";
            setting.Converters.Add(timeConverter);

            return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, setting);
        }



        public static void Writetxt(string logMsg, Exception exp)
        {

            if (!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);
            DateTime dt = DateTime.Now;
            string logFileName = string.Format("Log-{0}.txt", dt.ToString("yyyy-MM-ddHH"));
            using (StreamWriter sw = File.AppendText(logPath + "\\" + logFileName))
            {
                //WriteLine為換行 
                sw.Write("<---" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "-->");
                sw.WriteLine(logMsg + "\r\n");
                sw.WriteLine(string.Format("Message:{0}\r\n StackTrace:{1} \r\n Source:{2} \r\n ", exp.Message, exp.StackTrace, exp.Source));
                sw.WriteLine("");
            }


        }
        public static T LoadTSCinfo<T>(string fname)
        {
            //string path = @"ini\TSC.json";
            //T resp = new T();
            // This text is added only once to the file.
            if (!File.Exists(fname))
            {
                var resp = JsonConvert.DeserializeObject<T>("{\"code\": \"I\",\"place\":\"\"}");
                return resp;// "The file is not Exists!";
            }


            // This text is always added, making the file longer over time
            // if it is not deleted.
            // Open the file to read from.
            string readText = File.ReadAllText(fname);
            var PirintInfo = JsonConvert.DeserializeObject<T>(readText);
            return PirintInfo;
        }
        public static List<T> TextToJson<T>(string json)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(List<T>));
                List<T> result = (List<T>)js.ReadObject(ms);
                return result;
            }

        }
        public static List<T> LoadJson<T>(string fname)
        {
            //string path = @"ini\TSC.json";

            // This text is added only once to the file.
            if (!File.Exists(fname))
            {
                // Create a file to write to.
                return null;// "The file is not Exists!";
            }


            // This text is always added, making the file longer over time
            // if it is not deleted.
            // Open the file to read from.
            string json = File.ReadAllText(fname);
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(List<T>));
                List<T> result = (List<T>)js.ReadObject(ms);
                return result;
            }
        }

        public static void Write2Json<T>(T obj, string fname)
        {
            //string logPath = System.Windows.Forms.Application.StartupPath;
            string path = Application.StartupPath + "\\ini";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string logMsg = ToJson<T>(obj);
            string logFileName = string.Format("{0}", fname);

            using (StreamWriter sw = new StreamWriter(path + "\\" + logFileName))
            {
                //WriteLine為換行 
                sw.WriteLine(logMsg);

            }


        }

        public static void Writetxt(string logMsg)
        {
            //string logPath = System.Windows.Forms.Application.StartupPath;

            if (!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);
            DateTime dt = DateTime.Now;
            string logFileName = string.Format("Log-{0}.txt", dt.ToString("yyyy-MM-ddHH"));

            using (StreamWriter sw = File.AppendText(logPath + "\\" + logFileName))
            {
                //WriteLine為換行 
                sw.Write("<---" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "-->");
                sw.WriteLine(logMsg + "\r\n");
                sw.WriteLine("");
            }
            //DAOMSSQL.InsertLogs("Log", logMsg);

        }

        public static bool TestConn(string ip, int port)
        {
            try
            {
                using (TcpClient tc = new TcpClient())
                {
                    IAsyncResult result = tc.BeginConnect(ip, port, null, null);
                    DateTime start = DateTime.Now;

                    do
                    {
                        SpinWait.SpinUntil(() => { return false; }, 100);
                        if (result.IsCompleted) break;
                    }
                    while (DateTime.Now.Subtract(start).TotalSeconds < 0.3);

                    if (result.IsCompleted)
                    {
                        tc.EndConnect(result);
                        return true;
                    }

                    tc.Close();

                    if (!result.IsCompleted)
                    {
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                Common.Writetxt("Connection Error", ex);
                throw;
            }

            return false;
        }
    }
}
