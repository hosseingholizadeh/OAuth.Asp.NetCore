using IdentityServer4.Models;
using static IdentityServer4.IdentityServerConstants;

namespace IDP.Configurations
{
	public class Clients
	{
		public static List<Client> GetClients()
		{
			return new List<Client>
			{
				new Client{
					ClientId = "hossein-test-id",
					ClientSecrets = new List<Secret>
					{
						new Secret("132456".Sha512())
					},
					AllowedGrantTypes = GrantTypes.Implicit,
					RedirectUris = {"https://localhost:7215/sinin-oicd"},
					PostLogoutRedirectUris = { "https://localhost:7215/signout-callback-oidc" },
					AllowedScopes = new List<string>
					{
						StandardScopes.OpenId,
						StandardScopes.Email,
						StandardScopes.Phone,
						StandardScopes.Profile,

					},
					RequireConsent = true,
				},
				new Client
				{
					ClientId = "IDP.Weather.Client.Id",
					ClientSecrets = new List<Secret>{new Secret("123456".Sha512())},
					AllowedGrantTypes = GrantTypes.ClientCredentials,
					AllowedScopes = new List<string>
					{
						"WeatherScope"
					},
				},
			};
		}

		public static IEnumerable<ApiScope> GetApiScopesScopes()
		{
			return new List<ApiScope> { new ApiScope("WeatherScope") };
		}
	}
}
