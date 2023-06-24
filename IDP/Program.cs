using IdentityServer4.Models;
using IDP.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddIdentityServer()
	.AddDeveloperSigningCredential()
	.AddInMemoryIdentityResources(new List<IdentityResource>
	{
		new IdentityResources.OpenId(),
		new IdentityResources.Phone(),
		new IdentityResources.Email(),
		new IdentityResources.Profile(),
		new IdentityResources.Address(),
	})
	.AddTestUsers(IdentityUsers.GetUsers())
	.AddInMemoryApiResources(IdentityApiResource.GetResources())
	.AddInMemoryClients(Clients.GetClients())
	.AddInMemoryApiScopes(Clients.GetApiScopesScopes());

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
