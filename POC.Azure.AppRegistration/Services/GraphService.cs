using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Identity.Client;

namespace POC.Azure.AppRegistration.Services
{
	// Create a GraphService class to encapsulate the Graph SDK logic
	public class GraphService
	{
		private readonly IConfidentialClientApplication _clientApp;
		private readonly GraphServiceClient _graphClient;

		public GraphService(IConfiguration configuration)
		{
			var clientId = configuration["AzureAd:ClientId"];
			var tenantId = configuration["AzureAd:TenantId"];
			var clientSecret = configuration["AzureAd:ClientSecret"];

			_clientApp = ConfidentialClientApplicationBuilder.Create(clientId)
				.WithClientSecret(clientSecret)
				.WithAuthority(new Uri($"https://login.microsoftonline.com/{tenantId}"))
				.Build();

			var tokenCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);

			_graphClient = new GraphServiceClient(tokenCredential);
		}

		public async Task<User?> GetUserDetailsAsync(string userPrincipalName)
		{
			return await _graphClient.Users[userPrincipalName].GetAsync();
		}

		public async Task<List<User>> GetUserListAsync(List<string> userPrincipalNames)
		{
			var tasks = userPrincipalNames.Select(upn => _graphClient.Users[upn].GetAsync()).ToArray();
			var users = await Task.WhenAll(tasks);

			return [.. users];
		}

	}
}
