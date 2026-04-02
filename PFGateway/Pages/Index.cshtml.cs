using CsvHelper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace PFGateway.Pages
{
    public class IndexModel(IConfiguration configuration, AccessChecker accessChecker) : PageModel
    {
        private readonly IConfiguration Configuration = configuration;
        private readonly AccessChecker AccessChecker = accessChecker;

        public List<PortForward> PortForwards { get; set; } = [];

        public async Task OnGetAsync(CancellationToken ct)
        {
            using var reader = new StreamReader(Configuration["DatabasePath"]!);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            PortForwards = [.. csv.GetRecords<PortForward>()];
            PortForwards.Where(pf => pf.Name == "PFGateway").First().Accessible = 1;

            await Task.WhenAll(PortForwards.Where(pf => pf.Name != "PFGateway").Select(async portForward =>
            {
                portForward.Accessible = await AccessChecker.GetAccessibleAsync(portForward, ct);
            }));
        }
    }
}