using BussinessManager.ModuleApplication.SqlService;
using CoreBussiness.DTO.Users;
using ElmahCore.Mvc;
using FluentValidation.AspNetCore;
using InstaManager.ModuleApplication.AuthApplication;
using InstaManager.ModuleApplication.Binders;
using InstaManager.ModuleApplication.ElmahService;
using InstaManager.ModuleApplication.SeedApplication;
using InstaManager.ModuleApplication.ServiceApps;
using InstaManager.ModuleApplication.SwaggerApp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerService();

builder.Services.StartSqlService(builder.Configuration);
builder.Services.StartApplicationService();
builder.Services.StartElmahService(builder.Configuration);
builder.Services.AddHealthChecks();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddJwtAuthenticationService(builder.Configuration);
builder.Services.AddDataProtection();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

builder.Services.BindApplication(builder.Configuration);
builder.Services.AddResponseCompression();
var app = builder.Build();
app.StartInitialService();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x => x.HeadContent = $@"
                     <style>
                        /* Full dark theme */
                        body {{
                            background-color: #1e1e1e; /* Dark background color */
                            color: #ffffff; /* Light text color */
                        }}
                        .swagger-ui .topbar {{
                            background-color: #333; /* Dark topbar background color */
                        }}
                        /* Customize other styles as needed */
                    </style>");
}





app.UseHttpsRedirection();
//Cores
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseElmah();
app.Run();