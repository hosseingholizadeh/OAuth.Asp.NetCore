using IdentityServer4.Models;
using IDP.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("IdentityDB");
var migrationAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

builder.Services.AddIdentityServer()
	.AddDeveloperSigningCredential()
	.AddConfigurationStore(options =>
	{
		options.ConfigureDbContext = c => c.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationAssembly));
	})
	// saves tokens - has espiration
	.AddOperationalStore(options =>
	{
		options.ConfigureDbContext = c => c.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationAssembly));
		options.EnableTokenCleanup = true;
	})
	// we can read these users from any database 
	.AddTestUsers(IdentityUsers.GetUsers())
	/*.AddInMemoryIdentityResources(new List<IdentityResource>
	{
		new IdentityResources.OpenId(),
		new IdentityResources.Phone(),
		new IdentityResources.Email(),
		new IdentityResources.Profile(),
		new IdentityResources.Address(),
	})
	.AddInMemoryApiResources(IdentityApiResource.GetResources())
	.AddInMemoryClients(Clients.GetClients())
	.AddInMemoryApiScopes(Clients.GetApiScopesScopes())*/;

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
