using EcommerceBackend.Helpers;
using EcommerceBackend.Mapper;
using EcommerceBackend.Mapper.Abstract;
using EcommerceBackend.Repository;
using EcommerceBackend.Repository.Abstract;
using EcommerceBackend.Resource;
using EcommerceBackend.Resource.Abstract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<TokenHelper>();

//Repository
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Resource
builder.Services.AddScoped<ILoginResource,LoginResource>();
builder.Services.AddScoped<IUserResource, UserResource>();

//Mapper
builder.Services.AddScoped<IUserMapper, UserMapper>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {

        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"]))
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowOrigin");
app.MapControllers();

app.Run();
