namespace PFGateway
{
    public class AccessChecker(HttpClient httpClient)
    {
        private readonly HttpClient HttpClient = httpClient;

        public async Task<int> GetAccessibleAsync(PortForward portForward, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(portForward.Protocol))
                return -1;

            if (!portForward.Protocol.StartsWith("HTTP", StringComparison.OrdinalIgnoreCase))
                return 0;

            try
            {
                var url = $"{portForward.Protocol.ToLowerInvariant()}://{portForward.Address}";
                var response = await HttpClient.GetAsync(url, ct);
                return response.IsSuccessStatusCode ? 1 : -1;
            }
            catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException)
            {
                return -1;
            }
        }
    }
}