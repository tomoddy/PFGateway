namespace PFGateway
{
    /// <summary>
    /// Access checker
    /// </summary>
    /// <param name="httpClient">Http client</param>
    public class AccessChecker(HttpClient httpClient)
    {
        // Http client
        private readonly HttpClient HttpClient = httpClient;

        /// <summary>
        /// Check if the port forward is accessible
        /// </summary>
        /// <param name="portForward">Port forward</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>1 if accessible, -1 if not, 0 if invalid</returns>
        public async Task<int> GetAccessibleAsync(PortForward portForward, CancellationToken ct = default)
        {
            // Return fail if protocl is empty
            if (string.IsNullOrWhiteSpace(portForward.Protocol))
                return -1;

            // Return invalid if non-http protocol
            if (!portForward.Protocol.StartsWith("HTTP", StringComparison.OrdinalIgnoreCase))
                return 0;

            try
            {
                // Send request
                string url = $"{portForward.Protocol.ToLowerInvariant()}://{portForward.Address}";
                HttpResponseMessage response = await HttpClient.GetAsync(url, ct);

                // Return unauthorized if status code is 401, which means the port forward is accessible but requires authentication
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return 1;

                // Return success if status code is 2xx, otherwise return fail
                return response.IsSuccessStatusCode ? 1 : -1;
            }
            catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException)
            {
                return -1;
            }
        }
    }
}