using System.Security.Claims;

namespace IDP.Configurations
{
	public class IdentityUsers
	{
		public static List<IdentityServer4.Test.TestUser> GetUsers()
		{
			return new List<IdentityServer4.Test.TestUser>
			{
				 new IdentityServer4.Test.TestUser
				 {
					 Username = "test",
					 IsActive = true,
					 Password = "1234",
					 SubjectId = Guid.NewGuid().ToString(),
					 ProviderName = "test",
					 ProviderSubjectId = Guid.NewGuid().ToString(),
					 Claims = new List<Claim>
					 {
						 new Claim(ClaimTypes.Name, "test"),
						 new Claim(ClaimTypes.Email, "test@gmail.com"),
						 new Claim(ClaimTypes.MobilePhone, "09015657617"),
						 new Claim("fullname", "hossein gholizadeh"),
					 }
				 }
			};
		}
	}
}
