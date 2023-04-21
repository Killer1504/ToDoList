using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListLib.Helper
{
    public static class CommonHelper
    {
        public static string GetTimeCurrentStr(string format = "HH_mm_ss_ffff")
        {
            try
            {
                var str = DateTime.Now.ToString(format, CultureInfo.InvariantCulture.DateTimeFormat);
                return str;
            }
            catch (Exception)
            {
                // when input format incorrect
                return DateTime.Now.ToString("HH_mm_ss_ffff", CultureInfo.InvariantCulture.DateTimeFormat);
            }
            
        }

        private static string ReFormatStr(string str, string sepecialChar = "-", int lengthGroup = 4)
        {
            var length = str.Length;
            if (length % lengthGroup == 0)
            {
                var list = new List<string>();
                for (int i = 0; i < length; i += lengthGroup)
                {
                    var sub = str.Substring(i, lengthGroup);
                    list.Add(sub);
                }
                return string.Join(sepecialChar, list.ToArray());
            }
            else
            {
                return str;
            }
        }

        public static string GetIDString()
        {
            var time = DateTime.Now.ToString(CultureInfo.InvariantCulture.DateTimeFormat);
            var rand = Guid.NewGuid() + time;
            var hash = ToDoListLib.Helper.EncryptHelper.GetHashSHA256(rand);
            var id = ReFormatStr(hash);
            return id;
        }
        /// <summary>
        /// get computer id from physical address network
        /// </summary>
        /// <returns></returns>
        public static string GetComputerID()
        {
            string physical_address = "F446379EAEBC";
            try
            {
                var mac_add = (from nic in NetworkInterface.GetAllNetworkInterfaces()
                               where nic.OperationalStatus == OperationalStatus.Up
                               select nic.GetPhysicalAddress().ToString()).FirstOrDefault();
                if (mac_add != null)
                {
                    physical_address = mac_add;
                }
            }
            catch (Exception)
            {

            }

            return ReFormatStr(Helper.EncryptHelper.GetHashSHA256(physical_address), "-", 4);
        }
    }
}
