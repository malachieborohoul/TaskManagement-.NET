

using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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


var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:5001";
        options.TokenValidationParameters.ValidateAudience = false;
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("WebUI");
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.Logger.LogInformation("Application started");
app.Run();

