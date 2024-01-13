
using Microsoft.EntityFrameworkCore;
using VpnetworkAPI.DbContex;
using VpnetworkAPI.Repository;
//using BackgroundServiceWorker;
using VpnetworkAPI.Services;
using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VpnetworkAPI.Models;
using Sieve.Services;
using BackgroundServiceWorker;

namespace VpnetworkAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<InterfaceGlobalProgramData, GlobalProgramDataRepository>();
            builder.Services.AddScoped<InterfaceUser, UserRepository>();
            //Add Services and Repository

            builder.Services.AddScoped<InterfaceAnalysis, AnalysisRepository>();

            builder.Services.AddScoped<SieveProcessor>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<UserDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("vpNetwork")));
            //builder.services.addhostedservice<modeltrainingservice>();
            builder.Services.AddHostedService<BackgroundServices>();
            //register automapper
            
            
            builder.Services.AddAutoMapper(typeof(Program));
            var app = builder.Build();
            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
