using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectCV.Server.DB;
using ProjectCV.Server.IServices.IAccountservices;
using ProjectCV.Server.IServices.INoteServices;
using ProjectCV.Server.IServices.ITypeServices;
using ProjectCV.Server.Services.AccountServices;
using ProjectCV.Server.Services.NoteServices;
using ProjectCV.Server.Services.TypeServices;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DBSetting>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("Context")));
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IJwtServices, JwtServices>();
builder.Services.AddScoped<ILoginServicescs, LoginServicescs>();
builder.Services.AddScoped<IRegisterServices, RegisterServices>();
builder.Services.AddScoped<IResetPasswordServices, ResetPasswordServices>();
builder.Services.AddScoped<ITypeServices, TypeServices>();
builder.Services.AddScoped<INoteServices,NoteServices>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = builder.Configuration["Jwt:Issuer"],
           ValidAudience = builder.Configuration["Jwt:Issuer"],
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
       };
       options.Events = new JwtBearerEvents
       {
           OnAuthenticationFailed = context =>
           {
               if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
               {
                   context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
               }
               return Task.CompletedTask;
           }
       };
   });
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
