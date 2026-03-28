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
    }
}