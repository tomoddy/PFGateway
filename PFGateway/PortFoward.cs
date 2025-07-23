using CsvHelper.Configuration.Attributes;

namespace PFGateway
{
    public class PortFoward
    {
        public string? Name { get; set; }

        public string? Type { get; set; }

        public string? Device { get; set; }

        public string? LocalIP { get; set; }

        public int LocalPort { get; set; }

        public string? WANIP { get; set; }

        public int WANPort { get; set; }

        public string? Protocol { get; set; }

        [Ignore]
        public string? Address
        {
            get
            {
                if (Protocol is not null && (Protocol.Equals("HTTP") || Protocol.Equals("HTTPS")))
                    return $"{Protocol.ToLower()}://tzer0m.duckdns.org:{WANPort}";
                else
                    return null;
            }
        }
    }
}