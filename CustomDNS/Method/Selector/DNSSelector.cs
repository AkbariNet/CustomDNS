using CustomDNS.Extra.ConfigControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CustomDNS.Method.Selector
{
    class DNSSelector
    {
        public static string DNSNAMESELECTED;
        public static string DNSSELECTED01;
        public static string DNSSELECTED02;
        public static int DNSIDSELECTED;
        public static ConfigController PreCC;
        public static bool ISSELECTOROPEN = true;


        public static event Action<ConfigController> isSelectorChangedEvent;
        public static void Select(ConfigController CC)
        {
            if (ISSELECTOROPEN)
            {

                //--- GET DNS PARAMETERS ---//
                DNSSELECTED01 = CC.DNSValue1;
                DNSSELECTED02 = CC.DNSValue2;
                DNSIDSELECTED = CC.ID;
                DNSNAMESELECTED = CC.DNSName;
                //--- GET DNS PARAMETERS ---//


                //--- REMOVE SELECT PREVIOUS DNS ---//
                if (PreCC != null)
                    PreCC.IsEnabled = false;

                PreCC = CC;
                //--- REMOVE SELECT PREVIOUS DNS ---//


                //--- ENABLE DNS SELECTOR ---//
                CC.IsEnabled = true;
                //--- ENABLE DNS SELECTOR ---//

                //---EVENT LOAD---//
                isSelectorChangedEvent?.Invoke(CC);
            }
        }

    }
}
