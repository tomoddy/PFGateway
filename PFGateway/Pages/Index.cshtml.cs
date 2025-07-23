using CsvHelper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace PFGateway.Pages
{
    public class IndexModel(IConfiguration configuration) : PageModel
    {
        private readonly CsvReader CsvReader = new(new StreamReader(configuration["DatabasePath"]!), CultureInfo.InvariantCulture);

        public List<PortFoward> PortForwards { get; set; } = [];

        public void OnGet()
        {
            PortForwards = [.. CsvReader.GetRecords<PortFoward>()];
        }
    }
}