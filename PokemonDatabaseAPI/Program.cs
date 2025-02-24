
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using PokemonDatabaseAPI.AiPlugins;
using PokemonDatabaseAPI.Data;
using PokemonDatabaseAPI.Interfaces;
using PokemonDatabaseAPI.Model;
using PokemonDatabaseAPI.Service;
using Scalar.AspNetCore;

namespace PokemonDatabaseAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           
 
            // Add services to the container.
            builder.Services.AddDbContext<PokemonDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IPokemonDbContext, PokemonDbContext>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IAiAgentService, AiAgentService>();

            //authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration.GetValue<string>("AppSettings:Issuer"),
                        ValidAudience = builder.Configuration.GetValue<string>("AppSettings:Audience"),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("AppSettings:Token")!))
                    };
                });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            //ai agent
            var aiBuilder = Kernel.CreateBuilder();
            aiBuilder.AddOpenAIChatCompletion("gpt-4o",
                builder.Configuration.GetValue<string>("AIConnection:ApiKey")!);

            builder.Services.AddScoped<PokemonPlugin>();
            builder.Services.AddScoped<PokemonAbilityPlugin>();
            builder.Services.AddScoped<PokemonType1Plugin>();
            builder.Services.AddScoped<PokemonType2Plugin>();

            Kernel kernel = aiBuilder.Build();
            var pokemonPlugin = builder.Services.BuildServiceProvider().GetRequiredService<PokemonPlugin>();
            kernel.Plugins.AddFromObject(pokemonPlugin);
            var pokemonAbilityPlugin = builder.Services.BuildServiceProvider().GetRequiredService<PokemonAbilityPlugin>();
            kernel.Plugins.AddFromObject(pokemonAbilityPlugin);
            var pokemonType1Plugin = builder.Services.BuildServiceProvider().GetRequiredService<PokemonType1Plugin>();
            kernel.Plugins.AddFromObject(pokemonType1Plugin);
            var pokemonType2Plugin = builder.Services.BuildServiceProvider().GetRequiredService<PokemonType2Plugin>();
            kernel.Plugins.AddFromObject(pokemonType2Plugin);



            builder.Services.AddSingleton(kernel);


            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();

            }

            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }

    }
}
