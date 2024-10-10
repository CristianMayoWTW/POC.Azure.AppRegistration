using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Identity.Client;

namespace POC.Azure.AppRegistration.Services
{
	// Create a GraphService class to encapsulate the Graph SDK logic
	public class GraphService
	{
		private readonly GraphServiceClient _graphClient;
		private static readonly string[] scopes = ["https://graph.microsoft.com/.default"];

		public GraphService(IConfiguration configuration)
		{
			var clientId = configuration["AzureAd:ClientId"];
			var tenantId = configuration["AzureAd:TenantId"];
			var clientSecret = configuration["AzureAd:ClientSecret"];

			var tokenCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);

			_graphClient = new GraphServiceClient(tokenCredential, scopes);
		}

		/// <summary>
		/// Get the details of a user by user principal name
		/// </summary>
		/// <param name="userPrincipalName">User's username in a form of email</param>
		/// <returns></returns>
		public async Task<User?> GetUserDetailsAsync(string userPrincipalName)
		{
			return await _graphClient.Users[userPrincipalName].GetAsync();
		}

		/// <summary>
		/// Get the list of users by user principal names
		/// </summary>
		/// <param name="userPrincipalNames">List of user's username in a form of email</param>
		/// <returns></returns>
		public async Task<List<User>> GetUserListAsync(List<string> userPrincipalNames)
		{
			var tasks = userPrincipalNames.Select(upn => _graphClient.Users[upn].GetAsync()).ToArray();
			var users = await Task.WhenAll(tasks);

			return [.. users];
		}

	}
}
