using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using VShop.DiscountApi.Context;
using VShop.DiscountApi.Repositories;
using VShop.DiscountApi.Repositories.Interface;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region Token JWT no Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "VShop.DiscountApi", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"'Bearer' [space] seu token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In= ParameterLocation.Header
                        },
                        new List<string> ()
                    }
                });
});
#endregion

#region String de Conexão - MySQL
var mySqlConnection = builder
    .Configuration
    .GetConnectionString("DefaultConnection");

builder.Services
        .AddDbContext<AppDbContext>(options =>
        options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));
#endregion

#region CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});
#endregion

#region Autenticação JWT
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority =
        builder.Configuration["VShop.IdentityServer:ApplicationUrl"];

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "vshop");
    });
});
#endregion

#region AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion

builder.Services.AddScoped<ICouponRepository, CouponRepository>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
