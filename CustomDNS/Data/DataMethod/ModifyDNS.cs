using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomDNS.Data.DataMethod
{
    class ModifyDNS
    {
        public static void AddDNS(string DNSName,string DNS1 , string DNS2)
        {

            var dnsList = DataPattern.LoadDNSList();

            int nextId = dnsList.Any() ? dnsList.Max(x => x.Id) + 1 : 1;
            dnsList.Add(new Config_List
            {
                DNSName = DNSName,
                DNSMain = DNS1,
                DNSSec = DNS2,
                Id = nextId

            });

            //save
            DataPattern.SaveDNSList(dnsList);
        }
        public static void RemoveDNS(int DNSID )
        {
            var dnsList = DataPattern.LoadDNSList();


            int idToDelete = DNSID; // آی‌دی مورد نظر
            try
            {
                var itemToRemove = dnsList.FirstOrDefault(x => x.Id == idToDelete);
                if (itemToRemove != null)
                {
                    dnsList.Remove(itemToRemove);
                }

            }
            catch (Exception)
            {

                throw;
            }

            // ذخیره نهایی
            DataPattern.SaveDNSList(dnsList);
        }
    }
}
