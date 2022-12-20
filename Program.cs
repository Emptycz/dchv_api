using System.Security.Claims;
using dchv_api.DataRepositories;
using dchv_api.DataRepositories.Implementations;
using dchv_api.Database;
using dchv_api.Middlewares;
using dchv_api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

internal class Program
{
  private static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    // Add repositories to the DI container
    builder.Services.AddScoped<ILoginRepository, LoginRepository>();
    builder.Services.AddScoped<IPersonRepository, PersonRepository>();
    builder.Services.AddScoped<IRecordRepository, RecordRepository>();
    builder.Services.AddScoped<IRoleRepository, RoleRepository>();
    builder.Services.AddScoped<IPersonGroupRepository, PersonGroupRepository>();
    builder.Services.AddScoped<IRecordDataRepository, RecordDataRepository>();

    builder.Services.AddScoped<JwtManager>();
    builder.Services.AddScoped<AuthManager>();

    builder.Services.AddAutoMapper(typeof(DTOMappingProfile));

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
        IssuerSigningKey = new SymmetricSecurityKey(
          System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt").GetValue<string>("Key"))
      )
      };
    });

    builder.Services.AddHttpContextAccessor();
    // Add specific DbContext as a service
    builder.Services.AddTransient<BaseDbContext>(serviceProvider =>
    {
        var context = serviceProvider.GetService<IHttpContextAccessor>();
        var options = new DbContextOptionsBuilder<BaseDbContext>();
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        if (context?.HttpContext?.User?.Identity?.IsAuthenticated == true)
        {
            // Try to find PrimarySID from JWT claims (PersonId)
            string? jwtPersonId = context?.HttpContext?.User.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
            string? roleName = context?.HttpContext?.User.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            uint personId;
            if(uint.TryParse(jwtPersonId, out personId) && roleName != "Admin")
            {
                return new PersonDbContext(options.Options, personId);
            }
        }

        return new BaseDbContext(options.Options);
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

      if (args.Length != 0) app.Logger.LogDebug("arg[0] {0}", args[0]);
      DatabaseSeedManager.MigrateUp(app);
      if (args.Contains("--sample")) DatabaseSeedManager.MigrateSampleSeed(app);
    }

    app.UseHttpsRedirection();

    app.UseCors(x => x.AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins("http://localhost:3000", ""));

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
  }
}