

using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using TaskManagement.Application.Services.Assignee;
using TaskManagement.Application.Services.Auth;
using TaskManagement.Application.Services.Excel;
using TaskManagement.Application.Services.Pdf;
using TaskManagement.Application.Services.Priority;
using TaskManagement.Application.Services.Role;
using TaskManagement.Application.Services.Status;
using TaskManagement.Application.Services.SubTask;
using TaskManagement.Application.Services.Tasks;
using TaskManagement.Application.Services.User;
using TaskManagement.Application.Validations.Auth;
using TaskManagement.Application.Validations.Priority;
using TaskManagement.Application.Validations.Status;
using TaskManagement.Application.Validations.SubTask;
using TaskManagement.Application.Validations.Tasks;
using TaskManagement.Application.Validations.User;
using TaskManagement.Infrastructure.DependencyInjection;
using TaskManagement.Infrastructure.IDbInitializer;

using TaskManagement.API;
using TaskManagement.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));


// Configure the data protection system to persist keys in a specific directory.
builder.Services.AddDataProtection()
    
    .PersistKeysToFileSystem(new DirectoryInfo(@"/home/app/.aspnet/DataProtection-Keys"));
// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "http://172.105.109.209:5001";
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false,
            ValidIssuer = "http://172.105.109.209:5001",
            NameClaimType = "name",
            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    {
        Console.WriteLine("AdminPolicy registered.");
        policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Admin");
    });
});



//API Service
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAssigneeService, AssigneeService>();
builder.Services.AddScoped<IPriorityService, PriorityService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<ISubTaskService, SubTaskService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IExcelService, ExcelService>();
builder.Services.AddScoped<IPdfService, PdfService>();

builder.Services.AddInfrastructureService(builder.Configuration);

//Validators
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<ChangeUserRoleDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateRoleDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreatePriorityDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateStatusDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateSubTaskDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateSubTaskDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ChangeTaskStatusDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateTaskDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateTaskDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateUserDtoValidator>();





builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
var app = builder.Build();


// Apply migrations
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    
    try
    {
        logger.LogInformation("Applying database migrations...");
        var dbContext = services.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
        logger.LogInformation("Database migrations applied successfully.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while applying database migrations.");
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
 
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();


}

SeedDatabase();

app.UseCors("WebUI");
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.Logger.LogInformation("Application started");
app.Run();


void SeedDatabase()
{
    using (var scope=app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}

