using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Models;
using System.Text;
using API_Progra_AvanzadaW.Services;
using API_Progra_AvanzadaW.Models;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
string SecretKey = config["settings:SecretKey"].ToString();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Session set up
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Configura el tiempo de espera de la sesión
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//ADD INTERFACES
builder.Services.AddSingleton<IUtilitariosModel, UtilitariosModel>();

//ADD JWT
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
            ValidateLifetime = true,
            LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters) =>
            {
                if (expires != null)
                {
                    return expires > DateTime.UtcNow;
                }
                return false;
            }
        };
    });

var app = builder.Build();


app.UseExceptionHandler("/api/error/error");

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSession();

app.Run();