namespace PFGateway
{
    public class PortForward
    {
        public string? Name { get; set; }

        public string? Address { get; set; }

        public string? Type { get; set; }

        public string? Device { get; set; }

        public string? LocalIP { get; set; }

        public int LocalPort { get; set; }

        public int WANPort { get; set; }

        public string? Protocol { get; set; }

        public int Accessible
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Protocol))
                    return -1;
                else if (!Protocol.StartsWith("HTTP"))
                    return 0;
                else
                    return new HttpClient().GetAsync($"{Protocol.ToLowerInvariant()}://{Address}").Result.IsSuccessStatusCode ? 1 : -1;
            }
        }
    }
}