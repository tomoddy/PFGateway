using CsvHelper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace PFGateway.Pages
{
    /// <summary>
    /// Index model
    /// </summary>
    /// <param name="configuration">Configuration</param>
    /// <param name="accessChecker">Access checker</param>
    public class IndexModel(IConfiguration configuration, AccessChecker accessChecker) : PageModel
    {
        // Constructor injection to the dependencies in private readonly fields
        private readonly IConfiguration Configuration = configuration;
        private readonly AccessChecker AccessChecker = accessChecker;

        /// <summary>
        /// List of port forwards
        /// </summary>
        public List<PortForward> PortForwards { get; set; } = [];

        /// <summary>
        /// On get
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Task</returns>
        public async Task OnGetAsync(CancellationToken ct)
        {
            // Create csv reader
            using StreamReader reader = new(Configuration["DatabasePath"]!);
            using CsvReader csv = new(reader, CultureInfo.InvariantCulture);

            // Read the port forwards, set pfgateway to accessible to stop recursion
            PortForwards = [.. csv.GetRecords<PortForward>()];
            PortForwards.Where(pf => pf.Name == "PFGateway").First().Accessible = 1;

            // Check the accessibility of each port forward except pfgateway
            await Task.WhenAll(PortForwards.Where(pf => pf.Name != "PFGateway").Select(async portForward =>
            {
                portForward.Accessible = await AccessChecker.GetAccessibleAsync(portForward, ct);
            }));
        }
    }
}