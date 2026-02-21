
using Hotel_Reservation_System.Web.Middlewares;

namespace Hotel_Reservation_System.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            DependencyInjection.AddWebServices(builder.Services, builder.Configuration);

            var app = builder.Build();
            
            app.UseAuthentication();
            app.UseAuthorization();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            //app.UseMiddleware<GlobalErrorHandlerMiddleware>();
            app.UseMiddleware<TransactionMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
