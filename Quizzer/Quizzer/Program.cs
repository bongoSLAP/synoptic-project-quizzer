using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Quizzer.Data;
using Quizzer.Handlers;
using Quizzer.Interfaces;
using Quizzer.Repositories;
using Quizzer.Wrappers;
using Scrypt;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ILoginHandler, LoginHandler>();
builder.Services.AddTransient<IQuizHandler, QuizHandler>();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IQuizRepository, QuizRepository>();

builder.Services.AddSingleton<ScryptEncoder, ScryptEncoder>();
builder.Services.AddSingleton<IScryptEncoder, ScryptEncoderWrapper>();

builder.Services.AddControllers();         
    /*.AddJsonOptions(jo =>
    jo.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);*/

var connectionString = builder.Configuration.GetConnectionString(name: "QuizzerContext");

builder.Services.AddDbContext<QuizzerContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

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
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        }
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
