using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SteamRoxa.Data;

var builder = WebApplication.CreateBuilder(args);

//Configurar a conex�o com bancos de dadso

builder.Services.AddDbContext<SteamRoxaContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Configurar o CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("AloowAll", builder =>
    { builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin(); });
});



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

// Adionar o Swagger com JWT Bearer
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
            In = ParameterLocation.Header,

        },
        new List<string>()
        }
    });
});

// Servi�o de EndPoints do Identity Framework
builder.Services.AddIdentityApiEndpoints<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
})
    .AddEntityFrameworkStores<SteamRoxaContext>()
    .AddDefaultTokenProviders(); // Adiocionando o provedor de tokens padr�o

// Add servicos de autentica��o e autoriza��o

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();



var app = builder.Build();

// SWAGGER EM AMBIENTE DE DESENVOLVIMENTO

app.UseSwagger();
app.UseSwaggerUI();

//MAPEAR OS ENDPOINTS PADRAO DO IDENTITY
app.MapGroup("/User").MapIdentityApi<IdentityUser>();
app.MapGroup("/Roles").MapIdentityApi<IdentityRole>();

app.UseHttpsRedirection();
//permitir a autentifica��o e autoriza��o de qualquer origem
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AlloAll");

app.MapControllers();

app.Run();
