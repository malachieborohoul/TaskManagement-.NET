using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog;
using TaskManagement.Application.DependencyInjection;
using TaskManagement.Application.Services.API.Assignee;
using TaskManagement.Application.Services.API.Auth;
using TaskManagement.Application.Services.API.Excel;
using TaskManagement.Application.Services.API.Pdf;
using TaskManagement.Application.Services.API.Priority;
using TaskManagement.Application.Services.API.Role;
using TaskManagement.Application.Services.API.Status;
using TaskManagement.Application.Services.API.SubTask;
using TaskManagement.Application.Services.API.Tasks;
using TaskManagement.Application.Services.API.User;
using TaskManagement.Application.Validations.Auth;
using TaskManagement.Application.Validations.Priority;
using TaskManagement.Application.Validations.Status;
using TaskManagement.Application.Validations.SubTask;
using TaskManagement.Application.Validations.Tasks;
using TaskManagement.Application.Validations.User;
using TaskManagement.Domain.DTOs.Request.Priority;
using TaskManagement.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);



// Add builder.Services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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


//builder.Services.AddControllers().AddFluentValidation(v => v.RegisterValidatorsFromAssemblyContaining<CreatePriorityDtoValidator>());


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

app.UseCors("WebUI");
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSerilogRequestLogging();

app.MapControllers();

app.Run();