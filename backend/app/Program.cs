using Early_warning.Hubs;
using Early_warning.Contexts;
using Early_warning.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Early_warning.Service;
using Microsoft.AspNetCore.Mvc;

namespace Early_warning
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add SignalR
            builder.Services.AddSignalR();

            // Add services to the container.
            builder.Services.AddDbContext<FloodContext>(Options =>
               Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDbContext<SensorDataContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("SensorConnection"));
            });
            // Register ONNX service

            //builder.Services.AddSingleton<OnnxModelService>(sp =>
            //    new OnnxModelService(Path.Combine(builder.Environment.ContentRootPath, "Models", "anomaly_detection.onnx")));
            //// ... existing code ...
            // Add this before registering OnnxModelService
         //   var nativePath = Path.Combine(builder.Environment.ContentRootPath, "OnnxNative");
            //Environment.SetEnvironmentVariable("PATH", nativePath + ";" + Environment.GetEnvironmentVariable("PATH"));

            // Register OnnxModelService
            /*  builder.Services.AddSingleton<OnnxModelService>(sp =>
              {
                  var logger = sp.GetRequiredService<ILogger<OnnxModelService>>();
                  var modelPath = Path.Combine(builder.Environment.ContentRootPath, "ModelAI", "anomaly_detector.onnx");

                  if (!File.Exists(modelPath))
                      throw new FileNotFoundException($"ONNX model file not found at: {modelPath}");

                  return new OnnxModelService(modelPath, logger);
              });*/


            // ... rest of your code ...
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actioncontext)
                =>
                {
                    //Select Errors in the ModelState for each Key that have error
                    var Errors = actioncontext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                        .SelectMany(P => P.Value.Errors)
                                                        .Select(E => E.ErrorMessage)
                                                        .ToArray();
                    //Beacuse its a group Then Select form each group the message
                    var Response = new 
                    {
                        Errors = Errors
                    };
                    return new BadRequestObjectResult(Response);

                };

            });
            builder.Services.AddSignalR();
            builder.Services.AddScoped<SensorService>();
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;
            var floodContext = services.GetRequiredService<FloodContext>();
            var sensorDataContext = services.GetRequiredService<SensorDataContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                // Apply migrations for both contexts
                await floodContext.Database.MigrateAsync();
                await sensorDataContext.Database.MigrateAsync();

                // Seed data for the flood context (make sure floodDbContextSeed is defined and injected properly)
              //  var floodDbContextSeed = services.GetRequiredService<floodDbContextSeed>(); // Assuming you have an interface for seeding
              //  await floodDbContextSeed.SeedDataAsync(floodContext);
            }
            catch (Exception ex)
            {
                // Log the exception if there is an error
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "There are problems during applying migrations or seeding data!");
            }



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapHub<SensorHub>("/sensorHub"); // ??? ?? ????? ???????

            app.Run();
        }
    }
}
