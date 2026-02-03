
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hotel.Domain.Contracts;
using Hotel.Persistence.Data.Contexts;
using Hotel.Persistence.Executor;
using Hotel.Persistence.Repositories;
using Hotel.Presentation.Controllers;
using Hotel.Presentation.Mapper.Rooms;
using Hotel.Presentation.Validations.Rooms;
using Hotel.Services.Interfaces;
using Hotel.Services.Mapper.Rooms;
using Hotel.Services.Rooms;
using Hotel_Reservation_System.Web.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;

namespace Hotel_Reservation_System.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddControllers()
                            .AddApplicationPart(typeof(RoomController).Assembly);
            builder.Services.AddValidatorsFromAssemblyContaining<GetAllRoomsWithPaginationViewModelValidator>();

            builder.Services.AddScoped<IRoomService,RoomService>();
            builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped<IRoomRepository,RoomRepository>();
            builder.Services.AddScoped<IAsyncQueryExecutor,EfAsyncQueryExecutor>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<TransactionMiddleware>();
            builder.Services.AddAutoMapper(typeof(RoomDtoProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(RoomViewModelProfile).Assembly);


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<TransactionMiddleware>();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
