using System.IO;
using System.Text.Json;

namespace CustomDNS.Data.DataMethod
{
    public class DataPattern
    {
        static string  filePath = "DefaultDNS.json";

        public static List<Config_List> LoadDNSList()
        {
            if (!File.Exists(filePath))
                return DefualtDNS();

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Config_List>>(json) ?? new List<Config_List>();
        }
        public static List<Config_List> DefualtDNS()
        {
            List<Config_List> dnsList = new List<Config_List>
            {
                new Config_List { Id = 1, DNSName = "Cloudflare", DNSMain = "1.1.1.1", DNSSec = "1.0.0.1" },
                new Config_List { Id = 2, DNSName = "Google", DNSMain = "8.8.8.8", DNSSec = "8.8.4.4" },
                new Config_List { Id = 3, DNSName = "Quad9", DNSMain = "9.9.9.9", DNSSec = "149.112.112.112" },
                new Config_List { Id = 4, DNSName = "OpenDNS", DNSMain = "208.67.222.222", DNSSec = "208.67.220.220" },
                new Config_List { Id = 5, DNSName = "NextDNS", DNSMain = "45.90.28.0", DNSSec = "45.90.30.0" },
                new Config_List { Id = 6, DNSName = "CleanBrowsing", DNSMain = "185.228.168.168", DNSSec = "185.228.169.168" },
                new Config_List { Id = 7, DNSName = "Comodo", DNSMain = "8.26.56.26", DNSSec = "8.20.247.20" },
                new Config_List { Id = 8, DNSName = "Yandex DNS", DNSMain = "77.88.8.88", DNSSec = "77.88.8.2" },
                new Config_List { Id = 9, DNSName = "Neustar", DNSMain = "156.154.70.1", DNSSec = "156.154.71.1" },
                new Config_List { Id = 10, DNSName = "Level3", DNSMain = "209.244.0.3", DNSSec = "209.244.0.4" },
             };
            return dnsList;
        }
        public static event Action isDNSListChanged;
        public static void SaveDNSList(List<Config_List> dnsList)
        {
            var json = JsonSerializer.Serialize(dnsList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
            isDNSListChanged?.Invoke();

        }

    }
}
    public class Config_List
    {
        public int Id { get; set; } 
        public string DNSName { get; set; }
        public string DNSMain { get; set; }
        public string DNSSec { get; set; }
}