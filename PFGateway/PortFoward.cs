using CsvHelper.Configuration.Attributes;

namespace PFGateway
{
    /// <summary>
    /// Port forward
    /// </summary>
    public class PortForward
    {
        /// <summary>
        /// Name of the port forward, e.g. PFGateway
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Address of the port forward, e.g. pfgateway.tzer0m.co.uk
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Type of port forward, e.g. Website
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Hosting device, e.g. TacMini
        /// </summary>
        public string? Device { get; set; }

        /// <summary>
        /// Local IP of the hosting device
        /// </summary>
        public string? LocalIP { get; set; }

        /// <summary>
        /// Local port of the hosting device
        /// </summary>
        public int LocalPort { get; set; }

        /// <summary>
        /// WAN port of the port forward
        /// </summary>
        public int WANPort { get; set; }

        /// <summary>
        /// Protocol
        /// </summary>
        public string? Protocol { get; set; }

        /// <summary>
        /// Accessible flag, 1 if accessible, -1 if not, 0 if invalid
        /// </summary>
        [Ignore]
        public int Accessible { get; set; }
    }
}