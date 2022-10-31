using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using System.Reflection.PortableExecutable;
using System.Security.AccessControl;
using System.IO.Enumeration;
using dchv_api.DataRepositories;
using dchv_api.DataRepositories.Implementations;
using dchv_api.Database;
using dchv_api.Middlewares;
using dchv_api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using dchv_api.Models;
using dchv_api.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add repositories to the container
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IRecordRepository, RecordRepository>();
builder.Services.AddScoped<ITableColumnRepository, TableColumnRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IPersonGroupRepository, PersonGroupRepository>();

builder.Services.AddScoped<JwtManager>();
builder.Services.AddScoped<AuthManager>();

builder.Services.AddAutoMapper(typeof(DTOMappingProfile));

// Add services to the container.
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidIssuer = builder.Configuration.GetSection("Jwt").GetValue<string>("Issuer"),
        ValidAudience = builder.Configuration.GetSection("Jwt").GetValue<string>("Audience"),
        IssuerSigningKey= new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt").GetValue<string>("Key"))
        )
    };
});

builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:3000"));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Custom middlewares
DatabaseStatusMiddleware.CreateDbIfNotExists(app);

app.Run();
