
using IdentityServer;
using IdentityServer.Pages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities.Authentication;
using TaskManagement.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// Configuration de l'identité
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddRazorPages();
builder.Services.AddIdentityServer(options =>
    {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;
        options.EmitStaticAudienceClaim = true;
    }).AddInMemoryIdentityResources(Config.GetIdentityResources())
    .AddInMemoryApiScopes(Config.GetApiScopes())
    .AddInMemoryClients(Config.Clients())
    .AddAspNetIdentity<ApplicationUser>()
    //.AddTestUsers(TestUsers.Users)
    .AddDeveloperSigningCredential();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.MapRazorPages().RequireAuthorization();

app.MapRazorPages();

app.Run();
