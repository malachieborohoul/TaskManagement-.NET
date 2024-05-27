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
using TaskManagement.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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



app.MapControllers();

app.Run();