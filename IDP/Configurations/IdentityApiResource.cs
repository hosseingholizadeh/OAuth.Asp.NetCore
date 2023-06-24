using IdentityServer4.Models;

namespace IDP.Configurations
{
	public class IdentityApiResource
	{
		public static List<ApiResource> GetResources()
		{
			return new List<ApiResource> {
				new ApiResource("IDP.Weather.Client")
			};
		}
	}
}
