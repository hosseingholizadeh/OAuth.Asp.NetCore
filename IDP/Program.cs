using IdentityServer4.Models;
using System.Security.Claims;
using static IdentityServer4.IdentityServerConstants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddIdentityServer()
	.AddDeveloperSigningCredential()
	.AddTestUsers(new List<IdentityServer4.Test.TestUser>
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
	})
	.AddInMemoryIdentityResources(new List<IdentityResource>
	{
		new IdentityResources.OpenId(),
		new IdentityResources.Phone(),
		new IdentityResources.Email(),
		new IdentityResources.Profile(),
		new IdentityResources.Address(),
	})
	.AddInMemoryApiResources(new List<ApiResource>
	{

	})
	.AddInMemoryClients(new List<Client>
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
		}
	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
