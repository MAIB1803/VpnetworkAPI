
using Microsoft.EntityFrameworkCore;
using VpnetworkAPI.DbContex;
using VpnetworkAPI.Repository;
using BackgroundServiceWorker;
using ModelTrainingServiceNamespace;
using VpnetworkAPI.Services;
using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VpnetworkAPI.Models;

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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<UserDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("vpNetwork")));
            builder.Services.AddHostedService<ModelTrainingService>();
            builder.Services.AddHostedService<BackgroundServices>();
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
