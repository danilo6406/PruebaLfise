using EasySales.Server.Models;
using EasySales.Server.Models.Interfaces;
using EasySales.Server.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using EasySales.Server.Data;
using EasySales.Server.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using EasySales.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

////Scope Services
#region Scope Services
builder.Services.AddScoped<ICategoriaProductosRepository, CategoriaProductosRepository>();
builder.Services.AddScoped<ISubCategoriaProductosRepository, SubCategoriaProductosRepository>();
builder.Services.AddScoped<IProductosRepository, ProductosRepository>();
builder.Services.AddScoped<ITipoIdentificacionRepository, TipoIdentificacionRepository>();
builder.Services.AddScoped<IParametrosSistemaRepository, ParametroSistemaRepository>();
builder.Services.AddScoped<IVentasRepository, VentasRepository>();
builder.Services.AddScoped<IClientesRepository, ClientesRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IRolesRepository, RolesRepository>();
builder.Services.AddTransient<IEmailSender, EmailSenderRepository>();
builder.Services.AddTransient<IEncryptationRepository, EncryptationRepository>();
builder.Services.AddTransient<IReservacionesRepository, ReservacionesRepository>();
builder.Services.AddTransient<IBodegasRepository, BodegasRepository>();
builder.Services.AddTransient<IFacturasRepository, FacturasRepository>();
#endregion


////mySQL Service configuration
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var cxString = builder.Configuration.GetConnectionString("DBConnection");
    options.UseMySql(cxString,ServerVersion.AutoDetect(cxString));
});

////Identifiacion de usuarios y roles
builder.Services.AddIdentity<EasySalesServerUser, EasySalesServerRoles>()
               .AddEntityFrameworkStores<AppDbContext>().AddErrorDescriber<SpanishIdentityErrorDescriber>().AddDefaultTokenProviders();

////Token JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["JwtIssuer"],
                        ValidAudience = builder.Configuration["JwtAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSecurityKey"]))
                    };
                });

////AutoMapper
builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseBlazorFrameworkFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
