
using EmployeeService.Models;
using Microsoft.EntityFrameworkCore;
using EmployeeService.Controllers;
using Microsoft.Extensions.FileProviders;

namespace EmployeeService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the DI container.
            builder.Services.AddDbContext<NorthwindContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Northwind"));
            });

            builder.Services.AddCors(options => {
                options.AddPolicy(
                        name:"WebFront",
                        policy => {
                            policy.WithOrigins("https://localhost:7236").AllowAnyHeader().AllowAnyMethod();
                        }
                    );
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions { 
                FileProvider=new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "images")),
                RequestPath="/images"
            });
            app.UseAuthorization();
            app.UseCors();

            app.MapControllers();                        

            app.Run();
        }
    }
}
