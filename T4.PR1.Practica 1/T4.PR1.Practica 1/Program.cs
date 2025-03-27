using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using T5.PR1.Practica_1.Data;

namespace T5.PR1.Practica_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<EcoEnergyDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("EcoEnergyDatabase")));

            builder.Services.AddHttpClient("EcoEnergyAPI", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "EcoEnergy API",
                    Version = "v1",
                    Description = "API para gestión de datos energéticos"
                });
            });

            builder.Services.AddRazorPages();
            builder.Services.AddControllers();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EcoEnergy API v1");
                });

                using (var scope = app.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    try
                    {
                        var context = services.GetRequiredService<EcoEnergyDbContext>();
                        DbInitializer.Initialize(context); 
                    }
                    catch (Exception ex)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "Error durante la inicialización de datos");
                    }
                }
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<EcoEnergyDbContext>();
                    context.Database.EnsureDeleted(); 
                    context.Database.EnsureCreated();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Error durante la inicialización");
                }
            }

            app.Run();
        }
    }
}
 